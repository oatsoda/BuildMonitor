using System;
using System.Drawing;
using System.Windows.Forms;

namespace BuildMonitor.UI.Helpers
{
    public static class ControlExtensions
    {
        public static Rectangle GetScreenBounds(this Control ctrl)
        {
            return Screen.FromControl(ctrl).WorkingArea;
        }

        public static void InvokeIfRequired(this Control control, Action action)
        {
            if (control.InvokeRequired)
                control.BeginInvoke(new MethodInvoker(() => action()));
            else
                action();
        }
    }
}
