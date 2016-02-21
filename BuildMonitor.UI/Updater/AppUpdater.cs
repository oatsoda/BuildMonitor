using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace BuildMonitor.UI.Updater
{
    public class AppUpdater : IAppUpdater
    {
        private readonly string m_VersionUrl;
        private readonly string m_LatestBinaryUrl;

        private Version CurrentVersion => Assembly.GetAssembly(GetType()).GetName().Version;

        public AppUpdater(string versionUrl, string installUrl)
        {
            m_VersionUrl = versionUrl; //ConfigurationManager.AppSettings["VersionUrl"];
            m_LatestBinaryUrl = installUrl; //ConfigurationManager.AppSettings["InstallUrl"];
        }

        public bool CheckForUpdates()
        {
            var latestVersion = GetLatestVersion();

            if (latestVersion <= CurrentVersion)
                return false;

            if (DialogResult.Cancel == MessageBox.Show(
                "A newer version of Build Monitor is available? Do you want to download and install it?",
                "New Version",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button2
                ))
                return false;

            RunUpdate();
            return true;
        }

        private Version GetLatestVersion()
        {
            string latestVersion;
            using (var wc = new WebClient())
            {
                latestVersion = wc.DownloadString(m_VersionUrl);
            }

            return Version.Parse(latestVersion);
        }

        private void RunUpdate()
        {
            var si = new ProcessStartInfo(m_LatestBinaryUrl);
            Process.Start(si);
        }
    }
}
