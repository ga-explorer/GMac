using System;
using System.Data;
using System.Linq;
using CodeComposerLib.Irony;
using Irony.Parsing;

namespace GeometricAlgebraNumericsLib.Applications.GAPoT
{
    public static class GaPoTNumUtils
    {
        public static int LaTeXDecimalPlaces { get; set; }
            = 4;

        public static string GetLaTeXBasisName(this string basisSubscript)
        {
            //return $@"\boldsymbol{{\sigma_{{{basisSubscript}}}}}";
            return $@"\sigma_{{{basisSubscript}}}";
        }

        public static string GetLaTeXNumber(this double value)
        {
            var valueText = value.ToString("G");

            if (valueText.Contains('E'))
            {
                var ePosition = valueText.IndexOf('E');

                var magnitude = Double.Parse(valueText.Substring(0, ePosition));
                var magnitudeText = Math.Round(magnitude, LaTeXDecimalPlaces).ToString("G");
                var exponentText = valueText.Substring(ePosition + 1);

                return $@"{magnitudeText} \times 10^{{{exponentText}}}";
            }

            return Math.Round(value, LaTeXDecimalPlaces).ToString("G");
        }


        private static GaPoTNumSinglePhaseVector GaPoTNumParseSinglePhaseVector(IronyParsingResults parsingResults, ParseTreeNode rootNode)
        {
            if (rootNode.ToString() != "spVector")
                throw new SyntaxErrorException(parsingResults.ToString());

            var vector = new GaPoTNumSinglePhaseVector();

            var vectorNode = rootNode;
            foreach (var vectorElementNode in vectorNode.ChildNodes)
            {
                if (vectorElementNode.ToString() == "spTerm")
                {
                    //Term Form
                    var value = Double.Parse(vectorElementNode.ChildNodes[0].FindTokenAndGetText());
                    var id = Int32.Parse(vectorElementNode.ChildNodes[1].FindTokenAndGetText()) - 1;

                    if (id < 0)
                        throw new SyntaxErrorException(parsingResults.ToString());

                    vector.AddTerm(id, value);
                }
                else if (vectorElementNode.ToString() == "spPolarPhasor")
                {
                    //Polar Phasor Form
                    var magnitude = Double.Parse(vectorElementNode.ChildNodes[1].FindTokenAndGetText());
                    var phase = Double.Parse(vectorElementNode.ChildNodes[2].FindTokenAndGetText());
                    var id1 = Int32.Parse(vectorElementNode.ChildNodes[3].FindTokenAndGetText()) - 1;
                    var id2 = Int32.Parse(vectorElementNode.ChildNodes[4].FindTokenAndGetText()) - 1;

                    if (id1 < 0 || id2 != id1 + 1)
                        throw new SyntaxErrorException(parsingResults.ToString());

                    vector.AddPolarPhasor(id1, magnitude, phase);
                }
                else if (vectorElementNode.ToString() == "spRectPhasor")
                {
                    //Rectangular Phasor Form
                    var xValue = Double.Parse(vectorElementNode.ChildNodes[1].FindTokenAndGetText());
                    var yValue = Double.Parse(vectorElementNode.ChildNodes[2].FindTokenAndGetText());
                    var id1 = Int32.Parse(vectorElementNode.ChildNodes[3].FindTokenAndGetText()) - 1;
                    var id2 = Int32.Parse(vectorElementNode.ChildNodes[4].FindTokenAndGetText()) - 1;

                    if (id1 < 0 || id2 != id1 + 1)
                        throw new SyntaxErrorException(parsingResults.ToString());

                    vector.AddRectPhasor(id1, xValue, yValue);
                }
                else
                {
                    throw new SyntaxErrorException(parsingResults.ToString());
                }
            }

            return vector;
        }

        private static GaPoTNumMultiPhaseVector GaPoTNumParseMultiPhaseVector(IronyParsingResults parsingResults, ParseTreeNode rootNode)
        {
            if (rootNode.ToString() != "mpVector")
                throw new SyntaxErrorException(parsingResults.ToString());

            var vector = new GaPoTNumMultiPhaseVector();

            var vectorNode = rootNode;
            foreach (var vectorElementNode in vectorNode.ChildNodes)
            {
                var spVector = GaPoTNumParseSinglePhaseVector(parsingResults, vectorElementNode.ChildNodes[0]);
                var id = (vectorElementNode.ChildNodes[1].FindTokenAndGetText().ToLower())[0] - 'a';

                vector.AddPhaseVector(id, spVector);
            }

            return vector;
        }

        public static GaPoTNumSinglePhaseVector GaPoTNumParseSinglePhaseVector(this string sourceText)
        {
            var parsingResults = new IronyParsingResults(
                new GaPoTNumVectorConstructorGrammar(), 
                sourceText
            );

            if (parsingResults.ContainsErrorMessages || !parsingResults.ContainsParseTreeRoot)
                throw new SyntaxErrorException(parsingResults.ToString());

            return GaPoTNumParseSinglePhaseVector(parsingResults, parsingResults.ParseTreeRoot);
        }

        public static GaPoTNumMultiPhaseVector GaPoTNumParseMultiPhaseVector(this string sourceText)
        {
            var parsingResults = new IronyParsingResults(
                new GaPoTNumVectorConstructorGrammar(), 
                sourceText
            );

            if (parsingResults.ContainsErrorMessages || !parsingResults.ContainsParseTreeRoot)
                throw new SyntaxErrorException(parsingResults.ToString());

            return GaPoTNumParseMultiPhaseVector(parsingResults, parsingResults.ParseTreeRoot);
        }
    }
}
