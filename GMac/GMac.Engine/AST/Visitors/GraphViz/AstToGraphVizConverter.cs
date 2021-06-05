using System.Drawing;
using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.GraphViz.Dot.Color;
using CodeComposerLib.GraphViz.Dot.Value;
using Microsoft.CSharp.RuntimeBinder;

namespace GMac.Engine.AST.Visitors.GraphViz
{
    public abstract class AstToGraphVizConverter : IAstObjectDynamicVisitor
    {
        private AstVisitorDotGraph _graph;

        public AstVisitorDotGraph Graph 
            => _graph ?? (_graph = InitGraph());

        public bool IgnoreNullElements => false;

        public bool UseExceptions => true;


        public virtual void Fallback(IAstObject objItem, RuntimeBinderException excException)
        {
        }


        protected virtual AstVisitorDotGraph InitGraph()
        {
            var graph = new AstVisitorDotGraph();

            graph
                .SetFontName("Consolas")
                .SetStyleRadial()
                .SetGradientAngle(45)
                //.SetSplines(DotSplines.Polyline)
                //.SetBackgroundColor(DotColorScheme.Blues[9][7], DotColorScheme.Blues[9][8]);
                .SetBackgroundColor(DotColorScheme.Ylgn[9][1], DotColorScheme.Ylgn[9][2]);

            graph
                .AddNodeDefaults()
                .SetFontName("Consolas")
                .SetFillColor(Color.White.ToDotRgbColor())
                .SetShape(DotNodeShape.Rectangle)
                .SetStyle(DotNodeStyle.Bold, DotNodeStyle.Rounded, DotNodeStyle.Filled);

            graph
                .AddEdgeDefaults()
                .SetHeadPort(DotCompass.Center)
                .SetTailPort(DotCompass.Center)
                .SetPenWidth(2)
                .SetArrowHead(DotArrowType.Vee)
                .SetArrowTail(DotArrowType.Crow)
                .SetColor(DotColor.ColorList(DotColorScheme.Ylgn[9][7], DotColorScheme.Ylgn[9][2], DotColorScheme.Ylgn[9][7]));
                //.SetColor(DotColor.ColorList(Color.DarkGreen, Color.DarkSeaGreen, Color.DarkGreen));
            //.SetColor(DotColor.Rgb(Color.LawnGreen));

            graph
                .AddSubGraphDefaults()
                .SetStyle(DotClusterStyle.Bold, DotClusterStyle.Rounded)
                .SetColor(Color.White.ToDotRgbColor())
                .SetFontColor(Color.White.ToDotRgbColor())
                .SetPenWidth(2);

            return graph;
        }
    }
}
