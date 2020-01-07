using System.ComponentModel;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace GMac.GMacIDE
{
    partial class FormGMacMacroExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGMacMacroExplorer));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.checkBoxFixOutputComputationsOrder = new System.Windows.Forms.CheckBox();
            this.textBoxParameters = new System.Windows.Forms.TextBox();
            this.textBoxMacroName = new System.Windows.Forms.TextBox();
            this.listBoxGenerationStage = new System.Windows.Forms.ListBox();
            this.buttonExcel = new System.Windows.Forms.Button();
            this.buttonGraphViz = new System.Windows.Forms.Button();
            this.textBoxDisplay = new FastColoredTextBoxNS.FastColoredTextBox();
            this.buttonResetParameters = new System.Windows.Forms.Button();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.comboBoxTargetLanguage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
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
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxTargetLanguage);
            this.splitContainer1.Panel2.Controls.Add(this.buttonExcel);
            this.splitContainer1.Panel2.Controls.Add(this.buttonGraphViz);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxDisplay);
            this.splitContainer1.Panel2.Controls.Add(this.buttonResetParameters);
            this.splitContainer1.Panel2.Controls.Add(this.buttonGenerate);
            this.splitContainer1.Panel2.Controls.Add(this.buttonClose);
            this.splitContainer1.Size = new System.Drawing.Size(784, 539);
            this.splitContainer1.SplitterDistance = 204;
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
            this.splitContainer2.Panel1.Controls.Add(this.checkBoxFixOutputComputationsOrder);
            this.splitContainer2.Panel1.Controls.Add(this.textBoxParameters);
            this.splitContainer2.Panel1.Controls.Add(this.textBoxMacroName);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listBoxGenerationStage);
            this.splitContainer2.Size = new System.Drawing.Size(784, 204);
            this.splitContainer2.SplitterDistance = 495;
            this.splitContainer2.TabIndex = 0;
            // 
            // checkBoxFixOutputComputationsOrder
            // 
            this.checkBoxFixOutputComputationsOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxFixOutputComputationsOrder.AutoSize = true;
            this.checkBoxFixOutputComputationsOrder.Location = new System.Drawing.Point(8, 180);
            this.checkBoxFixOutputComputationsOrder.Name = "checkBoxFixOutputComputationsOrder";
            this.checkBoxFixOutputComputationsOrder.Size = new System.Drawing.Size(232, 20);
            this.checkBoxFixOutputComputationsOrder.TabIndex = 6;
            this.checkBoxFixOutputComputationsOrder.Text = "Fix Output Computations Order";
            this.checkBoxFixOutputComputationsOrder.UseVisualStyleBackColor = true;
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
            this.textBoxParameters.Size = new System.Drawing.Size(489, 139);
            this.textBoxParameters.TabIndex = 5;
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
            this.textBoxMacroName.Size = new System.Drawing.Size(489, 26);
            this.textBoxMacroName.TabIndex = 4;
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
            this.listBoxGenerationStage.Size = new System.Drawing.Size(276, 194);
            this.listBoxGenerationStage.TabIndex = 5;
            this.listBoxGenerationStage.SelectedIndexChanged += new System.EventHandler(this.listBoxGenerationStage_SelectedIndexChanged);
            // 
            // buttonExcel
            // 
            this.buttonExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExcel.Location = new System.Drawing.Point(373, 295);
            this.buttonExcel.Name = "buttonExcel";
            this.buttonExcel.Size = new System.Drawing.Size(101, 33);
            this.buttonExcel.TabIndex = 6;
            this.buttonExcel.Text = "To E&xcel";
            this.buttonExcel.UseVisualStyleBackColor = true;
            this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
            // 
            // buttonGraphViz
            // 
            this.buttonGraphViz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGraphViz.Location = new System.Drawing.Point(266, 295);
            this.buttonGraphViz.Name = "buttonGraphViz";
            this.buttonGraphViz.Size = new System.Drawing.Size(101, 33);
            this.buttonGraphViz.TabIndex = 5;
            this.buttonGraphViz.Text = "To Graph&Viz";
            this.buttonGraphViz.UseVisualStyleBackColor = true;
            this.buttonGraphViz.Click += new System.EventHandler(this.buttonGraphViz_Click);
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
            this.textBoxDisplay.Size = new System.Drawing.Size(770, 248);
            this.textBoxDisplay.TabIndex = 4;
            this.textBoxDisplay.Zoom = 100;
            // 
            // buttonResetParameters
            // 
            this.buttonResetParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonResetParameters.Location = new System.Drawing.Point(3, 295);
            this.buttonResetParameters.Name = "buttonResetParameters";
            this.buttonResetParameters.Size = new System.Drawing.Size(150, 33);
            this.buttonResetParameters.TabIndex = 3;
            this.buttonResetParameters.Text = "&Reset Parameters";
            this.buttonResetParameters.UseVisualStyleBackColor = true;
            this.buttonResetParameters.Click += new System.EventHandler(this.buttonResetParameters_Click);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGenerate.Location = new System.Drawing.Point(159, 295);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(101, 33);
            this.buttonGenerate.TabIndex = 2;
            this.buttonGenerate.Text = "&Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(677, 295);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(101, 33);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // comboBoxTargetLanguage
            // 
            this.comboBoxTargetLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxTargetLanguage.FormattingEnabled = true;
            this.comboBoxTargetLanguage.Items.AddRange(new object[] {
            "C#",
            "C++"});
            this.comboBoxTargetLanguage.Location = new System.Drawing.Point(142, 263);
            this.comboBoxTargetLanguage.Name = "comboBoxTargetLanguage";
            this.comboBoxTargetLanguage.Size = new System.Drawing.Size(225, 24);
            this.comboBoxTargetLanguage.Sorted = true;
            this.comboBoxTargetLanguage.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Target Language:";
            // 
            // FormGMacMacroExplorer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormGMacMacroExplorer";
            this.Text = "Macro Generation Explorer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
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
        private Button buttonExcel;
        private SaveFileDialog saveFileDialog1;
        private CheckBox checkBoxFixOutputComputationsOrder;
        private Label label1;
        private ComboBox comboBoxTargetLanguage;
    }
}