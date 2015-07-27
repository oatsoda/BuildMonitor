namespace BuildMonitor.UI.Controls
{
    partial class PopupStatusForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buildDetailControl = new BuildMonitor.UI.Controls.BuildDetailControl();
            this.SuspendLayout();
            // 
            // buildDetailControl
            // 
            this.buildDetailControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.buildDetailControl.Location = new System.Drawing.Point(0, 0);
            this.buildDetailControl.Margin = new System.Windows.Forms.Padding(0);
            this.buildDetailControl.Name = "buildDetailControl";
            this.buildDetailControl.Size = new System.Drawing.Size(257, 38);
            this.buildDetailControl.TabIndex = 0;
            // 
            // PopupStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 38);
            this.ControlBox = false;
            this.Controls.Add(this.buildDetailControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PopupStatusForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "PopupStatusForm";
            this.ResumeLayout(false);

        }

        #endregion

        private BuildDetailControl buildDetailControl;

    }
}