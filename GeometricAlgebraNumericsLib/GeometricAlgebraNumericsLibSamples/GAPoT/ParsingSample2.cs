using System;
using CodeComposerLib.Irony;
using GeometricAlgebraNumericsLib.Applications.GAPoT;
using TextComposerLib.Text.Markdown;

namespace GeometricAlgebraNumericsLibSamples.GAPoT
{
    public static class ParsingSample2
    {
        public static void Execute()
        {
            //Examples:
            //Single Phase GAPoT vector using terms form:
            //  -1.3<1>, 1.2<3>, -4.6<5>
            //
            //Single Phase GAPoT vector using polar form:
            //  p(233.92, −1.57) <1,2>, p(120, 0) <3,4>
            //
            //Single Phase GAPoT vector using rectangular form:
            //  r(10, 20) <1,2>, r(30, 0) <3,4>
            //
            //All the above can be mixed together
            //
            //Multi-phase GAPoT vector:
            //  [-1.3<1>, 1.2<3>, -4.6<5>] <a>; [p(233.92, −1.57) <1,2>] <b>; [r(10, 20) <1,2>, r(30, 0) <3,4>] <c>

            var sourceText =
                "[-1.3<1>, 1.2<3>, -4.6<5>] <a>; [p(233.92, −1.57) <1,2>] <b>; [r(10, 20) <1,2>, r(30, 0) <3,4>] <c>";

            var parsingResults = new IronyParsingResults(
                new GaPoTNumVectorConstructorGrammar(), 
                sourceText
            );

            var mpVector = sourceText.GaPoTNumParseMultiPhaseVector();

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