using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace BuildMonitor.Core
{
    public sealed class BuildDefinitionMonitor : IDisposable
    {
        private const int _NOTIFY_AFTER_CONSECUTIVE_EXCEPTIONS = 3;

        private bool m_RequestStop;
        private bool m_Stopped;
        private readonly ManualResetEvent m_StopWaitHandle;

        private readonly IBuildStoreFactory m_BuildStoreFactory;
        private IMonitorOptions? m_Options;
        private IMonitorOptions Options
        {
            get
            {
                if (m_Options == null)
                    throw new InvalidOperationException("Monitor options have not been set.");
                return m_Options;
            }
        }

        // ** Option Extras
        private TimeSpan m_RefreshDefinitionInterval;
        private TimeSpan m_RefreshBuildInterval;

        // ** State
        private DateTimeOffset m_LastDefinitionRefresh;
        private DateTimeOffset m_LastBuildRefresh;
        private Status m_LastWorstStatus;
        private int m_ConsecutiveExceptions;

        private List<BuildDefinition> m_MonitoredDefinitions = [];
        private Dictionary<int, BuildStatus> m_LatestStatuses = [];

        // * Events
        public event EventHandler<List<BuildDetail>>? Updated;
        public event EventHandler<BuildDetail>? OverallStatusChanged;
        public event EventHandler<Exception>? ExceptionOccurred;
        public event EventHandler<string>? MonitoringStopped;

        public BuildDefinitionMonitor(IBuildStoreFactory buildStoreFactory)
        {
            m_BuildStoreFactory = buildStoreFactory;

            m_Stopped = true;
            m_StopWaitHandle = new ManualResetEvent(true); // Initial value is Signalled.
        }

        public async Task Start(IMonitorOptions options)
        {
            Debug.WriteLine("Monitor starting...");

            Stop();

            Debug.WriteLine("Monitor ensuring any previous stops have completed...");
            // First time this is already signalled so will immediately pass.
            // Subsequent calls to Start will wait for it to stop first.
            m_StopWaitHandle.WaitOne();

            Debug.WriteLine("Monitor setting options...");

            m_Options = options;
            m_RefreshDefinitionInterval = TimeSpan.FromSeconds(Options.RefreshDefinitionIntervalSeconds);
            m_RefreshBuildInterval = TimeSpan.FromSeconds(Options.IntervalSeconds);

            m_LastWorstStatus = Status.Unknown;

            if (m_MonitoredDefinitions.Count > 0)
                m_MonitoredDefinitions = [];

            if (m_LatestStatuses.Count > 0)
                m_LatestStatuses = [];

            m_LastDefinitionRefresh = DateTimeOffset.MinValue;
            m_LastBuildRefresh = DateTimeOffset.MinValue;
            m_ConsecutiveExceptions = 0;

            m_RequestStop = false;
            m_Stopped = false;

            if (!options.ValidOptions)
            {
                m_Stopped = true;
                OnMonitoringStopped("Settings are incomplete");
                return;
            }

            Debug.WriteLine("Monitor starting...");

            await Run();
        }

        public void Stop()
        {
            if (!m_Stopped)
                m_RequestStop = true;
        }

        private async Task Run()
        {
            bool restartAfterException;

            do
            {
                restartAfterException = false;

                Debug.WriteLine("Monitor Run...");

                m_StopWaitHandle.Reset();

                Debug.WriteLine("Monitor getting store...");

                var buildStore = m_BuildStoreFactory.GetBuildStore(Options, false);

                try
                {
                    while (!m_RequestStop)
                    {
                        Debug.WriteLine("Monitor checking now...");

                        var requireDefinitionRefresh = RequiresDefinitionRefresh();
                        var requireBuildRefresh = requireDefinitionRefresh || RequiresBuildRefresh();

                        if (!m_RequestStop && requireDefinitionRefresh)
                            await RefreshDefinitions(buildStore);

                        if (!m_RequestStop && requireBuildRefresh)
                            await RefreshStatuses(buildStore);

                        if (!m_RequestStop && (requireDefinitionRefresh || requireBuildRefresh))
                            RaiseEvents();

                        if (!m_RequestStop)
                            await Task.Delay(1000);

                        m_ConsecutiveExceptions = 0;
                    }
                }
                catch (AuthenticationException)
                {
                    OnMonitoringStopped("Authentication failed.");
                }
                catch (Exception ex)
                {
                    m_ConsecutiveExceptions++;

                    if (!m_RequestStop)
                        restartAfterException = true;

                    if (m_ConsecutiveExceptions >= _NOTIFY_AFTER_CONSECUTIVE_EXCEPTIONS)
                    {
                        ExceptionOccurred?.Invoke(this, ex);
                        m_ConsecutiveExceptions = 0;
                    }
                }

                if (restartAfterException)
                {
                    await Task.Delay(10000);
                }
                else
                {
                    m_RequestStop = false;
                    m_Stopped = true;
                    m_StopWaitHandle.Set();
                    return;
                }

            } while (restartAfterException);
        }

        private bool RequiresDefinitionRefresh()
        {
            // Require if a) None loaded OR b) option to refresh is on AND interval exceeded
            if (m_MonitoredDefinitions.Count == 0)
                return true;

            if (!Options.RefreshDefintions)
                return false;

            return DateTimeOffset.UtcNow - m_LastDefinitionRefresh >= m_RefreshDefinitionInterval;
        }

        private bool RequiresBuildRefresh()
        {
            return DateTimeOffset.UtcNow - m_LastBuildRefresh >= m_RefreshBuildInterval;
        }

        private async Task RefreshDefinitions(IBuildStore buildStore)
        {
            Debug.WriteLine("Monitor refreshing definitions...");

            var definitionsBuiltSince = !Options.HideStaleDefinitions
                ? (DateTimeOffset?)null
                : DateTimeOffset.UtcNow.AddDays(-Options.StaleDefinitionDays);

            var definitions = await buildStore.GetDefinitions(definitionsBuiltSince);
            m_MonitoredDefinitions = [.. definitions];
            m_LastDefinitionRefresh = DateTimeOffset.UtcNow;
        }

        private async Task RefreshStatuses(IBuildStore buildStore)
        {
            Debug.WriteLine("Monitor refreshing statuses...");

            foreach (var definition in m_MonitoredDefinitions)
            {
                var status = await buildStore.GetLatestBuild(definition);

                if (status == null)
                    continue;

                m_LatestStatuses[definition.Id] = status;
            }

            m_LastBuildRefresh = DateTimeOffset.UtcNow;
        }

        private void RaiseEvents()
        {
            Debug.WriteLine("Monitor raising updated event...");

            var buildDetails = m_MonitoredDefinitions
               // TODO: If HideStaleDefinitions is enabled, filter out stale definitions if we're not refreshing them?
               .Select(d => new BuildDetail(d, m_LatestStatuses.TryGetValue(d.Id, out var value) ? value : null))
               .ToList();
            OnUpdated(buildDetails);

            // Raise OverallStatusChanged with the worst status, if changed
            var worstStatus = m_LatestStatuses
                .OrderByDescending(s => s.Value.Status)
                .ThenByDescending(s => s.Value.Finish)
                .FirstOrDefault();

            if (worstStatus.Key == 0)
                return;

            // TODO: Does it make sense to only raise Worst status. If you want to monitor all builds then overlapping InProgress
            // won't all trigger individually. Maybe this should just be the most recent?
            if (worstStatus.Value.Status == m_LastWorstStatus)
                return;

            var worstDefn = m_MonitoredDefinitions.Single(d => d.Id == worstStatus.Key);

            m_LastWorstStatus = worstStatus.Value.Status;

            OnOverallStatusChanged(
                new BuildDetail(worstDefn, worstStatus.Value)
            );
        }

        private void OnUpdated(List<BuildDetail> e)
        {
            Updated?.Invoke(this, e);
        }

        private void OnOverallStatusChanged(BuildDetail e)
        {
            OverallStatusChanged?.Invoke(this, e);
        }

        private void OnMonitoringStopped(string e)
        {
            MonitoringStopped?.Invoke(this, e);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
