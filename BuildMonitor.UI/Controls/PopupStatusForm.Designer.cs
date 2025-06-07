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
            buildDetailControl = new BuildDetailControl();
            SuspendLayout();
            // 
            // buildDetailControl
            // 
            buildDetailControl.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
            buildDetailControl.Location = new System.Drawing.Point(0, 0);
            buildDetailControl.Margin = new System.Windows.Forms.Padding(0);
            buildDetailControl.Name = "buildDetailControl";
            buildDetailControl.Size = new System.Drawing.Size(300, 44);
            buildDetailControl.TabIndex = 0;
            // 
            // PopupStatusForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(300, 44);
            ControlBox = false;
            Controls.Add(buildDetailControl);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "PopupStatusForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "PopupStatusForm";
            TopMost = true;
            ResumeLayout(false);

        }

        #endregion

        private BuildDetailControl buildDetailControl;

    }
}