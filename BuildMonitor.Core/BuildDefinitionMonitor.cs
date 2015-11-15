using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using BuildMonitor.Core.InterfaceExtensions;

namespace BuildMonitor.Core
{
    public sealed class BuildDefinitionMonitor : IBuildDefinitionMonitor, IDisposable
    {
        private bool m_RequestStop;
        private bool m_Stopped;
        private readonly ManualResetEvent m_StopWaitHandle;

        private readonly IBuildStoreFactory m_BuildStoreFactory;
        private IMonitorOptions m_Options;
        
        private Status m_OverallStatus;
        private List<IBuildDefinition> m_MonitoredDefinitions;
        private Dictionary<int, IBuildStatus> m_LatestStatuses;
        private DateTime m_LastDefinitionRefresh;

        public event EventHandler<BuildDetail> OverallStatusChanged;
        public event EventHandler<Exception> ExceptionOccurred;
        public event EventHandler<string> MonitoringStopped;
        public event EventHandler<List<BuildDetail>> Updated;

        private ReadOnlyDictionary<int, IBuildStatus> LatestStatuses
        {
            get
            {
                if (!m_Options.HideStaleDefinitions) // Option is not enabled
                    return new ReadOnlyDictionary<int, IBuildStatus>(m_LatestStatuses);

                return
                    new ReadOnlyDictionary<int, IBuildStatus>(
                        m_LatestStatuses
                        .Where(kvp =>
                            kvp.Value.TimeSpanSinceStart().TotalDays < m_Options.StaleDefinitionDays // Status is within days
                        )
                        .ToDictionary(pair => pair.Key, pair => pair.Value)
                    );
            }
        }

        public BuildDefinitionMonitor(IBuildStoreFactory buildStoreFactory)
        {
            m_BuildStoreFactory = buildStoreFactory;

            m_Stopped = true;
            m_StopWaitHandle = new ManualResetEvent(true); // Initial value is Signalled.
        }

        public void Start(IMonitorOptions options)
        {
            Stop();
            // First time this is already signalled so will immediately pass.
            // Subsequent calls to Start will wait for it to stop first.
            m_StopWaitHandle.WaitOne();
            
            m_Options = options;

            m_MonitoredDefinitions = null;
            m_LatestStatuses = new Dictionary<int, IBuildStatus>();
            m_RequestStop = false;
            m_Stopped = false;

            if (!options.ValidOptions)
            {
                m_Stopped = true;
                OnMonitoringStopped("Settings are incomplete");
                return;
            }

            Task.Factory.StartNew(Run);
        }

        public void Stop()
        {
            if (!m_Stopped)
                m_RequestStop = true;
        }
        
        private void Run()
        {
            m_StopWaitHandle.Reset();

            var restartAfterException = false;

            var sw = new Stopwatch();
            var intervalMilliseconds = m_Options.IntervalSeconds*1000;

            try
            {
                while (!m_RequestStop)
                {
                    var buildStore = m_BuildStoreFactory.GetBuildStore(m_Options);

                    if (!m_RequestStop)
                        RefreshDefinitionsIfRequired(buildStore);

                    if (!m_RequestStop)
                        RefreshStatuses(buildStore).Wait(); // Async ends here at the mo.

                    if (!m_RequestStop)
                        RaiseUpdated();

                    if (!m_RequestStop)
                    {
                        sw.Start();

                        while (sw.ElapsedMilliseconds < intervalMilliseconds)
                        {
                            Thread.Sleep(1000);

                            if (m_RequestStop)
                                break;
                        }

                        sw.Reset();
                    }
                }
            }
            catch (AuthenticationException)
            {
                OnMonitoringStopped("Authentication failed.");
            }
            catch (Exception ex)
            {
                ExceptionOccurred?.Invoke(this, ex);

                if (!m_RequestStop && !m_Stopped)
                    restartAfterException = true;
            }
            finally
            {
                if (restartAfterException)
                {
                    Thread.Sleep(10000); // Wait for 10 secs before starting again
                    Run();
                }
                else
                {
                    m_RequestStop = false;
                    m_Stopped = true;
                    m_StopWaitHandle.Set();
                }
            }
        }

        private void RaiseUpdated()
        {
            Updated?.Invoke(this, GetBuildDetails());
        }


        private List<BuildDetail> GetBuildDetails()
        {
            return m_MonitoredDefinitions
                .Where(d => 
                    !m_Options.HideStaleDefinitions || // Option is disabled
                    LatestStatuses.ContainsKey(d.Id) // No status means either no status or stale status
                    )
                .Select(d => new BuildDetail(d, LatestStatuses.ContainsKey(d.Id) ? LatestStatuses[d.Id] : null))
                .OrderBy(d => d.Definition.Name)
                .ToList();
        }

        private void RefreshDefinitionsIfRequired(IBuildStore buildStore)
        {
            // Don't refresh defns if: a) already loaded AND 
            // ( b) option to refresh is off OR c) option is on but interval not exceeded

            if (m_MonitoredDefinitions != null &&
                (!m_Options.RefreshDefintions || DateTime.UtcNow.Subtract(m_LastDefinitionRefresh).Seconds < m_Options.RefreshDefinitionIntervalSeconds))
                return;

            RefreshDefinitions(buildStore).Wait(); // Async ends here at the mo.
        }

        private async Task RefreshDefinitions(IBuildStore buildStore)
        {
            var definitions = await buildStore.GetDefinitions(m_Options.ProjectName);
            m_MonitoredDefinitions = definitions.ToList();
            m_LastDefinitionRefresh = DateTime.UtcNow;
        }

        private async Task RefreshStatuses(IBuildStore buildStore)
        {
            foreach (var definition in m_MonitoredDefinitions)
            {
                var status = await buildStore.GetLatestBuild(m_Options.ProjectName, definition);

                if (status == null)
                    continue;

                m_LatestStatuses[definition.Id] = status;
            }

            var worst = LatestStatuses
                .OrderByDescending(s => s.Value.Status)
                .ThenByDescending(s => s.Value.Finish)
                .First();

            var worstDefn = m_MonitoredDefinitions.Single(d => d.Id == worst.Key);
            var worstStatus = worst.Value;

            UpdateOverallStatus(
                new BuildDetail(worstDefn, worstStatus)
                );
        }

        private void UpdateOverallStatus(BuildDetail updatedStatus)
        {
            var currentWorstStatus = LatestStatuses.Any()
                                            ? LatestStatuses.Values.Max(s => s.Status)
                                            : Status.Unknown;

            if (currentWorstStatus != m_OverallStatus)
                OnOverallStatusChanged(updatedStatus);

            m_OverallStatus = currentWorstStatus;
        }

        private void OnOverallStatusChanged(BuildDetail e)
        {
            var handler = OverallStatusChanged;
            handler?.Invoke(this, e);
        }

        private void OnMonitoringStopped(string e)
        {
            var handler = MonitoringStopped;
            handler?.Invoke(this, e);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
