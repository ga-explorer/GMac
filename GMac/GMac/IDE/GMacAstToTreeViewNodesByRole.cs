using System.Windows.Forms;
using CodeComposerLib.Irony.Semantic;
using GMac.Engine.Compiler.Semantic.AST;
using Microsoft.CSharp.RuntimeBinder;

namespace GMac.IDE
{
    public sealed class GMacAstToTreeViewNodesByRole : IAstNodeDynamicVisitor
    {
        public static TreeNode Convert(GMacAst ast)
        {
            var converter = new GMacAstToTreeViewNodesByRole();

            converter.Visit(ast);

            return converter.DslNode;
        }


        public readonly int NamespacesNode = 0;

        public readonly int FramesNode = 1;

        public readonly int FrameSubspacesNode = 2;

        public readonly int ConstantsNode = 3;

        public readonly int StructuresNode = 4;

        public readonly int MacrosNode = 5;

        public readonly int MacroTemplatesNode = 6;

        public readonly int TransformsNode = 7;


        public TreeNode[] RoleNodes;


        public TreeNode DslNode { get; private set; }

        public bool IgnoreNullElements => true;

        public bool UseExceptions => false;


        private GMacAstToTreeViewNodesByRole()
        {
        }


        private void InitializeRoleNodes(GMacAst dsl)
        {
            RoleNodes = new TreeNode[8];

            RoleNodes[NamespacesNode] = new TreeNode("Namespaces");

            RoleNodes[FramesNode] = new TreeNode("Frames");

            RoleNodes[FrameSubspacesNode] = new TreeNode("Subspaces");

            RoleNodes[ConstantsNode] = new TreeNode("Constants");

            RoleNodes[StructuresNode] = new TreeNode("Structures");

            RoleNodes[MacrosNode] = new TreeNode("Macros");

            RoleNodes[MacroTemplatesNode] = new TreeNode("Macro Templates");

            RoleNodes[TransformsNode] = new TreeNode("Transforms");


            DslNode = new TreeNode("GMacDSL") {Tag = dsl};

            DslNode.Nodes.AddRange(RoleNodes);

            DslNode.Expand();
        }


        public void Visit(GMacStructure structure)
        {
            var node = new TreeNode(structure.SymbolAccessName) {Tag = structure};

            RoleNodes[StructuresNode].Nodes.Add(node);
        }

        public void Visit(GMacMacroTemplate macroTemplate)
        {
            var node = new TreeNode(macroTemplate.SymbolAccessName) {Tag = macroTemplate};

            RoleNodes[MacroTemplatesNode].Nodes.Add(node);
        }

        public void Visit(GMacMultivectorTransform transform)
        {
            var nodeName =
                transform.SymbolAccessName + " : " + 
                transform.SourceFrame.SymbolAccessName + " -> " +
                transform.TargetFrame.SymbolAccessName;

            var node = new TreeNode(nodeName) { Tag = transform };

            RoleNodes[TransformsNode].Nodes.Add(node);
        }

        public void Visit(GMacMacro macro)
        {
            var node = new TreeNode(macro.SymbolAccessName) {Tag = macro};

            RoleNodes[MacrosNode].Nodes.Add(node);
        }

        public void Visit(GMacConstant constant)
        {
            var node = new TreeNode(constant.SymbolAccessName) {Tag = constant};

            RoleNodes[ConstantsNode].Nodes.Add(node);
        }

        public void Visit(GMacFrameSubspace subspace)
        {
            var node = new TreeNode(subspace.SymbolAccessName) {Tag = subspace};

            RoleNodes[FrameSubspacesNode].Nodes.Add(node);
        }

        public void Visit(GMacFrame frame)
        {
            var node = new TreeNode(frame.SymbolAccessName) {Tag = frame};

            RoleNodes[FramesNode].Nodes.Add(node);

            foreach (var childSymbol in frame.FrameSubspaces)
                Visit(childSymbol);

            foreach (var childSymbol in frame.ChildConstants)
                Visit(childSymbol);

            foreach (var childSymbol in frame.Structures)
                Visit(childSymbol);

            foreach (var childSymbol in frame.ChildMacros)
                Visit(childSymbol);
        }

        public void Visit(GMacNamespace nameSpace)
        {
            var node = new TreeNode(nameSpace.SymbolAccessName) {Tag = nameSpace};

            RoleNodes[NamespacesNode].Nodes.Add(node);

            foreach (var childSymbol in nameSpace.ChildNamespaces)
                Visit(childSymbol);

            foreach (var childSymbol in nameSpace.ChildFrames)
                Visit(childSymbol);

            foreach (var childSymbol in nameSpace.ChildConstants)
                Visit(childSymbol);

            foreach (var childSymbol in nameSpace.ChildStructures)
                Visit(childSymbol);

            foreach (var childSymbol in nameSpace.ChildTransforms)
                Visit(childSymbol);

            foreach (var childSymbol in nameSpace.ChildMacros)
                Visit(childSymbol);

            foreach (var childSymbol in nameSpace.ChildMacroTemplates)
                Visit(childSymbol);
        }

        public void Visit(GMacAst dsl)
        {
            InitializeRoleNodes(dsl);

            foreach (var childSymbol in dsl.ChildNamespaces)
                Visit(childSymbol);
        }


        public void Fallback(IIronyAstObject objItem, RuntimeBinderException excException)
        {
        }
    }
}
