using System;
using System.Collections.Generic;
using TextComposerLib.Text.Linear;

namespace UtilLib.DataStructures.SimpleTree
{
    /// <summary>
    /// A simple tree is a node with possibly several child nodes of the same base class type.
    /// In a simple tree only a parent can have reference to the child node.
    /// Several kinds of nodes exist derived from this base class
    /// </summary>
    /// <typeparam name="TLeaf"></typeparam>
    [Serializable]
    public abstract class SimpleTreeNode<TLeaf>
    {
        public abstract IEnumerable<SimpleTreeNode<TLeaf>> ChildNodes { get; }

        public abstract IEnumerable<SimpleTreeBranch<TLeaf>> ChildBranches { get; }


        public bool IsLeaf { get { return this is SimpleTreeLeaf<TLeaf>; } }

        public bool IsBranchDictionaryByName { get { return this is SimpleTreeBranchDictionaryByName<TLeaf>; } }

        public bool IsBranchDictionaryByIndex { get { return this is SimpleTreeBranchDictionaryByIndex<TLeaf>; } }

        public bool IsBranchList { get { return this is SimpleTreeBranchList<TLeaf>; } }

        public bool IsNodeDictionaryByName { get { return this is SimpleTreeNodeDictionaryByName<TLeaf>; } }

        public bool IsNodeDictionaryByIndex { get { return this is SimpleTreeNodeDictionaryByIndex<TLeaf>; } }

        public bool IsNodeList { get { return this is SimpleTreeNodeList<TLeaf>; } }


        public SimpleTreeLeaf<TLeaf> 
            AsLeaf { get { return this as SimpleTreeLeaf<TLeaf>; } }

        public SimpleTreeBranchDictionaryByName<TLeaf> 
            AsBranchDictionaryByName { get { return this as SimpleTreeBranchDictionaryByName<TLeaf>; } }

        public SimpleTreeBranchDictionaryByIndex<TLeaf>
            AsBranchDictionaryByIndex { get { return this as SimpleTreeBranchDictionaryByIndex<TLeaf>; } }

        public SimpleTreeBranchList<TLeaf> 
            AsBranchList { get { return this as SimpleTreeBranchList<TLeaf>; } }

        public SimpleTreeNodeDictionaryByName<TLeaf> 
            AsNodeDictionaryByName { get { return this as SimpleTreeNodeDictionaryByName<TLeaf>; } }

        public SimpleTreeNodeDictionaryByIndex<TLeaf>
            AsNodeDictionaryByIndex { get { return this as SimpleTreeNodeDictionaryByIndex<TLeaf>; } }

        public SimpleTreeNodeList<TLeaf> 
            AsNodeList { get { return this as SimpleTreeNodeList<TLeaf>; } }

        public abstract void ToString(LinearComposer textBuilder);

        public override string ToString()
        {
            var textBuilder = new LinearComposer();

            ToString(textBuilder);

            return textBuilder.ToString();
        }
    }
}
