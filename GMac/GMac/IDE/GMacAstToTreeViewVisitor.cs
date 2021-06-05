using System.Linq;
using System.Windows.Forms;
using GMac.Engine.AST;
using GMac.Engine.AST.Symbols;
using GMac.Engine.AST.Visitors;
using Microsoft.CSharp.RuntimeBinder;

namespace GMac.IDE
{
    internal sealed class GMacAstToTreeViewVisitor : IAstObjectDynamicVisitor<TreeNode>
    {
        public bool IgnoreNullElements => false;

        public bool UseExceptions => false;

        public TreeNode Fallback(IAstObject item, RuntimeBinderException excException)
        {
            if (ReferenceEquals(item, null))
                return new TreeNode("Null Node!");

            return new TreeNode("Unhandled Node Object");
        }


        public TreeNode Visit(AstNamespace item)
        {
            var node = new TreeNode(item.Name)
            {
                Tag = item,
                ImageKey = @"Namespace64.png",
                SelectedImageKey = @"Namespace64.png"
            };

            node.Nodes.AddRange(
                item.ChildNamespaces.OrderBy(i => i.Name).Select(Visit).ToArray()
                );

            node.Nodes.AddRange(
                item.ChildFrames.OrderBy(i => i.Name).Select(Visit).ToArray()
                );

            node.Nodes.AddRange(
                item.ChildConstants.OrderBy(i => i.Name).Select(Visit).ToArray()
                );

            node.Nodes.AddRange(
                item.ChildStructures.OrderBy(i => i.Name).Select(Visit).ToArray()
                );

            node.Nodes.AddRange(
                item.ChildTransforms.OrderBy(i => i.Name).Select(Visit).ToArray()
                );

            node.Nodes.AddRange(
                item.ChildMacros.OrderBy(i => i.Name).Select(Visit).ToArray()
                );

            return node;
        }

        public TreeNode Visit(AstConstant item)
        {
            var node = new TreeNode(item.Name)
            {
                Tag = item,
                ImageKey = @"Constant64.png",
                SelectedImageKey = @"Constant64.png"
            };

            return node;
        }

        public TreeNode Visit(AstStructure item)
        {
            var node = new TreeNode(item.Name)
            {
                Tag = item,
                ImageKey = @"Structure64.png",
                SelectedImageKey = @"Structure64.png"
            };

            return node;
        }

        public TreeNode Visit(AstMacro item)
        {
            var node = new TreeNode(item.Name)
            {
                Tag = item,
                ImageKey = @"Macro64.png",
                SelectedImageKey = @"Macro64.png"
            };

            return node;
        }

        public TreeNode Visit(AstFrameSubspace item)
        {
            var node = new TreeNode(item.Name)
            {
                Tag = item,
                ImageKey = @"Subspace64.png",
                SelectedImageKey = @"Subspace64.png"
            };

            return node;
        }

        public TreeNode Visit(AstTransform item)
        {
            var node = new TreeNode(item.Name)
            {
                Tag = item,
                ImageKey = @"Transform64.png",
                SelectedImageKey = @"Transform64.png"
            };

            return node;
        }

        public TreeNode Visit(AstFrameMultivector item)
        {
            var node = new TreeNode(item.Name)
            {
                Tag = item,
                ImageKey = @"Frame64.png",
                SelectedImageKey = @"Frame64.png"
            };

            return node;
        }

        public TreeNode Visit(AstFrame item)
        {
            var node = new TreeNode(item.Name)
            {
                Tag = item,
                ImageKey = @"Frame64.png",
                SelectedImageKey = @"Frame64.png"
            };

            node.Nodes.Add(
                Visit(item.FrameMultivector)
                );

            node.Nodes.AddRange(
                item.Subspaces.OrderBy(i => i.Name).Select(Visit).ToArray()
                );

            node.Nodes.AddRange(
                item.Constants.OrderBy(i => i.Name).Select(Visit).ToArray()
                );

            node.Nodes.AddRange(
                item.Structures.OrderBy(i => i.Name).Select(Visit).ToArray()
                );

            node.Nodes.AddRange(
                item.Transforms.OrderBy(i => i.Name).Select(Visit).ToArray()
                );

            node.Nodes.AddRange(
                item.Macros.OrderBy(i => i.Name).Select(Visit).ToArray()
                );

            return node;
        }

        public TreeNode Visit(AstType item)
        {
            var node = new TreeNode(item.GMacTypeSignature)
            {
                Tag = item,
                ImageKey = @"Scalar64.png",
                SelectedImageKey = @"Scalar64.png"
            };

            return node;
        }

        public TreeNode Visit(AstRoot item)
        {
            var node = new TreeNode("Root")
            {
                Tag = item,
                ImageKey = @"GMacAST64.png",
                SelectedImageKey = @"GMacAST64.png"
            };

            node.Nodes.Add(
                Visit(item.ScalarType)
                );

            node.Nodes.AddRange(
                item.ChildNamespaces.OrderBy(i => i.Name).Select(Visit).ToArray()
                );

            return node;
        }
    }
}
