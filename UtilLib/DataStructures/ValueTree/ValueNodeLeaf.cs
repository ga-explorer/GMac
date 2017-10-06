using System;
using System.Xml.Linq;
using TextComposerLib.Text.Linear;

namespace UtilLib.DataStructures.ValueTree
{
    public sealed class ValueNodeLeaf<TK, TV> : ValueNode<TK> where TK : IComparable<TK>
    {
        public TV Value { get; private set; }


        internal ValueNodeLeaf(TK key, TV value)
            : base(key)
        {
            Value = value;
        }

        private ValueNodeLeaf(ValueNodeInternal<TK> parentNode, TK key, TV value)
            : base(parentNode, key)
        {
            Value = value;
        }


        public override bool IsLeafNode
        {
            get { return true; }
        }

        public override bool IsInternalNode
        {
            get { return false; }
        }

        internal override ValueNode<TK> DuplicateStructure(ValueNodeInternal<TK> parentNode, TK key)
        {
            if (HasParent) 
                return new ValueNodeLeaf<TK, TV>(parentNode, key, Value);

            Key = key;
            ParentNode = parentNode;

            return this;
        }

        public override void ToTextTree(LinearComposer log)
        {
            log.Append(Value.ToString());
        }

        public override XElement ToXElement(TK parentKey)
        {
            return new XElement(parentKey.ToString(), new XAttribute("Key", Key.ToString()), new XAttribute("Value", Value.ToString()));
        }

        public override void LeafValuesToTextLines(TextStack textStack)
        {
            textStack.AddLeaf(Key + " = " + Value);
        }
    }
}
