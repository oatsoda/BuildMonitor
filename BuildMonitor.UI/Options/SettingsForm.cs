﻿using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuildMonitor.Core;
using BuildMonitor.UI.Helpers;
using BuildMonitor.UI.Protection;

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

            m_Options = new MonitorOptions(currentOptions);
            m_BuildStoreFactory = buildStoreFactory;

            // Windows Tab
            cbStartup.Checked = StartupSettingHelper.RunOnStartup;

            // General Tab
            txtInterval.Text = m_Options.IntervalSeconds.ToString(CultureInfo.InvariantCulture);

            cbIncludeRunning.Checked = m_Options.IncludeRunningBuilds;

            cbRefreshDefinitions.Checked = m_Options.RefreshDefintions;
            txtDefinitionInterval.Text = m_Options.RefreshDefinitionIntervalSeconds.ToString(CultureInfo.InvariantCulture);
            txtDefinitionInterval.Enabled = m_Options.RefreshDefintions;

            cbHideStale.Checked = m_Options.HideStaleDefinitions;
            txtStaleDays.Text = m_Options.StaleDefinitionDays.ToString(CultureInfo.InvariantCulture);
            txtStaleDays.Enabled = m_Options.HideStaleDefinitions;

            // VSO Tab
            // backwards compatibility
            if (m_Options.TfsApiUrl != null &&
                m_Options.TfsApiUrl.StartsWith("https://") &&
                m_Options.TfsApiUrl.Contains("visualstudio.com"))
            {
                txtTfsApiUrl.Text = m_Options.TfsApiUrl.Substring(
                    "https://".Length,
                    (m_Options.TfsApiUrl.IndexOf("visualstudio.com", StringComparison.InvariantCultureIgnoreCase) - "https://".Length) - 1
                    );
            }
            else
            {
                txtTfsApiUrl.Text = m_Options.TfsApiUrl;
            }

            cboTfsProjectName.SelectedItem = m_Options.ProjectName;
            
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
            options.IntervalSeconds = Convert.ToInt32(txtInterval.Text);

            options.IncludeRunningBuilds = cbIncludeRunning.Checked;

            options.RefreshDefintions = cbRefreshDefinitions.Checked;
            options.RefreshDefinitionIntervalSeconds = Convert.ToInt32(txtDefinitionInterval.Text);

            options.HideStaleDefinitions = cbHideStale.Checked;
            options.StaleDefinitionDays = Convert.ToInt32(txtStaleDays.Text);

            options.TfsApiUrl = txtTfsApiUrl.Text;

            if (m_SavedSettingsValidated) // Tab never loaded, so don't overwrite this setting because it will not be loaded
                options.ProjectName = (string) cboTfsProjectName.SelectedItem;

            options.UseCredentials = true;

            if (options.UseCredentials)
            {
                options.UsernameProtected = ProtectionMethods.Protect(txtUsername.Text);
                options.PasswordProtected = ProtectionMethods.Protect(txtPassword.Text);
            }

            options.ValidOptions = cboTfsProjectName.Enabled;
        }
        
        private void btnValidate_Click(object sender, EventArgs e)
        {
#pragma warning disable 4014
            ValidateTfsSettings();
#pragma warning restore 4014
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab != tabTfs)
                return;

            if (m_SavedSettingsValidated)
                return;

#pragma warning disable 4014
            ValidateTfsSettings();
#pragma warning restore 4014
        }

        // ReSharper disable once UnusedMethodReturnValue.Local - not recommended to use void with async
        private async Task ValidateTfsSettings()
        {
            tabTfs.Enabled = false;

            var tempOptions = new MonitorOptions();
            RetrieveOptions(tempOptions);

            if (string.IsNullOrWhiteSpace(tempOptions.TfsApiUrl))
            {
                cboTfsProjectName.Enabled = false;
                btnOk.Enabled = false;
                tabTfs.Enabled = true;
                return;
            }


            try
            {
                var store = m_BuildStoreFactory.GetBuildStore(tempOptions);
                var projects = await store.GetProjects();
                cboTfsProjectName.Items.AddRange(projects.Cast<object>().ToArray());
                imgBox.Image = Status.Succeeded.ToBitmap(new Size(24, 24));
            }
            // filters handle Aggregation Exception
            catch(Exception ex) when (ex is AuthenticationException || 
                    ex.GetBaseException() is AuthenticationException)
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
                m_SavedSettingsValidated = true;
                cboTfsProjectName.SelectedItem = !string.IsNullOrWhiteSpace(m_Options.ProjectName) 
                    ? m_Options.ProjectName 
                    : cboTfsProjectName.Items[0];

                cboTfsProjectName.Enabled = true;
                btnOk.Enabled = true;
                tabTfs.Enabled = true;
                return;
            }

            cboTfsProjectName.Enabled = false;
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
