using BuildMonitor.Core;
using BuildMonitor.Tfs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
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
            
            m_BaseUrl = new Uri(options.TfsApiUrl);
            m_SpecificCredentials = options.UseCredentials ? options.Credential : null;
            m_IncludeRunningBuilds = options.IncludeRunningBuilds;
        }

        public IEnumerable<string> GetProjects()
        {
            var queryPath = "_apis/projects?api-version=1.0&statefilter=wellFormed";

            var projects = GetTfsResult(queryPath)["value"].Children();

            return projects.Select(p => p["name"].Value<string>());
        }

        public IEnumerable<IBuildDefinition> GetDefinitions(string projectName)
        {
            var queryPath = string.Format("{0}/_apis/build/definitions?api-version=2.0", WebUtility.UrlEncode(projectName));

            var definitions = GetTfsResult(queryPath)["value"].Children();

            var buildDefinitions = definitions.Select(b => new BuildDefinition
            {
                Id = b["id"].Value<int>(),
                Name = b["name"].Value<string>()
            });

            return buildDefinitions.Select(d => GetDefinitionDetail(projectName, d))
                .ToList();
        }

        private IBuildDefinition GetDefinitionDetail(string projectName, BuildDefinition definition)
        {
            var queryPath = string.Format("{0}/_apis/build/definitions/{1}?api-version=2.0", 
                WebUtility.UrlEncode(projectName), definition.Id);

            var definitionDetail = GetTfsResult(queryPath);

            definition.Url = definitionDetail["_links"]["web"]["href"].Value<string>();
            definition.DropLocation = definitionDetail["defaultDropLocation"].Value<string>();
            definition.TriggerType = definitionDetail["triggerType"].Value<string>();

            return definition;
        }

        public IBuildStatus GetLatestBuild(string projectName, IBuildDefinition definition)
        {
            var queryPath =
                string.Format(
                    "{0}/_apis/build/builds?api-version=2.0&definitions={1}&resultFilter=succeeded,partiallySucceeded,failed&statusFilter=completed{2}&$top=1",
                   WebUtility.UrlEncode(projectName),
                   definition.Id,
                   m_IncludeRunningBuilds ? ",inProgress" : "");

            var builds = GetTfsResult(queryPath)["value"].Children();

            if (!builds.Any())
                return null;

            var b = builds.OrderByDescending(t => t["startTime"]).First();

            return new BuildStatus
            {
                Id = b["id"].Value<int>(),
                Name = b["buildNumber"].Value<string>(),
                Url = b["_links"]["web"]["href"].Value<string>(),
                Start = b["startTime"].Value<DateTime>(),
                Finish = b["finishTime"] == null ? (DateTime?)null : b["finishTime"].Value<DateTime>(),
                Status = StatusFromString((b["result"] ?? b["status"]).Value<string>()),
                RequestedBy = b["requestedFor"]["displayName"].Value<string>()
            };
        }

        private JObject GetTfsResult(string queryPath)
        {
            var resultJson = GetJson(queryPath);

            return JObject.Parse(resultJson);
        }
        
        private string GetJson(string queryPath)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", m_SpecificCredentials.UserName, m_SpecificCredentials.Password))));

                using (var response = client.GetAsync(FormatUrl(queryPath)).Result)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                        throw new AuthenticationException();

                    response.EnsureSuccessStatusCode();
                    var t = response.Content.ReadAsStringAsync();
                    t.Wait();
                    return t.Result;
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
                    throw new ArgumentOutOfRangeException("statusString");
            }
        }

    }
    
}
