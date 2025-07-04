using BuildMonitor.UI.Controls;
using BuildMonitor.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildMonitor.UI.Updater
{
    public sealed class AppUpdater : IAppUpdater, IDisposable
    {
        private readonly List<HttpClient> m_HttpClients;

        private readonly string m_VersionPath;
        private readonly string m_LatestBinaryUrl;

        public AppUpdater(string[] baseUrls, string versionPath, string latestBinaryPath)
        {
            // Not really the way HttpClient should be used, but this AppUpdater is used once on startup, and on request.
            m_HttpClients = [.. baseUrls.Select(baseUrl => new HttpClient { BaseAddress = new Uri(baseUrl) })];

            m_VersionPath = versionPath;
            m_LatestBinaryUrl = latestBinaryPath;
        }

        public async Task<bool> CheckForUpdates()
        {
            var checkResult = await GetLatestVersion();

            if (!checkResult.IsSuccess)
            {
                MessageBox.Show(
                    @"Failed to check for updates. Please try again later.",
                    @"Update Check Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (checkResult.LatestVersion <= VersionHelper.Version)
                return false;

            var msg = $"A newer version ({checkResult.LatestVersion!.ToString(3)}) of {VersionHelper.AppName} is available? Do you want to download and install it?";

            if (DialogResult.Cancel == MessageBox.Show(
                msg,
                @"New Version",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button2
                ))
                return false;

            LinkHelper.OpenUrl(checkResult.DownloadUrl!);
            return true;
        }

        private record CheckResult(bool IsSuccess, Version? LatestVersion, int? UrlIndex, string? DownloadUrl);

        private async Task<CheckResult> GetLatestVersion()
        {
            foreach ((var httpClient, int index) in m_HttpClients.Select((c, i) => (c, i)))
            {
                try
                {
                    string latestVersion = await httpClient.GetStringAsync(m_VersionPath);

                    return new CheckResult(true,
                        Version.Parse(latestVersion),
                        index,
                        new Uri(httpClient.BaseAddress!, m_LatestBinaryUrl).ToString()
                        );
                }
                catch (Exception ex)
                {
                    AboutForm.AddException(ex);
                }
            }

            return new CheckResult(false, null, null, null);
        }

        public void Dispose() => m_HttpClients.ForEach(h => h.Dispose());
    }
}
