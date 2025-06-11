namespace BuildMonitor.UI.Controls
{
    partial class BuildDetailControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblLinkTitle = new System.Windows.Forms.LinkLabel();
            lblRequestedBy = new System.Windows.Forms.Label();
            lblStart = new System.Windows.Forms.Label();
            picStatus = new System.Windows.Forms.PictureBox();
            panelLine = new System.Windows.Forms.Panel();
            lblErrors = new System.Windows.Forms.Label();
            lblWarnings = new System.Windows.Forms.Label();
            imgErrors = new System.Windows.Forms.PictureBox();
            imgWarnings = new System.Windows.Forms.PictureBox();
            tipLink = new System.Windows.Forms.ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)picStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imgErrors).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imgWarnings).BeginInit();
            SuspendLayout();
            // 
            // lblLinkTitle
            // 
            lblLinkTitle.ActiveLinkColor = System.Drawing.Color.FromArgb(0, 122, 204);
            lblLinkTitle.AutoSize = true;
            lblLinkTitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblLinkTitle.LinkColor = System.Drawing.Color.FromArgb(0, 122, 204);
            lblLinkTitle.Location = new System.Drawing.Point(40, 4);
            lblLinkTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblLinkTitle.Name = "lblLinkTitle";
            lblLinkTitle.Size = new System.Drawing.Size(81, 20);
            lblLinkTitle.TabIndex = 0;
            lblLinkTitle.TabStop = true;
            lblLinkTitle.Text = "lblLinkTitle";
            lblLinkTitle.VisitedLinkColor = System.Drawing.Color.FromArgb(0, 122, 204);
            lblLinkTitle.LinkClicked += lblLinkTitle_LinkClicked;
            // 
            // lblRequestedBy
            // 
            lblRequestedBy.AutoSize = true;
            lblRequestedBy.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblRequestedBy.ForeColor = System.Drawing.Color.FromArgb(241, 241, 241);
            lblRequestedBy.Location = new System.Drawing.Point(40, 28);
            lblRequestedBy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblRequestedBy.Name = "lblRequestedBy";
            lblRequestedBy.Size = new System.Drawing.Size(112, 20);
            lblRequestedBy.TabIndex = 1;
            lblRequestedBy.Text = "lblRequestedBy";
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblStart.ForeColor = System.Drawing.Color.FromArgb(241, 241, 241);
            lblStart.Location = new System.Drawing.Point(254, 28);
            lblStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblStart.Name = "lblStart";
            lblStart.Size = new System.Drawing.Size(57, 20);
            lblStart.TabIndex = 2;
            lblStart.Text = "lblStart";
            // 
            // picStatus
            // 
            picStatus.Location = new System.Drawing.Point(9, 7);
            picStatus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            picStatus.Name = "picStatus";
            picStatus.Size = new System.Drawing.Size(24, 24);
            picStatus.TabIndex = 3;
            picStatus.TabStop = false;
            // 
            // panelLine
            // 
            panelLine.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            panelLine.ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
            panelLine.Location = new System.Drawing.Point(265, 10);
            panelLine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelLine.Name = "panelLine";
            panelLine.Size = new System.Drawing.Size(23, 1);
            panelLine.TabIndex = 4;
            panelLine.Visible = false;
            // 
            // lblErrors
            // 
            lblErrors.AutoSize = true;
            lblErrors.ForeColor = System.Drawing.Color.FromArgb(241, 241, 241);
            lblErrors.Location = new System.Drawing.Point(302, 9);
            lblErrors.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblErrors.Name = "lblErrors";
            lblErrors.Size = new System.Drawing.Size(13, 15);
            lblErrors.TabIndex = 5;
            lblErrors.Text = "0";
            lblErrors.Visible = false;
            // 
            // lblWarnings
            // 
            lblWarnings.AutoSize = true;
            lblWarnings.ForeColor = System.Drawing.Color.FromArgb(241, 241, 241);
            lblWarnings.Location = new System.Drawing.Point(346, 9);
            lblWarnings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblWarnings.Name = "lblWarnings";
            lblWarnings.Size = new System.Drawing.Size(13, 15);
            lblWarnings.TabIndex = 6;
            lblWarnings.Text = "0";
            lblWarnings.Visible = false;
            // 
            // imgErrors
            // 
            imgErrors.Location = new System.Drawing.Point(286, 8);
            imgErrors.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            imgErrors.Name = "imgErrors";
            imgErrors.Size = new System.Drawing.Size(16, 16);
            imgErrors.TabIndex = 7;
            imgErrors.TabStop = false;
            imgErrors.Visible = false;
            // 
            // imgWarnings
            // 
            imgWarnings.Location = new System.Drawing.Point(330, 8);
            imgWarnings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            imgWarnings.Name = "imgWarnings";
            imgWarnings.Size = new System.Drawing.Size(16, 16);
            imgWarnings.TabIndex = 8;
            imgWarnings.TabStop = false;
            imgWarnings.Visible = false;
            // 
            // tipLink
            // 
            tipLink.IsBalloon = true;
            tipLink.ShowAlways = true;
            // 
            // BuildDetailControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
            Controls.Add(imgWarnings);
            Controls.Add(imgErrors);
            Controls.Add(lblWarnings);
            Controls.Add(lblErrors);
            Controls.Add(panelLine);
            Controls.Add(picStatus);
            Controls.Add(lblStart);
            Controls.Add(lblRequestedBy);
            Controls.Add(lblLinkTitle);
            Margin = new System.Windows.Forms.Padding(0);
            Name = "BuildDetailControl";
            Size = new System.Drawing.Size(370, 53);
            ((System.ComponentModel.ISupportInitialize)picStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)imgErrors).EndInit();
            ((System.ComponentModel.ISupportInitialize)imgWarnings).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lblLinkTitle;
        private System.Windows.Forms.Label lblRequestedBy;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.PictureBox picStatus;
        private System.Windows.Forms.Panel panelLine;
        private System.Windows.Forms.Label lblErrors;
        private System.Windows.Forms.Label lblWarnings;
        private System.Windows.Forms.PictureBox imgErrors;
        private System.Windows.Forms.PictureBox imgWarnings;
        private System.Windows.Forms.ToolTip tipLink;
    }
}
