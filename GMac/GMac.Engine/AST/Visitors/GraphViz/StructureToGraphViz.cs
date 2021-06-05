using System.Linq;
using CodeComposerLib.GraphViz.Dot;
using GMac.Engine.AST.Symbols;

namespace GMac.Engine.AST.Visitors.GraphViz
{
    public sealed class StructureToGraphViz : AstToGraphVizConverter
    {
        private void AddMultivectorCoefs(string parentNodeName, AstFrameMultivector mvClass)
        {
            Graph
            .AddNode(Graph.NewNodeName())
            .SetLabel(
                Graph.Table(
                    "Scalar",
                    Graph.SimpleTable("scalar", "Coefficients"),
                    mvClass
                        .ParentFrame
                        .BasisBladesSortedByGrade()
                        .Select(item => item.CoefName)
                    )
                )
            .AddEdgeFrom(parentNodeName);
        }

        private void AddDataMembers(string parentNodeName, AstStructure structure)
        {
            var dict = structure.GroupDataMembersByType();

            foreach (var pair in dict)
            {
                var nodeName = Graph.NewNodeName();

                Graph
                .AddNode(nodeName)
                .SetLabel(
                    Graph.Table(
                        Graph.SelectIconName(pair.Key),
                        Graph.SimpleTable(pair.Key.GMacTypeSignature, "Structure Members"),
                        pair.Value.Select(item => item.Name)
                        )
                    )
                .AddEdgeFrom(parentNodeName);

                if (pair.Key.IsValidStructureType)
                    AddDataMembers(nodeName, pair.Key.ToStructure);

                else if (pair.Key.IsValidMultivectorType)
                    AddMultivectorCoefs(nodeName, pair.Key.ToFrameMultivector);
            }
        }


        public void Visit(AstStructure structure)
        {
            Graph
                .AddNode(structure.AccessName)
                .SetLabel(
                    Graph.Table(
                        "Structure",
                        structure.AccessName
                        )
                    );

            AddDataMembers(structure.AccessName, structure);
        }
    }
}
