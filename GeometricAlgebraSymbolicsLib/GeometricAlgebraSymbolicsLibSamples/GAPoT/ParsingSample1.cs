using System;
using CodeComposerLib.Irony;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using TextComposerLib.Text.Markdown;

namespace GeometricAlgebraSymbolicsLibSamples.GAPoT
{
    public static class ParsingSample1
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
                @"r('Subscript[x,1,2]', 'Subscript[y,1,2]') <1,2>, 'Subscript[t,1]'<1>, r('Subscript[R,3,4]', '0') <3,4>, 'Subscript[t,3]'<3>, p('Subscript[R,1,2]', 'Subscript[\[Theta],1,2]') <1,2>, p('Subscript[R,3,4]', '0') <3,4>";

            var parsingResults = new IronyParsingResults(
                new GaPoTSymVectorConstructorGrammar(), 
                sourceText
            );

            var spVector = sourceText.GaPoTSymParseVector();

            var composer = new MarkdownComposer();

            composer
                .AppendLine(parsingResults.ToString());

            if (!parsingResults.ContainsErrorMessages && spVector != null)
            {
                composer
                    .AppendLine()
                    .AppendLine(spVector.TermsToText())
                    .AppendLine()
                    .AppendLine(spVector.TermsToLaTeX())
                    .AppendLine()
                    .AppendLine(spVector.PolarPhasorsToText())
                    .AppendLine()
                    .AppendLine(spVector.PolarPhasorsToLaTeX())
                    .AppendLine()
                    .AppendLine(spVector.RectPhasorsToText())
                    .AppendLine()
                    .AppendLine(spVector.RectPhasorsToLaTeX());
            }

            Console.WriteLine(composer.ToString());
        }
    }
}