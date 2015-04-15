using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BuildMonitor.Core
{
    public sealed class BuildDefinitionMonitor : IDisposable
    {
        private readonly IBuildStore m_BuildStore;
        private readonly IMonitorOptions m_Options;
        private readonly bool m_ThrowExceptions;

        private bool m_Running;
        
        private Status m_OverallStatus;
        private List<IBuildDefinition> m_MonitoredDefinitions;
        private Dictionary<int, IBuildStatus> m_LatestStatuses;
        private DateTime m_LastDefinitionRefresh;

        public event EventHandler<BuildDetail> OverallStatusChanged;
        public event EventHandler<Exception> ExceptionOccurred;
        public event EventHandler<List<BuildDetail>> Updated;

        public BuildDefinitionMonitor(IBuildStore buildStore, IMonitorOptions options, bool throwExceptions = false)
        {
            m_BuildStore = buildStore;
            m_Options = options;
            m_ThrowExceptions = throwExceptions;
        }

        public void Start()
        {
            if (m_Running)
                throw new InvalidOperationException("Cannot call Start when already running.");

            m_LatestStatuses = new Dictionary<int, IBuildStatus>();
            m_Running = true;
            Task.Factory.StartNew(Run);
        }

        public void Stop()
        {
            m_Running = false;
        }
        
        private void Run()
        {
            try
            {
                while (m_Running)
                {
                    if (m_Running)
                        RefreshDefinitionsIfRequired();

                    if (m_Running)
                        RefreshStatuses();

                    if (m_Running)
                        RaiseUpdated();

                    if (m_Running)
                        Thread.Sleep(m_Options.Interval);
                }
            }
            catch (Exception ex)
            {
                if (m_ThrowExceptions)
                    throw;

                if (ExceptionOccurred != null)
                    ExceptionOccurred(this, ex);
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

        private void RefreshDefinitionsIfRequired()
        {
            if (m_MonitoredDefinitions == null)
                RefreshDefinitions();

            if (!m_Options.RefreshDefintions)
                return;

            if (DateTime.UtcNow.Subtract(m_LastDefinitionRefresh).Seconds > m_Options.RefreshDefinitionInterval)
                RefreshDefinitions();
        }

        private void RefreshDefinitions()
        {
            m_MonitoredDefinitions = m_BuildStore.GetDefinitions(m_Options.ProjectName).ToList();
            m_LastDefinitionRefresh = DateTime.UtcNow;
        }

        private void RefreshStatuses()
        {
            BuildDetail worstNew = null;

            foreach (var definition in m_MonitoredDefinitions)
            {
                var status = m_BuildStore.GetLatestBuild(definition);

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
                !(currentWorstStatus == Status.Unknown && m_OverallStatus == Status.Succeeded)) // Ignore succeded on first load
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
