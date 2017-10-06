using System;
using System.Windows.Forms;
using GMac.GMacAST;
using GMac.GMacAST.Symbols;
using GMac.GMacAST.Visitors;
using GMac.GMacAST.Visitors.GraphViz;
using GMac.GMacCompiler.Semantic.AST;
using TextComposerLib;
using TextComposerLib.Diagrams.GraphViz.Dot;
using TextComposerLib.Diagrams.GraphViz.UI;

namespace GMac.GMacIDE.AstExplorer
{
    public partial class FormAstExplorer : Form
    {
        private readonly GMacAstDescriptionVisitor _astDescriptionVisitor;

        
        public AstRoot Root { get; }


        internal FormAstExplorer(GMacAst ast)
        {
            InitializeComponent();

            Root = new AstRoot(ast);

            _astDescriptionVisitor = new GMacAstDescriptionVisitor(Root);

            FillComponentsTree();
        }

        private void FillComponentsTree()
        {
            treeViewComponents.Nodes.Clear();

            //treeViewRoles.Sorted = true;

            //treeViewRoles.Nodes.Add(GMacAstToTreeViewNodesByRole.Convert(Root.AssociatedAst));

            var converter = new GMacAstToTreeViewVisitor();
            treeViewComponents.Nodes.Add(converter.Visit(Root));
        }

        private void FillOutputBox(IAstObject item)
        {
            textBoxOutput.Text = item.AcceptVisitor(_astDescriptionVisitor);
        }

        private void ShowDiagram(DotGraph dataGraph)
        {
            var form = new FormGraphVizEditor(dataGraph);

            form.ShowDialog(this);
        }


        private AstStructure GetSelectedStructure()
        {
            if (treeViewComponents.SelectedNode != null)
            {
                var item = treeViewComponents.SelectedNode.Tag as AstStructure;

                if (item.IsNotNullAndValid()) return item;
            }

            MessageBox.Show(@"Please select a structure");

            return null;
        }

        private AstFrame GetSelectedFrame()
        {
            if (treeViewComponents.SelectedNode != null)
            {
                var item = treeViewComponents.SelectedNode.Tag as AstFrame;

                if (item.IsNotNullAndValid()) return item;
            }

            MessageBox.Show(@"Please select a frame");

            return null;
        }

        private AstMacro GetSelectedMacro()
        {
            if (treeViewComponents.SelectedNode != null)
            {
                var item = treeViewComponents.SelectedNode.Tag as AstMacro;

                if (item.IsNotNullAndValid()) return item;
            }

            MessageBox.Show(@"Please select a macro");

            return null;
        }

        private IAstObject GetSelectedAstNode()
        {
            if (treeViewComponents.SelectedNode != null)
            {
                var item = treeViewComponents.SelectedNode.Tag as IAstObject;

                if (ReferenceEquals(item, null) == false && item.IsValid) return item;
            }

            MessageBox.Show(@"Please select a valid GMacAST node");

            return null;
        }


        private void treeViewComponents_DoubleClick(object sender, EventArgs e)
        {
            var node = GetSelectedAstNode();

            if (node == null) return;

            FillOutputBox(node);
        }

        private void menuItemView_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItemDiagrams_Ast_Components_Click(object sender, EventArgs e)
        {
            ShowDiagram(Root.ToGraphViz());
        }

        private void menuItemDiagrams_Ast_TypesDep_Click(object sender, EventArgs e)
        {
            ShowDiagram(Root.GetTypeDependencies().ToGraphViz());
        }

        private void menuItemDiagrams_Ast_MacrosDep_Click(object sender, EventArgs e)
        {
            ShowDiagram(Root.GetMacroDependencies().ToGraphViz());
        }

        private void menuItemDiagrams_Frame_Components_Click(object sender, EventArgs e)
        {
            var node = GetSelectedFrame();

            if (node == null) return;

            ShowDiagram(node.ToGraphViz());
        }

        private void menuItemDiagrams_Frame_MvTypeDep_Click(object sender, EventArgs e)
        {
            var node = GetSelectedFrame();

            if (node == null) return;

            ShowDiagram(node.FrameMultivector.ToDependenciesGraphViz());
        }

        private void menuItemDiagrams_Structure_Components_Click(object sender, EventArgs e)
        {
            var node = GetSelectedStructure();

            if (node == null) return;

            ShowDiagram(node.ToGraphViz());
        }

        private void menuItemDiagrams_Structure_Dependencies_Click(object sender, EventArgs e)
        {
            var node = GetSelectedStructure();

            if (node == null) return;

            ShowDiagram(node.ToDependenciesGraphViz());
        }

        private void menuItemDiagrams_Structure_FullTree_Click(object sender, EventArgs e)
        {
            var node = GetSelectedStructure();

            if (node == null) return;

            ShowDiagram(node.GMacType.ToGraphViz());
        }

        private void menuItemDiagrams_Macro_Components_Click(object sender, EventArgs e)
        {
            var node = GetSelectedMacro();

            if (node == null) return;

            ShowDiagram(node.ToGraphViz());
            //ShowDiagram(GetSelectedMacro());
        }

        private void menuItemDiagrams_Macro_Dependencies_Click(object sender, EventArgs e)
        {
            var node = GetSelectedMacro();

            if (node == null) return;

            ShowDiagram(node.ToDependenciesGraphViz());
        }

        private void menuItemDiagrams_General_GMacAstItems_Click(object sender, EventArgs e)
        {
            ShowDiagram(new AstGeneralDiagrams().GMacAstGeneral());
        }
    }
}
