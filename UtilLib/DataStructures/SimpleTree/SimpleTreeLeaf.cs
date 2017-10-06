using System;
using System.Collections.Generic;
using System.Linq;
using TextComposerLib.Text.Linear;

namespace UtilLib.DataStructures.SimpleTree
{
    /// <summary>
    /// A leaf node of a simple tree. It contains a mutable value of type TLeaf and it can have no child nodes
    /// </summary>
    /// <typeparam name="TLeaf"></typeparam>
    [Serializable]
    public sealed class SimpleTreeLeaf<TLeaf> : SimpleTreeNode<TLeaf>
    {
        public TLeaf Value { get; set; }


        public SimpleTreeLeaf()
        {
            Value = default(TLeaf);
        }

        public SimpleTreeLeaf(TLeaf value)
        {
            Value = value;
        }

        public override IEnumerable<SimpleTreeNode<TLeaf>> ChildNodes
        {
            get { return Enumerable.Empty<SimpleTreeNode<TLeaf>>(); }
        }

        public override IEnumerable<SimpleTreeBranch<TLeaf>> ChildBranches
        {
            get { return Enumerable.Empty<SimpleTreeBranch<TLeaf>>(); }
        }

        public override void ToString(LinearComposer textBuilder)
        {
            textBuilder.Append(Value.ToString());
        }
    }
}
