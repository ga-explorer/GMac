using System;

namespace UtilLib.DataStructures.ValueTree
{
    public interface IValueTreeSource<TKey> where TKey : IComparable<TKey>
    {
        ValueNodeInternal<TKey> ToValueTree();
    }
}
