using System.ComponentModel;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace GMac.GMacIDE
{
    partial class FormMacroExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMacroExplorer));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listBoxGenerationStage = new System.Windows.Forms.ListBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxMacroName = new System.Windows.Forms.TextBox();
            this.textBoxParameters = new System.Windows.Forms.TextBox();
            this.buttonResetParameters = new System.Windows.Forms.Button();
            this.textBoxDisplay = new FastColoredTextBoxNS.FastColoredTextBox();
            this.buttonGraphViz = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 420);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(624, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
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
            this.splitContainer1.Panel2.Controls.Add(this.buttonGraphViz);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxDisplay);
            this.splitContainer1.Panel2.Controls.Add(this.buttonResetParameters);
            this.splitContainer1.Panel2.Controls.Add(this.buttonGenerate);
            this.splitContainer1.Panel2.Controls.Add(this.buttonClose);
            this.splitContainer1.Size = new System.Drawing.Size(624, 420);
            this.splitContainer1.SplitterDistance = 159;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.textBoxParameters);
            this.splitContainer2.Panel1.Controls.Add(this.textBoxMacroName);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listBoxGenerationStage);
            this.splitContainer2.Size = new System.Drawing.Size(624, 159);
            this.splitContainer2.SplitterDistance = 394;
            this.splitContainer2.TabIndex = 0;
            // 
            // listBoxGenerationStage
            // 
            this.listBoxGenerationStage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxGenerationStage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxGenerationStage.FormattingEnabled = true;
            this.listBoxGenerationStage.IntegralHeight = false;
            this.listBoxGenerationStage.ItemHeight = 16;
            this.listBoxGenerationStage.Location = new System.Drawing.Point(3, 6);
            this.listBoxGenerationStage.Name = "listBoxGenerationStage";
            this.listBoxGenerationStage.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxGenerationStage.Size = new System.Drawing.Size(217, 149);
            this.listBoxGenerationStage.TabIndex = 5;
            this.listBoxGenerationStage.SelectedIndexChanged += new System.EventHandler(this.listBoxGenerationStage_SelectedIndexChanged);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGenerate.Location = new System.Drawing.Point(159, 221);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(150, 33);
            this.buttonGenerate.TabIndex = 2;
            this.buttonGenerate.Text = "&Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(531, 221);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(87, 33);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // textBoxMacroName
            // 
            this.textBoxMacroName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMacroName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMacroName.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMacroName.Location = new System.Drawing.Point(3, 6);
            this.textBoxMacroName.Name = "textBoxMacroName";
            this.textBoxMacroName.ReadOnly = true;
            this.textBoxMacroName.Size = new System.Drawing.Size(388, 26);
            this.textBoxMacroName.TabIndex = 4;
            // 
            // textBoxParameters
            // 
            this.textBoxParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxParameters.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxParameters.Location = new System.Drawing.Point(3, 35);
            this.textBoxParameters.MaxLength = 0;
            this.textBoxParameters.Multiline = true;
            this.textBoxParameters.Name = "textBoxParameters";
            this.textBoxParameters.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxParameters.Size = new System.Drawing.Size(388, 120);
            this.textBoxParameters.TabIndex = 5;
            // 
            // buttonResetParameters
            // 
            this.buttonResetParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonResetParameters.Location = new System.Drawing.Point(3, 221);
            this.buttonResetParameters.Name = "buttonResetParameters";
            this.buttonResetParameters.Size = new System.Drawing.Size(150, 33);
            this.buttonResetParameters.TabIndex = 3;
            this.buttonResetParameters.Text = "&Reset Parameters";
            this.buttonResetParameters.UseVisualStyleBackColor = true;
            this.buttonResetParameters.Click += new System.EventHandler(this.buttonResetParameters_Click);
            // 
            // textBoxDisplay
            // 
            this.textBoxDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDisplay.AutoCompleteBracketsList = new char[] {
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
            this.textBoxDisplay.AutoScrollMinSize = new System.Drawing.Size(27, 17);
            this.textBoxDisplay.BackBrush = null;
            this.textBoxDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDisplay.CharHeight = 17;
            this.textBoxDisplay.CharWidth = 8;
            this.textBoxDisplay.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxDisplay.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textBoxDisplay.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxDisplay.IsReplaceMode = false;
            this.textBoxDisplay.Location = new System.Drawing.Point(8, 8);
            this.textBoxDisplay.Name = "textBoxDisplay";
            this.textBoxDisplay.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxDisplay.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textBoxDisplay.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBoxDisplay.ServiceColors")));
            this.textBoxDisplay.ShowFoldingLines = true;
            this.textBoxDisplay.Size = new System.Drawing.Size(610, 207);
            this.textBoxDisplay.TabIndex = 4;
            this.textBoxDisplay.Zoom = 100;
            // 
            // buttonGraphViz
            // 
            this.buttonGraphViz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGraphViz.Location = new System.Drawing.Point(315, 221);
            this.buttonGraphViz.Name = "buttonGraphViz";
            this.buttonGraphViz.Size = new System.Drawing.Size(150, 33);
            this.buttonGraphViz.TabIndex = 5;
            this.buttonGraphViz.Text = "Graph&Viz";
            this.buttonGraphViz.UseVisualStyleBackColor = true;
            this.buttonGraphViz.Click += new System.EventHandler(this.buttonGraphViz_Click);
            // 
            // FormMacroExplorer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormMacroExplorer";
            this.Text = "Macro Generation Explorer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip1;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private ListBox listBoxGenerationStage;
        private Button buttonGenerate;
        private Button buttonClose;
        private TextBox textBoxMacroName;
        private TextBox textBoxParameters;
        private Button buttonResetParameters;
        private FastColoredTextBox textBoxDisplay;
        private Button buttonGraphViz;

    }
}