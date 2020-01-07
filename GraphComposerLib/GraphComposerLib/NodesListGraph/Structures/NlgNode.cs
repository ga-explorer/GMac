using System.Collections;
using System.Collections.Generic;

namespace GraphComposerLib.NodesListGraph.Structures
{
    public sealed class NlgNode<TNodeData, TEdgeData> 
        : IReadOnlyList<NlgEdge<TNodeData, TEdgeData>>
    {
        private readonly List<NlgEdge<TNodeData, TEdgeData>> _edgesList =
            new List<NlgEdge<TNodeData, TEdgeData>>();


        public int NodeID { get; internal set; }

        public TNodeData NodeData { get; set; }

        public int Count 
            => _edgesList.Count;

        public NlgEdge<TNodeData, TEdgeData> this[int index] 
            => _edgesList[index];


        public NlgNode()
        {
        }


        public NlgEdge<TNodeData, TEdgeData> AddEdge(NlgNode<TNodeData, TEdgeData> targetNode)
        {
            var edge =
                new NlgEdge<TNodeData, TEdgeData>(
                    this, 
                    targetNode
                );

            _edgesList.Add(edge);

            return edge;
        }


        public IEnumerator<NlgEdge<TNodeData, TEdgeData>> GetEnumerator()
        {
            return _edgesList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _edgesList.GetEnumerator();
        }
    }
}
