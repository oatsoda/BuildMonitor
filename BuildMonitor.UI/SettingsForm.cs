using System;
using System.Globalization;
using System.Windows.Forms;
using BuildMonitor.Core;

namespace BuildMonitor.UI
{
    internal partial class SettingsForm : Form
    {
        public MonitorOptions Options { get; private set; }

        public SettingsForm(MonitorOptions currentOptions)
        {
            InitializeComponent();

            txtInterval.Text = currentOptions.Interval.ToString(CultureInfo.InvariantCulture);
            txtTfsApiUrl.Text = currentOptions.TfsApiUrl;
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
        }

        private void rdoSpecify_CheckedChanged(object sender, EventArgs e)
        {
            txtUsername.Enabled = txtPassword.Enabled = rdoSpecify.Checked;
        }

    }
}
