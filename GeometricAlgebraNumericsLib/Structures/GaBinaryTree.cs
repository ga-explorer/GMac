using System;
using System.Collections.Generic;
using System.Diagnostics;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraNumericsLib.Structures
{
    public sealed class GaBinaryTree<T> : IGaBinaryTreeNode<T>
    {
        public int TreeDepth { get; }

        public IGaBinaryTreeNode<T> ChildNode0 { get; private set; }

        public IGaBinaryTreeNode<T> ChildNode1 { get; private set; }

        public GaBinaryTreeLeafNode<T> LeafChildNode0 
            => ChildNode0 as GaBinaryTreeLeafNode<T>;

        public GaBinaryTreeLeafNode<T> LeafChildNode1
            => ChildNode1 as GaBinaryTreeLeafNode<T>;

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

        public IEnumerable<KeyValuePair<ulong, IGaBinaryTreeNode<T>>> NodePairs
        {
            get
            {
                var nodeStack = new Stack<IGaBinaryTreeNode<T>>();
                var idStack = new Stack<ulong>();
                nodeStack.Push(this);
                idStack.Push(0ul);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();
                    var id = idStack.Pop();

                    yield return
                        new KeyValuePair<ulong, IGaBinaryTreeNode<T>>(id, node);

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

        public IEnumerable<KeyValuePair<ulong, GaBinaryTree<T>>> ParentNodePairs
        {
            get
            {
                var nodeStack = new Stack<GaBinaryTree<T>>();
                var idStack = new Stack<ulong>();
                nodeStack.Push(this);
                idStack.Push(0ul);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();
                    var id = idStack.Pop();

                    yield return
                        new KeyValuePair<ulong, GaBinaryTree<T>>(id, node);

                    var childNode = node.ChildNode1 as GaBinaryTree<T>;
                    if (!ReferenceEquals(childNode, null))
                    {
                        nodeStack.Push(childNode);
                        idStack.Push(id | childNode.BitMask);
                    }

                    childNode = node.ChildNode0 as GaBinaryTree<T>;
                    if (!ReferenceEquals(childNode, null))
                    {
                        nodeStack.Push(childNode);
                        idStack.Push(id);
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, GaBinaryTreeLeafNode<T>>> LeafNodePairs
        {
            get
            {
                var nodeStack = new Stack<GaBinaryTree<T>>();
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
                            nodeStack.Push(childNode as GaBinaryTree<T>);
                            idStack.Push(childNodeId);
                        }
                        else
                            yield return new KeyValuePair<ulong, GaBinaryTreeLeafNode<T>>(
                                childNodeId,
                                childNode as GaBinaryTreeLeafNode<T>
                            );
                    }

                    if (node.HasChildNode0)
                    {
                        var childNode = node.ChildNode0;
                        var childNodeId = id;

                        if (childNode.IsParentNode)
                        {
                            nodeStack.Push(childNode as GaBinaryTree<T>);
                            idStack.Push(id);
                        }
                        else
                            yield return new KeyValuePair<ulong, GaBinaryTreeLeafNode<T>>(
                                childNodeId,
                                childNode as GaBinaryTreeLeafNode<T>
                            );
                    }
                }
            }
        }

        public IEnumerable<KeyValuePair<ulong, T>> LeafValuePairs
        {
            get
            {
                var nodeStack = new Stack<IGaBinaryTreeNode<T>>();
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

        public IEnumerable<IGaBinaryTreeNode<T>> Nodes
        {
            get
            {
                var nodeStack = new Stack<IGaBinaryTreeNode<T>>();
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

        public IEnumerable<GaBinaryTree<T>> ParentNodes
        {
            get
            {
                var nodeStack = new Stack<GaBinaryTree<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    yield return node;

                    var childNode = node.ChildNode1 as GaBinaryTree<T>;
                    if (!ReferenceEquals(childNode, null))
                        nodeStack.Push(childNode);

                    childNode = node.ChildNode0 as GaBinaryTree<T>;
                    if (!ReferenceEquals(childNode, null))
                        nodeStack.Push(childNode);
                }
            }
        }

        public IEnumerable<GaBinaryTreeLeafNode<T>> LeafNodes
        {
            get
            {
                var nodeStack = new Stack<GaBinaryTree<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    if (node.HasChildNode1)
                    {
                        var childNode = node.ChildNode1;

                        if (childNode.IsParentNode)
                            nodeStack.Push(childNode as GaBinaryTree<T>);
                        else
                            yield return childNode as GaBinaryTreeLeafNode<T>;
                    }

                    if (node.HasChildNode0)
                    {
                        var childNode = node.ChildNode0;

                        if (childNode.IsParentNode)
                            nodeStack.Push(childNode as GaBinaryTree<T>);
                        else
                            yield return childNode as GaBinaryTreeLeafNode<T>;
                    }
                }
            }
        }

        public IEnumerable<ulong> NodeIDs
        {
            get
            {
                var nodeStack = new Stack<IGaBinaryTreeNode<T>>();
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
                var nodeStack = new Stack<GaBinaryTree<T>>();
                var idStack = new Stack<ulong>();
                nodeStack.Push(this);
                idStack.Push(0ul);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();
                    var id = idStack.Pop();

                    yield return id;

                    var childNode = node.ChildNode1 as GaBinaryTree<T>;
                    if (!ReferenceEquals(childNode, null))
                    {
                        nodeStack.Push(childNode);
                        idStack.Push(id | childNode.BitMask);
                    }

                    childNode = node.ChildNode0 as GaBinaryTree<T>;
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
                var nodeStack = new Stack<GaBinaryTree<T>>();
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
                            nodeStack.Push(childNode as GaBinaryTree<T>);
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
                            nodeStack.Push(childNode as GaBinaryTree<T>);
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
                var nodeStack = new Stack<IGaBinaryTreeNode<T>>();
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


        public GaBinaryTree(int treeDepth)
        {
            Debug.Assert(treeDepth > 0 && treeDepth < 64);

            TreeDepth = treeDepth;
        }


        /// <summary>
        /// Remove parent nodes having no child nodes in this tree
        /// </summary>
        /// <returns></returns>
        public GaBinaryTree<T> RemoveEmptyDescendantNodes()
        {
            //TODO: Implement this using stacks
            if (HasChildNode1)
            {
                var childNode = ChildNode1 as GaBinaryTree<T>;

                if (!ReferenceEquals(childNode, null))
                {
                    childNode.RemoveEmptyDescendantNodes();

                    if (childNode.HasNoChildNodes)
                        ChildNode1 = null;
                }
            }

            if (HasChildNode0)
            {
                var childNode = ChildNode0 as GaBinaryTree<T>;

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
        public GaBinaryTree<T> TrimByLeafValues(Func<T, bool> selectionFunc)
        {
            var nodeStack = new Stack<GaBinaryTree<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                if (node.HasChildNode1)
                {
                    var childNode = node.ChildNode1;

                    if (childNode.IsParentNode)
                        nodeStack.Push(childNode as GaBinaryTree<T>);

                    else if (selectionFunc(childNode.Value))
                        node.ChildNode1 = null;
                }

                if (node.HasChildNode0)
                {
                    var childNode = node.ChildNode0;

                    if (childNode.IsParentNode)
                        nodeStack.Push(childNode as GaBinaryTree<T>);

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
        public GaBinaryTree<T> TrimByLeafValuePairs(Func<ulong, T, bool> selectionFunc)
        {
            var nodeStack = new Stack<GaBinaryTree<T>>();
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
                        nodeStack.Push(childNode as GaBinaryTree<T>);
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
                        nodeStack.Push(childNode as GaBinaryTree<T>);
                        idStack.Push(childNodeId);
                    }
                    else if (selectionFunc(childNodeId, childNode.Value))
                        node.ChildNode0 = null;
                }
            }

            return RemoveEmptyDescendantNodes();
        }


        public GaBinaryTreeLeafNode<T> GetLeafNode(ulong index)
        {
            var node = this;
            ulong bitPattern;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GaBinaryTree<T>;

                if (ReferenceEquals(node, null))
                    return null;
            }

            bitPattern = 1ul & index;
            return node.GetChildNode(bitPattern != 0) as GaBinaryTreeLeafNode<T>;
        }

        public bool TryGetLeafNode(ulong index, out GaBinaryTreeLeafNode<T> leafNode)
        {
            var node = this;
            ulong bitPattern;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GaBinaryTree<T>;

                if (ReferenceEquals(node, null))
                {
                    leafNode = null;
                    return false;
                }
            }

            bitPattern = 1ul & index;
            leafNode = node.GetChildNode(bitPattern != 0) as GaBinaryTreeLeafNode<T>;
            return !ReferenceEquals(leafNode, null);
        }

        public T GetLeafValue(ulong index)
        {
            var node = this;
            ulong bitPattern;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GaBinaryTree<T>;

                if (ReferenceEquals(node, null))
                    return default(T);
            }

            bitPattern = 1ul & index;
            var leafNode = node.GetChildNode(bitPattern != 0) as GaBinaryTreeLeafNode<T>;

            return ReferenceEquals(leafNode, null) ? default(T) : leafNode.Value;
        }

        public bool TryGetLeafValue(ulong index, out T value)
        {
            var node = this;
            ulong bitPattern;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GaBinaryTree<T>;

                if (ReferenceEquals(node, null))
                {
                    value = default(T);
                    return false;
                }
            }

            bitPattern = 1ul & index;
            var leafNode = node.GetChildNode(bitPattern != 0) as GaBinaryTreeLeafNode<T>;

            if (ReferenceEquals(leafNode, null))
            {
                value = default(T);
                return false;
            }

            value = leafNode.Value;
            return true;
        }

        public KeyValuePair<ulong, GaBinaryTreeLeafNode<T>> GetLeafNodePair(ulong id)
        {
            return new KeyValuePair<ulong, GaBinaryTreeLeafNode<T>>(
                id, 
                GetLeafNode(id)
            );
        }

        public KeyValuePair<ulong, T> GetValuePair(ulong id)
        {
            return new KeyValuePair<ulong, T>(id, GetLeafValue(id));
        }

        public GaBinaryTree<T> SetLeafValue(ulong index, T value)
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
                node = node.GetChildNode(bitPattern != 0) as GaBinaryTree<T>;

                if (ReferenceEquals(node, null))
                    return false;
            }

            bitPattern = 1ul & index;
            var leafNode = node.GetChildNode(bitPattern != 0) as GaBinaryTreeLeafNode<T>;

            return !ReferenceEquals(leafNode, null);
        }

        public bool HasChildNode(bool rightChild)
        {
            return !ReferenceEquals(rightChild ? ChildNode1 : ChildNode0, null);
        }

        public IGaBinaryTreeNode<T> GetChildNode(bool rightChild)
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
        public GaBinaryTree<T> GetOrAddInternalChildNode(bool rightChild)
        {
            Debug.Assert(TreeDepth > 1);

            if (rightChild)
            {
                var childNode = ChildNode1 as GaBinaryTree<T>;

                if (!ReferenceEquals(childNode, null))
                    return childNode;

                childNode = new GaBinaryTree<T>(TreeDepth - 1);
                ChildNode1 = childNode;

                return childNode;
            }
            else
            {
                var childNode = ChildNode0 as GaBinaryTree<T>;

                if (!ReferenceEquals(childNode, null))
                    return childNode;

                childNode = new GaBinaryTree<T>(TreeDepth - 1);
                ChildNode0 = childNode;

                return childNode;
            }
        }

        public void SetOrAddLeafChildNode(bool rightChild, T value)
        {
            Debug.Assert(TreeDepth == 1);

            if (rightChild)
            {
                var childNode = ChildNode1 as GaBinaryTreeLeafNode<T>;

                if (!ReferenceEquals(childNode, null))
                    childNode.Value = value;
                else
                    ChildNode1 = new GaBinaryTreeLeafNode<T>(value);
            }
            else
            {
                var childNode = ChildNode0 as GaBinaryTreeLeafNode<T>;

                if (!ReferenceEquals(childNode, null))
                    childNode.Value = value;
                else
                    ChildNode0 = new GaBinaryTreeLeafNode<T>(value);
            }
        }

        public GaBinaryTree<T> ResetInternalChildNode0()
        {
            var childNode = new GaBinaryTree<T>(TreeDepth - 1);

            ChildNode0 = childNode;

            return childNode;
        }

        public GaBinaryTree<T> ResetInternalChildNode1()
        {
            var childNode = new GaBinaryTree<T>(TreeDepth - 1);

            ChildNode1 = childNode;

            return childNode;
        }

        public GaBinaryTreeLeafNode<T> ResetLeafChildNode0(T value)
        {
            Debug.Assert(TreeDepth == 1);

            var childNode = new GaBinaryTreeLeafNode<T>(value);

            ChildNode0 = childNode;

            return childNode;
        }

        public GaBinaryTreeLeafNode<T> ResetLeafChildNode1(T value)
        {
            Debug.Assert(TreeDepth == 1);

            var childNode = new GaBinaryTreeLeafNode<T>(value);

            ChildNode1 = childNode;

            return childNode;
        }

        public GaBinaryTree<T> RemoveChildNode1()
        {
            ChildNode1 = null;

            return this;
        }

        public GaBinaryTree<T> RemoveChildNode0()
        {
            ChildNode0 = null;

            return this;
        }

        public GaBinaryTree<T> RemoveChildNodes()
        {
            ChildNode1 = null;
            ChildNode0 = null;

            return this;
        }

        public GaBinaryTree<T> RemoveChildNode(bool rightChild)
        {
            if (rightChild)
                ChildNode1 = null;
            else
                ChildNode0 = null;

            return this;
        }


        public GaBinaryTree<T> FillFromTree(GaBinaryTree<T> sourceTree)
        {
            if (TreeDepth != sourceTree.TreeDepth)
                throw new InvalidOperationException("Tree depth mismatch");

            var sourceNodeStack = new Stack<GaBinaryTree<T>>();
            var targetNodeStack = new Stack<GaBinaryTree<T>>();

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
                        sourceNodeStack.Push(sourceChildNode as GaBinaryTree<T>);
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
                        sourceNodeStack.Push(sourceChildNode as GaBinaryTree<T>);
                        targetNodeStack.Push(targetNode.ResetInternalChildNode1());
                    }
                    else
                        targetNode.ResetLeafChildNode1(sourceChildNode.Value);
                }
            }

            return this;
        }

        public GaBinaryTree<T> FillFromTree<TU>(GaBinaryTree<TU> sourceTree, Func<TU, T> valueConversionFunc)
        {
            if (TreeDepth != sourceTree.TreeDepth)
                throw new InvalidOperationException("Tree depth mismatch");

            var sourceNodeStack = new Stack<GaBinaryTree<TU>>();
            var targetNodeStack = new Stack<GaBinaryTree<T>>();

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
                        sourceNodeStack.Push(sourceChildNode as GaBinaryTree<TU>);
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
                        sourceNodeStack.Push(sourceChildNode as GaBinaryTree<TU>);
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

        public GaBinaryTree<TU> MapLeafValues<TU>(Func<T, TU> valueConversionFunc)
        {
            return new GaBinaryTree<TU>(TreeDepth).FillFromTree(this, valueConversionFunc);
        }


        public Stack<ulong> CreateNodeIDsStack()
        {
            var stack = new Stack<ulong>();

            stack.Push(0ul);

            return stack;
        }

        public Stack<IGaBinaryTreeNode<T>> CreateNodesStack()
        {
            var stack = new Stack<IGaBinaryTreeNode<T>>();

            stack.Push(this);

            return stack;
        }

        public Stack<GaBinaryTree<T>> CreateParentNodesStack()
        {
            var stack = new Stack<GaBinaryTree<T>>();

            stack.Push(this);

            return stack;
        }


        public override string ToString()
        {
            var textComposer = new LinearComposer();

            var idStack = new Stack<ulong>();
            var nodeStack = new Stack<IGaBinaryTreeNode<T>>();

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