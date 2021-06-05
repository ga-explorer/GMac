using System.ComponentModel;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace GMac.IDE.Scripting
{
    partial class FormInteractiveScript
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInteractiveScript));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControlUpper = new System.Windows.Forms.TabControl();
            this.tabPageScript = new System.Windows.Forms.TabPage();
            this.textBoxScript = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabPageMembers = new System.Windows.Forms.TabPage();
            this.textBoxMembers = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabPageNamespaces = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.buttonAddSelected = new System.Windows.Forms.Button();
            this.listBoxAllNamespaces = new System.Windows.Forms.ListBox();
            this.buttonRemoveNamespaces = new System.Windows.Forms.Button();
            this.listBoxUsedNamespaces = new System.Windows.Forms.ListBox();
            this.tabPageCSharpCode = new System.Windows.Forms.TabPage();
            this.textBoxCSharpCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.textBoxCommands = new FastColoredTextBoxNS.FastColoredTextBox();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.treeViewComponents = new System.Windows.Forms.TreeView();
            this.tabControlLower = new System.Windows.Forms.TabControl();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.tabPageCommands = new System.Windows.Forms.TabPage();
            this.tabPageErrors = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewErrors = new System.Windows.Forms.ListView();
            this.columnHeaderSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxErrorDetails = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_New = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFile_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemFile_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemScript = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemScript_Generate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemScript_Compile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemScript_Execute = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemView_Progress = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabPageMath = new System.Windows.Forms.TabPage();
            this.listBoxMath = new System.Windows.Forms.ListBox();
            this.statusStrip1.SuspendLayout();
            this.tabControlUpper.SuspendLayout();
            this.tabPageScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxScript)).BeginInit();
            this.tabPageMembers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxMembers)).BeginInit();
            this.tabPageNamespaces.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.tabPageCSharpCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCSharpCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCommands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tabControlLower.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.tabPageCommands.SuspendLayout();
            this.tabPageErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabPageMath.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(769, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Ready";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControlUpper
            // 
            this.tabControlUpper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlUpper.Controls.Add(this.tabPageScript);
            this.tabControlUpper.Controls.Add(this.tabPageMembers);
            this.tabControlUpper.Controls.Add(this.tabPageNamespaces);
            this.tabControlUpper.Controls.Add(this.tabPageCSharpCode);
            this.tabControlUpper.Location = new System.Drawing.Point(3, 3);
            this.tabControlUpper.Name = "tabControlUpper";
            this.tabControlUpper.SelectedIndex = 0;
            this.tabControlUpper.Size = new System.Drawing.Size(572, 321);
            this.tabControlUpper.TabIndex = 1;
            // 
            // tabPageScript
            // 
            this.tabPageScript.Controls.Add(this.textBoxScript);
            this.tabPageScript.Location = new System.Drawing.Point(4, 25);
            this.tabPageScript.Name = "tabPageScript";
            this.tabPageScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScript.Size = new System.Drawing.Size(564, 292);
            this.tabPageScript.TabIndex = 0;
            this.tabPageScript.Text = "Script";
            this.tabPageScript.UseVisualStyleBackColor = true;
            // 
            // textBoxScript
            // 
            this.textBoxScript.AutoCompleteBracketsList = new char[] {
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
            this.textBoxScript.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.textBoxScript.AutoScrollMinSize = new System.Drawing.Size(27, 17);
            this.textBoxScript.BackBrush = null;
            this.textBoxScript.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxScript.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.textBoxScript.CharHeight = 17;
            this.textBoxScript.CharWidth = 8;
            this.textBoxScript.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxScript.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textBoxScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxScript.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxScript.IsReplaceMode = false;
            this.textBoxScript.Language = FastColoredTextBoxNS.Language.CSharp;
            this.textBoxScript.LeftBracket = '(';
            this.textBoxScript.LeftBracket2 = '{';
            this.textBoxScript.Location = new System.Drawing.Point(3, 3);
            this.textBoxScript.Name = "textBoxScript";
            this.textBoxScript.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxScript.RightBracket = ')';
            this.textBoxScript.RightBracket2 = '}';
            this.textBoxScript.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textBoxScript.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBoxScript.ServiceColors")));
            this.textBoxScript.ShowFoldingLines = true;
            this.textBoxScript.Size = new System.Drawing.Size(558, 286);
            this.textBoxScript.TabIndex = 1;
            this.textBoxScript.Zoom = 100;
            this.textBoxScript.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.textBoxScript_TextChanged);
            // 
            // tabPageMembers
            // 
            this.tabPageMembers.Controls.Add(this.textBoxMembers);
            this.tabPageMembers.Location = new System.Drawing.Point(4, 22);
            this.tabPageMembers.Name = "tabPageMembers";
            this.tabPageMembers.Size = new System.Drawing.Size(564, 295);
            this.tabPageMembers.TabIndex = 5;
            this.tabPageMembers.Text = "Members";
            this.tabPageMembers.UseVisualStyleBackColor = true;
            // 
            // textBoxMembers
            // 
            this.textBoxMembers.AutoCompleteBracketsList = new char[] {
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
            this.textBoxMembers.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.textBoxMembers.AutoScrollMinSize = new System.Drawing.Size(2, 17);
            this.textBoxMembers.BackBrush = null;
            this.textBoxMembers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMembers.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.textBoxMembers.CharHeight = 17;
            this.textBoxMembers.CharWidth = 8;
            this.textBoxMembers.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxMembers.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textBoxMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMembers.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxMembers.IsReplaceMode = false;
            this.textBoxMembers.Language = FastColoredTextBoxNS.Language.CSharp;
            this.textBoxMembers.LeftBracket = '(';
            this.textBoxMembers.LeftBracket2 = '{';
            this.textBoxMembers.Location = new System.Drawing.Point(0, 0);
            this.textBoxMembers.Name = "textBoxMembers";
            this.textBoxMembers.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxMembers.RightBracket = ')';
            this.textBoxMembers.RightBracket2 = '}';
            this.textBoxMembers.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textBoxMembers.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBoxMembers.ServiceColors")));
            this.textBoxMembers.ShowFoldingLines = true;
            this.textBoxMembers.Size = new System.Drawing.Size(564, 295);
            this.textBoxMembers.TabIndex = 2;
            this.textBoxMembers.Zoom = 100;
            this.textBoxMembers.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.textBoxMembers_TextChanged);
            // 
            // tabPageNamespaces
            // 
            this.tabPageNamespaces.Controls.Add(this.splitContainer5);
            this.tabPageNamespaces.Location = new System.Drawing.Point(4, 22);
            this.tabPageNamespaces.Name = "tabPageNamespaces";
            this.tabPageNamespaces.Size = new System.Drawing.Size(564, 295);
            this.tabPageNamespaces.TabIndex = 4;
            this.tabPageNamespaces.Text = "Namespaces";
            this.tabPageNamespaces.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.buttonAddSelected);
            this.splitContainer5.Panel1.Controls.Add(this.listBoxAllNamespaces);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.buttonRemoveNamespaces);
            this.splitContainer5.Panel2.Controls.Add(this.listBoxUsedNamespaces);
            this.splitContainer5.Size = new System.Drawing.Size(564, 295);
            this.splitContainer5.SplitterDistance = 281;
            this.splitContainer5.TabIndex = 0;
            // 
            // buttonAddSelected
            // 
            this.buttonAddSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddSelected.Location = new System.Drawing.Point(8, 263);
            this.buttonAddSelected.Name = "buttonAddSelected";
            this.buttonAddSelected.Size = new System.Drawing.Size(270, 29);
            this.buttonAddSelected.TabIndex = 3;
            this.buttonAddSelected.Text = "&Use Selected Namespaces";
            this.buttonAddSelected.UseVisualStyleBackColor = true;
            this.buttonAddSelected.Click += new System.EventHandler(this.buttonAddSelected_Click);
            // 
            // listBoxAllNamespaces
            // 
            this.listBoxAllNamespaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxAllNamespaces.FormattingEnabled = true;
            this.listBoxAllNamespaces.IntegralHeight = false;
            this.listBoxAllNamespaces.ItemHeight = 16;
            this.listBoxAllNamespaces.Location = new System.Drawing.Point(5, 3);
            this.listBoxAllNamespaces.Name = "listBoxAllNamespaces";
            this.listBoxAllNamespaces.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxAllNamespaces.Size = new System.Drawing.Size(273, 254);
            this.listBoxAllNamespaces.Sorted = true;
            this.listBoxAllNamespaces.TabIndex = 1;
            // 
            // buttonRemoveNamespaces
            // 
            this.buttonRemoveNamespaces.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveNamespaces.Location = new System.Drawing.Point(3, 263);
            this.buttonRemoveNamespaces.Name = "buttonRemoveNamespaces";
            this.buttonRemoveNamespaces.Size = new System.Drawing.Size(270, 29);
            this.buttonRemoveNamespaces.TabIndex = 4;
            this.buttonRemoveNamespaces.Text = "&Remove Selected Namespaces";
            this.buttonRemoveNamespaces.UseVisualStyleBackColor = true;
            this.buttonRemoveNamespaces.Click += new System.EventHandler(this.buttonRemoveNamespaces_Click);
            // 
            // listBoxUsedNamespaces
            // 
            this.listBoxUsedNamespaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxUsedNamespaces.FormattingEnabled = true;
            this.listBoxUsedNamespaces.IntegralHeight = false;
            this.listBoxUsedNamespaces.ItemHeight = 16;
            this.listBoxUsedNamespaces.Location = new System.Drawing.Point(3, 3);
            this.listBoxUsedNamespaces.Name = "listBoxUsedNamespaces";
            this.listBoxUsedNamespaces.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxUsedNamespaces.Size = new System.Drawing.Size(273, 254);
            this.listBoxUsedNamespaces.Sorted = true;
            this.listBoxUsedNamespaces.TabIndex = 0;
            // 
            // tabPageCSharpCode
            // 
            this.tabPageCSharpCode.Controls.Add(this.textBoxCSharpCode);
            this.tabPageCSharpCode.Location = new System.Drawing.Point(4, 22);
            this.tabPageCSharpCode.Name = "tabPageCSharpCode";
            this.tabPageCSharpCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCSharpCode.Size = new System.Drawing.Size(564, 295);
            this.tabPageCSharpCode.TabIndex = 1;
            this.tabPageCSharpCode.Text = "C# Code";
            this.tabPageCSharpCode.UseVisualStyleBackColor = true;
            // 
            // textBoxCSharpCode
            // 
            this.textBoxCSharpCode.AutoCompleteBracketsList = new char[] {
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
            this.textBoxCSharpCode.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.textBoxCSharpCode.AutoScrollMinSize = new System.Drawing.Size(2, 17);
            this.textBoxCSharpCode.BackBrush = null;
            this.textBoxCSharpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCSharpCode.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.textBoxCSharpCode.CharHeight = 17;
            this.textBoxCSharpCode.CharWidth = 8;
            this.textBoxCSharpCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxCSharpCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textBoxCSharpCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCSharpCode.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxCSharpCode.IsReplaceMode = false;
            this.textBoxCSharpCode.Language = FastColoredTextBoxNS.Language.CSharp;
            this.textBoxCSharpCode.LeftBracket = '(';
            this.textBoxCSharpCode.LeftBracket2 = '{';
            this.textBoxCSharpCode.Location = new System.Drawing.Point(3, 3);
            this.textBoxCSharpCode.Name = "textBoxCSharpCode";
            this.textBoxCSharpCode.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxCSharpCode.ReadOnly = true;
            this.textBoxCSharpCode.RightBracket = ')';
            this.textBoxCSharpCode.RightBracket2 = '}';
            this.textBoxCSharpCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textBoxCSharpCode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBoxCSharpCode.ServiceColors")));
            this.textBoxCSharpCode.ShowFoldingLines = true;
            this.textBoxCSharpCode.Size = new System.Drawing.Size(558, 289);
            this.textBoxCSharpCode.TabIndex = 2;
            this.textBoxCSharpCode.Zoom = 100;
            // 
            // textBoxCommands
            // 
            this.textBoxCommands.AutoCompleteBracketsList = new char[] {
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
            this.textBoxCommands.AutoScrollMinSize = new System.Drawing.Size(2, 17);
            this.textBoxCommands.BackBrush = null;
            this.textBoxCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCommands.CharHeight = 17;
            this.textBoxCommands.CharWidth = 8;
            this.textBoxCommands.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxCommands.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textBoxCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCommands.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxCommands.IsReplaceMode = false;
            this.textBoxCommands.Location = new System.Drawing.Point(3, 3);
            this.textBoxCommands.Name = "textBoxCommands";
            this.textBoxCommands.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxCommands.ReadOnly = true;
            this.textBoxCommands.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textBoxCommands.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBoxCommands.ServiceColors")));
            this.textBoxCommands.ShowFoldingLines = true;
            this.textBoxCommands.Size = new System.Drawing.Size(770, 144);
            this.textBoxCommands.TabIndex = 3;
            this.textBoxCommands.Zoom = 100;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOutput.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOutput.Location = new System.Drawing.Point(0, 0);
            this.textBoxOutput.MaxLength = 0;
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxOutput.Size = new System.Drawing.Size(776, 150);
            this.textBoxOutput.TabIndex = 3;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer4.Location = new System.Drawing.Point(0, 27);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.treeViewComponents);
            this.splitContainer4.Panel1.Controls.Add(this.tabControlUpper);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.tabControlLower);
            this.splitContainer4.Size = new System.Drawing.Size(784, 510);
            this.splitContainer4.SplitterDistance = 327;
            this.splitContainer4.TabIndex = 6;
            // 
            // treeViewComponents
            // 
            this.treeViewComponents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewComponents.Location = new System.Drawing.Point(581, 3);
            this.treeViewComponents.Name = "treeViewComponents";
            this.treeViewComponents.Size = new System.Drawing.Size(199, 321);
            this.treeViewComponents.TabIndex = 2;
            this.treeViewComponents.DoubleClick += new System.EventHandler(this.treeViewComponents_DoubleClick);
            // 
            // tabControlLower
            // 
            this.tabControlLower.Controls.Add(this.tabPageMath);
            this.tabControlLower.Controls.Add(this.tabPageOutput);
            this.tabControlLower.Controls.Add(this.tabPageCommands);
            this.tabControlLower.Controls.Add(this.tabPageErrors);
            this.tabControlLower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlLower.Location = new System.Drawing.Point(0, 0);
            this.tabControlLower.Name = "tabControlLower";
            this.tabControlLower.SelectedIndex = 0;
            this.tabControlLower.Size = new System.Drawing.Size(784, 179);
            this.tabControlLower.TabIndex = 0;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.textBoxOutput);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 25);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Size = new System.Drawing.Size(776, 150);
            this.tabPageOutput.TabIndex = 2;
            this.tabPageOutput.Text = "Output";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // tabPageCommands
            // 
            this.tabPageCommands.Controls.Add(this.textBoxCommands);
            this.tabPageCommands.Location = new System.Drawing.Point(4, 25);
            this.tabPageCommands.Name = "tabPageCommands";
            this.tabPageCommands.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCommands.Size = new System.Drawing.Size(776, 150);
            this.tabPageCommands.TabIndex = 1;
            this.tabPageCommands.Text = "Commands";
            this.tabPageCommands.UseVisualStyleBackColor = true;
            // 
            // tabPageErrors
            // 
            this.tabPageErrors.Controls.Add(this.splitContainer1);
            this.tabPageErrors.Location = new System.Drawing.Point(4, 25);
            this.tabPageErrors.Name = "tabPageErrors";
            this.tabPageErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageErrors.Size = new System.Drawing.Size(776, 150);
            this.tabPageErrors.TabIndex = 0;
            this.tabPageErrors.Text = "Errors";
            this.tabPageErrors.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewErrors);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxErrorDetails);
            this.splitContainer1.Size = new System.Drawing.Size(770, 144);
            this.splitContainer1.SplitterDistance = 375;
            this.splitContainer1.TabIndex = 0;
            // 
            // listViewErrors
            // 
            this.listViewErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSource,
            this.columnHeaderTitle});
            this.listViewErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewErrors.Location = new System.Drawing.Point(0, 0);
            this.listViewErrors.Name = "listViewErrors";
            this.listViewErrors.Size = new System.Drawing.Size(375, 144);
            this.listViewErrors.TabIndex = 5;
            this.listViewErrors.UseCompatibleStateImageBehavior = false;
            this.listViewErrors.View = System.Windows.Forms.View.Details;
            this.listViewErrors.SelectedIndexChanged += new System.EventHandler(this.listViewErrors_SelectedIndexChanged);
            // 
            // columnHeaderSource
            // 
            this.columnHeaderSource.Text = "Source";
            this.columnHeaderSource.Width = 227;
            // 
            // columnHeaderTitle
            // 
            this.columnHeaderTitle.Text = "Title";
            this.columnHeaderTitle.Width = 285;
            // 
            // textBoxErrorDetails
            // 
            this.textBoxErrorDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxErrorDetails.Location = new System.Drawing.Point(0, 0);
            this.textBoxErrorDetails.MaxLength = 0;
            this.textBoxErrorDetails.Multiline = true;
            this.textBoxErrorDetails.Name = "textBoxErrorDetails";
            this.textBoxErrorDetails.ReadOnly = true;
            this.textBoxErrorDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxErrorDetails.Size = new System.Drawing.Size(391, 144);
            this.textBoxErrorDetails.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemScript,
            this.menuItemView});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile_New,
            this.menuItemFile_Open,
            this.menuItemFile_Save,
            this.menuItemFile_SaveAs,
            this.toolStripSeparator1,
            this.menuItemFile_Close});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(37, 20);
            this.menuItemFile.Text = "&File";
            // 
            // menuItemFile_New
            // 
            this.menuItemFile_New.Name = "menuItemFile_New";
            this.menuItemFile_New.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuItemFile_New.Size = new System.Drawing.Size(183, 22);
            this.menuItemFile_New.Text = "New Session";
            this.menuItemFile_New.Click += new System.EventHandler(this.menuItemFile_New_Click);
            // 
            // menuItemFile_Open
            // 
            this.menuItemFile_Open.Name = "menuItemFile_Open";
            this.menuItemFile_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuItemFile_Open.Size = new System.Drawing.Size(183, 22);
            this.menuItemFile_Open.Text = "Open Script";
            this.menuItemFile_Open.Click += new System.EventHandler(this.menuItemFile_Open_Click);
            // 
            // menuItemFile_Save
            // 
            this.menuItemFile_Save.Name = "menuItemFile_Save";
            this.menuItemFile_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItemFile_Save.Size = new System.Drawing.Size(183, 22);
            this.menuItemFile_Save.Text = "Save Script";
            this.menuItemFile_Save.Click += new System.EventHandler(this.menuItemFile_Save_Click);
            // 
            // menuItemFile_SaveAs
            // 
            this.menuItemFile_SaveAs.Name = "menuItemFile_SaveAs";
            this.menuItemFile_SaveAs.Size = new System.Drawing.Size(183, 22);
            this.menuItemFile_SaveAs.Text = "Save Script As...";
            this.menuItemFile_SaveAs.Click += new System.EventHandler(this.menuItemFile_SaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // menuItemFile_Close
            // 
            this.menuItemFile_Close.Name = "menuItemFile_Close";
            this.menuItemFile_Close.Size = new System.Drawing.Size(183, 22);
            this.menuItemFile_Close.Text = "Close";
            // 
            // menuItemScript
            // 
            this.menuItemScript.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemScript_Generate,
            this.menuItemScript_Compile,
            this.menuItemScript_Execute});
            this.menuItemScript.Name = "menuItemScript";
            this.menuItemScript.Size = new System.Drawing.Size(49, 20);
            this.menuItemScript.Text = "&Script";
            // 
            // menuItemScript_Generate
            // 
            this.menuItemScript_Generate.Name = "menuItemScript_Generate";
            this.menuItemScript_Generate.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.menuItemScript_Generate.Size = new System.Drawing.Size(188, 22);
            this.menuItemScript_Generate.Text = "Generate C# Class";
            this.menuItemScript_Generate.Click += new System.EventHandler(this.menuItemScript_Generate_Click);
            // 
            // menuItemScript_Compile
            // 
            this.menuItemScript_Compile.Name = "menuItemScript_Compile";
            this.menuItemScript_Compile.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.menuItemScript_Compile.Size = new System.Drawing.Size(188, 22);
            this.menuItemScript_Compile.Text = "Compile C# Class";
            this.menuItemScript_Compile.Click += new System.EventHandler(this.menuItemScript_Compile_Click);
            // 
            // menuItemScript_Execute
            // 
            this.menuItemScript_Execute.Name = "menuItemScript_Execute";
            this.menuItemScript_Execute.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuItemScript_Execute.Size = new System.Drawing.Size(188, 22);
            this.menuItemScript_Execute.Text = "Execute Script";
            this.menuItemScript_Execute.Click += new System.EventHandler(this.menuItemScript_Execute_Click);
            // 
            // menuItemView
            // 
            this.menuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemView_Progress});
            this.menuItemView.Name = "menuItemView";
            this.menuItemView.Size = new System.Drawing.Size(44, 20);
            this.menuItemView.Text = "&View";
            // 
            // menuItemView_Progress
            // 
            this.menuItemView_Progress.Name = "menuItemView_Progress";
            this.menuItemView_Progress.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.menuItemView_Progress.Size = new System.Drawing.Size(183, 22);
            this.menuItemView_Progress.Text = "Progress Log";
            this.menuItemView_Progress.Click += new System.EventHandler(this.menuItemView_Progress_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "gmacscript";
            this.openFileDialog1.Filter = "GMac Script Files|*.gmacscript|All Files|*.*";
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            this.openFileDialog1.Title = "Open GMac Script File";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "gmacscript";
            this.saveFileDialog1.Filter = "GMac Script Files|*.gmacscript|All Files|*.*";
            this.saveFileDialog1.SupportMultiDottedExtensions = true;
            this.saveFileDialog1.Title = "Save GMac Script";
            // 
            // tabPageMath
            // 
            this.tabPageMath.Controls.Add(this.listBoxMath);
            this.tabPageMath.Location = new System.Drawing.Point(4, 25);
            this.tabPageMath.Name = "tabPageMath";
            this.tabPageMath.Size = new System.Drawing.Size(776, 150);
            this.tabPageMath.TabIndex = 3;
            this.tabPageMath.Text = "Math";
            this.tabPageMath.UseVisualStyleBackColor = true;
            // 
            // listBoxMath
            // 
            this.listBoxMath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMath.FormattingEnabled = true;
            this.listBoxMath.ItemHeight = 16;
            this.listBoxMath.Location = new System.Drawing.Point(0, 0);
            this.listBoxMath.Name = "listBoxMath";
            this.listBoxMath.Size = new System.Drawing.Size(776, 150);
            this.listBoxMath.TabIndex = 0;
            // 
            // FormInteractiveScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer4);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormInteractiveScript";
            this.Text = "Interactive Script";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormInteractiveScript_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControlUpper.ResumeLayout(false);
            this.tabPageScript.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxScript)).EndInit();
            this.tabPageMembers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxMembers)).EndInit();
            this.tabPageNamespaces.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.tabPageCSharpCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCSharpCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCommands)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tabControlLower.ResumeLayout(false);
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageOutput.PerformLayout();
            this.tabPageCommands.ResumeLayout(false);
            this.tabPageErrors.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPageMath.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip1;
        private TabControl tabControlUpper;
        private TabPage tabPageScript;
        private TabPage tabPageCSharpCode;
        private FastColoredTextBox textBoxScript;
        private FastColoredTextBox textBoxCSharpCode;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private FastColoredTextBox textBoxCommands;
        private TabPage tabPageNamespaces;
        private SplitContainer splitContainer5;
        private TextBox textBoxOutput;
        private TabPage tabPageMembers;
        private FastColoredTextBox textBoxMembers;
        private ListBox listBoxUsedNamespaces;
        private ListBox listBoxAllNamespaces;
        private Button buttonAddSelected;
        private Button buttonRemoveNamespaces;
        private SplitContainer splitContainer4;
        private TabControl tabControlLower;
        private TabPage tabPageErrors;
        private TabPage tabPageCommands;
        private TabPage tabPageOutput;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuItemFile;
        private ToolStripMenuItem menuItemFile_Open;
        private ToolStripMenuItem menuItemFile_Save;
        private ToolStripMenuItem menuItemFile_SaveAs;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menuItemFile_Close;
        private ToolStripMenuItem menuItemScript;
        private ToolStripMenuItem menuItemScript_Generate;
        private ToolStripMenuItem menuItemScript_Compile;
        private ToolStripMenuItem menuItemScript_Execute;
        private TreeView treeViewComponents;
        private ToolStripMenuItem menuItemView;
        private ToolStripMenuItem menuItemView_Progress;
        private SplitContainer splitContainer1;
        private ListView listViewErrors;
        private ColumnHeader columnHeaderSource;
        private ColumnHeader columnHeaderTitle;
        private TextBox textBoxErrorDetails;
        private ToolStripMenuItem menuItemFile_New;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private TabPage tabPageMath;
        private ListBox listBoxMath;
    }
}