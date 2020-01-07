using System;

namespace GeometricAlgebraNumericsLib.Structures
{
    /// <summary>
    /// This class represents a leaf node in a quad tree used for efficient
    /// computations on sparse multivectors and sparse linear maps on
    /// multivectors
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GaQuadTreeLeafNode<T> : IGaQuadTreeNode<T>
    {
        public Tuple<ulong, ulong> Id { get; }

        public Tuple<ulong, ulong> ChildNodeId00 
            => Tuple.Create(0ul, 0ul);

        public Tuple<ulong, ulong> ChildNodeId01
            => Tuple.Create(0ul, 0ul);

        public Tuple<ulong, ulong> ChildNodeId10
            => Tuple.Create(0ul, 0ul);

        public Tuple<ulong, ulong> ChildNodeId11
            => Tuple.Create(0ul, 0ul);

        public T Value { get; set; }

        public bool IsInternalNode => false;

        public bool IsLeafNode => true;

        public IGaQuadTreeNode<T> ChildNode00 => null;

        public IGaQuadTreeNode<T> ChildNode01 => null;

        public IGaQuadTreeNode<T> ChildNode10 => null;

        public IGaQuadTreeNode<T> ChildNode11 => null;

        public GaQuadTreeLeafNode<T> LeafChildNode00 => null;

        public GaQuadTreeLeafNode<T> LeafChildNode01 => null;

        public GaQuadTreeLeafNode<T> LeafChildNode10 => null;

        public GaQuadTreeLeafNode<T> LeafChildNode11 => null;

        public int TreeDepth => 0;

        public bool HasChildNode00 => false;

        public bool HasChildNode01 => false;

        public bool HasChildNode10 => false;

        public bool HasChildNode11 => false;

        public bool HasNoChildNodes => true;

        public ulong BitMask => 1ul;


        internal GaQuadTreeLeafNode(Tuple<ulong, ulong> id, T value)
        {
            Id = id;
            Value = value;
        }

        internal GaQuadTreeLeafNode(ulong id1, ulong id2, T value)
        {
            Id = Tuple.Create(id1, id2);
            Value = value;
        }
    }
}