using System;

namespace GeometricAlgebraNumericsLib.Structures
{
    /// <summary>
    /// This interface represents a quad tree node to hold information for
    /// implementing efficient computations on sparse multivectors
    /// </summary>
    /// <typeparam name="T">The type of values in the leaf nodes</typeparam>
    public interface IGaQuadTreeNode<T>
    {
        /// <summary>
        /// The ID of this node in the tree
        /// </summary>
        Tuple<ulong, ulong> Id { get; }

        /// <summary>
        /// The ID of child 00 of this node in the tree
        /// </summary>
        Tuple<ulong, ulong> ChildNodeId00 { get; }

        /// <summary>
        /// The ID of child 01 of this node in the tree
        /// </summary>
        Tuple<ulong, ulong> ChildNodeId01 { get; }

        /// <summary>
        /// The ID of child 10 of this node in the tree
        /// </summary>
        Tuple<ulong, ulong> ChildNodeId10 { get; }

        /// <summary>
        /// The ID of child 11 of this node in the tree
        /// </summary>
        Tuple<ulong, ulong> ChildNodeId11 { get; }

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
        IGaQuadTreeNode<T> ChildNode00 { get; }

        /// <summary>
        /// The second child node of this node
        /// </summary>
        IGaQuadTreeNode<T> ChildNode01 { get; }

        /// <summary>
        /// The third child node of this node
        /// </summary>
        IGaQuadTreeNode<T> ChildNode10 { get; }

        /// <summary>
        /// The fourth child node of this node
        /// </summary>
        IGaQuadTreeNode<T> ChildNode11 { get; }

        /// <summary>
        /// The first leaf child node of this node
        /// </summary>
        GaQuadTreeLeafNode<T> LeafChildNode00 { get; }

        /// <summary>
        /// The second leaf child node of this node
        /// </summary>
        GaQuadTreeLeafNode<T> LeafChildNode01 { get; }

        /// <summary>
        /// The third leaf child node of this node
        /// </summary>
        GaQuadTreeLeafNode<T> LeafChildNode10 { get; }

        /// <summary>
        /// The fourth leaf child node of this node
        /// </summary>
        GaQuadTreeLeafNode<T> LeafChildNode11 { get; }

        /// <summary>
        /// The number of child node levels under this node. For a leaf node this is zero, and
        /// for a root node this is equal to the vector space dimension of the space
        /// </summary>
        int TreeDepth { get; }

        /// <summary>
        /// True if this node has a first child node
        /// </summary>
        bool HasChildNode00 { get; }

        /// <summary>
        /// True if this node has a second child node
        /// </summary>
        bool HasChildNode01 { get; }

        /// <summary>
        /// True if this node has a third child node
        /// </summary>
        bool HasChildNode10 { get; }

        /// <summary>
        /// True if this node has a fourth child node
        /// </summary>
        bool HasChildNode11 { get; }

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