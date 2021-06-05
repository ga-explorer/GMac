using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.GraphViz.Dot.Value;

namespace GMac.Engine.AST.Visitors.GraphViz
{
    public sealed class AstGeneralDiagrams : AstToGraphVizConverter
    {
        public DotGraph GMacAstGeneral()
        {
            Graph
                .SetRankDir(DotRankDirection.LeftToRight)
                .AddEdgeDefaults()
                .SetTailPort(DotCompass.Center)
                .SetHeadPort(DotCompass.Center);

            Graph
                .AddNode("Ast Root")
                .SetLabel(Graph.Table("GMacAST", "AST Root".Bold()));

            Graph
                .AddNode("Ast Root Namespaces")
                .SetLabel(Graph.Table("Namespace", "Namespaces".Bold()))
                .AddEdgeFrom("Ast Root");

            Graph
                .AddNode("Scalar")
                .SetLabel(Graph.Table("Scalar", "Scalar Type".Bold()))
                .AddEdgeFrom("Ast Root");


            Graph
                .AddNode("Namespace Namespaces")
                .SetLabel(Graph.Table("Namespace", "Namespaces".Bold()))
                .AddEdgeFrom("Ast Root Namespaces");

            Graph
                .AddNode("Namespace Frames")
                .SetLabel(Graph.Table("Frame", "Frames".Bold()))
                .AddEdgeFrom("Ast Root Namespaces");

            Graph
                .AddNode("Namespace Constants")
                .SetLabel(Graph.Table("Constant", "Constants".Bold()))
                .AddEdgeFrom("Ast Root Namespaces");

            Graph
                .AddNode("Namespace Structures")
                .SetLabel(Graph.Table("Structure", "Structures".Bold()))
                .AddEdgeFrom("Ast Root Namespaces");

            Graph
                .AddNode("Namespace Macros")
                .SetLabel(Graph.Table("Macro", "Macros".Bold()))
                .AddEdgeFrom("Ast Root Namespaces");

            Graph
                .AddNode("Namespace Transforms")
                .SetLabel(Graph.Table("Transform", "Transforms".Bold()))
                .AddEdgeFrom("Ast Root Namespaces");


            Graph
                .AddNode("Frame Basis Vectors")
                .SetLabel(Graph.Table("BasisVector", "Basis Vectors".Bold()))
                .AddEdgeFrom("Namespace Frames");

            Graph
                .AddNode("Frame Basis Blades")
                .SetLabel(Graph.Table("BasisVector", "Basis Blades".Bold()))
                .AddEdgeFrom("Namespace Frames");

            Graph
                .AddNode("Frame Multivector Type")
                .SetLabel(Graph.Table("Frame", "Multivector Type".Bold()))
                .AddEdgeFrom("Namespace Frames");

            Graph
                .AddNode("Frame Subspaces")
                .SetLabel(Graph.Table("Subspace", "Subspaces".Bold()))
                .AddEdgeFrom("Namespace Frames");

            Graph
                .AddNode("Frame Constants")
                .SetLabel(Graph.Table("Constant", "Constants".Bold()))
                .AddEdgeFrom("Namespace Frames");

            Graph
                .AddNode("Frame Structures")
                .SetLabel(Graph.Table("Structure", "Structures".Bold()))
                .AddEdgeFrom("Namespace Frames");

            Graph
                .AddNode("Frame Macros")
                .SetLabel(Graph.Table("Macro", "Macros".Bold()))
                .AddEdgeFrom("Namespace Frames");

            Graph
                .AddNode("Frame Transforms")
                .SetLabel(Graph.Table("Transform", "Transforms".Bold()))
                .AddEdgeFrom("Namespace Frames");


            //Graph
            //    .AddNode("Namespace Structures Data Members")
            //    .SetLabel("Data Members".Bold())
            //    .AddEdgeFrom("Namespace Structures");

            //Graph
            //    .AddNode("Frame Structures Data Members")
            //    .SetLabel("Data Members".Bold())
            //    .AddEdgeFrom("Frame Structures");


            //Graph
            //    .AddNode("Macro")
            //    .SetLabel(Graph.Table("Macro", "Macro".Bold()));

            //Graph
            //    .AddNode("Macro Parameters")
            //    .SetLabel("Macro Parameters".Bold())
            //    .AddEdgeFrom("Macro");

            //Graph
            //    .AddNode("Macro Body")
            //    .SetLabel("Macro Body Command Block".Bold())
            //    .AddEdgeFrom("Macro");

            //Graph
            //    .AddNode("Macro Body Local Variables")
            //    .SetLabel("Local Variables".Bold())
            //    .AddEdgeFrom("Macro Body");

            //Graph
            //    .AddNode("Macro Body Commands")
            //    .SetLabel("Commands".Bold())
            //    .AddEdgeFrom("Macro Body");


            return Graph;
        }
    }
}
