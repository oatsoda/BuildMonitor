using System;
using System.Configuration;
using System.Windows.Forms;
using BuildMonitor.Core;
using BuildMonitor.TfsOnline;
using BuildMonitor.UI.Controls;
using BuildMonitor.UI.Options;
using BuildMonitor.UI.Updater;

namespace BuildMonitor.UI
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
            
            var updater = new AppUpdater();
            if (updater.CheckForUpdates())
                return;

            IBuildStoreFactory buildStoreFactory = new TfsOnlineBuildStoreFactory();
            IBuildDefinitionMonitor monitor = new BuildDefinitionMonitor(buildStoreFactory);

            var options = new MonitorOptions();

            Application.Run(new BuildDefinitionsListForm(monitor, options, buildStoreFactory));
        }
    }
}
