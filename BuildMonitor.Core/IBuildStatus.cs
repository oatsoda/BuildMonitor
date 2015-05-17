using System;

namespace BuildMonitor.Core
{
    public interface IBuildStatus
    {
        int Id { get; }
        string Name { get; }
        string Url { get; }
        DateTime Start { get; }
        DateTime? Finish { get; }
        Status Status { get; }
        string RequestedBy { get; }
    }
}