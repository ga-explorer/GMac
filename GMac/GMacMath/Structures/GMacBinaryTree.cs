using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using TextComposerLib.Diagrams.GraphViz.Dot;
using TextComposerLib.Diagrams.GraphViz.Dot.Color;
using TextComposerLib.Diagrams.GraphViz.Dot.Value;
using TextComposerLib.Text.Linear;

namespace GMac.GMacMath.Structures
{
    public sealed class GMacBinaryTree<T> : IGMacBinaryTreeNode<T>
    {
        public int TreeDepth { get; }

        public IGMacBinaryTreeNode<T> ChildNode0 { get; private set; }

        public IGMacBinaryTreeNode<T> ChildNode1 { get; private set; }

        public GMacBinaryTreeLeafNode<T> LeafChildNode0 
            => ChildNode0 as GMacBinaryTreeLeafNode<T>;

        public GMacBinaryTreeLeafNode<T> LeafChildNode1
            => ChildNode1 as GMacBinaryTreeLeafNode<T>;

        public T Value => default(T);

        public ulong BitMask
            => 1ul << TreeDepth;

        public bool HasChildNode0 
            => !ReferenceEquals(ChildNode0, null);

        public bool HasChildNode1 
            => !ReferenceEquals(ChildNode1, null);

        public bool HasNoChildNodes 
            => ReferenceEquals(ChildNode1, null) && ReferenceEquals(ChildNode0, null);

        public bool IsParentNode => true;

        public bool IsLeafNode => false;

        public IEnumerable<KeyValuePair<ulong, IGMacBinaryTreeNode<T>>> NodePairs
        {
            get
            {
                var nodeStack = new Stack<IGMacBinaryTreeNode<T>>();
                var idStack = new Stack<ulong>();
                nodeStack.Push(this);
                idStack.Push(0ul);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();
                    var id = idStack.Pop();

                    yield return
                        new KeyValuePair<ulong, IGMacBinaryTreeNode<T>>(id, node);

                    if (node.IsLeafNode)
                        continue;

                    if (node.HasChildNode1)
                    {
                        nodeStack.Push(node.ChildNode1);
                        idStack.Push(id | node.ChildNode1.BitMask);
                    }

                    if (node.HasChildNode0)
                    {
                        nodeStack.Push(node.ChildNode0);
                        idStack.Push(id);
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, GMacBinaryTree<T>>> ParentNodePairs
        {
            get
            {
                var nodeStack = new Stack<GMacBinaryTree<T>>();
                var idStack = new Stack<ulong>();
                nodeStack.Push(this);
                idStack.Push(0ul);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();
                    var id = idStack.Pop();

                    yield return
                        new KeyValuePair<ulong, GMacBinaryTree<T>>(id, node);

                    var childNode = node.ChildNode1 as GMacBinaryTree<T>;
                    if (!ReferenceEquals(childNode, null))
                    {
                        nodeStack.Push(childNode);
                        idStack.Push(id | childNode.BitMask);
                    }

                    childNode = node.ChildNode0 as GMacBinaryTree<T>;
                    if (!ReferenceEquals(childNode, null))
                    {
                        nodeStack.Push(childNode);
                        idStack.Push(id);
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, GMacBinaryTreeLeafNode<T>>> LeafNodePairs
        {
            get
            {
                var nodeStack = new Stack<GMacBinaryTree<T>>();
                var idStack = new Stack<ulong>();
                nodeStack.Push(this);
                idStack.Push(0ul);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();
                    var id = idStack.Pop();

                    if (node.HasChildNode1)
                    {
                        var childNode = node.ChildNode1;
                        var childNodeId = id | childNode.BitMask;

                        if (childNode.IsParentNode)
                        {
                            nodeStack.Push(childNode as GMacBinaryTree<T>);
                            idStack.Push(childNodeId);
                        }
                        else
                            yield return new KeyValuePair<ulong, GMacBinaryTreeLeafNode<T>>(
                                childNodeId,
                                childNode as GMacBinaryTreeLeafNode<T>
                            );
                    }

                    if (node.HasChildNode0)
                    {
                        var childNode = node.ChildNode0;
                        var childNodeId = id;

                        if (childNode.IsParentNode)
                        {
                            nodeStack.Push(childNode as GMacBinaryTree<T>);
                            idStack.Push(id);
                        }
                        else
                            yield return new KeyValuePair<ulong, GMacBinaryTreeLeafNode<T>>(
                                childNodeId,
                                childNode as GMacBinaryTreeLeafNode<T>
                            );
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> LeafValuePairs
        {
            get
            {
                var nodeStack = new Stack<IGMacBinaryTreeNode<T>>();
                var idStack = new Stack<ulong>();
                nodeStack.Push(this);
                idStack.Push(0ul);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();
                    var id = idStack.Pop();

                    if (node.IsLeafNode)
                    {
                        yield return
                            new KeyValuePair<ulong, T>(id, node.Value);

                        continue;
                    }

                    if (node.HasChildNode1)
                    {
                        nodeStack.Push(node.ChildNode1);
                        idStack.Push(id | node.ChildNode1.BitMask);
                    }

                    if (node.HasChildNode0)
                    {
                        nodeStack.Push(node.ChildNode0);
                        idStack.Push(id);
                    }
                }
            }
        }

        public IEnumerable<IGMacBinaryTreeNode<T>> Nodes
        {
            get
            {
                var nodeStack = new Stack<IGMacBinaryTreeNode<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    yield return node;

                    if (node.IsLeafNode)
                        continue;

                    if (node.HasChildNode1)
                        nodeStack.Push(node.ChildNode1);

                    if (node.HasChildNode0)
                        nodeStack.Push(node.ChildNode0);
                }
            }
        }

        public IEnumerable<GMacBinaryTree<T>> ParentNodes
        {
            get
            {
                var nodeStack = new Stack<GMacBinaryTree<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    yield return node;

                    var childNode = node.ChildNode1 as GMacBinaryTree<T>;
                    if (!ReferenceEquals(childNode, null))
                        nodeStack.Push(childNode);

                    childNode = node.ChildNode0 as GMacBinaryTree<T>;
                    if (!ReferenceEquals(childNode, null))
                        nodeStack.Push(childNode);
                }
            }
        }

        public IEnumerable<GMacBinaryTreeLeafNode<T>> LeafNodes
        {
            get
            {
                var nodeStack = new Stack<GMacBinaryTree<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    if (node.HasChildNode1)
                    {
                        var childNode = node.ChildNode1;

                        if (childNode.IsParentNode)
                            nodeStack.Push(childNode as GMacBinaryTree<T>);
                        else
                            yield return childNode as GMacBinaryTreeLeafNode<T>;
                    }

                    if (node.HasChildNode0)
                    {
                        var childNode = node.ChildNode0;

                        if (childNode.IsParentNode)
                            nodeStack.Push(childNode as GMacBinaryTree<T>);
                        else
                            yield return childNode as GMacBinaryTreeLeafNode<T>;
                    }
                }
            }
        }

        public IEnumerable<ulong> NodeIDs
        {
            get
            {
                var nodeStack = new Stack<IGMacBinaryTreeNode<T>>();
                var idStack = new Stack<ulong>();
                nodeStack.Push(this);
                idStack.Push(0ul);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();
                    var id = idStack.Pop();

                    yield return id;

                    if (node.IsLeafNode)
                        continue;

                    if (node.HasChildNode1)
                    {
                        nodeStack.Push(node.ChildNode1);
                        idStack.Push(id | node.ChildNode1.BitMask);
                    }

                    if (node.HasChildNode0)
                    {
                        nodeStack.Push(node.ChildNode0);
                        idStack.Push(id);
                    }
                }
            }
        }

        public IEnumerable<ulong> ParentNodeIDs
        {
            get
            {
                var nodeStack = new Stack<GMacBinaryTree<T>>();
                var idStack = new Stack<ulong>();
                nodeStack.Push(this);
                idStack.Push(0ul);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();
                    var id = idStack.Pop();

                    yield return id;

                    var childNode = node.ChildNode1 as GMacBinaryTree<T>;
                    if (!ReferenceEquals(childNode, null))
                    {
                        nodeStack.Push(childNode);
                        idStack.Push(id | childNode.BitMask);
                    }

                    childNode = node.ChildNode0 as GMacBinaryTree<T>;
                    if (!ReferenceEquals(childNode, null))
                    {
                        nodeStack.Push(childNode);
                        idStack.Push(id);
                    }
                }
            }
        }

        public IEnumerable<ulong> LeafNodeIDs
        {
            get
            {
                var nodeStack = new Stack<GMacBinaryTree<T>>();
                var idStack = new Stack<ulong>();
                nodeStack.Push(this);
                idStack.Push(0ul);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();
                    var id = idStack.Pop();

                    if (node.HasChildNode0)
                    {
                        var childNode = node.ChildNode0;
                        var childNodeId = id;

                        if (childNode.IsParentNode)
                        {
                            nodeStack.Push(childNode as GMacBinaryTree<T>);
                            idStack.Push(childNodeId);
                        }
                        else
                            yield return childNodeId;
                    }

                    if (node.HasChildNode1)
                    {
                        var childNode = node.ChildNode1;
                        var childNodeId = id | childNode.BitMask;

                        if (childNode.IsParentNode)
                        {
                            nodeStack.Push(childNode as GMacBinaryTree<T>);
                            idStack.Push(childNodeId);
                        }
                        else
                            yield return childNodeId;
                    }
                }
            }
        }

        public IEnumerable<T> LeafValues
        {
            get
            {
                var nodeStack = new Stack<IGMacBinaryTreeNode<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    if (node.IsLeafNode)
                    {
                        yield return node.Value;

                        continue;
                    }

                    if (node.HasChildNode1)
                        nodeStack.Push(node.ChildNode1);

                    if (node.HasChildNode0)
                        nodeStack.Push(node.ChildNode0);
                }
            }
        }


        public GMacBinaryTree(int treeDepth)
        {
            Debug.Assert(treeDepth > 0 && treeDepth < 64);

            TreeDepth = treeDepth;
        }


        /// <summary>
        /// Remove parent nodes having no child nodes in this tree
        /// </summary>
        /// <returns></returns>
        public GMacBinaryTree<T> RemoveEmptyDescendantNodes()
        {
            //TODO: Implement this using stacks
            if (HasChildNode1)
            {
                var childNode = ChildNode1 as GMacBinaryTree<T>;

                if (!ReferenceEquals(childNode, null))
                {
                    childNode.RemoveEmptyDescendantNodes();

                    if (childNode.HasNoChildNodes)
                        ChildNode1 = null;
                }
            }

            if (HasChildNode0)
            {
                var childNode = ChildNode0 as GMacBinaryTree<T>;

                if (!ReferenceEquals(childNode, null))
                {
                    childNode.RemoveEmptyDescendantNodes();

                    if (childNode.HasNoChildNodes)
                        ChildNode0 = null;
                }
            }

            return this;
        }

        /// <summary>
        /// Remove all leafs from tree where selectionFunc is true for the leaf value, and then remove
        /// parent nodes having no child nodes.
        /// </summary>
        /// <param name="selectionFunc"></param>
        /// <returns></returns>
        public GMacBinaryTree<T> TrimByLeafValues(Func<T, bool> selectionFunc)
        {
            var nodeStack = new Stack<GMacBinaryTree<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                if (node.HasChildNode1)
                {
                    var childNode = node.ChildNode1;

                    if (childNode.IsParentNode)
                        nodeStack.Push(childNode as GMacBinaryTree<T>);

                    else if (selectionFunc(childNode.Value))
                        node.ChildNode1 = null;
                }

                if (node.HasChildNode0)
                {
                    var childNode = node.ChildNode0;

                    if (childNode.IsParentNode)
                        nodeStack.Push(childNode as GMacBinaryTree<T>);

                    else if (selectionFunc(childNode.Value))
                        node.ChildNode0 = null;
                }
            }

            return RemoveEmptyDescendantNodes();
        }

        /// <summary>
        /// Remove all leafs from tree where selectionFunc is true for the leaf id and value, and then remove
        /// parent nodes having no child nodes.
        /// </summary>
        /// <param name="selectionFunc"></param>
        /// <returns></returns>
        public GMacBinaryTree<T> TrimByLeafValuePairs(Func<ulong, T, bool> selectionFunc)
        {
            var nodeStack = new Stack<GMacBinaryTree<T>>();
            var idStack = new Stack<ulong>();
            nodeStack.Push(this);
            idStack.Push(0ul);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();
                var id = idStack.Pop();

                if (node.HasChildNode1)
                {
                    var childNode = node.ChildNode1;
                    var childNodeId = id | childNode.BitMask;

                    if (childNode.IsParentNode)
                    {
                        nodeStack.Push(childNode as GMacBinaryTree<T>);
                        idStack.Push(childNodeId);
                    }
                    else if (selectionFunc(childNodeId, childNode.Value))
                        node.ChildNode1 = null;
                }

                if (node.HasChildNode0)
                {
                    var childNode = node.ChildNode0;
                    var childNodeId = id;

                    if (childNode.IsParentNode)
                    {
                        nodeStack.Push(childNode as GMacBinaryTree<T>);
                        idStack.Push(childNodeId);
                    }
                    else if (selectionFunc(childNodeId, childNode.Value))
                        node.ChildNode0 = null;
                }
            }

            return RemoveEmptyDescendantNodes();
        }


        public GMacBinaryTreeLeafNode<T> GetLeafNode(ulong index)
        {
            var node = this;
            ulong bitPattern;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GMacBinaryTree<T>;

                if (ReferenceEquals(node, null))
                    return null;
            }

            bitPattern = 1ul & index;
            return node.GetChildNode(bitPattern != 0) as GMacBinaryTreeLeafNode<T>;
        }

        public bool TryGetLeafNode(ulong index, out GMacBinaryTreeLeafNode<T> leafNode)
        {
            var node = this;
            ulong bitPattern;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GMacBinaryTree<T>;

                if (ReferenceEquals(node, null))
                {
                    leafNode = null;
                    return false;
                }
            }

            bitPattern = 1ul & index;
            leafNode = node.GetChildNode(bitPattern != 0) as GMacBinaryTreeLeafNode<T>;
            return !ReferenceEquals(leafNode, null);
        }

        public T GetLeafValue(ulong index)
        {
            var node = this;
            ulong bitPattern;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GMacBinaryTree<T>;

                if (ReferenceEquals(node, null))
                    return default(T);
            }

            bitPattern = 1ul & index;
            var leafNode = node.GetChildNode(bitPattern != 0) as GMacBinaryTreeLeafNode<T>;

            return ReferenceEquals(leafNode, null) ? default(T) : leafNode.Value;
        }

        public bool TryGetLeafValue(ulong index, out T value)
        {
            var node = this;
            ulong bitPattern;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GMacBinaryTree<T>;

                if (ReferenceEquals(node, null))
                {
                    value = default(T);
                    return false;
                }
            }

            bitPattern = 1ul & index;
            var leafNode = node.GetChildNode(bitPattern != 0) as GMacBinaryTreeLeafNode<T>;

            if (ReferenceEquals(leafNode, null))
            {
                value = default(T);
                return false;
            }

            value = leafNode.Value;
            return true;
        }

        public KeyValuePair<ulong, GMacBinaryTreeLeafNode<T>> GetLeafNodePair(ulong id)
        {
            return new KeyValuePair<ulong, GMacBinaryTreeLeafNode<T>>(
                id, 
                GetLeafNode(id)
            );
        }

        public KeyValuePair<ulong, T> GetValuePair(ulong id)
        {
            return new KeyValuePair<ulong, T>(id, GetLeafValue(id));
        }

        public GMacBinaryTree<T> SetLeafValue(ulong index, T value)
        {
            var node = this;
            for (var i = TreeDepth - 1; i > 0; i--)
            {
                var bitPattern = (1ul << i) & index;
                node = node.GetOrAddInternalChildNode(bitPattern != 0);
            }

            node.SetOrAddLeafChildNode((1ul & index) != 0, value);

            return this;
        }

        public bool ContainsLeafNodeId(ulong index)
        {
            var node = this;
            ulong bitPattern;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GMacBinaryTree<T>;

                if (ReferenceEquals(node, null))
                    return false;
            }

            bitPattern = 1ul & index;
            var leafNode = node.GetChildNode(bitPattern != 0) as GMacBinaryTreeLeafNode<T>;

            return !ReferenceEquals(leafNode, null);
        }

        public bool HasChildNode(bool rightChild)
        {
            return !ReferenceEquals(rightChild ? ChildNode1 : ChildNode0, null);
        }

        public IGMacBinaryTreeNode<T> GetChildNode(bool rightChild)
        {
            return rightChild ? ChildNode1 : ChildNode0;
        }

        public ulong ChildId0(ulong id)
        {
            return id;
        }

        public ulong ChildId1(ulong id)
        {
            return id | (1ul << (TreeDepth - 1));
        }

        /// <summary>
        /// Try to get a child node if it exists, else create a new internal child and return it
        /// </summary>
        /// <param name="rightChild"></param>
        /// <returns></returns>
        public GMacBinaryTree<T> GetOrAddInternalChildNode(bool rightChild)
        {
            Debug.Assert(TreeDepth > 1);

            if (rightChild)
            {
                var childNode = ChildNode1 as GMacBinaryTree<T>;

                if (!ReferenceEquals(childNode, null))
                    return childNode;

                childNode = new GMacBinaryTree<T>(TreeDepth - 1);
                ChildNode1 = childNode;

                return childNode;
            }
            else
            {
                var childNode = ChildNode0 as GMacBinaryTree<T>;

                if (!ReferenceEquals(childNode, null))
                    return childNode;

                childNode = new GMacBinaryTree<T>(TreeDepth - 1);
                ChildNode0 = childNode;

                return childNode;
            }
        }

        public void SetOrAddLeafChildNode(bool rightChild, T value)
        {
            Debug.Assert(TreeDepth == 1);

            if (rightChild)
            {
                var childNode = ChildNode1 as GMacBinaryTreeLeafNode<T>;

                if (!ReferenceEquals(childNode, null))
                    childNode.Value = value;
                else
                    ChildNode1 = new GMacBinaryTreeLeafNode<T>(value);
            }
            else
            {
                var childNode = ChildNode0 as GMacBinaryTreeLeafNode<T>;

                if (!ReferenceEquals(childNode, null))
                    childNode.Value = value;
                else
                    ChildNode0 = new GMacBinaryTreeLeafNode<T>(value);
            }
        }

        public GMacBinaryTree<T> ResetInternalChildNode0()
        {
            var childNode = new GMacBinaryTree<T>(TreeDepth - 1);

            ChildNode0 = childNode;

            return childNode;
        }

        public GMacBinaryTree<T> ResetInternalChildNode1()
        {
            var childNode = new GMacBinaryTree<T>(TreeDepth - 1);

            ChildNode1 = childNode;

            return childNode;
        }

        public GMacBinaryTreeLeafNode<T> ResetLeafChildNode0(T value)
        {
            Debug.Assert(TreeDepth == 1);

            var childNode = new GMacBinaryTreeLeafNode<T>(value);

            ChildNode0 = childNode;

            return childNode;
        }

        public GMacBinaryTreeLeafNode<T> ResetLeafChildNode1(T value)
        {
            Debug.Assert(TreeDepth == 1);

            var childNode = new GMacBinaryTreeLeafNode<T>(value);

            ChildNode1 = childNode;

            return childNode;
        }

        public GMacBinaryTree<T> RemoveChildNode1()
        {
            ChildNode1 = null;

            return this;
        }

        public GMacBinaryTree<T> RemoveChildNode0()
        {
            ChildNode0 = null;

            return this;
        }

        public GMacBinaryTree<T> RemoveChildNodes()
        {
            ChildNode1 = null;
            ChildNode0 = null;

            return this;
        }

        public GMacBinaryTree<T> RemoveChildNode(bool rightChild)
        {
            if (rightChild)
                ChildNode1 = null;
            else
                ChildNode0 = null;

            return this;
        }


        public GMacBinaryTree<T> FillFromTree(GMacBinaryTree<T> sourceTree)
        {
            if (TreeDepth != sourceTree.TreeDepth)
                throw new InvalidOperationException("Tree depth mismatch");

            var sourceNodeStack = new Stack<GMacBinaryTree<T>>();
            var targetNodeStack = new Stack<GMacBinaryTree<T>>();

            sourceNodeStack.Push(sourceTree);
            targetNodeStack.Push(this);

            while (sourceNodeStack.Count > 0)
            {
                var sourceNode = sourceNodeStack.Pop();
                var targetNode = targetNodeStack.Pop();

                if (sourceNode.HasChildNode0)
                {
                    var sourceChildNode = sourceNode.ChildNode0;

                    if (sourceChildNode.IsParentNode)
                    {
                        sourceNodeStack.Push(sourceChildNode as GMacBinaryTree<T>);
                        targetNodeStack.Push(targetNode.ResetInternalChildNode0());
                    }
                    else
                        targetNode.ResetLeafChildNode0(sourceChildNode.Value);
                }

                if (sourceNode.HasChildNode1)
                {
                    var sourceChildNode = sourceNode.ChildNode1;

                    if (sourceChildNode.IsParentNode)
                    {
                        sourceNodeStack.Push(sourceChildNode as GMacBinaryTree<T>);
                        targetNodeStack.Push(targetNode.ResetInternalChildNode1());
                    }
                    else
                        targetNode.ResetLeafChildNode1(sourceChildNode.Value);
                }
            }

            return this;
        }

        public GMacBinaryTree<T> FillFromTree<TU>(GMacBinaryTree<TU> sourceTree, Func<TU, T> valueConversionFunc)
        {
            if (TreeDepth != sourceTree.TreeDepth)
                throw new InvalidOperationException("Tree depth mismatch");

            var sourceNodeStack = new Stack<GMacBinaryTree<TU>>();
            var targetNodeStack = new Stack<GMacBinaryTree<T>>();

            sourceNodeStack.Push(sourceTree);
            targetNodeStack.Push(this);

            while (sourceNodeStack.Count > 0)
            {
                var sourceNode = sourceNodeStack.Pop();
                var targetNode = targetNodeStack.Pop();

                if (sourceNode.HasChildNode1)
                {
                    var sourceChildNode = sourceNode.ChildNode1;

                    if (sourceChildNode.IsParentNode)
                    {
                        sourceNodeStack.Push(sourceChildNode as GMacBinaryTree<TU>);
                        targetNodeStack.Push(targetNode.ResetInternalChildNode1());
                    }
                    else
                        targetNode.ResetLeafChildNode1(valueConversionFunc(sourceChildNode.Value));
                }

                if (sourceNode.HasChildNode0)
                {
                    var sourceChildNode = sourceNode.ChildNode0;

                    if (sourceChildNode.IsParentNode)
                    {
                        sourceNodeStack.Push(sourceChildNode as GMacBinaryTree<TU>);
                        targetNodeStack.Push(targetNode.ResetInternalChildNode0());
                    }
                    else
                        targetNode.ResetLeafChildNode0(
                            valueConversionFunc(sourceChildNode.Value)
                        );
                }
            }

            return this;
        }

        public GMacBinaryTree<TU> MapLeafValues<TU>(Func<T, TU> valueConversionFunc)
        {
            return new GMacBinaryTree<TU>(TreeDepth).FillFromTree(this, valueConversionFunc);
        }


        public Stack<ulong> CreateNodeIDsStack()
        {
            var stack = new Stack<ulong>();

            stack.Push(0ul);

            return stack;
        }

        public Stack<IGMacBinaryTreeNode<T>> CreateNodesStack()
        {
            var stack = new Stack<IGMacBinaryTreeNode<T>>();

            stack.Push(this);

            return stack;
        }

        public Stack<GMacBinaryTree<T>> CreateParentNodesStack()
        {
            var stack = new Stack<GMacBinaryTree<T>>();

            stack.Push(this);

            return stack;
        }


        public override string ToString()
        {
            var textComposer = new LinearComposer();

            var idStack = new Stack<ulong>();
            var nodeStack = new Stack<IGMacBinaryTreeNode<T>>();

            idStack.Push(0ul);
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var id = idStack.Pop();
                var node = nodeStack.Pop();
                var level = TreeDepth - node.TreeDepth;

                if (node.IsLeafNode)
                {
                    textComposer
                        .AppendAtNewLine("<")
                        .Append(id.PatternToString(level))
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
                            id
                                .PatternToString(TreeDepth)
                                .Substring(0, level)
                                .PadRight(TreeDepth, '-')
                        )
                        .Append("> ")
                        .Append("".PadRight(level * 2, ' '))
                        .AppendLine("Node");

                if (node.HasChildNode1)
                {
                    idStack.Push(id | node.ChildNode1.BitMask);
                    nodeStack.Push(node.ChildNode1);
                }

                if (node.HasChildNode0)
                {
                    idStack.Push(id);
                    nodeStack.Push(node.ChildNode0);
                }
            }

            return textComposer.ToString();
        }
    }
}