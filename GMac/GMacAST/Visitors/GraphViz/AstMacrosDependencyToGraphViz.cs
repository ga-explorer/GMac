using System.Linq;
using GMac.GMacAST.Dependency;
using TextComposerLib.Diagrams.GraphViz.Dot;
using TextComposerLib.Diagrams.GraphViz.Dot.Value;

namespace GMac.GMacAST.Visitors.GraphViz
{
    public sealed class AstMacrosDependencyToGraphViz : GraphVizConverter 
    {
        internal void ToGraphViz(AstMacroDependencyGraph dep)
        {
            Graph.SetRankDir(DotRankDirection.LeftToRight);

            foreach (var astTypeDep in dep.Items)
            {
                var node =
                    Graph
                    .AddNode(astTypeDep.BaseItem.AccessName)
                    .SetLabel(
                        Graph.Table(
                            "Macro",
                            astTypeDep.BaseItem.AccessName
                            )
                        );

                if (astTypeDep.UsedCount > 0)
                    node
                    .AddEdgesFrom(
                        astTypeDep.UsedItems.Select(t => t.AccessName)
                        );
            }
        }
    }
}
