using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GMac.GMacAPI.CodeBlock;
using TextComposerLib.Diagrams.GraphViz.Dot;
using TextComposerLib.Diagrams.GraphViz.Dot.Color;
using TextComposerLib.Diagrams.GraphViz.Dot.Label.Table;
using TextComposerLib.Diagrams.GraphViz.Dot.Value;

namespace GMac.GMacAST.Visitors.GraphViz
{
    public sealed class GMacCodeBlockToGraphViz : GraphVizConverter
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
                .SetSplines(DotSplines.Line);
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

        private void AddExpressions()
        {
            foreach (var inputVar in CodeBlock.UsedInputVariables)
                Graph
                    .AddNode(inputVar.LowLevelName)
                    //.SetShape(DotNodeShape.Circle)
                    .SetFillColor(Color.DodgerBlue.ToDotRgbColor())
                    .SetLabel("");

            foreach (var outputVar in CodeBlock.OutputVariables)
                Graph
                    .AddNode(outputVar.LowLevelName)
                    //.SetShape(DotNodeShape.DoubleCircle)
                    .SetFillColor(Color.Teal.ToDotRgbColor())
                    .SetLabel("");

            foreach (var computedVar in CodeBlock.ComputedVariables)
            {
                if (computedVar.RhsVariablesCount == 0) continue;

                var rhsVars = computedVar.RhsVariables.Distinct();

                foreach (var rhsVar in rhsVars)
                    Graph
                        .AddEdge(rhsVar.LowLevelName, computedVar.LowLevelName)
                        .SetColor(
                            GetEdgeColor(rhsVar.MaxComputationLevel)
                        );
            }
        }

        private DotHtmlTable GetColorsTable()
        {
            var dict = new Dictionary<int, Tuple<int, int>>();

            for (var i = 0; i <= MaxLevels; i++)
            {
                var colorNum = GetEdgeColorIndex(i);

                Tuple<int, int> range;

                if (dict.TryGetValue(colorNum, out range) == false)
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

            AddStatistics();

            AddExpressions();

            return Graph;
        }
    }
}
