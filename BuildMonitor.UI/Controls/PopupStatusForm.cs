using BuildMonitor.Core;
using BuildMonitor.UI.Helpers;
using System;
using System.Windows.Forms;

namespace BuildMonitor.UI.Controls
{
    public sealed partial class PopupStatusForm : Form
    {
        private enum TransitionState { Opening, Hold, Closing }

        private const int _INTERVAL_PIXELS = 2;
        private const int _MOVE_TIMER_INTERVAL = 100;
        private const int _HOLD_TIMER_INTERVAL = 10000;

        private readonly Timer m_Timer;
        private int m_ExpectedMaxTop;
        private int m_ExpectedMinTop;
        private TransitionState m_TransitionState = TransitionState.Opening;

        public PopupStatusForm(BuildDetail buildDetail)
        {
            InitializeComponent();
            ScreenLayout.SetToSectionSizeWithZeroMinHeight(this);
            ScreenLayout.SetToSectionSizeFixed(buildDetailControl);

            TopMost = true;

            buildDetailControl.ToolTip = new ToolTip()
            {
                ShowAlways = true,
                IsBalloon = true
            };
            buildDetailControl.BackColor = System.Drawing.Color.FromArgb(
                buildDetailControl.BackColor.R + 20,
                buildDetailControl.BackColor.G + 20,
                buildDetailControl.BackColor.B + 20);

            m_Timer = new Timer
            {
                Enabled = false,
                Interval = _MOVE_TIMER_INTERVAL
            };
            m_Timer.Tick += TimerOnTick;

            buildDetailControl.DisplayDetail(buildDetail);
            SetStartLocation();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            m_Timer.Start();
        }

        private void SetStartLocation()
        {
            var bounds = this.GetScreenBounds();

            m_ExpectedMinTop = bounds.Height;
            m_ExpectedMaxTop = (bounds.Height - Height) - 2;

            var x = (bounds.Width - Width) - ScreenLayout.OFFSET_X;
            var y = (bounds.Height) - ScreenLayout.OFFSET_Y;
            SetDesktopLocation(x, y);

            Height = 0;
        }

        private void TimerOnTick(object? sender, EventArgs eventArgs)
        {
            switch (m_TransitionState)
            {
                case TransitionState.Opening:

                    Top -= _INTERVAL_PIXELS;
                    Height += _INTERVAL_PIXELS;

                    var reachedHold = Top <= m_ExpectedMaxTop;

                    if (reachedHold)
                    {
                        m_TransitionState = TransitionState.Hold;
                        m_Timer.Interval = _HOLD_TIMER_INTERVAL;
                    }

                    break;

                case TransitionState.Hold:

                    m_TransitionState = TransitionState.Closing;
                    m_Timer.Interval = _MOVE_TIMER_INTERVAL;

                    break;

                case TransitionState.Closing:

                    Top += _INTERVAL_PIXELS;
                    Height -= _INTERVAL_PIXELS;

                    var reachedBottom = Top >= m_ExpectedMinTop;

                    if (reachedBottom)
                        Finish();

                    break;
            }
        }

        private void Finish()
        {
            Hide();
            m_Timer.Stop();
            m_Timer.Tick -= TimerOnTick;
            Dispose();
        }
    }
}
