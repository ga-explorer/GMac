using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GMac.GMacAST.Commands;
using GMac.GMacAST.Expressions;
using GMac.GMacAST.Symbols;
using TextComposerLib;
using TextComposerLib.Diagrams.GraphViz.Dot;
using TextComposerLib.Diagrams.GraphViz.Dot.Value;

namespace GMac.GMacAST.Visitors.GraphViz
{
    public sealed class MacroToGraphViz : GraphVizConverter
    {
        internal struct ActiveNodeInfo
        {
            public string NodeName { get; set; }

            public DotNodeRef LinkToNode { get; set; }

            public bool HasLinkToNode => LinkToNode != null;
        }


        private readonly Stack<ActiveNodeInfo> _nodesStack = new Stack<ActiveNodeInfo>();

        private ActiveNodeInfo NodeInfo => _nodesStack.Peek();

        private DotSubGraph StepsCluster { get; set; }


        private void AddInputsCluster(AstMacro macro)
        {
            var inputsGraph = Graph;
            //    Graph
            //    .AddSubGraph("Inputs")
            //    .SetRank(DotRankType.Min);
            ////.SetLabel("Input Parameters".Bold());

            foreach (var inParam in macro.InputParameters)
            {
                var node = inputsGraph
                    .AddNode(inParam.Name)
                    .SetLabel(
                        Graph.Table(
                            "Input",
                            Graph.SimpleTable(inParam.GMacTypeSignature, "Input"),
                            inParam.Name
                            )
                    );

                inputsGraph
                .AddEdge(
                    macro.AccessName.ToNodeRef(DotCompass.Center), 
                    node.ToNodeRef(DotCompass.Center)
                    )
                .SetColor(Color.Black.ToDotRgbColor());
            }
        }

        private void AddOutputsCluster(AstMacro macro)
        {
            var outputsGraph =
                Graph
                .AddSubGraph("Outputs")
                .SetRank(DotRankType.Max);

            outputsGraph
                .AddNode(macro.OutputParameter.Name)
                .SetLabel(
                    Graph.Table(
                        "Output",
                        Graph.SimpleTable(macro.OutputParameter.GMacTypeSignature, "Output"),
                        macro.OutputParameter.Name
                        )
                    );
        }

        private void AddStepsCluster(AstMacro macro)
        {
            StepsCluster =
                Graph
                .AddSubGraph("Processing");
                //.SetLabel("Optimized Processing Steps".Bold());

            var commands =
                macro
                    .OptimizedCommandBlock
                    .Commands
                    .Where(command => command.IsValidCommandLet);

            foreach (var command in commands)
                command.AcceptVisitor(this);
        }

        private void FollowExpression(AstExpression expr, DotNodeRef linkToNode)
        {
            if (ReferenceEquals(expr, null) || expr.IsInvalid)
                return;

            var nodeInfo =
                new ActiveNodeInfo()
                {
                    NodeName = Graph.NewNodeName(),
                    LinkToNode = linkToNode
                };

            
            _nodesStack.Push(nodeInfo);

            expr.AcceptVisitor(this);

            _nodesStack.Pop();

            if (expr.IsValidDatastoreValueAccess == false)
                Graph
                    .AddEdge(nodeInfo.NodeName, linkToNode.NodeName)
                    .SetHeadPort(linkToNode.PortName, linkToNode.Compass)
                    .SetTailPort(DotCompass.Center);
        }

        private void FollowExpression(AstExpression expr, string nodeName)
        {
            if (ReferenceEquals(expr, null) || expr.IsInvalid)
                return;

            var nodeInfo =
                new ActiveNodeInfo()
                {
                    NodeName = nodeName,
                    LinkToNode = null
                };


            _nodesStack.Push(nodeInfo);

            expr.AcceptVisitor(this);

            _nodesStack.Pop();
        }


        public void Visit(AstDatastoreValueAccess expr)
        {
            if (NodeInfo.HasLinkToNode)
            {
                if (expr.IsFullAccess)
                {
                    StepsCluster
                    .AddEdge(
                        expr.RootSymbol.Name,
                        NodeInfo.LinkToNode.NodeName
                    )
                    .SetTailPort(DotCompass.Center)
                    .SetHeadPort(NodeInfo.LinkToNode.PortName, NodeInfo.LinkToNode.Compass);

                    return;
                }

                var node =
                    StepsCluster
                    .AddNode(Graph.NewNodeName())
                    .SetLabel(
                        Graph.Table(
                            "Filter",
                            Graph.SimpleTable(expr.GMacTypeSignature, "Partial Access"),
                            expr.PartialAccessName.ToHtmlString()
                            )
                        );

                StepsCluster
                    .AddEdge(
                        expr.RootSymbol.Name,
                        node.NodeName
                    )
                    .SetTailPort(DotCompass.Center)
                    .SetHeadPort(DotCompass.West);

                StepsCluster
                    .AddEdge(
                        node.NodeName,
                        NodeInfo.LinkToNode.NodeName
                    )
                    .SetTailPort(DotCompass.Center)
                    .SetHeadPort(NodeInfo.LinkToNode.PortName, NodeInfo.LinkToNode.Compass);

                return;
            }

            if (expr.IsFullAccess)
            {
                Graph
                    .AddEdge(expr.RootSymbol.Name, NodeInfo.NodeName)
                    .SetTailPort(DotCompass.Center)
                    .SetHeadPort(DotCompass.West);

                return;
            }

            var node1 =
                StepsCluster
                .AddNode(Graph.NewNodeName())
                .SetLabel(
                    Graph.Table(
                        "Filter",
                        Graph.SimpleTable(expr.GMacTypeSignature, "Partial Access"),
                        expr.PartialAccessName.ToHtmlString()
                        )
                    );

            Graph
                .AddEdge(expr.RootSymbol.Name, node1.NodeName)
                .SetTailPort(DotCompass.Center)
                .SetHeadPort(DotCompass.West);

            Graph
                .AddEdge(node1.NodeName, NodeInfo.NodeName)
                .SetTailPort(DotCompass.Center)
                .SetHeadPort(DotCompass.West);
        }

        public void Visit(AstValueScalar expr)
        {
            StepsCluster
                .AddNode(NodeInfo.NodeName)
                .SetLabel(expr.ToString());
        }

        public void Visit(AstValueMultivector expr)
        {
            var frame = expr.Frame;

            var dict = 
                expr.Terms.Select(
                    term => 
                        frame.BasisBlade(term.TermBasisBladeId).CoefName + " = " + term.CoefValue.ToString()
                    );

            //var dict = 
            //    expr.ActiveIDs.ToDictionary(
            //        id => frame.BasisBlade(id).BinaryIndexedName, 
            //        id => "#" + frame.BasisBlade(id).BasisBladeName + "#"
            //        );

            //var node =
                StepsCluster
                .AddNode(NodeInfo.NodeName)
                .SetLabel(
                    Graph.Table(
                        "Frame",
                        Graph.SimpleTable(expr.FrameMultivector.AccessName, "Value"),
                        Graph.SimpleTable(dict)
                        )
                    );

            //foreach (var term in expr.Terms)
            //    FollowExpression(
            //        term.CoefValue, 
            //        node.ToNodeRef(
            //            frame.BasisBlade(term.TermId).BinaryIndexedName, 
            //            DotCompass.West
            //            )
            //        );
        }

        public void Visit(AstValueStructure expr)
        {
            var dict =
                expr.Terms.ToDictionary(term => term.DataMemberName, term => term.DataMemberName);

            var node =
                StepsCluster
                .AddNode(NodeInfo.NodeName)
                .SetLabel(
                    Graph.Table(
                        "Structure",
                        Graph.SimpleTable(expr.Structure.AccessName, "Value"),
                        Graph.SimpleTable(dict)
                        )
                    );

            foreach (var term in expr.Terms)
                FollowExpression(term.DataMemberValue, node.ToNodeRef(term.DataMemberName, DotCompass.West));
        }

        public void Visit(AstUnaryExpression expr)
        {
            var opName =
                String.IsNullOrEmpty(expr.OperatorSymbol)
                    ? expr.OperatorName
                    : expr.OperatorSymbol;

            var node =
                StepsCluster
                .AddNode(NodeInfo.NodeName)
                .SetLabel(opName.Bold());

            FollowExpression(expr.Operand, node.ToNodeRef(DotCompass.West));
        }

        public void Visit(AstTypeCast expr)
        {
            var node =
                StepsCluster
                .AddNode(NodeInfo.NodeName)
                .SetLabel(
                    Graph.Table(
                        Graph.SelectIconName(expr.GMacType),
                        Graph.SimpleTable(expr.GMacTypeSignature, "Cast")
                        )
                    );

            FollowExpression(expr.Operand, node.ToNodeRef(DotCompass.West));
        }

        public void Visit(AstTransformCall expr)
        {
            var node =
                StepsCluster
                .AddNode(NodeInfo.NodeName)
                .SetLabel(
                    Graph.Table(
                        "Transform",
                        Graph.SimpleTable(
                            expr.GMacTypeSignature, 
                            "Transform", 
                            expr.CalledTransformAccessName
                            )
                        )
                    );

            FollowExpression(expr.Operand, node.ToNodeRef(DotCompass.West));
        }

        public void Visit(AstBinaryExpression expr)
        {
            var opName =
                String.IsNullOrEmpty(expr.OperatorSymbol)
                    ? expr.OperatorName
                    : expr.OperatorSymbol;

            var node =
                StepsCluster
                .AddNode(NodeInfo.NodeName)
                .SetLabel(opName.Bold());


            FollowExpression(expr.FirstOperand, node.ToNodeRef(DotCompass.NorthWest));

            FollowExpression(expr.SecondOperand, node.ToNodeRef(DotCompass.SouthWest));
        }

        public void Visit(AstParametricSymbolicExpression expr)
        {
            var dict = 
                expr.UsedSymbolicVariables.ToDictionary(item => item);

            var node =
                StepsCluster
                .AddNode(NodeInfo.NodeName)
                .SetLabel(
                    Graph.Table(
                        "Scalar",
                        Graph.SimpleTable(
                            expr.GMacTypeSignature, 
                            "Symbolic Expression", 
                            expr.SymbolicScalar.ToString()
                            ),
                        Graph.SimpleTable(dict)
                        )
                    );

            foreach (var pair in expr.Assignments)
                FollowExpression(pair.Value, node.ToNodeRef(pair.Key, DotCompass.West));
        }

        public void Visit(AstMultivectorConstructor expr)
        {
            //var frame = expr.ConstructedMultivector.ParentFrame;

            var dict = new Dictionary<string, string> { { "Default", "Default" } };

            foreach (var pair in expr.Assignments)
                dict.Add(
                    pair.Key.BinaryIndexedName,
                    pair.Key.CoefName
                    );

            var node =
                StepsCluster
                .AddNode(NodeInfo.NodeName)
                .SetLabel(
                    Graph.Table(
                        "Frame",
                        Graph.SimpleTable(expr.GMacTypeSignature, "Construct"),
                        Graph.SimpleTable(dict)
                        )
                    );

            FollowExpression(expr.DefaultExpression, node.ToNodeRef("Default", DotCompass.West));

            foreach (var pair in expr.Assignments)
                FollowExpression(
                    pair.Value, 
                    node.ToNodeRef(
                        pair.Key.BinaryIndexedName, 
                        DotCompass.West
                        )
                    );
        }

        public void Visit(AstStructureConstructor expr)
        {
            var dict = new Dictionary<string, string> { { "Default", "Default" } };

            foreach (var pair in expr.Assignments)
                dict.Add(
                    pair.Key.ValueAccessName,
                    pair.Key.ValueAccessName
                    );

            var node =
                StepsCluster
                .AddNode(NodeInfo.NodeName)
                .SetLabel(
                    Graph.Table(
                        "Structure",
                        Graph.SimpleTable(expr.GMacTypeSignature, "Construct"),
                        Graph.SimpleTable(dict)
                        )
                    );

            FollowExpression(expr.DefaultExpression, node.ToNodeRef("Default", DotCompass.West));

            foreach (var pair in expr.Assignments)
                FollowExpression(pair.Value, node.ToNodeRef(pair.Key.ValueAccessName, DotCompass.West));
        }

        public void Visit(AstCommandLet command)
        {
            var nodeName = command.DatastoreValueAccess.RootSymbol.Name;

            if (command.DatastoreValueAccess.IsOutputParameter)
                FollowExpression(
                    command.Expression, 
                    Graph.AddNode(nodeName).ToNodeRef(DotCompass.West)
                    );

            else
                FollowExpression(
                    command.Expression, 
                    nodeName
                    );
        }

        public void Visit(AstMacro macro)
        {
            //Graph = AstVisitorDotGraph.Create();

            _nodesStack.Clear();

            Graph
                .SetRankDir(DotRankDirection.LeftToRight);

            Graph
            .AddNode(macro.AccessName)
            .SetLabel(
                Graph.Table("Macro", macro.AccessName)
                );


            AddInputsCluster(macro);

            AddOutputsCluster(macro);

            AddStepsCluster(macro);
        }
    }
}
