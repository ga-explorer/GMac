using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TextComposerLib.Text.Linear;

namespace UtilLib.DataStructures.ValueTree
{
    /// <summary>
    /// The base class for value nodes
    /// </summary>
    /// <typeparam name="TK">The key</typeparam>
    public abstract class ValueNode<TK> where TK : IComparable<TK>
    {
        public TK Key { get; protected set; }

        private ValueNodeInternal<TK> _parentNode;

        public ValueNodeInternal<TK> ParentNode 
        { 
            get
            {
                return _parentNode;
            }
            set
            {
                if (_parentNode == null)
                    _parentNode = value;

                else
                    throw new InvalidOperationException("Parent Node Already Assigned");
            }
        }

        public IValueTreeNodeInfo<TK> NodeInfo = null;

        public bool HasParent { get { return ReferenceEquals(ParentNode, null) == false; } }


        protected ValueNode(TK key)
        {
            if (ReferenceEquals(key, null))
                throw new InvalidOperationException();

            ParentNode = null;
            Key = key;
        }

        protected ValueNode(ValueNodeInternal<TK> parentNode, TK key)
        {
            if (ReferenceEquals(key, null))
                throw new InvalidOperationException();

            ParentNode = parentNode;
            Key = key;
        }


        public abstract bool IsLeafNode { get; }

        public abstract bool IsInternalNode { get; }

        internal abstract ValueNode<TK> DuplicateStructure(ValueNodeInternal<TK> parentNode, TK key);

        public abstract XElement ToXElement(TK parentKey);

        public abstract void ToTextTree(LinearComposer log);

        public string ToTextTree()
        {
            var log = new LinearComposer();

            ToTextTree(log);

            return log.ToString();
        }

        public abstract void LeafValuesToTextLines(TextStack textStack);

        public IEnumerable<string> LeafValuesToTextLines(string separator = ".")
        {
            var textStack = new TextStack(separator);

            LeafValuesToTextLines(textStack);

            return textStack.Lines;
        }


        public override string ToString()
        {
            //return this.ToXElement(default(K)).ToString();
            return ToTextTree();
        }


        public static ValueNodeInternal<TK> CreateInternalNode(TK key)
        {
            return new ValueNodeInternal<TK>(key);
        }

        public static ValueNodeLeaf<TK, TV> CreateLeafNode<TV>(TK key, TV value)
        {
            return new ValueNodeLeaf<TK, TV>(key, value);
        }
    }
}
