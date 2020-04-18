using GeometricAlgebraNumericsLib.Structures.BinaryTrees.NodeInfo;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTrees
{
    /// <summary>
    /// This interface represents a binary tree to hold information for
    /// implementing efficient computations on sparse multivectors
    /// </summary>
    /// <typeparam name="T">The type of values in the leaf nodes</typeparam>
    public interface IGaBtrNode<T>
    {
        /// <summary>
        /// The value this node holds, only leaf nodes can have
        /// non-default values
        /// </summary>
        T Value { get; }

        /// <summary>
        /// True if this node is an internal node (non-leaf node)
        /// </summary>
        bool IsInternalNode { get; }

        /// <summary>
        /// True if this is a leaf node
        /// </summary>
        bool IsLeafNode { get; }

        /// <summary>
        /// The first child node of this node
        /// </summary>
        IGaBtrNode<T> ChildNode0 { get; }

        /// <summary>
        /// The second child node of this node
        /// </summary>
        IGaBtrNode<T> ChildNode1 { get; }

        /// <summary>
        /// The first leaf child node of this node
        /// </summary>
        GaBtrLeafNode<T> LeafChildNode0 { get; }

        /// <summary>
        /// The second child leaf node of this node
        /// </summary>
        GaBtrLeafNode<T> LeafChildNode1 { get; }

        /// <summary>
        /// True if this node has a first child node
        /// </summary>
        bool HasChildNode0 { get; }

        /// <summary>
        /// True if this node has a second child node
        /// </summary>
        bool HasChildNode1 { get; }

        /// <summary>
        /// True if this node has no child nodes
        /// </summary>
        bool HasNoChildNodes { get; }

        /// <summary>
        /// Traverse the nodes under this node to find the actual tree depth
        /// </summary>
        /// <returns></returns>
        int GetTreeDepth();

        /// <summary>
        /// Create a tree node info structure holding information about this node
        /// used during tree traversal
        /// </summary>
        /// <param name="treeDepth"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        GaBinaryTreeNodeInfo1<T> GetNodeInfo(int treeDepth, ulong id);
    }
}