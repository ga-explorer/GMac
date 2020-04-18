using System.ComponentModel;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace GMacSamples
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDslCode = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTools_BaseSamples = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTools_BaseSamples_BitUtils = new System.Windows.Forms.ToolStripMenuItem();
            this.exploreGMacASTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTools_CodeGenerators = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SourceCodeTextEditor = new FastColoredTextBoxNS.FastColoredTextBox();
            this.menuItemTools_BaseSamples_MultivectorUtils = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SourceCodeTextEditor)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "DSL Code:";
            // 
            // comboBoxDslCode
            // 
            this.comboBoxDslCode.FormattingEnabled = true;
            this.comboBoxDslCode.Location = new System.Drawing.Point(95, 42);
            this.comboBoxDslCode.Name = "comboBoxDslCode";
            this.comboBoxDslCode.Size = new System.Drawing.Size(149, 24);
            this.comboBoxDslCode.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(624, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTools_BaseSamples,
            this.exploreGMacASTToolStripMenuItem,
            this.menuItemTools_CodeGenerators,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // menuItemTools_BaseSamples
            // 
            this.menuItemTools_BaseSamples.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTools_BaseSamples_BitUtils,
            this.menuItemTools_BaseSamples_MultivectorUtils});
            this.menuItemTools_BaseSamples.Name = "menuItemTools_BaseSamples";
            this.menuItemTools_BaseSamples.Size = new System.Drawing.Size(167, 22);
            this.menuItemTools_BaseSamples.Text = "&Base Samples";
            // 
            // menuItemTools_BaseSamples_BitUtils
            // 
            this.menuItemTools_BaseSamples_BitUtils.Name = "menuItemTools_BaseSamples_BitUtils";
            this.menuItemTools_BaseSamples_BitUtils.Size = new System.Drawing.Size(158, 22);
            this.menuItemTools_BaseSamples_BitUtils.Text = "BitUtils";
            this.menuItemTools_BaseSamples_BitUtils.Click += new System.EventHandler(this.menuItemTools_BaseSamples_BitUtils_Click);
            // 
            // exploreGMacASTToolStripMenuItem
            // 
            this.exploreGMacASTToolStripMenuItem.Name = "exploreGMacASTToolStripMenuItem";
            this.exploreGMacASTToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.exploreGMacASTToolStripMenuItem.Text = "Explore GMac&AST";
            // 
            // menuItemTools_CodeGenerators
            // 
            this.menuItemTools_CodeGenerators.Name = "menuItemTools_CodeGenerators";
            this.menuItemTools_CodeGenerators.Size = new System.Drawing.Size(167, 22);
            this.menuItemTools_CodeGenerators.Text = "Code &Generators";
            this.menuItemTools_CodeGenerators.Click += new System.EventHandler(this.menuItemTools_CodeGenerators_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(164, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // SourceCodeTextEditor
            // 
            this.SourceCodeTextEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.SourceCodeTextEditor.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.SourceCodeTextEditor.IsReplaceMode = false;
            this.SourceCodeTextEditor.Location = new System.Drawing.Point(12, 72);
            this.SourceCodeTextEditor.Name = "SourceCodeTextEditor";
            this.SourceCodeTextEditor.Paddings = new System.Windows.Forms.Padding(0);
            this.SourceCodeTextEditor.ReadOnly = true;
            this.SourceCodeTextEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.SourceCodeTextEditor.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("SourceCodeTextEditor.ServiceColors")));
            this.SourceCodeTextEditor.ShowFoldingLines = true;
            this.SourceCodeTextEditor.Size = new System.Drawing.Size(600, 358);
            this.SourceCodeTextEditor.TabIndex = 3;
            this.SourceCodeTextEditor.Zoom = 100;
            this.SourceCodeTextEditor.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.SourceCodeTextEditor_TextChanged);
            // 
            // menuItemTools_BaseSamples_MultivectorUtils
            // 
            this.menuItemTools_BaseSamples_MultivectorUtils.Name = "menuItemTools_BaseSamples_MultivectorUtils";
            this.menuItemTools_BaseSamples_MultivectorUtils.Size = new System.Drawing.Size(158, 22);
            this.menuItemTools_BaseSamples_MultivectorUtils.Text = "MultivectorUtils";
            this.menuItemTools_BaseSamples_MultivectorUtils.Click += new System.EventHandler(this.menuItemTools_BaseSamples_MultivectorUtils_Click);
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.SourceCodeTextEditor);
            this.Controls.Add(this.comboBoxDslCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormMain";
            this.Text = "GMac Samples";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SourceCodeTextEditor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private ComboBox comboBoxDslCode;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem exploreGMacASTToolStripMenuItem;
        private ToolStripMenuItem menuItemTools_CodeGenerators;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private FastColoredTextBox SourceCodeTextEditor;
        private ToolStripMenuItem menuItemTools_BaseSamples;
        private ToolStripMenuItem menuItemTools_BaseSamples_BitUtils;
        private ToolStripMenuItem menuItemTools_BaseSamples_MultivectorUtils;
    }
}

