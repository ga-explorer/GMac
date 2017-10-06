using System.ComponentModel;
using System.Windows.Forms;

namespace UtilLib.FolderTree.Viewer
{
    partial class FormPrdefinedGroupings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.buttonAddRootFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRootFolderPath = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.listBoxGroupings = new System.Windows.Forms.ListBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonCreateGrouping = new System.Windows.Forms.Button();
            this.buttonReadHierarchy = new System.Windows.Forms.Button();
            this.checkBoxReadFiles = new System.Windows.Forms.CheckBox();
            this.checkBoxReadFolders = new System.Windows.Forms.CheckBox();
            this.textBoxFilesPattern = new System.Windows.Forms.TextBox();
            this.textBoxFoldersPattern = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxGroupingDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAddRootFolder
            // 
            this.buttonAddRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddRootFolder.Location = new System.Drawing.Point(482, 10);
            this.buttonAddRootFolder.Name = "buttonAddRootFolder";
            this.buttonAddRootFolder.Size = new System.Drawing.Size(130, 28);
            this.buttonAddRootFolder.TabIndex = 10;
            this.buttonAddRootFolder.Text = "Add Folder";
            this.buttonAddRootFolder.UseVisualStyleBackColor = true;
            this.buttonAddRootFolder.Click += new System.EventHandler(this.buttonAddRootFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "Root Folders:";
            // 
            // textBoxRootFolderPath
            // 
            this.textBoxRootFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRootFolderPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRootFolderPath.Location = new System.Drawing.Point(0, 46);
            this.textBoxRootFolderPath.Multiline = true;
            this.textBoxRootFolderPath.Name = "textBoxRootFolderPath";
            this.textBoxRootFolderPath.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxRootFolderPath.Size = new System.Drawing.Size(624, 136);
            this.textBoxRootFolderPath.TabIndex = 8;
            this.textBoxRootFolderPath.WordWrap = false;
            this.textBoxRootFolderPath.TextChanged += new System.EventHandler(this.textBoxRootFolderPath_TextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 420);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(624, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // listBoxGroupings
            // 
            this.listBoxGroupings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxGroupings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxGroupings.FormattingEnabled = true;
            this.listBoxGroupings.IntegralHeight = false;
            this.listBoxGroupings.ItemHeight = 19;
            this.listBoxGroupings.Location = new System.Drawing.Point(0, 279);
            this.listBoxGroupings.Name = "listBoxGroupings";
            this.listBoxGroupings.Size = new System.Drawing.Size(298, 100);
            this.listBoxGroupings.TabIndex = 12;
            this.listBoxGroupings.SelectedIndexChanged += new System.EventHandler(this.listBoxGroupings_SelectedIndexChanged);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(482, 385);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(130, 28);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonCreateGrouping
            // 
            this.buttonCreateGrouping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreateGrouping.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCreateGrouping.Location = new System.Drawing.Point(482, 245);
            this.buttonCreateGrouping.Name = "buttonCreateGrouping";
            this.buttonCreateGrouping.Size = new System.Drawing.Size(130, 28);
            this.buttonCreateGrouping.TabIndex = 14;
            this.buttonCreateGrouping.Text = "Apply Grouping";
            this.buttonCreateGrouping.UseVisualStyleBackColor = true;
            this.buttonCreateGrouping.Click += new System.EventHandler(this.buttonCreateGrouping_Click);
            // 
            // buttonReadHierarchy
            // 
            this.buttonReadHierarchy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReadHierarchy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonReadHierarchy.Location = new System.Drawing.Point(482, 188);
            this.buttonReadHierarchy.Name = "buttonReadHierarchy";
            this.buttonReadHierarchy.Size = new System.Drawing.Size(130, 28);
            this.buttonReadHierarchy.TabIndex = 15;
            this.buttonReadHierarchy.Text = "Read Hierarchy";
            this.buttonReadHierarchy.UseVisualStyleBackColor = true;
            this.buttonReadHierarchy.Click += new System.EventHandler(this.buttonReadHierarchy_Click);
            // 
            // checkBoxReadFiles
            // 
            this.checkBoxReadFiles.AutoSize = true;
            this.checkBoxReadFiles.Checked = true;
            this.checkBoxReadFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReadFiles.Location = new System.Drawing.Point(8, 188);
            this.checkBoxReadFiles.Name = "checkBoxReadFiles";
            this.checkBoxReadFiles.Size = new System.Drawing.Size(103, 23);
            this.checkBoxReadFiles.TabIndex = 16;
            this.checkBoxReadFiles.Text = "Read Files:";
            this.checkBoxReadFiles.UseVisualStyleBackColor = true;
            // 
            // checkBoxReadFolders
            // 
            this.checkBoxReadFolders.AutoSize = true;
            this.checkBoxReadFolders.Checked = true;
            this.checkBoxReadFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReadFolders.Location = new System.Drawing.Point(8, 216);
            this.checkBoxReadFolders.Name = "checkBoxReadFolders";
            this.checkBoxReadFolders.Size = new System.Drawing.Size(123, 23);
            this.checkBoxReadFolders.TabIndex = 17;
            this.checkBoxReadFolders.Text = "Read Folders:";
            this.checkBoxReadFolders.UseVisualStyleBackColor = true;
            // 
            // textBoxFilesPattern
            // 
            this.textBoxFilesPattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxFilesPattern.Location = new System.Drawing.Point(137, 188);
            this.textBoxFilesPattern.Name = "textBoxFilesPattern";
            this.textBoxFilesPattern.Size = new System.Drawing.Size(161, 26);
            this.textBoxFilesPattern.TabIndex = 19;
            this.textBoxFilesPattern.Text = "*.*";
            // 
            // textBoxFoldersPattern
            // 
            this.textBoxFoldersPattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxFoldersPattern.Location = new System.Drawing.Point(137, 215);
            this.textBoxFoldersPattern.Name = "textBoxFoldersPattern";
            this.textBoxFoldersPattern.Size = new System.Drawing.Size(161, 26);
            this.textBoxFoldersPattern.TabIndex = 20;
            this.textBoxFoldersPattern.Text = "*.*";
            // 
            // textBoxGroupingDescription
            // 
            this.textBoxGroupingDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGroupingDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxGroupingDescription.Location = new System.Drawing.Point(304, 279);
            this.textBoxGroupingDescription.Multiline = true;
            this.textBoxGroupingDescription.Name = "textBoxGroupingDescription";
            this.textBoxGroupingDescription.ReadOnly = true;
            this.textBoxGroupingDescription.Size = new System.Drawing.Size(320, 100);
            this.textBoxGroupingDescription.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 22;
            this.label2.Text = "Groupings:";
            // 
            // formPrdefinedGroupings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.textBoxFoldersPattern);
            this.Controls.Add(this.textBoxFilesPattern);
            this.Controls.Add(this.checkBoxReadFolders);
            this.Controls.Add(this.checkBoxReadFiles);
            this.Controls.Add(this.buttonReadHierarchy);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonAddRootFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxRootFolderPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxGroupingDescription);
            this.Controls.Add(this.buttonCreateGrouping);
            this.Controls.Add(this.listBoxGroupings);
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormPrdefinedGroupings";
            this.Text = "Prdefined Groupings";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buttonAddRootFolder;
        private Label label1;
        private TextBox textBoxRootFolderPath;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ListBox listBoxGroupings;
        private Button buttonClose;
        private Button buttonCreateGrouping;
        private Button buttonReadHierarchy;
        private CheckBox checkBoxReadFiles;
        private CheckBox checkBoxReadFolders;
        private TextBox textBoxFilesPattern;
        private TextBox textBoxFoldersPattern;
        private FolderBrowserDialog folderBrowserDialog1;
        private TextBox textBoxGroupingDescription;
        private Label label2;
    }
}