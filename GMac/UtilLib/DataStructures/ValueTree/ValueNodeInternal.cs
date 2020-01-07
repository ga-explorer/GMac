using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TextComposerLib.Text.Linear;

namespace UtilLib.DataStructures.ValueTree
{
    public sealed class ValueNodeInternal<TK> : ValueNode<TK> where TK : IComparable<TK>
    {
        private readonly Dictionary<TK, ValueNode<TK>> _chidNodesDictionary = new Dictionary<TK, ValueNode<TK>>();


        internal ValueNodeInternal(TK key)
            : base(key)
        {
        }

        private ValueNodeInternal(ValueNodeInternal<TK> parentNode, TK key)
            : base(parentNode, key)
        {
        }

        //private ValueTreeInternalNode(K key, ValueTreeInternalNode<K> src_node)
        //    : base(key)
        //{
        //    foreach (var pair in src_node._ChidNodesDictionary)
        //        _ChidNodesDictionary.Add(pair.Key, pair.Value);
        //}


        public bool TrySeparateKeysList(IEnumerable<TK> keysList, out ValueNodeInternal<TK> parentNode, out ValueNode<TK> childNode)
        {
            childNode = null;
            parentNode = this;

            foreach (var key in keysList)
            {
                if (childNode != null)
                {
                    if (childNode.IsInternalNode)
                    {
                        parentNode = (ValueNodeInternal<TK>)childNode;
                    }
                    else
                    {
                        parentNode = null;
                        childNode = null;
                        return false;
                    }
                }

                if (parentNode._chidNodesDictionary.TryGetValue(key, out childNode))
                    continue;

                parentNode = null;
                return false;
            }

            if (childNode != null) 
                return true;

            parentNode = null;
            return false;
        }


        public int ChildNodesCount { get { return _chidNodesDictionary.Count; } }

        public ValueNode<TK> this[TK key]
        {
            get
            {
                return _chidNodesDictionary[key];
            }
            set
            {
                if (ReferenceEquals(value, null))
                    throw new InvalidOperationException();

                _chidNodesDictionary[key] = value.DuplicateStructure(this, key);
            }
        }

        public ValueNode<TK> this[IEnumerable<TK> keysList]
        {
            get
            {
                ValueNodeInternal<TK> parentNode;
                ValueNode<TK> childNode;

                if (TrySeparateKeysList(keysList, out parentNode, out childNode))
                    return childNode;
                
                throw new KeyNotFoundException();
            }
            set
            {
                if (ReferenceEquals(value, null))
                    throw new InvalidOperationException();

                ValueNodeInternal<TK> parentNode;
                ValueNode<TK> childNode;

                if (TrySeparateKeysList(keysList, out parentNode, out childNode))
                    parentNode._chidNodesDictionary[childNode.Key] = value.DuplicateStructure(parentNode, childNode.Key);
                else
                    throw new KeyNotFoundException();
            }
        }


        public bool ContainsKey(TK key)
        {
            return _chidNodesDictionary.ContainsKey(key);
        }

        public bool ContainsKey(IEnumerable<TK> keysList)
        {
            ValueNodeInternal<TK> parentNode;
            ValueNode<TK> childNode;

            return TrySeparateKeysList(keysList, out parentNode, out childNode);
        }

        public bool ContainsLeafNodeWithKey(TK key)
        {
            ValueNode<TK> node;

            return _chidNodesDictionary.TryGetValue(key, out node) && node.IsLeafNode;
        }

        public bool ContainsLeafNodeWithKey(IEnumerable<TK> keysList)
        {
            ValueNodeInternal<TK> parentNode;
            ValueNode<TK> childNode;

            return TrySeparateKeysList(keysList, out parentNode, out childNode) && childNode.IsLeafNode;
        }

        public bool ContainsInternalNodeWithKey(TK key)
        {
            ValueNode<TK> node;

            return _chidNodesDictionary.TryGetValue(key, out node) && node.IsInternalNode;
        }

        public bool ContainsInternalNodeWithKey(IEnumerable<TK> keysList)
        {
            ValueNodeInternal<TK> parentNode;
            ValueNode<TK> childNode;

            return TrySeparateKeysList(keysList, out parentNode, out childNode) && childNode.IsInternalNode;
        }


        public IEnumerable<ValueNode<TK>> LeafNodes()
        {
            var nodesStack = new Stack<ValueNodeInternal<TK>>();

            nodesStack.Push(this);

            while (nodesStack.Count > 0)
            {
                var node = nodesStack.Pop();

                foreach (var pair in node._chidNodesDictionary)
                    if (pair.Value.IsLeafNode)
                        yield return pair.Value;
                    else
                        nodesStack.Push((ValueNodeInternal<TK>)pair.Value);
            }
        }

        public IEnumerable<ValueNodeLeaf<TK, TV>> LeafNodes<TV>()
        {
            return LeafNodes().OfType<ValueNodeLeaf<TK, TV>>();
        }

        public IEnumerable<ValueNodeInternal<TK>> InternalNodes()
        {
            var nodesStack = new Stack<ValueNodeInternal<TK>>();

            nodesStack.Push(this);

            while (nodesStack.Count > 0)
            {
                var node = nodesStack.Pop();

                yield return node;

                foreach (var pair in node._chidNodesDictionary.Where(pair => pair.Value.IsInternalNode))
                    nodesStack.Push((ValueNodeInternal<TK>)pair.Value);
            }
        }

        public IEnumerable<ValueNode<TK>> Nodes()
        {
            var nodesStack = new Stack<ValueNodeInternal<TK>>();

            nodesStack.Push(this);

            while (nodesStack.Count > 0)
            {
                var node = nodesStack.Pop();

                yield return node;

                foreach (var pair in node._chidNodesDictionary)
                    if (pair.Value.IsInternalNode)
                        nodesStack.Push((ValueNodeInternal<TK>)pair.Value);
                    else
                        yield return pair.Value;
            }
        }

        public IEnumerable<ValueNode<TK>> DirectLeafNodes()
        {
            return 
                _chidNodesDictionary
                .Where(pair => pair.Value.IsLeafNode)
                .Select(pair => pair.Value);
        }

        public IEnumerable<ValueNodeLeaf<TK, TV>> DirectLeafNodes<TV>()
        {
            return
                _chidNodesDictionary
                .Where(pair => pair.Value is ValueNodeLeaf<TK, TV>)
                .Select(pair => (ValueNodeLeaf<TK, TV>) pair.Value);
        }

        public IEnumerable<ValueNodeInternal<TK>> DirectInternalNodes()
        {
            return
                _chidNodesDictionary
                .Where(pair => pair.Value.IsInternalNode)
                .Select(pair => (ValueNodeInternal<TK>) pair.Value);
        }

        public IEnumerable<ValueNode<TK>> DirectNodes()
        {
            return _chidNodesDictionary.Select(pair => pair.Value);
        }


        public TV GetValue<TV>(TK key)
        {
            var childNode = _chidNodesDictionary[key];

            var leaf = childNode as ValueNodeLeaf<TK, TV>;

            if (leaf != null)
                return leaf.Value;

            throw new InvalidOperationException();
        }

        public TV GetValue<TV>(IEnumerable<TK> keysList)
        {
            ValueNodeInternal<TK> parentNode;
            ValueNode<TK> childNode;

            if (!TrySeparateKeysList(keysList, out parentNode, out childNode)) 
                throw new KeyNotFoundException();

            var leaf = childNode as ValueNodeLeaf<TK, TV>;

            if (leaf != null)
                return leaf.Value;

            throw new InvalidOperationException();
        }

        public ValueNode<TK> GetLeafNode(TK key)
        {
            var childNode = _chidNodesDictionary[key];

            if (childNode.IsLeafNode)
                return childNode;

            throw new InvalidOperationException();
        }

        public ValueNode<TK> GetLeafNode(IEnumerable<TK> keysList)
        {
            var childNode = this[keysList];

            if (childNode.IsLeafNode)
                return childNode;

            throw new InvalidOperationException();
        }

        public ValueNodeLeaf<TK, TV> GetLeafNode<TV>(TK key)
        {
            var childNode = _chidNodesDictionary[key];

            var leaf = childNode as ValueNodeLeaf<TK, TV>;

            if (leaf != null)
                return leaf;

            throw new InvalidOperationException();
        }

        public ValueNodeLeaf<TK, TV> GetLeafNode<TV>(IEnumerable<TK> keysList)
        {
            var childNode = this[keysList];

            var leaf = childNode as ValueNodeLeaf<TK, TV>;

            if (leaf != null)
                return leaf;

            throw new InvalidOperationException();
        }

        public ValueNodeInternal<TK> GetInternalNode(TK key)
        {
            var childNode = _chidNodesDictionary[key];

            if (childNode.IsInternalNode)
                return ((ValueNodeInternal<TK>)childNode);

            throw new InvalidOperationException();
        }

        public ValueNodeInternal<TK> GetInternalNode(IEnumerable<TK> keysList)
        {
            var childNode = this[keysList];

            if (childNode.IsInternalNode)
                return ((ValueNodeInternal<TK>)childNode);

            throw new InvalidOperationException();
        }

        public ValueNode<TK> GetNode(TK key)
        {
            return _chidNodesDictionary[key];
        }

        public ValueNode<TK> GetNode(IEnumerable<TK> keysList)
        {
            return this[keysList];
        }


        public void SetValue<TV>(TK key, TV value)
        {
            _chidNodesDictionary[key] = new ValueNodeLeaf<TK, TV>(key, value);
        }

        public void SetValue<TV>(IEnumerable<TK> keysList, TV value)
        {
            ValueNodeInternal<TK> parentNode;
            ValueNode<TK> childNode;

            if (TrySeparateKeysList(keysList, out parentNode, out childNode))
                parentNode._chidNodesDictionary[childNode.Key] = new ValueNodeLeaf<TK, TV>(childNode.Key, value);
            else
                throw new KeyNotFoundException();
        }


        public bool TryGetValue<TV>(TK key, out TV value)
        {
            ValueNode<TK> childNode;

            if (_chidNodesDictionary.TryGetValue(key, out childNode))
            {
                var leaf = childNode as ValueNodeLeaf<TK, TV>;

                if (leaf != null)
                {
                    value = leaf.Value;
                    return true;
                }
            }

            value = default(TV);
            return false;
        }

        public bool TryGetValue<TV>(IEnumerable<TK> keysList, out TV value)
        {
            ValueNodeInternal<TK> parentNode;
            ValueNode<TK> childNode;

            if (TrySeparateKeysList(keysList, out parentNode, out childNode))
            {
                var leaf = childNode as ValueNodeLeaf<TK, TV>;

                if (leaf != null)
                {
                    value = leaf.Value;
                    return true;
                }
            }

            value = default(TV);
            return false;
        }

        public bool TryGetLeafNode(TK key, out ValueNode<TK> node)
        {
            if (_chidNodesDictionary.TryGetValue(key, out node) && node.IsLeafNode)
                return true;

            node = null;
            return false;
        }

        public bool TryGetLeafNode(IEnumerable<TK> keysList, out ValueNode<TK> node)
        {
            ValueNodeInternal<TK> parentNode;

            if (TrySeparateKeysList(keysList, out parentNode, out node) && node.IsLeafNode)
                return true;

            node = null;
            return false;
        }

        public bool TryGetLeafNode<TV>(TK key, out ValueNodeLeaf<TK, TV> node)
        {
            ValueNode<TK> childNode;

            if (_chidNodesDictionary.TryGetValue(key, out childNode))
            {
                var leaf = childNode as ValueNodeLeaf<TK, TV>;

                if (leaf != null)
                {
                    node = leaf;
                    return true;
                }
            }

            node = null;
            return false;
        }

        public bool TryGetLeafNode<TV>(IEnumerable<TK> keysList, out ValueNodeLeaf<TK, TV> node)
        {
            ValueNodeInternal<TK> parentNode;
            ValueNode<TK> childNode;

            if (TrySeparateKeysList(keysList, out parentNode, out childNode))
            {
                var leaf = childNode as ValueNodeLeaf<TK, TV>;

                if (leaf != null)
                {
                    node = leaf;
                    return true;
                }
            }

            node = null;
            return false;
        }

        public bool TryGetInternalNode(TK key, out ValueNodeInternal<TK> node)
        {
            ValueNode<TK> childNode;

            if (_chidNodesDictionary.TryGetValue(key, out childNode))
            {
                if (childNode.IsInternalNode)
                {
                    node = (ValueNodeInternal<TK>)childNode;
                    return true;
                }
            }

            node = null;
            return false;
        }

        public bool TryGetInternalNode(IEnumerable<TK> keysList, out ValueNodeInternal<TK> node)
        {
            ValueNodeInternal<TK> parentNode;
            ValueNode<TK> childNode;

            if (TrySeparateKeysList(keysList, out parentNode, out childNode))
            {
                if (childNode.IsInternalNode)
                {
                    node = (ValueNodeInternal<TK>)childNode;
                    return true;
                }
            }

            node = null;
            return false;
        }

        public bool TryGetNode(TK key, out ValueNode<TK> node)
        {
            return _chidNodesDictionary.TryGetValue(key, out node);
        }

        public bool TryGetNode(IEnumerable<TK> keysList, out ValueNode<TK> node)
        {
            ValueNodeInternal<TK> parentNode;

            return TrySeparateKeysList(keysList, out parentNode, out node);
        }


        public ValueNodeLeaf<TK, TV> AddValue<TV>(TK key, TV value)
        {
            var newNode = new ValueNodeLeaf<TK, TV>(key, value);

            _chidNodesDictionary.Add(key, newNode);

            return newNode;
        }

        public ValueNodeLeaf<TK, TV> AddValue<TV>(IEnumerable<TK> keysList, TV value)
        {
            var keys = new List<TK>(keysList);
            
            switch (keys.Count)
            {
                case 0:
                    throw new InvalidOperationException();

                case 1:
                    return AddValue(keys[0], value);
            }

            var curNode = AddInternalNode(keys.Take(keys.Count - 1));

            return curNode.AddValue(keys[keys.Count - 1], value);
        }

        public ValueNodeInternal<TK> AddInternalNode(TK key)
        {
            var addedNode = new ValueNodeInternal<TK>(key);

            _chidNodesDictionary.Add(key, addedNode);

            return addedNode;
        }

        public ValueNodeInternal<TK> AddInternalNode(IEnumerable<TK> keysList)
        {
            var curNode = this;
            var testExistsFlag = true;

            foreach (var key in keysList)
            {
                if (testExistsFlag) 
                {
                    ValueNode<TK> childNode;

                    if (curNode.TryGetNode(key, out childNode))
                    {
                        if (childNode.IsInternalNode)
                            curNode = (ValueNodeInternal<TK>)childNode;
                        else
                            throw new InvalidOperationException();
                    }
                    else
                        testExistsFlag = false;
                }
                else
                    curNode = curNode.AddInternalNode(key);
            }

            return curNode;
        }

        public ValueNode<TK> AddNode(TK key, ValueNode<TK> node)
        {
            if (ReferenceEquals(node, null))
                throw new InvalidOperationException();

            var addedNode = node.DuplicateStructure(this, key);

            _chidNodesDictionary.Add(key, addedNode);

            return addedNode;
        }

        public ValueNode<TK> AddNode(IEnumerable<TK> keysList, ValueNode<TK> node)
        {
            if (ReferenceEquals(node, null))
                throw new InvalidOperationException();

            var keys = new List<TK>(keysList);

            switch (keys.Count)
            {
                case 0:
                    throw new InvalidOperationException();

                case 1:
                    return AddNode(keys[0], node);
            }

            var curNode = AddInternalNode(keys.Take(keys.Count - 1));

            return curNode.AddNode(keys[keys.Count - 1], node);
        }


        public ValueNodeLeaf<TK, TV> AddOrSetValue<TV>(TK key, TV value)
        {
            var newNode = new ValueNodeLeaf<TK, TV>(key, value);

            if (_chidNodesDictionary.ContainsKey(key))
                _chidNodesDictionary[key] = newNode;

            else
                _chidNodesDictionary.Add(key, newNode);

            return newNode;
        }

        public ValueNodeLeaf<TK, TV> AddOrSetValue<TV>(IEnumerable<TK> keysList, TV value)
        {
            var keys = new List<TK>(keysList);

            switch (keys.Count)
            {
                case 0:
                    throw new InvalidOperationException();

                case 1:
                    return AddValue(keys[0], value);
            }

            var curNode = AddInternalNode(keys.Take(keys.Count - 1));

            return curNode.AddOrSetValue(keys[keys.Count - 1], value);
        }

        public ValueNode<TK> AddOrSetNode(TK key, ValueNode<TK> node)
        {
            if (ReferenceEquals(node, null))
                throw new InvalidOperationException();

            var addedNode = node.DuplicateStructure(this, key);

            if (_chidNodesDictionary.ContainsKey(key))
                _chidNodesDictionary[node.Key] = addedNode;

            else
                _chidNodesDictionary.Add(key, addedNode);

            return addedNode;
        }

        public ValueNode<TK> AddOrSetNode(IEnumerable<TK> keysList, ValueNode<TK> node)
        {
            var keys = new List<TK>(keysList);

            switch (keys.Count)
            {
                case 0:
                    throw new InvalidOperationException();

                case 1:
                    return AddOrSetNode(keys[0], node);
            }

            var curNode = AddInternalNode(keys.Take(keys.Count - 1));

            return curNode.AddOrSetNode(keys[keys.Count - 1], node);
        }


        public bool Remove(IEnumerable<TK> keysList)
        {
            ValueNodeInternal<TK> parentNode;
            ValueNode<TK> childNode;

            return 
                TrySeparateKeysList(keysList, out parentNode, out childNode) 
                && parentNode._chidNodesDictionary.Remove(childNode.Key);
        }

        public bool RemoveLeafNode(IEnumerable<TK> keysList)
        {
            ValueNodeInternal<TK> parentNode;
            ValueNode<TK> childNode;

            if (TrySeparateKeysList(keysList, out parentNode, out childNode) && childNode.IsLeafNode)
                return parentNode._chidNodesDictionary.Remove(childNode.Key);

            return false;
        }

        public bool RemoveInternalNode(IEnumerable<TK> keysList)
        {
            ValueNodeInternal<TK> parentNode;
            ValueNode<TK> childNode;

            if (TrySeparateKeysList(keysList, out parentNode, out childNode) && childNode.IsInternalNode)
                return parentNode._chidNodesDictionary.Remove(childNode.Key);

            return false;
        }

        public bool Remove(TK key)
        {
            return _chidNodesDictionary.Remove(key);
        }

        public bool RemoveLeafNode(TK key)
        {
            ValueNode<TK> childNode;

            if (_chidNodesDictionary.TryGetValue(key, out childNode) && childNode.IsLeafNode)
                return _chidNodesDictionary.Remove(childNode.Key);

            return false;
        }

        public bool RemoveInternalNode(TK key)
        {
            ValueNode<TK> childNode;

            if (_chidNodesDictionary.TryGetValue(key, out childNode) && childNode.IsInternalNode)
                return _chidNodesDictionary.Remove(childNode.Key);

            return false;
        }


        public override bool IsLeafNode
        {
            get { return false; }
        }

        public override bool IsInternalNode
        {
            get { return true; }
        }

        internal override ValueNode<TK> DuplicateStructure(ValueNodeInternal<TK> parentNode, TK key)
        {
            if (HasParent == false)
            {
                Key = key;
                ParentNode = parentNode;

                return this;
            }

            var result = new ValueNodeInternal<TK>(parentNode, key);

            var srcStack = new Stack<ValueNodeInternal<TK>>();
            var dstStack = new Stack<ValueNodeInternal<TK>>();

            srcStack.Push(this);
            dstStack.Push(result);

            while (srcStack.Count > 0)
            {
                var srcNode = srcStack.Pop();
                var dstNode = dstStack.Pop();

                foreach (var pair in srcNode._chidNodesDictionary)
                {
                    if (pair.Value.IsLeafNode)
                    {
                        dstNode._chidNodesDictionary.Add(pair.Key, pair.Value);
                    }
                    else
                    {
                        var srcChild = (ValueNodeInternal<TK>)pair.Value;
                        var dstChild = dstNode.AddInternalNode(srcChild.Key);

                        srcStack.Push(srcChild);
                        dstStack.Push(dstChild);
                    }
                }
            }

            return result;
        }

        public override void ToTextTree(LinearComposer log)
        {
            log.AppendLine("{");

            log.IncreaseIndentation();

            var flag = false;
            foreach (var pair in _chidNodesDictionary)
            {
                if (flag)
                    log.Append(", ");
                else
                    flag = true;

                log.Append(Key + " : ");
                pair.Value.ToTextTree(log);
            }

            log.DecreaseIndentation();

            log.AppendAtNewLine("}");
        }

        public override XElement ToXElement(TK parentKey)
        {
            var childList = 
                _chidNodesDictionary
                .Select(pair => pair.Value.ToXElement(pair.Key))
                .ToList();

            var parentKeyText = 
                ReferenceEquals(parentKey, null) 
                ? "null" 
                : parentKey.ToString();

            return 
                new XElement(
                    parentKeyText, 
                    new XAttribute("Key", Key.ToString()), childList
                    );
        }

        public override void LeafValuesToTextLines(TextStack textStack)
        {
            textStack.Push(Key.ToString());

            foreach (var pair in _chidNodesDictionary)
                pair.Value.LeafValuesToTextLines(textStack);

            textStack.Pop();
        }
    }
}
