using BuildMonitor.Core;
using BuildMonitor.UI.Properties;
using System;
using System.Drawing;

namespace BuildMonitor.UI
{
    internal static class StatusExtensions
    {
        public static Bitmap ToBitmap(this Status status)
        {
            switch (status)
            {
                case Status.Unknown:
                    return null;
                case Status.Succeeded:
                    return Resources.Succeeded;
                case Status.PartiallySucceeded:
                    return Resources.PartiallySucceeded;
                case Status.Failed:
                    return Resources.Failed;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        public static Icon ToIcon(this Status status)
        {
            return status.ToBitmap().ToPngIcon();
        }
    }
}
