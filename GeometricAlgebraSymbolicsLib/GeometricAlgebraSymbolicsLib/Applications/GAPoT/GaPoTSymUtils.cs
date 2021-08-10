using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using CodeComposerLib.Irony;
using DataStructuresLib;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Irony.Parsing;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public static class GaPoTSymUtils
    {
        public static int LaTeXDecimalPlaces { get; set; }
            = 4;

        public static string GetLaTeXBasisName(this string basisSubscript)
        {
            //return $@"\boldsymbol{{\sigma_{{{basisSubscript}}}}}";
            return $@"\sigma_{{{basisSubscript}}}";
        }

        public static string GetLaTeXSymber(this double value)
        {
            var valueText = value.ToString("G");

            if (valueText.Contains('E'))
            {
                var ePosition = valueText.IndexOf('E');

                var magnitude = double.Parse(valueText.Substring(0, ePosition));
                var magnitudeText = Math.Round(magnitude, LaTeXDecimalPlaces).ToString("G");
                var exponentText = valueText.Substring(ePosition + 1);

                return $@"{magnitudeText} \times 10^{{{exponentText}}}";
            }

            return Math.Round(value, LaTeXDecimalPlaces).ToString("G");
        }

        internal static Tuple<int, int> ValidateBiversorTermIDs(int id1, int id2)
        {
            Debug.Assert(id1 == id2 || (id1 > 0 && id2 > 0));

            if (id1 == id2)
                return new Tuple<int, int>(0, 0);

            return id1 < id2 
                ? new Tuple<int, int>(id1, id2) 
                : new Tuple<int, int>(id2, id1);
        }

        public static Expr GaPoTSymToScalar(this string exprText)
        {
            if (exprText[0] == '\'' && exprText[exprText.Length - 1] == '\'')
                exprText = exprText
                    .Substring(1, exprText.Length - 2)
                    .Trim();

            var expr = exprText.ToExpr();

            return expr;
        }

        public static bool ValidateIndexPermutationList(params int[] inputList)
        {
            var orderedList =
                inputList.Distinct().OrderBy(i => i).ToArray();

            return
                orderedList.Length == inputList.Length &&
                orderedList[0] == 0 &&
                orderedList[orderedList.Length - 1] != (orderedList.Length - 1);
        }


        private static GaPoTSymVector GaPoTSymParseVector(IronyParsingResults parsingResults, ParseTreeNode rootNode)
        {
            if (rootNode.ToString() != "spVector")
                throw new SyntaxErrorException(parsingResults.ToString());

            var vector = new GaPoTSymVector();

            var vectorNode = rootNode;
            foreach (var vectorElementNode in vectorNode.ChildNodes)
            {
                if (vectorElementNode.ToString() == "spTerm")
                {
                    //Term Form
                    var value = vectorElementNode.ChildNodes[0].FindTokenAndGetText().GaPoTSymToScalar();
                    var id = int.Parse(vectorElementNode.ChildNodes[1].FindTokenAndGetText());

                    if (id < 0)
                        throw new SyntaxErrorException(parsingResults.ToString());

                    vector.AddTerm(id, value);
                }
                else if (vectorElementNode.ToString() == "spPolarPhasor")
                {
                    //Polar Phasor Form
                    var magnitude = vectorElementNode.ChildNodes[1].FindTokenAndGetText().GaPoTSymToScalar();
                    var phase = vectorElementNode.ChildNodes[2].FindTokenAndGetText().GaPoTSymToScalar();
                    var id1 = int.Parse(vectorElementNode.ChildNodes[3].FindTokenAndGetText());
                    var id2 = int.Parse(vectorElementNode.ChildNodes[4].FindTokenAndGetText());

                    if (id1 < 0 || id2 != id1 + 1)
                        throw new SyntaxErrorException(parsingResults.ToString());

                    vector.AddPolarPhasor(id1, magnitude, phase);
                }
                else if (vectorElementNode.ToString() == "spRectPhasor")
                {
                    //Rectangular Phasor Form
                    var xValue = vectorElementNode.ChildNodes[1].FindTokenAndGetText().GaPoTSymToScalar();
                    var yValue = vectorElementNode.ChildNodes[2].FindTokenAndGetText().GaPoTSymToScalar();
                    var id1 = int.Parse(vectorElementNode.ChildNodes[3].FindTokenAndGetText());
                    var id2 = int.Parse(vectorElementNode.ChildNodes[4].FindTokenAndGetText());

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

        public static GaPoTSymVector GaPoTSymParseVector(this string sourceText)
        {
            var parsingResults = new IronyParsingResults(
                new GaPoTSymVectorConstructorGrammar(), 
                sourceText
            );

            if (parsingResults.ContainsErrorMessages || !parsingResults.ContainsParseTreeRoot)
                throw new SyntaxErrorException(parsingResults.ToString());

            return GaPoTSymParseVector(parsingResults, parsingResults.ParseTreeRoot);
        }


        private static GaPoTSymBiversor GaPoTSymParseBiversor(IronyParsingResults parsingResults, ParseTreeNode rootNode)
        {
            if (rootNode.ToString() != "bivector")
                throw new SyntaxErrorException(parsingResults.ToString());

            var vector = new GaPoTSymBiversor();

            var vectorNode = rootNode;
            foreach (var vectorElementNode in vectorNode.ChildNodes)
            {
                if (vectorElementNode.ToString() == "bivectorTerm0")
                {
                    //Term Form
                    var value = vectorElementNode.ChildNodes[0].FindTokenAndGetText().GaPoTSymToScalar();

                    vector.AddTerm(1, 1, value);
                }
                else if (vectorElementNode.ToString() == "bivectorTerm2")
                {
                    //Term Form
                    var value = vectorElementNode.ChildNodes[0].FindTokenAndGetText().GaPoTSymToScalar();
                    var id1 = int.Parse(vectorElementNode.ChildNodes[1].FindTokenAndGetText());
                    var id2 = int.Parse(vectorElementNode.ChildNodes[2].FindTokenAndGetText());

                    if (id1 < 0 || id2 < 0)
                        throw new SyntaxErrorException(parsingResults.ToString());

                    vector.AddTerm(id1, id2, value);
                }
                else
                {
                    throw new SyntaxErrorException(parsingResults.ToString());
                }
            }

            return vector;
        }

        public static GaPoTSymBiversor GaPoTSymParseBiversor(this string sourceText)
        {
            var parsingResults = new IronyParsingResults(
                new GaPoTSymBiversorConstructorGrammar(), 
                sourceText
            );

            if (parsingResults.ContainsErrorMessages || !parsingResults.ContainsParseTreeRoot)
                throw new SyntaxErrorException(parsingResults.ToString());

            return GaPoTSymParseBiversor(parsingResults, parsingResults.ParseTreeRoot);
        }


        public static IEnumerable<IList<int>> GetIndexPermutations(int count)
        {
            var indicesArray = Enumerable.Range(0, count).ToArray();

            var list = new List<IList<int>>();

            return GetIndexPermutations(indicesArray, 0, indicesArray.Length - 1, list);
        }

        private static IEnumerable<IList<int>> GetIndexPermutations(int[] indicesArray, int start, int end, IList<IList<int>> list)
        {
            if (start == end)
            {
                // We have one of our possible n! solutions,
                // add it to the list.
                list.Add(new List<int>(indicesArray));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    Swap(ref indicesArray[start], ref indicesArray[i]);

                    GetIndexPermutations(indicesArray, start + 1, end, list);

                    Swap(ref indicesArray[start], ref indicesArray[i]);
                }
            }

            return list;
        }

        private static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }


        public static GaPoTSymVector[] Negative(this IEnumerable<GaPoTSymVector> vectorsList)
        {
            return vectorsList.Select(v => v.Negative()).ToArray();
        }

        public static GaPoTSymVector[] Inverse(this IEnumerable<GaPoTSymVector> vectorsList)
        {
            return vectorsList.Select(v => v.Inverse()).ToArray();
        }

        public static GaPoTSymVector[] Reverse(this IEnumerable<GaPoTSymVector> vectorsList)
        {
            return vectorsList.Select(v => v.Reverse()).ToArray();
        }

        public static Expr[] Norm(this IEnumerable<GaPoTSymVector> vectorsList)
        {
            return vectorsList.Select(v => v.Norm()).ToArray();
        }

        public static Expr[] Norm2(this IEnumerable<GaPoTSymVector> vectorsList)
        {
            return vectorsList.Select(v => v.Norm2()).ToArray();
        }

        public static GaPoTSymVector[] Add(this GaPoTSymVector[] vectorsList1, GaPoTSymVector[] vectorsList2)
        {
            if (vectorsList1.Length != vectorsList2.Length)
                throw new InvalidOperationException();

            var results = new GaPoTSymVector[vectorsList1.Length];

            for (var i = 0; i < vectorsList1.Length; i++)
                results[i] = vectorsList1[i].Add(vectorsList2[i]);

            return results;
        }

        public static GaPoTSymVector[] Subtract(this GaPoTSymVector[] vectorsList1, GaPoTSymVector[] vectorsList2)
        {
            if (vectorsList1.Length != vectorsList2.Length)
                throw new InvalidOperationException();

            var results = new GaPoTSymVector[vectorsList1.Length];

            for (var i = 0; i < vectorsList1.Length; i++)
                results[i] = vectorsList1[i].Subtract(vectorsList2[i]);

            return results;
        }

        public static GaPoTSymBiversor[] Gp(this GaPoTSymVector[] vectorsList1, GaPoTSymVector[] vectorsList2)
        {
            if (vectorsList1.Length != vectorsList2.Length)
                throw new InvalidOperationException();

            var results = new GaPoTSymBiversor[vectorsList1.Length];

            for (var i = 0; i < vectorsList1.Length; i++)
                results[i] = vectorsList1[i].Gp(vectorsList2[i]);

            return results;
        }
        
        public static GaPoTSymMultivector OuterProduct(IReadOnlyList<GaPoTSymVector> vectorsList)
        {
            return vectorsList
                .Skip(1)
                .Aggregate(
                    vectorsList[0].ToMultivector(), 
                    (current, mv) => current.Op(mv.ToMultivector())
                );
        }
        
        public static GaPoTSymMultivector OuterProduct(params GaPoTSymVector[] vectorsList)
        {
            return vectorsList
                .Skip(1)
                .Aggregate(
                    vectorsList[0].ToMultivector(), 
                    (current, mv) => current.Op(mv.ToMultivector())
                );
        }

        public static GaPoTSymMultivector OuterProduct(params GaPoTSymMultivector[] multivectorsList)
        {
            return multivectorsList
                .Skip(1)
                .Aggregate(
                    multivectorsList[0], 
                    (current, mv) => current.Op(mv)
                );
        }

        public static IEnumerable<GaPoTSymVector> ApplyGramSchmidt(this IReadOnlyList<GaPoTSymVector> vectorsArray, bool makeUnitVectors)
        {
            var v1 = vectorsArray[0];
            yield return makeUnitVectors 
                ? (v1 / v1.Norm()) 
                : v1;
            
            var mv1 = vectorsArray[0].ToMultivector();
            
            for (var i = 1; i < vectorsArray.Count; i++)
            {
                var mv2 = mv1.Op(vectorsArray[i]);
                
                var orthogonalVector = mv1.Reverse().Gp(mv2).GetVectorPart();
                //var orthogonalVector = mv2.Gp(mv1.Reverse()).GetVectorPart();
                
                yield return makeUnitVectors
                    ? (orthogonalVector / orthogonalVector.Norm())
                    : orthogonalVector;
                
                mv1 = mv2;
            }
        }

        public static void EigenDecomposition(this Expr matrixExpr, out Expr[] values, out Expr[] vectors)
        {
            var casInterface = MathematicaInterface.DefaultCas;

            var sysExpr = casInterface[Mfs.Eigensystem[matrixExpr]];

            values = sysExpr.Args[0].Args;

            vectors = sysExpr.Args[1].Args;
        }


        public static GaPoTSymVector TermsToVector(this IEnumerable<GaPoTSymVectorTerm> termsList)
        {
            return new GaPoTSymVector(termsList);
        }

        public static GaPoTSymBiversor TermsToBiversor(this IEnumerable<GaPoTSymBiversorTerm> termsList)
        {
            return new GaPoTSymBiversor(termsList);
        }

        public static GaPoTSymMultivector TermsToMultivector(this IEnumerable<GaPoTSymMultivectorTerm> termsList)
        {
            return new GaPoTSymMultivector(termsList);
        }
        
        public static IEnumerable<GaPoTSymMultivectorTerm> OrderByGrade(this IEnumerable<GaPoTSymMultivectorTerm> termsList)
        {
            var termsArray = termsList.ToArray();
            var bitsCount = termsArray.Max(t => t.IDsPattern).LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                .Where(t => !t.Value.IsZero())
                .OrderBy(t => t.GetGrade())
                .ThenByDescending(t => t.IDsPattern.ReverseBits(bitsCount));
        }
        
        public static IEnumerable<GaPoTSymMultivectorTerm> OrderById(this IEnumerable<GaPoTSymMultivectorTerm> termsList)
        {
            return termsList
                .Where(t => !t.Value.IsZero())
                .OrderBy(t => t.IDsPattern);
        }
    }
}
