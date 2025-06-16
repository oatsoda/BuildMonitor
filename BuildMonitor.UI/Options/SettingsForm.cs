using BuildMonitor.Core;
using BuildMonitor.UI.Helpers;
using BuildMonitor.UI.Protection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildMonitor.UI.Options
{
    internal partial class SettingsForm : Form
    {
        // Deps
        private readonly IBuildStoreFactory m_BuildStoreFactory;

        // Model
        private readonly ViewModel m_ViewModel;

        // Result Property
        public IMonitorOptions Options => m_ViewModel.Options;

        // Types        
        private record ADOValidationModel(bool IsValid, IList<string>? ProjectNames);
        private class WindowsOptions
        {
            public bool RunOnStartup { get; set; }
        }

        private class ViewModel
        {
            public required MonitorOptions Options { get; init; }
            public required WindowsOptions Windows { get; init; }
            public ADOValidationModel? ADOValidationResult { get; set; }
        }

        public SettingsForm(IMonitorOptions currentOptions, IBuildStoreFactory buildStoreFactory)
        {
            InitializeComponent();
            Icon = Properties.Resources._0031_Tools;

            m_ViewModel = new ViewModel
            {
                Options = new MonitorOptions(currentOptions), // Make a copy of the options to avoid referential updating
                Windows = new WindowsOptions { RunOnStartup = StartupSettingHelper.RunOnStartup }
            };

            m_BuildStoreFactory = buildStoreFactory;

            UpdateViewFromModel(m_ViewModel);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (DialogResult != DialogResult.OK)
                return;

            // View-to-Model
            UpdateModelFromView(m_ViewModel);

            // Save Model
            StartupSettingHelper.SetStartup(cbStartup.Checked);
            m_ViewModel.Options.Save();
        }

        private void UpdateViewFromModel(ViewModel viewModel)
        {
            SuspendLayout();

            // Windows Tab
            cbStartup.Checked = viewModel.Windows.RunOnStartup;

#if DEBUG
            cbStartup.Enabled = false;
            cbStartup.Text += " [Option disabled in debug mode]";
#endif

            // General Tab
            numInterval.Value = viewModel.Options.IntervalSeconds;

            cbIncludeRunning.Checked = viewModel.Options.IncludeRunningBuilds;

            cbRefreshDefinitions.Checked = viewModel.Options.RefreshDefintions;
            numDefinitionInterval.Value = viewModel.Options.RefreshDefinitionIntervalSeconds;
            numDefinitionInterval.Enabled = viewModel.Options.RefreshDefintions;

            cbHideStale.Checked = viewModel.Options.HideStaleDefinitions;
            numStaleDays.Value = viewModel.Options.StaleDefinitionDays;
            numStaleDays.Enabled = viewModel.Options.HideStaleDefinitions;

            cbOrderByMostRecent.Checked = viewModel.Options.OrderByMostRecent;

            // ADO Tab
            txtAdoOrganisation.Text = viewModel.Options.AzureDevOpsOrganisation;

            if (!string.IsNullOrWhiteSpace(viewModel.Options.PersonalAccessTokenCipher))
            {
                txtAdoPat.Text = ProtectionMethods.Unprotect(viewModel.Options.PersonalAccessTokenProtected);
            }

            cboAdoProjectName.Items.Clear();

            if (viewModel.ADOValidationResult != null) // Values dependent on having validated are not updated until that time
            {
                if (viewModel.ADOValidationResult.IsValid)
                {
                    cboAdoProjectName.Enabled = viewModel.ADOValidationResult.ProjectNames!.Count > 0;
                    cboAdoProjectName.Items.AddRange([.. viewModel.ADOValidationResult.ProjectNames!.Cast<object>()]);
                    cboAdoProjectName.SelectedItem = viewModel.Options.ProjectName ?? viewModel.ADOValidationResult.ProjectNames!.FirstOrDefault();
                }
                else
                {
                    cboAdoProjectName.Enabled = false;
                    cboAdoProjectName.SelectedItem = null;
                }

                imgBox.Image = viewModel.ADOValidationResult.IsValid
                    ? Status.Succeeded.ToBitmap(new Size(24, 24))
                    : Status.Failed.ToBitmap(new Size(24, 24));

                txtPipelines.Text = viewModel.Options.SpecificDefinitionIds == null || viewModel.Options.SpecificDefinitionIds.Length == 0
                    ? "All Pipelines"
                    : $"{viewModel.Options.SpecificDefinitionIds.Length} Selected Pipeline(s)";
            }

            btnOk.Enabled =
                btnPipelines.Enabled = viewModel.Options.ValidOptions;

            ResumeLayout();
        }

        private void UpdateModelFromView(ViewModel viewModel)
        {
            // Windows Tab
            viewModel.Windows.RunOnStartup = cbStartup.Checked;

            // General Tab
            viewModel.Options.IntervalSeconds = (int)numInterval.Value;

            viewModel.Options.IncludeRunningBuilds = cbIncludeRunning.Checked;

            viewModel.Options.RefreshDefintions = cbRefreshDefinitions.Checked;
            viewModel.Options.RefreshDefinitionIntervalSeconds = (int)numDefinitionInterval.Value;

            viewModel.Options.HideStaleDefinitions = cbHideStale.Checked;
            viewModel.Options.StaleDefinitionDays = (int)numStaleDays.Value;

            viewModel.Options.OrderByMostRecent = cbOrderByMostRecent.Checked;

            // ADO Tab
            viewModel.Options.AzureDevOpsOrganisation = txtAdoOrganisation.Text;
            viewModel.Options.PersonalAccessTokenProtected = ProtectionMethods.Protect(txtAdoPat.Text);

            if (viewModel.ADOValidationResult != null) // Tab never loaded, so don't overwrite this setting because it will not be loaded
            {
                viewModel.Options.ProjectName = (string)cboAdoProjectName.SelectedItem!;
            }

            viewModel.Options.ValidOptions = cboAdoProjectName.Enabled;
        }

        private async Task RunADOValidation()
        {
            tabADO.Enabled = false;

            UpdateModelFromView(m_ViewModel);
            m_ViewModel.ADOValidationResult = await ValidateADOSettings();
            m_ViewModel.Options.ValidOptions = m_ViewModel.ADOValidationResult.IsValid; // TODO: Improve interplay between IsValid and ValidOptions
            UpdateViewFromModel(m_ViewModel);

            tabADO.Enabled = true;
        }

        private async Task<ADOValidationModel> ValidateADOSettings()
        {
            try
            {
                var store = m_BuildStoreFactory.GetBuildStore(m_ViewModel.Options, true);
                var projects = await store.GetProjects();
                return new ADOValidationModel(true, [.. projects]);
            }
            // filters handle Aggregation Exception
            catch (Exception ex) when (ex is AuthenticationException ||
                    ex.GetBaseException() is AuthenticationException)
            {
                return new ADOValidationModel(false, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), @"Failed to fetch ADO Projects", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new ADOValidationModel(false, null);
            }
        }

        #region Event Handlers

        // ** General Tab

        private void cbRefreshDefinitions_CheckedChanged(object sender, EventArgs e)
        {
            numDefinitionInterval.Enabled = cbRefreshDefinitions.Enabled;
        }

        private void cbHideStale_CheckedChanged(object sender, EventArgs e)
        {
            numStaleDays.Enabled = cbHideStale.Checked;
        }

        // ** ADO Tab

        private void txtAdoOrganisation_TextChanged(object sender, EventArgs e)
        {
            lblLinkPat.Enabled =
                btnValidate.Enabled = txtAdoOrganisation.Text.Length >= 0;

            var url = $"https://dev.azure.com/{txtAdoOrganisation.Text}/_usersSettings/tokens";
            lblLinkPat.SetUrl(url);
        }

        private void lblLinkPat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            e.VisitUrl(sender);
        }

        private async void btnValidate_Click(object sender, EventArgs e)
        {
            await RunADOValidation();
        }

        private async void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab != tabADO)
                return;

            if (m_ViewModel.ADOValidationResult != null)
                return;

            await RunADOValidation();
        }

        // ** Other Events

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (DialogResult.Cancel == MessageBox.Show(
                @"Are you sure you want to delete all settings? It cannot be undone.",
                @"Reset All Settings?",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
                ))
                return;

            m_ViewModel.Options.Reset();
            DialogResult = DialogResult.Abort; // Close without triggering OnClose saving of settings.
        }

        #endregion
    }
}
