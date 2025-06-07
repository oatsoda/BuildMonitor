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

    public static class LinkLabelExtensions
    {
        public static void SetUrl(this LinkLabel linkLabel, string url, string changeText = null)
        {
            if (changeText != null)
                linkLabel.Text = changeText;

            linkLabel.Links.Clear();
            linkLabel.Links.Add(0, linkLabel.Text.Length, url);
        }

        public static void VisitUrl(this LinkLabelLinkClickedEventArgs e, object sender)
        {
            var url = e.Link.LinkData.ToString();
            LinkHelper.OpenUrl(url);
            ((LinkLabel)sender).LinkVisited = true;
        }
    }
}
