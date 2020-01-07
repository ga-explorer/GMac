using System;
using System.Collections;
using System.Collections.Generic;
using GraphComposerLib.NodesListGraph.Structures;

namespace GraphComposerLib.NodesListGraph.Algorithms
{
    public class NlgTraversal<TNodeData, TEdgeData>
    {
        public NlgGraph<TNodeData, TEdgeData> Graph { get; set; }

        public List<NlgNode<TNodeData, TEdgeData>> StartNodes { get; }
            = new List<NlgNode<TNodeData, TEdgeData>>();

        public Action<NlgNode<TNodeData, TEdgeData>> NodeAction { get; set; }

        
        public void BreadthFirstSearch()
        {
            if (ReferenceEquals(Graph, null))
                return;

            if (StartNodes.Count == 0)
                return;

            if (ReferenceEquals(NodeAction, null))
                return;

            var nodeSelectedArray = new BitArray(Graph.Count, false);

            Graph.UpdateNodeIDs();

            var nodesQueue = new Queue<NlgNode<TNodeData, TEdgeData>>();
            foreach (var node in StartNodes)
            {
                nodesQueue.Enqueue(node);
                nodeSelectedArray.Set(node.NodeID, true);
            }

            while (nodesQueue.Count > 0)
            {
                var node = nodesQueue.Dequeue();
                NodeAction(node);

                foreach (var edge in node)
                {
                    var targetNode = edge.TargetNode;

                    if (!nodeSelectedArray.Get(targetNode.NodeID))
                    {
                        nodesQueue.Enqueue(targetNode);
                        nodeSelectedArray.Set(targetNode.NodeID, true);
                    }
                }
            }
        }

        public void DepthFirstSearch()
        {
            if (ReferenceEquals(Graph, null))
                return;

            if (StartNodes.Count == 0)
                return;

            if (ReferenceEquals(NodeAction, null))
                return;

            var nodeSelectedArray = new BitArray(Graph.Count, false);

            Graph.UpdateNodeIDs();

            var nodesStack = new Stack<NlgNode<TNodeData, TEdgeData>>();
            foreach (var node in StartNodes)
            {
                nodesStack.Push(node);
                nodeSelectedArray.Set(node.NodeID, true);
            }

            while (nodesStack.Count > 0)
            {
                var node = nodesStack.Pop();
                NodeAction(node);

                foreach (var edge in node)
                {
                    var targetNode = edge.TargetNode;

                    if (!nodeSelectedArray.Get(targetNode.NodeID))
                    {
                        nodesStack.Push(targetNode);
                        nodeSelectedArray.Set(targetNode.NodeID, true);
                    }
                }
            }
        }
    }
}
