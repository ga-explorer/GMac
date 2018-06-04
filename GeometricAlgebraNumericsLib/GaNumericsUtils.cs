using System;
using System.Drawing;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Maps.Unilinear;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Structures;
using MathNet.Numerics.LinearAlgebra.Double;
using TextComposerLib.Diagrams.GraphViz.Dot;
using TextComposerLib.Diagrams.GraphViz.Dot.Color;
using TextComposerLib.Diagrams.GraphViz.Dot.Value;

namespace GeometricAlgebraNumericsLib
{
    public static class GaNumericsUtils
    {
        public static double Epsilon { get; internal set; } 
            = Math.Pow(2, -25);

        public static bool IsNearZero(this double d)
        {
            return Math.Abs(d) <= Epsilon;
        }

        public static bool IsNearZero(this double d, double epsilon)
        {
            return Math.Abs(d) <= epsilon;
        }

        public static bool IsNearEqual(this double d1, double d2)
        {
            return Math.Abs(d2 - d1) <= Epsilon;
        }


        public static bool IsDiagonal(this Matrix matrix)
        {
            if (ReferenceEquals(matrix, null) || matrix.RowCount != matrix.ColumnCount)
                return false;
            
            var diagonalMatrix = matrix as DiagonalMatrix;
            if (!ReferenceEquals(diagonalMatrix, null))
                return true;

            for (var row = 0; row < matrix.RowCount - 1; row++)
            for (var col = row + 1; col < matrix.ColumnCount; col++)
                if (!matrix[row, col].IsNearZero() || !matrix[col, row].IsNearZero())
                        return false;

            return true;
        }

        public static bool IsIdentity(this Matrix matrix)
        {
            if (ReferenceEquals(matrix, null) || matrix.RowCount != matrix.ColumnCount)
                return false;

            var diagonalMatrix = matrix as DiagonalMatrix;
            if (ReferenceEquals(diagonalMatrix, null))
            {
                for (var row = 0; row < matrix.RowCount - 1; row++)
                for (var col = row + 1; col < matrix.ColumnCount; col++)
                    if (!matrix[row, col].IsNearZero() || !matrix[col, row].IsNearZero())
                        return false;
            }

            for (var row = 0; row < matrix.RowCount; row++)
                if (!matrix[row, row].IsNearEqual(1.0d))
                    return false;

            return true;
        }


        public static DotGraph ToGraphViz(this GaNumMultivector mv, bool showFullGraph = false, bool showLeafValues = false)
        {
            var dotGraph = DotGraph.Directed("Graph");
            dotGraph.AddBinaryTree(mv.TermsTree, showFullGraph);

            if (!showLeafValues)
                return dotGraph;

            foreach (var term in mv.Terms)
            {
                var parentNodeName = term.Key.PatternToString(mv.VSpaceDimension);
                var node = dotGraph.AddNode("coef" + parentNodeName);

                node.SetLabel(term.Value.ToString("G"));
                node.SetShape(DotNodeShape.Note);
                node.SetStyle(DotNodeStyle.Solid);
                node.SetColor(DotColor.Rgb(Color.Black));
                node.SetFontColor(DotColor.Rgb(Color.Black));

                var dotEdge = node.AddEdgeFrom(parentNodeName);
                dotEdge.SetArrowHead(DotArrowType.Inv);
                dotEdge.SetArrowTail(DotArrowType.Inv);
                dotEdge.SetStyle(DotEdgeStyle.Solid);
                dotEdge.SetColor(DotColor.Rgb(Color.Black));
                dotEdge.SetFontColor(DotColor.Rgb(Color.Black));
            }

            return dotGraph;
        }

        public static DotGraph ToGraphViz(this GaNumMapBilinearTree bilinearMap, bool showFullGraph = false, bool showLeafValues = false)
        {
            var dotGraph = DotGraph.Directed("Graph");
            dotGraph.AddQuadTree(bilinearMap.BasisBladesMapTree, showFullGraph);

            if (!showLeafValues)
                return dotGraph;

            foreach (var leafPair in bilinearMap.BasisBladesMapTree.LeafValuePairs)
            {
                var dotHtmlTable = DotUtils.Table();
                dotHtmlTable.SetBorder(0);
                dotHtmlTable.SetCellBorder(1);
                var dotHtmlRow = dotHtmlTable.AddRow();
                var mv = leafPair.Item3;

                foreach (var term in mv.Terms)
                {
                    var columnTable = DotUtils.Table();
                    columnTable.SetBorder(0);
                    columnTable.SetCellBorder(0);
                    columnTable.AddRow(
                        term.Key.PatternToString(mv.VSpaceDimension).ToHtmlString()
                    );
                    columnTable.AddRow(term.Value.ToString("G"));

                    dotHtmlRow.AddCell(columnTable);
                }

                var parentNodeName = 
                    leafPair.Item1.PatternToString(bilinearMap.DomainVSpaceDimension) + ", " +
                    leafPair.Item2.PatternToString(bilinearMap.DomainVSpaceDimension);
                var node = dotGraph.AddNode("coef" + parentNodeName);

                node.SetLabel(dotHtmlTable);
                node.SetShape(DotNodeShape.Note);
                node.SetStyle(DotNodeStyle.Solid);
                node.SetColor(DotColor.Rgb(Color.Black));
                node.SetFontColor(DotColor.Rgb(Color.Black));

                var dotEdge = node.AddEdgeFrom(parentNodeName);
                dotEdge.SetArrowHead(DotArrowType.Inv);
                dotEdge.SetArrowTail(DotArrowType.Inv);
                dotEdge.SetStyle(DotEdgeStyle.Solid);
                dotEdge.SetColor(DotColor.Rgb(Color.Black));
                dotEdge.SetFontColor(DotColor.Rgb(Color.Black));
            }

            return dotGraph;
        }

        public static DotGraph ToGraphViz(this GaNumMapUnilinearTree unilinearMap, bool showFullGraph = false, bool showLeafValues = false)
        {
            var dotGraph = DotGraph.Directed("Graph");
            dotGraph.AddBinaryTree(unilinearMap.BasisBladesMapTree, showFullGraph);

            if (!showLeafValues)
                return dotGraph;

            foreach (var leafPair in unilinearMap.BasisBladesMapTree.LeafValuePairs)
            {
                var dotHtmlTable = DotUtils.Table();
                dotHtmlTable.SetBorder(0);
                dotHtmlTable.SetCellBorder(1);
                var dotHtmlRow = dotHtmlTable.AddRow();
                var mv = leafPair.Value;

                foreach (var term in mv.Terms)
                {
                    var columnTable = DotUtils.Table();
                    columnTable.SetBorder(0);
                    columnTable.SetCellBorder(0);
                    columnTable.AddRow(
                        term.Key.PatternToString(mv.VSpaceDimension).ToHtmlString()
                    );
                    columnTable.AddRow(term.Value.ToString("G"));

                    dotHtmlRow.AddCell(columnTable);
                }

                var parentNodeName = leafPair.Key.PatternToString(unilinearMap.DomainVSpaceDimension);
                var node = dotGraph.AddNode("coef" + parentNodeName);

                node.SetLabel(dotHtmlTable);
                node.SetShape(DotNodeShape.Note);
                node.SetStyle(DotNodeStyle.Solid);
                node.SetColor(DotColor.Rgb(Color.Black));
                node.SetFontColor(DotColor.Rgb(Color.Black));

                var dotEdge = node.AddEdgeFrom(parentNodeName);
                dotEdge.SetArrowHead(DotArrowType.Inv);
                dotEdge.SetArrowTail(DotArrowType.Inv);
                dotEdge.SetStyle(DotEdgeStyle.Solid);
                dotEdge.SetColor(DotColor.Rgb(Color.Black));
                dotEdge.SetFontColor(DotColor.Rgb(Color.Black));
            }

            return dotGraph;
        }
    }
}
