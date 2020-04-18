using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CodeComposerLib.Irony;
using DataStructuresLib;
using DataStructuresLib.Extensions;
using DataStructuresLib.Permutations;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.GAPoT;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using TextComposerLib.Text;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    public static class GaTermUtils
    {
        public static bool ContainsUniqueIDs<T>(this IEnumerable<GaTerm<T>> termsList)
        {
            var dict = new Dictionary<int, T>();

            foreach (var term in termsList)
                if (dict.ContainsKey(term.BasisBladeId))
                    return false;
                else
                    dict.Add(term.BasisBladeId, term.ScalarValue);

            return true;
        }


        public static string GetNumTermsListText(this GaTerm<double> term)
        {
            var basisVectorIDs =
                term.BasisBladeId
                    .PatternToPositions()
                    .Select(i => i + 1)
                    .Concatenate(",", "<", ">");

            var scalarValue =
                term.ScalarValue.ToString("G");

            return $"{scalarValue}<{basisVectorIDs}>";
        }

        public static string GetNumTermsListText(this IEnumerable<GaTerm<double>> termsList)
        {
            var composer = new StringBuilder();

            var firstTermFlag = true;
            foreach (var term in termsList)
            {
                var basisVectorIDs =
                    term.BasisBladeId
                        .PatternToPositions()
                        .Select(i => i + 1)
                        .Concatenate(",", "<", ">");

                var scalarValue =
                    term.ScalarValue.ToString("G");

                if (firstTermFlag)
                    firstTermFlag = false;
                else
                    composer.Append(", ");

                composer
                    .Append(scalarValue)
                    .Append(basisVectorIDs);
            }

            return composer.ToString();
        }

        public static string GetNumTermsListText(this IEnumerable<KeyValuePair<int, double>> termsList)
        {
            var composer = new StringBuilder();

            var firstTermFlag = true;
            foreach (var term in termsList)
            {
                var basisVectorIDs =
                    term.Key
                        .PatternToPositions()
                        .Select(i => i + 1)
                        .Concatenate(",", "<", ">");

                var scalarValue =
                    term.Value.ToString("G");

                if (firstTermFlag)
                    firstTermFlag = false;
                else
                    composer.Append(", ");

                composer
                    .Append(scalarValue)
                    .Append(basisVectorIDs);
            }

            return composer.ToString();
        }


        //public static string GetPoTTermsListText(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim1, int vSpaceDim2)
        //{
        //    const string basisVectorNames2 = "abcdefghijklmnopqrstuvwxyz";

        //    var mask1 = (1 << vSpaceDim1) - 1;
        //    var mask2 = ~mask1;

        //    var termsDictionary =
        //        termsList
        //            .GroupBy(t => t.BasisBladeId & mask2)
        //            .OrderBy(g => g.Key.BasisBladeGrade())
        //            .ThenBy(g => g.Key.BasisBladeIndex())
        //            .ToDictionary(
        //                g => g.Key, 
        //                g => g.ToArray()
        //            );

        //    var composer = new StringBuilder();

        //    foreach (var pair in termsDictionary)
        //    {
        //        var basisVectorIDs2 = pair
        //            .Key
        //            .GetGaPoTBasisBladeId2(vSpaceDim1, vSpaceDim2)
        //            .PatternToPositions()
        //            .Select(i => basisVectorNames2[i])
        //            .Concatenate(",", "<", ">");

        //        composer.Append("(");

        //        var firstTermFlag = true;
        //        var terms1List =
        //            pair
        //                .Value
        //                .OrderBy(a => a.BasisBladeGrade)
        //                .ThenBy(a => a.BasisBladeIndex);

        //        foreach (var term in terms1List)
        //        {
        //            var basisVectorIDs1 =
        //                (term.BasisBladeId & mask1)
        //                .PatternToPositions()
        //                .Select(i => i + 1)
        //                .Concatenate(",", "<", ">");

        //            var scalarValue =
        //                term.ScalarValue.ToString("G");

        //            if (firstTermFlag)
        //                firstTermFlag = false;
        //            else
        //                composer.Append(", ");

        //            composer
        //                .Append(scalarValue)
        //                .Append(basisVectorIDs1);
        //        }

        //        composer
        //            .Append(")")
        //            .Append(basisVectorIDs2)
        //            .Append("; ");
        //    }

        //    if (termsDictionary.Count > 0)
        //        composer.Length -= 2;

        //    return composer.ToString();
        //}


        public static IEnumerable<GaTerm<double>> ParseGaNumTermsList(this string termsText)
        {
            //Examples:
            //  Define a scalar: "1.2<>"
            //  Define a scalar plus 3-blade: "1.2<>, -3.24<1,3,5>"
            //  Define the sum of multiple terms with repeated IDs: "-3.24<1,3,5>, 1.556<1>, 1.4<1,3,5>, -2.6<1>" 
            //Note: basis vectors inside <> are labeled 1,2,3,4,...,30
            var parsingResults = new IronyParsingResults(
                new GaNumMultivectorConstructorGrammar(), 
                termsText
            );

            if (parsingResults.ContainsErrorMessages || !parsingResults.ContainsParseTreeRoot)
                throw new SyntaxErrorException(parsingResults.ToString());

            foreach (var termNode in parsingResults.ParseTreeRoot.ChildNodes)
            {
                var scalarValue = 
                    double.Parse(termNode.ChildNodes[0].FindTokenAndGetText());

                var basisBladeIdNode = termNode.ChildNodes[1];

                var id = basisBladeIdNode.ChildNodes.Count > 0
                    ? basisBladeIdNode.ChildNodes.Select(
                        n => 1 << (int.Parse(n.FindTokenAndGetText()) - 1)
                    ).Distinct() .Sum()
                    : 0;

                yield return new GaTerm<double>(id, scalarValue);
            }
        }

        //public static IEnumerable<GaTerm<double>> ParseGaPoTTermsList(this string termsText, int vSpaceDim1, int vSpaceDim2)
        //{
        //    var parsingResults = new IronyParsingResults(
        //        new GaPoTMultivectorConstructorGrammar(), 
        //        termsText
        //    );

        //    if (parsingResults.ContainsErrorMessages || !parsingResults.ContainsParseTreeRoot)
        //        throw new SyntaxErrorException(parsingResults.ToString());

        //    var lastBasisVectorId = 1 << vSpaceDim2;
        //    foreach (var term2Node in parsingResults.ParseTreeRoot.ChildNodes)
        //    {
        //        var multivector1Node = term2Node.ChildNodes[0];
        //        var basisBladeId2Node = term2Node.ChildNodes[1];

        //        var id2 = basisBladeId2Node.ChildNodes.Count > 0
        //            ? basisBladeId2Node.ChildNodes.Select(
        //                n => (1 << (n.FindTokenAndGetText()[0] - 'a')) | lastBasisVectorId
        //            ).Distinct().BitwiseXor()
        //            : 0;

        //        foreach (var term1Node in multivector1Node.ChildNodes)
        //        {
        //            if (term1Node.Term.Name == "expTerm1")
        //            {
        //                var magnitudeValue =
        //                    double.Parse(term1Node.ChildNodes[0].FindTokenAndGetText());

        //                var phaseValue =
        //                    double.Parse(term1Node.ChildNodes[1].FindTokenAndGetText());

        //                var i1 = 1 << (int.Parse(term1Node.ChildNodes[2].FindTokenAndGetText()) - 1);
        //                var i2 = 1 << (int.Parse(term1Node.ChildNodes[3].FindTokenAndGetText()) - 1);
        //                var i3 = 1 << (int.Parse(term1Node.ChildNodes[4].FindTokenAndGetText()) - 1);

        //                var basisBivectorId = i1 | i2;
        //                var basisVectorId = i3;

        //                var basisBladeId = basisVectorId + (id2 << vSpaceDim1);
        //                var scalarValue = magnitudeValue * Math.Cos(phaseValue);

        //                yield return new GaTerm<double>(basisBladeId, scalarValue);

        //                basisBladeId = (basisBivectorId ^ basisVectorId) + (id2 << vSpaceDim1);
        //                scalarValue = Math.Sin(phaseValue) * (
        //                    GaNumFrameUtils.IsNegativeEGp(basisBivectorId, basisVectorId)
        //                        ? -magnitudeValue
        //                        : magnitudeValue
        //                    );

        //                yield return new GaTerm<double>(basisBladeId, scalarValue);
        //            }
        //            else
        //            {
        //                var scalarValue =
        //                    double.Parse(term1Node.ChildNodes[0].FindTokenAndGetText());

        //                var basisBladeId1Node = term1Node.ChildNodes[1];

        //                var id1 = basisBladeId1Node.ChildNodes.Count > 0
        //                    ? basisBladeId1Node.ChildNodes.Select(
        //                        n => 1 << (int.Parse(n.FindTokenAndGetText()) - 1)
        //                    ).Distinct().Sum()
        //                    : 0;

        //                var basisBladeId = id1 + (id2 << vSpaceDim1);

        //                yield return new GaTerm<double>(basisBladeId, scalarValue);
        //            }
        //        }
        //    }
        //}


        public static GaTerm<double> GaScaleBy(this GaTerm<double> term, double scalingFactor)
        {
            return new GaTerm<double>(term.BasisBladeId, scalingFactor * term.ScalarValue);
        }

        public static GaTerm<double> GaNegative(this GaTerm<double> term)
        {
            return new GaTerm<double>(term.BasisBladeId, -term.ScalarValue);
        }

        public static GaTerm<double> GaReverse(this GaTerm<double> term)
        {
            return term.BasisBladeId.BasisBladeIdHasNegativeReverse()
                ? new GaTerm<double>(term.BasisBladeId, -term.ScalarValue)
                : term;
        }

        public static GaTerm<double> GaGradeInv(this GaTerm<double> term)
        {
            return term.BasisBladeId.BasisBladeIdHasNegativeGradeInv()
                ? new GaTerm<double>(term.BasisBladeId, -term.ScalarValue)
                : term;
        }

        public static GaTerm<double> GaCliffConj(this GaTerm<double> term)
        {
            return term.BasisBladeId.BasisBladeIdHasNegativeCliffConj()
                ? new GaTerm<double>(term.BasisBladeId, -term.ScalarValue)
                : term;
        }


        public static GaNumTerm ToNumTerm(this GaTerm<double> term, int vSpaceDim)
        {
            return new GaNumTerm(vSpaceDim, term.BasisBladeId, term.ScalarValue);
        }


        public static IEnumerable<GaTerm<double>> GetTerms(this GaBtrInternalNode<double> btrRootNode)
        {
            var treeDepth = btrRootNode.GetTreeDepth();

            return btrRootNode
                .GetNodeInfo(treeDepth, 0)
                .GetTreeLeafNodesInfo()
                .Select(node => new GaTerm<double>((int)node.Id, node.Value));
        }

        public static IEnumerable<GaTerm<double>> GetTerms(this GaBtrInternalNode<double> btrRootNode, int treeDepth)
        {
            Debug.Assert(treeDepth == btrRootNode.GetTreeDepth());

            return btrRootNode
                .GetNodeInfo(treeDepth, 0)
                .GetTreeLeafNodesInfo()
                .Select(node => new GaTerm<double>((int)node.Id, node.Value));
        }


        public static GaBtrInternalNode<double> CreateBtr(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            var btrRootNode = new GaBtrInternalNode<double>();

            foreach (var term in termsList)
            {
                var id = term.BasisBladeId;
                var scalarValue = term.ScalarValue;

                var node = btrRootNode;
                for (var i = vSpaceDim - 1; i > 0; i--)
                {
                    var bitPattern = (1 << i) & id;
                    node = node.GetOrAddInternalChildNode(bitPattern != 0);
                }

                if ((1 & id) == 0)
                    node.ResetLeafChildNode0(scalarValue);
                else
                    node.ResetLeafChildNode1(scalarValue);
            }

            return btrRootNode;
        }


        public static IEnumerable<GaTerm<double>> MapUsing(this IEnumerable<GaTerm<double>> termsList, Func<double, double> mappingFunc)
        {
            return termsList.Select(t => 
                new GaTerm<double>(
                    t.BasisBladeId, 
                    mappingFunc(t.ScalarValue)
                )
            );
        }

        public static IEnumerable<GaTerm<double>> MapUsing(this IEnumerable<GaTerm<double>> termsList, Func<int, double, GaTerm<double>> mappingFunc)
        {
            return termsList.Select(t =>
                mappingFunc(
                    t.BasisBladeId,
                    t.ScalarValue
                )
            );
        }

        public static IEnumerable<GaTerm<double>> MapUsing(this IEnumerable<GaTerm<double>> termsList, Func<GaTerm<double>, GaTerm<double>> mappingFunc)
        {
            return termsList.Select(mappingFunc);
        }

        public static IEnumerable<GaTerm<double>> MapUsing(this IEnumerable<GaTerm<double>> termsList, Func<int, int, double, GaTerm<double>> mappingFunc)
        {
            return termsList.Select(t =>
                mappingFunc(
                    t.BasisBladeGrade,
                    t.BasisBladeIndex,
                    t.ScalarValue
                )
            );
        }


        public static IEnumerable<GaTerm<double>> GaNegative(this IEnumerable<GaTerm<double>> termsList, Func<int, bool> isNegativeFunc)
        {
            return termsList.Select(t =>
                new GaTerm<double>(
                    t.BasisBladeId,
                    isNegativeFunc(t.BasisBladeId) ? -t.ScalarValue : t.ScalarValue
                )
            );
        }

        public static IEnumerable<GaTerm<double>> GaNegative(this IEnumerable<GaTerm<double>> termsList, Func<GaTerm<double>, bool> isNegativeFunc)
        {
            return termsList.Select(t =>
                new GaTerm<double>(
                    t.BasisBladeId,
                    isNegativeFunc(t) ? -t.ScalarValue : t.ScalarValue
                )
            );
        }

        public static IEnumerable<GaTerm<double>> GaNegative(this IEnumerable<GaTerm<double>> termsList)
        {
            return termsList.Select(t =>
                new GaTerm<double>(
                    t.BasisBladeId,
                    -t.ScalarValue
                )
            );
        }

        public static IEnumerable<GaTerm<double>> GaReverse(this IEnumerable<GaTerm<double>> termsList)
        {
            return termsList.Select(t => 
                new GaTerm<double>(
                    t.BasisBladeId,
                    t.BasisBladeId.BasisBladeIdHasNegativeReverse() ? -t.ScalarValue : t.ScalarValue
                )
            );
        }

        public static IEnumerable<GaTerm<double>> GaGradeInv(this IEnumerable<GaTerm<double>> termsList)
        {
            return termsList.Select(t =>
                new GaTerm<double>(
                    t.BasisBladeId,
                    t.BasisBladeId.BasisBladeIdHasNegativeGradeInv() ? -t.ScalarValue : t.ScalarValue
                )
            );
        }

        public static IEnumerable<GaTerm<double>> GaCliffConj(this IEnumerable<GaTerm<double>> termsList)
        {
            return termsList.Select(t =>
                new GaTerm<double>(
                    t.BasisBladeId,
                    t.BasisBladeId.BasisBladeIdHasNegativeCliffConj() ? -t.ScalarValue : t.ScalarValue
                )
            );
        }

        public static IEnumerable<GaTerm<double>> GaScaleBy(this IEnumerable<GaTerm<double>> termsList, double scalingFactor)
        {
            return termsList.Select(t =>
                new GaTerm<double>(
                    t.BasisBladeId,
                    t.ScalarValue * scalingFactor
                )
            );
        }
    }
}