using BuildMonitor.Core;
using BuildMonitor.UI.Properties;
using System;
using System.Drawing;

namespace BuildMonitor.UI.Helpers
{
    internal static class StatusExtensions
    {
        public static Bitmap? ToBitmap(this Status status, Size size)
        {
            var ico = status.ToIcon();
            if (ico == null)
                return null;
            return new Bitmap(ico.ToBitmap(), size);
        }

        public static Icon? ToIcon(this Status status)
        {
            switch (status)
            {
                case Status.Unknown:
                    return null;
                case Status.Succeeded:
                    return Resources._1437954485_accepted_24;
                case Status.PartiallySucceeded:
                    return Resources._1437954464_warning_24;
                case Status.Failed:
                    return Resources._1437954459_cancel_24;
                case Status.InProgress:
                    return Resources._1437954472_arrow_right_24;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status));
            }
        }
    }
}
