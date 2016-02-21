using System.Diagnostics;
using System.Windows.Forms;
using BuildMonitor.Core;
using BuildMonitor.Core.InterfaceExtensions;
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
                panelLine.Visible = false;
            }
            else
            {
                panelLine.Width = Width;
                panelLine.Height = 1;
                panelLine.Left = 0;
                panelLine.Top = Height - 1;
                panelLine.Visible = true;
            }
        }

        public void DisplayDetail(BuildDetail buildDetail)
        {
            var url = buildDetail.Status == null ? buildDetail.Definition.Url : buildDetail.Status.Url;
            
            lblLinkTitle.Text = buildDetail.Definition.Name;
            lblLinkTitle.Links.Clear();
            lblLinkTitle.Links.Add(0, lblLinkTitle.Text.Length, url);
            //if (buildDetail.Status != null)
            //    new ToolTip().SetToolTip(lblLinkTitle, buildDetail.Status.Name);

            lblRequestedBy.Text = buildDetail.Status.ToRequestedByDescription(18);
            lblStart.Text = buildDetail.Status.ToCurrentTimeDescription();

            picStatus.Image = buildDetail.Status?.Status.ToBitmap(picStatus.Size);
        }

        private void lblLinkTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var si = new ProcessStartInfo(e.Link.LinkData.ToString());
            Process.Start(si);
            lblLinkTitle.LinkVisited = true;
        }
    }
}
