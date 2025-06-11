using BuildMonitor.Core;
using BuildMonitor.UI.Helpers;
using BuildMonitor.UI.Options;
using BuildMonitor.UI.Updater;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildMonitor.UI.Controls
{
    public partial class BuildDefinitionsListForm : Form
    {
        #region Private Fields & Properties

        private bool m_Closing;
        private int m_CalculatedWidth;
        private int m_CalculatedHeight;

        private bool m_FirstStatusUpdate;

        private IMonitorOptions m_CurrentMonitorOptions;
        private readonly IBuildStoreFactory m_BuildStoreFactory;
        private readonly IAppUpdater m_AppUpdater;

        private readonly BuildDefinitionMonitor m_Monitor;

        private IEnumerable<BuildDetailControl> BuildDetailControls => Controls.OfType<BuildDetailControl>();

        private bool m_IsSettingsOpen;
        private bool m_IsAboutOpen;

        #endregion

        #region Constructor

        public BuildDefinitionsListForm(BuildDefinitionMonitor monitor,
                                        IMonitorOptions currentOptions,
                                        IBuildStoreFactory buildStoreFactory,
                                        IAppUpdater appUpdater)
        {
            InitializeComponent();
            notifyIcon.Text = VersionHelper.AppName;
            ScreenLayout.SetToSectionSizeWithoutMaximum(this);

            // This ensures first click on notify icon displays the form. Otherwise the
            // first call to SetDesktopLocation sets the WindowState back to Minimized even
            // though we have just set it to Normal.
            // (TODO: Actually this sometimes seem to still happen and the weird form appears)
            Visible = false;

            m_Monitor = monitor;
            m_CurrentMonitorOptions = currentOptions;
            m_BuildStoreFactory = buildStoreFactory;
            m_AppUpdater = appUpdater;

            m_Monitor.OverallStatusChanged += OnBuildMonitorOnOverallStatusChanged;
            m_Monitor.ExceptionOccurred += OnBuildMonitorOnExceptionOccurred;
            m_Monitor.Updated += OnBuildMonitorOnUpdated;
            m_Monitor.MonitoringStopped += OnBuildMonitorMonitoringStopped;

            TopMost = true;

            m_CalculatedHeight = m_CalculatedWidth = 0;
            Controls.Clear();

            ApplyOptions();
        }

        #endregion

        #region Private Methods

        private void ApplyOptions()
        {
            Debug.WriteLine("ApplyOptions...");

            m_FirstStatusUpdate = true;
            notifyIcon.Icon = Icon;
            SetMessageOnly("Waiting for builds...");
            SetSizeAndPosition();

            Debug.WriteLine("Triggering start...");

            Task.Run(() => m_Monitor.Start(m_CurrentMonitorOptions));
        }

        private void UpdateBuildControls(IEnumerable<BuildDetail> buildDetails)
        {
            SuspendLayout();
            RemoveLabels();

            var controlHeight = 0;
            var controlWidth = 0;

            var buildDetailControls = BuildDetailControls.ToList();

            var buildDetailsList = m_CurrentMonitorOptions.OrderByMostRecent
                ? buildDetails.OrderBy(b => b.Status?.Start ?? DateTimeOffset.MinValue).ToList()
                : [.. buildDetails.OrderBy(b => b.Definition.Name)];

            var x = 0;
            foreach (var detail in buildDetailsList)
            {
                x++;

                BuildDetailControl? c = buildDetailControls.FirstOrDefault(b => b.BuildDefinitionId == detail.Definition.Id);

                if (c == null)
                {
                    c = new BuildDetailControl();
                    Controls.Add(c);
                }

                c.Top = ((x - 1) * c.Height);
                c.DisplayDetail(detail); // TODO: make it clearer we re-use same control to avoid re-painting (Link URL error)

                controlHeight = c.Height; // Need this later
                controlWidth = c.Width;

                c.ToggleBorder(x < buildDetailsList.Count);
            }

            m_CalculatedHeight = x * controlHeight;
            m_CalculatedWidth = controlWidth;

            if (x == 0)
                SetMessageOnly("No builds found.");

            foreach (var c in buildDetailControls.Where(c => !buildDetailsList.Any(d => d.Definition.Id == c.BuildDefinitionId)))
            {
                Controls.Remove(c);
                c.Dispose();
            }

            ResumeLayout();
        }

        private void RemoveLabels()
        {
            foreach (var label in Controls.OfType<Label>())
            {
                Controls.Remove(label);
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
                MaximumSize = new Size(ScreenLayout.SECTION_WIDTH, ScreenLayout.SECTION_HEIGHT * 10),
                MinimumSize = ScreenLayout.SectionSize,

                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.OrangeRed,
                Padding = new Padding(3)
            };

            Controls.Add(label);

            m_CalculatedHeight = label.Height;
            m_CalculatedWidth = ScreenLayout.SECTION_WIDTH;
        }

        private void SetSizeAndPosition()
        {
            Height = m_CalculatedHeight;
            Width = m_CalculatedWidth;

            Debug.WriteLine($"Setting Size [{m_CalculatedWidth},{m_CalculatedHeight} / {Width},{Height}]");

            var bounds = this.GetScreenBounds();
            var x = (bounds.Width - m_CalculatedWidth) - ScreenLayout.OFFSET_X;
            var y = (bounds.Height - m_CalculatedHeight) - ScreenLayout.OFFSET_Y;
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

        private void ShowSettingsForm()
        {
            if (m_IsSettingsOpen)
                return;

            m_IsSettingsOpen = true;

            // Stop monitoring while changing settings
            m_Monitor.Stop();

            using (var settingsForm = new SettingsForm(m_CurrentMonitorOptions, m_BuildStoreFactory))
            {
                settingsForm.ShowDialog(this);

                m_IsSettingsOpen = false;

                if (settingsForm.DialogResult == DialogResult.OK ||
                    settingsForm.DialogResult == DialogResult.Abort)
                {
                    m_CurrentMonitorOptions = settingsForm.Options;
                }
                else
                {
                    return;
                }
            }

            Hide();
            ApplyOptions();
        }

        private void ShowAboutForm()
        {
            if (m_IsAboutOpen)
                return;

            m_IsAboutOpen = true;

            using (var aboutForm = new AboutForm())
                aboutForm.ShowDialog(this);

            m_IsAboutOpen = false;
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

        private void OnBuildMonitorOnUpdated(object? sender, List<BuildDetail> list)
        {
            Debug.WriteLine($"OnBuildMonitorOnUpdated: [{string.Join(", ", list.Select(b => $"{b.Definition.Name}: {b.Status?.Status}"))}]");

            this.InvokeIfRequired(() =>
            {
                UpdateBuildControls(list);
                SetSizeAndPosition();
            });
        }

        private void OnBuildMonitorOnExceptionOccurred(object? sender, Exception exception)
        {
            this.InvokeIfRequired(() =>
            {
                if (exception is AggregateException aggEx)
                    exception = aggEx.Flatten();

                AboutForm.AddException(exception);
                notifyIcon.BalloonTipText = $"Monitor error: {exception}";
                notifyIcon.ShowBalloonTip(20000);
            });
        }

        private void OnBuildMonitorMonitoringStopped(object? sender, string stoppedReason)
        {
            this.InvokeIfRequired(() =>
            {
                SetMessageOnly(stoppedReason);
                SetSizeAndPosition();
                notifyIcon.BalloonTipText = $"Monitor stopped: {stoppedReason}";
                notifyIcon.ShowBalloonTip(20000);
                ShowSettingsForm();
            });

        }

        private void OnBuildMonitorOnOverallStatusChanged(object? sender, BuildDetail buildDetail)
        {
            Debug.WriteLine($"OnBuildMonitorOnOverallStatusChanged: {buildDetail.Definition.Name} - {buildDetail.Status?.Status}");

            this.InvokeIfRequired(() =>
            {
                notifyIcon.Icon = buildDetail.Status?.Status.ToIcon();

                if (!m_FirstStatusUpdate) // Don't notify on first load
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

        private async void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (await m_AppUpdater.CheckForUpdates())
                CloseApplication();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettingsForm();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAboutForm();
        }

        #endregion

        #region Notify Icon Events

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            Debug.WriteLine($"NotifyItem Click {WindowState} [{m_CalculatedWidth},{m_CalculatedHeight} / {Width},{Height}]");

            SuspendLayout();

            if (WindowState == FormWindowState.Normal)
            {
                Debug.WriteLine($"NotifyItem Hiding...");
                Hide();
                WindowState = FormWindowState.Minimized;
            }
            else
            {
                Debug.WriteLine($"NotifyItem Set Size...");
                WindowState = FormWindowState.Normal;
                SetSizeAndPosition();

                Debug.WriteLine($"NotifyItem Showing [{m_CalculatedWidth},{m_CalculatedHeight} / {Width},{Height}]");

                Show();

                Debug.WriteLine($"NotifyItem Shown ({WindowState}) [{m_CalculatedWidth},{m_CalculatedHeight} / {Width},{Height}]");

                if (WindowState == FormWindowState.Minimized)
                {
                    Debug.WriteLine($"Incorrect Show(); State still Minimised...");
                    WindowState = FormWindowState.Normal;
                }
            }

            ResumeLayout();
        }

        #endregion
    }
}
