using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GeometricAlgebraNumericsLib.Structures
{
    /// <summary>
    /// This class represents an internal node (non-leaf node) in a quad tree
    /// used for efficient computations on sparse multivectors and sparse linear
    /// maps on multivectors
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GaQuadTreeInternalNode<T> : IGaQuadTreeNode<T>
    {
        public Tuple<ulong, ulong> Id { get; }

        public Tuple<ulong, ulong> ChildNodeId00 
            => Id;

        public Tuple<ulong, ulong> ChildNodeId01
            => Tuple.Create(BitMask | Id.Item1, Id.Item2);

        public Tuple<ulong, ulong> ChildNodeId10
            => Tuple.Create(Id.Item1, BitMask | Id.Item2);

        public Tuple<ulong, ulong> ChildNodeId11
            => Tuple.Create(BitMask | Id.Item1, BitMask | Id.Item2);

        public int TreeDepth { get; }

        public IGaQuadTreeNode<T> ChildNode00 { get; private set; }

        public IGaQuadTreeNode<T> ChildNode01 { get; private set; }

        public IGaQuadTreeNode<T> ChildNode10 { get; private set; }

        public IGaQuadTreeNode<T> ChildNode11 { get; private set; }

        public GaQuadTreeLeafNode<T> LeafChildNode00
            => ChildNode00 as GaQuadTreeLeafNode<T>;

        public GaQuadTreeLeafNode<T> LeafChildNode01
            => ChildNode01 as GaQuadTreeLeafNode<T>;

        public GaQuadTreeLeafNode<T> LeafChildNode10
            => ChildNode10 as GaQuadTreeLeafNode<T>;

        public GaQuadTreeLeafNode<T> LeafChildNode11
            => ChildNode11 as GaQuadTreeLeafNode<T>;

        public T Value 
            => default(T);

        public ulong BitMask
            => 1ul << TreeDepth;

        public bool HasChildNode00
            => !ReferenceEquals(ChildNode00, null);

        public bool HasChildNode01
            => !ReferenceEquals(ChildNode01, null);

        public bool HasChildNode10
            => !ReferenceEquals(ChildNode10, null);

        public bool HasChildNode11
            => !ReferenceEquals(ChildNode11, null);

        public bool HasNoChildNodes
            => ReferenceEquals(ChildNode01, null) && 
               ReferenceEquals(ChildNode00, null) && 
               ReferenceEquals(ChildNode11, null) &&
               ReferenceEquals(ChildNode10, null);

        public bool IsInternalNode 
            => true;

        public bool IsLeafNode 
            => false;

        /// <summary>
        /// Returns all leaf node path-ids in this tree starting from this node
        /// in depth-first order.
        /// </summary>
        public IEnumerable<Tuple<ulong, ulong>> LeafNodeIDs
        {
            get
            {
                var nodeStack = new Stack<GaQuadTreeInternalNode<T>>();

                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    if (node.HasChildNode00)
                    {
                        var childNode = node.ChildNode00;

                        if (childNode.IsInternalNode)
                            nodeStack.Push(childNode as GaQuadTreeInternalNode<T>);

                        else
                            yield return node.ChildNodeId00;
                    }

                    if (node.HasChildNode01)
                    {
                        var childNode = node.ChildNode01;

                        if (childNode.IsInternalNode)
                            nodeStack.Push(childNode as GaQuadTreeInternalNode<T>);
                        
                        else
                            yield return node.ChildNodeId01;
                    }

                    if (node.HasChildNode10)
                    {
                        var childNode = node.ChildNode10;

                        if (childNode.IsInternalNode) 
                            nodeStack.Push(childNode as GaQuadTreeInternalNode<T>);

                        else
                            yield return node.ChildNodeId10;
                    }

                    if (node.HasChildNode11)
                    {
                        var childNode = node.ChildNode11;

                        if (childNode.IsInternalNode)
                            nodeStack.Push(childNode as GaQuadTreeInternalNode<T>);
                        
                        else
                            yield return node.ChildNodeId11;
                    }
                }
            }
        }

        /// <summary>
        /// Returns all leaf node values in this tree starting from this node in
        /// depth-first order. Each node value is returned with its path-id defining
        /// its path in the tree
        /// </summary>
        public IEnumerable<Tuple<ulong, ulong, T>> LeafValuePairs
        {
            get
            {
                var nodeStack = new Stack<IGaQuadTreeNode<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    if (node.IsLeafNode)
                    {
                        yield return
                            Tuple.Create(
                                node.Id.Item1, 
                                node.Id.Item2, 
                                node.Value
                            );

                        continue;
                    }

                    if (node.HasChildNode00) 
                        nodeStack.Push(node.ChildNode00);

                    if (node.HasChildNode01) 
                        nodeStack.Push(node.ChildNode01);

                    if (node.HasChildNode10) 
                        nodeStack.Push(node.ChildNode10);

                    if (node.HasChildNode11) 
                        nodeStack.Push(node.ChildNode11);
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
                var nodeStack = new Stack<IGaQuadTreeNode<T>>();
                nodeStack.Push(this);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    if (node.IsLeafNode)
                    {
                        yield return node.Value;

                        continue;
                    }

                    if (node.HasChildNode00)
                        nodeStack.Push(node.ChildNode00);

                    if (node.HasChildNode01)
                        nodeStack.Push(node.ChildNode01);

                    if (node.HasChildNode10)
                        nodeStack.Push(node.ChildNode10);

                    if (node.HasChildNode11)
                        nodeStack.Push(node.ChildNode11);
                }
            }
        }


        public GaQuadTreeInternalNode(Tuple<ulong, ulong> id, int treeDepth)
        {
            Debug.Assert(treeDepth > 0 && treeDepth < 64);

            Id = id;
            TreeDepth = treeDepth;
        }

        public GaQuadTreeInternalNode(ulong id1, ulong id2, int treeDepth)
        {
            Debug.Assert(treeDepth > 0 && treeDepth < 64);

            Id = Tuple.Create(id1, id2);
            TreeDepth = treeDepth;
        }


        /// <summary>
        /// Gets the child node of this node. The child is specified
        /// using the input flag (false: 1st, true:2nd child)
        /// </summary>
        /// <param name="rightChild1"></param>
        /// <param name="rightChild2"></param>
        /// <returns></returns>
        public IGaQuadTreeNode<T> GetChildNode(bool rightChild1, bool rightChild2)
        {
            //First bit is 1
            if (rightChild1)
                return rightChild2 ? ChildNode11 : ChildNode01;

            //First bit is 0
            return rightChild2 ? ChildNode10 : ChildNode00;
        }

        public GaQuadTreeInternalNode<T> RemoveChildNodes()
        {
            ChildNode00 = null;
            ChildNode01 = null;
            ChildNode10 = null;
            ChildNode11 = null;

            return this;
        }

        /// <summary>
        /// Find a leaf node value given its path-id under this node, if no node
        /// is found this returns the default value
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetLeafValue(ulong index1, ulong index2, out T value)
        {
            var node = this;
            ulong bitPattern1;
            ulong bitPattern2;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern1 = (1ul << i) & index1;
                bitPattern2 = (1ul << i) & index2;
                node = node.GetChildNode(bitPattern1 != 0, bitPattern2 != 0) as GaQuadTreeInternalNode<T>;

                if (ReferenceEquals(node, null))
                {
                    value = default(T);
                    return false;
                }
            }

            bitPattern1 = 1ul & index1;
            bitPattern2 = 1ul & index2;

            if (!(node.GetChildNode(bitPattern1 != 0, bitPattern2 != 0) is GaQuadTreeLeafNode<T> leafNode))
            {
                value = default(T);
                return false;
            }

            value = leafNode.Value;
            return true;
        }

        /// <summary>
        /// Try to get a child node if it exists, else create a new internal child and return it
        /// </summary>
        /// <param name="rightChild1"></param>
        /// <param name="rightChild2"></param>
        /// <returns></returns>
        public GaQuadTreeInternalNode<T> GetOrAddInternalChildNode(bool rightChild1, bool rightChild2)
        {
            Debug.Assert(TreeDepth > 1);

            if (rightChild1)
            {
                if (rightChild2)
                {
                    if (ChildNode11 is GaQuadTreeInternalNode<T> childNode)
                        return childNode;

                    childNode = new GaQuadTreeInternalNode<T>(
                        ChildNodeId11, 
                        TreeDepth - 1
                    );

                    ChildNode11 = childNode;

                    return childNode;
                }
                else
                {
                    if (ChildNode01 is GaQuadTreeInternalNode<T> childNode)
                        return childNode;

                    childNode = new GaQuadTreeInternalNode<T>(
                        ChildNodeId01, 
                        TreeDepth - 1
                    );

                    ChildNode01 = childNode;

                    return childNode;
                }
            }
            else
            {
                if (rightChild2)
                {
                    if (ChildNode10 is GaQuadTreeInternalNode<T> childNode)
                        return childNode;

                    childNode = new GaQuadTreeInternalNode<T>(
                        ChildNodeId10, 
                        TreeDepth - 1
                    );

                    ChildNode10 = childNode;

                    return childNode;
                }
                else
                {
                    if (ChildNode00 is GaQuadTreeInternalNode<T> childNode)
                        return childNode;

                    childNode = new GaQuadTreeInternalNode<T>(
                        ChildNodeId00, 
                        TreeDepth - 1
                    );

                    ChildNode00 = childNode;

                    return childNode;
                }
            }
        }

        public void SetOrAddLeafChildNode(bool rightChild1, bool rightChild2, T value)
        {
            Debug.Assert(TreeDepth == 1);

            if (rightChild1)
            {
                if (rightChild2)
                {
                    if (ChildNode11 is GaQuadTreeLeafNode<T> childNode)
                        childNode.Value = value;
                    else
                        ChildNode11 = new GaQuadTreeLeafNode<T>(ChildNodeId11, value);
                }
                else
                {
                    if (ChildNode01 is GaQuadTreeLeafNode<T> childNode)
                        childNode.Value = value;
                    else
                        ChildNode01 = new GaQuadTreeLeafNode<T>(ChildNodeId01, value);
                }
            }
            else
            {
                if (rightChild2)
                {
                    if (ChildNode10 is GaQuadTreeLeafNode<T> childNode)
                        childNode.Value = value;
                    else
                        ChildNode10 = new GaQuadTreeLeafNode<T>(ChildNodeId10, value);
                }
                else
                {
                    if (ChildNode00 is GaQuadTreeLeafNode<T> childNode)
                        childNode.Value = value;
                    else
                        ChildNode00 = new GaQuadTreeLeafNode<T>(ChildNodeId00, value);
                }
            }
        }

        /// <summary>
        /// Given a path-id of a leaf node this finds or creates the leaf node
        /// and sets its value as given
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public GaQuadTreeInternalNode<T> SetLeafValue(ulong index1, ulong index2, T value)
        {
            var node = this;
            for (var i = TreeDepth - 1; i > 0; i--)
            {
                var bitPattern1 = (1ul << i) & index1;
                var bitPattern2 = (1ul << i) & index2;
                node = node.GetOrAddInternalChildNode(bitPattern1 != 0, bitPattern2 != 0);
            }

            node.SetOrAddLeafChildNode((1ul & index1) != 0, (1ul & index2) != 0, value);

            return this;
        }

        public Stack<IGaQuadTreeNode<T>> CreateNodesStack()
        {
            var stack = new Stack<IGaQuadTreeNode<T>>();

            stack.Push(this);

            return stack;
        }

        public Stack<GaQuadTreeInternalNode<T>> CreateInternalNodesStack()
        {
            var stack = new Stack<GaQuadTreeInternalNode<T>>();

            stack.Push(this);

            return stack;
        }
    }
}