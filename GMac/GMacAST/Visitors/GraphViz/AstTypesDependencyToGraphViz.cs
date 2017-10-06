using System;
using System.Linq;
using GMac.GMacAST.Dependency;
using TextComposerLib.Diagrams.GraphViz.Dot;
using TextComposerLib.Diagrams.GraphViz.Dot.Value;
using TextComposerLib.Text;

namespace GMac.GMacAST.Visitors.GraphViz
{
    public sealed class AstTypesDependencyToGraphViz : GraphVizConverter 
    {
        private DotNode AddNode(AstType astType)
        {
            if (astType.IsValidStructureType == false)
                return
                    Graph
                    .AddNode(astType.GMacTypeSignature)
                    .SetLabel(
                        Graph.Table(
                            Graph.SelectIconName(astType),
                            astType.GMacTypeSignature
                            )
                        );

            var astStruct = astType.ToStructure;

            var dict =
                astStruct
                .GroupDataMembersByType()
                .ToDictionary(
                    p => p.Key.GMacTypeSignature,
                    p => p.Value.Select(m => m.Name).Concatenate(Environment.NewLine)
                    );

            return
                Graph
                .AddNode(astStruct.GMacTypeSignature)
                .SetLabel(
                    Graph.Table(
                        "Structure",
                        astStruct.GMacTypeSignature,
                        Graph.SimpleTable(dict)
                        )
                    );
        }

        private DotEdge AddEdge(AstType usedType, AstType userType)
        {
            if (userType.IsValidStructureType)
                return Graph.AddEdge(
                    usedType.GMacTypeSignature.ToNodeRef(DotCompass.Center),
                    userType.GMacTypeSignature.ToNodeRef(usedType.GMacTypeSignature, DotCompass.West)
                    );

            return Graph.AddEdge(
                usedType.GMacTypeSignature, userType.GMacTypeSignature
                );
        }


        public void ToGraphViz(AstTypeDependencyGraph dep)
        {
            Graph.SetRankDir(DotRankDirection.LeftToRight);

            foreach (var astTypeDep in dep.Items)
            {
                var node = AddNode(astTypeDep.BaseItem);

                if (astTypeDep.BaseItem.IsValidStructureType)
                {
                    var usedTypes = astTypeDep.UsedItems;

                    foreach (var usedType in usedTypes)
                        AddEdge(usedType, astTypeDep.BaseItem);
                }
                else
                {
                    if (astTypeDep.UsedCount > 0)
                        node
                        .AddEdgesFrom(
                            astTypeDep.UsedItems.Select(t => t.GMacTypeSignature)
                            );
                }
            }
        }
    }
}
