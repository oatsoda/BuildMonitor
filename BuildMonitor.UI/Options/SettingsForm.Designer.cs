namespace BuildMonitor.UI.Options
{
    partial class SettingsForm
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
            this.txtTfsApiUrl = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabWindows = new System.Windows.Forms.TabPage();
            this.cbStartup = new System.Windows.Forms.CheckBox();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.cbIncludeRunning = new System.Windows.Forms.CheckBox();
            this.tabTfs = new System.Windows.Forms.TabPage();
            this.imgBox = new System.Windows.Forms.PictureBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.cboTfsProjectName = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.rdoSpecify = new System.Windows.Forms.RadioButton();
            this.rdoAuthIntegrated = new System.Windows.Forms.RadioButton();
            this.tabControl.SuspendLayout();
            this.tabWindows.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabTfs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTfsApiUrl
            // 
            this.txtTfsApiUrl.Location = new System.Drawing.Point(113, 15);
            this.txtTfsApiUrl.Name = "txtTfsApiUrl";
            this.txtTfsApiUrl.Size = new System.Drawing.Size(380, 20);
            this.txtTfsApiUrl.TabIndex = 0;
            this.txtTfsApiUrl.Text = "https://voy-devtfs2.visualstudio.com/DefaultCollection/_apis/";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(373, 268);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(454, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tfs Api Url";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Project Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Refresh Seconds";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(115, 45);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(36, 20);
            this.txtInterval.TabIndex = 8;
            this.txtInterval.Text = "30";
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabWindows);
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabTfs);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(517, 250);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabWindows
            // 
            this.tabWindows.Controls.Add(this.cbStartup);
            this.tabWindows.Location = new System.Drawing.Point(4, 22);
            this.tabWindows.Name = "tabWindows";
            this.tabWindows.Padding = new System.Windows.Forms.Padding(3);
            this.tabWindows.Size = new System.Drawing.Size(509, 224);
            this.tabWindows.TabIndex = 2;
            this.tabWindows.Text = "Windows Preferences";
            this.tabWindows.UseVisualStyleBackColor = true;
            // 
            // cbStartup
            // 
            this.cbStartup.AutoSize = true;
            this.cbStartup.Location = new System.Drawing.Point(20, 18);
            this.cbStartup.Name = "cbStartup";
            this.cbStartup.Size = new System.Drawing.Size(197, 17);
            this.cbStartup.TabIndex = 7;
            this.cbStartup.Text = "Run automatically on Windows login";
            this.cbStartup.UseVisualStyleBackColor = true;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.cbIncludeRunning);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Controls.Add(this.txtInterval);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(509, 224);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General Settings";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // cbIncludeRunning
            // 
            this.cbIncludeRunning.AutoSize = true;
            this.cbIncludeRunning.Location = new System.Drawing.Point(20, 18);
            this.cbIncludeRunning.Name = "cbIncludeRunning";
            this.cbIncludeRunning.Size = new System.Drawing.Size(208, 17);
            this.cbIncludeRunning.TabIndex = 9;
            this.cbIncludeRunning.Text = "Include monitoring of builds in progress";
            this.cbIncludeRunning.UseVisualStyleBackColor = true;
            // 
            // tabTfs
            // 
            this.tabTfs.Controls.Add(this.imgBox);
            this.tabTfs.Controls.Add(this.btnValidate);
            this.tabTfs.Controls.Add(this.cboTfsProjectName);
            this.tabTfs.Controls.Add(this.label5);
            this.tabTfs.Controls.Add(this.label4);
            this.tabTfs.Controls.Add(this.txtPassword);
            this.tabTfs.Controls.Add(this.txtUsername);
            this.tabTfs.Controls.Add(this.rdoSpecify);
            this.tabTfs.Controls.Add(this.rdoAuthIntegrated);
            this.tabTfs.Controls.Add(this.label1);
            this.tabTfs.Controls.Add(this.txtTfsApiUrl);
            this.tabTfs.Controls.Add(this.label2);
            this.tabTfs.Location = new System.Drawing.Point(4, 22);
            this.tabTfs.Name = "tabTfs";
            this.tabTfs.Padding = new System.Windows.Forms.Padding(3);
            this.tabTfs.Size = new System.Drawing.Size(509, 224);
            this.tabTfs.TabIndex = 1;
            this.tabTfs.Text = "TFS";
            this.tabTfs.UseVisualStyleBackColor = true;
            // 
            // imgBox
            // 
            this.imgBox.Location = new System.Drawing.Point(129, 138);
            this.imgBox.Name = "imgBox";
            this.imgBox.Size = new System.Drawing.Size(24, 24);
            this.imgBox.TabIndex = 14;
            this.imgBox.TabStop = false;
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(171, 138);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(140, 23);
            this.btnValidate.TabIndex = 13;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // cboTfsProjectName
            // 
            this.cboTfsProjectName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTfsProjectName.Location = new System.Drawing.Point(113, 180);
            this.cboTfsProjectName.Name = "cboTfsProjectName";
            this.cboTfsProjectName.Size = new System.Drawing.Size(203, 21);
            this.cboTfsProjectName.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(218, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(218, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Username";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(279, 102);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(188, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(279, 76);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(188, 20);
            this.txtUsername.TabIndex = 4;
            // 
            // rdoSpecify
            // 
            this.rdoSpecify.AutoSize = true;
            this.rdoSpecify.Checked = true;
            this.rdoSpecify.Location = new System.Drawing.Point(37, 77);
            this.rdoSpecify.Name = "rdoSpecify";
            this.rdoSpecify.Size = new System.Drawing.Size(118, 17);
            this.rdoSpecify.TabIndex = 3;
            this.rdoSpecify.TabStop = true;
            this.rdoSpecify.Text = "Specific Credentials";
            this.rdoSpecify.UseVisualStyleBackColor = true;
            this.rdoSpecify.CheckedChanged += new System.EventHandler(this.rdoSpecify_CheckedChanged);
            // 
            // rdoAuthIntegrated
            // 
            this.rdoAuthIntegrated.AutoSize = true;
            this.rdoAuthIntegrated.Location = new System.Drawing.Point(37, 53);
            this.rdoAuthIntegrated.Name = "rdoAuthIntegrated";
            this.rdoAuthIntegrated.Size = new System.Drawing.Size(140, 17);
            this.rdoAuthIntegrated.TabIndex = 2;
            this.rdoAuthIntegrated.Text = "Windows Authentication";
            this.rdoAuthIntegrated.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 303);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitor Settings";
            this.tabControl.ResumeLayout(false);
            this.tabWindows.ResumeLayout(false);
            this.tabWindows.PerformLayout();
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabTfs.ResumeLayout(false);
            this.tabTfs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtTfsApiUrl;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabTfs;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.RadioButton rdoSpecify;
        private System.Windows.Forms.RadioButton rdoAuthIntegrated;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabWindows;
        private System.Windows.Forms.CheckBox cbStartup;
        private System.Windows.Forms.CheckBox cbIncludeRunning;
        private System.Windows.Forms.ComboBox cboTfsProjectName;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.PictureBox imgBox;
    }
}