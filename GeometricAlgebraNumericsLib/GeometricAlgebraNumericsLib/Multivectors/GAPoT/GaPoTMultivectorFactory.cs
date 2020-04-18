using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CodeComposerLib.Irony;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;

namespace GeometricAlgebraNumericsLib.Multivectors.GAPoT
{
    //public sealed class GaPoTMultivectorFactory : IEnumerable<GaTerm<double>>
    //{
    //    public static IronyParsingResults GetParsingResults(string sourceText)
    //    {
    //        return new IronyParsingResults(
    //            new GaPoTMultivectorConstructorGrammar(), 
    //            sourceText
    //        );
    //    }

    //    public static GaNumSarMultivector Parse(string sourceText, int vSpaceDim1, int vSpaceDim2)
    //    {
    //        var parsingResults = GetParsingResults(sourceText);

    //        if (parsingResults.ContainsErrorMessages || !parsingResults.ContainsParseTreeRoot)
    //            return GaNumSarMultivector.CreateZero(vSpaceDim1 + vSpaceDim2);

    //        var factory = new GaPoTMultivectorFactory(vSpaceDim1, vSpaceDim2);

    //        foreach (var term2Node in parsingResults.ParseTreeRoot.ChildNodes)
    //        {
    //            var multivector1Node = term2Node.ChildNodes[0];
    //            var basisBladeId2Node = term2Node.ChildNodes[1];

    //            var id2 = basisBladeId2Node.ChildNodes.Count > 0
    //                ? basisBladeId2Node.ChildNodes.Sum(
    //                    n => 1 << (n.FindTokenAndGetText()[0] - 'a')
    //                )
    //                : 0;

    //            var mvFactory = factory[id2];

    //            foreach (var term1Node in multivector1Node.ChildNodes)
    //            {
    //                var scalarValue = 
    //                    double.Parse(term1Node.ChildNodes[0].FindTokenAndGetText());

    //                var basisBladeId1Node = term1Node.ChildNodes[1];

    //                var id1 = basisBladeId1Node.ChildNodes.Count > 0
    //                    ? basisBladeId1Node.ChildNodes.Sum(
    //                        n => 1 << (int.Parse(n.FindTokenAndGetText()) - 1)
    //                    ) 
    //                    : 0;

    //                mvFactory.AddTerm(id1, scalarValue);
    //            }
    //        }

    //        return factory.SumAsSarMultivector(vSpaceDim1 + vSpaceDim2);
    //    }


    //    private readonly Dictionary<int, GaNumSarMultivectorFactory> _factoriesDictionary 
    //        = new Dictionary<int, GaNumSarMultivectorFactory>();

    //    public int VSpaceDimension1 { get; }

    //    public int VSpaceDimension2 { get; }

    //    public int GaSpaceDimension1 
    //        => VSpaceDimension1.ToGaSpaceDimension();

    //    public int GaSpaceDimension2 
    //        => VSpaceDimension2.ToGaSpaceDimension();


    //    public GaNumSarMultivectorFactory this[int id]
    //    {
    //        get
    //        {
    //            if (id < 0 || id >= GaSpaceDimension2)
    //                throw new IndexOutOfRangeException();

    //            if (_factoriesDictionary.TryGetValue(id, out var term))
    //                return term;

    //            term = new GaNumSarMultivectorFactory(VSpaceDimension1);

    //            _factoriesDictionary.Add(id, term);

    //            return term;
    //        }
    //    }


    //    public GaPoTMultivectorFactory(int vSpaceDim1, int vSpaceDim2)
    //    {
    //        Debug.Assert(
    //            (vSpaceDim1 + vSpaceDim2).IsValidVSpaceDimension()
    //        );

    //        VSpaceDimension1 = vSpaceDim1;
    //        VSpaceDimension2 = vSpaceDim2;
    //    }


    //    public GaPoTMultivectorFactory Reset()
    //    {
    //        _factoriesDictionary.Clear();

    //        return this;
    //    }

    //    public IEnumerator<GaTerm<double>> GetEnumerator()
    //    {
    //        foreach (var pair in _factoriesDictionary)
    //        {
    //            var id2 = pair.Key << VSpaceDimension1;

    //            foreach (var term in pair.Value.GetNonZeroTerms())
    //                yield return new GaTerm<double>(
    //                    term.BasisBladeId + id2, 
    //                    term.ScalarValue
    //                );
    //        }
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }
    //}
}