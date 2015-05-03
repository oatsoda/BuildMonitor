using System;
using BuildMonitor.Core;

namespace BuildMonitor.Tfs
{
    public class BuildStatus : IBuildStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public Status Status { get; set; }
        public string RequestedBy { get; set; }
    }
}