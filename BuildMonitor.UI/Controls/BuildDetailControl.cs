﻿using System.Diagnostics;
using System.Windows.Forms;
using BuildMonitor.Core;
using BuildMonitor.Core.InterfaceExtensions;
using BuildMonitor.UI.Helpers;

namespace BuildMonitor.UI.Controls
{
    public partial class BuildDetailControl : UserControl
    {
        public ToolTip ToolTip { get; set; }

        public BuildDetailControl()
        {
            InitializeComponent();

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
            var url = buildDetail.Status == null ? buildDetail.Definition.Url : buildDetail.Status.Url;
            
            lblLinkTitle.Text = buildDetail.Definition.Name;

            lblLinkTitle.Links.Clear();
            lblLinkTitle.Links.Add(0, lblLinkTitle.Text.Length, url);

            if (buildDetail.Status != null)
                ToolTip.SetToolTip(lblLinkTitle, buildDetail.Status.Name);

            lblRequestedBy.Text = buildDetail.Status.ToRequestedByDescription(18);
            lblStart.Text = buildDetail.Status.ToCurrentTimeDescription();

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
            var si = new ProcessStartInfo(e.Link.LinkData.ToString());
            Process.Start(si);
            lblLinkTitle.LinkVisited = true;
        }
    }
}
