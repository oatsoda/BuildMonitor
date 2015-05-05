using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BuildMonitor.Core;
using BuildMonitor.UI.Helpers;
using BuildMonitor.UI.Options;

namespace BuildMonitor.UI.Controls
{
    public partial class BuildDefinitionsListForm : Form
    {
        public const int OFFSET_X = 10;
        public const int OFFSET_Y = 3;

        private bool m_Closing;
        private int m_CalculatedWidth;
        private int m_CalculatedHeight;

        private IMonitorOptions m_CurrentMonitorOptions;

        private readonly IBuildDefinitionMonitor m_Monitor;

        private IEnumerable<BuildDetailControl> BuildDetailControls
        {
            get { return Controls.OfType<BuildDetailControl>(); }
        } 

        public BuildDefinitionsListForm(IBuildDefinitionMonitor monitor, IMonitorOptions currentOptions)
        {
            InitializeComponent();

            m_Monitor = monitor;
            m_CurrentMonitorOptions = currentOptions;

            m_Monitor.OverallStatusChanged += OnBuildMonitorOnOverallStatusChanged;
            m_Monitor.ExceptionOccurred += OnBuildMonitorOnExceptionOccurred;
            m_Monitor.Updated += OnBuildMonitorOnUpdated;

            TopMost = true;
            notifyIcon.Icon = Icon;

            m_CalculatedHeight = m_CalculatedWidth = 0;

            ApplyOptions();
        }

        private void ApplyOptions()
        {
            Controls.Clear();

            //if (string.IsNullOrWhiteSpace(m_CurrentMonitorOptions.TfsApiUrl))
            //{
            //    SetMessageOnly("No TFS URL configured...");
            //    return;
            //}

            SetMessageOnly("Loading builds...");
            
            m_Monitor.Start(m_CurrentMonitorOptions);
        }

        #region Build Definition Monitor

        private void OnBuildMonitorOnUpdated(object sender, List<BuildDetail> list)
        {
            this.InvokeIfRequired(() =>
            {
                UpdateBuildControls(list);
                SetSizeAndPosition();
            });
        }

        private void OnBuildMonitorOnExceptionOccurred(object sender, Exception exception)
        {
            this.InvokeIfRequired(() =>
            {
                notifyIcon.BalloonTipText = string.Format("Failed to monitor server: {0}", exception);
                notifyIcon.ShowBalloonTip(20000);
            });
        }

        private void OnBuildMonitorOnOverallStatusChanged(object sender, BuildDetail buildDetail)
        {
            this.InvokeIfRequired(() =>
            {
                notifyIcon.Icon = buildDetail.Status.Status.ToIcon();

                new PopupStatusForm(buildDetail).Show();
            });
        }

        #endregion

        private void UpdateBuildControls(IEnumerable<BuildDetail> buildDetails)
        {
            RemoveLabels();

            var controlHeight = 0;
            var controlWidth = 0;

            var buildDetailControls = BuildDetailControls.ToList();

            var buildDetailsList = buildDetails.ToList();

            var x = 0;
            foreach (var detail in buildDetailsList)
            {
                x++;

                BuildDetailControl c;

                if (x <= buildDetailControls.Count)
                {
                    c = buildDetailControls[x - 1];
                }
                else
                {
                    c = new BuildDetailControl();
                    c.Top = ((x - 1)*c.Height);
                    c.DisplayDetail(detail);
                    Controls.Add(c);
                }

                controlHeight = c.Height; // Need this later
                controlWidth = c.Width;

                c.ToggleBorder(x < buildDetailsList.Count);
            }

            m_CalculatedHeight = x * controlHeight;
            m_CalculatedWidth = controlWidth;

            if (x == 0)
                SetMessageOnly("No builds found.");

            if (x >= buildDetailControls.Count) // original count
                return;

            RemoveUnusedControls(buildDetailControls.Count - x);
        }

        private void RemoveUnusedControls(int numberToRemove)
        {
            while (numberToRemove > 0)
            {
                var lastControl = Controls.OfType<BuildDetailControl>().Last();
                Controls.Remove(lastControl);
                lastControl.Dispose();
                numberToRemove--;
            }
        }

        private void RemoveLabels()
        {
            while (Controls.OfType<Label>().Any())
            {
                Controls.Remove(Controls.OfType<Label>().Last());
            }
        }

        private void SetMessageOnly(string message)
        {
            var label = new Label
            {
                Text = message
            };

            Controls.Add(label);

            m_CalculatedHeight = label.Height;
            m_CalculatedWidth = label.Width;
        }

        private void SetSizeAndPosition()
        {
            if (WindowState == FormWindowState.Minimized)
                return;

            Height = m_CalculatedHeight;
            Width = m_CalculatedWidth;

            var bounds = this.GetSreenBounds();
            var x = (bounds.Width - Width) - OFFSET_X;
            var y = (bounds.Height - Height) - OFFSET_Y;
            SetDesktopLocation(x, y);
        }

        #region Form Events

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (m_Closing)
                return;

            e.Cancel = true;
            Hide();
        }

        #endregion

        #region Toolstrip Event Handlers
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_Closing = true;

            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            
            m_Monitor.OverallStatusChanged -= OnBuildMonitorOnOverallStatusChanged;
            m_Monitor.ExceptionOccurred -= OnBuildMonitorOnExceptionOccurred;
            m_Monitor.Updated -= OnBuildMonitorOnUpdated;

            m_Monitor.Dispose();

            Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new SettingsForm(m_CurrentMonitorOptions))
            {
                settingsForm.ShowDialog(this);

                if (settingsForm.DialogResult != DialogResult.OK)
                    return;

                m_CurrentMonitorOptions = settingsForm.Options;
            }

            ApplyOptions();
        }

        #endregion

        #region Notify Icon Events

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (WindowState == FormWindowState.Normal)
            {
                Hide();
                WindowState = FormWindowState.Minimized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                SetSizeAndPosition();
                Show();
            }
        }

        #endregion
    }
}
