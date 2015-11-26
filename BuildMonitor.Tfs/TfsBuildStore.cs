using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BuildMonitor.Core;
using Newtonsoft.Json.Linq;

namespace BuildMonitor.Tfs
{
    public class TfsBuildStore : IBuildStore
    {
        private readonly Uri m_BaseUrl;
        private readonly bool m_IncludeRunningBuilds;

        private NetworkCredential SpecificCredentials { get; }

        public TfsBuildStore(IMonitorOptions options)
        {
            m_BaseUrl = new Uri(options.TfsApiUrl);
            SpecificCredentials = options.UseCredentials ? options.Credential :  null;
            m_IncludeRunningBuilds = options.IncludeRunningBuilds;
        }

        public async Task<IEnumerable<string>> GetProjects()
        {
            var queryPath = "projects?api-version=1.0&statefilter=wellFormed";

            var projects = await GetTfsResult(queryPath);

            return projects.Select(p => p["name"].Value<string>());
        }

        public async Task<IEnumerable<IBuildDefinition>> GetDefinitions(string projectName)
        {
            var queryPath = string.Format("build/definitions?api-version=1.0&projectname={0}", WebUtility.UrlEncode(projectName));

            var definitions = await GetTfsResult(queryPath);

            return definitions.Select(b => new BuildDefinition
            {
                Id = b["id"].Value<int>(),
                Name = b["name"].Value<string>(),
                Url = b["url"].Value<string>(),
                DropLocation = b["defaultDropLocation"].Value<string>(),
                TriggerType = b["triggerType"].Value<string>()
            }).ToList();
        }

        public async Task<IBuildStatus> GetLatestBuild(string projectName, IBuildDefinition definition)
        {
            var queryPath =
                string.Format(
                    "build/builds?api-version=1.0&projectname={0}&definition={1}&status=Succeeded,PartiallySucceeded,Failed{2}", //&$top=1
                   WebUtility.UrlEncode(projectName),
                   WebUtility.UrlEncode(definition.Name),
                   m_IncludeRunningBuilds ? ",InProgress" : "");

            var builds = await GetTfsResult(queryPath);

            if (!builds.Any())
                return null;

            var b = builds.OrderByDescending(t => t["startTime"]).First();

            return new BuildStatus
            {
                Id = b["id"].Value<int>(),
                Name = b["buildNumber"].Value<string>(),
                Url = b["url"].Value<string>(),
                Start = b["startTime"].Value<DateTime>(),
                Finish = b["finishTime"]?.Value<DateTime>(),
                Status = StatusFromString(b["status"].Value<string>()),
                RequestedBy = b["requests"][0]["requestedFor"]["displayName"].Value<string>()
            };
        }
        
        private async Task<JEnumerable<JToken>> GetTfsResult(string queryPath)
        {
            var resultJson = await GetJson(queryPath);

            var result = JObject.Parse(resultJson);

            return result["value"].Children();
        } 

        private async Task<string> GetJson(string queryPath)
        {
            var url = FormatUrl(queryPath);

            using (var client = new WebClient())
            {
                client.Credentials = SpecificCredentials ?? CredentialCache.DefaultCredentials;
                var result = await client.DownloadStringTaskAsync(url);
                return result;
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
                case "succeeded" :
                    return Status.Succeeded;
                case "partiallySucceeded" :
                    return Status.PartiallySucceeded;
                case "failed" :
                    return Status.Failed;
                case "inProgress" :
                    return Status.InProgress;

                default :
                    throw new ArgumentOutOfRangeException(nameof(statusString));
            }
        }
    }
}
