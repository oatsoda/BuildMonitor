using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace BuildMonitor.Core
{
    public sealed class BuildDefinitionMonitor : IDisposable
    {
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

        private Status m_OverallStatus;
        private List<BuildDefinition> m_MonitoredDefinitions = [];
        private Dictionary<int, BuildStatus> m_LatestStatuses = [];
        private DateTime m_LastDefinitionRefresh;

        public event EventHandler<BuildDetail>? OverallStatusChanged;
        public event EventHandler<Exception>? ExceptionOccurred;
        public event EventHandler<string>? MonitoringStopped;
        public event EventHandler<List<BuildDetail>>? Updated;

        private ReadOnlyDictionary<int, BuildStatus> LatestStatuses
        {
            get
            {
                if (!Options.HideStaleDefinitions) // Option is not enabled
                    return new ReadOnlyDictionary<int, BuildStatus>(m_LatestStatuses);

                return
                    new ReadOnlyDictionary<int, BuildStatus>(
                        m_LatestStatuses
                        .Where(kvp =>
                            kvp.Value.TimeSpanSinceStart().TotalDays < Options.StaleDefinitionDays // Status is within days
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
            Debug.WriteLine("Monitor starting...");

            Stop();

            Debug.WriteLine("Monitor ensuring any previous stops have completed...");
            // First time this is already signalled so will immediately pass.
            // Subsequent calls to Start will wait for it to stop first.
            m_StopWaitHandle.WaitOne();

            Debug.WriteLine("Monitor setting options...");

            m_Options = options;

            if (m_MonitoredDefinitions.Count > 0)
                m_MonitoredDefinitions = [];

            if (m_LatestStatuses.Count > 0)
                m_LatestStatuses = [];

            m_RequestStop = false;
            m_Stopped = false;

            if (!options.ValidOptions)
            {
                m_Stopped = true;
                OnMonitoringStopped("Settings are incomplete");
                return;
            }

            Debug.WriteLine("Monitor running...");

            Task.Run(Run);
        }

        public void Stop()
        {
            if (!m_Stopped)
                m_RequestStop = true;
        }

        private void Run()
        {
            Debug.WriteLine("Monitor Run...");

            m_StopWaitHandle.Reset();

            var restartAfterException = false;

            var sw = new Stopwatch();
            var intervalMilliseconds = Options.IntervalSeconds * 1000;

            Debug.WriteLine("Monitor getting store...");

            var buildStore = m_BuildStoreFactory.GetBuildStore(Options);

            try
            {
                while (!m_RequestStop)
                {
                    if (!m_RequestStop)
                        RefreshDefinitionsIfRequired(buildStore);

                    if (!m_RequestStop)
                        RefreshStatuses(buildStore).GetAwaiter().GetResult(); // Async ends here at the mo.

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
                    !Options.HideStaleDefinitions || // Option is disabled
                    LatestStatuses.ContainsKey(d.Id) // No status means either no status or stale status
                    )
                .Select(d => new BuildDetail(d, LatestStatuses.TryGetValue(d.Id, out var value) ? value : null))
                .OrderBy(d => d.Definition.Name)
                .ToList();
        }

        private void RefreshDefinitionsIfRequired(IBuildStore buildStore)
        {
            // Don't refresh defns if: a) already loaded AND 
            // ( b) option to refresh is off OR c) option is on but interval not exceeded

            if (m_MonitoredDefinitions.Count > 0 &&
                (!Options.RefreshDefintions || DateTime.UtcNow.Subtract(m_LastDefinitionRefresh).Seconds < Options.RefreshDefinitionIntervalSeconds))
                return;

            RefreshDefinitions(buildStore).Wait(); // Async ends here at the mo.
        }

        private async Task RefreshDefinitions(IBuildStore buildStore)
        {
            var definitions = await buildStore.GetDefinitions(Options.ProjectName);
            m_MonitoredDefinitions = [.. definitions];
            m_LastDefinitionRefresh = DateTime.UtcNow;
        }

        private async Task RefreshStatuses(IBuildStore buildStore)
        {
            foreach (var definition in m_MonitoredDefinitions)
            {
                var status = await buildStore.GetLatestBuild(Options.ProjectName, definition);

                if (status == null)
                    continue;

                m_LatestStatuses[definition.Id] = status;
            }

            var worstList = LatestStatuses
                .OrderByDescending(s => s.Value.Status)
                .ThenByDescending(s => s.Value.Finish)
                .ToList();

            if (!worstList.Any())
                return;

            var worst = worstList.First();

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
