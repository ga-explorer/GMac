using System;

namespace UtilLib.DataStructures.Tree.SimpleValueTree
{
    public abstract class ValueTreeNode : ITreeNode, IEquatable<ValueTreeNode>
    {
        public bool IsRootNode
        {
            get { throw new NotImplementedException(); }
        }

        public abstract bool IsBranchNode { get; }

        public abstract bool IsLeafNode { get; }

        public int NodeLevel
        {
            get { throw new NotImplementedException(); }
        }


        //protected ValueTreeNode()
        //{
        //}


        public abstract bool Equals(ValueTreeNode other);

        public abstract void AcceptValueTreeNodeVisitor(ValueTreeNodeVisitor visitor);

        public abstract void AcceptValueTreeNodeVisitor<TKey>(TKey key, ValueTreeNodeVisitor visitor);
    }
}
