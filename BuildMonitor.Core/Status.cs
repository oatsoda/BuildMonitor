namespace BuildMonitor.Core
{
    public enum Status
    {
        Unknown = 0,

        // Order is important here as a higher value is considered a "Worse" status
        // so InProgress is made "Worst" so that it always gets reported (When that
        // setting is enabled)
        Succeeded = 1,
        PartiallySucceeded,
        Failed,

        InProgress,
    }
}