using System.Diagnostics;
using System.Windows.Forms;

namespace BuildMonitor.UI.Helpers
{
    internal static class LinkHelper
    {
        public static void OpenUrl(string url)
        {
            var si = new ProcessStartInfo(url)
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(si);
        }
    }

    public static class LinkLabelLinkClickedEventArgsExtensions
    {
        public static void VisitUrl(this LinkLabelLinkClickedEventArgs e, object sender)
        {
            var url = e.Link.LinkData.ToString();
            LinkHelper.OpenUrl(url);
            ((LinkLabel)sender).LinkVisited = true;
        }
    }
}
