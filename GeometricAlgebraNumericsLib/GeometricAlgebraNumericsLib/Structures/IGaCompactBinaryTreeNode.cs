namespace GeometricAlgebraNumericsLib.Structures
{
    /// <summary>
    /// This interface represents a binary tree to hold information for
    /// implementing efficient computations on sparse multivectors
    /// </summary>
    /// <typeparam name="T">The type of values in the leaf nodes</typeparam>
    public interface IGaCompactBinaryTreeNode<out T>
    {
        /// <summary>
        /// True if this node is an internal node (non-leaf node)
        /// </summary>
        bool IsInternalNode { get; }

        /// <summary>
        /// True if this is a leaf node
        /// </summary>
        bool IsLeafNode { get; }

        /// <summary>
        /// True if this is an empty node
        /// </summary>
        bool IsEmptyNode { get; }

        /// <summary>
        /// The first child node of this node
        /// </summary>
        int ChildNodeIndex0 { get; }

        /// <summary>
        /// The second child node of this node
        /// </summary>
        int ChildNodeIndex1 { get; }
        
        /// <summary>
        /// The path ID of this node
        /// </summary>
        ulong Id { get; }

        /// <summary>
        /// The value this node holds, only leaf nodes can have
        /// non-default values
        /// </summary>
        T Value { get; }

        /// <summary>
        /// The number of child node levels under this node. For a leaf node this is zero, and
        /// for a root node this is equal to the vector space dimension of the space
        /// </summary>
        int TreeDepth { get; }

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
        /// The bit mask associated with this node which is composed depending
        /// on the tree depth of this node
        /// </summary>
        ulong BitMask { get; }

    }
}