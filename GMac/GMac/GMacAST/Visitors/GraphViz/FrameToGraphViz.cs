using System.Linq;
using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.GraphViz.Dot.Value;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Frames;
using GMac.GMacAST.Symbols;


namespace GMac.GMacAST.Visitors.GraphViz
{
    public sealed class FrameToGraphViz : AstToGraphVizConverter
    {
        private DotNode AddIpm(AstFrame frame)
        {
            var innerTable =
                DotUtils.Table()
                .AddRows(frame.VSpaceDimension + 1, frame.VSpaceDimension + 1)
                .SetBorder(0);

            var vectorsNames = 
                frame.BasisVectors.Select(item => item.Name).ToArray();

            var ipm = frame.GetInnerProductMatrix();

            for (var row = 0; row < frame.VSpaceDimension; row++)
            {
                innerTable[row + 1][0].SetContents(vectorsNames[row]);
                innerTable[0][row + 1].SetContents(vectorsNames[row]);

                for (var col = 0; col < frame.VSpaceDimension; col++)
                    innerTable[row + 1][col + 1].SetContents(ipm[row, col].ToString());
            }

            var table =
                DotUtils.Table()
                .SetBorder(0);

            table.AddRows(
                DotUtils.Cell().SetContents("Inner Product Matrix".Bold()),
                DotUtils.Cell().SetContents(innerTable)
                );

            var node =
                Graph
                .AddNode("Inner Product Matrix")
                .SetLabel(table);

            Graph.AddEdge(frame.AccessName, node.NodeName);

            return node;
        }

        private DotNode AddBasisBlades(AstFrame frame)
        {
            var innerTable =
                DotUtils.Table()
                .SetBorder(0);

            innerTable
                .AddRow()
                .AddCells("Grade", "Index", "ID", "Name", "Indexed Name", "Binary Name", "Grade + Index Name");

            foreach (var cell in innerTable.FirstRow)
                cell
                    .SetBorder(1)
                    .SetSides(DotSides.Top, DotSides.Bottom);

            for (var grade = 0; grade <= frame.VSpaceDimension; grade++)
            {
                var n = frame.KvSpaceDimension(grade);

                for (var index = 0; index < n; index++)
                {
                    var basisBlade = frame.BasisBlade(grade, index);

                    innerTable
                    .AddRow()
                    .AddCells(
                        basisBlade.Grade.ToString(),
                        basisBlade.Index.ToString(),
                        basisBlade.BasisBladeId.ToString(),
                        basisBlade.Name,
                        basisBlade.IndexedName,
                        basisBlade.BinaryIndexedName,
                        basisBlade.GradeIndexName
                    );
                }
            }

            var table =
                DotUtils.Table()
                .SetBorder(0);

            table.AddRows(
                DotUtils.Cell().SetContents("Basis Blades".Bold()),
                DotUtils.Cell().SetContents(innerTable)
                );

            var node =
                Graph
                .AddNode("Basis Blades")
                .SetLabel(table);

            Graph.AddEdge("Inner Product Matrix", "Basis Blades");

            return node;
        }


        public void Visit(AstFrameSubspace subspace)
        {
            Graph
            .AddNode("Subspace " + subspace.Name)
            .SetLabel(
                Graph.Table(
                    "Subspace", 
                    "Subspace: " + subspace.Name, 
                    subspace.BasisBlades.Select(item => item.Name).ToArray()
                    )
                )
            .AddEdgeFrom("Basis Blades");
        }

        public void Visit(AstFrame frame)
        {
            //Graph = AstVisitorDotGraph.Create();

            Graph
            .AddNode(frame.AccessName)
            .SetLabel(
                Graph.Table("Frame", frame.AccessName)
                );

            AddIpm(frame);

            AddBasisBlades(frame);

            foreach (var subspace in frame.Subspaces)
                subspace.AcceptVisitor(this);
        }
    }
}
