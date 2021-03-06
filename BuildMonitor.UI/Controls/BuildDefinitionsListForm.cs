﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuildMonitor.Core;
using BuildMonitor.UI.Helpers;
using BuildMonitor.UI.Options;
using BuildMonitor.UI.Updater;

namespace BuildMonitor.UI.Controls
{
    public partial class BuildDefinitionsListForm : Form
    {
        #region Public Constants

        public const int OFFSET_X = 10;
        public const int OFFSET_Y = 3;

        #endregion

        #region Private Fields & Properties

        private bool m_Closing;
        private int m_CalculatedWidth;
        private int m_CalculatedHeight;

        private bool m_FirstStatusUpdate;

        private IMonitorOptions m_CurrentMonitorOptions;
        private readonly IBuildStoreFactory m_BuildStoreFactory;
        private readonly IAppUpdater m_AppUpdater;

        private readonly IBuildDefinitionMonitor m_Monitor;
        
        private IEnumerable<BuildDetailControl> BuildDetailControls => Controls.OfType<BuildDetailControl>();

        private ToolTip m_ToolTip;

        #endregion

        #region Constructor

        public BuildDefinitionsListForm(IBuildDefinitionMonitor monitor, 
                                        IMonitorOptions currentOptions, 
                                        IBuildStoreFactory buildStoreFactory,
                                        IAppUpdater appUpdater)
        {
            InitializeComponent();

            // This ensures first click on notify icon displays the form. Otherwise the
            // first call to SetDesktopLocation sets the WindowState back to Minimized even
            // though we have just set it to Normal.
            Visible = false; 

            m_FirstStatusUpdate = true;

            m_Monitor = monitor;
            m_CurrentMonitorOptions = currentOptions;
            m_BuildStoreFactory = buildStoreFactory;
            m_AppUpdater = appUpdater;

            m_Monitor.OverallStatusChanged += OnBuildMonitorOnOverallStatusChanged;
            m_Monitor.ExceptionOccurred += OnBuildMonitorOnExceptionOccurred;
            m_Monitor.Updated += OnBuildMonitorOnUpdated;
            m_Monitor.MonitoringStopped += OnBuildMonitorMonitoringStopped;

            TopMost = true;
            notifyIcon.Icon = Icon;

            m_CalculatedHeight = m_CalculatedWidth = 0;

            ApplyOptions();

            m_ToolTip = new ToolTip()
            {
                ShowAlways = true,
                IsBalloon = true
            };
        }
        
        #endregion

        #region Private Methods

        private void ApplyOptions()
        {
            Controls.Clear();

            SetMessageOnly("Waiting for builds...");
            
            Task.Run(() => m_Monitor.Start(m_CurrentMonitorOptions));
        }

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
                    c.DisplayDetail(detail);
                }
                else
                {
                    c = new BuildDetailControl();
                    c.Top = ((x - 1)*c.Height);
                    c.ToolTip = m_ToolTip;
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
            Controls.Clear();

            var label = new Label
            {
                AutoSize = true,
                Text = message,
                Dock = DockStyle.Fill,
                MaximumSize = new Size(Width, 0)
            };

            Controls.Add(label);

            m_CalculatedHeight = label.Height;
            m_CalculatedWidth = label.Width;
        }

        private void SetSizeAndPosition()
        {
            Height = m_CalculatedHeight;
            Width = m_CalculatedWidth;

            var bounds = this.GetSreenBounds();
            var x = (bounds.Width - Width) - OFFSET_X;
            var y = (bounds.Height - Height) - OFFSET_Y;
            SetDesktopLocation(x, y);
        }

        private void CloseApplication()
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

        #endregion

        #region Base Form Overrides

        protected override void SetVisibleCore(bool value)
        {
            if (!IsHandleCreated)
            {
                // First time, don't make visible but still create the handle
                // http://stackoverflow.com/a/3742980/868159
                value = false;
                CreateHandle();
            }
            base.SetVisibleCore(value);
        }

        #endregion

        #region Build Definition Monitor Events

        private void OnBuildMonitorOnUpdated(object sender, List<BuildDetail> list)
        {
            this.InvokeIfRequired(() =>
            {
                UpdateBuildControls(list);
                if (WindowState != FormWindowState.Minimized)
                    SetSizeAndPosition();
            });
        }

        private void OnBuildMonitorOnExceptionOccurred(object sender, Exception exception)
        {
            this.InvokeIfRequired(() =>
            {
                var aggEx = exception as AggregateException;
                if (aggEx != null)
                    exception = aggEx.Flatten();

                SetMessageOnly(exception.ToString());
                notifyIcon.BalloonTipText = $"Monitor error: {exception}";
                notifyIcon.ShowBalloonTip(20000);
            });
        }

        private void OnBuildMonitorMonitoringStopped(object sender, string stoppedReason)
        {
            this.InvokeIfRequired(() =>
            {
                SetMessageOnly(stoppedReason);
                notifyIcon.BalloonTipText = $"Monitor stopped: {stoppedReason}";
                notifyIcon.ShowBalloonTip(20000);
            });
        }

        private void OnBuildMonitorOnOverallStatusChanged(object sender, BuildDetail buildDetail)
        {
            this.InvokeIfRequired(() =>
            {
                notifyIcon.Icon = buildDetail.Status.Status.ToIcon();

                if (!m_FirstStatusUpdate || buildDetail.Status.Status != Status.Succeeded) // Don't notify if first load is Succeeded
                    new PopupStatusForm(buildDetail).Show();

                m_FirstStatusUpdate = false;
            });
        }

        #endregion

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
            CloseApplication();
        }
        
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_AppUpdater.CheckForUpdates())
                CloseApplication();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Stop monitoring while changing settings
            m_Monitor.Stop();

            using (var settingsForm = new SettingsForm(m_CurrentMonitorOptions, m_BuildStoreFactory))
            {
                settingsForm.ShowDialog(this);

                if (settingsForm.DialogResult != DialogResult.OK)
                    return;

                m_CurrentMonitorOptions = settingsForm.Options;
                Hide();
            }

            ApplyOptions();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var aboutForm = new AboutForm())
                aboutForm.ShowDialog(this);
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
