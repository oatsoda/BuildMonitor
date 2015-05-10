using System.Windows.Forms;
using Microsoft.Win32;

namespace BuildMonitor.UI.Helpers
{
    internal class StartupSettingHelper
    {
        private const string _APP_NAME = "BuildMonitor";
        private const string _REG_KEY = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static bool RunOnStartup
        {
// ReSharper disable once PossibleNullReferenceException - Expect Key to always be there (std Windows thing)
            get { return Registry.CurrentUser.OpenSubKey(_REG_KEY).GetValue(_APP_NAME) != null; }
        }
        
        public static void SetStartup(bool runOnStartup)
        {
            var key = Registry.CurrentUser.OpenSubKey(_REG_KEY, true);

            // ReSharper disable PossibleNullReferenceException - Expect Key to always be there (std Windows thing)
            if (runOnStartup)
                key.SetValue(_APP_NAME, Application.ExecutablePath);
            else
                key.DeleteValue(_APP_NAME, false);
            // ReSharper restore PossibleNullReferenceException - Expect Key to always be there (std Windows thing)
        }
    }
}
