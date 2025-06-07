using BuildMonitor.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuildMonitor.ADO
{
    public sealed class ADOBuildStore : IBuildStore, IDisposable
    {
        private readonly bool m_IncludeRunningBuilds;
        private readonly HttpClient m_HttpClient;

        public ADOBuildStore(IMonitorOptions options)
        {
            var baseUrl = new Uri(
                string.Format($"https://{options.AzureDevOpsOrganisation}.visualstudio.com/DefaultCollection/")
                );

            m_HttpClient = new HttpClient() { BaseAddress = baseUrl };
            m_HttpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            m_HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        $"{options.PersonalAccessTokenPlainText}:")));

            m_IncludeRunningBuilds = options.IncludeRunningBuilds;
        }

        public record ADOProject(string Name);
        public record ADOProjects(int Count, ADOProject[] Value);

        public async Task<IEnumerable<string>> GetProjects()
        {
            var queryPath = "_apis/projects?api-version=7.1";

            var projects = await GetADOResult<ADOProjects>(queryPath);

            return projects.Value
                .Select(p => p.Name)
                .OrderBy(p => p);
        }

        public async Task<IEnumerable<BuildDefinition>> GetDefinitions(string projectName)
        {
            var projectNameEncoded = Uri.EscapeDataString(projectName);
            var queryPath = $"{projectNameEncoded}/_apis/build/definitions?api-version=2.0";

            var result = await GetADOResult(queryPath);

            var jsonDefinitions = result["value"].Children();

            return await Task.WhenAll(
                jsonDefinitions.Select(async d =>
                {
                    int id = d["id"].Value<int>();
                    var detail = await GetDefinitionDetail(projectName, id);
                    return new BuildDefinition
                    {
                        Id = id,
                        Name = d["name"].Value<string>(),
                        IsVNext = d["type"]?.Value<string>() == "build",
                        Url = detail.Url
                    };
                })
                );
        }

        public record BuildDefinitionDetail(string Url);

        private async Task<BuildDefinitionDetail> GetDefinitionDetail(string projectName, int definitionId)
        {
            var projectNameEncoded = Uri.EscapeDataString(projectName);
            var queryPath = $"{projectNameEncoded}/_apis/build/definitions/{definitionId}?api-version=2.0";

            var definitionDetail = await GetADOResult(queryPath);

            return new(definitionDetail["_links"]["web"]["href"].Value<string>());
        }

        public async Task<BuildStatus> GetLatestBuild(string projectName, BuildDefinition definition)
        {
            var projectNameEncoded = Uri.EscapeDataString(projectName);
            var includeRunningFilter = m_IncludeRunningBuilds ? ",inProgress" : "";
            var queryPath = $"{projectNameEncoded}/_apis/build/builds?api-version=2.0";

            queryPath = string.Join("&",
                queryPath,
                $"definitions={definition.Id}",
                $"statusFilter=completed{includeRunningFilter}"
                );

            var result = await GetADOResult(queryPath);

            var builds = result["value"].Children();

            if (!builds.Any())
                return null;

            var b = builds
                // This should leave: succeeded, partiallySucceeded, failed and, if
                // the option was added above, builds without a result - i.e. inProgress status
                .Where(t => (t["result"]?.Value<string>() ?? string.Empty) != "canceled")
                .OrderByDescending(t => t["startTime"])
                .FirstOrDefault();

            if (b == null)
                return null;

            var buildStatus = new BuildStatus
            {
                Id = b["id"].Value<int>(),
                Name = b["buildNumber"].Value<string>(),
                Url = b["_links"]["web"]["href"].Value<string>(),
                Start = b["startTime"].Value<DateTime>(),
                Finish = b["finishTime"]?.Value<DateTime>(),
                Status = StatusFromString((b["result"] ?? b["status"]).Value<string>()),
                RequestedBy = b["requestedFor"]["displayName"].Value<string>()
            };

            if (definition.IsVNext)
                return await GetBuildTimeline(projectName, buildStatus);

            return buildStatus;
        }

        public record ADOTimelineRecord(int ErrorCount, int WarningCount);
        public record ADOTimeline(ADOTimelineRecord[] Records);

        private async Task<BuildStatus> GetBuildTimeline(string projectName, BuildStatus buildStatus)
        {
            var projectNameEncoded = Uri.EscapeDataString(projectName);
            var queryPath = $"{projectNameEncoded}/_apis/build/builds/{buildStatus.Id}/timeline?api-version=2.0";

            var buildTimeline = await GetADOResult<ADOTimeline>(queryPath);

            if (buildTimeline == null)
                return buildStatus;

            buildStatus.ErrorCount = buildTimeline.Records.Sum(r => r.ErrorCount);
            buildStatus.WarningCount = buildTimeline.Records.Sum(r => r.WarningCount);

            return buildStatus;
        }

        private static readonly JsonSerializerOptions s_JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private async Task<T> GetADOResult<T>(string queryPath)
        {
            var json = await GetADOJsonResult(queryPath);

            return JsonSerializer.Deserialize<T>(json, s_JsonOptions);
        }

        private async Task<JObject> GetADOResult(string queryPath)
        {
            var json = await GetADOJsonResult(queryPath);

            if (json == null)
                return null;
            return JObject.Parse(json);
        }

        private async Task<string> GetADOJsonResult(string path)
        {
            using var response = await m_HttpClient.GetAsync(path);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new AuthenticationException();

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }


        private static Status StatusFromString(string statusString)
        {
            return statusString switch
            {
                "succeeded" => Status.Succeeded,
                "partiallySucceeded" => Status.PartiallySucceeded,
                "failed" => Status.Failed,
                "inProgress" => Status.InProgress,
                _ => throw new ArgumentOutOfRangeException(nameof(statusString)),
            };
        }

        public void Dispose()
        {
            m_HttpClient?.Dispose();
        }
    }

    public static class JsonSerializerExtensions
    {
        public static T DeserializeAnonymousType<T>(this string json, T _)
            => JsonSerializer.Deserialize<T>(json);
    }
}
