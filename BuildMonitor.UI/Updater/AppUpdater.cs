using BuildMonitor.UI.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildMonitor.UI.Updater
{
    public class AppUpdater : IAppUpdater
    {
        private readonly HttpClient m_HttpClient = new();

        private readonly string m_VersionUrl;
        private readonly string m_LatestBinaryUrl;

        public AppUpdater(string versionUrl, string installUrl)
        {
            m_VersionUrl = versionUrl;
            m_LatestBinaryUrl = installUrl;
        }

        public async Task<bool> CheckForUpdates()
        {
            var latestVersion = await GetLatestVersion();

            if (latestVersion <= VersionHelper.Version)
                return false;

            var msg = $"A newer version ({latestVersion.ToString(3)}) of {VersionHelper.AppName} is available? Do you want to download and install it?";

            if (DialogResult.Cancel == MessageBox.Show(
                msg,
                @"New Version",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button2
                ))
                return false;

            RunUpdate();
            return true;
        }

        private async Task<Version> GetLatestVersion()
        {
            string latestVersion = await m_HttpClient.GetStringAsync(m_VersionUrl);

            return Version.Parse(latestVersion);
        }

        private void RunUpdate()
        {
            LinkHelper.OpenUrl(m_LatestBinaryUrl);
        }
    }
}
