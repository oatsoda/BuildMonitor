using BuildMonitor.Core;
using BuildMonitor.Tfs;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BuildMonitor.TfsOnline
{
    public class TfsOnlineBuildStore : TfsBuildStore
    {
        public TfsOnlineBuildStore(IMonitorOptions options) : base(options)
        {
            if (!options.UseCredentials)
                throw new ArgumentOutOfRangeException();

            if (options.Credential == null)
                throw new ArgumentOutOfRangeException();
        }

        protected override string GetJson(string queryPath)
        {
            //var basicCred = new BasicAuthCredential(m_SpecificCredentials);
            //var tfsCred = new TfsClientCredentials(basicCred);
            //tfsCred.AllowInteractive = false;

            //var tpc = new TfsTeamProjectCollection(
            //    m_BaseUrl,
            //    tfsCred);

            //tpc.Authenticate();

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

                    response.EnsureSuccessStatusCode();
                    var t = response.Content.ReadAsStringAsync();
                    t.Wait();
                    return t.Result;
                }
            }

        }
    }
}
