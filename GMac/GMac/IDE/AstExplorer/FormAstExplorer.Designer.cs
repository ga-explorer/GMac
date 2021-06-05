using System.ComponentModel;
using System.Windows.Forms;

namespace GMac.IDE.AstExplorer
{
    partial class FormAstExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAstExplorer));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemView_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_General = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_General_GMacAstItems = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Ast = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Ast_Components = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Ast_TypesDep = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Ast_MacrosDep = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Frame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Frame_Components = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Frame_MvTypeDep = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Structure = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Structure_Components = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Structure_Dependencies = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Structure_FullTree = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Macro = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Macro_Components = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDiagrams_Macro_Dependencies = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControlASTTrees = new System.Windows.Forms.TabControl();
            this.tabPageComponents = new System.Windows.Forms.TabPage();
            this.treeViewComponents = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPageLogicalView = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.menuStripMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlASTTrees.SuspendLayout();
            this.tabPageComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.menuItemDiagrams});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1045, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemView_Close});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // menuItemView_Close
            // 
            this.menuItemView_Close.Name = "menuItemView_Close";
            this.menuItemView_Close.Size = new System.Drawing.Size(103, 22);
            this.menuItemView_Close.Text = "&Close";
            this.menuItemView_Close.Click += new System.EventHandler(this.menuItemView_Close_Click);
            // 
            // menuItemDiagrams
            // 
            this.menuItemDiagrams.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDiagrams_General,
            this.menuItemDiagrams_Ast,
            this.menuItemDiagrams_Frame,
            this.menuItemDiagrams_Structure,
            this.menuItemDiagrams_Macro});
            this.menuItemDiagrams.Name = "menuItemDiagrams";
            this.menuItemDiagrams.Size = new System.Drawing.Size(69, 20);
            this.menuItemDiagrams.Text = "&Diagrams";
            // 
            // menuItemDiagrams_General
            // 
            this.menuItemDiagrams_General.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDiagrams_General_GMacAstItems});
            this.menuItemDiagrams_General.Name = "menuItemDiagrams_General";
            this.menuItemDiagrams_General.Size = new System.Drawing.Size(122, 22);
            this.menuItemDiagrams_General.Text = "&General";
            // 
            // menuItemDiagrams_General_GMacAstItems
            // 
            this.menuItemDiagrams_General_GMacAstItems.Name = "menuItemDiagrams_General_GMacAstItems";
            this.menuItemDiagrams_General_GMacAstItems.Size = new System.Drawing.Size(188, 22);
            this.menuItemDiagrams_General_GMacAstItems.Text = "GMacAST Main Items";
            this.menuItemDiagrams_General_GMacAstItems.Click += new System.EventHandler(this.menuItemDiagrams_General_GMacAstItems_Click);
            // 
            // menuItemDiagrams_Ast
            // 
            this.menuItemDiagrams_Ast.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDiagrams_Ast_Components,
            this.menuItemDiagrams_Ast_TypesDep,
            this.menuItemDiagrams_Ast_MacrosDep});
            this.menuItemDiagrams_Ast.Name = "menuItemDiagrams_Ast";
            this.menuItemDiagrams_Ast.Size = new System.Drawing.Size(122, 22);
            this.menuItemDiagrams_Ast.Text = "&AST";
            // 
            // menuItemDiagrams_Ast_Components
            // 
            this.menuItemDiagrams_Ast_Components.Name = "menuItemDiagrams_Ast_Components";
            this.menuItemDiagrams_Ast_Components.Size = new System.Drawing.Size(182, 22);
            this.menuItemDiagrams_Ast_Components.Text = "Components";
            this.menuItemDiagrams_Ast_Components.Click += new System.EventHandler(this.menuItemDiagrams_Ast_Components_Click);
            // 
            // menuItemDiagrams_Ast_TypesDep
            // 
            this.menuItemDiagrams_Ast_TypesDep.Name = "menuItemDiagrams_Ast_TypesDep";
            this.menuItemDiagrams_Ast_TypesDep.Size = new System.Drawing.Size(182, 22);
            this.menuItemDiagrams_Ast_TypesDep.Text = "Types Dependency";
            this.menuItemDiagrams_Ast_TypesDep.Click += new System.EventHandler(this.menuItemDiagrams_Ast_TypesDep_Click);
            // 
            // menuItemDiagrams_Ast_MacrosDep
            // 
            this.menuItemDiagrams_Ast_MacrosDep.Name = "menuItemDiagrams_Ast_MacrosDep";
            this.menuItemDiagrams_Ast_MacrosDep.Size = new System.Drawing.Size(182, 22);
            this.menuItemDiagrams_Ast_MacrosDep.Text = "Macros Dependency";
            this.menuItemDiagrams_Ast_MacrosDep.Click += new System.EventHandler(this.menuItemDiagrams_Ast_MacrosDep_Click);
            // 
            // menuItemDiagrams_Frame
            // 
            this.menuItemDiagrams_Frame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDiagrams_Frame_Components,
            this.menuItemDiagrams_Frame_MvTypeDep});
            this.menuItemDiagrams_Frame.Name = "menuItemDiagrams_Frame";
            this.menuItemDiagrams_Frame.Size = new System.Drawing.Size(122, 22);
            this.menuItemDiagrams_Frame.Text = "&Frame";
            // 
            // menuItemDiagrams_Frame_Components
            // 
            this.menuItemDiagrams_Frame_Components.Name = "menuItemDiagrams_Frame_Components";
            this.menuItemDiagrams_Frame_Components.Size = new System.Drawing.Size(241, 22);
            this.menuItemDiagrams_Frame_Components.Text = "Components";
            this.menuItemDiagrams_Frame_Components.Click += new System.EventHandler(this.menuItemDiagrams_Frame_Components_Click);
            // 
            // menuItemDiagrams_Frame_MvTypeDep
            // 
            this.menuItemDiagrams_Frame_MvTypeDep.Name = "menuItemDiagrams_Frame_MvTypeDep";
            this.menuItemDiagrams_Frame_MvTypeDep.Size = new System.Drawing.Size(241, 22);
            this.menuItemDiagrams_Frame_MvTypeDep.Text = "Multivector Type Dependencies";
            this.menuItemDiagrams_Frame_MvTypeDep.Click += new System.EventHandler(this.menuItemDiagrams_Frame_MvTypeDep_Click);
            // 
            // menuItemDiagrams_Structure
            // 
            this.menuItemDiagrams_Structure.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDiagrams_Structure_Components,
            this.menuItemDiagrams_Structure_Dependencies,
            this.menuItemDiagrams_Structure_FullTree});
            this.menuItemDiagrams_Structure.Name = "menuItemDiagrams_Structure";
            this.menuItemDiagrams_Structure.Size = new System.Drawing.Size(122, 22);
            this.menuItemDiagrams_Structure.Text = "&Structure";
            // 
            // menuItemDiagrams_Structure_Components
            // 
            this.menuItemDiagrams_Structure_Components.Name = "menuItemDiagrams_Structure_Components";
            this.menuItemDiagrams_Structure_Components.Size = new System.Drawing.Size(191, 22);
            this.menuItemDiagrams_Structure_Components.Text = "Components";
            this.menuItemDiagrams_Structure_Components.Click += new System.EventHandler(this.menuItemDiagrams_Structure_Components_Click);
            // 
            // menuItemDiagrams_Structure_Dependencies
            // 
            this.menuItemDiagrams_Structure_Dependencies.Name = "menuItemDiagrams_Structure_Dependencies";
            this.menuItemDiagrams_Structure_Dependencies.Size = new System.Drawing.Size(191, 22);
            this.menuItemDiagrams_Structure_Dependencies.Text = "Dependencies";
            this.menuItemDiagrams_Structure_Dependencies.Click += new System.EventHandler(this.menuItemDiagrams_Structure_Dependencies_Click);
            // 
            // menuItemDiagrams_Structure_FullTree
            // 
            this.menuItemDiagrams_Structure_FullTree.Name = "menuItemDiagrams_Structure_FullTree";
            this.menuItemDiagrams_Structure_FullTree.Size = new System.Drawing.Size(191, 22);
            this.menuItemDiagrams_Structure_FullTree.Text = "Full Components Tree";
            this.menuItemDiagrams_Structure_FullTree.Click += new System.EventHandler(this.menuItemDiagrams_Structure_FullTree_Click);
            // 
            // menuItemDiagrams_Macro
            // 
            this.menuItemDiagrams_Macro.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDiagrams_Macro_Components,
            this.menuItemDiagrams_Macro_Dependencies});
            this.menuItemDiagrams_Macro.Name = "menuItemDiagrams_Macro";
            this.menuItemDiagrams_Macro.Size = new System.Drawing.Size(122, 22);
            this.menuItemDiagrams_Macro.Text = "&Macro";
            // 
            // menuItemDiagrams_Macro_Components
            // 
            this.menuItemDiagrams_Macro_Components.Name = "menuItemDiagrams_Macro_Components";
            this.menuItemDiagrams_Macro_Components.Size = new System.Drawing.Size(148, 22);
            this.menuItemDiagrams_Macro_Components.Text = "Components";
            this.menuItemDiagrams_Macro_Components.Click += new System.EventHandler(this.menuItemDiagrams_Macro_Components_Click);
            // 
            // menuItemDiagrams_Macro_Dependencies
            // 
            this.menuItemDiagrams_Macro_Dependencies.Name = "menuItemDiagrams_Macro_Dependencies";
            this.menuItemDiagrams_Macro_Dependencies.Size = new System.Drawing.Size(148, 22);
            this.menuItemDiagrams_Macro_Dependencies.Text = "Dependencies";
            this.menuItemDiagrams_Macro_Dependencies.Click += new System.EventHandler(this.menuItemDiagrams_Macro_Dependencies_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 670);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1045, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControlASTTrees);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1045, 646);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 2;
            // 
            // tabControlASTTrees
            // 
            this.tabControlASTTrees.Controls.Add(this.tabPageComponents);
            this.tabControlASTTrees.Controls.Add(this.tabPageLogicalView);
            this.tabControlASTTrees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlASTTrees.Location = new System.Drawing.Point(0, 0);
            this.tabControlASTTrees.Name = "tabControlASTTrees";
            this.tabControlASTTrees.SelectedIndex = 0;
            this.tabControlASTTrees.Size = new System.Drawing.Size(266, 646);
            this.tabControlASTTrees.TabIndex = 0;
            // 
            // tabPageComponents
            // 
            this.tabPageComponents.Controls.Add(this.treeViewComponents);
            this.tabPageComponents.Location = new System.Drawing.Point(4, 25);
            this.tabPageComponents.Name = "tabPageComponents";
            this.tabPageComponents.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageComponents.Size = new System.Drawing.Size(258, 617);
            this.tabPageComponents.TabIndex = 0;
            this.tabPageComponents.Text = "GMacAST Components";
            this.tabPageComponents.UseVisualStyleBackColor = true;
            // 
            // treeViewComponents
            // 
            this.treeViewComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewComponents.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewComponents.ImageKey = "GMacAST64.png";
            this.treeViewComponents.ImageList = this.imageList1;
            this.treeViewComponents.Location = new System.Drawing.Point(3, 3);
            this.treeViewComponents.Name = "treeViewComponents";
            this.treeViewComponents.SelectedImageIndex = 0;
            this.treeViewComponents.Size = new System.Drawing.Size(252, 611);
            this.treeViewComponents.TabIndex = 0;
            this.treeViewComponents.DoubleClick += new System.EventHandler(this.treeViewComponents_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "GMacAST64.png");
            this.imageList1.Images.SetKeyName(1, "Namespace64.png");
            this.imageList1.Images.SetKeyName(2, "Frame64.png");
            this.imageList1.Images.SetKeyName(3, "BasisVector64.png");
            this.imageList1.Images.SetKeyName(4, "Subspace64.png");
            this.imageList1.Images.SetKeyName(5, "Constant64.png");
            this.imageList1.Images.SetKeyName(6, "Structure64.png");
            this.imageList1.Images.SetKeyName(7, "Macro64.png");
            this.imageList1.Images.SetKeyName(8, "Input64.png");
            this.imageList1.Images.SetKeyName(9, "Output64.png");
            this.imageList1.Images.SetKeyName(10, "Scalar64.png");
            this.imageList1.Images.SetKeyName(11, "Transform64.png");
            // 
            // tabPageLogicalView
            // 
            this.tabPageLogicalView.Location = new System.Drawing.Point(4, 25);
            this.tabPageLogicalView.Name = "tabPageLogicalView";
            this.tabPageLogicalView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLogicalView.Size = new System.Drawing.Size(258, 617);
            this.tabPageLogicalView.TabIndex = 1;
            this.tabPageLogicalView.Text = "Logical View";
            this.tabPageLogicalView.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.textBoxOutput);
            this.splitContainer3.Size = new System.Drawing.Size(775, 646);
            this.splitContainer3.SplitterDistance = 488;
            this.splitContainer3.TabIndex = 0;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOutput.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOutput.Location = new System.Drawing.Point(0, 0);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxOutput.Size = new System.Drawing.Size(775, 488);
            this.textBoxOutput.TabIndex = 0;
            this.textBoxOutput.WordWrap = false;
            // 
            // FormAstExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 692);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStripMain);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStripMain;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1061, 726);
            this.Name = "FormAstExplorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GMac AST Explorer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlASTTrees.ResumeLayout(false);
            this.tabPageComponents.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStripMain;
        private ToolStripMenuItem viewToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private SplitContainer splitContainer1;
        private TabControl tabControlASTTrees;
        private TabPage tabPageComponents;
        private TabPage tabPageLogicalView;
        private TreeView treeViewComponents;
        private SplitContainer splitContainer3;
        private TextBox textBoxOutput;
        private ToolStripMenuItem menuItemView_Close;
        private ToolStripMenuItem menuItemDiagrams;
        private ToolStripMenuItem menuItemDiagrams_Ast;
        private ToolStripMenuItem menuItemDiagrams_Ast_Components;
        private ToolStripMenuItem menuItemDiagrams_Ast_TypesDep;
        private ToolStripMenuItem menuItemDiagrams_Ast_MacrosDep;
        private ToolStripMenuItem menuItemDiagrams_Frame;
        private ToolStripMenuItem menuItemDiagrams_Frame_Components;
        private ToolStripMenuItem menuItemDiagrams_Structure;
        private ToolStripMenuItem menuItemDiagrams_Structure_Components;
        private ToolStripMenuItem menuItemDiagrams_Structure_Dependencies;
        private ToolStripMenuItem menuItemDiagrams_Macro;
        private ToolStripMenuItem menuItemDiagrams_Macro_Components;
        private ToolStripMenuItem menuItemDiagrams_Macro_Dependencies;
        private ToolStripMenuItem menuItemDiagrams_Frame_MvTypeDep;
        private ToolStripMenuItem menuItemDiagrams_General;
        private ToolStripMenuItem menuItemDiagrams_General_GMacAstItems;
        private ToolStripMenuItem menuItemDiagrams_Structure_FullTree;
        private ImageList imageList1;
    }
}