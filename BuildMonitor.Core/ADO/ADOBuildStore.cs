using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BuildMonitor.Core.ADO
{
    public sealed class ADOBuildStore : IBuildStore, IDisposable
    {
        private readonly HttpClient m_HttpClient;

        private readonly bool m_IncludeRunningBuilds;
        private readonly string m_ProjectNameUrlEncoded;

        public ADOBuildStore(IMonitorOptions options, bool forValidatingOnly)
        {
            var baseUrl = new Uri(
                string.Format($"https://dev.azure.com/{Uri.EscapeDataString(options.AzureDevOpsOrganisation)}/")
                );

            m_HttpClient = new HttpClient() { BaseAddress = baseUrl };
            m_HttpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            m_HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        $"{options.PersonalAccessTokenPlainText}:")));

            m_IncludeRunningBuilds = options.IncludeRunningBuilds;
            m_ProjectNameUrlEncoded = forValidatingOnly ? "" : Uri.EscapeDataString(options.ProjectName);
        }

        public record ADOProject(string Name);
        public record ADOListResult<T>(int Count, T[] Value);

        public async Task<IEnumerable<string>> GetProjects()
        {
            // https://learn.microsoft.com/en-us/rest/api/azure/devops/core/projects/list?view=azure-devops-rest-7.1&tabs=HTTP
            var queryPath = "_apis/projects?api-version=7.1";

            var projects = await GetADOResult<ADOListResult<ADOProject>>(queryPath);

            return projects.Value
                .Select(p => p.Name)
                .OrderBy(p => p);
        }

        public record ADOLink(string Href);
        public record ADOLinks(ADOLink Web);

        public enum DefinitionType { Build, Xaml };
        public record ADOBuildDefinition(int Id, string Name, DefinitionType Type,
            [property: JsonPropertyName("_links")] ADOLinks Links);

        public async Task<IEnumerable<BuildDefinition>> GetDefinitions()
        {
            // https://learn.microsoft.com/en-us/rest/api/azure/devops/build/definitions/list?view=azure-devops-rest-7.1
            var queryPath = $"{m_ProjectNameUrlEncoded}/_apis/build/definitions?api-version=7.1";

            var definitions = await GetADOResult<ADOListResult<ADOBuildDefinition>>(queryPath);

            return definitions.Value.Select(d
                => new BuildDefinition
                {
                    Id = d.Id,
                    Name = d.Name,
                    IsVNext = d.Type == DefinitionType.Build,
                    Url = d.Links.Web.Href
                });
        }

        public record ADOBuildRequestedFor(string DisplayName);
        public record ADOBuild(int Id, string BuildNumber, ADOStatus Status, ADOResult Result,
            DateTimeOffset StartTime, DateTimeOffset? FinishTime, ADOBuildRequestedFor RequestedFor,
            [property: JsonPropertyName("_links")] ADOLinks Links);
        public enum ADOResult { None, Succeeded, PartiallySucceeded, Canceled, Failed };
        public enum ADOStatus { None, InProgress, Completed, NotStarted, Postponed, Canceling, All };

        public async Task<BuildStatus?> GetLatestBuild(BuildDefinition definition)
        {
            // https://learn.microsoft.com/en-us/rest/api/azure/devops/build/builds/get?view=azure-devops-rest-7.1
            var statuses = m_IncludeRunningBuilds ? "completed,inProgress" : "completed";
            var queryPath = $"{m_ProjectNameUrlEncoded}/_apis/build/builds?api-version=2.0";

            queryPath = string.Join("&",
                queryPath,
                $"definitions={definition.Id}",
                $"statusFilter={statuses}"
                );

            var result = await GetADOResult<ADOListResult<ADOBuild>>(queryPath);

            var builds = result.Value;

            if (builds.Length == 0)
                return null;

            var b = builds
                // This should leave: succeeded, partiallySucceeded, failed and, if
                // the option was added above, builds without a result - i.e. inProgress status
                .Where(t => t.Result != ADOResult.Canceled)
                .OrderByDescending(t => t.StartTime)
                .FirstOrDefault();

            if (b == null)
                return null;

            var buildStatus = new BuildStatus
            {
                Id = b.Id,
                Name = b.BuildNumber,
                Url = b.Links.Web.Href,
                Start = b.StartTime,
                Finish = b.FinishTime,
                Status = b.Status == ADOStatus.InProgress ? Status.InProgress : ToStatus(b.Result),
                RequestedBy = b.RequestedFor.DisplayName
            };

            if (definition.IsVNext)
                return await GetBuildTimeline(buildStatus);

            return buildStatus;
        }

        public record ADOTimelineRecord(int ErrorCount, int WarningCount);
        public record ADOTimeline(ADOTimelineRecord[] Records);

        private async Task<BuildStatus> GetBuildTimeline(BuildStatus buildStatus)
        {
            // https://learn.microsoft.com/en-us/rest/api/azure/devops/build/timeline/get?view=azure-devops-rest-7.1
            var queryPath = $"{m_ProjectNameUrlEncoded}/_apis/build/builds/{buildStatus.Id}/timeline?api-version=2.0";

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
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };

        private async Task<T> GetADOResult<T>(string queryPath)
        {
            var json = await GetADOJsonResult(queryPath);

            return JsonSerializer.Deserialize<T>(json, s_JsonOptions)!;
        }

        private async Task<string> GetADOJsonResult(string path)
        {
            using var response = await m_HttpClient.GetAsync(path);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new AuthenticationException();

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private static Status ToStatus(ADOResult result)
        {
            return result switch
            {
                ADOResult.Succeeded => Status.Succeeded,
                ADOResult.PartiallySucceeded => Status.PartiallySucceeded,
                ADOResult.Failed => Status.Failed,
                _ => throw new ArgumentOutOfRangeException(nameof(result), result, $"Result '{result}' is not expected."),
            };
        }

        public void Dispose()
        {
            m_HttpClient?.Dispose();
        }
    }
}
