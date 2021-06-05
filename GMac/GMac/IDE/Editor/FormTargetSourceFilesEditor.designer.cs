using System.ComponentModel;
using System.Windows.Forms;

namespace GMac.IDE.Editor
{
    partial class FormTargetSourceFilesEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTargetSourceFilesEditor));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonDone = new System.Windows.Forms.Button();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.columnHeaderSerial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonAddFiles = new System.Windows.Forms.Button();
            this.buttonAddFilesInFolder = new System.Windows.Forms.Button();
            this.checkBoxSearchSubfolders = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFileSearchPattern = new System.Windows.Forms.TextBox();
            this.checkBoxGMacOny = new System.Windows.Forms.CheckBox();
            this.textBoxMaxFileSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonRemoveFiles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(647, 515);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(125, 35);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDone.Location = new System.Drawing.Point(516, 515);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(125, 35);
            this.buttonDone.TabIndex = 5;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // listViewFiles
            // 
            this.listViewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSerial,
            this.columnHeaderFile,
            this.columnHeaderFolder});
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.Location = new System.Drawing.Point(0, 0);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(783, 389);
            this.listViewFiles.TabIndex = 6;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderSerial
            // 
            this.columnHeaderSerial.Text = "#";
            // 
            // columnHeaderFile
            // 
            this.columnHeaderFile.Text = "File";
            // 
            // columnHeaderFolder
            // 
            this.columnHeaderFolder.Text = "Folder";
            // 
            // buttonAddFiles
            // 
            this.buttonAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddFiles.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonAddFiles.Location = new System.Drawing.Point(12, 515);
            this.buttonAddFiles.Name = "buttonAddFiles";
            this.buttonAddFiles.Size = new System.Drawing.Size(125, 35);
            this.buttonAddFiles.TabIndex = 7;
            this.buttonAddFiles.Text = "Add Files";
            this.buttonAddFiles.UseVisualStyleBackColor = true;
            this.buttonAddFiles.Click += new System.EventHandler(this.buttonAddFiles_Click);
            // 
            // buttonAddFilesInFolder
            // 
            this.buttonAddFilesInFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddFilesInFolder.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonAddFilesInFolder.Location = new System.Drawing.Point(143, 515);
            this.buttonAddFilesInFolder.Name = "buttonAddFilesInFolder";
            this.buttonAddFilesInFolder.Size = new System.Drawing.Size(162, 35);
            this.buttonAddFilesInFolder.TabIndex = 8;
            this.buttonAddFilesInFolder.Text = "Add Files in Folder";
            this.buttonAddFilesInFolder.UseVisualStyleBackColor = true;
            this.buttonAddFilesInFolder.Click += new System.EventHandler(this.buttonAddFilesInFolder_Click);
            // 
            // checkBoxSearchSubfolders
            // 
            this.checkBoxSearchSubfolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSearchSubfolders.AutoSize = true;
            this.checkBoxSearchSubfolders.Location = new System.Drawing.Point(311, 522);
            this.checkBoxSearchSubfolders.Name = "checkBoxSearchSubfolders";
            this.checkBoxSearchSubfolders.Size = new System.Drawing.Size(182, 22);
            this.checkBoxSearchSubfolders.TabIndex = 9;
            this.checkBoxSearchSubfolders.Text = "Search All Subfolders";
            this.checkBoxSearchSubfolders.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 401);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "File Search Pattern:";
            // 
            // textBoxFileSearchPattern
            // 
            this.textBoxFileSearchPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFileSearchPattern.Location = new System.Drawing.Point(172, 398);
            this.textBoxFileSearchPattern.Name = "textBoxFileSearchPattern";
            this.textBoxFileSearchPattern.Size = new System.Drawing.Size(133, 26);
            this.textBoxFileSearchPattern.TabIndex = 11;
            this.textBoxFileSearchPattern.Text = "*.*";
            // 
            // checkBoxGMacOny
            // 
            this.checkBoxGMacOny.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxGMacOny.AutoSize = true;
            this.checkBoxGMacOny.Location = new System.Drawing.Point(172, 462);
            this.checkBoxGMacOny.Name = "checkBoxGMacOny";
            this.checkBoxGMacOny.Size = new System.Drawing.Size(367, 22);
            this.checkBoxGMacOny.TabIndex = 12;
            this.checkBoxGMacOny.Text = "Only Add Files Containing GMac Binding Points";
            this.checkBoxGMacOny.UseVisualStyleBackColor = true;
            // 
            // textBoxMaxFileSize
            // 
            this.textBoxMaxFileSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxMaxFileSize.Location = new System.Drawing.Point(172, 430);
            this.textBoxMaxFileSize.Name = "textBoxMaxFileSize";
            this.textBoxMaxFileSize.Size = new System.Drawing.Size(133, 26);
            this.textBoxMaxFileSize.TabIndex = 14;
            this.textBoxMaxFileSize.Text = "1024";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 433);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 18);
            this.label2.TabIndex = 13;
            this.label2.Text = "Maximum File Size:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(311, 433);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 18);
            this.label3.TabIndex = 15;
            this.label3.Text = "KBytes";
            // 
            // buttonRemoveFiles
            // 
            this.buttonRemoveFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemoveFiles.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonRemoveFiles.Location = new System.Drawing.Point(610, 398);
            this.buttonRemoveFiles.Name = "buttonRemoveFiles";
            this.buttonRemoveFiles.Size = new System.Drawing.Size(162, 35);
            this.buttonRemoveFiles.TabIndex = 16;
            this.buttonRemoveFiles.Text = "Remove Selected";
            this.buttonRemoveFiles.UseVisualStyleBackColor = true;
            this.buttonRemoveFiles.Click += new System.EventHandler(this.buttonRemoveFiles_Click);
            // 
            // FormTargetSourceFilesEditor
            // 
            this.AcceptButton = this.buttonDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.ControlBox = false;
            this.Controls.Add(this.buttonRemoveFiles);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxMaxFileSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxGMacOny);
            this.Controls.Add(this.textBoxFileSearchPattern);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxSearchSubfolders);
            this.Controls.Add(this.buttonAddFilesInFolder);
            this.Controls.Add(this.buttonAddFiles);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormTargetSourceFilesEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Target Source Files List";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buttonCancel;
        private FolderBrowserDialog folderBrowserDialog1;
        private OpenFileDialog openFileDialog1;
        private Button buttonDone;
        private ListView listViewFiles;
        private Button buttonAddFiles;
        private Button buttonAddFilesInFolder;
        private CheckBox checkBoxSearchSubfolders;
        private Label label1;
        private TextBox textBoxFileSearchPattern;
        private CheckBox checkBoxGMacOny;
        private TextBox textBoxMaxFileSize;
        private Label label2;
        private Label label3;
        private ColumnHeader columnHeaderSerial;
        private ColumnHeader columnHeaderFolder;
        private ColumnHeader columnHeaderFile;
        private Button buttonRemoveFiles;
    }
}