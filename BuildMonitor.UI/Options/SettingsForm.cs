using System;
using System.Globalization;
using System.Windows.Forms;
using BuildMonitor.UI.Helpers;
using BuildMonitor.UI.Protection;

namespace BuildMonitor.UI.Options
{
    internal partial class SettingsForm : Form
    {
        public MonitorOptions Options { get; private set; }

        public SettingsForm(MonitorOptions currentOptions)
        {
            InitializeComponent();

            // General Tab
            cbStartup.Checked = StartupSettingHelper.RunOnStartup;
            txtInterval.Text = currentOptions.Interval.ToString(CultureInfo.InvariantCulture);

            // TFS Tab
            if (!string.IsNullOrEmpty(currentOptions.TfsApiUrl)) // Show defaults when not set
                txtTfsApiUrl.Text = currentOptions.TfsApiUrl;

            if (!string.IsNullOrEmpty(currentOptions.ProjectName)) // Show defaults when not set
                txtTfsProjectName.Text = currentOptions.ProjectName;

            rdoSpecify.Checked = currentOptions.UseCredentials;

            if (!string.IsNullOrWhiteSpace(currentOptions.Username) &&
                !string.IsNullOrWhiteSpace(currentOptions.Password))
            {
                txtUsername.Text = ProtectionMethods.Unprotect(currentOptions.UsernameProtected);
                txtPassword.Text = ProtectionMethods.Unprotect(currentOptions.PasswordProtected);
            }

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (DialogResult == DialogResult.Cancel)
                return;

            Options = new MonitorOptions
            {
                Interval = Convert.ToInt32(txtInterval.Text),

                TfsApiUrl = txtTfsApiUrl.Text,
                ProjectName = txtTfsProjectName.Text,

                UseCredentials = rdoSpecify.Checked
            };

            if (Options.UseCredentials)
            {
                Options.UsernameProtected = ProtectionMethods.Protect(txtUsername.Text);
                Options.PasswordProtected = ProtectionMethods.Protect(txtPassword.Text);
            }

            Options.Save();

            StartupSettingHelper.SetStartup(cbStartup.Checked);
        }

        private void rdoSpecify_CheckedChanged(object sender, EventArgs e)
        {
            txtUsername.Enabled = txtPassword.Enabled = rdoSpecify.Checked;
        }

    }
}
