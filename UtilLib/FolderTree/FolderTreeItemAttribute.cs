using System;
using System.Collections.Generic;

namespace UtilLib.FolderTree
{
    public class FolderTreeItemAttribute<T> where T : IComparable<T>
    {
        public static readonly FolderTreeItemAttributeComparer<T> EqualityComparer = new FolderTreeItemAttributeComparer<T>();


        public FolderTreeItem Item { get; private set; }

        public T Value { get; private set; }


        public FolderTreeItemAttribute(FolderTreeItem item, T value)
        {
            Item = item;
            Value = value;
        }
    }

    public sealed class FolderTreeItemAttributeComparer<T> : IEqualityComparer<FolderTreeItemAttribute<T>> where T : IComparable<T>
    {
        //public int Compare(FolderTreeItemAttribute<T> x, FolderTreeItemAttribute<T> y)
        //{
        //    string x_path = x.Item.ItemPath.ToLower();
        //    string y_path = y.Item.ItemPath.ToLower();

        //    if (x_path == y_path)
        //        return x.Value.CompareTo(y.Value);

        //    return x_path.CompareTo(y_path);
        //}

        public bool Equals(FolderTreeItemAttribute<T> x, FolderTreeItemAttribute<T> y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            if (ReferenceEquals(x, y))
                return true;

            var xPath = x.Item.ItemPath.ToLower();
            var yPath = y.Item.ItemPath.ToLower();

            if (xPath == yPath)
                return x.Value.CompareTo(y.Value) == 0;

            return false;
        }

        public int GetHashCode(FolderTreeItemAttribute<T> obj)
        {
            //return obj.Item.ItemPath.ToLower().GetHashCode() ^ obj.Value.GetHashCode();
            return obj.Value.GetHashCode();
        }
    }
}
