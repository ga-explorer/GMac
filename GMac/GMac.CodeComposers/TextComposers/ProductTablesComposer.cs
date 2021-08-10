using System;
using System.Linq;
using CodeComposerLib.MathML;
using CodeComposerLib.MathML.Composers;
using CodeComposerLib.MathML.Elements;
using CodeComposerLib.MathML.Elements.Layout.Elementary;
using CodeComposerLib.MathML.Elements.Layout.Tabular;
using CodeComposerLib.MathML.Elements.Tokens;
using DataStructuresLib;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraStructuresLib.Frames;
using TextComposerLib.Text.Linear;

namespace GMac.CodeComposers.TextComposers
{
    public sealed class ProductTablesComposer
    {
        public static IMathMlElement[] CreateBasisBladeNames(params string[] basisVectorSubscripts)
        {
            var gaSpaceDim = basisVectorSubscripts.Length.ToGaSpaceDimension();

            var result = basisVectorSubscripts
                .ConcatenateUsingPatterns(
                    Enumerable.Range(0, (int)gaSpaceDim),
                    ""
                )
                .Select(n => "e".ToMathMlSubscript(n) as IMathMlElement)
                .ToArray();

            result[0] = 1.ToMathMlNumber();

            return result;
        }

        public static string ComposeHga4D()
        {
            var frame = GaNumFrame.CreateEuclidean(4);

            var basisBladeNames = CreateBasisBladeNames("x", "y", "z", "w");

            return ComposeMathMlTableColumns(frame, id => basisBladeNames[id]);
        }


        private static MathMlRow ToMathMlRow(IGaNumMultivector mv, Func<ulong, IMathMlElement> getBasisBladeNameFunc)
        {
            var rowElement = MathMlRow.Create();

            var termsList =
                mv.GetNonZeroTerms()
                    .OrderBy(t => t.BasisBladeId.BasisBladeGrade())
                    .ThenBy(t => t.BasisBladeId);

            var firstItemFlag = true;
            foreach (var term in termsList)
            {
                var basisBladeName = getBasisBladeNameFunc(term.BasisBladeId);

                if (firstItemFlag)
                {
                    if (term.ScalarValue == 1.0d)
                        rowElement.Append(basisBladeName);

                    else if (term.ScalarValue == -1.0d)
                        rowElement.AppendElements(
                            MathMlOperator.MinusSign,
                            basisBladeName
                        );

                    else if (term.ScalarValue > 0 || term.ScalarValue < 0)
                        rowElement.AppendElements(
                            term.ScalarValue.ToMathMlNumber(),
                            basisBladeName
                        );

                    firstItemFlag = false;
                    continue;
                }

                if (term.ScalarValue == 1.0d)
                    rowElement.AppendElements(
                        MathMlOperator.PlusSign,
                        basisBladeName
                    );

                else if (term.ScalarValue == -1.0d)
                    rowElement.AppendElements(
                        MathMlOperator.MinusSign,
                        basisBladeName
                    );

                else if (term.ScalarValue > 0)
                    rowElement.AppendElements(
                        MathMlOperator.PlusSign,
                        term.ScalarValue.ToMathMlNumber(),
                        basisBladeName
                    );

                else if (term.ScalarValue < 0)
                    rowElement.AppendElements(
                        MathMlOperator.MinusSign,
                        (-term.ScalarValue).ToMathMlNumber(),
                        basisBladeName
                    );
            }

            return rowElement;
        }

        public static MathMlTable ComposeMathMlTable(GaNumFrame frame, Func<ulong, IMathMlElement> getBasisBladeNameFunc)
        {
            var idsList = frame.BasisBladeIDsSortedByGrade().ToArray();
            var gaDim = idsList.Length;

            var table = MathMlTable.Create(
                gaDim + 2, 
                gaDim + 2
            );

            for (var i = 0; i < gaDim; i++)
            {
                var id1 = idsList[i];
                var grade1 = id1.BasisBladeGrade().ToMathMlNumber();
                var name1 = getBasisBladeNameFunc(id1);

                table[i + 2, 0].Append(grade1);
                table[i + 2, 1].Append(name1);

                table[0, i + 2].Append(grade1);
                table[1, i + 2].Append(name1);

                for (var j = 0; j < gaDim; j++)
                {
                    var id2 = idsList[j];

                    var mv = frame.Gp[id1, id2];

                    table[i + 2, j + 2].Append(
                        ToMathMlRow(mv, getBasisBladeNameFunc)
                    );
                }
            }

            return table;
        }

        public static string ComposeMathMlTableColumns(GaNumFrame frame, Func<ulong, IMathMlElement> getBasisBladeNameFunc)
        {
            var textComposer = new LinearTextComposer();

            var idsList = frame.BasisBladeIDsSortedByGrade().ToArray();
            var gaDim = idsList.Length;

            for (var i = 0; i < gaDim; i++)
            {
                var id1 = idsList[i];
                var name1 = getBasisBladeNameFunc(id1);

                textComposer.AppendAtNewLine(
                    MathMlCodeComposer.ComposeCode(name1,true)
                );
            }

            textComposer
                .AppendLineAtNewLine()
                .AppendLineAtNewLine();

            for (var i = 0; i < gaDim; i++)
            {
                var id1 = idsList[i];

                for (var j = 0; j < gaDim; j++)
                {
                    var id2 = idsList[j];

                    var mv = frame.Gp[id1, id2];
                    var mvName = ToMathMlRow(mv, getBasisBladeNameFunc);

                    textComposer.AppendAtNewLine(
                        MathMlCodeComposer.ComposeCode(
                            (IMathMlElement)mvName, 
                            true
                        )
                    );
                }

                textComposer
                    .AppendLineAtNewLine()
                    .AppendLineAtNewLine();
            }

            return textComposer.ToString();
        }
    }
}
