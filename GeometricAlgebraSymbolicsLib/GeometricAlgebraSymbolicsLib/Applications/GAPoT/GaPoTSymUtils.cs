using System;
using System.Data;
using CodeComposerLib.Irony;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Irony.Parsing;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public static class GaPoTSymUtils
    {
        public static Expr GaPoTSymToScalar(this string exprText)
        {
            if (exprText[0] == '\'' && exprText[exprText.Length - 1] == '\'')
                exprText = exprText
                    .Substring(1, exprText.Length - 2)
                    .Trim();

            var expr = exprText.ToExpr(MathematicaInterface.DefaultCas);

            return expr;
        }

        public static Expr GaPoTSymSimplify(this Expr expr)
        {
            return expr.Simplify(MathematicaInterface.DefaultCas);
        }

        public static string GaPoTSymEvaluateToString(this Expr expr)
        {
            return MathematicaInterface.DefaultCasConnection.EvaluateToString(expr);
        }

        public static string GetLaTeXScalar(this Expr expr)
        {
            return Mfs.EToString[Mfs.TeXForm[expr]].GaPoTSymEvaluateToString();
        }


        private static GaPoTSymSinglePhaseVector GaPoTSymParseSinglePhaseVector(IronyParsingResults parsingResults, ParseTreeNode rootNode)
        {
            if (rootNode.ToString() != "spVector")
                throw new SyntaxErrorException(parsingResults.ToString());

            var vector = new GaPoTSymSinglePhaseVector();

            var vectorNode = rootNode;
            foreach (var vectorElementNode in vectorNode.ChildNodes)
            {
                if (vectorElementNode.ToString() == "spTerm")
                {
                    //Term Form
                    var value = vectorElementNode.ChildNodes[0].FindTokenAndGetText().GaPoTSymToScalar();
                    var id = int.Parse(vectorElementNode.ChildNodes[1].FindTokenAndGetText()) - 1;

                    if (id < 0)
                        throw new SyntaxErrorException(parsingResults.ToString());

                    vector.AddTerm(id, value);
                }
                else if (vectorElementNode.ToString() == "spPolarPhasor")
                {
                    //Polar Phasor Form
                    var magnitude = vectorElementNode.ChildNodes[1].FindTokenAndGetText().GaPoTSymToScalar();
                    var phase = vectorElementNode.ChildNodes[2].FindTokenAndGetText().GaPoTSymToScalar();
                    var id1 = int.Parse(vectorElementNode.ChildNodes[3].FindTokenAndGetText()) - 1;
                    var id2 = int.Parse(vectorElementNode.ChildNodes[4].FindTokenAndGetText()) - 1;

                    if (id1 < 0 || id2 != id1 + 1)
                        throw new SyntaxErrorException(parsingResults.ToString());

                    vector.AddPolarPhasor(id1, magnitude, phase);
                }
                else if (vectorElementNode.ToString() == "spRectPhasor")
                {
                    //Rectangular Phasor Form
                    var xValue = vectorElementNode.ChildNodes[1].FindTokenAndGetText().GaPoTSymToScalar();
                    var yValue = vectorElementNode.ChildNodes[2].FindTokenAndGetText().GaPoTSymToScalar();
                    var id1 = int.Parse(vectorElementNode.ChildNodes[3].FindTokenAndGetText()) - 1;
                    var id2 = int.Parse(vectorElementNode.ChildNodes[4].FindTokenAndGetText()) - 1;

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

        private static GaPoTSymMultiPhaseVector GaPoTSymParseMultiPhaseVector(IronyParsingResults parsingResults, ParseTreeNode rootNode)
        {
            if (rootNode.ToString() != "mpVector")
                throw new SyntaxErrorException(parsingResults.ToString());

            var vector = new GaPoTSymMultiPhaseVector();

            var vectorNode = rootNode;
            foreach (var vectorElementNode in vectorNode.ChildNodes)
            {
                var spVector = GaPoTSymParseSinglePhaseVector(parsingResults, vectorElementNode.ChildNodes[0]);
                var id = (vectorElementNode.ChildNodes[1].FindTokenAndGetText().ToLower())[0] - 'a';

                vector.AddPhaseVector(id, spVector);
            }

            return vector;
        }

        public static GaPoTSymSinglePhaseVector GaPoTSymParseSinglePhaseVector(this string sourceText)
        {
            var parsingResults = new IronyParsingResults(
                new GaPoTSymVectorConstructorGrammar(), 
                sourceText
            );

            if (parsingResults.ContainsErrorMessages || !parsingResults.ContainsParseTreeRoot)
                throw new SyntaxErrorException(parsingResults.ToString());

            return GaPoTSymParseSinglePhaseVector(parsingResults, parsingResults.ParseTreeRoot);
        }

        public static GaPoTSymMultiPhaseVector GaPoTSymParseMultiPhaseVector(this string sourceText)
        {
            var parsingResults = new IronyParsingResults(
                new GaPoTSymVectorConstructorGrammar(),
                sourceText
            );

            if (parsingResults.ContainsErrorMessages || !parsingResults.ContainsParseTreeRoot)
                throw new SyntaxErrorException(parsingResults.ToString());

            return GaPoTSymParseMultiPhaseVector(parsingResults, parsingResults.ParseTreeRoot);
        }
    }
}
