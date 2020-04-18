using System.Drawing;
using System.IO;
using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.GraphViz.Dot.Color;
using CodeComposerLib.GraphViz.Dot.Value;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Rendering;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Multivectors;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Rendering
{
    public static class RenderingUtils
    {
        public static string ToLaTeX(this Expr expr)
        {
            return GaSymbolicsUtils.Cas.Connection.EvaluateToString(
                Mfs.EToString[Mfs.TeXForm[expr]]
            );
        }

        public static string ToLaTeX(this MathematicaScalar scalar)
        {
            return GaSymbolicsUtils.Cas.Connection.EvaluateToString(
                Mfs.EToString[Mfs.TeXForm[scalar.Expression]]
            );
        }

        public static DotGraph ToGraphViz(this GaSymMultivector mv, bool showFullGraph = false, bool showLeafValues = false)
        {
            var dotGraph = DotGraph.Directed("Graph");
            dotGraph.SetImagePath(
                Directory
                    .GetCurrentDirectory()
                    .Replace('\\', '/')
            );

            dotGraph.AddBinaryTree(mv.TermsTree, showFullGraph);

            if (!showLeafValues)
                return dotGraph;

            foreach (var term in mv.Terms)
            {
                var parentNodeName = term.Key.PatternToString(mv.VSpaceDimension);
                var node = dotGraph.AddNode("coef" + parentNodeName);

                node.SetLaTeXImage(term.Value.ToLaTeX());
                node.SetLabel("");
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
