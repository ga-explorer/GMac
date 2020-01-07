using System.ComponentModel;
using System.Windows.Forms;

namespace UtilLib.FolderTree.Viewer
{
    partial class FormGroupsDictionaryViewer
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonRefreshList = new System.Windows.Forms.Button();
            this.listBoxItems = new System.Windows.Forms.ListBox();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.checkBoxShowParentFolders = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCopyItems = new System.Windows.Forms.Button();
            this.checkBoxShowFolders = new System.Windows.Forms.CheckBox();
            this.checkBoxShowFiles = new System.Windows.Forms.CheckBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxItems = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 420);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(624, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(304, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Ready";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(304, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "0 Group(s), 0 File(s), 0 Folder(s)";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonRefreshList);
            this.splitContainer1.Panel1.Controls.Add(this.listBoxItems);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxFilter);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxShowParentFolders);
            this.splitContainer1.Panel2.Controls.Add(this.buttonSave);
            this.splitContainer1.Panel2.Controls.Add(this.buttonCopyItems);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxShowFolders);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxShowFiles);
            this.splitContainer1.Panel2.Controls.Add(this.buttonClose);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxItems);
            this.splitContainer1.Size = new System.Drawing.Size(624, 420);
            this.splitContainer1.SplitterDistance = 251;
            this.splitContainer1.TabIndex = 1;
            // 
            // buttonRefreshList
            // 
            this.buttonRefreshList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefreshList.Location = new System.Drawing.Point(150, 389);
            this.buttonRefreshList.Name = "buttonRefreshList";
            this.buttonRefreshList.Size = new System.Drawing.Size(98, 28);
            this.buttonRefreshList.TabIndex = 3;
            this.buttonRefreshList.Text = "&Refresh";
            this.buttonRefreshList.UseVisualStyleBackColor = true;
            this.buttonRefreshList.Click += new System.EventHandler(this.buttonRefreshList_Click);
            // 
            // listBoxItems
            // 
            this.listBoxItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxItems.FormattingEnabled = true;
            this.listBoxItems.HorizontalScrollbar = true;
            this.listBoxItems.IntegralHeight = false;
            this.listBoxItems.ItemHeight = 19;
            this.listBoxItems.Location = new System.Drawing.Point(3, 3);
            this.listBoxItems.Name = "listBoxItems";
            this.listBoxItems.ScrollAlwaysVisible = true;
            this.listBoxItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxItems.Size = new System.Drawing.Size(245, 346);
            this.listBoxItems.TabIndex = 2;
            this.listBoxItems.SelectedIndexChanged += new System.EventHandler(this.listBoxItems_SelectedIndexChanged);
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxFilter.Location = new System.Drawing.Point(3, 355);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(245, 26);
            this.textBoxFilter.TabIndex = 1;
            // 
            // checkBoxShowParentFolders
            // 
            this.checkBoxShowParentFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxShowParentFolders.AutoSize = true;
            this.checkBoxShowParentFolders.Location = new System.Drawing.Point(164, 326);
            this.checkBoxShowParentFolders.Name = "checkBoxShowParentFolders";
            this.checkBoxShowParentFolders.Size = new System.Drawing.Size(172, 23);
            this.checkBoxShowParentFolders.TabIndex = 10;
            this.checkBoxShowParentFolders.Text = "Show P&arent Folders";
            this.checkBoxShowParentFolders.UseVisualStyleBackColor = true;
            this.checkBoxShowParentFolders.CheckedChanged += new System.EventHandler(this.checkBoxShowParentFolders_CheckedChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(60, 389);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(98, 28);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "S&ave";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCopyItems
            // 
            this.buttonCopyItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopyItems.Location = new System.Drawing.Point(164, 389);
            this.buttonCopyItems.Name = "buttonCopyItems";
            this.buttonCopyItems.Size = new System.Drawing.Size(98, 28);
            this.buttonCopyItems.TabIndex = 8;
            this.buttonCopyItems.Text = "Co&py";
            this.buttonCopyItems.UseVisualStyleBackColor = true;
            this.buttonCopyItems.Click += new System.EventHandler(this.buttonCopyItems_Click);
            // 
            // checkBoxShowFolders
            // 
            this.checkBoxShowFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxShowFolders.AutoSize = true;
            this.checkBoxShowFolders.Location = new System.Drawing.Point(3, 355);
            this.checkBoxShowFolders.Name = "checkBoxShowFolders";
            this.checkBoxShowFolders.Size = new System.Drawing.Size(122, 23);
            this.checkBoxShowFolders.TabIndex = 6;
            this.checkBoxShowFolders.Text = "Show F&olders";
            this.checkBoxShowFolders.UseVisualStyleBackColor = true;
            this.checkBoxShowFolders.CheckedChanged += new System.EventHandler(this.checkBoxShowFolders_CheckedChanged);
            // 
            // checkBoxShowFiles
            // 
            this.checkBoxShowFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxShowFiles.AutoSize = true;
            this.checkBoxShowFiles.Location = new System.Drawing.Point(3, 326);
            this.checkBoxShowFiles.Name = "checkBoxShowFiles";
            this.checkBoxShowFiles.Size = new System.Drawing.Size(102, 23);
            this.checkBoxShowFiles.TabIndex = 5;
            this.checkBoxShowFiles.Text = "Show F&iles";
            this.checkBoxShowFiles.UseVisualStyleBackColor = true;
            this.checkBoxShowFiles.CheckedChanged += new System.EventHandler(this.checkBoxShowFiles_CheckedChanged);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(268, 389);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(98, 28);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // textBoxItems
            // 
            this.textBoxItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxItems.Location = new System.Drawing.Point(3, 0);
            this.textBoxItems.MaxLength = 0;
            this.textBoxItems.Multiline = true;
            this.textBoxItems.Name = "textBoxItems";
            this.textBoxItems.ReadOnly = true;
            this.textBoxItems.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxItems.Size = new System.Drawing.Size(363, 320);
            this.textBoxItems.TabIndex = 7;
            this.textBoxItems.WordWrap = false;
            // 
            // formGroupsDictionaryViewer
            // 
            this.AcceptButton = this.buttonRefreshList;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormGroupsDictionaryViewer";
            this.Text = "formGroupsDictionaryViewer";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private SplitContainer splitContainer1;
        private TextBox textBoxFilter;
        private Button buttonRefreshList;
        private ListBox listBoxItems;
        private Button buttonClose;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private Button buttonCopyItems;
        private TextBox textBoxItems;
        private CheckBox checkBoxShowFolders;
        private CheckBox checkBoxShowFiles;
        private Button buttonSave;
        private SaveFileDialog saveFileDialog1;
        private CheckBox checkBoxShowParentFolders;
    }
}