using BuildMonitor.Core;
using BuildMonitor.UI.Helpers;
using System.ComponentModel;
using System.Windows.Forms;

namespace BuildMonitor.UI.Controls
{
    public partial class BuildDetailControl : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolTip ToolTip { get; set; }

        public BuildDetailControl()
        {
            InitializeComponent();

            ToolTip = new();
            imgErrors.Image = Status.Failed.ToBitmap(imgErrors.Size);
            imgWarnings.Image = Status.PartiallySucceeded.ToBitmap(imgErrors.Size);
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
            var url = buildDetail.Status == null
                ? buildDetail.Definition.Url
                : buildDetail.Status.Url;

            lblLinkTitle.SetUrl(url, buildDetail.Definition.Name);

            if (buildDetail.Status != null)
                ToolTip.SetToolTip(lblLinkTitle, buildDetail.Status.Name);

            lblRequestedBy.Text = buildDetail.Status?.ToRequestedByDescription(30);
            lblStart.Text = buildDetail.Status?.ToCurrentTimeDescription();

            if (buildDetail.Definition.IsVNext)
            {
                lblErrors.Text = buildDetail.Status?.ErrorCount.ToString() ?? "0";
                lblWarnings.Text = buildDetail.Status?.WarningCount.ToString() ?? "0";
            }

            lblErrors.Visible =
                imgErrors.Visible =
                lblWarnings.Visible =
                imgWarnings.Visible = buildDetail.Definition.IsVNext;

            picStatus.Image = buildDetail.Status?.Status.ToBitmap(picStatus.Size);
        }

        private void lblLinkTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            e.VisitUrl(sender);
        }
    }
}
