using BuildMonitor.Core;
using BuildMonitor.Core.ADO;
using BuildMonitor.UI.Controls;
using BuildMonitor.UI.Options;
using BuildMonitor.UI.Updater;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildMonitor.App
{
    static class Program
    {
        private const string _BASE_URL = "https://raw.githubusercontent.com/oatsoda/BuildMonitor/main/";
        private const string _FALLBACK_BASE_URL = "https://raw.githubusercontent.com/oatsoda/BuildMonitor/master/";

        private const string _VERSION_URL = "Current.ver";
        private const string _INSTALL_URL = "Binaries/BuildMonitor.Setup.msi";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var updater = new AppUpdater([_BASE_URL, _FALLBACK_BASE_URL], _VERSION_URL, _INSTALL_URL);
            if (await updater.CheckForUpdates())
                return;

            var buildStoreFactory = new ADOBuildStoreFactory();
            var monitor = new BuildDefinitionMonitor(buildStoreFactory);

            var options = new MonitorOptions();

            Application.Run(
                new BuildDefinitionsListForm(monitor, options, buildStoreFactory, updater)
                );
        }
    }
}
