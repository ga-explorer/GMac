using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CodeComposerLib.GraphViz.Dot;
using CodeComposerLib.GraphViz.Dot.Label;
using CodeComposerLib.GraphViz.Dot.Label.Table;
using CodeComposerLib.GraphViz.Dot.Value;
using TextComposerLib.Text.Parametric;

namespace GMac.GMacAST.Visitors.GraphViz
{
    public sealed class AstVisitorDotGraph : DotGraph
    {
        private static StringSequenceTemplate _nodeNamesTemplate;

        public string IconsPath { get; }


        internal AstVisitorDotGraph()
            : base(DotGraphType.Directed, string.Empty)
        {
            IconsPath = Path.Combine(
                Path.GetDirectoryName(Application.ExecutablePath) ?? "", 
                "Icons\\64"
                );

            _nodeNamesTemplate = new StringSequenceTemplate() { NameParamValue = "gvNode" };
        }


        public string NewNodeName()
        {
            return
                _nodeNamesTemplate.GenerateNextString();
        }

        public string SelectIconName(AstType astType)
        {
            if (astType.IsValidScalarType)
                return "Scalar";

            if (astType.IsValidMultivectorType)
                return "Frame";

            if (astType.IsValidStructureType)
                return "Structure";

            return string.Empty;
        }


        public DotHtmlTable SimpleTable(IEnumerable<string> tableContents)
        {
            var table =
                DotUtils.Table()
                .SetBorder(0);

            //Add a row with a single cell for each entry
            table.AddRows(tableContents);

            //Set common attributes of added cells
            foreach (var row in table.Skip(1))
                row.FirstCell
                    .SetBorder(1)
                    .SetSides(DotSides.Top);

            return table;
        }

        public DotHtmlTable SimpleTable(params string[] tableContents)
        {
            return SimpleTable((IEnumerable<string>)tableContents);
        }

        public DotHtmlTable SimpleTable(IEnumerable<KeyValuePair<string, IDotHtmlLabel>> tableContents)
        {
            var table =
                DotUtils.Table()
                .SetBorder(0);

            //Add a row with a single cell and port name for each entry
            table
                .AddRows(
                    tableContents.Select(
                        item =>
                            DotUtils.Cell()
                            .SetContents(item.Value)
                            .SetPort(item.Key)
                        )
                    );

            //Set common attributes of added cells
            foreach (var row in table.Skip(1))
                row.FirstCell
                    .SetBorder(1)
                    .SetSides(DotSides.Top);

            return table;
        }

        public DotHtmlTable SimpleTable(IEnumerable<KeyValuePair<string, string>> tableContents)
        {
            var table =
                DotUtils.Table()
                .SetBorder(0);

            //Add a row with a single cell and port name for each entry
            table
                .AddRows(
                    tableContents.Select(
                        item => 
                            DotUtils.Cell()
                            .SetContents(item.Value)
                            .SetPort(item.Key)
                        )
                    );

            //Set common attributes of added cells
            foreach (var row in table.Skip(1))
                row.FirstCell
                    .SetBorder(1)
                    .SetSides(DotSides.Top);

            return table;
        }

        public DotHtmlTable Table(string iconName, IDotHtmlLabel titleContents)
        {
            var table =
                DotUtils.Table()
                .SetBorder(0);

            table.AddRow(
                DotUtils.ImageCell(Path.Combine(IconsPath, iconName + "64.png")),
                DotUtils.Cell().SetContents(titleContents)
                );

            return table;
        }

        public DotHtmlTable Table(string iconName, IDotHtmlLabel titleContents, IDotHtmlLabel tableContents)
        {
            var table =
                DotUtils.Table()
                .SetBorder(0);

            table.AddRow(
                DotUtils.ImageCell(Path.Combine(IconsPath, iconName + "64.png")),
                DotUtils.Cell().SetContents(titleContents)
                );

            table
                .AddRow(tableContents)
                .FirstCell
                .SetColumnSpan(2)
                .SetBorder(2)
                .SetSides(DotSides.Top);

            return table;
        }

        public DotHtmlTable Table(string iconName, IDotHtmlLabel titleContents, IEnumerable<string> tableContents)
        {
            return Table(
                iconName,
                titleContents,
                SimpleTable(tableContents)
                );
        }

        public DotHtmlTable Table(string iconName, IDotHtmlLabel titleContents, string tableContents)
        {
            return Table(
                iconName,
                titleContents,
                tableContents.ToHtmlString()
                );
        }

        public DotHtmlTable Table(string iconName, string titleContents)
        {
            return Table(
                iconName,
                titleContents.ToHtmlString()
                );
        }

        public DotHtmlTable Table(string iconName, string titleContents, IDotHtmlLabel tableContents)
        {
            return Table(
                iconName,
                titleContents.ToHtmlString(),
                tableContents
                );
        }

        public DotHtmlTable Table(string iconName, string titleContents, IEnumerable<string> tableContents)
        {
            return Table(
                iconName,
                titleContents.ToHtmlString(),
                SimpleTable(tableContents)
                );
        }

        public DotHtmlTable Table(string iconName, string titleContents, string tableContents)
        {
            return Table(
                iconName,
                titleContents.ToHtmlString(),
                tableContents.ToHtmlString()
                );
        }


    }
}
