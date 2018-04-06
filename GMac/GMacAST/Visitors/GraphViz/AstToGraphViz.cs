using System.Collections.Generic;
using System.Linq;
using GMac.GMacAST.Symbols;
using TextComposerLib;
using TextComposerLib.Diagrams.GraphViz.Dot;
using TextComposerLib.Diagrams.GraphViz.Dot.Value;

namespace GMac.GMacAST.Visitors.GraphViz
{
    public sealed class AstToGraphViz : GraphVizConverter
    {
        private readonly List<DotSubGraph> _namespacesSubGraphs = new List<DotSubGraph>();

        private DotSubGraph _framesSubGraph;

        private DotSubGraph _basisVectorsSubGraph;

        private DotSubGraph _subspacesSubGraph;

        private DotSubGraph _structsSubGraph;

        private DotSubGraph _constsSubGraph;

        private DotSubGraph _macrosSubGraph;


        private void AddConstants(string parentNode, IEnumerable<AstConstant> constants)
        {
            var constsArray = constants.Select(m => m.Name).ToArray();

            if (constsArray.Length == 0) return;

            Graph
            .AddNode(parentNode + " Constants")
            .SetLabel(Graph.Table("Constant", "Constants", constsArray))
            .AddEdgeFrom(parentNode);

            _constsSubGraph.AddNode(parentNode + " Constants");
        }

        private void AddStructures(string parentNode, IEnumerable<AstStructure> structures)
        {
            var structsArray = structures.Select(m => m.Name).ToArray();

            if (structsArray.Length == 0) return;

            Graph
            .AddNode(parentNode + " Structures")
            .SetLabel(Graph.Table("Structure", "Structures", structsArray))
            .AddEdgeFrom(parentNode);

            _structsSubGraph.AddNode(parentNode + " Structures");
        }

        private void AddMacros(string parentNode, IEnumerable<AstMacro> macros)
        {
            var macrosArray = macros.Select(m => m.Name).ToArray();

            if (macrosArray.Length == 0) return;

            Graph
            .AddNode(parentNode + " Macros")
            .SetLabel(Graph.Table("Macro", "Macros", macrosArray))
            .AddEdgeFrom(parentNode);

            _macrosSubGraph.AddNode(parentNode + " Macros");
        }

        private void AddBasisVectors(string parentNode, IEnumerable<AstFrameBasisVector> basisVectors)
        {
            var vectorsArray = basisVectors.Select(m => m.Name).ToArray();

            if (vectorsArray.Length == 0) return;

            Graph
            .AddNode(parentNode + " Basis Vectors")
            .SetLabel(Graph.Table("BasisVector", "Basis Vectors", vectorsArray))
            .AddEdgeFrom(parentNode);

            _basisVectorsSubGraph.AddNode(parentNode + " Basis Vectors");
        }

        private void AddSubspaces(string parentNode, IEnumerable<AstFrameSubspace> subspaces)
        {
            var subspacesArray = subspaces.Select(m => m.Name).ToArray();

            if (subspacesArray.Length == 0) return;

            Graph
            .AddNode(parentNode + " Subspaces")
            .SetLabel(Graph.Table("Subspace", "Subspaces", subspacesArray))
            .AddEdgeFrom(parentNode);

            _subspacesSubGraph.AddNode(parentNode + " Subspaces");
        }

        private void AddRankingSubGraphs(AstRoot ast)
        {
            _namespacesSubGraphs.Clear();

            var maxNamespaceDepth =
                1 + ast.Namespaces.Select(n => n.ParentSymbolsCount).Max();

            Graph
                .AddSubGraph()
                .AddEdge(
                    Enumerable
                    .Range(1, maxNamespaceDepth)
                    .Select(n => "Namespaces " + n)
                )
                .AddSides(
                    "Frames",
                    "Basis Vectors",
                    "Structures",
                    "Subspaces",
                    "Constants",
                    "Macros"
                    )
                .SetPenWidth(0)
                .SetArrowSize(0);

            for (var i = 1; i <= maxNamespaceDepth; i++)
            {
                var subGraph =
                    Graph
                    .AddSubGraph("GMacAST Namespaces " + i)
                    .SetRank(DotRankType.Same);

                subGraph
                .AddNode("Namespaces " + i)
                .SetLabel(
                    Graph.Table("Namespace", "Namespaces " + i)
                    );

                _namespacesSubGraphs.Add(subGraph);
            }

            _framesSubGraph =
                Graph
                .AddSubGraph("GMacAST Frames")
                .SetRank(DotRankType.Same);

            _basisVectorsSubGraph =
                Graph
                .AddSubGraph("GMacAST Basis Vectors")
                .SetRank(DotRankType.Same);

            _subspacesSubGraph =
                Graph
                .AddSubGraph("GMacAST Subspaces")
                .SetRank(DotRankType.Same);

            _constsSubGraph =
                Graph
                .AddSubGraph("GMacAST Constants")
                .SetRank(DotRankType.Same);

            _structsSubGraph =
                Graph
                .AddSubGraph("GMacAST Structures")
                .SetRank(DotRankType.Same);

            _macrosSubGraph =
                Graph
                .AddSubGraph("GMacAST Macros")
                .SetRank(DotRankType.Same);

            _framesSubGraph.AddNode("Frames").SetLabel(Graph.Table("Frame", "Frames"));
            _basisVectorsSubGraph.AddNode("Basis Vectors").SetLabel(Graph.Table("BasisVector", "Basis Vectors"));
            _subspacesSubGraph.AddNode("Subspaces").SetLabel(Graph.Table("Subspace", "Subspaces"));
            _constsSubGraph.AddNode("Subspaces").SetLabel(Graph.Table("Constant", "Constants"));
            _structsSubGraph.AddNode("Subspaces").SetLabel(Graph.Table("Structure", "Structures"));
            _macrosSubGraph.AddNode("Macros").SetLabel(Graph.Table("Macro", "Macros"));
        }


        public void Visit(AstFrame frame)
        {
            Graph
            .AddNode(frame.AccessName)
            .SetLabel(Graph.Table("Frame", frame.Name))
            .AddEdgeFrom(frame.ParentNamespace.AccessName);

            AddBasisVectors(frame.AccessName, frame.BasisVectors);

            AddSubspaces(frame.AccessName, frame.Subspaces);

            AddConstants(frame.AccessName, frame.Constants);

            AddMacros(frame.AccessName, frame.Macros);

            AddStructures(frame.AccessName, frame.Structures);


            _framesSubGraph.AddNode(frame.AccessName);
        }

        public void Visit(AstNamespace nameSpace)
        {
            Graph
            .AddNode(nameSpace.AccessName)
            .SetLabel(Graph.Table("Namespace", nameSpace.Name))
            .AddEdgeFrom(
                nameSpace.HasParentNamespace
                ? nameSpace.ParentNamespace.AccessName
                : "GMacAST Root"
                );

            AddConstants(nameSpace.AccessName, nameSpace.ChildConstants);

            AddStructures(nameSpace.AccessName, nameSpace.ChildStructures);

            AddMacros(nameSpace.AccessName, nameSpace.ChildMacros);

            foreach (var child in nameSpace.ChildNamespaces)
                child.AcceptVisitor(this);

            foreach (var child in nameSpace.ChildFrames)
                child.AcceptVisitor(this);


            var n = nameSpace.ParentSymbolsCount;

            _namespacesSubGraphs[n].AddNode(nameSpace.AccessName);
        }

        public void Visit(AstRoot ast)
        {
            //Graph = AstVisitorDotGraph.Create();

            AddRankingSubGraphs(ast);

            Graph
            .AddNode("GMacAST Root")
            .SetLabel(
                Graph.Table("GMacAST", "GMacAst")
                );
            
            foreach (var nameSpace in ast.ChildNamespaces)
                nameSpace.AcceptVisitor(this);
        }
    }
}
