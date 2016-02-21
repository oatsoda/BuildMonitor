using System;
using System.Drawing;
using System.Windows.Forms;
using BuildMonitor.Core;
using BuildMonitor.UI.Helpers;

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

            TopMost = true;
            Width = buildDetailControl.Width;
            Height = buildDetailControl.Height;
            MinimumSize = new Size(Width, 0);

            buildDetailControl.ToolTip = new ToolTip()
            {
                ShowAlways = true,
                IsBalloon = true
            };

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
            var bounds = this.GetSreenBounds();

            m_ExpectedMinTop = bounds.Height;
            m_ExpectedMaxTop = (bounds.Height - Height) - 2;

            
            var x = (bounds.Width - Width) - BuildDefinitionsListForm.OFFSET_X;
            var y = (bounds.Height) - BuildDefinitionsListForm.OFFSET_Y;
            SetDesktopLocation(x, y);
        
            Height = 0;
        }
        
        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            switch (m_TransitionState)
            {
                case TransitionState.Opening:

                    Top = Top - _INTERVAL_PIXELS;
                    Height = Height + _INTERVAL_PIXELS;

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

                    Top = Top + _INTERVAL_PIXELS;
                    Height = Height - _INTERVAL_PIXELS;

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
