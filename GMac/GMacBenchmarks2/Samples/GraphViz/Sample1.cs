using GMacBenchmarks2.Samples.Computations;
using System;
using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.GraphViz.Dot.Value;

namespace GMacBenchmarks2.Samples.GraphViz
{
    public sealed class Sample1 : IGMacSample
    {
        public string Title 
            => "Draw Prefix\\Suffix tree of a multivector";

        public string Description 
            => "Draw Prefix\\Suffix tree of a multivector";


        private string IdToNodeName(int vSpaceDim, int id)
        {
            return Convert.ToString(id, 2).PadLeft(vSpaceDim, '0');
        }

        public string Execute()
        {
            var graph = DotGraph.Undirected();
            graph.SetRankDir(DotRankDirection.LeftToRight);

            var nodeDefaults = graph.AddNodeDefaults();
            nodeDefaults
                .SetShape(DotNodeShape.Box)
                .SetStyle(DotNodeStyle.Rounded, DotNodeStyle.Bold);

            var vSpaceDim = 6;
            var gaSpaceDim = 1 << vSpaceDim;
            var drawPrefixTree = true;
            var drawSuffixTree = false;

            for (var id = 0; id < gaSpaceDim; id++)
            {
                var nodeName = IdToNodeName(vSpaceDim, id);

                graph.AddNode(nodeName);
            }

            for (var basisVectorIndex = 0; basisVectorIndex < vSpaceDim; basisVectorIndex++)
            {
                var basisVectorId = 1 << basisVectorIndex;

                graph.AddEdge(
                    IdToNodeName(vSpaceDim, 0),
                    IdToNodeName(vSpaceDim, basisVectorId)
                );

                if (drawPrefixTree && basisVectorIndex > 0)
                {
                    var basisBladesCount = basisVectorId;
                    for (var i = 1; i < basisBladesCount; i++)
                    {
                        var id1 = i;
                        var id2 = id1 | basisVectorId;

                        graph.AddEdge(
                            IdToNodeName(vSpaceDim, id1),
                            IdToNodeName(vSpaceDim, id2)
                        );
                    }
                }

                if (drawSuffixTree && basisVectorIndex < vSpaceDim - 1)
                {
                    var basisBladesCount = 1 << (vSpaceDim - basisVectorIndex - 1);
                    for (var i = 1; i < basisBladesCount; i++)
                    {
                        var id1 = i << (basisVectorIndex + 1);
                        var id2 = id1 | basisVectorId;

                        graph.AddEdge(
                            IdToNodeName(vSpaceDim, id1),
                            IdToNodeName(vSpaceDim, id2)
                        );
                    }
                }
            }
            
            return graph.GenerateDotCode();
        }
    }
}
