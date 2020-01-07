using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UtilLib.DataStructures.Tree.SimpleValueTree
{
    public sealed class ValueTreeBranchNode<TKey> : ValueTreeNode, ITreeDictionaryBranchNode<TKey>
    {
        public static ValueTreeBranchNode<TKey> Create()
        {
            return new ValueTreeBranchNode<TKey>();
        }


        private readonly Dictionary<TKey, ValueTreeNode> _childNodesDictionary = new Dictionary<TKey, ValueTreeNode>();


        public override bool IsBranchNode
        {
            get { return true; }
        }

        public override bool IsLeafNode
        {
            get { return false; }
        }

        public bool HasChildNodes
        {
            get { return _childNodesDictionary.Count > 0; }
        }

        public IEnumerable<ITreeNode> ChildNodes
        {
            get { return _childNodesDictionary.Values; }
        }

        public IEnumerable<ITreeNode> DescendantNodes
        {
            get 
            { 
                throw new NotImplementedException(); 
            }
        }

        public IEnumerable<ITreeNode> DownTreeNodes
        {
            get 
            { 
                throw new NotImplementedException(); 
            }
        }

        public void Add(TKey key, ITreeNode value)
        {
            _childNodesDictionary.Add(key, (ValueTreeNode)value);
        }

        public bool ContainsKey(TKey key)
        {
            return _childNodesDictionary.ContainsKey(key);
        }

        public ICollection<TKey> Keys
        {
            get { return _childNodesDictionary.Keys; }
        }

        public bool Remove(TKey key)
        {
            return _childNodesDictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out ITreeNode value)
        {
            ValueTreeNode v;

            if (_childNodesDictionary.TryGetValue(key, out v))
            {
                value = v;
                return true;
            }

            value = null;
            return false;
        }

        public ICollection<ITreeNode> Values
        {
            get { throw new NotImplementedException(); }
        }

        public ITreeNode this[TKey key]
        {
            get
            {
                return _childNodesDictionary[key];
            }
            set
            {
                _childNodesDictionary[key] = (ValueTreeNode)value;
            }
        }

        public void Add(KeyValuePair<TKey, ITreeNode> item)
        {
            _childNodesDictionary.Add(item.Key, (ValueTreeNode)item.Value);
        }

        public void Clear()
        {
            _childNodesDictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, ITreeNode> item)
        {
            ValueTreeNode childNode;

            return 
                _childNodesDictionary
                .TryGetValue(item.Key, out childNode) 
                && childNode.Equals(item.Value as ValueTreeNode);
        }

        public void CopyTo(KeyValuePair<TKey, ITreeNode>[] array, int arrayIndex)
        {
            var i = arrayIndex;

            foreach (var pair in _childNodesDictionary)
                array[i++] = new KeyValuePair<TKey,ITreeNode>(pair.Key, pair.Value);
        }

        public int Count
        {
            get { return _childNodesDictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<TKey, ITreeNode> item)
        {
            ValueTreeNode childNode;

            if (_childNodesDictionary.TryGetValue(item.Key, out childNode) == false)
                return false;

            return 
                childNode
                .Equals(item.Value as ValueTreeNode) 
                && _childNodesDictionary.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<TKey, ITreeNode>> GetEnumerator()
        {
            return 
                _childNodesDictionary
                .Select(
                    pair => 
                        new KeyValuePair<TKey, ITreeNode>(pair.Key, pair.Value)
                    )
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public override bool Equals(ValueTreeNode other)
        {
            var otherBranch = other as ValueTreeBranchNode<TKey>;

            //The other node must be the same type as this one
            if (otherBranch == null)
                return false;

            //Return true for reference equality
            if (ReferenceEquals(this, other))
                return true;

            //The number of child nodes must be equal
            if (_childNodesDictionary.Count != otherBranch._childNodesDictionary.Count)
                return false;

            //Each child node in the other node must be equal to a child with the same key of this node
            ValueTreeNode otherChildNode;

            return 
                _childNodesDictionary
                .All(
                    pair => 
                        otherBranch._childNodesDictionary.TryGetValue(pair.Key, out otherChildNode) 
                        && pair.Value.Equals(otherChildNode)
                    );
        }

        public override void AcceptValueTreeNodeVisitor(ValueTreeNodeVisitor visitor)
        {
            visitor.Visit(this);

            foreach (var pair in _childNodesDictionary)
                pair.Value.AcceptValueTreeNodeVisitor(pair.Key, visitor);
        }

        public override void AcceptValueTreeNodeVisitor<TK>(TK key, ValueTreeNodeVisitor visitor)
        {
            visitor.Visit(key, this);

            foreach (var pair in _childNodesDictionary)
                pair.Value.AcceptValueTreeNodeVisitor(pair.Key, visitor);
        }
    }
}
