using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Authentication;
using System.Windows.Forms;
using BuildMonitor.Core;
using BuildMonitor.UI.Helpers;
using BuildMonitor.UI.Properties;
using BuildMonitor.UI.Protection;

namespace BuildMonitor.UI.Options
{
    internal partial class SettingsForm : Form
    {
        private readonly IBuildStoreFactory m_BuildStoreFactory;
        private readonly MonitorOptions m_Options;

        private bool m_SavedSettingsValidated;
        
        public IMonitorOptions Options
        {
            get { return m_Options; }
        }

        public SettingsForm(IMonitorOptions currentOptions, IBuildStoreFactory buildStoreFactory)
        {
            InitializeComponent();

            m_Options = new MonitorOptions(currentOptions);
            m_BuildStoreFactory = buildStoreFactory;

            // Windows Tab
            cbStartup.Checked = StartupSettingHelper.RunOnStartup;

            // General Tab
            cbIncludeRunning.Checked = currentOptions.IncludeRunningBuilds;

            txtInterval.Text = m_Options.IntervalSeconds.ToString(CultureInfo.InvariantCulture);

            // TFS Tab
            if (!string.IsNullOrEmpty(m_Options.TfsApiUrl)) // Show defaults when not set
                txtTfsApiUrl.Text = m_Options.TfsApiUrl;

            if (!string.IsNullOrEmpty(m_Options.ProjectName)) // Show defaults when not set
                cboTfsProjectName.SelectedItem = m_Options.ProjectName;

            rdoSpecify.Checked = m_Options.UseCredentials;
            rdoAuthIntegrated.Checked = !rdoSpecify.Checked;

            if (!string.IsNullOrWhiteSpace(m_Options.Username) &&
                !string.IsNullOrWhiteSpace(m_Options.Password))
            {
                txtUsername.Text = ProtectionMethods.Unprotect(m_Options.UsernameProtected);
                txtPassword.Text = ProtectionMethods.Unprotect(m_Options.PasswordProtected);
            }

            btnOk.Enabled = cboTfsProjectName.Enabled =
                m_Options.ValidOptions;
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
            options.IncludeRunningBuilds = cbIncludeRunning.Checked;

            options.IntervalSeconds = Convert.ToInt32(txtInterval.Text);

            // TFS Tab
            options.TfsApiUrl = txtTfsApiUrl.Text;
            options.ProjectName = (string)cboTfsProjectName.SelectedItem;

            options.UseCredentials = rdoSpecify.Checked;

            if (options.UseCredentials)
            {
                options.UsernameProtected = ProtectionMethods.Protect(txtUsername.Text);
                options.PasswordProtected = ProtectionMethods.Protect(txtPassword.Text);
            }

            options.ValidOptions = cboTfsProjectName.Enabled;
        }

        private void rdoSpecify_CheckedChanged(object sender, EventArgs e)
        {
            txtUsername.Enabled = txtPassword.Enabled = rdoSpecify.Checked;
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            ValidateTfsSettings();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab != tabTfs)
                return;

            if (m_SavedSettingsValidated)
                return;

            ValidateTfsSettings();

            m_SavedSettingsValidated = true;
        }

        private void ValidateTfsSettings()
        {
            var tempOptions = new MonitorOptions();
            RetrieveOptions(tempOptions);

            var store = m_BuildStoreFactory.GetBuildStore(tempOptions);

            try
            {
                var projects = store.GetProjects();
                cboTfsProjectName.Items.AddRange(projects.ToArray());
                imgBox.Image = Status.Succeeded.ToBitmap(new Size(24, 24));
            }
            catch (AuthenticationException)
            {
                cboTfsProjectName.Items.Clear();
                imgBox.Image = Status.Failed.ToBitmap(new Size(24, 24));
            }
            catch (Exception ex)
            {
                cboTfsProjectName.Items.Clear();
                imgBox.Image = Status.Failed.ToBitmap(new Size(24, 24));
                MessageBox.Show(ex.ToString());
            }

            if (cboTfsProjectName.Items.Count > 0)
            {
                cboTfsProjectName.SelectedItem = !string.IsNullOrWhiteSpace(m_Options.ProjectName) 
                    ? m_Options.ProjectName 
                    : cboTfsProjectName.Items[0];

                cboTfsProjectName.Enabled = true;
                btnOk.Enabled = true;
                return;
            }

            cboTfsProjectName.Enabled = false;
            btnOk.Enabled = false;
        }
    }
}
