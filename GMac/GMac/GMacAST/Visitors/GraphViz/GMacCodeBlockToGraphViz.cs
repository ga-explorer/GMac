using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.GraphViz.Dot.Color;
using CodeComposerLib.GraphViz.Dot.Label.Table;
using CodeComposerLib.GraphViz.Dot.Value;
using GMac.GMacAPI.CodeBlock;

namespace GMac.GMacAST.Visitors.GraphViz
{
    public sealed class GMacCodeBlockToGraphViz : AstToGraphVizConverter
    {
        public GMacCodeBlock CodeBlock { get; }

        public int MaxLevels { get; private set; }

        public DotIndexedColorSchemeTemplate ColorTemplate { get; private set; }


        public GMacCodeBlockToGraphViz(GMacCodeBlock codeBlock)
        {
            CodeBlock = codeBlock;
        }


        protected override AstVisitorDotGraph InitGraph()
        {
            var graph = new AstVisitorDotGraph();

            graph
                .SetFontName("Consolas")
                .SetStyleRadial()
                .SetGradientAngle(45)
                .SetBackgroundColor(Color.Wheat.ToDotRgbColor(), Color.BurlyWood.ToDotRgbColor())
                .SetSplines(DotSplines.Spline);
                //.SetBackgroundColor(DotColorScheme.Blues[9][7], DotColorScheme.Blues[9][8]);

            graph
                .AddNodeDefaults()
                .SetFontName("Consolas")
                .SetStyle(DotNodeStyle.Filled)
                .SetFillColor(Color.White.ToDotRgbColor())
                .SetShape(DotNodeShape.Point)
                .SetLabel("");

            graph
                .AddEdgeDefaults()
                .SetHeadPort(DotCompass.Center)
                .SetTailPort(DotCompass.Center)
                //.SetPenWdith(2)
                .SetArrowHead(DotArrowType.Diamond)
                .SetArrowSize(0.5f)
                .SetColor(Color.Green.ToDotRgbColor());
            //.SetColor(DotColor.Rgb(Color.LawnGreen));

            graph
                .AddSubGraphDefaults()
                .SetStyle(DotClusterStyle.Bold, DotClusterStyle.Rounded)
                .SetColor(Color.White.ToDotRgbColor())
                .SetFontColor(Color.White.ToDotRgbColor());
            //.SetPenWidth(2);

            return graph;
        }


        private int GetEdgeColorIndex(int level)
        {
            var maxColors = ColorTemplate.MaxColors;

            var ratio = level / (double)MaxLevels;

            return (int)Math.Round(ratio * (maxColors - 1));
        }

        private DotColor GetEdgeColor(int level)
        {
            return ColorTemplate[ColorTemplate.MaxColors][GetEdgeColorIndex(level)];
        }
        

        private static string GetNodeName(IGMacCbVariable codeBlockVar, int stepNumber)
        {
            return "Step-" + stepNumber + "-" + codeBlockVar.MidLevelName;
        }

        private DotSubGraph AddInputNodes()
        {
            var sourceSubGraph =
                Graph
                    .AddSubGraph("Inputs")
                    .SetRank(DotRankType.Source);

            sourceSubGraph
                .AddNodeDefaults()
                .SetShape(DotNodeShape.Rectangle)
                .SetFillColor(Color.Black.ToDotRgbColor())
                .SetStyle(DotNodeStyle.Rounded)
                .SetGroup("Inputs");

            //Add input nodes of code block
            foreach (var inputVar in CodeBlock.InputVariables)
            {
                sourceSubGraph
                    .AddNode(inputVar.MidLevelName)
                    .SetLabel(inputVar.ValueAccessName);
            }

            return sourceSubGraph;
        }

        private DotSubGraph AddOutputNodes()
        {
            var sourceSubGraph =
                Graph
                    .AddSubGraph("Outputs")
                    .SetRank(DotRankType.Sink);

            sourceSubGraph
                .AddNodeDefaults()
                .SetShape(DotNodeShape.Rectangle)
                .SetFillColor(Color.Black.ToDotRgbColor())
                .SetStyle(DotNodeStyle.Rounded)
                .SetGroup("Outputs");

            //Add input nodes of code block
            foreach (var outputVar in CodeBlock.OutputVariables)
            {
                sourceSubGraph
                    .AddNode(outputVar.MidLevelName)
                    .SetLabel(outputVar.ValueAccessName);
            }

            return sourceSubGraph;
        }

        private void AddExpressions()
        {
            var inputsSubGraph = AddInputNodes();
            var outputsSubGraph = AddOutputNodes();

            var lastUpdatedDict = new Dictionary<string, int>();

            Graph
                .SetRankDir(DotRankDirection.LeftToRight)
                .AddNodeDefaults()
                .SetShape(DotNodeShape.Point)
                .SetColor(Color.Black.ToDotRgbColor());

            //Prepare first sub graph to visualize initial values of inputs
            var prevSubGraph =
                Graph
                    .AddSubGraph("Step-0")
                    .SetRank(DotRankType.Same);

            prevSubGraph
                .AddNodeDefaults()
                .SetGroup("Step-0");

            prevSubGraph
                .AddNode("Step-0")
                .SetShape(DotNodeShape.Rectangle)
                .SetStyle(DotNodeStyle.Rounded)
                .SetLabel("0");
                //.SetLabel("0: Initialize Inputs");

            foreach (var inputVar in CodeBlock.InputVariables)
            {
                lastUpdatedDict.Add(inputVar.MidLevelName, 0);

                var nodeName1 = inputVar.MidLevelName;
                var nodeName2 = GetNodeName(inputVar, 0);

                prevSubGraph
                    .AddNode(GetNodeName(inputVar, 0))
                    .SetLabel(inputVar.MidLevelName);

                Graph
                    .AddEdge(nodeName1, nodeName2);
            }

            var stepNumber = 0;
            foreach (var computedVar in CodeBlock.ComputedVariables)
            {
                stepNumber++;
                
                var subGraph =
                    Graph
                        .AddSubGraph("Step-" + stepNumber)
                        .SetRank(DotRankType.Same);

                subGraph
                    .AddNodeDefaults()
                    .SetGroup("Step-" + stepNumber);

                var varName =
                    computedVar.IsOutput
                        ? ((GMacCbOutputVariable) computedVar).ValueAccessName
                        : computedVar.MidLevelName;

                subGraph
                    .AddNode("Step-" + stepNumber)
                    .SetShape(DotNodeShape.Rectangle)
                    .SetStyle(DotNodeStyle.Rounded)
                    .SetLabel(stepNumber.ToString());
                    //.SetLabel(stepNumber + ": Compute " + varName);

                Graph
                    .AddEdge("Step-" + (stepNumber - 1), "Step-" + stepNumber);
                

                var computedNodeName = GetNodeName(computedVar, stepNumber);

                subGraph
                    .AddNode(computedNodeName);
                    //.SetLabel(computedVar.MidLevelName);

                if (computedVar.RhsVariablesCount == 0)
                {
                    prevSubGraph = subGraph;
                    continue;
                }

                var rhsVars = computedVar.RhsVariables.Distinct();
                foreach (var rhsVar in rhsVars)
                {
                    var rhsNodeName = GetNodeName(rhsVar, stepNumber - 1);

                    var rhsStep = lastUpdatedDict[rhsVar.MidLevelName];

                    //
                    if (rhsStep < stepNumber - 1)
                    {
                        lastUpdatedDict[rhsVar.MidLevelName] = stepNumber - 1;

                        prevSubGraph
                            .AddNode(rhsNodeName);
                            //.SetLabel(rhsVar.MidLevelName);

                        Graph
                            .AddEdge(GetNodeName(rhsVar, rhsStep), rhsNodeName)
                            .SetStyle(DotEdgeStyle.Dashed)
                            .SetArrowHead(DotArrowType.None);
                    }

                    Graph
                        .AddEdge(rhsNodeName, computedNodeName);
                }

                if (lastUpdatedDict.ContainsKey(computedVar.MidLevelName))
                    lastUpdatedDict[computedVar.MidLevelName] = stepNumber;
                else
                    lastUpdatedDict.Add(computedVar.MidLevelName, stepNumber);

                if (computedVar.IsOutput)
                {
                    Graph
                        .AddEdge(computedNodeName, computedVar.MidLevelName);
                }

                prevSubGraph = subGraph;
            }
        }

        private DotHtmlTable GetColorsTable()
        {
            var dict = new Dictionary<int, Tuple<int, int>>();

            for (var i = 0; i <= MaxLevels; i++)
            {
                var colorNum = GetEdgeColorIndex(i);

                if (dict.TryGetValue(colorNum, out var range) == false)
                    dict.Add(colorNum, new Tuple<int, int>(i, i));

                else
                {
                    if (i < range.Item1) 
                        dict[colorNum] = new Tuple<int, int>(i, range.Item2);

                    else if (i > range.Item2)
                        dict[colorNum] = new Tuple<int, int>(range.Item1, i);
                }
            }

            var colorsTable =
                DotUtils.Table()
                .SetCellBorder(1)
                .SetBorder(0);

            foreach (var pair in dict)
                colorsTable
                .AddRow()
                .AddCell(
                    pair.Value.Item1 == pair.Value.Item2 
                    ? pair.Value.Item1.ToString()
                    : (pair.Value.Item1 + " : " + pair.Value.Item2)
                    )
                .SetBackgroundColor(
                    ColorTemplate[ColorTemplate.MaxColors][pair.Key]
                    );

            return colorsTable;
        }

        private void AddStatistics()
        {
            var statsTable = 
                DotUtils.Table()
                .SetCellBorder(1)
                .SetBorder(0);

            var stats = CodeBlock.GetStatistics();

            var flag = false;
            foreach (var pair in stats)
            {
                var row = statsTable.AddRow(pair.Key, pair.Value);

                if (flag)
                {
                    row[0].SetSides(DotSides.Top);
                    row[1].SetSides(DotSides.Top);
                }
                else
                {
                    row[0].SetBorder(0);
                    row[1].SetBorder(0);

                    flag = true;
                }

                row[0].SetAlign(DotAlign.Right);
                row[1].SetAlign(DotAlign.Left);
            }

            var colorsTable = GetColorsTable();

            var finalTable =
                DotUtils.Table()
                .SetCellBorder(1)
                .SetBorder(0);

            finalTable
                .AddRow()
                .AddCells(statsTable, colorsTable);

            Graph
            .AddNode("Statistics")
            .SetFontSize(11)
            .SetLabel(finalTable)
            .SetFontName("Consolas")
            .SetFillColor(Color.White.ToDotRgbColor())
            .SetShape(DotNodeShape.Rectangle)
            .SetStyle(DotNodeStyle.Bold, DotNodeStyle.Rounded, DotNodeStyle.Filled);

        }


        public AstVisitorDotGraph ToGraphViz()
        {
            var computationLevels = 
                CodeBlock
                .NonConstantOutputVariables
                .Select(v => v.MaxComputationLevel)
                .ToArray();

            MaxLevels = computationLevels.Length == 0 ? 1 : computationLevels.Max();

            ColorTemplate = DotColorScheme.Prgn;

            //AddStatistics();

            AddExpressions();

            return Graph;
        }
    }
}
