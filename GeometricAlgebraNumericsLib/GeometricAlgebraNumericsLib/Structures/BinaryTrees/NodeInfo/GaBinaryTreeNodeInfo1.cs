using System.Collections.Generic;
using DataStructuresLib;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTrees.NodeInfo
{
    public sealed class GaBinaryTreeNodeInfo1<T>
    {
        public static Stack<GaBinaryTreeNodeInfo1<T>> CreateStack(int treeDepth, IGaBtrNode<T> node)
        {
            var stack = new Stack<GaBinaryTreeNodeInfo1<T>>();

            var rootNodeInfo = new GaBinaryTreeNodeInfo1<T>(
                treeDepth,
                0,
                node
            );

            stack.Push(rootNodeInfo);

            return stack;
        }


        /// <summary>
        /// A reference to the node
        /// </summary>
        public IGaBtrNode<T> Node { get; }

        /// <summary>
        /// The number of tree levels under this node. For leaf nodes this is zero
        /// </summary>
        public int TreeDepth { get; }

        /// <summary>
        /// The ID of this node
        /// </summary>
        public ulong Id { get; }

        /// <summary>
        /// The ID of child 0 of this node
        /// </summary>
        public ulong ChildId0
            => Id;

        /// <summary>
        /// The ID of child 1 of this node
        /// </summary>
        public ulong ChildId1
            => Id | (1ul << (TreeDepth - 1));

        /// <summary>
        /// A bit mask used in traversing trees
        /// </summary>
        public ulong BitMask
            => 1ul << TreeDepth;

        /// <summary>
        /// Cast the node as an internal node
        /// </summary>
        public GaBtrInternalNode<T> NodeAsInternal
            => Node as GaBtrInternalNode<T>;

        /// <summary>
        /// Cast the node as a leaf node
        /// </summary>
        public GaBtrLeafNode<T> NodeAsLeaf
            => Node as GaBtrLeafNode<T>;

        /// <summary>
        /// True if this is a leaf node
        /// </summary>
        public bool IsLeafNode
            => TreeDepth == 0;

        /// <summary>
        /// True if this is not a leaf node
        /// </summary>
        public bool IsInternalNode
            => TreeDepth > 0;

        public T Value
            => Node.Value;

        public IGaBtrNode<T> ChildNode0
            => Node.ChildNode0;

        public IGaBtrNode<T> ChildNode1
            => Node.ChildNode1;

        public GaBtrLeafNode<T> LeafChildNode0
            => Node.LeafChildNode0;

        public GaBtrLeafNode<T> LeafChildNode1
            => Node.LeafChildNode1;

        public bool HasChildNode0
            => Node.HasChildNode0;

        public bool HasChildNode1
            => Node.HasChildNode1;

        public bool HasNoChildNodes
            => Node.HasNoChildNodes;


        internal GaBinaryTreeNodeInfo1(int treeDepth, ulong id, IGaBtrNode<T> node)
        {
            Node = node;
            TreeDepth = treeDepth;
            Id = id;
        }


        /// <summary>
        /// Create node info for child 0
        /// </summary>
        /// <returns></returns>
        public GaBinaryTreeNodeInfo1<T> GetChildNodeInfo0()
        {
            return new GaBinaryTreeNodeInfo1<T>(
                TreeDepth - 1,
                ChildId0,
                Node.ChildNode0
            );
        }

        /// <summary>
        /// Create node info for child 1
        /// </summary>
        /// <returns></returns>
        public GaBinaryTreeNodeInfo1<T> GetChildNodeInfo1()
        {
            return new GaBinaryTreeNodeInfo1<T>(
                TreeDepth - 1,
                ChildId1,
                Node.ChildNode1
            );
        }

        public IEnumerable<GaBinaryTreeNodeInfo1<T>> GetTreeNodesInfo()
        {
            var nodeStack = new Stack<GaBinaryTreeNodeInfo1<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var nodeInfo = nodeStack.Pop();

                yield return
                    nodeInfo;

                if (nodeInfo.IsLeafNode)
                    continue;

                if (nodeInfo.HasChildNode1)
                    nodeStack.Push(nodeInfo.GetChildNodeInfo1());

                if (nodeInfo.HasChildNode0)
                    nodeStack.Push(nodeInfo.GetChildNodeInfo0());
            }
        }

        public IEnumerable<GaBinaryTreeNodeInfo1<T>> GetTreeInternalNodesInfo()
        {
            if (IsLeafNode)
                yield break;

            if (TreeDepth == 1)
            {
                yield return this;
                yield break;
            }

            var nodeStack = new Stack<GaBinaryTreeNodeInfo1<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var nodeInfo = nodeStack.Pop();

                yield return
                    nodeInfo;

                if (nodeInfo.TreeDepth > 1)
                    continue;

                if (nodeInfo.HasChildNode1)
                    nodeStack.Push(nodeInfo.GetChildNodeInfo1());

                if (nodeInfo.HasChildNode0)
                    nodeStack.Push(nodeInfo.GetChildNodeInfo0());
            }
        }

        public IEnumerable<GaBinaryTreeNodeInfo1<T>> GetTreeLeafNodesInfo()
        {
            if (IsLeafNode)
            {
                yield return this;
                yield break;
            }

            var nodeStack = new Stack<GaBinaryTreeNodeInfo1<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var nodeInfo = nodeStack.Pop();

                if (nodeInfo.IsLeafNode)
                {
                    yield return nodeInfo;
                    continue;
                }

                if (nodeInfo.HasChildNode1)
                    nodeStack.Push(nodeInfo.GetChildNodeInfo1());

                if (nodeInfo.HasChildNode0)
                    nodeStack.Push(nodeInfo.GetChildNodeInfo0());
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetTreeLeafValuePairs()
        {
            var nodeStack = new Stack<GaBinaryTreeNodeInfo1<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                if (node.IsLeafNode)
                {
                    yield return new KeyValuePair<ulong, T>(
                        node.Id,
                        node.Value
                    );

                    continue;
                }

                if (node.HasChildNode1)
                    nodeStack.Push(node.GetChildNodeInfo1());

                if (node.HasChildNode0)
                    nodeStack.Push(node.GetChildNodeInfo0());
            }
        }

        public IEnumerable<ulong> GetTreeNodeIDs()
        {
            var nodeStack = new Stack<GaBinaryTreeNodeInfo1<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                yield return node.Id;

                if (node.IsLeafNode)
                    continue;

                if (node.HasChildNode1)
                    nodeStack.Push(node.GetChildNodeInfo1());

                if (node.HasChildNode0)
                    nodeStack.Push(node.GetChildNodeInfo0());
            }
        }

        public IEnumerable<ulong> GetTreeInternalNodeIDs()
        {
            if (TreeDepth < 1)
                yield break;

            if (TreeDepth < 2)
            {
                yield return Id;
                yield break;
            }

            var nodeStack = new Stack<GaBinaryTreeNodeInfo1<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                yield return node.Id;

                if (node.TreeDepth < 1)
                    continue;

                if (node.HasChildNode1)
                    nodeStack.Push(node.GetChildNodeInfo1());

                if (node.HasChildNode0)
                    nodeStack.Push(node.GetChildNodeInfo0());
            }
        }

        public IEnumerable<ulong> GetTreeLeafNodeIDs()
        {
            if (TreeDepth < 2)
            {
                yield return Id;
                yield break;
            }

            var nodeStack = new Stack<GaBinaryTreeNodeInfo1<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                if (node.IsLeafNode)
                {
                    yield return node.Id;

                    continue;
                }

                if (node.HasChildNode1)
                    nodeStack.Push(node.GetChildNodeInfo1());

                if (node.HasChildNode0)
                    nodeStack.Push(node.GetChildNodeInfo0());
            }
        }

        public override string ToString()
        {
            var textComposer = new LinearTextComposer();

            var nodeStack = new Stack<GaBinaryTreeNodeInfo1<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();
                var level = TreeDepth - node.TreeDepth;

                if (node.IsLeafNode)
                {
                    textComposer
                        .AppendAtNewLine("<")
                        .Append(node.Id.PatternToString(level))
                        .Append("> ")
                        .Append("".PadRight(level * 2, ' '))
                        .Append("Leaf { ")
                        .Append(node.Value.ToString())
                        .AppendLine(" }");

                    continue;
                }

                if (node.TreeDepth == TreeDepth)
                    textComposer
                        .AppendAtNewLine("<")
                        .Append("".PadRight(TreeDepth, '-'))
                        .Append("> ")
                        .AppendLine("Root");
                else
                    textComposer
                        .AppendAtNewLine("<")
                        .Append(
                            node.Id
                                .PatternToString(TreeDepth)
                                .Substring(0, level)
                                .PadRight(TreeDepth, '-')
                        )
                        .Append("> ")
                        .Append("".PadRight(level * 2, ' '))
                        .AppendLine("Node");

                if (node.HasChildNode1)
                {
                    nodeStack.Push(node.GetChildNodeInfo1());
                }

                if (node.HasChildNode0)
                {
                    nodeStack.Push(node.GetChildNodeInfo0());
                }
            }

            return textComposer.ToString();
        }
    }
}