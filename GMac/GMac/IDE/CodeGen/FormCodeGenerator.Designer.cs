using System.ComponentModel;
using System.Windows.Forms;

namespace GMac.IDE.CodeGen
{
    partial class FormCodeGenerator
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGenOptions = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonShowAll = new System.Windows.Forms.Button();
            this.buttonFilterView = new System.Windows.Forms.Button();
            this.textBoxNameFilter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxRuleFilter = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonInvertSelection = new System.Windows.Forms.Button();
            this.buttonSelectNone = new System.Windows.Forms.Button();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.listViewAstItems = new System.Windows.Forms.ListView();
            this.columnHeaderRule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBoxGenerateMacroCode = new System.Windows.Forms.CheckBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.comboBoxGenerators = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPageGenOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageGenOptions);
            this.tabControl1.Location = new System.Drawing.Point(1, 119);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(621, 286);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPageGenOptions
            // 
            this.tabPageGenOptions.Controls.Add(this.label6);
            this.tabPageGenOptions.Controls.Add(this.buttonShowAll);
            this.tabPageGenOptions.Controls.Add(this.buttonFilterView);
            this.tabPageGenOptions.Controls.Add(this.textBoxNameFilter);
            this.tabPageGenOptions.Controls.Add(this.label5);
            this.tabPageGenOptions.Controls.Add(this.textBoxRuleFilter);
            this.tabPageGenOptions.Controls.Add(this.label4);
            this.tabPageGenOptions.Controls.Add(this.buttonInvertSelection);
            this.tabPageGenOptions.Controls.Add(this.buttonSelectNone);
            this.tabPageGenOptions.Controls.Add(this.buttonSelectAll);
            this.tabPageGenOptions.Controls.Add(this.listViewAstItems);
            this.tabPageGenOptions.Controls.Add(this.checkBoxGenerateMacroCode);
            this.tabPageGenOptions.Location = new System.Drawing.Point(4, 25);
            this.tabPageGenOptions.Name = "tabPageGenOptions";
            this.tabPageGenOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGenOptions.Size = new System.Drawing.Size(613, 257);
            this.tabPageGenOptions.TabIndex = 1;
            this.tabPageGenOptions.Text = "Generation Options";
            this.tabPageGenOptions.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "AST Symbols:";
            // 
            // buttonShowAll
            // 
            this.buttonShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowAll.Location = new System.Drawing.Point(475, 108);
            this.buttonShowAll.Name = "buttonShowAll";
            this.buttonShowAll.Size = new System.Drawing.Size(128, 24);
            this.buttonShowAll.TabIndex = 34;
            this.buttonShowAll.Text = "Show All";
            this.buttonShowAll.UseVisualStyleBackColor = true;
            this.buttonShowAll.Click += new System.EventHandler(this.buttonShowAll_Click);
            // 
            // buttonFilterView
            // 
            this.buttonFilterView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFilterView.Location = new System.Drawing.Point(475, 48);
            this.buttonFilterView.Name = "buttonFilterView";
            this.buttonFilterView.Size = new System.Drawing.Size(128, 24);
            this.buttonFilterView.TabIndex = 31;
            this.buttonFilterView.Text = "&Filter View";
            this.buttonFilterView.UseVisualStyleBackColor = true;
            this.buttonFilterView.Click += new System.EventHandler(this.buttonFilterView_Click);
            // 
            // textBoxNameFilter
            // 
            this.textBoxNameFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNameFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxNameFilter.Location = new System.Drawing.Point(182, 48);
            this.textBoxNameFilter.Name = "textBoxNameFilter";
            this.textBoxNameFilter.Size = new System.Drawing.Size(287, 23);
            this.textBoxNameFilter.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(179, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 29;
            this.label5.Text = "Name Filter:";
            // 
            // textBoxRuleFilter
            // 
            this.textBoxRuleFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRuleFilter.Location = new System.Drawing.Point(7, 48);
            this.textBoxRuleFilter.Name = "textBoxRuleFilter";
            this.textBoxRuleFilter.Size = new System.Drawing.Size(169, 23);
            this.textBoxRuleFilter.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 27;
            this.label4.Text = "Rule Filter:";
            // 
            // buttonInvertSelection
            // 
            this.buttonInvertSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInvertSelection.Location = new System.Drawing.Point(475, 198);
            this.buttonInvertSelection.Name = "buttonInvertSelection";
            this.buttonInvertSelection.Size = new System.Drawing.Size(128, 24);
            this.buttonInvertSelection.TabIndex = 26;
            this.buttonInvertSelection.Text = "Invert Selection";
            this.buttonInvertSelection.UseVisualStyleBackColor = true;
            this.buttonInvertSelection.Click += new System.EventHandler(this.buttonInvertSelection_Click);
            // 
            // buttonSelectNone
            // 
            this.buttonSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectNone.Location = new System.Drawing.Point(475, 168);
            this.buttonSelectNone.Name = "buttonSelectNone";
            this.buttonSelectNone.Size = new System.Drawing.Size(128, 24);
            this.buttonSelectNone.TabIndex = 25;
            this.buttonSelectNone.Text = "Select None";
            this.buttonSelectNone.UseVisualStyleBackColor = true;
            this.buttonSelectNone.Click += new System.EventHandler(this.buttonSelectNone_Click);
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectAll.Location = new System.Drawing.Point(475, 138);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(128, 24);
            this.buttonSelectAll.TabIndex = 24;
            this.buttonSelectAll.Text = "Select All";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // listViewAstItems
            // 
            this.listViewAstItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAstItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewAstItems.CheckBoxes = true;
            this.listViewAstItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderRule,
            this.columnHeaderName});
            this.listViewAstItems.FullRowSelect = true;
            this.listViewAstItems.GridLines = true;
            this.listViewAstItems.HideSelection = false;
            this.listViewAstItems.Location = new System.Drawing.Point(7, 108);
            this.listViewAstItems.MultiSelect = false;
            this.listViewAstItems.Name = "listViewAstItems";
            this.listViewAstItems.Size = new System.Drawing.Size(462, 143);
            this.listViewAstItems.TabIndex = 23;
            this.listViewAstItems.UseCompatibleStateImageBehavior = false;
            this.listViewAstItems.View = System.Windows.Forms.View.Details;
            this.listViewAstItems.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewAstItems_ItemChecked);
            // 
            // columnHeaderRule
            // 
            this.columnHeaderRule.Text = "Rule";
            this.columnHeaderRule.Width = 103;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 395;
            // 
            // checkBoxGenerateMacroCode
            // 
            this.checkBoxGenerateMacroCode.AutoSize = true;
            this.checkBoxGenerateMacroCode.Location = new System.Drawing.Point(6, 6);
            this.checkBoxGenerateMacroCode.Name = "checkBoxGenerateMacroCode";
            this.checkBoxGenerateMacroCode.Size = new System.Drawing.Size(170, 20);
            this.checkBoxGenerateMacroCode.TabIndex = 22;
            this.checkBoxGenerateMacroCode.Text = "Generate Macro Code";
            this.checkBoxGenerateMacroCode.UseVisualStyleBackColor = true;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGenerate.Location = new System.Drawing.Point(11, 411);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(128, 24);
            this.buttonGenerate.TabIndex = 18;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // comboBoxGenerators
            // 
            this.comboBoxGenerators.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxGenerators.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGenerators.FormattingEnabled = true;
            this.comboBoxGenerators.Location = new System.Drawing.Point(180, 6);
            this.comboBoxGenerators.Name = "comboBoxGenerators";
            this.comboBoxGenerators.Size = new System.Drawing.Size(442, 24);
            this.comboBoxGenerators.TabIndex = 13;
            this.comboBoxGenerators.SelectedIndexChanged += new System.EventHandler(this.comboBoxGenerators_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Code Library Generator:";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(480, 411);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(128, 24);
            this.buttonClose.TabIndex = 20;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(5, 36);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDescription.Size = new System.Drawing.Size(613, 77);
            this.textBoxDescription.TabIndex = 21;
            // 
            // FormCodeGenerator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxGenerators);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormCodeGenerator";
            this.Text = "FormCodeGenerator";
            this.tabControl1.ResumeLayout(false);
            this.tabPageGenOptions.ResumeLayout(false);
            this.tabPageGenOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FolderBrowserDialog folderBrowserDialog1;
        private TabControl tabControl1;
        private TabPage tabPageGenOptions;
        private Button buttonClose;
        private Button buttonGenerate;
        private ComboBox comboBoxGenerators;
        private Label label1;
        private CheckBox checkBoxGenerateMacroCode;
        private Button buttonInvertSelection;
        private Button buttonSelectNone;
        private Button buttonSelectAll;
        private ListView listViewAstItems;
        private ColumnHeader columnHeaderRule;
        private ColumnHeader columnHeaderName;
        private Button buttonFilterView;
        private TextBox textBoxNameFilter;
        private Label label5;
        private TextBox textBoxRuleFilter;
        private Label label4;
        private Label label6;
        private Button buttonShowAll;
        private TextBox textBoxDescription;
    }
}