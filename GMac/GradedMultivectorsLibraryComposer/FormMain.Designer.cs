namespace GradedMultivectorsLibraryComposer
{
    partial class FormMain
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.textBoxGMacDslCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxTargetLanguage = new System.Windows.Forms.ComboBox();
            this.buttonComposeCode = new System.Windows.Forms.Button();
            this.checkBoxGenerateMacroCode = new System.Windows.Forms.CheckBox();
            this.comboBoxGMacDslCode = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 420);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(624, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "Ready";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(609, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Ready";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Output Folder:";
            this.label1.Visible = false;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(537, 12);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 27);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Visible = false;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxOutputFolder
            // 
            this.textBoxOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputFolder.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOutputFolder.Location = new System.Drawing.Point(148, 12);
            this.textBoxOutputFolder.Name = "textBoxOutputFolder";
            this.textBoxOutputFolder.Size = new System.Drawing.Size(383, 26);
            this.textBoxOutputFolder.TabIndex = 3;
            this.textBoxOutputFolder.Visible = false;
            // 
            // textBoxGMacDslCode
            // 
            this.textBoxGMacDslCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGMacDslCode.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGMacDslCode.HideSelection = false;
            this.textBoxGMacDslCode.Location = new System.Drawing.Point(12, 76);
            this.textBoxGMacDslCode.Multiline = true;
            this.textBoxGMacDslCode.Name = "textBoxGMacDslCode";
            this.textBoxGMacDslCode.ReadOnly = true;
            this.textBoxGMacDslCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxGMacDslCode.Size = new System.Drawing.Size(600, 308);
            this.textBoxGMacDslCode.TabIndex = 5;
            this.textBoxGMacDslCode.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "GMacDSL Code:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 394);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Target Language:";
            // 
            // comboBoxTargetLanguage
            // 
            this.comboBoxTargetLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxTargetLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTargetLanguage.FormattingEnabled = true;
            this.comboBoxTargetLanguage.Location = new System.Drawing.Point(148, 391);
            this.comboBoxTargetLanguage.Name = "comboBoxTargetLanguage";
            this.comboBoxTargetLanguage.Size = new System.Drawing.Size(267, 26);
            this.comboBoxTargetLanguage.TabIndex = 7;
            // 
            // buttonComposeCode
            // 
            this.buttonComposeCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonComposeCode.Location = new System.Drawing.Point(421, 390);
            this.buttonComposeCode.Name = "buttonComposeCode";
            this.buttonComposeCode.Size = new System.Drawing.Size(191, 27);
            this.buttonComposeCode.TabIndex = 8;
            this.buttonComposeCode.Text = "Compose Code";
            this.buttonComposeCode.UseVisualStyleBackColor = true;
            this.buttonComposeCode.Click += new System.EventHandler(this.buttonComposeCode_Click);
            // 
            // checkBoxGenerateMacroCode
            // 
            this.checkBoxGenerateMacroCode.AutoSize = true;
            this.checkBoxGenerateMacroCode.Location = new System.Drawing.Point(421, 46);
            this.checkBoxGenerateMacroCode.Name = "checkBoxGenerateMacroCode";
            this.checkBoxGenerateMacroCode.Size = new System.Drawing.Size(191, 22);
            this.checkBoxGenerateMacroCode.TabIndex = 9;
            this.checkBoxGenerateMacroCode.Text = "Generate Macro Code";
            this.checkBoxGenerateMacroCode.UseVisualStyleBackColor = true;
            // 
            // comboBoxGMacDslCode
            // 
            this.comboBoxGMacDslCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGMacDslCode.FormattingEnabled = true;
            this.comboBoxGMacDslCode.Location = new System.Drawing.Point(148, 44);
            this.comboBoxGMacDslCode.Name = "comboBoxGMacDslCode";
            this.comboBoxGMacDslCode.Size = new System.Drawing.Size(267, 26);
            this.comboBoxGMacDslCode.TabIndex = 10;
            this.comboBoxGMacDslCode.SelectedIndexChanged += new System.EventHandler(this.comboBoxGMacDslCode_SelectedIndexChanged);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select Output Folder";
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.comboBoxGMacDslCode);
            this.Controls.Add(this.checkBoxGenerateMacroCode);
            this.Controls.Add(this.buttonComposeCode);
            this.Controls.Add(this.comboBoxTargetLanguage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxGMacDslCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxOutputFolder);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormMain";
            this.Text = "Blades Library Composer";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxOutputFolder;
        private System.Windows.Forms.TextBox textBoxGMacDslCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxTargetLanguage;
        private System.Windows.Forms.Button buttonComposeCode;
        private System.Windows.Forms.CheckBox checkBoxGenerateMacroCode;
        private System.Windows.Forms.ComboBox comboBoxGMacDslCode;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

