using CodeComposerLib.Irony;
using GeometricAlgebraNumericsLib.Applications.GAPoT;
using GMacBenchmarks2.Samples.Computations;
using TextComposerLib.Text.Linear;

namespace GMacBenchmarks2.Samples.Parsing.GAPoT
{
    public sealed class Sample1 : IGMacSample
    {
        public string Title { get; }
            = "Parse GAPoT expression to construct a multivector";

        public string Description { get; }
            = "Parse GAPoT expression to construct a multivector";


        public string Execute()
        {
            var sourceText =
                "(1.2<1>, -4.6<3>)<a>; (-9.3<1>, 12.4<2>, -14.2<4>)<b>";

            var parsingResults =
                new IronyParsingResults(
                    new GaPoTNumVectorConstructorGrammar(), 
                    sourceText
                );

            var mv = 
                sourceText.GaPoTNumParseSinglePhaseVector();

            var composer = new LinearTextComposer();

            composer
                .AppendLine(parsingResults)
                .AppendLine()
                .AppendLine(mv);

            return composer.ToString();
        }
    }
}
