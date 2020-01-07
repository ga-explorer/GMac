using System;

namespace UtilLib.DataStructures.Tree.SimpleValueTree
{
    public sealed class ValueTreeLeafNode<TValue> : ValueTreeNode, ITreeLeafNode, ITreeValueNode<TValue> where TValue : IEquatable<TValue>
    {
        public static ValueTreeLeafNode<TValue> Create(TValue value)
        {
            return new ValueTreeLeafNode<TValue>(value);
        }


        private readonly TValue _value;

        public TValue Value
        {
            get { return _value; }
        }

        public override bool IsBranchNode
        {
            get { return false; }
        }

        public override bool IsLeafNode
        {
            get { return true; }
        }


        internal ValueTreeLeafNode(TValue value)
        {
            _value = value;
        }


        public override bool Equals(ValueTreeNode other)
        {
            var otherLeaf = other as ValueTreeLeafNode<TValue>;

            if (otherLeaf == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return 
                IsLeafNode == otherLeaf.IsLeafNode 
                && Value.Equals(otherLeaf.Value);
        }

        public override void AcceptValueTreeNodeVisitor(ValueTreeNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void AcceptValueTreeNodeVisitor<TKey>(TKey key, ValueTreeNodeVisitor visitor)
        {
            visitor.Visit(key, this);
        }
    }
}
