using System;
using System.Globalization;
using System.Windows.Forms;
using BuildMonitor.Core;
using BuildMonitor.UI.Helpers;
using BuildMonitor.UI.Protection;

namespace BuildMonitor.UI.Options
{
    internal partial class SettingsForm : Form
    {
        private readonly MonitorOptions m_Options;

        public IMonitorOptions Options
        {
            get { return m_Options; }
        }

        public SettingsForm(IMonitorOptions currentOptions)
        {
            InitializeComponent();

            m_Options = new MonitorOptions(currentOptions);

            // Windows Tab
            cbStartup.Checked = StartupSettingHelper.RunOnStartup;

            // General Tab
            cbIncludeRunning.Checked = currentOptions.IncludeRunningBuilds;

            txtInterval.Text = m_Options.IntervalSeconds.ToString(CultureInfo.InvariantCulture);

            // TFS Tab
            if (!string.IsNullOrEmpty(m_Options.TfsApiUrl)) // Show defaults when not set
                txtTfsApiUrl.Text = m_Options.TfsApiUrl;

            if (!string.IsNullOrEmpty(m_Options.ProjectName)) // Show defaults when not set
                txtTfsProjectName.Text = m_Options.ProjectName;

            rdoSpecify.Checked = m_Options.UseCredentials;

            if (!string.IsNullOrWhiteSpace(m_Options.Username) &&
                !string.IsNullOrWhiteSpace(m_Options.Password))
            {
                txtUsername.Text = ProtectionMethods.Unprotect(m_Options.UsernameProtected);
                txtPassword.Text = ProtectionMethods.Unprotect(m_Options.PasswordProtected);
            }

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (DialogResult == DialogResult.Cancel)
                return;

            // Windows Tab
            StartupSettingHelper.SetStartup(cbStartup.Checked);

            // General Tab
            m_Options.IncludeRunningBuilds = cbIncludeRunning.Checked;

            m_Options.IntervalSeconds = Convert.ToInt32(txtInterval.Text);

            // TFS Tab
            m_Options.TfsApiUrl = txtTfsApiUrl.Text;
            m_Options.ProjectName = txtTfsProjectName.Text;

            m_Options.UseCredentials = rdoSpecify.Checked;

            if (m_Options.UseCredentials)
            {
                m_Options.UsernameProtected = ProtectionMethods.Protect(txtUsername.Text);
                m_Options.PasswordProtected = ProtectionMethods.Protect(txtPassword.Text);
            }
            
            m_Options.Save();
        }

        private void rdoSpecify_CheckedChanged(object sender, EventArgs e)
        {
            txtUsername.Enabled = txtPassword.Enabled = rdoSpecify.Checked;
        }

    }
}
