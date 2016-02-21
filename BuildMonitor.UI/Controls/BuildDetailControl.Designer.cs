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
            this.lblLinkTitle = new System.Windows.Forms.LinkLabel();
            this.lblRequestedBy = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.panelLine = new System.Windows.Forms.Panel();
            this.lblErrors = new System.Windows.Forms.Label();
            this.lblWarnings = new System.Windows.Forms.Label();
            this.imgErrors = new System.Windows.Forms.PictureBox();
            this.imgWarnings = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgWarnings)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLinkTitle
            // 
            this.lblLinkTitle.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblLinkTitle.AutoSize = true;
            this.lblLinkTitle.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblLinkTitle.Location = new System.Drawing.Point(32, 3);
            this.lblLinkTitle.Name = "lblLinkTitle";
            this.lblLinkTitle.Size = new System.Drawing.Size(57, 13);
            this.lblLinkTitle.TabIndex = 0;
            this.lblLinkTitle.TabStop = true;
            this.lblLinkTitle.Text = "lblLinkTitle";
            this.lblLinkTitle.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblLinkTitle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLinkTitle_LinkClicked);
            // 
            // lblRequestedBy
            // 
            this.lblRequestedBy.AutoSize = true;
            this.lblRequestedBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.lblRequestedBy.Location = new System.Drawing.Point(36, 20);
            this.lblRequestedBy.Name = "lblRequestedBy";
            this.lblRequestedBy.Size = new System.Drawing.Size(81, 13);
            this.lblRequestedBy.TabIndex = 1;
            this.lblRequestedBy.Text = "lblRequestedBy";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.lblStart.Location = new System.Drawing.Point(164, 20);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(39, 13);
            this.lblStart.TabIndex = 2;
            this.lblStart.Text = "lblStart";
            // 
            // picStatus
            // 
            this.picStatus.Location = new System.Drawing.Point(4, 4);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(24, 24);
            this.picStatus.TabIndex = 3;
            this.picStatus.TabStop = false;
            // 
            // panelLine
            // 
            this.panelLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelLine.Location = new System.Drawing.Point(227, 9);
            this.panelLine.Name = "panelLine";
            this.panelLine.Size = new System.Drawing.Size(20, 1);
            this.panelLine.TabIndex = 4;
            this.panelLine.Visible = false;
            // 
            // lblErrors
            // 
            this.lblErrors.AutoSize = true;
            this.lblErrors.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.lblErrors.Location = new System.Drawing.Point(191, 3);
            this.lblErrors.Name = "lblErrors";
            this.lblErrors.Size = new System.Drawing.Size(13, 13);
            this.lblErrors.TabIndex = 5;
            this.lblErrors.Text = "0";
            this.lblErrors.Visible = false;
            // 
            // lblWarnings
            // 
            this.lblWarnings.AutoSize = true;
            this.lblWarnings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.lblWarnings.Location = new System.Drawing.Point(229, 3);
            this.lblWarnings.Name = "lblWarnings";
            this.lblWarnings.Size = new System.Drawing.Size(13, 13);
            this.lblWarnings.TabIndex = 6;
            this.lblWarnings.Text = "0";
            this.lblWarnings.Visible = false;
            // 
            // imgErrors
            // 
            this.imgErrors.Location = new System.Drawing.Point(179, 3);
            this.imgErrors.Name = "imgErrors";
            this.imgErrors.Size = new System.Drawing.Size(12, 12);
            this.imgErrors.TabIndex = 7;
            this.imgErrors.TabStop = false;
            this.imgErrors.Visible = false;
            // 
            // imgWarnings
            // 
            this.imgWarnings.Location = new System.Drawing.Point(217, 3);
            this.imgWarnings.Name = "imgWarnings";
            this.imgWarnings.Size = new System.Drawing.Size(12, 12);
            this.imgWarnings.TabIndex = 8;
            this.imgWarnings.TabStop = false;
            this.imgWarnings.Visible = false;
            // 
            // BuildDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.Controls.Add(this.imgWarnings);
            this.Controls.Add(this.imgErrors);
            this.Controls.Add(this.lblWarnings);
            this.Controls.Add(this.lblErrors);
            this.Controls.Add(this.panelLine);
            this.Controls.Add(this.picStatus);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.lblRequestedBy);
            this.Controls.Add(this.lblLinkTitle);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BuildDetailControl";
            this.Size = new System.Drawing.Size(257, 38);
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgWarnings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}
