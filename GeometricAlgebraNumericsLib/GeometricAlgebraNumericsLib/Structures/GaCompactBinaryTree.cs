using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraNumericsLib.Structures
{

    public sealed class GaCompactBinaryTree<T>
    {
        public static IGaCompactBinaryTreeNode<T> EmptyNode { get; }
            = new GaCompactBinaryTreeEmptyNode<T>();


        private GaCompactBinaryTreeLeafNode<T>[] _leafNodes;
        private GaCompactBinaryTreeInternalNode<T>[] _internalNodes;


        public GaCompactBinaryTreeInternalNode<T> RootNode 
            => _internalNodes[0];

        public int TreeDepth 
            => RootNode.TreeDepth;


        /// <summary>
        /// Returns all nodes in this tree starting from the root node in depth-first
        /// order. Each node is returned with its path-id defining its path in the
        /// tree
        /// </summary>
        public IEnumerable<KeyValuePair<ulong, IGaCompactBinaryTreeNode<T>>> NodePairs
            => GetNodePairs(RootNode);

        /// <summary>
        /// Returns all leaf nodes in this tree starting from this node in
        /// depth-first order. Each node is returned with its path-id defining
        /// its path in the tree
        /// </summary>
        public IEnumerable<KeyValuePair<ulong, GaCompactBinaryTreeLeafNode<T>>> LeafNodePairs
            => _leafNodes.Select(node =>
                new KeyValuePair<ulong, GaCompactBinaryTreeLeafNode<T>>(
                    node.Id,
                    node
                )
            );

        /// <summary>
        /// Returns all internal nodes in this tree starting from this node in
        /// depth-first order. Each node is returned with its path-id defining
        /// its path in the tree
        /// </summary>
        public IEnumerable<KeyValuePair<ulong, GaCompactBinaryTreeInternalNode<T>>> InternalNodePairs
            => GetInternalNodePairs(RootNode);

        /// <summary>
        /// Returns all leaf node values in this tree starting from this node in
        /// depth-first order. Each node value is returned with its path-id defining
        /// its path in the tree
        /// </summary>
        public IEnumerable<KeyValuePair<ulong, T>> LeafValuePairs
            => _leafNodes.Select(node =>
                new KeyValuePair<ulong, T>(
                    node.Id,
                    node.Value
                )
            );

        /// <summary>
        /// Returns all nodes in this tree starting from this node in depth-first
        /// order.
        /// </summary>
        public IEnumerable<IGaCompactBinaryTreeNode<T>> Nodes
            => GetNodes(RootNode);

        /// <summary>
        /// Returns all internal nodes in this tree starting from this node in
        /// depth-first order.
        /// </summary>
        public IEnumerable<GaCompactBinaryTreeInternalNode<T>> InternalNodes
            => GetInternalNodes(RootNode);

        /// <summary>
        /// Returns all leaf nodes in this tree starting from this node in
        /// depth-first order.
        /// </summary>
        public IEnumerable<GaCompactBinaryTreeLeafNode<T>> LeafNodes
            => _leafNodes;

        /// <summary>
        /// Returns all node path-ids in this tree starting from this node in
        /// depth-first order.
        /// </summary>
        public IEnumerable<ulong> NodeIDs
            => GetNodeIDs(RootNode);

        /// <summary>
        /// Returns all internal node path-ids in this tree starting from this
        /// node in depth-first order.
        /// </summary>
        public IEnumerable<ulong> InternalNodeIDs
            => GetInternalNodeIDs(RootNode);

        /// <summary>
        /// Returns all leaf node path-ids in this tree starting from this node
        /// in depth-first order.
        /// </summary>
        public IEnumerable<ulong> LeafNodeIDs
            => _leafNodes.Select(node => node.Id);

        /// <summary>
        /// Returns all leaf node values in this tree starting from this node
        /// in depth-first order.
        /// </summary>
        public IEnumerable<T> LeafValues
            => _leafNodes.Select(node => node.Value);

        
        public GaCompactBinaryTree(int treeDepth)
        {
            _leafNodes = new GaCompactBinaryTreeLeafNode<T>[0];
            _internalNodes = new []
            {
                new GaCompactBinaryTreeInternalNode<T>(
                    0ul, 
                    treeDepth, 
                    -1, 
                    -1
                )
            };
        }

        public GaCompactBinaryTree(int treeDepth, int termId, T termValue)
        {
            _leafNodes = new []
            {
                new GaCompactBinaryTreeLeafNode<T>(termId, termValue)
            };

            _internalNodes = new GaCompactBinaryTreeInternalNode<T>[treeDepth];

            _internalNodes[0] =
                new GaCompactBinaryTreeInternalNode<T>(
                    0ul, 
                    treeDepth, 
                    -1, 
                    -1
                );

            var leafNodeId = termId;

            var nodeIndex = 0;
            var childNodeIndex = 1;
            for (var childTreeDepth = treeDepth - 1; childTreeDepth > 0; childTreeDepth--)
            {
                var childNodeId = (1 << childTreeDepth) & leafNodeId;

                _internalNodes[childNodeIndex] = 
                    new GaCompactBinaryTreeInternalNode<T>(
                        childNodeId, 
                        childTreeDepth, 
                        -1, 
                        -1
                    );

                _internalNodes[nodeIndex] = 
                    _internalNodes[nodeIndex].GetUpdatedCopy(
                        childNodeId, 
                        childNodeIndex
                    );

                nodeIndex = childNodeIndex;
                childNodeIndex++;
            }

            _internalNodes[nodeIndex] = 
                _internalNodes[nodeIndex].GetUpdatedCopy(
                    1 & leafNodeId, 
                    0
                );
        }

        public GaCompactBinaryTree(int treeDepth, ulong termId, T termValue)
        {
            _leafNodes = new []
            {
                new GaCompactBinaryTreeLeafNode<T>(termId, termValue)
            };

            _internalNodes = new GaCompactBinaryTreeInternalNode<T>[treeDepth];

            _internalNodes[0] =
                new GaCompactBinaryTreeInternalNode<T>(
                    0ul, 
                    treeDepth, 
                    -1, 
                    -1
                );

            var leafNodeId = termId;

            var nodeIndex = 0;
            var childNodeIndex = 1;
            for (var childTreeDepth = treeDepth - 1; childTreeDepth > 0; childTreeDepth--)
            {
                var childNodeId = (1ul << childTreeDepth) & leafNodeId;

                _internalNodes[childNodeIndex] = 
                    new GaCompactBinaryTreeInternalNode<T>(
                        childNodeId, 
                        childTreeDepth, 
                        -1, 
                        -1
                    );

                _internalNodes[nodeIndex] = 
                    _internalNodes[nodeIndex].GetUpdatedCopy(
                        childNodeId, 
                        childNodeIndex
                    );

                nodeIndex = childNodeIndex;
                childNodeIndex++;
            }

            _internalNodes[nodeIndex] = 
                _internalNodes[nodeIndex].GetUpdatedCopy(
                    1 & leafNodeId, 
                    0
                );

        }

        public GaCompactBinaryTree(int treeDepth, IEnumerable<KeyValuePair<int, T>> termsList)
        {
            var leafNodesList = new List<GaCompactBinaryTreeLeafNode<T>>();
            var internalNodesList = new List<GaCompactBinaryTreeInternalNode<T>>
            {
                new GaCompactBinaryTreeInternalNode<T>(
                    0, 
                    treeDepth, 
                    -1, 
                    -1
                )
            };

            foreach (var term in termsList)
            {
                leafNodesList.Add(
                    new GaCompactBinaryTreeLeafNode<T>(term.Key, term.Value)
                );

                var leafNodeId = term.Key;
                var leafNodeIndex = leafNodesList.Count - 1;

                var nodeIndex = 0;
                for (var childTreeDepth = treeDepth - 1; childTreeDepth > 0; childTreeDepth--)
                {
                    var childNodeId = (1 << childTreeDepth) & leafNodeId;
                    var node = internalNodesList[nodeIndex];

                    if (node.HasChildNode0 && childNodeId == 0)
                    {
                        nodeIndex = node.ChildNodeIndex0;
                        continue;
                    }
                    
                    if (node.HasChildNode1 && childNodeId != 0)
                    {
                        nodeIndex = node.ChildNodeIndex1;
                        continue;
                    }

                    var childNodeIndex = internalNodesList.Count;

                    internalNodesList[nodeIndex] =
                        node.GetUpdatedCopy(
                            childNodeId,
                            childNodeIndex
                        );

                    internalNodesList.Add(
                        new GaCompactBinaryTreeInternalNode<T>(
                            childNodeId,
                            childTreeDepth,
                            -1,
                            -1
                        )
                    );

                    nodeIndex = childNodeIndex;
                }

                internalNodesList[nodeIndex] = 
                    internalNodesList[nodeIndex].GetUpdatedCopy(
                        1 & leafNodeId, 
                        leafNodeIndex
                    );
            }

            _leafNodes = leafNodesList.ToArray();
            _internalNodes = internalNodesList.ToArray();
        }

        public GaCompactBinaryTree(int treeDepth, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var leafNodesList = new List<GaCompactBinaryTreeLeafNode<T>>();
            var internalNodesList = new List<GaCompactBinaryTreeInternalNode<T>>
            {
                new GaCompactBinaryTreeInternalNode<T>(
                    0, 
                    treeDepth, 
                    -1, 
                    -1
                )
            };

            foreach (var term in termsList)
            {
                leafNodesList.Add(
                    new GaCompactBinaryTreeLeafNode<T>(term.Key, term.Value)
                );

                var leafNodeId = term.Key;
                var leafNodeIndex = leafNodesList.Count - 1;

                var nodeIndex = 0;
                for (var childTreeDepth = treeDepth - 1; childTreeDepth > 0; childTreeDepth--)
                {
                    var childNodeId = (1ul << childTreeDepth) & leafNodeId;
                    var node = internalNodesList[nodeIndex];

                    if (node.HasChildNode0 && childNodeId == 0)
                    {
                        nodeIndex = node.ChildNodeIndex0;
                        continue;
                    }
                    
                    if (node.HasChildNode1 && childNodeId != 0)
                    {
                        nodeIndex = node.ChildNodeIndex1;
                        continue;
                    }

                    var childNodeIndex = internalNodesList.Count;

                    internalNodesList[nodeIndex] =
                        node.GetUpdatedCopy(
                            childNodeId,
                            childNodeIndex
                        );

                    internalNodesList.Add(
                        new GaCompactBinaryTreeInternalNode<T>(
                            childNodeId,
                            childTreeDepth,
                            -1,
                            -1
                        )
                    );

                    nodeIndex = childNodeIndex;
                }

                internalNodesList[nodeIndex] = 
                    internalNodesList[nodeIndex].GetUpdatedCopy(
                        1 & leafNodeId, 
                        leafNodeIndex
                    );
            }

            _leafNodes = leafNodesList.ToArray();
            _internalNodes = internalNodesList.ToArray();
        }


        public IGaCompactBinaryTreeNode<T> GetChildNode0(IGaCompactBinaryTreeNode<T> parentNode)
        {
            if (parentNode.ChildNodeIndex0 < 0)
                return EmptyNode;

            if (parentNode.TreeDepth == 1)
                return _leafNodes[parentNode.ChildNodeIndex0];

            return _internalNodes[parentNode.ChildNodeIndex0];
        }

        public IGaCompactBinaryTreeNode<T> GetChildNode1(IGaCompactBinaryTreeNode<T> parentNode)
        {
            if (parentNode.ChildNodeIndex1 < 0)
                return EmptyNode;

            if (parentNode.TreeDepth == 1)
                return _leafNodes[parentNode.ChildNodeIndex1];

            return _internalNodes[parentNode.ChildNodeIndex1];
        }

        
        /// <summary>
        /// Returns all nodes in this tree starting from this node in depth-first
        /// order. Each node is returned with its path-id defining its path in the
        /// tree
        /// </summary>
        public IEnumerable<KeyValuePair<ulong, IGaCompactBinaryTreeNode<T>>> GetNodePairs(IGaCompactBinaryTreeNode<T> parentNode)
        {
            var nodeStack = new Stack<IGaCompactBinaryTreeNode<T>>(RootNode.TreeDepth + 1);
            var idStack = new Stack<ulong>(RootNode.TreeDepth + 1);
            nodeStack.Push(parentNode);
            idStack.Push(0ul);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();
                var id = idStack.Pop();

                yield return
                    new KeyValuePair<ulong, IGaCompactBinaryTreeNode<T>>(id, node);

                if (node.IsLeafNode)
                    continue;

                if (node.HasChildNode1)
                {
                    var childNode = GetChildNode1(node);

                    nodeStack.Push(childNode);
                    idStack.Push(id | childNode.BitMask);
                }

                if (node.HasChildNode0)
                {
                    var childNode = GetChildNode0(node);

                    nodeStack.Push(childNode);
                    idStack.Push(id);
                }
            }
        }

        /// <summary>
        /// Returns all internal nodes in this tree starting from this node in
        /// depth-first order. Each node is returned with its path-id defining
        /// its path in the tree
        /// </summary>
        public IEnumerable<KeyValuePair<ulong, GaCompactBinaryTreeInternalNode<T>>> GetInternalNodePairs(GaCompactBinaryTreeInternalNode<T> parentNode)
        {
            var nodeStack = new Stack<GaCompactBinaryTreeInternalNode<T>>(RootNode.TreeDepth + 1);
            var idStack = new Stack<ulong>(RootNode.TreeDepth + 1);
            nodeStack.Push(parentNode);
            idStack.Push(0ul);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();
                var id = idStack.Pop();

                yield return
                    new KeyValuePair<ulong, GaCompactBinaryTreeInternalNode<T>>(id, node);

                if (node.HasChildNode1)
                {
                    var childNode = GetChildNode1(node);

                    if (childNode.IsInternalNode)
                    {
                        nodeStack.Push((GaCompactBinaryTreeInternalNode<T>)childNode);
                        idStack.Push(id | childNode.BitMask);
                    }
                }

                if (node.HasChildNode0)
                {
                    var childNode = GetChildNode0(node);

                    if (childNode.IsInternalNode)
                    {
                        nodeStack.Push((GaCompactBinaryTreeInternalNode<T>)childNode);
                        idStack.Push(id);
                    }
                }
            }
        }

        /// <summary>
        /// Returns all leaf nodes in this tree starting from this node in
        /// depth-first order. Each node is returned with its path-id defining
        /// its path in the tree
        /// </summary>
        public IEnumerable<KeyValuePair<ulong, GaCompactBinaryTreeLeafNode<T>>> GetLeafNodePairs(GaCompactBinaryTreeInternalNode<T> parentNode)
        {
            var nodeStack = new Stack<GaCompactBinaryTreeInternalNode<T>>(RootNode.TreeDepth + 1);
            var idStack = new Stack<ulong>(RootNode.TreeDepth + 1);
            nodeStack.Push(parentNode);
            idStack.Push(0ul);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();
                var id = idStack.Pop();

                if (node.HasChildNode1)
                {
                    var childNode = GetChildNode1(node);
                    var childNodeId = id | childNode.BitMask;

                    if (childNode.IsInternalNode)
                    {
                        nodeStack.Push((GaCompactBinaryTreeInternalNode<T>)childNode);
                        idStack.Push(childNodeId);
                    }
                    else
                        yield return new KeyValuePair<ulong, GaCompactBinaryTreeLeafNode<T>>(
                            childNodeId,
                            (GaCompactBinaryTreeLeafNode<T>)childNode
                        );
                }

                if (node.HasChildNode0)
                {
                    var childNode = GetChildNode0(node);
                    var childNodeId = id;

                    if (childNode.IsInternalNode)
                    {
                        nodeStack.Push((GaCompactBinaryTreeInternalNode<T>)childNode);
                        idStack.Push(childNodeId);
                    }
                    else
                        yield return new KeyValuePair<ulong, GaCompactBinaryTreeLeafNode<T>>(
                            childNodeId,
                            (GaCompactBinaryTreeLeafNode<T>)childNode
                        );
                }
            }
        }

        /// <summary>
        /// Returns all leaf node values in this tree starting from this node in
        /// depth-first order. Each node value is returned with its path-id defining
        /// its path in the tree
        /// </summary>
        public IEnumerable<KeyValuePair<ulong, T>> GetLeafValuePairs(IGaCompactBinaryTreeNode<T> parentNode)
        {
            var nodeStack = new Stack<IGaCompactBinaryTreeNode<T>>(RootNode.TreeDepth + 1);
            var idStack = new Stack<ulong>(RootNode.TreeDepth + 1);
            nodeStack.Push(parentNode);
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
                    var childNode = GetChildNode1(node);

                    nodeStack.Push(childNode);
                    idStack.Push(id | childNode.BitMask);
                }

                if (node.HasChildNode0)
                {
                    var childNode = GetChildNode0(node);

                    nodeStack.Push(childNode);
                    idStack.Push(id);
                }
            }
        }

        /// <summary>
        /// Returns all nodes in this tree starting from this node in depth-first
        /// order.
        /// </summary>
        public IEnumerable<IGaCompactBinaryTreeNode<T>> GetNodes(IGaCompactBinaryTreeNode<T> parentNode)
        {
            var nodeStack = new Stack<IGaCompactBinaryTreeNode<T>>(RootNode.TreeDepth + 1);
            nodeStack.Push(parentNode);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                yield return node;

                if (node.IsLeafNode)
                    continue;

                if (node.HasChildNode1)
                    nodeStack.Push(GetChildNode1(node));

                if (node.HasChildNode0)
                    nodeStack.Push(GetChildNode0(node));
            }
        }

        /// <summary>
        /// Returns all internal nodes in this tree starting from this node in
        /// depth-first order.
        /// </summary>
        public IEnumerable<GaCompactBinaryTreeInternalNode<T>> GetInternalNodes(GaCompactBinaryTreeInternalNode<T> parentNode)
        {
            var nodeStack = new Stack<GaCompactBinaryTreeInternalNode<T>>(RootNode.TreeDepth + 1);
            nodeStack.Push(parentNode);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                yield return node;

                if (node.HasChildNode1)
                {
                    var childNode = GetChildNode1(node);

                    if (childNode.IsInternalNode)
                        nodeStack.Push((GaCompactBinaryTreeInternalNode<T>)childNode);
                }

                if (node.HasChildNode0)
                {
                    var childNode = GetChildNode0(node);

                    if (childNode.IsInternalNode)
                        nodeStack.Push((GaCompactBinaryTreeInternalNode<T>)childNode);
                }
            }
        }

        /// <summary>
        /// Returns all leaf nodes in this tree starting from this node in
        /// depth-first order.
        /// </summary>
        public IEnumerable<GaCompactBinaryTreeLeafNode<T>> GetLeafNodes(GaCompactBinaryTreeInternalNode<T> parentNode)
        {
            var nodeStack = new Stack<GaCompactBinaryTreeInternalNode<T>>(RootNode.TreeDepth + 1);
            nodeStack.Push(parentNode);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                if (node.HasChildNode1)
                {
                    var childNode = GetChildNode1(node);

                    if (childNode.IsInternalNode)
                        nodeStack.Push((GaCompactBinaryTreeInternalNode<T>)childNode);
                    else
                        yield return (GaCompactBinaryTreeLeafNode<T>)childNode;
                }

                if (node.HasChildNode0)
                {
                    var childNode = GetChildNode0(node);

                    if (childNode.IsInternalNode)
                        nodeStack.Push((GaCompactBinaryTreeInternalNode<T>)childNode);
                    else
                        yield return (GaCompactBinaryTreeLeafNode<T>)childNode;
                }
            }
        }

        /// <summary>
        /// Returns all node path-ids in this tree starting from this node in
        /// depth-first order.
        /// </summary>
        public IEnumerable<ulong> GetNodeIDs(IGaCompactBinaryTreeNode<T> parentNode)
        {
            var nodeStack = new Stack<IGaCompactBinaryTreeNode<T>>(RootNode.TreeDepth + 1);
            var idStack = new Stack<ulong>(RootNode.TreeDepth + 1);
            nodeStack.Push(parentNode);
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
                    var childNode = GetChildNode1(node);

                    nodeStack.Push(childNode);
                    idStack.Push(id | childNode.BitMask);
                }

                if (node.HasChildNode0)
                {
                    var childNode = GetChildNode0(node);

                    nodeStack.Push(childNode);
                    idStack.Push(id);
                }
            }
        }

        /// <summary>
        /// Returns all internal node path-ids in this tree starting from this
        /// node in depth-first order.
        /// </summary>
        public IEnumerable<ulong> GetInternalNodeIDs(GaCompactBinaryTreeInternalNode<T> parentNode)
        {
            var nodeStack = new Stack<GaCompactBinaryTreeInternalNode<T>>(RootNode.TreeDepth + 1);
            var idStack = new Stack<ulong>(RootNode.TreeDepth + 1);
            nodeStack.Push(parentNode);
            idStack.Push(0ul);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();
                var id = idStack.Pop();

                yield return id;

                if (node.HasChildNode1)
                {
                    var childNode = GetChildNode1(node);

                    if (childNode.IsInternalNode)
                    {
                        nodeStack.Push((GaCompactBinaryTreeInternalNode<T>)childNode);
                        idStack.Push(id | childNode.BitMask);
                    }
                }

                if (node.HasChildNode0)
                {
                    var childNode = GetChildNode0(node);

                    if (childNode.IsInternalNode)
                    {
                        nodeStack.Push((GaCompactBinaryTreeInternalNode<T>)childNode);
                        idStack.Push(id);
                    }
                }
            }
        }

        /// <summary>
        /// Returns all leaf node path-ids in this tree starting from this node
        /// in depth-first order.
        /// </summary>
        public IEnumerable<ulong> GetLeafNodeIDs(GaCompactBinaryTreeInternalNode<T> parentNode)
        {
            var nodeStack = new Stack<GaCompactBinaryTreeInternalNode<T>>(RootNode.TreeDepth + 1);
            var idStack = new Stack<ulong>(RootNode.TreeDepth + 1);
            nodeStack.Push(parentNode);
            idStack.Push(0ul);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();
                var id = idStack.Pop();

                if (node.HasChildNode0)
                {
                    var childNode = GetChildNode0(node);
                    var childNodeId = id;

                    if (childNode.IsInternalNode)
                    {
                        nodeStack.Push((GaCompactBinaryTreeInternalNode<T>)childNode);
                        idStack.Push(childNodeId);
                    }
                    else
                        yield return childNodeId;
                }

                if (node.HasChildNode1)
                {
                    var childNode = GetChildNode1(node);
                    var childNodeId = id | childNode.BitMask;

                    if (childNode.IsInternalNode)
                    {
                        nodeStack.Push((GaCompactBinaryTreeInternalNode<T>)childNode);
                        idStack.Push(childNodeId);
                    }
                    else
                        yield return childNodeId;
                }
            }
        }

        /// <summary>
        /// Returns all leaf node values in this tree starting from this node
        /// in depth-first order.
        /// </summary>
        public IEnumerable<T> GetLeafValues(IGaCompactBinaryTreeNode<T> parentNode)
        {
            var nodeStack = new Stack<IGaCompactBinaryTreeNode<T>>(RootNode.TreeDepth + 1);
            nodeStack.Push(parentNode);

            while (nodeStack.Count > 0)
            {
                var node = nodeStack.Pop();

                if (node.IsLeafNode)
                {
                    yield return node.Value;

                    continue;
                }

                if (node.HasChildNode1)
                    nodeStack.Push(GetChildNode1(node));

                if (node.HasChildNode0)
                    nodeStack.Push(GetChildNode0(node));
            }
        }


        /// <summary>
        /// Remove all leafs from tree where selectionFunc is true for the leaf value, and then remove
        /// parent nodes having no child nodes.
        /// </summary>
        /// <param name="selectionFunc"></param>
        /// <returns></returns>
        public GaCompactBinaryTree<T> TrimByLeafValues(Func<T, bool> selectionFunc)
        {
            var termsList =
                GetLeafValuePairs(RootNode).Where(p => selectionFunc(p.Value));

            return new GaCompactBinaryTree<T>(
                RootNode.TreeDepth,
                termsList
            );
        }

        /// <summary>
        /// Remove all leafs from tree where selectionFunc is true for the leaf id and value, and then remove
        /// parent nodes having no child nodes.
        /// </summary>
        /// <param name="selectionFunc"></param>
        /// <returns></returns>
        public GaCompactBinaryTree<T> TrimByLeafValuePairs(Func<ulong, T, bool> selectionFunc)
        {
            var termsList =
                GetLeafValuePairs(RootNode).Where(p => selectionFunc(p.Key, p.Value));

            return new GaCompactBinaryTree<T>(
                RootNode.TreeDepth,
                termsList
            );
        }

        /// <summary>
        /// Remove all leafs from tree where selectionFunc is true for the leaf id and value, and then remove
        /// parent nodes having no child nodes.
        /// </summary>
        /// <param name="selectionFunc"></param>
        /// <returns></returns>
        public GaCompactBinaryTree<T> TrimByLeafIds(Func<ulong, bool> selectionFunc)
        {
            var termsList =
                GetLeafValuePairs(RootNode).Where(p => selectionFunc(p.Key));

            return new GaCompactBinaryTree<T>(
                RootNode.TreeDepth,
                termsList
            );
        }

        /// <summary>
        /// Find a leaf node given its path-id under this node, if no node is
        /// found this returns null
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public GaCompactBinaryTreeLeafNode<T> GetLeafNode(GaCompactBinaryTreeInternalNode<T> parentNode, ulong index)
        {
            var node = parentNode;
            ulong bitPattern;

            for (var i = parentNode.TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                var childNode = GetChildNode(node, bitPattern != 0);

                if (!childNode.IsInternalNode)
                    throw new IndexOutOfRangeException();

                node = (GaCompactBinaryTreeInternalNode<T>) childNode;
            }

            bitPattern = 1ul & index;
            var finalChildNode = GetChildNode(node, bitPattern != 0);

            if (!finalChildNode.IsLeafNode)
                throw new IndexOutOfRangeException();

            return (GaCompactBinaryTreeLeafNode<T>) finalChildNode;
        }

        /// <summary>
        /// Try to find a leaf node given its path-id under this node, if no
        /// node is found this returns null
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="index"></param>
        /// <param name="leafNode"></param>
        /// <returns></returns>
        public bool TryGetLeafNode(GaCompactBinaryTreeInternalNode<T> parentNode, ulong index, out GaCompactBinaryTreeLeafNode<T> leafNode)
        {
            var node = parentNode;
            ulong bitPattern;

            for (var i = parentNode.TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                var childNode = GetChildNode(node, bitPattern != 0);

                if (!childNode.IsInternalNode)
                {
                    leafNode = new GaCompactBinaryTreeLeafNode<T>();
                    return false;
                }

                node = (GaCompactBinaryTreeInternalNode<T>) childNode;
            }

            bitPattern = 1ul & index;
            var finalChildNode = GetChildNode(node, bitPattern != 0);

            if (!finalChildNode.IsLeafNode)
            {
                leafNode = new GaCompactBinaryTreeLeafNode<T>();
                return false;
            }

            leafNode = (GaCompactBinaryTreeLeafNode<T>)finalChildNode;
            return true;
        }

        /// <summary>
        /// Find a leaf node value given its path-id under this node, if no node
        /// is found this returns the default value
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetLeafValue(GaCompactBinaryTreeInternalNode<T> parentNode, ulong index)
        {
            var node = parentNode;
            ulong bitPattern;

            for (var i = parentNode.TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                var childNode = GetChildNode(node, bitPattern != 0);

                if (!childNode.IsInternalNode)
                    return default(T);

                node = (GaCompactBinaryTreeInternalNode<T>) childNode;
            }

            bitPattern = 1ul & index;
            var finalChildNode = GetChildNode(node, bitPattern != 0);

            if (!finalChildNode.IsLeafNode)
                return default(T);

            return ((GaCompactBinaryTreeLeafNode<T>)finalChildNode).Value;
        }

        /// <summary>
        /// Find a leaf node value given its path-id under this node, if no node
        /// is found this returns the default value
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetLeafValue(ulong index)
        {
            return GetLeafValue(RootNode, index);
        }

        /// <summary>
        /// Try to find a leaf node value given its path-id under this node, if no
        /// node is found this returns the default value
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetLeafValue(GaCompactBinaryTreeInternalNode<T> parentNode, ulong index, out T value)
        {
            var node = parentNode;
            ulong bitPattern;

            for (var i = parentNode.TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                var childNode = GetChildNode(node, bitPattern != 0);

                if (!childNode.IsInternalNode)
                {
                    value = default(T);
                    return false;
                }

                node = (GaCompactBinaryTreeInternalNode<T>) childNode;
            }

            bitPattern = 1ul & index;
            var finalChildNode = GetChildNode(node, bitPattern != 0);

            if (!finalChildNode.IsLeafNode)
            {
                value = default(T);
                return false;
            }

            value = ((GaCompactBinaryTreeLeafNode<T>)finalChildNode).Value;
            return true;
        }

        /// <summary>
        /// Find a leaf node and its path-id given its path-id under this node,
        /// if no node is found this returns null
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public KeyValuePair<ulong, GaCompactBinaryTreeLeafNode<T>> GetLeafNodePair(GaCompactBinaryTreeInternalNode<T> parentNode, ulong id)
        {
            return new KeyValuePair<ulong, GaCompactBinaryTreeLeafNode<T>>(
                id, 
                GetLeafNode(parentNode, id)
            );
        }

        /// <summary>
        /// Find a leaf node value and its path-id given its path-id under this
        /// node, if no node is found this returns null
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public KeyValuePair<ulong, T> GetValuePair(GaCompactBinaryTreeInternalNode<T> parentNode, ulong id)
        {
            return new KeyValuePair<ulong, T>(id, GetLeafValue(parentNode, id));
        }

        /// <summary>
        /// Test if this contains a leaf node with the given path-id
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool ContainsLeafNodeId(GaCompactBinaryTreeInternalNode<T> parentNode, ulong index)
        {
            var node = parentNode;
            ulong bitPattern;

            for (var i = parentNode.TreeDepth - 1; i > 0; i--)
            {
                bitPattern = (1ul << i) & index;
                var childNode = GetChildNode(node, bitPattern != 0);

                if (!childNode.IsInternalNode)
                    return false;

                node = (GaCompactBinaryTreeInternalNode<T>) childNode;
            }

            bitPattern = 1ul & index;
            var finalChildNode = GetChildNode(node, bitPattern != 0);

            return finalChildNode.IsLeafNode;
        }

        /// <summary>
        /// Test if this contains a leaf node with the given path-id
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool ContainsLeafNodeId(ulong index)
        {
            return ContainsLeafNodeId(RootNode, index);
        }

        /// <summary>
        /// Tests if this node has a child node. The tested child is specified
        /// using the input flag (false: 1st, true:2nd child)
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="rightChild"></param>
        /// <returns></returns>
        public bool HasChildNode(GaCompactBinaryTreeInternalNode<T> parentNode, bool rightChild)
        {
            return rightChild 
                ? parentNode.HasChildNode1 
                : parentNode.HasChildNode0;
        }

        /// <summary>
        /// Gets the child node of this node. The child is specified
        /// using the input flag (false: 1st, true:2nd child)
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="rightChild"></param>
        /// <returns></returns>
        public IGaCompactBinaryTreeNode<T> GetChildNode(GaCompactBinaryTreeInternalNode<T> parentNode, bool rightChild)
        {
            return rightChild 
                ? GetChildNode1(parentNode) 
                : GetChildNode0(parentNode);
        }

        /// <summary>
        /// Gets the path-id of the first child node
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ulong GetChildId0(GaCompactBinaryTreeInternalNode<T> parentNode, ulong id)
        {
            return id;
        }

        /// <summary>
        /// Gets the path-id of the second child node
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ulong GetChildId1(GaCompactBinaryTreeInternalNode<T> parentNode, ulong id)
        {
            return id | (1ul << (parentNode.TreeDepth - 1));
        }

        /// <summary>
        /// Set the nodes under this node from the given internal node
        /// </summary>
        /// <param name="sourceTree"></param>
        /// <returns></returns>
        public GaCompactBinaryTree<T> FillFromTree(GaCompactBinaryTree<T> sourceTree)
        {
            if (RootNode.TreeDepth != sourceTree.RootNode.TreeDepth)
                throw new InvalidOperationException("Tree depth mismatch");

            _internalNodes = new GaCompactBinaryTreeInternalNode<T>[sourceTree._internalNodes.Length];
            _leafNodes = new GaCompactBinaryTreeLeafNode<T>[sourceTree._leafNodes.Length];

            for (var i = 0; i < _internalNodes.Length; i++)
            {
                var sourceNode = sourceTree._internalNodes[i];

                _internalNodes[i] = new GaCompactBinaryTreeInternalNode<T>(
                    sourceNode.Id,
                    sourceNode.TreeDepth,
                    sourceNode.ChildNodeIndex0,
                    sourceNode.ChildNodeIndex1
                );
            }

            for (var i = 0; i < _leafNodes.Length; i++)
            {
                var leafNode = sourceTree._leafNodes[i];

                _leafNodes[i] = new GaCompactBinaryTreeLeafNode<T>(
                    leafNode.Id, 
                    leafNode.Value
                );
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
        public GaCompactBinaryTree<T> FillFromTree<TU>(GaCompactBinaryTree<TU> sourceTree, Func<TU, T> valueConversionFunc)
        {
            if (RootNode.TreeDepth != sourceTree.RootNode.TreeDepth)
                throw new InvalidOperationException("Tree depth mismatch");

            _internalNodes = new GaCompactBinaryTreeInternalNode<T>[sourceTree._internalNodes.Length];
            _leafNodes = new GaCompactBinaryTreeLeafNode<T>[sourceTree._leafNodes.Length];

            for (var i = 0; i < _internalNodes.Length; i++)
            {
                var sourceNode = sourceTree._internalNodes[i];

                _internalNodes[i] = new GaCompactBinaryTreeInternalNode<T>(
                    sourceNode.Id,
                    sourceNode.TreeDepth,
                    sourceNode.ChildNodeIndex0,
                    sourceNode.ChildNodeIndex1
                );
            }

            for (var i = 0; i < _leafNodes.Length; i++)
            {
                var leafNode = sourceTree._leafNodes[i];

                _leafNodes[i] = new GaCompactBinaryTreeLeafNode<T>(
                    leafNode.Id,
                    valueConversionFunc(leafNode.Value)
                );
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
        public GaCompactBinaryTree<T> FillFromTree<TU>(GaCompactBinaryTree<TU> sourceTree, Func<ulong, TU, T> valueConversionFunc)
        {
            if (RootNode.TreeDepth != sourceTree.RootNode.TreeDepth)
                throw new InvalidOperationException("Tree depth mismatch");

            _internalNodes = new GaCompactBinaryTreeInternalNode<T>[sourceTree._internalNodes.Length];
            _leafNodes = new GaCompactBinaryTreeLeafNode<T>[sourceTree._leafNodes.Length];

            for (var i = 0; i < _internalNodes.Length; i++)
            {
                var sourceNode = sourceTree._internalNodes[i];

                _internalNodes[i] = new GaCompactBinaryTreeInternalNode<T>(
                    sourceNode.Id,
                    sourceNode.TreeDepth,
                    sourceNode.ChildNodeIndex0,
                    sourceNode.ChildNodeIndex1
                );
            }

            for (var i = 0; i < _leafNodes.Length; i++)
            {
                var leafNode = sourceTree._leafNodes[i];

                _leafNodes[i] = new GaCompactBinaryTreeLeafNode<T>(
                    leafNode.Id,
                    valueConversionFunc(leafNode.Id, leafNode.Value)
                );
            }

            return this;
        }

        /// <summary>
        /// Create a new tree based on the nodes of this tree
        /// </summary>
        /// <typeparam name="TU"></typeparam>
        /// <param name="valueConversionFunc"></param>
        /// <returns></returns>
        public GaCompactBinaryTree<TU> MapTree<TU>(Func<T, TU> valueConversionFunc)
        {
            var tree = new GaCompactBinaryTree<TU>(RootNode.TreeDepth);
            
            return tree.FillFromTree(this, valueConversionFunc);
        }

        /// <summary>
        /// Create a new tree based on the nodes of this tree
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public GaCompactBinaryTree<T> CopyTree()
        {
            var tree = new GaCompactBinaryTree<T>(RootNode.TreeDepth);
            
            return tree.FillFromTree(this);
        }

        /// <summary>
        /// Change the values of the leaf nodes based on the given update function
        /// </summary>
        /// <param name="valueConversionFunc"></param>
        /// <returns></returns>
        public GaCompactBinaryTree<T> UpdateLeafValues(Func<T, T> valueConversionFunc)
        {
            for (var i = 0; i < _leafNodes.Length; i++)
            {
                var leafNode = _leafNodes[i];

                _leafNodes[i] = new GaCompactBinaryTreeLeafNode<T>(
                    leafNode.Id, 
                    valueConversionFunc(leafNode.Value)
                ); 
            }

            return this;
        }

        /// <summary>
        /// Change the values of the leaf nodes based on the given update function
        /// </summary>
        /// <param name="valueConversionFunc"></param>
        /// <returns></returns>
        public GaCompactBinaryTree<T> UpdateLeafValues(Func<ulong, T, T> valueConversionFunc)
        {
            for (var i = 0; i < _leafNodes.Length; i++)
            {
                var leafNode = _leafNodes[i];

                _leafNodes[i] = new GaCompactBinaryTreeLeafNode<T>(
                    leafNode.Id, 
                    valueConversionFunc(leafNode.Id, leafNode.Value)
                ); 
            }

            return this;
        }


        public Stack<ulong> CreateNodeIDsStack()
        {
            var stack = new Stack<ulong>(RootNode.TreeDepth + 1);

            stack.Push(0ul);

            return stack;
        }

        public Stack<IGaCompactBinaryTreeNode<T>> CreateNodesStack()
        {
            var stack = new Stack<IGaCompactBinaryTreeNode<T>>(RootNode.TreeDepth + 1);

            stack.Push(RootNode);

            return stack;
        }

        public Stack<GaCompactBinaryTreeInternalNode<T>> CreateInternalNodesStack()
        {
            var stack = new Stack<GaCompactBinaryTreeInternalNode<T>>(RootNode.TreeDepth + 1);

            stack.Push(RootNode);

            return stack;
        }


        public override string ToString()
        {
            var textComposer = new LinearTextComposer();

            var idStack = new Stack<ulong>();
            var nodeStack = new Stack<IGaCompactBinaryTreeNode<T>>();

            idStack.Push(0ul);
            nodeStack.Push(RootNode);

            while (nodeStack.Count > 0)
            {
                var id = idStack.Pop();
                var node = nodeStack.Pop();
                var level = RootNode.TreeDepth - node.TreeDepth;

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

                if (node.TreeDepth == RootNode.TreeDepth)
                    textComposer
                        .AppendAtNewLine("<")
                        .Append("".PadRight(RootNode.TreeDepth, '-'))
                        .Append("> ")
                        .AppendLine("Root");
                else
                    textComposer
                        .AppendAtNewLine("<")
                        .Append(
                            id
                                .PatternToString(RootNode.TreeDepth)
                                .Substring(0, level)
                                .PadRight(RootNode.TreeDepth, '-')
                        )
                        .Append("> ")
                        .Append("".PadRight(level * 2, ' '))
                        .AppendLine("Node");

                if (node.HasChildNode1)
                {
                    var childNode = GetChildNode1(node);

                    idStack.Push(id | childNode.BitMask);
                    nodeStack.Push(childNode);
                }

                if (node.HasChildNode0)
                {
                    var childNode = GetChildNode0(node);

                    idStack.Push(id);
                    nodeStack.Push(childNode);
                }
            }

            return textComposer.ToString();
        }
    }
}