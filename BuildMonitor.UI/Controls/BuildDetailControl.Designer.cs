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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLinkTitle
            // 
            this.lblLinkTitle.AutoSize = true;
            this.lblLinkTitle.Location = new System.Drawing.Point(48, 5);
            this.lblLinkTitle.Name = "lblLinkTitle";
            this.lblLinkTitle.Size = new System.Drawing.Size(57, 13);
            this.lblLinkTitle.TabIndex = 0;
            this.lblLinkTitle.TabStop = true;
            this.lblLinkTitle.Text = "lblLinkTitle";
            this.lblLinkTitle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLinkTitle_LinkClicked);
            // 
            // lblRequestedBy
            // 
            this.lblRequestedBy.AutoSize = true;
            this.lblRequestedBy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRequestedBy.Location = new System.Drawing.Point(55, 27);
            this.lblRequestedBy.Name = "lblRequestedBy";
            this.lblRequestedBy.Size = new System.Drawing.Size(81, 13);
            this.lblRequestedBy.TabIndex = 1;
            this.lblRequestedBy.Text = "lblRequestedBy";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStart.Location = new System.Drawing.Point(167, 27);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(39, 13);
            this.lblStart.TabIndex = 2;
            this.lblStart.Text = "lblStart";
            // 
            // picStatus
            // 
            this.picStatus.Location = new System.Drawing.Point(4, 4);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(40, 40);
            this.picStatus.TabIndex = 3;
            this.picStatus.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(227, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(20, 1);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Visible = false;
            // 
            // BuildDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.picStatus);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.lblRequestedBy);
            this.Controls.Add(this.lblLinkTitle);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BuildDetailControl";
            this.Size = new System.Drawing.Size(257, 48);
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lblLinkTitle;
        private System.Windows.Forms.Label lblRequestedBy;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.PictureBox picStatus;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
