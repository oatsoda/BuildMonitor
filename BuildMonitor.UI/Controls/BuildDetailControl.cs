using System;
using System.Diagnostics;
using System.Windows.Forms;
using BuildMonitor.Core;
using BuildMonitor.UI.Helpers;

namespace BuildMonitor.UI.Controls
{
    public partial class BuildDetailControl : UserControl
    {
        public BuildDetailControl()
        {
            InitializeComponent();
        }

        public void ToggleBorder(bool show)
        {
            if (!show)
            {
                groupBox1.Visible = false;
            }
            else
            {
                groupBox1.Width = Width;
                groupBox1.Height = 1;
                groupBox1.Left = 0;
                groupBox1.Top = Height - 1;
                groupBox1.Visible = true;
            }
        }

        public void DisplayDetail(BuildDetail buildDetail)
        {
            var url = buildDetail.Status == null ? buildDetail.Definition.Url : buildDetail.Status.Url;

            lblLinkTitle.Text = buildDetail.Definition.Name;
            lblLinkTitle.Links.Clear();
            lblLinkTitle.Links.Add(0, lblLinkTitle.Text.Length, url);

            lblRequestedBy.Text = GetRequestedByDescr(buildDetail.Status);
            lblStart.Text = GetStartDescr(buildDetail.Status);

            picStatus.Image = buildDetail.Status == null ? null : buildDetail.Status.Status.ToBitmap(picStatus.Size);
        }

        private void lblLinkTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var si = new ProcessStartInfo(e.Link.LinkData.ToString());
            Process.Start(si);
            lblLinkTitle.LinkVisited = true;
        }

        private static string GetStartDescr(IBuildStatus status)
        {
            if (status == null)
                return "-";

            var diff = DateTime.Now.Subtract(status.Start);

            if (diff.TotalHours >= 48)
                return string.Format("{0} days ago", (int)diff.TotalDays);

            if (diff.TotalMinutes >= 120)
                return string.Format("{0} hours ago", (int)diff.TotalHours);

            if (diff.TotalMinutes >= 1)
                return string.Format("{0} minutes ago", (int)diff.TotalMinutes);

            return string.Format("{0} seconds ago", (int)diff.TotalSeconds);
        }

        private static string GetRequestedByDescr(IBuildStatus status)
        {
            const int len = 18;
            if (status == null || status.RequestedBy == null)
                return "-";

            if (status.RequestedBy.Length > len)
                return string.Format("{0}...", status.RequestedBy.Substring(0, len));

            return status.RequestedBy;
        }
    }
}
