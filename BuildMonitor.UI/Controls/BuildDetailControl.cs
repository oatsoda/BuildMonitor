using BuildMonitor.Core;
using BuildMonitor.UI.Helpers;
using System.ComponentModel;
using System.Windows.Forms;

namespace BuildMonitor.UI.Controls
{
    public partial class BuildDetailControl : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BuildDefinitionId { get; private set; }

        public BuildDetailControl()
        {
            InitializeComponent();
            ScreenLayout.SetToSectionSizeFixed(this);

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
            SuspendLayout();

            BuildDefinitionId = buildDetail.Definition.Id;

            var url = buildDetail.Status == null
                ? buildDetail.Definition.Url
                : buildDetail.Status.Url;

            lblLinkTitle.SetUrl(url, buildDetail.Definition.Name);

            if (buildDetail.Status != null)
            {
                tipLink.SetToolTip(lblLinkTitle, buildDetail.Status.Name);
            }

            lblRequestedBy.Text = buildDetail.Status?.ToRequestedByDescription(30);
            lblStart.Text = buildDetail.Status?.ToCurrentTimeDescription();

            if (buildDetail.Definition.IsVNext)
            {
                var errorCount = buildDetail.Status?.ErrorCount ?? 0;
                var hasErrors = errorCount > 0;
                lblErrors.Text = errorCount.ToString();
                lblErrors.Visible = imgErrors.Visible = hasErrors;
                lblWarnings.Text = buildDetail.Status?.WarningCount.ToString() ?? "0";
                lblWarnings.Visible = imgWarnings.Visible = hasErrors || buildDetail.Status?.WarningCount > 0;
            }
            else
            {
                lblErrors.Visible =
                    imgErrors.Visible =
                    lblWarnings.Visible =
                    imgWarnings.Visible = false;
            }

            picStatus.Image = buildDetail.Status?.Status.ToBitmap(picStatus.Size);

            ResumeLayout();
        }

        private void lblLinkTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            e.VisitUrl(sender);
        }
    }
}
