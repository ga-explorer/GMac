using System.Drawing;
using System.Linq;
using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.GraphViz.Dot.Value;
using GMac.Engine.API.Binding;
using GMac.Engine.API.CodeBlock;
using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler.Semantic.ASTInterpreter.LowLevel;

namespace GMac.Engine.AST.Visitors.GraphViz
{
    public sealed class MacroBindingToGraphViz : AstToGraphVizConverter
    {
        public GMacMacroBinding MacroBinding { get; }

        public GMacCodeBlock OptimizedCodeBlock { get; }

        public AstMacro Macro => MacroBinding.BaseMacro;

        public DotSubGraph ExprSubGraph { get; private set; }


        public MacroBindingToGraphViz(GMacMacroBinding macroBinding, GMacCodeBlock codeBlock)
        {
            MacroBinding = macroBinding;

            OptimizedCodeBlock = codeBlock;
        }

        //private DotNode AddConstantOutputExpression(TlOutputVariable computedVar)
        //{
        //    return
        //        Graph
        //        .AddNode(computedVar.LowLevelName)
        //        .SetLabel(
        //            Graph.Table(
        //                "Output",
        //                Graph.SimpleTable(
        //                    computedVar.LowLevelName,
        //                    computedVar.AssociatedValueAccess.GetName() + " = " + computedVar.RhsExpr
        //                    )
        //                )
        //            );
        //}

        private DotNode AddConstantTempExpression(GMacCbComputedVariable computedVar)
        {
            return
                Graph
                .AddNode(computedVar.LowLevelName)
                .SetLabel(
                    Graph.Table(
                        "Scalar",
                        Graph.SimpleTable(
                            computedVar.LowLevelName,
                            computedVar.RhsExpr.ToString()
                            )
                        )
                    );
        }

        private DotNode AddOutputExpression(GMacCbOutputVariable computedVar)
        {
            var dict =
                computedVar.RhsExpr.Arguments.ToDictionary(
                    item => item.ToString(),
                    item => item.ToString()
                    );

            return
                Graph
                .AddNode(computedVar.LowLevelName)
                .SetLabel(
                    Graph.Table(
                        "Output",
                        Graph.SimpleTable(
                            computedVar.LowLevelName,
                            computedVar.ValueAccessName,
                            computedVar.RhsExpr.HeadText
                            ),
                        Graph.SimpleTable(dict)
                        )
                    );
        }

        private DotNode AddTempExpression(GMacCbComputedVariable computedVar)
        {
            var dict =
                computedVar.RhsExpr.Arguments.ToDictionary(
                    item => item.ToString(),
                    item => item.ToString()
                    );

            return
                Graph
                .AddNode(computedVar.LowLevelName)
                .SetLabel(
                    Graph.Table(
                        "Scalar",
                        Graph.SimpleTable(computedVar.LowLevelName, computedVar.RhsExpr.HeadText),
                        Graph.SimpleTable(dict)
                        )
                );
        }

        private void AddExpressions()
        {
            foreach (var computedVar in OptimizedCodeBlock.ComputedVariables)
            {
                if (computedVar.RhsExpr.ArgumentsCount == 0)
                {
                    if (computedVar.IsOutput) continue;

                    var node0 = AddConstantTempExpression(computedVar);

                    ExprSubGraph.AddNode(node0.NodeName);

                    continue;
                }

                var node =
                    computedVar.IsOutput
                    ? AddOutputExpression((GMacCbOutputVariable)computedVar)
                    : AddTempExpression(computedVar);

                ExprSubGraph.AddNode(node.NodeName);

                foreach (var rhsVar in computedVar.RhsExpr.Arguments.Where(arg => arg.IsLowLevelVariable()))
                    Graph
                    .AddEdge(
                        rhsVar.ToNodeRef(DotCompass.Center),
                        node.ToNodeRef(rhsVar.ToString(), DotCompass.West)
                        );
            }
        }

        private void LinkInputs()
        {
            foreach (var inVar in OptimizedCodeBlock.UsedInputVariables)
            {
                Graph
                .AddNode(inVar.LowLevelName)
                .SetLabel(
                    Graph.Table(
                        "Input", 
                        Graph.SimpleTable(inVar.LowLevelName, inVar.ValueAccess.ValueAccessName)
                        )
                    );

                Graph.AddEdge(
                    "Macro Inputs".ToNodeRef(DotCompass.Center),
                    inVar.LowLevelName.ToNodeRef(DotCompass.West)
                    )
                .SetColor(Color.White.ToDotRgbColor());
            }
        }

        private void AddConstantInputs()
        {
            var dict =
                MacroBinding
                .Bindings
                .Where(item => item.ValueAccess.IsInputParameter && item.IsConstant)
                .Select(item => item.ValueAccessName + " = " + item.ConstantValue.ToString())
                .ToArray();

            var subGraph = Graph.AddSubGraph().SetRank(DotRankType.Source);

            if (dict.Length > 0)
                subGraph
                .AddNode("Macro Inputs")
                .SetLabel(
                    Graph.Table(
                        "Input",
                        Graph.SimpleTable(Macro.AccessName, "Constant Inputs"),
                        Graph.SimpleTable(dict)
                        )
                    );
            else
                subGraph
                .AddNode("Macro Inputs")
                .SetLabel(Graph.Table("Macro", Macro.AccessName));
        }

        private void AddConstantOutputs()
        {
            var dict =
                OptimizedCodeBlock
                .OutputVariables
                .Where(item => item.IsConstant)
                .Select(item => item.ValueAccessName + " = " + item.RhsExpr)
                .ToArray();

            if (dict.Length > 0)
                Graph
                .AddNode("Macro Outputs")
                .SetLabel(
                    Graph.Table(
                        "Output",
                        Graph.SimpleTable(Macro.AccessName, "Constant Outputs"),
                        Graph.SimpleTable(dict)
                        )
                    );
        }


        public AstVisitorDotGraph ToGraphViz()
        {
            Graph
            .SetRankDir(DotRankDirection.LeftToRight);

            ExprSubGraph =
                Graph
                .AddSubGraph();

            AddConstantInputs();

            AddConstantOutputs();

            AddExpressions();

            LinkInputs();

            return Graph;
        }
    }
}
