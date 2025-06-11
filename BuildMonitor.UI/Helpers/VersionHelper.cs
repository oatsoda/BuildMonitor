using System;
using System.Reflection;

namespace BuildMonitor.UI.Helpers
{
    internal static class VersionHelper
    {
        private static readonly Version s_Version = Assembly.GetAssembly(typeof(VersionHelper))!.GetName()!.Version!;

        public static Version Version => s_Version;
        public static string VersionString => s_Version.ToString(3);


        public static string AppName { get; } = "Azure DevOps Pipeline Monitor";
    }
}
