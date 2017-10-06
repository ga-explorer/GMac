using System;

namespace UtilLib.DataStructures.ValueTree
{
    public interface IValueTreeNodeInfo<TK> where TK : IComparable<TK>
    {
        ValueNode<TK> AssociatedNode { get; }
    }
}
