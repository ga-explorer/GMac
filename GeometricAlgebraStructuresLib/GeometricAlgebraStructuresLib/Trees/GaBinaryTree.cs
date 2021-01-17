using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeometricAlgebraStructuresLib.Trees
{
    /// <summary>
    /// This class contains an internal read-only list of type T and a binary tree
    /// index with fixed number of levels.  
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GaBinaryTree<T> : IReadOnlyList<T>
    {
        private readonly List<Tuple<int, int>> _internalNodesList;

        private readonly IReadOnlyList<int> _leafNodeIDsList;

        private T[] _leafNodeValuesArray;


        public int TreeDepth { get; }

        public T DefaultValue { get; set; }
            = default;

        public int Count
            => _leafNodeIDsList.Count;

        public T this[int id] 
        { 
            get =>
                TryGetLeafNodeIndex(id, out var index)
                    ? _leafNodeValuesArray[index]
                    : DefaultValue;
            set
            {
                if (TryGetLeafNodeIndex(id, out var index))
                    _leafNodeValuesArray[index] = value;

                throw new IndexOutOfRangeException();
            }
        }

        public int RootNodeIndex
            => 0;

        public Tuple<int, int> RootNode 
            => _internalNodesList[0];


        public GaBinaryTree(int treeDepth, IReadOnlyList<int> leafNodeIDsList)
        {
            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodeIDsList;
            _leafNodeValuesArray = new T[_leafNodeIDsList.Count];
            _internalNodesList = new List<Tuple<int, int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }

        public GaBinaryTree(int treeDepth, IReadOnlyDictionary<int, T> leafNodes)
        {
            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodes.Keys.ToArray();
            _leafNodeValuesArray = leafNodes.Values.ToArray();
            _internalNodesList = new List<Tuple<int, int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }

        public GaBinaryTree(int treeDepth, IDictionary<int, T> leafNodes)
        {
            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodes.Keys.ToArray();
            _leafNodeValuesArray = leafNodes.Values.ToArray();
            _internalNodesList = new List<Tuple<int, int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }

        public GaBinaryTree(int treeDepth, IReadOnlyList<int> leafNodeIDsList, IReadOnlyCollection<T> leafNodeValuesList)
        {
            if (leafNodeIDsList.Count != leafNodeValuesList.Count)
                throw new InvalidOperationException();

            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodeIDsList;
            _leafNodeValuesArray = leafNodeValuesList.ToArray();
            _internalNodesList = new List<Tuple<int, int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }

        public GaBinaryTree(GaBinaryTree<T> binaryTree)
        {
            TreeDepth = binaryTree.TreeDepth;
            _internalNodesList = binaryTree._internalNodesList;
            _leafNodeIDsList = binaryTree._leafNodeIDsList;
            _leafNodeValuesArray = new T[binaryTree.Count];
            binaryTree._leafNodeValuesArray.CopyTo(_leafNodeValuesArray, 0);
        }


        private void ConstructIndexTree()
        {
            var emptyInternalNode = new Tuple<int, int>(-1, -1);

            _internalNodesList.Clear();
            _internalNodesList.Add(new Tuple<int, int>(-1, -1));

            for (var i = 0; i < Count; i++)
            {
                var id = _leafNodeIDsList[i];
                var bitMask = 1 << (TreeDepth - 1);
                var index = 0;

                while (bitMask != 1)
                {
                    var (childIndex0, childIndex1) = _internalNodesList[index];

                    if ((bitMask & id) == 0)
                    {
                        if (childIndex0 < 0)
                        {
                            var newIndex = _internalNodesList.Count;

                            _internalNodesList[index] = new Tuple<int, int>(newIndex, childIndex1);

                            _internalNodesList.Add(emptyInternalNode);

                            index = newIndex;
                        }
                        else
                            index = childIndex0;
                    }
                    else
                    {
                        if (childIndex1 < 0)
                        {
                            var newIndex = _internalNodesList.Count;

                            _internalNodesList[index] = new Tuple<int, int>(childIndex0, newIndex);

                            _internalNodesList.Add(emptyInternalNode);

                            index = newIndex;
                        }
                        else
                            index = childIndex1;
                    }

                    bitMask >>= 1;
                }

                var (leafIndex0, leafIndex1) = _internalNodesList[index];

                if ((id & 1) == 0)
                {
                    if (leafIndex0 < 0)
                        _internalNodesList[index] = new Tuple<int, int>(i, leafIndex1);
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    if (leafIndex1 < 0)
                        _internalNodesList[index] = new Tuple<int, int>(leafIndex0, i);
                    else
                        throw new InvalidOperationException();
                }
            }
        }


        public void ClearValues()
        {
            _leafNodeValuesArray = new T[Count];
        }

        public Tuple<int, int> GetInternalNodeByIndex(int index)
        {
            return _internalNodesList[index];
        }

        public Tuple<int, T> GetLeafNodeByIndex(int index)
        {
            return new Tuple<int, T>(
                _leafNodeIDsList[index],
                _leafNodeValuesArray[index]
            );
        }

        public int GetLeafNodeIdByIndex(int index)
        {
            return _leafNodeIDsList[index];
        }

        public T GetLeafNodeValueByIndex(int index)
        {
            return _leafNodeValuesArray[index];
        }

        public GaBinaryTree<T> SetLeafNodeValueByIndex(int index, T value)
        {
            _leafNodeValuesArray[index] = value;

            return this;
        }
        
        public bool TryGetLeafNodeIndex(int id, out int index)
        {
            var bitMask = 1 << (TreeDepth - 1);
            index = 0;

            while (bitMask != 1)
            {
                var (childIndex0, childIndex1) = _internalNodesList[index];

                if ((bitMask & id) == 0)
                {
                    if (childIndex0 < 0)
                    {
                        index = -1;
                        return false;
                    }
                    
                    index = childIndex0;
                }
                else
                {
                    if (childIndex1 < 0)
                    {
                        index = -1;
                        return false;
                    }
                    
                    index = childIndex1;
                }

                bitMask >>= 1;
            }

            var (leafIndex0, leafIndex1) = _internalNodesList[index];

            if ((id & 1) == 0)
            {
                if (leafIndex0 < 0)
                {
                    index = -1;
                    return false;
                }

                index = leafIndex0;
            }
            else
            {
                if (leafIndex1 < 0)
                {
                    index = -1;
                    return false;
                }

                index = leafIndex1;
            }

            return true;
        }

        public bool TryGetValue(int id, out T value)
        {
            if (TryGetLeafNodeIndex(id, out var index))
            {
                value = _leafNodeValuesArray[index];
                return true;
            }

            value = DefaultValue;
            return false;
        }

        public bool TryGetLeafNodeIndexValue(int id, out int index, out T value)
        {
            if (TryGetLeafNodeIndex(id, out index))
            {
                value = _leafNodeValuesArray[index];
                return true;
            }

            value = DefaultValue;
            return false;
        }
        
        public bool ContainsId(int id)
        {
            var bitMask = 1 << (TreeDepth - 1);
            var index = 0;

            while (bitMask != 1)
            {
                var (childIndex0, childIndex1) = _internalNodesList[index];

                if ((bitMask & id) == 0)
                {
                    if (childIndex0 < 0)
                        return false;

                    index = childIndex0;
                }
                else
                {
                    if (childIndex1 < 0)
                        return false;

                    index = childIndex1;
                }

                bitMask >>= 1;
            }

            var (leafIndex0, leafIndex1) = _internalNodesList[index];

            if ((id & 1) == 0)
            {
                if (leafIndex0 < 0)
                    return false;
            }
            else
            {
                if (leafIndex1 < 0)
                    return false;
            }

            return true;
        }

        public IEnumerable<int> GetLeafNodeIDs()
        {
            return _leafNodeIDsList;
        }
        
        public IEnumerable<KeyValuePair<int, T>> GetLeafNodes()
        {
            for (var i = 0; i < Count; i++)
                yield return new KeyValuePair<int, T>(
                    _leafNodeIDsList[i], 
                    _leafNodeValuesArray[i]
                );
        }


        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_leafNodeValuesArray).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)_leafNodeValuesArray).GetEnumerator();
        }

        public override string ToString()
        {
            var composer = new StringBuilder();

            var stack = new Stack<Tuple<int, int>>();
            stack.Push(new Tuple<int, int>(TreeDepth, 0));

            while (stack.Count > 0)
            {
                var (treeDepth, index) = stack.Pop();
                var prefixText = "".PadLeft(2 * (TreeDepth - treeDepth));

                if (treeDepth == 0)
                {
                    var leafNodeId = _leafNodeIDsList[index];
                    var leafNodeValue = _leafNodeValuesArray[index];

                    composer
                        .AppendLine($"{prefixText}Leaf <Index = {index}, ID = {leafNodeId}, Value = {leafNodeValue}>");

                    continue;
                }

                composer
                    .AppendLine($"{prefixText}Node <Index = {index}>");

                var (childNodeIndex0, childNodeIndex1) = _internalNodesList[index];

                if (childNodeIndex1 >= 0)
                    stack.Push(
                        new Tuple<int, int>(treeDepth - 1, childNodeIndex1)
                    );

                if (childNodeIndex0 >= 0)
                    stack.Push(
                        new Tuple<int, int>(treeDepth - 1, childNodeIndex0)
                    );
            }

            return composer.ToString();
        }
    }
}
