using System;
using System.Collections.Generic;
using System.Linq;
using GMac.GMacAST.Commands;
using GMac.GMacAST.Dependency;
using GMac.GMacAST.Symbols;
using Microsoft.CSharp.RuntimeBinder;
using TextComposerLib.Text;
using TextComposerLib.Text.Tabular.Columns;

namespace GMac.GMacAST.Visitors
{
    public sealed class GMacAstDescriptionVisitor : IAstObjectDynamicVisitor<string>
    {
        public AstRoot Root { get; }

        private readonly AstTypeDependencyGraph _typeDepGraph;

        private readonly AstMacroDependencyGraph _macroDepGraph;


        public bool IgnoreNullElements => false;

        public bool UseExceptions => false;


        public GMacAstDescriptionVisitor(AstRoot root)
        {
            Root = root;
            _typeDepGraph = Root.GetTypeDependencies();
            _macroDepGraph = Root.GetMacroDependencies();
        }


        public string Fallback(IAstObject item, RuntimeBinderException excException)
        {
            if (ReferenceEquals(item, null))
                return "Null GMacAST Node!";

            return "Unhandled Node Object: " + item;
        }


        private static TextColumnsComposer CreateColumnsComposer()
        {
            var composer = new TextColumnsComposer(2)
            {
                ColumnSeparator = " ",
                DefaultRowAlignment = TextRowAlignment.Top,
                DefaultColumnAlignment = TextColumnAlignment.Left
            };

            composer.SetColumnAlignment(TextColumnAlignment.Right, 0);

            return composer;
        }

        private static string BasisBladesInformationTable(IEnumerable<AstFrameBasisBlade> basisBladesList)
        {
            var composer = new TextColumnsComposer(7)
            {
                ColumnSeparator = " ",
                DefaultRowAlignment = TextRowAlignment.Top,
                DefaultColumnAlignment = TextColumnAlignment.Left
            };


            composer.AppendToColumns("Grade", "Index", "ID", "Name", "Indexed Name", "Binary Name", "Grade+Index Name");
            composer.AppendToColumns("-----", "-----", "--", "----", "------------", "-----------", "----------------");

            foreach (var basisBlade in basisBladesList)
                composer.AppendToColumns(
                    basisBlade.Grade.ToString(),
                    basisBlade.Index.ToString(),
                    basisBlade.BasisBladeId.ToString(),
                    basisBlade.Name,
                    basisBlade.IndexedName,
                    basisBlade.BinaryIndexedName,
                    basisBlade.GradeIndexName
                    );

            return composer.GenerateText();
        }


        public string Visit(AstFrameMultivector item)
        {
            var composer = CreateColumnsComposer();

            composer
            .AppendToColumns("Multivector Type Name:", item.AccessName)
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Basis Blades:",
                item
                    .ParentFrame
                    .BasisBlades()
                    .OrderBy(b => b.Grade)
                    .ThenBy(b => b.Index)
                    .Select(t => t.Name)
                    .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Types depending on this multivector type:",
                _typeDepGraph
                .GetAllUserItems(item.GMacType)
                .Select(d => d.BaseItem.GMacTypeSignature)
                .Concatenate(Environment.NewLine)
                );

            return composer.GenerateText();
        }

        public string Visit(AstFrameSubspace item)
        {
            var composer = CreateColumnsComposer();

            composer
            .AppendToColumns("Subspace Name:", item.AccessName)
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Basis Blades:",
                BasisBladesInformationTable(item.BasisBlades.OrderBy(b => b.Grade).ThenBy(b => b.Index))
                );

            return composer.GenerateText();
        }

        public string Visit(AstFrame item)
        {
            var composer = CreateColumnsComposer();

            composer
            .AppendToColumns("Frame Name:", item.AccessName)
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Basis Vectors:",
                item
                    .BasisVectors
                    .Select(t => t.Name)
                    .Concatenate(", ")
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Basis Blades:",
                BasisBladesInformationTable(item.BasisBladesSortedByGrade())
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Subspaces:",
                item
                    .Subspaces
                    .Select(t => t.Name)
                    .Concatenate(Environment.NewLine)
                );

            return composer.GenerateText();
        }

        public string Visit(AstStructure item)
        {
            var composer = CreateColumnsComposer();

            composer
            .AppendToColumns("Structure Name:", item.AccessName)
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Data Members:",
                item
                    .DataMembers
                    .Select(t => t.AccessName + " : " + t.GMacTypeSignature)
                    .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Types this structure depends on:",
                _typeDepGraph
                .GetAllUsedItems(item.GMacType)
                .Select(d => d.BaseItem.GMacTypeSignature)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Types depending on this structure:",
                _typeDepGraph
                .GetAllUserItems(item.GMacType)
                .Select(d => d.BaseItem.GMacTypeSignature)
                .Concatenate(Environment.NewLine)
                );

            return composer.GenerateText();
        }

        public string Visit(AstCommandBlock item)
        {
            return item.ToString();
        }

        public string Visit(AstMacro item)
        {
            var composer = CreateColumnsComposer();

            composer
            .AppendToColumns("Macro Name:", item.AccessName)
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Output Parameter:",
                item.OutputParameterName + " : " + item.OutputParameter.GMacTypeSignature
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Input Parameters:",
                item
                    .InputParameters
                    .Select(t => t.AccessName + " : " + t.GMacTypeSignature)
                    .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Macros calling this macro:",
                _macroDepGraph
                .GetAllUserItems(item)
                .Select(d => d.BaseItem.AccessName)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Macros called by this macro:",
                _macroDepGraph
                .GetAllUsedItems(item)
                .Select(d => d.BaseItem.AccessName)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Main Command Block:",
                Visit(item.CommandBlock)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Compiled Command Block:",
                Visit(item.CompiledCommandBlock)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Optimized Command Block:",
                Visit(item.OptimizedCommandBlock)
                );

            return composer.GenerateText();
        }

        public string Visit(AstConstant item)
        {
            var composer = CreateColumnsComposer();

            composer
            .AppendToColumns("Constant Name:", item.AccessName);
            

            return composer.GenerateText();
        }

        public string Visit(AstNamespace item)
        {
            var composer = CreateColumnsComposer();

            composer
            .AppendToColumns("Namespace Name:", item.AccessName)
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Namespaces:",
                item
                .Namespaces
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Frames:",
                item
                .Frames
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Multivector Types:",
                item
                .FrameMultivectors
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Subspaces:",
                item
                .Subspaces
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Structures:",
                item
                .Structures
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Constants:",
                item
                .Constants
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Transforms:",
                item
                .Transforms
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Macros:",
                item
                .Macros
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                );

            return composer.GenerateText();
        }

        public string Visit(AstRoot item)
        {
            var composer = CreateColumnsComposer();

            composer
            .AppendToColumns("GMacAST Root", "")
            .AppendToColumns("------------", "")
            .AppendEmptyStringsToColumns()
            .AppendToColumns("Scalar Type:", item.ScalarType.GMacTypeSignature)
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Namespaces:", 
                item
                .Namespaces
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Frames:",
                item
                .Frames
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Multivector Types:",
                item
                .FrameMultivectors
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Subspaces:",
                item
                .Subspaces
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Structures:",
                item
                .Structures
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Constants:",
                item
                .Constants
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Transforms:",
                item
                .Transforms
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                )
            .AppendEmptyStringsToColumns()
            .AppendToColumns(
                "Macros:",
                item
                .Macros
                .Select(t => t.AccessName)
                .OrderBy(t => t)
                .Concatenate(Environment.NewLine)
                );

            return composer.GenerateText();
        }
    }
}
