using System;
using System.Drawing;
using BuildMonitor.Core;
using BuildMonitor.UI.Properties;

namespace BuildMonitor.UI.Helpers
{
    internal static class StatusExtensions
    {
        public static Bitmap ToBitmap(this Status status, Size size)
        {
            var ico = status.ToIcon();
            return new Bitmap(ico.ToBitmap(), size);
        }

        public static Icon ToIcon(this Status status)
        {
            switch (status)
            {
                case Status.Unknown:
                    return null;
                case Status.Succeeded:
                    return Resources._0007_Tick;
                case Status.PartiallySucceeded:
                    return Resources._0010_Alert;
                case Status.Failed:
                    return Resources._0006_Cross;
                case Status.InProgress:
                    return Resources.Play1Normal;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }
    }
}
