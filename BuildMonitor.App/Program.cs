using BuildMonitor.Core;
using BuildMonitor.TfsOnline;
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
        private const string _BASE_URL = "https://raw.githubusercontent.com/oatsoda/BuildMonitor/master/";

        private const string _VERSION_URL = _BASE_URL + "Current.ver";
        private const string _INSTALL_URL = _BASE_URL + "Binaries/BuildMonitor.Setup.msi";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var updater = new AppUpdater(_VERSION_URL, _INSTALL_URL);
            if (await updater.CheckForUpdates())
                return;

            IBuildStoreFactory buildStoreFactory = new TfsOnlineBuildStoreFactory();
            IBuildDefinitionMonitor monitor = new BuildDefinitionMonitor(buildStoreFactory);

            var options = new MonitorOptions();

            Application.Run(
                new BuildDefinitionsListForm(monitor, options, buildStoreFactory, updater)
                );
        }
    }
}
