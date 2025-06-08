using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace BuildMonitor.UI.Helpers
{
    internal static class StartupSettingHelper
    {
        private const string _APP_NAME = "BuildMonitor";
        private const string _REG_KEY = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        private static RegistryKey GetKey(bool forWriting = false)
        {
            var regKey = Registry.CurrentUser.OpenSubKey(_REG_KEY);

            if (regKey == null)
                throw new InvalidOperationException($"Registry key '{_REG_KEY}' could not be opened for {(forWriting ? "writing" : "reading")}.");

            return regKey;
        }

        public static bool RunOnStartup => GetKey().GetValue(_APP_NAME) != null;

        public static void SetStartup(bool runOnStartup)
        {
            if (runOnStartup)
                GetKey(true).SetValue(_APP_NAME, Application.ExecutablePath);
            else
                GetKey(true).DeleteValue(_APP_NAME, false);
        }
    }
}
