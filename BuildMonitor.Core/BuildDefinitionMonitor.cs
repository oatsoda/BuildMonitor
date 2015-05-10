using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BuildMonitor.Core
{
    public sealed class BuildDefinitionMonitor : IBuildDefinitionMonitor, IDisposable
    {
        private readonly bool m_ThrowExceptions;
        private bool m_FirstRun;
        
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
        public event EventHandler<List<BuildDetail>> Updated;

        public BuildDefinitionMonitor(IBuildStoreFactory buildStoreFactory, bool throwExceptions = false)
        {
            m_FirstRun = true;

            m_BuildStoreFactory = buildStoreFactory;
            m_ThrowExceptions = throwExceptions;

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

            m_LatestStatuses = new Dictionary<int, IBuildStatus>();
            m_RequestStop = false;
            m_Stopped = true;
            Task.Factory.StartNew(Run);
        }

        public void Stop()
        {
            if (m_Stopped)
                m_RequestStop = true;
        }
        
        private void Run()
        {
            m_StopWaitHandle.Reset();

            try
            {
                while (!m_RequestStop)
                {
                    var buildStore = m_BuildStoreFactory.GetBuildStore(m_Options);

                    if (!m_RequestStop)
                        RefreshDefinitionsIfRequired(buildStore);

                    if (!m_RequestStop)
                        RefreshStatuses(buildStore);

                    if (!m_RequestStop)
                        RaiseUpdated();

                    m_FirstRun = false;

                    if (!m_RequestStop)
                        Thread.Sleep(m_Options.IntervalSeconds * 1000);
                }
            }
            catch (Exception ex)
            {
                if (m_ThrowExceptions)
                    throw;

                if (ExceptionOccurred != null)
                    ExceptionOccurred(this, ex);
            }
            finally
            {
                m_RequestStop = false;
                m_Stopped = true;
                m_StopWaitHandle.Set();
            }
        }

        private void RaiseUpdated()
        {
            if (Updated == null)
                return;

            Updated(this, GetBuildDetails());
        }


        private List<BuildDetail> GetBuildDetails()
        {
            return m_MonitoredDefinitions
                .Select(d =>
                    new BuildDetail(d, m_LatestStatuses.ContainsKey(d.Id) ? m_LatestStatuses[d.Id] : null)
                )
                .OrderBy(d => d.Definition.Name)
                .ToList();
        }

        private void RefreshDefinitionsIfRequired(IBuildStore buildStore)
        {
            if (m_MonitoredDefinitions == null)
                RefreshDefinitions(buildStore);

            if (!m_Options.RefreshDefintions)
                return;

            if (DateTime.UtcNow.Subtract(m_LastDefinitionRefresh).Seconds > m_Options.RefreshDefinitionIntervalSeconds)
                RefreshDefinitions(buildStore);
        }

        private void RefreshDefinitions(IBuildStore buildStore)
        {
            m_MonitoredDefinitions = buildStore.GetDefinitions(m_Options.ProjectName).ToList();
            m_LastDefinitionRefresh = DateTime.UtcNow;
        }

        private void RefreshStatuses(IBuildStore buildStore)
        {
            BuildDetail worstNew = null;

            foreach (var definition in m_MonitoredDefinitions)
            {
                var status = buildStore.GetLatestBuild(definition);

                if (status == null)
                    continue;

                if (worstNew == null || status.Status > worstNew.Status.Status)
                    worstNew = new BuildDetail(definition, status);

                m_LatestStatuses[definition.Id] = status;
            }

            UpdateOverallStatus(worstNew);
        }

        private void UpdateOverallStatus(BuildDetail updatedStatus)
        {
            var currentWorstStatus = m_LatestStatuses.Any()
                                            ? m_LatestStatuses.Values.Max(s => s.Status)
                                            : Status.Unknown;

            if (currentWorstStatus != m_OverallStatus && 
                !(m_FirstRun && m_OverallStatus == Status.Succeeded)) // Ignore succeded on first load
                OnOverallStatusChanged(updatedStatus);

            m_OverallStatus = currentWorstStatus;
        }

        private void OnOverallStatusChanged(BuildDetail e)
        {
            var handler = OverallStatusChanged;
            if (handler != null)
                handler(this, e);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
