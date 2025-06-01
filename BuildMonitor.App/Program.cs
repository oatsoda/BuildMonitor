using BuildMonitor.App.Properties;
using BuildMonitor.Core;
using BuildMonitor.TfsOnline;
using BuildMonitor.UI.Controls;
using BuildMonitor.UI.Options;
using BuildMonitor.UI.Updater;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace BuildMonitor.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var versionUrl = ConfigurationManager.AppSettings["VersionUrl"];
            var installUrl = ConfigurationManager.AppSettings["InstallUrl"];

            var updater = new AppUpdater(versionUrl, installUrl);
            if (updater.CheckForUpdates())
                return;

            IBuildStoreFactory buildStoreFactory = new TfsOnlineBuildStoreFactory();
            IBuildDefinitionMonitor monitor = new BuildDefinitionMonitor(buildStoreFactory);

            var options = new MonitorOptions(Settings.Default);

            Application.Run(
                new BuildDefinitionsListForm(monitor, options, buildStoreFactory, updater)
                );
        }
    }
}
