namespace cuiliangbjgmailcom.AutoResx
{
    partial class OptionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.ddlProjects = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlFileCount = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numWaitForView = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numWaitForCmd = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numWaitForSave = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.chkRemoveOldTags = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitForView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitForCmd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitForSave)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Target project:";
            // 
            // ddlProjects
            // 
            this.ddlProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProjects.FormattingEnabled = true;
            this.ddlProjects.Location = new System.Drawing.Point(15, 29);
            this.ddlProjects.Name = "ddlProjects";
            this.ddlProjects.Size = new System.Drawing.Size(267, 20);
            this.ddlProjects.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(299, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Max file count to process(small number for test):";
            // 
            // ddlFileCount
            // 
            this.ddlFileCount.FormattingEnabled = true;
            this.ddlFileCount.Items.AddRange(new object[] {
            "ALL",
            "1",
            "2",
            "5",
            "10"});
            this.ddlFileCount.Location = new System.Drawing.Point(15, 77);
            this.ddlFileCount.Name = "ddlFileCount";
            this.ddlFileCount.Size = new System.Drawing.Size(69, 20);
            this.ddlFileCount.TabIndex = 3;
            this.ddlFileCount.Text = "2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Wait for design view ready:";
            // 
            // numWaitForView
            // 
            this.numWaitForView.Location = new System.Drawing.Point(15, 125);
            this.numWaitForView.Name = "numWaitForView";
            this.numWaitForView.Size = new System.Drawing.Size(69, 21);
            this.numWaitForView.TabIndex = 5;
            this.numWaitForView.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(89, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Seconds";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(317, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "Wait for GenerateLocalResource cmd if not available:";
            // 
            // numWaitForCmd
            // 
            this.numWaitForCmd.Location = new System.Drawing.Point(15, 173);
            this.numWaitForCmd.Name = "numWaitForCmd";
            this.numWaitForCmd.Size = new System.Drawing.Size(69, 21);
            this.numWaitForCmd.TabIndex = 5;
            this.numWaitForCmd.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(89, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Seconds";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 204);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "Wait for saving files:";
            // 
            // numWaitForSave
            // 
            this.numWaitForSave.Location = new System.Drawing.Point(15, 219);
            this.numWaitForSave.Name = "numWaitForSave";
            this.numWaitForSave.Size = new System.Drawing.Size(69, 21);
            this.numWaitForSave.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(89, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "Seconds";
            // 
            // chkRemoveOldTags
            // 
            this.chkRemoveOldTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRemoveOldTags.AutoSize = true;
            this.chkRemoveOldTags.Location = new System.Drawing.Point(15, 257);
            this.chkRemoveOldTags.Name = "chkRemoveOldTags";
            this.chkRemoveOldTags.Size = new System.Drawing.Size(282, 16);
            this.chkRemoveOldTags.TabIndex = 7;
            this.chkRemoveOldTags.Text = "Clear old \"meta:res...\" tags and .resx file\r\n";
            this.chkRemoveOldTags.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(69, 290);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 30);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(204, 290);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // OptionForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 335);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkRemoveOldTags);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numWaitForSave);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numWaitForCmd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numWaitForView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ddlFileCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ddlProjects);
            this.Controls.Add(this.label1);
            this.Name = "OptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoResx Options";
            this.Load += new System.EventHandler(this.OptionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numWaitForView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitForCmd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitForSave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlProjects;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlFileCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numWaitForView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numWaitForCmd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numWaitForSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkRemoveOldTags;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}