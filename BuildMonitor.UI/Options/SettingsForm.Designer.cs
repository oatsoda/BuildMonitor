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
            this.txtAdoOrganisation = new System.Windows.Forms.TextBox();
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
            this.label7 = new System.Windows.Forms.Label();
            this.txtStaleDays = new System.Windows.Forms.TextBox();
            this.cbHideStale = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDefinitionInterval = new System.Windows.Forms.TextBox();
            this.cbRefreshDefinitions = new System.Windows.Forms.CheckBox();
            this.cbIncludeRunning = new System.Windows.Forms.CheckBox();
            this.tabTfs = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.imgBox = new System.Windows.Forms.PictureBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.cboAdoProjectName = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAdoPat = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabWindows.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabTfs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAdoOrganisation
            // 
            this.txtAdoOrganisation.Location = new System.Drawing.Point(289, 15);
            this.txtAdoOrganisation.Name = "txtAdoOrganisation";
            this.txtAdoOrganisation.Size = new System.Drawing.Size(153, 20);
            this.txtAdoOrganisation.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(373, 254);
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
            this.btnCancel.Location = new System.Drawing.Point(454, 254);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Organisation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Project Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Refresh seconds";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(115, 14);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(36, 20);
            this.txtInterval.TabIndex = 8;
            this.txtInterval.Text = "60";
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
            this.tabControl.Size = new System.Drawing.Size(517, 236);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabWindows
            // 
            this.tabWindows.Controls.Add(this.cbStartup);
            this.tabWindows.Location = new System.Drawing.Point(4, 22);
            this.tabWindows.Name = "tabWindows";
            this.tabWindows.Padding = new System.Windows.Forms.Padding(3);
            this.tabWindows.Size = new System.Drawing.Size(509, 210);
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
            this.tabGeneral.Controls.Add(this.label7);
            this.tabGeneral.Controls.Add(this.txtStaleDays);
            this.tabGeneral.Controls.Add(this.cbHideStale);
            this.tabGeneral.Controls.Add(this.label6);
            this.tabGeneral.Controls.Add(this.txtDefinitionInterval);
            this.tabGeneral.Controls.Add(this.cbRefreshDefinitions);
            this.tabGeneral.Controls.Add(this.cbIncludeRunning);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Controls.Add(this.txtInterval);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(509, 210);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General Settings";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Stale build days";
            // 
            // txtStaleDays
            // 
            this.txtStaleDays.Location = new System.Drawing.Point(187, 136);
            this.txtStaleDays.Name = "txtStaleDays";
            this.txtStaleDays.Size = new System.Drawing.Size(36, 20);
            this.txtStaleDays.TabIndex = 15;
            this.txtStaleDays.Text = "30";
            // 
            // cbHideStale
            // 
            this.cbHideStale.AutoSize = true;
            this.cbHideStale.Checked = true;
            this.cbHideStale.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHideStale.Location = new System.Drawing.Point(20, 115);
            this.cbHideStale.Name = "cbHideStale";
            this.cbHideStale.Size = new System.Drawing.Size(148, 17);
            this.cbHideStale.TabIndex = 13;
            this.cbHideStale.Text = "Hide stale build definitions";
            this.cbHideStale.UseVisualStyleBackColor = true;
            this.cbHideStale.CheckedChanged += new System.EventHandler(this.cbHideStale_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Refresh definition seconds";
            // 
            // txtDefinitionInterval
            // 
            this.txtDefinitionInterval.Location = new System.Drawing.Point(187, 87);
            this.txtDefinitionInterval.Name = "txtDefinitionInterval";
            this.txtDefinitionInterval.Size = new System.Drawing.Size(36, 20);
            this.txtDefinitionInterval.TabIndex = 12;
            this.txtDefinitionInterval.Text = "3600";
            // 
            // cbRefreshDefinitions
            // 
            this.cbRefreshDefinitions.AutoSize = true;
            this.cbRefreshDefinitions.Location = new System.Drawing.Point(20, 66);
            this.cbRefreshDefinitions.Name = "cbRefreshDefinitions";
            this.cbRefreshDefinitions.Size = new System.Drawing.Size(158, 17);
            this.cbRefreshDefinitions.TabIndex = 10;
            this.cbRefreshDefinitions.Text = "Also refresh list of definitions";
            this.cbRefreshDefinitions.UseVisualStyleBackColor = true;
            this.cbRefreshDefinitions.CheckedChanged += new System.EventHandler(this.cbRefreshDefinitions_CheckedChanged);
            // 
            // cbIncludeRunning
            // 
            this.cbIncludeRunning.AutoSize = true;
            this.cbIncludeRunning.Location = new System.Drawing.Point(20, 40);
            this.cbIncludeRunning.Name = "cbIncludeRunning";
            this.cbIncludeRunning.Size = new System.Drawing.Size(208, 17);
            this.cbIncludeRunning.TabIndex = 9;
            this.cbIncludeRunning.Text = "Include monitoring of builds in progress";
            this.cbIncludeRunning.UseVisualStyleBackColor = true;
            // 
            // tabTfs
            // 
            this.tabTfs.Controls.Add(this.label8);
            this.tabTfs.Controls.Add(this.imgBox);
            this.tabTfs.Controls.Add(this.btnValidate);
            this.tabTfs.Controls.Add(this.cboAdoProjectName);
            this.tabTfs.Controls.Add(this.label5);
            this.tabTfs.Controls.Add(this.txtAdoPat);
            this.tabTfs.Controls.Add(this.label1);
            this.tabTfs.Controls.Add(this.txtAdoOrganisation);
            this.tabTfs.Controls.Add(this.label2);
            this.tabTfs.Location = new System.Drawing.Point(4, 22);
            this.tabTfs.Name = "tabTfs";
            this.tabTfs.Padding = new System.Windows.Forms.Padding(3);
            this.tabTfs.Size = new System.Drawing.Size(509, 210);
            this.tabTfs.TabIndex = 1;
            this.tabTfs.Text = "Azure DevOps";
            this.tabTfs.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(165, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "https://dev.azure.com/";
            // 
            // imgBox
            // 
            this.imgBox.Location = new System.Drawing.Point(314, 78);
            this.imgBox.Name = "imgBox";
            this.imgBox.Size = new System.Drawing.Size(24, 24);
            this.imgBox.TabIndex = 14;
            this.imgBox.TabStop = false;
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(168, 79);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(140, 23);
            this.btnValidate.TabIndex = 13;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // cboAdoProjectName
            // 
            this.cboAdoProjectName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAdoProjectName.Location = new System.Drawing.Point(168, 120);
            this.cboAdoProjectName.Name = "cboAdoProjectName";
            this.cboAdoProjectName.Size = new System.Drawing.Size(203, 21);
            this.cboAdoProjectName.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Personal Access Token";
            // 
            // txtAdoPat
            // 
            this.txtAdoPat.Location = new System.Drawing.Point(168, 45);
            this.txtAdoPat.Name = "txtAdoPat";
            this.txtAdoPat.PasswordChar = '*';
            this.txtAdoPat.Size = new System.Drawing.Size(188, 20);
            this.txtAdoPat.TabIndex = 5;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 289);
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

        private System.Windows.Forms.TextBox txtAdoOrganisation;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabTfs;
        private System.Windows.Forms.TextBox txtAdoPat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabWindows;
        private System.Windows.Forms.CheckBox cbStartup;
        private System.Windows.Forms.CheckBox cbIncludeRunning;
        private System.Windows.Forms.ComboBox cboAdoProjectName;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.PictureBox imgBox;
        private System.Windows.Forms.CheckBox cbRefreshDefinitions;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDefinitionInterval;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtStaleDays;
        private System.Windows.Forms.CheckBox cbHideStale;
        private System.Windows.Forms.Label label8;
    }
}