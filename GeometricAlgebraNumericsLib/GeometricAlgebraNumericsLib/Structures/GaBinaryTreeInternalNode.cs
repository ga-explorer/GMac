using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraNumericsLib.Structures
{
    /// <summary>
    /// This class represents an internal node (non-leaf node) in a binary tree
    /// used for efficient computations on sparse multivectors and sparse linear
    /// maps on multivectors
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GaBinaryTreeInternalNode<T> : IGaBinaryTreeNode<T>
    {
        public ulong Id { get; }

        public ulong ChildId0 
            => Id;

        public ulong ChildId1 
            => Id | (1ul << TreeDepth - 1);

        public T Value 
            => default(T);

        public int TreeDepth { get; }

        public IGaBinaryTreeNode<T> ChildNode0 { get; private set; }

        public IGaBinaryTreeNode<T> ChildNode1 { get; private set; }

        public GaBinaryTreeLeafNode<T> LeafChildNode0 
            => ChildNode0 as GaBinaryTreeLeafNode<T>;

        public GaBinaryTreeLeafNode<T> LeafChildNode1
            => ChildNode1 as GaBinaryTreeLeafNode<T>;

        public ulong BitMask
            => 1ul << TreeDepth;

        public bool HasChildNode0 
            => !ReferenceEquals(ChildNode0, null);

        public bool HasChildNode1 
            => !ReferenceEquals(ChildNode1, null);

        public bool HasNoChildNodes 
            => ReferenceEquals(ChildNode1, null) && ReferenceEquals(ChildNode0, null);

        public bool IsInternalNode => true;

        public bool IsLeafNode => false;

        /// <summary>
        /// Returns all nodes in this tree starting from this node in depth-first
        /// order. Each node is returned with its path-id defining its path in the
        /// tree
        /// </summary>
        public IEnumerable<KeyValuePair<ulong, IGaBinaryTreeNode<T>>> NodePairs
        {
            get
            {
                var nodeStack = new Stack<IGaBinaryTreeNode<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    yield return
                        new KeyValuePair<ulong, IGaBinaryTreeNode<T>>(node.Id, node);

                    if (node.IsLeafNode)
                        continue;

                    if (node.HasChildNode1) 
                        nodeStack.Push(node.ChildNode1);

                    if (node.HasChildNode0) 
                        nodeStack.Push(node.ChildNode0);
                }
            }
        }

        /// <summary>
        /// Returns all internal nodes in this tree starting from this node in
        /// depth-first order. Each node is returned with its path-id defining
        /// its path in the tree
        /// </summary>
        public IEnumerable<KeyValuePair<ulong, GaBinaryTreeInternalNode<T>>> InternalNodePairs
        {
            get
            {
                var nodeStack = new Stack<GaBinaryTreeInternalNode<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    yield return
                        new KeyValuePair<ulong, GaBinaryTreeInternalNode<T>>(node.Id, node);

                    if (node.ChildNode1 is GaBinaryTreeInternalNode<T> childNode)
                        nodeStack.Push(childNode);

                    childNode = node.ChildNode0 as GaBinaryTreeInternalNode<T>;
                    if (ReferenceEquals(childNode, null)) 
                        continue;

                    nodeStack.Push(childNode);
                }
            }
        }

        /// <summary>
        /// Returns all leaf nodes in this tree starting from this node in
        /// depth-first order. Each node is returned with its path-id defining
        /// its path in the tree
        /// </summary>
        public IEnumerable<KeyValuePair<ulong, GaBinaryTreeLeafNode<T>>> LeafNodePairs
        {
            get
            {
                var nodeStack = new Stack<GaBinaryTreeInternalNode<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    if (node.HasChildNode1)
                    {
                        var childNode = node.ChildNode1;

                        if (childNode.IsInternalNode)
                            nodeStack.Push(childNode as GaBinaryTreeInternalNode<T>);

                        else
                            yield return new KeyValuePair<ulong, GaBinaryTreeLeafNode<T>>(
                                childNode.Id,
                                childNode as GaBinaryTreeLeafNode<T>
                            );
                    }

                    if (node.HasChildNode0)
                    {
                        var childNode = node.ChildNode0;

                        if (childNode.IsInternalNode)
                            nodeStack.Push(childNode as GaBinaryTreeInternalNode<T>);

                        else
                            yield return new KeyValuePair<ulong, GaBinaryTreeLeafNode<T>>(
                                childNode.Id,
                                childNode as GaBinaryTreeLeafNode<T>
                            );
                    }
                }
            }
        }

        /// <summary>
        /// Returns all leaf node values in this tree starting from this node in
        /// depth-first order. Each node value is returned with its path-id defining
        /// its path in the tree
        /// </summary>
        public IEnumerable<KeyValuePair<ulong, T>> LeafValuePairs
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
                        yield return
                            new KeyValuePair<ulong, T>(node.Id, node.Value);

                        continue;
                    }

                    if (node.HasChildNode1)
                        nodeStack.Push(node.ChildNode1);

                    if (node.HasChildNode0)
                        nodeStack.Push(node.ChildNode0);
                }
            }
        }

        /// <summary>
        /// Returns all nodes in this tree starting from this node in depth-first
        /// order.
        /// </summary>
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

        /// <summary>
        /// Returns all internal nodes in this tree starting from this node in
        /// depth-first order.
        /// </summary>
        public IEnumerable<GaBinaryTreeInternalNode<T>> InternalNodes
        {
            get
            {
                var nodeStack = new Stack<GaBinaryTreeInternalNode<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    yield return node;

                    if (node.ChildNode1 is GaBinaryTreeInternalNode<T> childNode)
                        nodeStack.Push(childNode);

                    childNode = node.ChildNode0 as GaBinaryTreeInternalNode<T>;
                    if (!ReferenceEquals(childNode, null))
                        nodeStack.Push(childNode);
                }
            }
        }

        /// <summary>
        /// Returns all leaf nodes in this tree starting from this node in
        /// depth-first order.
        /// </summary>
        public IEnumerable<GaBinaryTreeLeafNode<T>> LeafNodes
        {
            get
            {
                var nodeStack = new Stack<GaBinaryTreeInternalNode<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    if (node.HasChildNode1)
                    {
                        var childNode = node.ChildNode1;

                        if (childNode.IsInternalNode)
                            nodeStack.Push(childNode as GaBinaryTreeInternalNode<T>);
                        else
                            yield return childNode as GaBinaryTreeLeafNode<T>;
                    }

                    if (node.HasChildNode0)
                    {
                        var childNode = node.ChildNode0;

                        if (childNode.IsInternalNode)
                            nodeStack.Push(childNode as GaBinaryTreeInternalNode<T>);
                        else
                            yield return childNode as GaBinaryTreeLeafNode<T>;
                    }
                }
            }
        }

        /// <summary>
        /// Returns all node path-ids in this tree starting from this node in
        /// depth-first order.
        /// </summary>
        public IEnumerable<ulong> NodeIDs
        {
            get
            {
                var nodeStack = new Stack<IGaBinaryTreeNode<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    yield return node.Id;

                    if (node.IsLeafNode)
                        continue;

                    if (node.HasChildNode1)
                        nodeStack.Push(node.ChildNode1);

                    if (node.HasChildNode0)
                        nodeStack.Push(node.ChildNode0);
                }
            }
        }

        /// <summary>
        /// Returns all internal node path-ids in this tree starting from this
        /// node in depth-first order.
        /// </summary>
        public IEnumerable<ulong> InternalNodeIDs
        {
            get
            {
                var nodeStack = new Stack<GaBinaryTreeInternalNode<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    yield return node.Id;

                    if (node.ChildNode1 is GaBinaryTreeInternalNode<T> childNode)
                        nodeStack.Push(childNode);

                    childNode = node.ChildNode0 as GaBinaryTreeInternalNode<T>;
                    if (!ReferenceEquals(childNode, null))
                        nodeStack.Push(childNode);
                }
            }
        }

        /// <summary>
        /// Returns all leaf node path-ids in this tree starting from this node
        /// in depth-first order.
        /// </summary>
        public IEnumerable<ulong> LeafNodeIDs
        {
            get
            {
                var nodeStack = new Stack<GaBinaryTreeInternalNode<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    if (node.HasChildNode0)
                    {
                        var childNode = node.ChildNode0;

                        if (childNode.IsInternalNode)
                            nodeStack.Push(childNode as GaBinaryTreeInternalNode<T>);
                        
                        else
                            yield return childNode.Id;
                    }

                    if (node.HasChildNode1)
                    {
                        var childNode = node.ChildNode1;

                        if (childNode.IsInternalNode)
                            nodeStack.Push(childNode as GaBinaryTreeInternalNode<T>);
                        
                        else
                            yield return childNode.Id;
                    }
                }
            }
        }

        /// <summary>
        /// Returns all leaf node values in this tree starting from this node
        /// in depth-first order.
        /// </summary>
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


        public GaBinaryTreeInternalNode(ulong id, int treeDepth)
        {
            Debug.Assert(treeDepth > 0 && treeDepth < 64);

            Id = id;
            TreeDepth = treeDepth;
        }


        /// <summary>
        /// Remove parent nodes having no child nodes in this tree
        /// </summary>
        /// <returns></returns>
        public GaBinaryTreeInternalNode<T> RemoveEmptyDescendantNodes()
        {
            //TODO: Implement this using stacks
            if (HasChildNode1)
            {
                if (ChildNode1 is GaBinaryTreeInternalNode<T> childNode)
                {
                    childNode.RemoveEmptyDescendantNodes();

                    if (childNode.HasNoChildNodes)
                        ChildNode1 = null;
                }
            }

            if (HasChildNode0)
            {
                if (ChildNode0 is GaBinaryTreeInternalNode<T> childNode)
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
        public GaBinaryTreeInternalNode<T> TrimByLeafValues(Func<T, bool> selectionFunc)
        {
            var nodeStack = new Stack<GaBinaryTreeInternalNode<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                if (node.HasChildNode1)
                {
                    var childNode = node.ChildNode1;

                    if (childNode.IsInternalNode)
                        nodeStack.Push(childNode as GaBinaryTreeInternalNode<T>);

                    else if (selectionFunc(childNode.Value))
                        node.ChildNode1 = null;
                }

                if (node.HasChildNode0)
                {
                    var childNode = node.ChildNode0;

                    if (childNode.IsInternalNode)
                        nodeStack.Push(childNode as GaBinaryTreeInternalNode<T>);

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
        public GaBinaryTreeInternalNode<T> TrimByLeafValuePairs(Func<ulong, T, bool> selectionFunc)
        {
            var nodeStack = new Stack<GaBinaryTreeInternalNode<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                if (node.HasChildNode1)
                {
                    var childNode = node.ChildNode1;

                    if (childNode.IsInternalNode)
                        nodeStack.Push(childNode as GaBinaryTreeInternalNode<T>);
                    
                    else if (selectionFunc(childNode.Id, childNode.Value))
                        node.ChildNode1 = null;
                }

                if (node.HasChildNode0)
                {
                    var childNode = node.ChildNode0;

                    if (childNode.IsInternalNode)
                        nodeStack.Push(childNode as GaBinaryTreeInternalNode<T>);
                    
                    else if (selectionFunc(childNode.Id, childNode.Value))
                        node.ChildNode0 = null;
                }
            }

            return RemoveEmptyDescendantNodes();
        }


        /// <summary>
        /// Find a leaf node given its path-id under this node, if no node is
        /// found this returns null
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GaBinaryTreeLeafNode<T> GetLeafNode(ulong index)
        {
            var node = this;
            ulong bitPattern;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GaBinaryTreeInternalNode<T>;

                if (ReferenceEquals(node, null))
                    return null;
            }

            bitPattern = 1ul & index;
            return node.GetChildNode(bitPattern != 0) as GaBinaryTreeLeafNode<T>;
        }

        /// <summary>
        /// Try to find a leaf node given its path-id under this node, if no
        /// node is found this returns null
        /// </summary>
        /// <param name="index"></param>
        /// <param name="leafNode"></param>
        /// <returns></returns>
        public bool TryGetLeafNode(ulong index, out GaBinaryTreeLeafNode<T> leafNode)
        {
            var node = this;
            ulong bitPattern;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GaBinaryTreeInternalNode<T>;

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

        /// <summary>
        /// Find a leaf node value given its path-id under this node, if no node
        /// is found this returns the default value
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetLeafValue(ulong index)
        {
            var node = this;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                node = (
                    ((1ul << i) & index) == 0 
                        ? node.ChildNode0 
                        : node.ChildNode1
                ) as GaBinaryTreeInternalNode<T>;

                if (ReferenceEquals(node, null))
                    return default(T);
            }

            return (
                (1ul & index) == 0 
                    ? node.ChildNode0 
                    : node.ChildNode1
            ) is GaBinaryTreeLeafNode<T> finalLeafNode 
                ? finalLeafNode.Value 
                : default(T);
        }

        /// <summary>
        /// Try to find a leaf node value given its path-id under this node, if no
        /// node is found this returns the default value
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetLeafValue(ulong index, out T value)
        {
            var node = this;
            ulong bitPattern;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GaBinaryTreeInternalNode<T>;

                if (ReferenceEquals(node, null))
                {
                    value = default(T);
                    return false;
                }
            }

            bitPattern = 1ul & index;

            if (!(node.GetChildNode(bitPattern != 0) is GaBinaryTreeLeafNode<T> leafNode))
            {
                value = default(T);
                return false;
            }

            value = leafNode.Value;
            return true;
        }

        /// <summary>
        /// Find a leaf node and its path-id given its path-id under this node,
        /// if no node is found this returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public KeyValuePair<ulong, GaBinaryTreeLeafNode<T>> GetLeafNodePair(ulong id)
        {
            return new KeyValuePair<ulong, GaBinaryTreeLeafNode<T>>(
                id, 
                GetLeafNode(id)
            );
        }

        /// <summary>
        /// Find a leaf node value and its path-id given its path-id under this
        /// node, if no node is found this returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public KeyValuePair<ulong, T> GetValuePair(ulong id)
        {
            return new KeyValuePair<ulong, T>(
                id, 
                GetLeafValue(id)
            );
        }

        /// <summary>
        /// Given a path-id of a leaf node this finds or creates the leaf node
        /// and sets its value as given
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public GaBinaryTreeInternalNode<T> SetLeafValue(ulong index, T value)
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

        /// <summary>
        /// Test if this contains a leaf node with the given path-id
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool ContainsLeafNodeId(ulong index)
        {
            var node = this;
            ulong bitPattern;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GaBinaryTreeInternalNode<T>;

                if (ReferenceEquals(node, null))
                    return false;
            }

            bitPattern = 1ul & index;

            return node.GetChildNode(bitPattern != 0) is GaBinaryTreeLeafNode<T>;
        }

        /// <summary>
        /// Tests if this node has a child node. The tested child is specified
        /// using the input flag (false: 1st, true:2nd child)
        /// </summary>
        /// <param name="rightChild"></param>
        /// <returns></returns>
        public bool HasChildNode(bool rightChild)
        {
            return !ReferenceEquals(rightChild ? ChildNode1 : ChildNode0, null);
        }

        /// <summary>
        /// Gets the child node of this node. The child is specified
        /// using the input flag (false: 1st, true:2nd child)
        /// </summary>
        /// <param name="rightChild"></param>
        /// <returns></returns>
        public IGaBinaryTreeNode<T> GetChildNode(bool rightChild)
        {
            return rightChild ? ChildNode1 : ChildNode0;
        }

        /// <summary>
        /// Try to get an internal child node if it exists, else create
        /// a new internal child and return it
        /// </summary>
        /// <param name="rightChild"></param>
        /// <returns></returns>
        public GaBinaryTreeInternalNode<T> GetOrAddInternalChildNode(bool rightChild)
        {
            Debug.Assert(TreeDepth > 1);

            if (rightChild)
            {
                if (ChildNode1 is GaBinaryTreeInternalNode<T> childNode)
                    return childNode;

                childNode = new GaBinaryTreeInternalNode<T>(
                    ChildId1,
                    TreeDepth - 1
                    );

                ChildNode1 = childNode;

                return childNode;
            }
            else
            {
                if (ChildNode0 is GaBinaryTreeInternalNode<T> childNode)
                    return childNode;

                childNode = new GaBinaryTreeInternalNode<T>(
                    ChildId0,
                    TreeDepth - 1
                );

                ChildNode0 = childNode;

                return childNode;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rightChild"></param>
        /// <param name="value"></param>
        public void SetOrAddLeafChildNode(bool rightChild, T value)
        {
            Debug.Assert(TreeDepth == 1);

            if (rightChild)
            {
                if (ChildNode1 is GaBinaryTreeLeafNode<T> childNode)
                    childNode.Value = value;
                else
                    ChildNode1 = new GaBinaryTreeLeafNode<T>(ChildId1, value);
            }
            else
            {
                if (ChildNode0 is GaBinaryTreeLeafNode<T> childNode)
                    childNode.Value = value;
                else
                    ChildNode0 = new GaBinaryTreeLeafNode<T>(ChildId0, value);
            }
        }

        /// <summary>
        /// Remove the first child node and set it to a new internal node
        /// </summary>
        /// <returns></returns>
        public GaBinaryTreeInternalNode<T> ResetInternalChildNode0()
        {
            Debug.Assert(TreeDepth > 1);

            var childNode = new GaBinaryTreeInternalNode<T>(
                ChildId0, 
                TreeDepth - 1
            );

            ChildNode0 = childNode;

            return childNode;
        }

        /// <summary>
        /// Remove the second child node and set it to a new internal node
        /// </summary>
        /// <returns></returns>
        public GaBinaryTreeInternalNode<T> ResetInternalChildNode1()
        {
            Debug.Assert(TreeDepth > 1);

            var childNode = new GaBinaryTreeInternalNode<T>(
                ChildId1, 
                TreeDepth - 1
            );

            ChildNode1 = childNode;

            return childNode;
        }

        /// <summary>
        /// Remove the first child node and set it to a new leaf node
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GaBinaryTreeLeafNode<T> ResetLeafChildNode0(T value)
        {
            Debug.Assert(TreeDepth == 1);

            var childNode = new GaBinaryTreeLeafNode<T>(ChildId0, value);

            ChildNode0 = childNode;

            return childNode;
        }

        /// <summary>
        /// Remove the second child node and set it to a new leaf node
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GaBinaryTreeLeafNode<T> ResetLeafChildNode1(T value)
        {
            Debug.Assert(TreeDepth == 1);

            var childNode = new GaBinaryTreeLeafNode<T>(ChildId1, value);

            ChildNode1 = childNode;

            return childNode;
        }

        public GaBinaryTreeInternalNode<T> RemoveChildNode1()
        {
            ChildNode1 = null;

            return this;
        }

        public GaBinaryTreeInternalNode<T> RemoveChildNode0()
        {
            ChildNode0 = null;

            return this;
        }

        public GaBinaryTreeInternalNode<T> RemoveChildNodes()
        {
            ChildNode1 = null;
            ChildNode0 = null;

            return this;
        }

        public GaBinaryTreeInternalNode<T> RemoveChildNode(bool rightChild)
        {
            if (rightChild)
                ChildNode1 = null;
            else
                ChildNode0 = null;

            return this;
        }

        /// <summary>
        /// Set the nodes under this node from the given internal node
        /// </summary>
        /// <param name="sourceTree"></param>
        /// <returns></returns>
        public GaBinaryTreeInternalNode<T> FillFromTree(GaBinaryTreeInternalNode<T> sourceTree)
        {
            if (TreeDepth != sourceTree.TreeDepth)
                throw new InvalidOperationException("Tree depth mismatch");

            var sourceNodeStack = new Stack<GaBinaryTreeInternalNode<T>>();
            var targetNodeStack = new Stack<GaBinaryTreeInternalNode<T>>();

            sourceNodeStack.Push(sourceTree);
            targetNodeStack.Push(this);

            while (sourceNodeStack.Count > 0)
            {
                var sourceNode = sourceNodeStack.Pop();
                var targetNode = targetNodeStack.Pop();

                if (sourceNode.HasChildNode0)
                {
                    var sourceChildNode = sourceNode.ChildNode0;

                    if (sourceChildNode.IsInternalNode)
                    {
                        sourceNodeStack.Push(sourceChildNode as GaBinaryTreeInternalNode<T>);
                        targetNodeStack.Push(targetNode.ResetInternalChildNode0());
                    }
                    else
                        targetNode.ResetLeafChildNode0(sourceChildNode.Value);
                }

                if (sourceNode.HasChildNode1)
                {
                    var sourceChildNode = sourceNode.ChildNode1;

                    if (sourceChildNode.IsInternalNode)
                    {
                        sourceNodeStack.Push(sourceChildNode as GaBinaryTreeInternalNode<T>);
                        targetNodeStack.Push(targetNode.ResetInternalChildNode1());
                    }
                    else
                        targetNode.ResetLeafChildNode1(sourceChildNode.Value);
                }
            }

            return this;
        }

        /// <summary>
        /// Set the nodes under this node from the given internal node
        /// </summary>
        /// <typeparam name="TU"></typeparam>
        /// <param name="sourceTree"></param>
        /// <param name="valueConversionFunc"></param>
        /// <returns></returns>
        public GaBinaryTreeInternalNode<T> FillFromTree<TU>(GaBinaryTreeInternalNode<TU> sourceTree, Func<TU, T> valueConversionFunc)
        {
            if (TreeDepth != sourceTree.TreeDepth)
                throw new InvalidOperationException("Tree depth mismatch");

            var sourceNodeStack = new Stack<GaBinaryTreeInternalNode<TU>>();
            var targetNodeStack = new Stack<GaBinaryTreeInternalNode<T>>();

            sourceNodeStack.Push(sourceTree);
            targetNodeStack.Push(this);

            while (sourceNodeStack.Count > 0)
            {
                var sourceNode = sourceNodeStack.Pop();
                var targetNode = targetNodeStack.Pop();

                if (sourceNode.HasChildNode1)
                {
                    var sourceChildNode = sourceNode.ChildNode1;

                    if (sourceChildNode.IsInternalNode)
                    {
                        sourceNodeStack.Push(sourceChildNode as GaBinaryTreeInternalNode<TU>);
                        targetNodeStack.Push(targetNode.ResetInternalChildNode1());
                    }
                    else
                        targetNode.ResetLeafChildNode1(valueConversionFunc(sourceChildNode.Value));
                }

                if (sourceNode.HasChildNode0)
                {
                    var sourceChildNode = sourceNode.ChildNode0;

                    if (sourceChildNode.IsInternalNode)
                    {
                        sourceNodeStack.Push(sourceChildNode as GaBinaryTreeInternalNode<TU>);
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

        /// <summary>
        /// Create a new tree based on the nodes of this node
        /// </summary>
        /// <typeparam name="TU"></typeparam>
        /// <param name="valueConversionFunc"></param>
        /// <returns></returns>
        public GaBinaryTreeInternalNode<TU> MapTree<TU>(Func<T, TU> valueConversionFunc)
        {
            return new GaBinaryTreeInternalNode<TU>(
                0,
                TreeDepth
            ).FillFromTree(this, valueConversionFunc);
        }


        //public Stack<ulong> CreateNodeIDsStack()
        //{
        //    var stack = new Stack<ulong>();

        //    stack.Push(0ul);

        //    return stack;
        //}

        public Stack<IGaBinaryTreeNode<T>> CreateNodesStack()
        {
            var stack = new Stack<IGaBinaryTreeNode<T>>();

            stack.Push(this);

            return stack;
        }

        public Stack<GaBinaryTreeInternalNode<T>> CreateInternalNodesStack()
        {
            var stack = new Stack<GaBinaryTreeInternalNode<T>>();

            stack.Push(this);

            return stack;
        }


        public override string ToString()
        {
            var textComposer = new LinearTextComposer();

            var nodeStack = new Stack<IGaBinaryTreeNode<T>>();
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
                    nodeStack.Push(node.ChildNode1);
                }

                if (node.HasChildNode0)
                {
                    nodeStack.Push(node.ChildNode0);
                }
            }

            return textComposer.ToString();
        }
    }
}