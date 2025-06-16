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
            txtAdoOrganisation = new System.Windows.Forms.TextBox();
            btnOk = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            tabControl = new System.Windows.Forms.TabControl();
            tabWindows = new System.Windows.Forms.TabPage();
            cbStartup = new System.Windows.Forms.CheckBox();
            tabGeneral = new System.Windows.Forms.TabPage();
            cbOrderByMostRecent = new System.Windows.Forms.CheckBox();
            numStaleDays = new System.Windows.Forms.NumericUpDown();
            numDefinitionInterval = new System.Windows.Forms.NumericUpDown();
            numInterval = new System.Windows.Forms.NumericUpDown();
            label7 = new System.Windows.Forms.Label();
            cbHideStale = new System.Windows.Forms.CheckBox();
            label6 = new System.Windows.Forms.Label();
            cbRefreshDefinitions = new System.Windows.Forms.CheckBox();
            cbIncludeRunning = new System.Windows.Forms.CheckBox();
            tabADO = new System.Windows.Forms.TabPage();
            lblLinkPat = new System.Windows.Forms.LinkLabel();
            label8 = new System.Windows.Forms.Label();
            imgBox = new System.Windows.Forms.PictureBox();
            btnValidate = new System.Windows.Forms.Button();
            cboAdoProjectName = new System.Windows.Forms.ComboBox();
            label5 = new System.Windows.Forms.Label();
            txtAdoPat = new System.Windows.Forms.TextBox();
            btnReset = new System.Windows.Forms.Button();
            label4 = new System.Windows.Forms.Label();
            txtPipelines = new System.Windows.Forms.TextBox();
            btnPipelines = new System.Windows.Forms.Button();
            tabControl.SuspendLayout();
            tabWindows.SuspendLayout();
            tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numStaleDays).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDefinitionInterval).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numInterval).BeginInit();
            tabADO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imgBox).BeginInit();
            SuspendLayout();
            // 
            // txtAdoOrganisation
            // 
            txtAdoOrganisation.Location = new System.Drawing.Point(321, 18);
            txtAdoOrganisation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtAdoOrganisation.Name = "txtAdoOrganisation";
            txtAdoOrganisation.Size = new System.Drawing.Size(178, 23);
            txtAdoOrganisation.TabIndex = 0;
            txtAdoOrganisation.TextChanged += txtAdoOrganisation_TextChanged;
            // 
            // btnOk
            // 
            btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnOk.Location = new System.Drawing.Point(435, 292);
            btnOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(88, 27);
            btnOk.TabIndex = 6;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(530, 292);
            btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(88, 27);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(86, 21);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(75, 15);
            label1.TabIndex = 3;
            label1.Text = "Organisation";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(80, 142);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(79, 15);
            label2.TabIndex = 5;
            label2.Text = "Project Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(20, 20);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(92, 15);
            label3.TabIndex = 7;
            label3.Text = "Refresh seconds";
            // 
            // tabControl
            // 
            tabControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl.Controls.Add(tabWindows);
            tabControl.Controls.Add(tabGeneral);
            tabControl.Controls.Add(tabADO);
            tabControl.Location = new System.Drawing.Point(14, 14);
            tabControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(603, 271);
            tabControl.TabIndex = 0;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // tabWindows
            // 
            tabWindows.Controls.Add(cbStartup);
            tabWindows.Location = new System.Drawing.Point(4, 24);
            tabWindows.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabWindows.Name = "tabWindows";
            tabWindows.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabWindows.Size = new System.Drawing.Size(595, 243);
            tabWindows.TabIndex = 2;
            tabWindows.Text = "Windows Preferences";
            tabWindows.UseVisualStyleBackColor = true;
            // 
            // cbStartup
            // 
            cbStartup.AutoSize = true;
            cbStartup.Location = new System.Drawing.Point(23, 21);
            cbStartup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbStartup.Name = "cbStartup";
            cbStartup.Size = new System.Drawing.Size(221, 19);
            cbStartup.TabIndex = 7;
            cbStartup.Text = "Run automatically on Windows login";
            cbStartup.UseVisualStyleBackColor = true;
            // 
            // tabGeneral
            // 
            tabGeneral.Controls.Add(cbOrderByMostRecent);
            tabGeneral.Controls.Add(numStaleDays);
            tabGeneral.Controls.Add(numDefinitionInterval);
            tabGeneral.Controls.Add(numInterval);
            tabGeneral.Controls.Add(label7);
            tabGeneral.Controls.Add(cbHideStale);
            tabGeneral.Controls.Add(label6);
            tabGeneral.Controls.Add(cbRefreshDefinitions);
            tabGeneral.Controls.Add(cbIncludeRunning);
            tabGeneral.Controls.Add(label3);
            tabGeneral.Location = new System.Drawing.Point(4, 24);
            tabGeneral.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabGeneral.Name = "tabGeneral";
            tabGeneral.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabGeneral.Size = new System.Drawing.Size(595, 243);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text = "General Settings";
            tabGeneral.UseVisualStyleBackColor = true;
            // 
            // cbOrderByMostRecent
            // 
            cbOrderByMostRecent.AutoSize = true;
            cbOrderByMostRecent.Location = new System.Drawing.Point(341, 19);
            cbOrderByMostRecent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbOrderByMostRecent.Name = "cbOrderByMostRecent";
            cbOrderByMostRecent.Size = new System.Drawing.Size(197, 19);
            cbOrderByMostRecent.TabIndex = 19;
            cbOrderByMostRecent.Text = "Order definitions by most recent";
            cbOrderByMostRecent.UseVisualStyleBackColor = true;
            // 
            // numStaleDays
            // 
            numStaleDays.Location = new System.Drawing.Point(218, 158);
            numStaleDays.Maximum = new decimal(new int[] { 365, 0, 0, 0 });
            numStaleDays.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numStaleDays.Name = "numStaleDays";
            numStaleDays.Size = new System.Drawing.Size(53, 23);
            numStaleDays.TabIndex = 18;
            numStaleDays.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numDefinitionInterval
            // 
            numDefinitionInterval.Location = new System.Drawing.Point(218, 102);
            numDefinitionInterval.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numDefinitionInterval.Minimum = new decimal(new int[] { 15, 0, 0, 0 });
            numDefinitionInterval.Name = "numDefinitionInterval";
            numDefinitionInterval.Size = new System.Drawing.Size(53, 23);
            numDefinitionInterval.TabIndex = 17;
            numDefinitionInterval.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // numInterval
            // 
            numInterval.Location = new System.Drawing.Point(137, 18);
            numInterval.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numInterval.Minimum = new decimal(new int[] { 15, 0, 0, 0 });
            numInterval.Name = "numInterval";
            numInterval.Size = new System.Drawing.Size(53, 23);
            numInterval.TabIndex = 16;
            numInterval.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(52, 160);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(89, 15);
            label7.TabIndex = 14;
            label7.Text = "Stale build days";
            // 
            // cbHideStale
            // 
            cbHideStale.AutoSize = true;
            cbHideStale.Location = new System.Drawing.Point(23, 133);
            cbHideStale.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbHideStale.Name = "cbHideStale";
            cbHideStale.Size = new System.Drawing.Size(167, 19);
            cbHideStale.TabIndex = 13;
            cbHideStale.Text = "Hide stale build definitions";
            cbHideStale.UseVisualStyleBackColor = true;
            cbHideStale.CheckedChanged += cbHideStale_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(52, 104);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(146, 15);
            label6.TabIndex = 11;
            label6.Text = "Refresh definition seconds";
            // 
            // cbRefreshDefinitions
            // 
            cbRefreshDefinitions.AutoSize = true;
            cbRefreshDefinitions.Location = new System.Drawing.Point(23, 76);
            cbRefreshDefinitions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbRefreshDefinitions.Name = "cbRefreshDefinitions";
            cbRefreshDefinitions.Size = new System.Drawing.Size(179, 19);
            cbRefreshDefinitions.TabIndex = 10;
            cbRefreshDefinitions.Text = "Also refresh list of definitions";
            cbRefreshDefinitions.UseVisualStyleBackColor = true;
            cbRefreshDefinitions.CheckedChanged += cbRefreshDefinitions_CheckedChanged;
            // 
            // cbIncludeRunning
            // 
            cbIncludeRunning.AutoSize = true;
            cbIncludeRunning.Location = new System.Drawing.Point(23, 46);
            cbIncludeRunning.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbIncludeRunning.Name = "cbIncludeRunning";
            cbIncludeRunning.Size = new System.Drawing.Size(238, 19);
            cbIncludeRunning.TabIndex = 9;
            cbIncludeRunning.Text = "Include monitoring of builds in progress";
            cbIncludeRunning.UseVisualStyleBackColor = true;
            // 
            // tabADO
            // 
            tabADO.Controls.Add(btnPipelines);
            tabADO.Controls.Add(txtPipelines);
            tabADO.Controls.Add(label4);
            tabADO.Controls.Add(lblLinkPat);
            tabADO.Controls.Add(label8);
            tabADO.Controls.Add(imgBox);
            tabADO.Controls.Add(btnValidate);
            tabADO.Controls.Add(cboAdoProjectName);
            tabADO.Controls.Add(label5);
            tabADO.Controls.Add(txtAdoPat);
            tabADO.Controls.Add(label1);
            tabADO.Controls.Add(txtAdoOrganisation);
            tabADO.Controls.Add(label2);
            tabADO.Location = new System.Drawing.Point(4, 24);
            tabADO.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabADO.Name = "tabADO";
            tabADO.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabADO.Size = new System.Drawing.Size(595, 243);
            tabADO.TabIndex = 1;
            tabADO.Text = "Azure DevOps";
            tabADO.UseVisualStyleBackColor = true;
            // 
            // lblLinkPat
            // 
            lblLinkPat.AutoSize = true;
            lblLinkPat.Enabled = false;
            lblLinkPat.Location = new System.Drawing.Point(498, 79);
            lblLinkPat.Name = "lblLinkPat";
            lblLinkPat.Size = new System.Drawing.Size(77, 15);
            lblLinkPat.TabIndex = 16;
            lblLinkPat.TabStop = true;
            lblLinkPat.Text = "Manage PATs";
            lblLinkPat.LinkClicked += lblLinkPat_LinkClicked;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(180, 21);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(129, 15);
            label8.TabIndex = 15;
            label8.Text = "https://dev.azure.com/";
            // 
            // imgBox
            // 
            imgBox.Location = new System.Drawing.Point(350, 91);
            imgBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            imgBox.Name = "imgBox";
            imgBox.Size = new System.Drawing.Size(28, 28);
            imgBox.TabIndex = 14;
            imgBox.TabStop = false;
            // 
            // btnValidate
            // 
            btnValidate.Location = new System.Drawing.Point(180, 92);
            btnValidate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnValidate.Name = "btnValidate";
            btnValidate.Size = new System.Drawing.Size(163, 27);
            btnValidate.TabIndex = 13;
            btnValidate.Text = "Validate";
            btnValidate.UseVisualStyleBackColor = true;
            btnValidate.Click += btnValidate_Click;
            // 
            // cboAdoProjectName
            // 
            cboAdoProjectName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboAdoProjectName.Location = new System.Drawing.Point(180, 139);
            cboAdoProjectName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cboAdoProjectName.Name = "cboAdoProjectName";
            cboAdoProjectName.Size = new System.Drawing.Size(236, 23);
            cboAdoProjectName.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(33, 56);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(126, 15);
            label5.TabIndex = 11;
            label5.Text = "Personal Access Token";
            // 
            // txtAdoPat
            // 
            txtAdoPat.Location = new System.Drawing.Point(180, 53);
            txtAdoPat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtAdoPat.Name = "txtAdoPat";
            txtAdoPat.PasswordChar = '*';
            txtAdoPat.Size = new System.Drawing.Size(395, 23);
            txtAdoPat.TabIndex = 5;
            // 
            // btnReset
            // 
            btnReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnReset.Location = new System.Drawing.Point(13, 293);
            btnReset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnReset.Name = "btnReset";
            btnReset.Size = new System.Drawing.Size(88, 27);
            btnReset.TabIndex = 8;
            btnReset.Text = "Reset All";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(105, 187);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(54, 15);
            label4.TabIndex = 17;
            label4.Text = "Pipelines";
            // 
            // txtPipelines
            // 
            txtPipelines.Location = new System.Drawing.Point(180, 184);
            txtPipelines.Name = "txtPipelines";
            txtPipelines.ReadOnly = true;
            txtPipelines.Size = new System.Drawing.Size(319, 23);
            txtPipelines.TabIndex = 18;
            // 
            // btnPipelines
            // 
            btnPipelines.Location = new System.Drawing.Point(498, 184);
            btnPipelines.Name = "btnPipelines";
            btnPipelines.Size = new System.Drawing.Size(40, 23);
            btnPipelines.TabIndex = 19;
            btnPipelines.Text = "...";
            btnPipelines.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(631, 332);
            ControlBox = false;
            Controls.Add(btnReset);
            Controls.Add(tabControl);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SettingsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Monitor Settings";
            tabControl.ResumeLayout(false);
            tabWindows.ResumeLayout(false);
            tabWindows.PerformLayout();
            tabGeneral.ResumeLayout(false);
            tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numStaleDays).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDefinitionInterval).EndInit();
            ((System.ComponentModel.ISupportInitialize)numInterval).EndInit();
            tabADO.ResumeLayout(false);
            tabADO.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)imgBox).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtAdoOrganisation;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabADO;
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbHideStale;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.LinkLabel lblLinkPat;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.NumericUpDown numStaleDays;
        private System.Windows.Forms.NumericUpDown numDefinitionInterval;
        private System.Windows.Forms.CheckBox cbOrderByMostRecent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPipelines;
        private System.Windows.Forms.Button btnPipelines;
    }
}