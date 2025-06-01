using BuildMonitor.Core;
using BuildMonitor.UI.Helpers;
using BuildMonitor.UI.Protection;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildMonitor.UI.Options
{
    internal partial class SettingsForm : Form
    {
        private readonly IBuildStoreFactory m_BuildStoreFactory;
        private readonly MonitorOptions m_Options;

        private bool m_SavedSettingsValidated;

        public IMonitorOptions Options => m_Options;

        public SettingsForm(IMonitorOptions currentOptions, IBuildStoreFactory buildStoreFactory)
        {
            InitializeComponent();
            Icon = Properties.Resources._0031_Tools;

            m_Options = new MonitorOptions(currentOptions);
            m_BuildStoreFactory = buildStoreFactory;

            // Windows Tab
            cbStartup.Checked = StartupSettingHelper.RunOnStartup;

#if DEBUG
            cbStartup.Enabled = false;
            cbStartup.Text += " [Option disabled in debug mode]";
#endif

            // General Tab
            txtInterval.Text = m_Options.IntervalSeconds.ToString(CultureInfo.InvariantCulture);

            cbIncludeRunning.Checked = m_Options.IncludeRunningBuilds;

            cbRefreshDefinitions.Checked = m_Options.RefreshDefintions;
            txtDefinitionInterval.Text = m_Options.RefreshDefinitionIntervalSeconds.ToString(CultureInfo.InvariantCulture);
            txtDefinitionInterval.Enabled = m_Options.RefreshDefintions;

            cbHideStale.Checked = m_Options.HideStaleDefinitions;
            txtStaleDays.Text = m_Options.StaleDefinitionDays.ToString(CultureInfo.InvariantCulture);
            txtStaleDays.Enabled = m_Options.HideStaleDefinitions;

            // ADO Tab
            txtAdoOrganisation.Text = m_Options.AzureDevOpsOrganisation;

            if (!string.IsNullOrWhiteSpace(m_Options.PersonalAccessTokenCipher))
            {
                txtAdoPat.Text = ProtectionMethods.Unprotect(m_Options.PersonalAccessTokenProtected);
            }

            cboAdoProjectName.SelectedItem = m_Options.ProjectName;

            btnOk.Enabled = cboAdoProjectName.Enabled = m_Options.ValidOptions;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (DialogResult == DialogResult.Cancel)
                return;

            // Windows Tab
            StartupSettingHelper.SetStartup(cbStartup.Checked);

            RetrieveOptions(m_Options);

            m_Options.Save();
        }

        private void RetrieveOptions(MonitorOptions options)
        {
            // General Tab
            options.IntervalSeconds = Convert.ToInt32(txtInterval.Text);

            options.IncludeRunningBuilds = cbIncludeRunning.Checked;

            options.RefreshDefintions = cbRefreshDefinitions.Checked;
            options.RefreshDefinitionIntervalSeconds = Convert.ToInt32(txtDefinitionInterval.Text);

            options.HideStaleDefinitions = cbHideStale.Checked;
            options.StaleDefinitionDays = Convert.ToInt32(txtStaleDays.Text);

            options.AzureDevOpsOrganisation = txtAdoOrganisation.Text;
            options.PersonalAccessTokenProtected = ProtectionMethods.Protect(txtAdoPat.Text);

            if (m_SavedSettingsValidated) // Tab never loaded, so don't overwrite this setting because it will not be loaded
                options.ProjectName = (string)cboAdoProjectName.SelectedItem;

            options.ValidOptions = cboAdoProjectName.Enabled;
        }

        private async void btnValidate_Click(object sender, EventArgs e)
        {
            await ValidateADOSettings();
        }

        private async void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab != tabTfs)
                return;

            if (m_SavedSettingsValidated)
                return;

            await ValidateADOSettings();
        }

        private async Task ValidateADOSettings()
        {
            tabTfs.Enabled = false;

            var tempOptions = new MonitorOptions();
            RetrieveOptions(tempOptions);

            if (string.IsNullOrWhiteSpace(tempOptions.AzureDevOpsOrganisation))
            {
                cboAdoProjectName.Enabled = false;
                btnOk.Enabled = false;
                tabTfs.Enabled = true;
                return;
            }


            try
            {
                var store = m_BuildStoreFactory.GetBuildStore(tempOptions);
                var projects = await store.GetProjects();
                cboAdoProjectName.Items.AddRange(projects.Cast<object>().ToArray());
                imgBox.Image = Status.Succeeded.ToBitmap(new Size(24, 24));
            }
            // filters handle Aggregation Exception
            catch (Exception ex) when (ex is AuthenticationException ||
                    ex.GetBaseException() is AuthenticationException)
            {
                cboAdoProjectName.Items.Clear();
                imgBox.Image = Status.Failed.ToBitmap(new Size(24, 24));
            }
            catch (Exception ex)
            {
                cboAdoProjectName.Items.Clear();
                imgBox.Image = Status.Failed.ToBitmap(new Size(24, 24));
                MessageBox.Show(ex.ToString());
            }

            if (cboAdoProjectName.Items.Count > 0)
            {
                m_SavedSettingsValidated = true;
                cboAdoProjectName.SelectedItem = !string.IsNullOrWhiteSpace(m_Options.ProjectName)
                    ? m_Options.ProjectName
                    : cboAdoProjectName.Items[0];

                cboAdoProjectName.Enabled = true;
                btnOk.Enabled = true;
                tabTfs.Enabled = true;
                return;
            }

            cboAdoProjectName.Enabled = false;
            btnOk.Enabled = false;
            tabTfs.Enabled = true;
        }

        private void cbRefreshDefinitions_CheckedChanged(object sender, EventArgs e)
        {
            txtDefinitionInterval.Enabled = cbRefreshDefinitions.Enabled;
        }

        private void cbHideStale_CheckedChanged(object sender, EventArgs e)
        {
            txtStaleDays.Enabled = cbHideStale.Checked;
        }

    }
}
