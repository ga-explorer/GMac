using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees.NodeInfo;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTrees
{
    /// <summary>
    /// This class represents an internal node (non-leaf node) in a binary tree
    /// used for efficient computations on sparse multivectors and sparse linear
    /// maps on multivectors
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GaBtrInternalNode<T> : IGaBtrNode<T>
    {
        public T Value 
            => default;

        public IGaBtrNode<T> ChildNode0 { get; private set; }

        public IGaBtrNode<T> ChildNode1 { get; private set; }

        public GaBtrLeafNode<T> LeafChildNode0 
            => ChildNode0 as GaBtrLeafNode<T>;

        public GaBtrLeafNode<T> LeafChildNode1
            => ChildNode1 as GaBtrLeafNode<T>;

        public bool HasChildNode0 
            => !ReferenceEquals(ChildNode0, null);

        public bool HasChildNode1 
            => !ReferenceEquals(ChildNode1, null);

        public bool HasNoChildNodes 
            => ReferenceEquals(ChildNode1, null) && 
               ReferenceEquals(ChildNode0, null);

        public bool IsInternalNode 
            => true;

        public bool IsLeafNode 
            => false;


        public int GetTreeDepth()
        {
            var nodeStack = new Stack<Tuple<int, IGaBtrNode<T>>>();
            nodeStack.Push(
                new Tuple<int, IGaBtrNode<T>>(0, this)
            );

            while (nodeStack.Count > 0)
            {
                var nodeInfo = nodeStack.Pop();
                var (treeDepth, node) = nodeInfo;

                if (node.IsLeafNode)
                    return treeDepth;

                if (node.HasChildNode1)
                    nodeStack.Push(
                        new Tuple<int, IGaBtrNode<T>>(
                            treeDepth + 1, 
                            node.ChildNode1
                        )
                    );

                if (node.HasChildNode0)
                    nodeStack.Push(
                        new Tuple<int, IGaBtrNode<T>>(
                            treeDepth + 1, 
                            node.ChildNode0
                        )
                    );
            }

            throw new InvalidOperationException("Tree contains no leaf nodes");
        }

        public GaBinaryTreeNodeInfo1<T> GetNodeInfo(int treeDepth, ulong id)
        {
            return new GaBinaryTreeNodeInfo1<T>(treeDepth, id, this);
        }

        /// <summary>
        /// Returns all nodes in this tree starting from this node in depth-first
        /// order.
        /// </summary>
        public IEnumerable<IGaBtrNode<T>> GetTreeNodes()
        {
            var nodeStack = new Stack<IGaBtrNode<T>>();
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

        /// <summary>
        /// Returns all internal nodes in this tree starting from this node in
        /// depth-first order.
        /// </summary>
        public IEnumerable<GaBtrInternalNode<T>> GetTreeInternalNodes()
        {
            var nodeStack = new Stack<GaBtrInternalNode<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                yield return node;

                if (node.ChildNode1 is GaBtrInternalNode<T> childNode)
                    nodeStack.Push(childNode);

                childNode = node.ChildNode0 as GaBtrInternalNode<T>;
                if (!ReferenceEquals(childNode, null))
                    nodeStack.Push(childNode);
            }
        }

        /// <summary>
        /// Returns all leaf nodes in this tree starting from this node in
        /// depth-first order.
        /// </summary>
        public IEnumerable<GaBtrLeafNode<T>> GetTreeLeafNodes()
        {
            var nodeStack = new Stack<GaBtrInternalNode<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                if (node.HasChildNode1)
                {
                    var childNode = node.ChildNode1;

                    if (childNode.IsInternalNode)
                        nodeStack.Push(childNode as GaBtrInternalNode<T>);
                    else
                        yield return childNode as GaBtrLeafNode<T>;
                }

                if (node.HasChildNode0)
                {
                    var childNode = node.ChildNode0;

                    if (childNode.IsInternalNode)
                        nodeStack.Push(childNode as GaBtrInternalNode<T>);
                    else
                        yield return childNode as GaBtrLeafNode<T>;
                }
            }
        }

        /// <summary>
        /// Returns all leaf node values in this tree starting from this node
        /// in depth-first order.
        /// </summary>
        public IEnumerable<T> GetTreeLeafValues()
        {
            var nodeStack = new Stack<IGaBtrNode<T>>();
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

        /// <summary>
        /// Remove parent nodes having no child nodes in this tree
        /// </summary>
        /// <returns></returns>
        public GaBtrInternalNode<T> RemoveEmptyDescendantNodes()
        {
            //TODO: Implement this using stacks
            if (HasChildNode1)
            {
                if (ChildNode1 is GaBtrInternalNode<T> childNode)
                {
                    childNode.RemoveEmptyDescendantNodes();

                    if (childNode.HasNoChildNodes)
                        ChildNode1 = null;
                }
            }

            if (HasChildNode0)
            {
                if (ChildNode0 is GaBtrInternalNode<T> childNode)
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
        public GaBtrInternalNode<T> TrimByLeafValues(Func<T, bool> selectionFunc)
        {
            var nodeStack = new Stack<GaBtrInternalNode<T>>();
            nodeStack.Push(this);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                if (node.HasChildNode1)
                {
                    var childNode = node.ChildNode1;

                    if (childNode.IsInternalNode)
                        nodeStack.Push(childNode as GaBtrInternalNode<T>);

                    else if (selectionFunc(childNode.Value))
                        node.ChildNode1 = null;
                }

                if (node.HasChildNode0)
                {
                    var childNode = node.ChildNode0;

                    if (childNode.IsInternalNode)
                        nodeStack.Push(childNode as GaBtrInternalNode<T>);

                    else if (selectionFunc(childNode.Value))
                        node.ChildNode0 = null;
                }
            }

            return RemoveEmptyDescendantNodes();
        }

        ///// <summary>
        ///// Remove all leafs from tree where selectionFunc is true for the leaf id and value, and then remove
        ///// parent nodes having no child nodes.
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="selectionFunc"></param>
        ///// <param name="treeDepth"></param>
        ///// <returns></returns>
        //public GaBtrInternalNode<T> TrimByLeafValuePairs(int treeDepth, ulong id, Func<ulong, T, bool> selectionFunc)
        //{
        //    var nodeStack = new Stack<GaBinaryTreeNodeInfo1<T>>();
        //    nodeStack.Push(GetNodeInfo(treeDepth, id));

        //    while (nodeStack.Count > 0)
        //    {
        //        var nodeInfo = nodeStack.Pop();

        //        if (nodeInfo.HasChildNode1)
        //        {
        //            var childNodeInfo = nodeInfo.GetChildNodeInfo1();

        //            if (childNodeInfo.IsInternalNode)
        //                nodeStack.Push(childNodeInfo);
                    
        //            else if (selectionFunc(childNodeInfo.Id, childNodeInfo.Value))
        //                nodeInfo.NodeAsInternal.ChildNode1 = null;
        //        }

        //        if (nodeInfo.HasChildNode0)
        //        {
        //            var childNodeInfo = nodeInfo.GetChildNodeInfo0();

        //            if (childNodeInfo.IsInternalNode)
        //                nodeStack.Push(childNodeInfo);
                    
        //            else if (selectionFunc(childNodeInfo.Id, childNodeInfo.Value))
        //                nodeInfo.NodeAsInternal.ChildNode0 = null;
        //        }
        //    }

        //    return RemoveEmptyDescendantNodes();
        //}


        /// <summary>
        /// Find a leaf node given its path-id under this node, if no node is
        /// found this returns null
        /// </summary>
        /// <param name="treeDepth"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public GaBtrLeafNode<T> GetLeafNode(int treeDepth, ulong index)
        {
            var node = this;
            ulong bitPattern;

            for (var i = treeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GaBtrInternalNode<T>;

                if (ReferenceEquals(node, null))
                    return null;
            }

            bitPattern = 1ul & index;
            return node.GetChildNode(bitPattern != 0) as GaBtrLeafNode<T>;
        }

        /// <summary>
        /// Try to find a leaf node given its path-id under this node, if no
        /// node is found this returns null
        /// </summary>
        /// <param name="treeDepth"></param>
        /// <param name="index"></param>
        /// <param name="leafNode"></param>
        /// <returns></returns>
        public bool TryGetLeafNode(int treeDepth, ulong index, out GaBtrLeafNode<T> leafNode)
        {
            var node = this;
            ulong bitPattern;

            for (var i = treeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GaBtrInternalNode<T>;

                if (ReferenceEquals(node, null))
                {
                    leafNode = null;
                    return false;
                }
            }

            bitPattern = 1ul & index;
            leafNode = node.GetChildNode(bitPattern != 0) as GaBtrLeafNode<T>;
            return !ReferenceEquals(leafNode, null);
        }

        /// <summary>
        /// Find a leaf node value given its path-id under this node, if no node
        /// is found this returns the default value
        /// </summary>
        /// <param name="treeDepth"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetLeafValue(int treeDepth, ulong index)
        {
            var node = this;

            for (var i = treeDepth - 1; i > 0; i--)
            {
                node = (
                    ((1ul << i) & index) == 0 
                        ? node.ChildNode0 
                        : node.ChildNode1
                ) as GaBtrInternalNode<T>;

                if (ReferenceEquals(node, null))
                    return default(T);
            }

            return (
                (1ul & index) == 0 
                    ? node.ChildNode0 
                    : node.ChildNode1
            ) is GaBtrLeafNode<T> finalLeafNode 
                ? finalLeafNode.Value 
                : default(T);
        }

        /// <summary>
        /// Try to find a leaf node value given its path-id under this node, if no
        /// node is found this returns the default value
        /// </summary>
        /// <param name="treeDepth"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetLeafValue(int treeDepth, ulong index, out T value)
        {
            var node = this;
            ulong bitPattern;

            for (var i = treeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GaBtrInternalNode<T>;

                if (ReferenceEquals(node, null))
                {
                    value = default(T);
                    return false;
                }
            }

            bitPattern = 1ul & index;

            if (!(node.GetChildNode(bitPattern != 0) is GaBtrLeafNode<T> leafNode))
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
        /// <param name="treeDepth"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public KeyValuePair<ulong, GaBtrLeafNode<T>> GetLeafNodePair(int treeDepth, ulong id)
        {
            return new KeyValuePair<ulong, GaBtrLeafNode<T>>(
                id, 
                GetLeafNode(treeDepth, id)
            );
        }

        /// <summary>
        /// Find a leaf node value and its path-id given its path-id under this
        /// node, if no node is found this returns null
        /// </summary>
        /// <param name="treeDepth"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public KeyValuePair<ulong, T> GetValuePair(int treeDepth, ulong id)
        {
            return new KeyValuePair<ulong, T>(
                id, 
                GetLeafValue(treeDepth, id)
            );
        }

        /// <summary>
        /// Given a path-id of a leaf node this finds or creates the leaf node
        /// and sets its value as given
        /// </summary>
        /// <param name="treeDepth"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public GaBtrInternalNode<T> SetLeafValue(int treeDepth, ulong index, T value)
        {
            var node = this;
            for (var i = treeDepth - 1; i > 0; i--)
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
        /// <param name="treeDepth"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool ContainsLeafNodeId(int treeDepth, ulong index)
        {
            var node = this;
            ulong bitPattern;

            for (var i = treeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                node = node.GetChildNode(bitPattern != 0) as GaBtrInternalNode<T>;

                if (ReferenceEquals(node, null))
                    return false;
            }

            bitPattern = 1ul & index;

            return node.GetChildNode(bitPattern != 0) is GaBtrLeafNode<T>;
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
        public IGaBtrNode<T> GetChildNode(bool rightChild)
        {
            return rightChild ? ChildNode1 : ChildNode0;
        }

        /// <summary>
        /// Try to get an internal child node if it exists, else create
        /// a new internal child and return it
        /// </summary>
        /// <param name="rightChild"></param>
        /// <returns></returns>
        public GaBtrInternalNode<T> GetOrAddInternalChildNode(bool rightChild)
        {
            if (rightChild)
            {
                if (ChildNode1 is GaBtrInternalNode<T> childNode)
                    return childNode;

                childNode = new GaBtrInternalNode<T>();

                ChildNode1 = childNode;

                return childNode;
            }
            else
            {
                if (ChildNode0 is GaBtrInternalNode<T> childNode)
                    return childNode;

                childNode = new GaBtrInternalNode<T>();

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
            if (rightChild)
            {
                if (ChildNode1 is GaBtrLeafNode<T> childNode)
                    childNode.Value = value;
                else
                    ChildNode1 = new GaBtrLeafNode<T>(value);
            }
            else
            {
                if (ChildNode0 is GaBtrLeafNode<T> childNode)
                    childNode.Value = value;
                else
                    ChildNode0 = new GaBtrLeafNode<T>(value);
            }
        }

        /// <summary>
        /// Remove the first child node and set it to a new internal node
        /// </summary>
        /// <returns></returns>
        public GaBtrInternalNode<T> ResetInternalChildNode0()
        {
            var childNode = new GaBtrInternalNode<T>();

            ChildNode0 = childNode;

            return childNode;
        }

        /// <summary>
        /// Remove the second child node and set it to a new internal node
        /// </summary>
        /// <returns></returns>
        public GaBtrInternalNode<T> ResetInternalChildNode1()
        {
            var childNode = new GaBtrInternalNode<T>();

            ChildNode1 = childNode;

            return childNode;
        }

        /// <summary>
        /// Remove the first child node and set it to a new leaf node
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GaBtrLeafNode<T> ResetLeafChildNode0(T value)
        {
            var childNode = new GaBtrLeafNode<T>(value);

            ChildNode0 = childNode;

            return childNode;
        }

        /// <summary>
        /// Remove the second child node and set it to a new leaf node
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GaBtrLeafNode<T> ResetLeafChildNode1(T value)
        {
            var childNode = new GaBtrLeafNode<T>(value);

            ChildNode1 = childNode;

            return childNode;
        }

        public GaBtrInternalNode<T> RemoveChildNode1()
        {
            ChildNode1 = null;

            return this;
        }

        public GaBtrInternalNode<T> RemoveChildNode0()
        {
            ChildNode0 = null;

            return this;
        }

        public GaBtrInternalNode<T> RemoveChildNodes()
        {
            ChildNode1 = null;
            ChildNode0 = null;

            return this;
        }

        public GaBtrInternalNode<T> RemoveChildNode(bool rightChild)
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
        public GaBtrInternalNode<T> FillFromTree(GaBtrInternalNode<T> sourceTree)
        {
            var sourceNodeStack = new Stack<GaBtrInternalNode<T>>();
            var targetNodeStack = new Stack<GaBtrInternalNode<T>>();

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
                        sourceNodeStack.Push(sourceChildNode as GaBtrInternalNode<T>);
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
                        sourceNodeStack.Push(sourceChildNode as GaBtrInternalNode<T>);
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
        public GaBtrInternalNode<T> FillFromTree<TU>(GaBtrInternalNode<TU> sourceTree, Func<TU, T> valueConversionFunc)
        {
            var sourceNodeStack = new Stack<GaBtrInternalNode<TU>>();
            var targetNodeStack = new Stack<GaBtrInternalNode<T>>();

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
                        sourceNodeStack.Push(sourceChildNode as GaBtrInternalNode<TU>);
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
                        sourceNodeStack.Push(sourceChildNode as GaBtrInternalNode<TU>);
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
        public GaBtrInternalNode<TU> MapTree<TU>(Func<T, TU> valueConversionFunc)
        {
            var newTree = new GaBtrInternalNode<TU>();

            newTree.FillFromTree(this, valueConversionFunc);

            return newTree;
        }


        public Stack<IGaBtrNode<T>> CreateNodesStack()
        {
            var stack = new Stack<IGaBtrNode<T>>();

            stack.Push(this);

            return stack;
        }

        public Stack<GaBtrInternalNode<T>> CreateInternalNodesStack()
        {
            var stack = new Stack<GaBtrInternalNode<T>>();

            stack.Push(this);

            return stack;
        }


        public override string ToString()
        {
            var nodeInfo = GetNodeInfo(GetTreeDepth(), 0);

            return nodeInfo.ToString();
        }
    }
}