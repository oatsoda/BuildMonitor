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

            var diff = status.TimeSpanSinceStart();

            if (diff.TotalHours >= 48)
                return $"{(int) diff.TotalDays} days ago";

            if (diff.TotalMinutes >= 120)
                return $"{(int) diff.TotalHours} hours ago";

            if (diff.TotalMinutes >= 1)
                return $"{(int) diff.TotalMinutes} minutes ago";

            if (diff.TotalSeconds > 1)
                return $"{(int) diff.TotalSeconds} seconds ago";

            return "Just now";
        }

        public static string ToRequestedByDescription(this IBuildStatus status, int trimLen)
        {
            if (status?.RequestedBy == null)
                return "-";

            return status.RequestedBy.Length > trimLen 
                ? $"{status.RequestedBy.Substring(0, trimLen)}..." 
                : status.RequestedBy;
        }

        public static TimeSpan TimeSpanSinceStart(this IBuildStatus status)
        {
            return DateTime.UtcNow.Subtract(status.Start);
        }
    }
}
