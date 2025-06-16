using BuildMonitor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BuildMonitor.UI.Options
{
    public partial class PipelineSelectorForm : Form
    {
        // Deps
        private readonly IMonitorOptions m_MonitorOptions;
        private readonly IBuildStoreFactory m_BuildStoreFactory;

        // Model
        private readonly ViewModel m_ViewModel;

        // Result Property
        public IList<int>? SelectedPipelineIds => m_ViewModel.SelectedPipelineIds.Count == 0
            ? null
            : m_ViewModel.SelectedPipelineIds;

        // Types
        private record ViewModel(List<int> SelectedPipelineIds);

        public PipelineSelectorForm(IMonitorOptions monitorOptions, IBuildStoreFactory buildStoreFactory)
        {
            InitializeComponent();

            m_ViewModel = new ViewModel(
                monitorOptions.SpecificDefinitionIds == null
                ? []
                : [.. monitorOptions.SpecificDefinitionIds]
            );
            m_MonitorOptions = monitorOptions;
            m_BuildStoreFactory = buildStoreFactory;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Enabled = false;
            SuspendLayout();

            var buildStore = m_BuildStoreFactory.GetBuildStore(m_MonitorOptions, false);
            try
            {
                var buildDefinitions = await buildStore.GetDefinitions(null, []);
                lbPipelines.Items.Clear();
                lbPipelines.Items.AddRange([.. buildDefinitions.Cast<object>()]);
                var selectedIndexes = m_ViewModel.SelectedPipelineIds.Count == 0
                    ? buildDefinitions.Select((b, i) => i)
                    : buildDefinitions.Select((b, i) => (b.Id, i))
                        .Where(t => m_ViewModel.SelectedPipelineIds.Contains(t.Id))
                        .Select(t => t.i);
                lbPipelines.SelectedIndices.Clear();
                foreach (var selectedIndex in selectedIndexes)
                {
                    lbPipelines.SelectedIndices.Add(selectedIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Faied to fetch ADO pipelines",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                Close();
                return;
            }

            Enabled = true;
            ResumeLayout();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            m_ViewModel.SelectedPipelineIds.Clear();

            var selectedPipelines = lbPipelines.SelectedItems.Cast<BuildDefinition>().ToList();
            if (selectedPipelines.Count < lbPipelines.Items.Count)
                m_ViewModel.SelectedPipelineIds.AddRange(selectedPipelines.Select(p => p.Id));
        }
    }
}
