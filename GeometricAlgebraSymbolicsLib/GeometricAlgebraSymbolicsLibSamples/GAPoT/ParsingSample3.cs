using System;
using CodeComposerLib.Irony;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using TextComposerLib.Text.Markdown;

namespace GeometricAlgebraSymbolicsLibSamples.GAPoT
{
    public static class ParsingSample3
    {
        public static void Execute()
        {
            //Examples:
            //GAPoT bivector using terms form:
            //  '-1.3'<>, '1.2'<1,2>, '-4.6'<3,4>

            var sourceText =
                "'-1.3'<>, '1.2'<1,2>, '-4.6'<3,4>";

            var parsingResults = new IronyParsingResults(
                new GaPoTSymBiversorConstructorGrammar(), 
                sourceText
            );

            var bivector = sourceText.GaPoTSymParseBiversor();

            var composer = new MarkdownComposer();

            composer
                .AppendLine(parsingResults.ToString());

            if (!parsingResults.ContainsErrorMessages && bivector != null)
            {
                composer
                    .AppendLine()
                    .AppendLine(bivector.TermsToText())
                    .AppendLine()
                    .AppendLine(bivector.TermsToLaTeX());
            }

            Console.WriteLine(composer.ToString());
        }
    }
}