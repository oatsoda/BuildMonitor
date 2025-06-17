namespace BuildMonitor.UI.Options
{
    partial class PipelineSelectorForm
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
            btnCancel = new System.Windows.Forms.Button();
            btnOk = new System.Windows.Forms.Button();
            lvPipelines = new System.Windows.Forms.ListView();
            colName = new System.Windows.Forms.ColumnHeader();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(210, 424);
            btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(88, 27);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnOk.Location = new System.Drawing.Point(115, 424);
            btnOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(88, 27);
            btnOk.TabIndex = 8;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // lvPipelines
            // 
            lvPipelines.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lvPipelines.CheckBoxes = true;
            lvPipelines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { colName });
            lvPipelines.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            lvPipelines.HideSelection = true;
            lvPipelines.Location = new System.Drawing.Point(10, 12);
            lvPipelines.Name = "lvPipelines";
            lvPipelines.Size = new System.Drawing.Size(288, 406);
            lvPipelines.TabIndex = 11;
            lvPipelines.UseCompatibleStateImageBehavior = false;
            lvPipelines.View = System.Windows.Forms.View.Details;
            lvPipelines.ItemSelectionChanged += lvPipelines_ItemSelectionChanged;
            // 
            // colName
            // 
            colName.Text = "Pipeline";
            // 
            // PipelineSelectorForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(311, 463);
            Controls.Add(lvPipelines);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PipelineSelectorForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Filter Pipelines";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListView lvPipelines;
        private System.Windows.Forms.ColumnHeader colName;
    }
}