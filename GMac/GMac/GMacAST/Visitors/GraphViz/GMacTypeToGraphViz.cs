using System;
using System.Linq;
using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.GraphViz.Dot.Value;

namespace GMac.GMacAST.Visitors.GraphViz
{
    public sealed class GMacTypeToGraphViz : AstToGraphVizConverter 
    {
        private string _parentNodeName;

        private string _memberName;


        internal GMacTypeToGraphViz(string rootName = "")
        {
            _memberName = rootName;
        }


        public void Visit(AstType astType)
        {
            if (String.IsNullOrEmpty(_parentNodeName))
            {
                Graph.SetRankDir(DotRankDirection.LeftToRight);
            }

            var node = Graph.AddNode(Graph.NewNodeName());

            if (String.IsNullOrEmpty(_memberName))
                node.SetLabel(
                    Graph.Table(
                        Graph.SelectIconName(astType),
                        astType.GMacTypeSignature
                        )
                    );
            else
                node.SetLabel(
                    Graph.Table(
                        Graph.SelectIconName(astType),
                        astType.GMacTypeSignature,
                        _memberName
                        )
                    );

            if (String.IsNullOrEmpty(_parentNodeName) == false)
            {
                Graph.AddEdge(_parentNodeName, node.NodeName);
            }

            if (astType.IsValidMultivectorType)
            {
                var astMultivector = astType.ToFrameMultivector;

                var scalarType = astMultivector.Root.ScalarType;

                Graph
                .AddNode(Graph.NewNodeName())
                .SetLabel(
                    Graph.Table(
                        Graph.SelectIconName(scalarType),
                        scalarType.GMacTypeSignature,
                        Graph.SimpleTable(
                            astMultivector.ParentFrame.BasisBlades().Select(b => b.CoefName)
                            )
                        )
                    )
                .AddEdgeFrom(node);
            }
            else if (astType.IsValidStructureType)
            {
                var astStructure = astType.ToStructure;

                foreach (var dataMember in astStructure.DataMembers)
                {
                    _parentNodeName = node.NodeName;

                    _memberName = dataMember.Name;

                    Visit(dataMember.GMacType);
                }
            }
        }
    }
}
