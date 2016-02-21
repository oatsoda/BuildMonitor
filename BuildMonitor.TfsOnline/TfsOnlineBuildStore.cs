using BuildMonitor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BuildMonitor.TfsOnline
{
    public class TfsOnlineBuildStore : IBuildStore
    {
        private readonly NetworkCredential m_SpecificCredentials;
        private readonly Uri m_BaseUrl;
        private readonly bool m_IncludeRunningBuilds;
        
        public TfsOnlineBuildStore(IMonitorOptions options)
        {
            if (!options.UseCredentials)
                throw new ArgumentOutOfRangeException();

            if (options.Credential == null)
                throw new ArgumentOutOfRangeException();
            
            m_BaseUrl = new Uri(
                string.Format($"https://{options.TfsApiUrl}.visualstudio.com/DefaultCollection/")
                );
            
            m_SpecificCredentials = options.UseCredentials ? options.Credential : null;
            m_IncludeRunningBuilds = options.IncludeRunningBuilds;
        }

        public async Task<IEnumerable<string>> GetProjects()
        {
            var queryPath = "_apis/projects?api-version=1.0&statefilter=wellFormed";

            var result = await GetTfsResult(queryPath);

            var projects = result["value"].Children();

            return projects.Select(p => p["name"].Value<string>());
        }

        public async Task<IEnumerable<IBuildDefinition>> GetDefinitions(string projectName)
        {
            var projectNameEncoded = WebUtility.UrlEncode(projectName);
            var queryPath = $"{projectNameEncoded}/_apis/build/definitions?api-version=2.0";

            var result = await GetTfsResult(queryPath);

            var definitions = result["value"].Children();

            var buildDefinitions = definitions.Select(b => new BuildDefinition
            {
                Id = b["id"].Value<int>(),
                Name = b["name"].Value<string>(),
                IsVNext = b["type"]?.Value<string>() == "build"
            });

            return await Task.WhenAll(
                buildDefinitions.Select(d => GetDefinitionDetail(projectName, d))
                );
        }
        

        private async Task<IBuildDefinition> GetDefinitionDetail(string projectName, BuildDefinition definition)
        {
            var projectNameEncoded = WebUtility.UrlEncode(projectName);
            var queryPath = $"{projectNameEncoded}/_apis/build/definitions/{definition.Id}?api-version=2.0";

            var definitionDetail = await GetTfsResult(queryPath);

            definition.Url = definitionDetail["_links"]["web"]["href"].Value<string>();

            return definition;
        }

        public async Task<IBuildStatus> GetLatestBuild(string projectName, IBuildDefinition definition)
        {
            var projectNameEncoded = WebUtility.UrlEncode(projectName);
            var includeRunningFilter = m_IncludeRunningBuilds ? ",inProgress" : "";
            var queryPath = $"{projectNameEncoded}/_apis/build/builds?api-version=2.0";

            queryPath = string.Join("&", 
                queryPath, 
                $"definitions={definition.Id}", 
                "resultFilter=succeeded,partiallySucceeded,failed",
                $"statusFilter=completed{includeRunningFilter}", "$top=1");

            var result = await GetTfsResult(queryPath);

            var builds = result["value"].Children();

            if (!builds.Any())
                return null;

            var b = builds.OrderByDescending(t => t["startTime"]).First();

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
        
        private async Task<IBuildStatus> GetBuildTimeline(string projectName, BuildStatus buildStatus)
        {
            var projectNameEncoded = WebUtility.UrlEncode(projectName);
            var queryPath = $"{projectNameEncoded}/_apis/build/builds/{buildStatus.Id}/timeline?api-version=2.0";

            var buildTimeline = await GetTfsResult(queryPath);

            var tasks = buildTimeline["records"].Children();

            if (!tasks.Any())
                return buildStatus;

            buildStatus.ErrorCount = tasks.Sum(t => t["errorCount"]?.Value<int>()) ?? 0;
            buildStatus.WarningCount = tasks.Sum(t => t["warningCount"]?.Value<int>()) ?? 0;

            return buildStatus;
        }

        private async Task<JObject> GetTfsResult(string queryPath)
        {
            var resultJson = await GetJson(queryPath);

            return JObject.Parse(resultJson);
        }
        
        private async Task<string> GetJson(string queryPath)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(
                            $"{m_SpecificCredentials.UserName}:{m_SpecificCredentials.Password}")));

                using (var response = client.GetAsync(FormatUrl(queryPath)).Result)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                        throw new AuthenticationException();

                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }

        }

        private Uri FormatUrl(string queryPath)
        {
            return new Uri(m_BaseUrl, queryPath.TrimStart('/'));
        }

        private Status StatusFromString(string statusString)
        {
            switch (statusString)
            {
                case "succeeded":
                    return Status.Succeeded;
                case "partiallySucceeded":
                    return Status.PartiallySucceeded;
                case "failed":
                    return Status.Failed;
                case "inProgress":
                    return Status.InProgress;

                default:
                    throw new ArgumentOutOfRangeException(nameof(statusString));
            }
        }

    }
    
}
