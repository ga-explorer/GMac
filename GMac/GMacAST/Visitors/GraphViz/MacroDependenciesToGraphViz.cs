using System.Collections.Generic;
using System.Drawing;
using GMac.GMacAST.Dependency;
using GMac.GMacAST.Symbols;
using TextComposerLib.Diagrams.GraphViz.Dot;
using TextComposerLib.Diagrams.GraphViz.Dot.Value;
using UtilLib.DataStructures;

namespace GMac.GMacAST.Visitors.GraphViz
{
    public sealed class MacroDependenciesToGraphViz : GraphVizConverter
    {
        private readonly Dictionary<string, string> _dict = new ADictionary<string, string>();

        private AstMacroDependencyGraph _dependencyGraph;


        private void AddCallerMacros(AstMacro macro)
        {
            var macroDep = _dependencyGraph[macro.AccessName];

            if (macroDep.UserCount == 0)
                return;

            foreach (var callerMacro in macroDep.UserItems)
            {
                if (_dict.ContainsKey(callerMacro.AccessName) == false)
                {
                    _dict.Add(callerMacro.AccessName, callerMacro.AccessName);

                    Graph
                    .AddNode(callerMacro.AccessName)
                    .SetFillColor(Color.Wheat.ToDotRgbColor())
                    .SetLabel(
                        Graph.Table(
                            "Macro",
                            callerMacro.AccessName
                            )
                        );
                }

                Graph.AddEdge(macro.AccessName, callerMacro.AccessName);

                AddCallerMacros(callerMacro);
            }
        }

        private void AddCalledMacros(AstMacro macro)
        {
            var macroDep = _dependencyGraph[macro.AccessName];

            if (macroDep.UsedCount == 0)
                return;

            foreach (var calledMacro in macroDep.UsedItems)
            {
                if (_dict.ContainsKey(calledMacro.AccessName) == false)
                {
                    _dict.Add(calledMacro.AccessName, calledMacro.AccessName);

                    Graph
                    .AddNode(calledMacro.AccessName)
                    .SetFillColor(Color.White.ToDotRgbColor())
                    .SetLabel(
                        Graph.Table(
                            "Macro",
                            calledMacro.AccessName
                            )
                        );
                }

                Graph.AddEdge(calledMacro.AccessName, macro.AccessName);

                AddCalledMacros(calledMacro);
            }
        }


        public void Visit(AstMacro macro)
        {
            _dependencyGraph = macro.Root.GetMacroDependencies();

            Graph.SetRankDir(DotRankDirection.LeftToRight);

            Graph
            .AddNode(macro.AccessName)
            .SetFillColor(Color.GreenYellow.ToDotRgbColor())
            .SetLabel(
                Graph.Table(
                    "Macro",
                    macro.AccessName
                    )
                );

            _dict.Clear();

            AddCallerMacros(macro);

            _dict.Clear();

            AddCalledMacros(macro);
        }

    }
}
