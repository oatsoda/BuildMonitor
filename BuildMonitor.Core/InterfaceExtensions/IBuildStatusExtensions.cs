using System;

namespace BuildMonitor.Core.InterfaceExtensions
{
    public static class InterfaceBuildStatusExtensions
    {
        public static string ToCurrentTimeDescription(this IBuildStatus status)
        {
            // extension methods still execute on NULL instances.
            if (status == null)
                return "-";

            var diff = DateTime.UtcNow.Subtract(status.Start);

            if (diff.TotalHours >= 48)
                return string.Format("{0} days ago", (int)diff.TotalDays);

            if (diff.TotalMinutes >= 120)
                return string.Format("{0} hours ago", (int)diff.TotalHours);

            if (diff.TotalMinutes >= 1)
                return string.Format("{0} minutes ago", (int)diff.TotalMinutes);

            if (diff.TotalSeconds > 1)
                return string.Format("{0} seconds ago", (int)diff.TotalSeconds);

            return "Just now";
        }

        public static string ToRequestedByDescription(this IBuildStatus status, int trimLen)
        {
            if (status == null || status.RequestedBy == null)
                return "-";

            if (status.RequestedBy.Length > trimLen)
                return string.Format("{0}...", status.RequestedBy.Substring(0, trimLen));

            return status.RequestedBy;
        }
    }
}
