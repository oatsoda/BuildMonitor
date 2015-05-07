namespace BuildMonitor.Core
{
    public enum Status
    {
        Unknown = 0,

        InProgress = 1,
        Succeeded,
        PartiallySucceeded,
        Failed
    }
}