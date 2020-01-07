using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphComposerLib.NodesListGraph.Structures
{
    public class NlgGraph<TNodeData, TEdgeData> 
        : IList<NlgNode<TNodeData, TEdgeData>>
    {
        private bool _validNodeIDs = true;

        private readonly List<NlgNode<TNodeData, TEdgeData>> _nodesList =
            new List<NlgNode<TNodeData, TEdgeData>>();


        public int Count 
            => _nodesList.Count;

        public bool IsReadOnly 
            => false;

        public NlgNode<TNodeData, TEdgeData> this[int index]
        {
            get => _nodesList[index];
            set
            {
                _nodesList[index] = value
                                    ?? throw new ArgumentNullException(nameof(value));
                value.NodeID = index;
            }
        }


        public NlgGraph<TNodeData, TEdgeData> UpdateNodeIDs()
        {
            if (_validNodeIDs)
                return this;

            for (var index = 0; index < _nodesList.Count; index++)
                _nodesList[index].NodeID = index;

            _validNodeIDs = true;

            return this;
        }

        public void Add(NlgNode<TNodeData, TEdgeData> item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException(nameof(item));

            _nodesList.Add(item);
            _validNodeIDs = false;
        }

        public void Clear()
        {
            _nodesList.Clear();
            _validNodeIDs = true;
        }

        public bool Contains(NlgNode<TNodeData, TEdgeData> item)
        {
            return _nodesList.Contains(item);
        }

        public void CopyTo(NlgNode<TNodeData, TEdgeData>[] array, int arrayIndex)
        {
            _nodesList.CopyTo(array, arrayIndex);
        }

        public bool Remove(NlgNode<TNodeData, TEdgeData> item)
        {
            var result = _nodesList.Remove(item);

            if (result)
                _validNodeIDs = false;

            return result;
        }

        public int IndexOf(NlgNode<TNodeData, TEdgeData> item)
        {
            return _nodesList.IndexOf(item);
        }

        public void Insert(int index, NlgNode<TNodeData, TEdgeData> item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException(nameof(item));

            _nodesList.Insert(index, item);
            _validNodeIDs = false;
        }

        public void RemoveAt(int index)
        {
            _nodesList.RemoveAt(index);
            _validNodeIDs = false;
        }

        public IEnumerable<NlgEdge<TNodeData, TEdgeData>> GetEdges()
        {
            return _nodesList.SelectMany(node => node);
        }

        public NlgNode<TNodeData, TEdgeData> AddNewNode()
        {
            var node = new NlgNode<TNodeData, TEdgeData>();

            _nodesList.Add(node);
            _validNodeIDs = false;

            return node;
        }

        public NlgNode<TNodeData, TEdgeData> InsertNewNode(int index)
        {
            var node = new NlgNode<TNodeData, TEdgeData>();

            _nodesList.Insert(index, node);
            _validNodeIDs = false;

            return node;
        }

        public IEnumerator<NlgNode<TNodeData, TEdgeData>> GetEnumerator()
        {
            return _nodesList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _nodesList.GetEnumerator();
        }
    }
}
