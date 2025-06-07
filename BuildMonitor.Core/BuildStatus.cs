using System;

namespace BuildMonitor.Core
{
    public class BuildStatus
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Finish { get; set; }
        public Status Status { get; set; }
        public required string RequestedBy { get; set; }
        public int ErrorCount { get; set; }
        public int WarningCount { get; set; }

        public TimeSpan TimeSpanSinceStart()
        {
            return DateTime.UtcNow.Subtract(Start);
        }

        public string ToCurrentTimeDescription()
        {
            var diff = TimeSpanSinceStart();

            if (diff.TotalHours >= 48)
                return $"{(int)diff.TotalDays} days ago";

            if (diff.TotalMinutes >= 120)
                return $"{(int)diff.TotalHours} hours ago";

            if (diff.TotalMinutes >= 1)
                return $"{(int)diff.TotalMinutes} minutes ago";

            if (diff.TotalSeconds > 1)
                return $"{(int)diff.TotalSeconds} seconds ago";

            return "Just now";
        }

        public string ToRequestedByDescription(int trimLen)
        {
            if (RequestedBy == null)
                return "-";

            return RequestedBy.Length > trimLen
                ? $"{RequestedBy.Substring(0, trimLen)}..."
                : RequestedBy;
        }
    }

    public static class BuildStatusExtensions
    {
        public static string ToCurrentTimeDescription(this BuildStatus status)
        {
            if (status == null)
                return "-";

            return status.ToCurrentTimeDescription();
        }

        public static string ToRequestedByDescription(this BuildStatus status, int trimLen)
        {
            if (status == null)
                return "-";

            return status.ToRequestedByDescription(trimLen);
        }
    }
}