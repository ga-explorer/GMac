using System;

namespace UtilLib.DataStructures.Tree.SimpleValueTree
{
    public abstract class ValueTreeNodeVisitor
    {
        public abstract void Visit<TNodeValue>(ValueTreeLeafNode<TNodeValue> node) where TNodeValue : IEquatable<TNodeValue>;

        public abstract void Visit<TKey>(ValueTreeBranchNode<TKey> node);

        public abstract void Visit<TKey, TNodeValue>(TKey key, ValueTreeLeafNode<TNodeValue> node) where TNodeValue : IEquatable<TNodeValue>;

        public abstract void Visit<TKey, TNodeKey>(TKey key, ValueTreeBranchNode<TNodeKey> node);
    }
}
