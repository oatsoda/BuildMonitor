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

                bool checkAll = m_ViewModel.SelectedPipelineIds.Count == 0;

                var listViewItems = buildDefinitions
                    .OrderBy(b => b.Name)
                    .Select(b => new ListViewItem
                    {
                        Text = b.Name,
                        Tag = b.Id,
                        Checked = checkAll || m_ViewModel.SelectedPipelineIds.Contains(b.Id)
                    });

                lvPipelines.Items.Clear();
                lvPipelines.Items.AddRange([.. listViewItems]);
                lvPipelines.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Faied to fetch ADO pipelines",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                DialogResult = DialogResult.Abort;
                Close();
                return;
            }

            Enabled = true;
            ResumeLayout();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (DialogResult != DialogResult.OK)
                return;


            var selectedPipelines = lvPipelines.CheckedItems;

            if (selectedPipelines.Count == 0)
            {
                MessageBox.Show(
                  "You must select at least one pipeline.",
                  "No pipelines selected",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error
                );

                e.Cancel = true;
            }

            m_ViewModel.SelectedPipelineIds.Clear();
            if (selectedPipelines.Count < lvPipelines.Items.Count)
                m_ViewModel.SelectedPipelineIds.AddRange(selectedPipelines.OfType<ListViewItem>().Select(p => (int)p.Tag!));
        }

        private void lvPipelines_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            lvPipelines.SelectedItems.Clear(); // Disable selecting
        }
    }
}
