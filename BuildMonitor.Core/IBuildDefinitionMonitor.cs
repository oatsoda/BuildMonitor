using System;
using System.Collections.Generic;

namespace BuildMonitor.Core
{
    public interface IBuildDefinitionMonitor
    {
        event EventHandler<BuildDetail> OverallStatusChanged;
        event EventHandler<Exception> ExceptionOccurred;
        event EventHandler<string> MonitoringStopped;
        event EventHandler<List<BuildDetail>> Updated;
        void Start(IMonitorOptions options);
        void Stop();
        void Dispose();
    }
}