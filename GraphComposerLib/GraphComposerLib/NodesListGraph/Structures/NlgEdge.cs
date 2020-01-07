using System;

namespace GraphComposerLib.NodesListGraph.Structures
{
    public sealed class NlgEdge<TNodeData, TEdgeData>
    {
        public NlgNode<TNodeData, TEdgeData> SourceNode { get; }

        public NlgNode<TNodeData, TEdgeData> TargetNode { get; }

        public TEdgeData EdgeData { get; set; }


        internal NlgEdge(NlgNode<TNodeData, TEdgeData> sourceNode, NlgNode<TNodeData, TEdgeData> targetNode)
        {
            SourceNode = sourceNode 
                         ?? throw new ArgumentNullException(nameof(sourceNode));

            TargetNode = targetNode 
                         ?? throw new ArgumentNullException(nameof(targetNode));
        }
    }
}
