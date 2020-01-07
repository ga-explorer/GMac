using System.ComponentModel;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace GMac.GMacIDE.Editor
{
    partial class FormGMacDslEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGMacDslEditor));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.SourceCodeTextEditor = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSourceFiles = new System.Windows.Forms.TabPage();
            this.listBoxSourceFiles = new System.Windows.Forms.ListBox();
            this.tabPageComponents = new System.Windows.Forms.TabPage();
            this.treeViewComponents = new System.Windows.Forms.TreeView();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.textBoxOutputLog = new System.Windows.Forms.TextBox();
            this.tabPageErrors = new System.Windows.Forms.TabPage();
            this.listViewErrors = new System.Windows.Forms.ListView();
            this.columnHeaderSerial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSourceFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageWarnings = new System.Windows.Forms.TabPage();
            this.listViewWarnings = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCodeLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_NewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_OpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_ManageSourceFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemFile_EditSourceFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_SaveSourceFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_CloseSourceFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCompile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCompile_CompileDSLCode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemCompile_CompilerOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTools_CodeGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTools_ExploreAST = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTools_ExploreMacro = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemTools_InteractiveScript = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTools_ExploreClasses = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp_AboutGMac = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SourceCodeTextEditor)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageSourceFiles.SuspendLayout();
            this.tabPageComponents.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.tabPageErrors.SuspendLayout();
            this.tabPageWarnings.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(783, 512);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Size = new System.Drawing.Size(783, 512);
            this.splitContainer1.SplitterDistance = 347;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.SourceCodeTextEditor);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(783, 347);
            this.splitContainer2.SplitterDistance = 524;
            this.splitContainer2.TabIndex = 0;
            // 
            // SourceCodeTextEditor
            // 
            this.SourceCodeTextEditor.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.SourceCodeTextEditor.AutoScrollMinSize = new System.Drawing.Size(27, 17);
            this.SourceCodeTextEditor.BackBrush = null;
            this.SourceCodeTextEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SourceCodeTextEditor.CharHeight = 17;
            this.SourceCodeTextEditor.CharWidth = 8;
            this.SourceCodeTextEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SourceCodeTextEditor.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.SourceCodeTextEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourceCodeTextEditor.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.SourceCodeTextEditor.IsReplaceMode = false;
            this.SourceCodeTextEditor.Location = new System.Drawing.Point(0, 0);
            this.SourceCodeTextEditor.Name = "SourceCodeTextEditor";
            this.SourceCodeTextEditor.Paddings = new System.Windows.Forms.Padding(0);
            this.SourceCodeTextEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.SourceCodeTextEditor.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("SourceCodeTextEditor.ServiceColors")));
            this.SourceCodeTextEditor.ShowFoldingLines = true;
            this.SourceCodeTextEditor.Size = new System.Drawing.Size(524, 347);
            this.SourceCodeTextEditor.TabIndex = 0;
            this.SourceCodeTextEditor.Zoom = 100;
            this.SourceCodeTextEditor.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.SourceCodeTextEditor_TextChanged);
            this.SourceCodeTextEditor.SelectionChanged += new System.EventHandler(this.SourceCodeTextEditor_SelectionChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageSourceFiles);
            this.tabControl1.Controls.Add(this.tabPageComponents);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(255, 347);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageSourceFiles
            // 
            this.tabPageSourceFiles.Controls.Add(this.listBoxSourceFiles);
            this.tabPageSourceFiles.Location = new System.Drawing.Point(4, 25);
            this.tabPageSourceFiles.Name = "tabPageSourceFiles";
            this.tabPageSourceFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSourceFiles.Size = new System.Drawing.Size(247, 318);
            this.tabPageSourceFiles.TabIndex = 1;
            this.tabPageSourceFiles.Text = "Source Files";
            this.tabPageSourceFiles.UseVisualStyleBackColor = true;
            // 
            // listBoxSourceFiles
            // 
            this.listBoxSourceFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSourceFiles.FormattingEnabled = true;
            this.listBoxSourceFiles.HorizontalScrollbar = true;
            this.listBoxSourceFiles.IntegralHeight = false;
            this.listBoxSourceFiles.ItemHeight = 16;
            this.listBoxSourceFiles.Location = new System.Drawing.Point(3, 3);
            this.listBoxSourceFiles.Name = "listBoxSourceFiles";
            this.listBoxSourceFiles.ScrollAlwaysVisible = true;
            this.listBoxSourceFiles.Size = new System.Drawing.Size(241, 312);
            this.listBoxSourceFiles.TabIndex = 0;
            this.listBoxSourceFiles.DoubleClick += new System.EventHandler(this.listBoxSourceFiles_DoubleClick);
            // 
            // tabPageComponents
            // 
            this.tabPageComponents.Controls.Add(this.treeViewComponents);
            this.tabPageComponents.Location = new System.Drawing.Point(4, 22);
            this.tabPageComponents.Name = "tabPageComponents";
            this.tabPageComponents.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageComponents.Size = new System.Drawing.Size(247, 321);
            this.tabPageComponents.TabIndex = 0;
            this.tabPageComponents.Text = "Components";
            this.tabPageComponents.UseVisualStyleBackColor = true;
            // 
            // treeViewComponents
            // 
            this.treeViewComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewComponents.Location = new System.Drawing.Point(3, 3);
            this.treeViewComponents.Name = "treeViewComponents";
            this.treeViewComponents.PathSeparator = ".";
            this.treeViewComponents.Size = new System.Drawing.Size(241, 315);
            this.treeViewComponents.TabIndex = 0;
            this.treeViewComponents.DoubleClick += new System.EventHandler(this.treeViewComponents_DoubleClick);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPageOutput);
            this.tabControl2.Controls.Add(this.tabPageErrors);
            this.tabControl2.Controls.Add(this.tabPageWarnings);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(783, 161);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.textBoxOutputLog);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 25);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(775, 132);
            this.tabPageOutput.TabIndex = 0;
            this.tabPageOutput.Text = "Output";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // textBoxOutputLog
            // 
            this.textBoxOutputLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOutputLog.Location = new System.Drawing.Point(3, 3);
            this.textBoxOutputLog.Multiline = true;
            this.textBoxOutputLog.Name = "textBoxOutputLog";
            this.textBoxOutputLog.ReadOnly = true;
            this.textBoxOutputLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxOutputLog.Size = new System.Drawing.Size(769, 126);
            this.textBoxOutputLog.TabIndex = 0;
            // 
            // tabPageErrors
            // 
            this.tabPageErrors.Controls.Add(this.listViewErrors);
            this.tabPageErrors.Location = new System.Drawing.Point(4, 22);
            this.tabPageErrors.Name = "tabPageErrors";
            this.tabPageErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageErrors.Size = new System.Drawing.Size(775, 135);
            this.tabPageErrors.TabIndex = 1;
            this.tabPageErrors.Text = "Errors";
            this.tabPageErrors.UseVisualStyleBackColor = true;
            // 
            // listViewErrors
            // 
            this.listViewErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSerial,
            this.columnHeaderSourceFile,
            this.columnHeaderLocation,
            this.columnHeaderDescription});
            this.listViewErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewErrors.FullRowSelect = true;
            this.listViewErrors.GridLines = true;
            this.listViewErrors.HideSelection = false;
            this.listViewErrors.LabelWrap = false;
            this.listViewErrors.Location = new System.Drawing.Point(3, 3);
            this.listViewErrors.MultiSelect = false;
            this.listViewErrors.Name = "listViewErrors";
            this.listViewErrors.Size = new System.Drawing.Size(769, 129);
            this.listViewErrors.TabIndex = 0;
            this.listViewErrors.UseCompatibleStateImageBehavior = false;
            this.listViewErrors.View = System.Windows.Forms.View.Details;
            this.listViewErrors.SelectedIndexChanged += new System.EventHandler(this.listViewErrors_SelectedIndexChanged);
            this.listViewErrors.DoubleClick += new System.EventHandler(this.listViewErrors_DoubleClick);
            // 
            // columnHeaderSerial
            // 
            this.columnHeaderSerial.Text = "#";
            // 
            // columnHeaderSourceFile
            // 
            this.columnHeaderSourceFile.Text = "Source File";
            this.columnHeaderSourceFile.Width = 92;
            // 
            // columnHeaderLocation
            // 
            this.columnHeaderLocation.Text = "Location";
            this.columnHeaderLocation.Width = 89;
            // 
            // columnHeaderDescription
            // 
            this.columnHeaderDescription.Text = "Description";
            this.columnHeaderDescription.Width = 361;
            // 
            // tabPageWarnings
            // 
            this.tabPageWarnings.Controls.Add(this.listViewWarnings);
            this.tabPageWarnings.Location = new System.Drawing.Point(4, 22);
            this.tabPageWarnings.Name = "tabPageWarnings";
            this.tabPageWarnings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWarnings.Size = new System.Drawing.Size(775, 135);
            this.tabPageWarnings.TabIndex = 2;
            this.tabPageWarnings.Text = "Warnings";
            this.tabPageWarnings.UseVisualStyleBackColor = true;
            // 
            // listViewWarnings
            // 
            this.listViewWarnings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewWarnings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewWarnings.FullRowSelect = true;
            this.listViewWarnings.GridLines = true;
            this.listViewWarnings.HideSelection = false;
            this.listViewWarnings.LabelWrap = false;
            this.listViewWarnings.Location = new System.Drawing.Point(3, 3);
            this.listViewWarnings.MultiSelect = false;
            this.listViewWarnings.Name = "listViewWarnings";
            this.listViewWarnings.Size = new System.Drawing.Size(769, 129);
            this.listViewWarnings.TabIndex = 1;
            this.listViewWarnings.UseCompatibleStateImageBehavior = false;
            this.listViewWarnings.View = System.Windows.Forms.View.Details;
            this.listViewWarnings.SelectedIndexChanged += new System.EventHandler(this.listViewWarnings_SelectedIndexChanged);
            this.listViewWarnings.DoubleClick += new System.EventHandler(this.listViewWarnings_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Source File";
            this.columnHeader2.Width = 92;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Location";
            this.columnHeader3.Width = 89;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Description";
            this.columnHeader4.Width = 361;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelCodeLocation});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(659, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Ready";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelCodeLocation
            // 
            this.toolStripStatusLabelCodeLocation.Name = "toolStripStatusLabelCodeLocation";
            this.toolStripStatusLabelCodeLocation.Size = new System.Drawing.Size(110, 17);
            this.toolStripStatusLabelCodeLocation.Text = "Line: 0, Character: 0";
            this.toolStripStatusLabelCodeLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Title = "Save File ...";
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemCompile,
            this.menuItemTools,
            this.menuItemHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(784, 24);
            this.menuStripMain.TabIndex = 2;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile_NewProject,
            this.menuItemFile_OpenProject,
            this.menuItemFile_ManageSourceFiles,
            this.toolStripMenuItem1,
            this.menuItemFile_EditSourceFile,
            this.menuItemFile_SaveSourceFile,
            this.menuItemFile_CloseSourceFile,
            this.toolStripSeparator1,
            this.menuItemFile_Exit});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(37, 20);
            this.menuItemFile.Text = "&File";
            // 
            // menuItemFile_NewProject
            // 
            this.menuItemFile_NewProject.Name = "menuItemFile_NewProject";
            this.menuItemFile_NewProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuItemFile_NewProject.Size = new System.Drawing.Size(198, 22);
            this.menuItemFile_NewProject.Text = "&New Project";
            this.menuItemFile_NewProject.Click += new System.EventHandler(this.menuItem_File_NewProject_Click);
            // 
            // menuItemFile_OpenProject
            // 
            this.menuItemFile_OpenProject.Name = "menuItemFile_OpenProject";
            this.menuItemFile_OpenProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuItemFile_OpenProject.Size = new System.Drawing.Size(198, 22);
            this.menuItemFile_OpenProject.Text = "&Open Project";
            this.menuItemFile_OpenProject.Click += new System.EventHandler(this.menuItem_File_OpenProject_Click);
            // 
            // menuItemFile_ManageSourceFiles
            // 
            this.menuItemFile_ManageSourceFiles.Name = "menuItemFile_ManageSourceFiles";
            this.menuItemFile_ManageSourceFiles.Size = new System.Drawing.Size(198, 22);
            this.menuItemFile_ManageSourceFiles.Text = "&Manage Source Files";
            this.menuItemFile_ManageSourceFiles.Click += new System.EventHandler(this.menuItem_File_ManageSourceFiles_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(195, 6);
            // 
            // menuItemFile_EditSourceFile
            // 
            this.menuItemFile_EditSourceFile.Name = "menuItemFile_EditSourceFile";
            this.menuItemFile_EditSourceFile.Size = new System.Drawing.Size(198, 22);
            this.menuItemFile_EditSourceFile.Text = "&Edit Source File";
            this.menuItemFile_EditSourceFile.Click += new System.EventHandler(this.menuItem_File_EditSourceFile_Click);
            // 
            // menuItemFile_SaveSourceFile
            // 
            this.menuItemFile_SaveSourceFile.Name = "menuItemFile_SaveSourceFile";
            this.menuItemFile_SaveSourceFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItemFile_SaveSourceFile.Size = new System.Drawing.Size(198, 22);
            this.menuItemFile_SaveSourceFile.Text = "&Save Source File";
            this.menuItemFile_SaveSourceFile.Click += new System.EventHandler(this.menuItem_File_SaveSourceFile_Click);
            // 
            // menuItemFile_CloseSourceFile
            // 
            this.menuItemFile_CloseSourceFile.Name = "menuItemFile_CloseSourceFile";
            this.menuItemFile_CloseSourceFile.Size = new System.Drawing.Size(198, 22);
            this.menuItemFile_CloseSourceFile.Text = "&Close Source File";
            this.menuItemFile_CloseSourceFile.Click += new System.EventHandler(this.menuItem_File_CloseSourceFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
            // 
            // menuItemFile_Exit
            // 
            this.menuItemFile_Exit.Name = "menuItemFile_Exit";
            this.menuItemFile_Exit.Size = new System.Drawing.Size(198, 22);
            this.menuItemFile_Exit.Text = "E&xit";
            this.menuItemFile_Exit.Click += new System.EventHandler(this.menuItem_File_Exit_Click);
            // 
            // menuItemCompile
            // 
            this.menuItemCompile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemCompile_CompileDSLCode,
            this.toolStripSeparator3,
            this.menuItemCompile_CompilerOptions});
            this.menuItemCompile.Name = "menuItemCompile";
            this.menuItemCompile.Size = new System.Drawing.Size(64, 20);
            this.menuItemCompile.Text = "&Compile";
            // 
            // menuItemCompile_CompileDSLCode
            // 
            this.menuItemCompile_CompileDSLCode.Name = "menuItemCompile_CompileDSLCode";
            this.menuItemCompile_CompileDSLCode.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.menuItemCompile_CompileDSLCode.Size = new System.Drawing.Size(250, 22);
            this.menuItemCompile_CompileDSLCode.Text = "&Compile GMacDSL Code";
            this.menuItemCompile_CompileDSLCode.Click += new System.EventHandler(this.menuItemCompile_CompileDSLCode_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(247, 6);
            // 
            // menuItemCompile_CompilerOptions
            // 
            this.menuItemCompile_CompilerOptions.Name = "menuItemCompile_CompilerOptions";
            this.menuItemCompile_CompilerOptions.Size = new System.Drawing.Size(250, 22);
            this.menuItemCompile_CompilerOptions.Text = "Compiler &Options...";
            this.menuItemCompile_CompilerOptions.Click += new System.EventHandler(this.menuItemCompile_CompilerOptions_Click);
            // 
            // menuItemTools
            // 
            this.menuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTools_CodeGenerator,
            this.menuItemTools_ExploreAST,
            this.menuItemTools_ExploreMacro,
            this.toolStripSeparator2,
            this.menuItemTools_InteractiveScript,
            this.menuItemTools_ExploreClasses});
            this.menuItemTools.Name = "menuItemTools";
            this.menuItemTools.Size = new System.Drawing.Size(48, 20);
            this.menuItemTools.Text = "&Tools";
            // 
            // menuItemTools_CodeGenerator
            // 
            this.menuItemTools_CodeGenerator.Name = "menuItemTools_CodeGenerator";
            this.menuItemTools_CodeGenerator.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.menuItemTools_CodeGenerator.Size = new System.Drawing.Size(244, 22);
            this.menuItemTools_CodeGenerator.Text = "Code &Generator";
            this.menuItemTools_CodeGenerator.Click += new System.EventHandler(this.menuItemTools_CodeGenerator_Click);
            // 
            // menuItemTools_ExploreAST
            // 
            this.menuItemTools_ExploreAST.Name = "menuItemTools_ExploreAST";
            this.menuItemTools_ExploreAST.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.menuItemTools_ExploreAST.Size = new System.Drawing.Size(244, 22);
            this.menuItemTools_ExploreAST.Text = "Explore GMac&AST";
            this.menuItemTools_ExploreAST.Click += new System.EventHandler(this.menuItemTools_ExploreAST_Click);
            // 
            // menuItemTools_ExploreMacro
            // 
            this.menuItemTools_ExploreMacro.Name = "menuItemTools_ExploreMacro";
            this.menuItemTools_ExploreMacro.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.menuItemTools_ExploreMacro.Size = new System.Drawing.Size(244, 22);
            this.menuItemTools_ExploreMacro.Text = "Explore &Macro";
            this.menuItemTools_ExploreMacro.Click += new System.EventHandler(this.menuItemTools_ExploreMacro_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(241, 6);
            // 
            // menuItemTools_InteractiveScript
            // 
            this.menuItemTools_InteractiveScript.Name = "menuItemTools_InteractiveScript";
            this.menuItemTools_InteractiveScript.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F6)));
            this.menuItemTools_InteractiveScript.Size = new System.Drawing.Size(244, 22);
            this.menuItemTools_InteractiveScript.Text = "Interactive &Script";
            this.menuItemTools_InteractiveScript.Click += new System.EventHandler(this.menuItemTools_InteractiveScript_Click);
            // 
            // menuItemTools_ExploreClasses
            // 
            this.menuItemTools_ExploreClasses.Name = "menuItemTools_ExploreClasses";
            this.menuItemTools_ExploreClasses.Size = new System.Drawing.Size(244, 22);
            this.menuItemTools_ExploreClasses.Text = "Explore Loaded .NET Assemblies";
            this.menuItemTools_ExploreClasses.Click += new System.EventHandler(this.menuItemTools_ExploreClasses_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemHelp_AboutGMac});
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(44, 20);
            this.menuItemHelp.Text = "&Help";
            // 
            // menuItemHelp_AboutGMac
            // 
            this.menuItemHelp_AboutGMac.Name = "menuItemHelp_AboutGMac";
            this.menuItemHelp_AboutGMac.Size = new System.Drawing.Size(141, 22);
            this.menuItemHelp_AboutGMac.Text = "About GMac";
            this.menuItemHelp_AboutGMac.Click += new System.EventHandler(this.menuItemHelp_AboutGMac_Click);
            // 
            // FormGMacDslEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStripMain;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormGMacDslEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GMacIDE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formDSLEditor_FormClosing);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SourceCodeTextEditor)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageSourceFiles.ResumeLayout(false);
            this.tabPageComponents.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageOutput.PerformLayout();
            this.tabPageErrors.ResumeLayout(false);
            this.tabPageWarnings.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private FastColoredTextBox SourceCodeTextEditor;
        private TabControl tabControl1;
        private TabPage tabPageComponents;
        private TabPage tabPageSourceFiles;
        private TabControl tabControl2;
        private TabPage tabPageOutput;
        private TabPage tabPageErrors;
        private TabPage tabPageWarnings;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabelCodeLocation;
        private TextBox textBoxOutputLog;
        private SaveFileDialog saveFileDialog1;
        private TreeView treeViewComponents;
        private ListBox listBoxSourceFiles;
        private ListView listViewErrors;
        private ColumnHeader columnHeaderSerial;
        private ColumnHeader columnHeaderSourceFile;
        private ColumnHeader columnHeaderLocation;
        private ColumnHeader columnHeaderDescription;
        private ListView listViewWarnings;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem menuItemFile;
        private ToolStripMenuItem menuItemFile_NewProject;
        private ToolStripMenuItem menuItemFile_OpenProject;
        private ToolStripMenuItem menuItemFile_ManageSourceFiles;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem menuItemFile_Exit;
        private ToolStripMenuItem menuItemTools;
        private ToolStripMenuItem menuItemFile_EditSourceFile;
        private ToolStripMenuItem menuItemFile_SaveSourceFile;
        private ToolStripMenuItem menuItemFile_CloseSourceFile;
        private ToolStripSeparator toolStripSeparator1;
        private OpenFileDialog openFileDialog1;
        private ToolStripMenuItem menuItemTools_ExploreAST;
        private ToolStripMenuItem menuItemTools_CodeGenerator;
        private ToolStripMenuItem menuItemTools_ExploreMacro;
        private ToolStripMenuItem menuItemTools_InteractiveScript;
        private ToolStripMenuItem menuItemCompile;
        private ToolStripMenuItem menuItemCompile_CompileDSLCode;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem menuItemCompile_CompilerOptions;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuItemTools_ExploreClasses;
        private ToolStripMenuItem menuItemHelp;
        private ToolStripMenuItem menuItemHelp_AboutGMac;
    }
}