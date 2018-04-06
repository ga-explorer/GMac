using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GMac.GMacMath.Structures
{
    public sealed class GMacQuadTree<T> : IGMacQuadTreeNode<T>
    {
        public int TreeDepth { get; }

        public IGMacQuadTreeNode<T> ChildNode00 { get; private set; }

        public IGMacQuadTreeNode<T> ChildNode01 { get; private set; }

        public IGMacQuadTreeNode<T> ChildNode10 { get; private set; }

        public IGMacQuadTreeNode<T> ChildNode11 { get; private set; }

        public GMacQuadTreeLeafNode<T> LeafChildNode00
            => ChildNode00 as GMacQuadTreeLeafNode<T>;

        public GMacQuadTreeLeafNode<T> LeafChildNode01
            => ChildNode01 as GMacQuadTreeLeafNode<T>;

        public GMacQuadTreeLeafNode<T> LeafChildNode10
            => ChildNode10 as GMacQuadTreeLeafNode<T>;

        public GMacQuadTreeLeafNode<T> LeafChildNode11
            => ChildNode11 as GMacQuadTreeLeafNode<T>;

        public T Value => default(T);

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

        public bool IsParentNode => true;

        public bool IsLeafNode => false;

        public IEnumerable<Tuple<ulong, ulong>> LeafNodeIDs
        {
            get
            {
                var nodeStack = new Stack<GMacQuadTree<T>>();
                var idStack1 = new Stack<ulong>();
                var idStack2 = new Stack<ulong>();

                nodeStack.Push(this);
                idStack1.Push(0ul);
                idStack2.Push(0ul);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();
                    var id1 = idStack1.Pop();
                    var id2 = idStack2.Pop();

                    if (node.HasChildNode00)
                    {
                        var childNode = node.ChildNode00;
                        var childNodeId1 = id1;
                        var childNodeId2 = id2;

                        if (childNode.IsParentNode)
                        {
                            nodeStack.Push(childNode as GMacQuadTree<T>);
                            idStack1.Push(childNodeId1);
                            idStack2.Push(childNodeId2);
                        }
                        else
                            yield return Tuple.Create(childNodeId1, childNodeId2);
                    }

                    if (node.HasChildNode01)
                    {
                        var childNode = node.ChildNode01;
                        var childNodeId1 = id1 | childNode.BitMask;
                        var childNodeId2 = id2;

                        if (childNode.IsParentNode)
                        {
                            nodeStack.Push(childNode as GMacQuadTree<T>);
                            idStack1.Push(childNodeId1);
                            idStack2.Push(childNodeId2);
                        }
                        else
                            yield return Tuple.Create(childNodeId1, childNodeId2);
                    }

                    if (node.HasChildNode10)
                    {
                        var childNode = node.ChildNode10;
                        var childNodeId1 = id1;
                        var childNodeId2 = id2 | childNode.BitMask;

                        if (childNode.IsParentNode)
                        {
                            nodeStack.Push(childNode as GMacQuadTree<T>);
                            idStack1.Push(childNodeId1);
                            idStack2.Push(childNodeId2);
                        }
                        else
                            yield return Tuple.Create(childNodeId1, childNodeId2);
                    }

                    if (node.HasChildNode11)
                    {
                        var childNode = node.ChildNode11;
                        var childNodeId1 = id1 | childNode.BitMask;
                        var childNodeId2 = id2 | childNode.BitMask;

                        if (childNode.IsParentNode)
                        {
                            nodeStack.Push(childNode as GMacQuadTree<T>);
                            idStack1.Push(childNodeId1);
                            idStack2.Push(childNodeId2);
                        }
                        else
                            yield return Tuple.Create(childNodeId1, childNodeId2);
                    }
                }
            }
        }

        public IEnumerable<Tuple<ulong, ulong, T>> LeafValuePairs
        {
            get
            {
                var nodeStack = new Stack<IGMacQuadTreeNode<T>>();

                var idStack1 = new Stack<ulong>();
                var idStack2 = new Stack<ulong>();

                nodeStack.Push(this);

                idStack1.Push(0ul);
                idStack2.Push(0ul);

                while (nodeStack.Count > 0)
                {
                    var node = nodeStack.Pop();

                    var id1 = idStack1.Pop();
                    var id2 = idStack2.Pop();

                    if (node.IsLeafNode)
                    {
                        yield return
                            Tuple.Create(id1, id2, node.Value);

                        continue;
                    }

                    if (node.HasChildNode00)
                    {
                        nodeStack.Push(node.ChildNode00);
                        idStack1.Push(id1);
                        idStack2.Push(id2);
                    }

                    if (node.HasChildNode01)
                    {
                        nodeStack.Push(node.ChildNode01);
                        idStack1.Push(id1 | node.ChildNode01.BitMask);
                        idStack2.Push(id2);
                    }

                    if (node.HasChildNode10)
                    {
                        nodeStack.Push(node.ChildNode10);
                        idStack1.Push(id1);
                        idStack2.Push(id2 | node.ChildNode10.BitMask);
                    }

                    if (node.HasChildNode11)
                    {
                        nodeStack.Push(node.ChildNode11);
                        idStack1.Push(id1 | node.ChildNode11.BitMask);
                        idStack2.Push(id2 | node.ChildNode11.BitMask);
                    }
                }
            }
        }

        public IEnumerable<T> LeafValues
        {
            get
            {
                var nodeStack = new Stack<IGMacQuadTreeNode<T>>();
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


        public GMacQuadTree(int treeDepth)
        {
            Debug.Assert(treeDepth > 0 && treeDepth < 64);

            TreeDepth = treeDepth;
        }


        public IGMacQuadTreeNode<T> GetChildNode(bool rightChild1, bool rightChild2)
        {
            //First bit is 1
            if (rightChild1)
                return rightChild2 ? ChildNode11 : ChildNode01;

            //First bit is 0
            return rightChild2 ? ChildNode10 : ChildNode00;
        }

        public GMacQuadTree<T> RemoveChildNodes()
        {
            ChildNode00 = null;
            ChildNode01 = null;
            ChildNode10 = null;
            ChildNode11 = null;

            return this;
        }

        public bool TryGetLeafValue(ulong index1, ulong index2, out T value)
        {
            var node = this;
            ulong bitPattern1;
            ulong bitPattern2;

            for (var i = TreeDepth - 1; i > 0; i--)
            {
                bitPattern1 = (1ul << i) & index1;
                bitPattern2 = (1ul << i) & index2;
                node = node.GetChildNode(bitPattern1 != 0, bitPattern2 != 0) as GMacQuadTree<T>;

                if (ReferenceEquals(node, null))
                {
                    value = default(T);
                    return false;
                }
            }

            bitPattern1 = 1ul & index1;
            bitPattern2 = 1ul & index2;
            var leafNode = node.GetChildNode(bitPattern1 != 0, bitPattern2 != 0) as GMacQuadTreeLeafNode<T>;

            if (ReferenceEquals(leafNode, null))
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
        public GMacQuadTree<T> GetOrAddInternalChildNode(bool rightChild1, bool rightChild2)
        {
            Debug.Assert(TreeDepth > 1);

            if (rightChild1)
            {
                if (rightChild2)
                {
                    var childNode = ChildNode11 as GMacQuadTree<T>;

                    if (!ReferenceEquals(childNode, null))
                        return childNode;

                    childNode = new GMacQuadTree<T>(TreeDepth - 1);
                    ChildNode11 = childNode;

                    return childNode;
                }
                else
                {
                    var childNode = ChildNode01 as GMacQuadTree<T>;

                    if (!ReferenceEquals(childNode, null))
                        return childNode;

                    childNode = new GMacQuadTree<T>(TreeDepth - 1);
                    ChildNode01 = childNode;

                    return childNode;
                }
            }
            else
            {
                if (rightChild2)
                {
                    var childNode = ChildNode10 as GMacQuadTree<T>;

                    if (!ReferenceEquals(childNode, null))
                        return childNode;

                    childNode = new GMacQuadTree<T>(TreeDepth - 1);
                    ChildNode10 = childNode;

                    return childNode;
                }
                else
                {
                    var childNode = ChildNode00 as GMacQuadTree<T>;

                    if (!ReferenceEquals(childNode, null))
                        return childNode;

                    childNode = new GMacQuadTree<T>(TreeDepth - 1);
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
                    var childNode = ChildNode11 as GMacQuadTreeLeafNode<T>;

                    if (!ReferenceEquals(childNode, null))
                        childNode.Value = value;
                    else
                        ChildNode11 = new GMacQuadTreeLeafNode<T>(value);
                }
                else
                {
                    var childNode = ChildNode01 as GMacQuadTreeLeafNode<T>;

                    if (!ReferenceEquals(childNode, null))
                        childNode.Value = value;
                    else
                        ChildNode01 = new GMacQuadTreeLeafNode<T>(value);
                }
            }
            else
            {
                if (rightChild2)
                {
                    var childNode = ChildNode10 as GMacQuadTreeLeafNode<T>;

                    if (!ReferenceEquals(childNode, null))
                        childNode.Value = value;
                    else
                        ChildNode10 = new GMacQuadTreeLeafNode<T>(value);
                }
                else
                {
                    var childNode = ChildNode00 as GMacQuadTreeLeafNode<T>;

                    if (!ReferenceEquals(childNode, null))
                        childNode.Value = value;
                    else
                        ChildNode00 = new GMacQuadTreeLeafNode<T>(value);
                }
            }
        }

        public GMacQuadTree<T> SetLeafValue(ulong index1, ulong index2, T value)
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

        public Stack<ulong> CreateNodeIDsStack()
        {
            var stack = new Stack<ulong>();

            stack.Push(0ul);

            return stack;
        }

        public Stack<IGMacQuadTreeNode<T>> CreateNodesStack()
        {
            var stack = new Stack<IGMacQuadTreeNode<T>>();

            stack.Push(this);

            return stack;
        }

        public Stack<GMacQuadTree<T>> CreateParentNodesStack()
        {
            var stack = new Stack<GMacQuadTree<T>>();

            stack.Push(this);

            return stack;
        }

    }
}