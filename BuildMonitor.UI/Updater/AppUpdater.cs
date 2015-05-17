using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace BuildMonitor.UI.Updater
{
    public class AppUpdater
    {
        private readonly string m_VersionUrl;
        private readonly string m_LatestBinaryUrl;

        private Version CurrentVersion
        {
            get { return Assembly.GetAssembly(GetType()).GetName().Version; }
        }

        public AppUpdater()
        {
            m_VersionUrl = ConfigurationManager.AppSettings["VersionUrl"];
            m_LatestBinaryUrl = ConfigurationManager.AppSettings["InstallUrl"];
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
            string latestVersion = null;
            using (var wc = new WebClient())
            {
                latestVersion = wc.DownloadString(m_VersionUrl);
            }

            return Version.Parse(latestVersion);
        }

        public void RunUpdate()
        {
            var si = new ProcessStartInfo(m_LatestBinaryUrl);
            Process.Start(si);
        }
    }
}
