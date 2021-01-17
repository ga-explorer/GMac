using System;
using CodeComposerLib.Irony;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using TextComposerLib.Text.Markdown;

namespace GeometricAlgebraSymbolicsLibSamples.GAPoT
{
    public static class ParsingSample2
    {
        public static void Execute()
        {
            //Examples:
            //GAPoT vector using terms form:
            //  '-1.3'<1>, '1.2'<3>, '-4.6'<5>
            //
            //GAPoT vector using polar form:
            //  p('233.92', '−1.57') <1,2>, p('120', '0') <3,4>
            //
            //GAPoT vector using rectangular form:
            //  r('10', '20') <1,2>, r('30', '0') <3,4>

            var sourceText =
                "'-1.3'<1>, '1.2'<3>, '-4.6'<5>, p('233.92', '−1.57') <7,8>, r('10', '20') <9,10>, r('30', '0') <11,12>";

            var parsingResults = new IronyParsingResults(
                new GaPoTSymVectorConstructorGrammar(), 
                sourceText
            );

            var mpVector = sourceText.GaPoTSymParseVector();

            var composer = new MarkdownComposer();

            composer
                .AppendLine(parsingResults.ToString());

            if (!parsingResults.ContainsErrorMessages && mpVector != null)
            {
                composer
                    .AppendLine()
                    .AppendLine(mpVector.TermsToText())
                    .AppendLine()
                    .AppendLine(mpVector.TermsToLaTeX())
                    .AppendLine()
                    .AppendLine(mpVector.PolarPhasorsToText())
                    .AppendLine()
                    .AppendLine(mpVector.PolarPhasorsToLaTeX())
                    .AppendLine()
                    .AppendLine(mpVector.RectPhasorsToText())
                    .AppendLine()
                    .AppendLine(mpVector.RectPhasorsToLaTeX());
            }

            Console.WriteLine(composer.ToString());
        }
    }
}