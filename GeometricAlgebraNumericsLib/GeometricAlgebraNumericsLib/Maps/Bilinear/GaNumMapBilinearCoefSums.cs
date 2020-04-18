using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraNumericsLib.Structures;
using TextComposerLib.Text;
using TextComposerLib.Text.Markdown.Tables;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraNumericsLib.Maps.Bilinear
{
    /// <summary>
    /// This class represents a general linear map on multivectors. The mapping factors are stored
    /// separately in a way useful for analysis but not for efficient computations.
    /// </summary>
    public sealed class GaNumMapBilinearCoefSums : GaNumMapBilinear
    {
        private sealed class GaNumMapBilinearCoefSumsTerm : IEnumerable<Tuple<int, int, double>>
        {
            private readonly List<Tuple<int, int, double>> _factorsList
                = new List<Tuple<int, int, double>>();


            public int TargetBasisBladeId { get; private set; }

            public IEnumerable<string> TermsText
            {
                get
                {
                    var termBuilder = new StringBuilder();

                    foreach (var pair in _factorsList)
                        yield return termBuilder
                            .Clear()
                            .Append(pair.Item3)
                            .Append(" * (")
                            .Append(pair.Item1.BasisBladeIndexedName())
                            .Append(", ")
                            .Append(pair.Item2.BasisBladeIndexedName())
                            .Append(")")
                            .ToString();
                }
            }


            internal GaNumMapBilinearCoefSumsTerm(int basisBladeId)
            {
                TargetBasisBladeId = basisBladeId;
            }


            public double this[int domainBasisBladeId1, int domainBasisBladeId2]
                => _factorsList
                    .FirstOrDefault(
                        factor => 
                            factor.Item1 == domainBasisBladeId1 && 
                            factor.Item2 == domainBasisBladeId2
                    )?.Item3 ?? 0.0d;

            public double this[IGaNumMultivector mv1, IGaNumMultivector mv2]
                => _factorsList.Count == 0
                    ? 0.0d
                    : _factorsList
                        .Select(factor => mv1[factor.Item1] * mv2[factor.Item2] * factor.Item3)
                        .Sum();


            internal GaNumMapBilinearCoefSumsTerm Reset()
            {
                _factorsList.Clear();

                return this;
            }

            internal GaNumMapBilinearCoefSumsTerm AddFactor(int domainBasisBladeId1, int domainBasisBladeId2, bool isNegative = false)
            {
                _factorsList.Add(
                    Tuple.Create(
                        domainBasisBladeId1, 
                        domainBasisBladeId2,
                        isNegative ? -1.0d : 1.0d
                    ));

                return this;
            }

            internal GaNumMapBilinearCoefSumsTerm AddFactor(int domainBasisBladeId1, int domainBasisBladeId2, double factor)
            {
                _factorsList.Add(
                    Tuple.Create(
                        domainBasisBladeId1,
                        domainBasisBladeId2,
                        factor
                    ));

                return this;
            }

            internal GaNumMapBilinearCoefSumsTerm RemoveFactor(int domainBasisBladeId1, int domainBasisBladeId2)
            {
                var index = _factorsList.FindIndex(
                    factor =>
                        factor.Item1 == domainBasisBladeId1 &&
                        factor.Item2 == domainBasisBladeId2
                    );

                if (index >= 0)
                    _factorsList.RemoveAt(index);

                return this;
            }

            public IEnumerator<Tuple<int, int, double>> GetEnumerator()
            {
                return _factorsList.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _factorsList.GetEnumerator();
            }


            public override string ToString()
            {
                var composer = new ListTextComposer(" + ")
                {
                    FinalPrefix = TargetBasisBladeId.BasisBladeIndexedName() + ": { ",
                    FinalSuffix = " }"
                };

                composer.AddRange(TermsText);

                return composer.ToString();
            }
        }


        public static GaNumMapBilinearCoefSums Create(int targetVSpaceDim, GaNumMapBilinearAssociativity associativity)
        {
            return new GaNumMapBilinearCoefSums(
                targetVSpaceDim,
                associativity
            );
        }

        public static GaNumMapBilinearCoefSums Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaNumMapBilinearCoefSums(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }

        public static GaNumMapBilinearCoefSums CreateFromOuterProduct(int vSpaceDimension)
        {
            return new GaNumOp(vSpaceDimension).ToCoefSumsMap();
        }

        public static GaNumMapBilinearCoefSums CreateFromOuterProduct(IGaFrame frame)
        {
            return new GaNumOp(frame.VSpaceDimension).ToCoefSumsMap();
        }


        private readonly GaSparseTable1D<int, GaNumMapBilinearCoefSumsTerm> _coefSumsTable
            = new GaSparseTable1D<int, GaNumMapBilinearCoefSumsTerm>();


        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        public int FactorsCount 
            => _coefSumsTable.Select(t => t.Value.Count()).Sum();

        public override IGaNumMultivector this[int domainBasisBladeId1, int domainBasisBladeId2]
        {
            get
            {
                var resultMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);

                foreach (var terms in _coefSumsTable.Values)
                    resultMv.AddTerm(
                        terms.TargetBasisBladeId,
                        terms[domainBasisBladeId1, domainBasisBladeId2]
                    );

                return resultMv.GetSarMultivector();
            }
        }

        public override GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var resultMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);

                foreach (var terms in _coefSumsTable.Values)
                    resultMv.AddTerm(
                        terms.TargetBasisBladeId,
                        terms[mv1, mv2]
                    );

                return resultMv.GetSarMultivector();
            }
        }

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var resultMv = new GaNumDgrMultivectorFactory(TargetGaSpaceDimension);

                foreach (var terms in _coefSumsTable.Values)
                    resultMv.AddTerm(
                        terms.TargetBasisBladeId,
                        terms[mv1, mv2]
                    );

                return resultMv.GetDgrMultivector();
            }
        }

        private GaNumMapBilinearCoefSums(int targetVSpaceDim, GaNumMapBilinearAssociativity associativity)
            : base(associativity)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaNumMapBilinearCoefSums(int domainVSpaceDim, int targetVSpaceDim)
            : base(GaNumMapBilinearAssociativity.NoneAssociative)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaNumMapBilinearCoefSums SetBasisBladesMap(int domainBasisBladeId1, int domainBasisBladeId2, IGaNumMultivector targetMv)
        {
            Debug.Assert(targetMv.VSpaceDimension == TargetVSpaceDimension);

            foreach (var term in targetMv.GetNonZeroTerms())
                SetFactor(term.BasisBladeId, domainBasisBladeId1, domainBasisBladeId2, term.ScalarValue);

            return this;
        }

        public GaNumMapBilinearCoefSums SetFactor(int targetBasisBladeId, int domainBasisBladeId1, int domainBasisBladeId2, bool isNegative = false)
        {
            if (!_coefSumsTable.TryGetValue(targetBasisBladeId, out var sum))
            {
                sum = new GaNumMapBilinearCoefSumsTerm(targetBasisBladeId);
                _coefSumsTable[targetBasisBladeId] = sum;
            }

            sum.AddFactor(domainBasisBladeId1, domainBasisBladeId2, isNegative);

            return this;
        }

        public GaNumMapBilinearCoefSums SetFactor(int termId, int domainBasisBladeId1, int domainBasisBladeId2, double factorValue)
        {
            if (!_coefSumsTable.TryGetValue(termId, out var sum))
            {
                sum = new GaNumMapBilinearCoefSumsTerm(termId);
                _coefSumsTable[termId] = sum;
            }

            sum.AddFactor(domainBasisBladeId1, domainBasisBladeId2, factorValue);

            return this;
        }

        public GaNumMapBilinearCoefSums RemoveFactor(int targetBasisBladeId, int domainBasisBladeId1, int domainBasisBladeId2)
        {
            if (!_coefSumsTable.TryGetValue(targetBasisBladeId, out var sum))
                return this;

            sum.RemoveFactor(domainBasisBladeId1, domainBasisBladeId2);

            return this;
        }

        public GaNumMapBilinearCoefSums RemoveFactors(int targetBasisBladeId)
        {
            _coefSumsTable.Remove(targetBasisBladeId);

            return this;
        }


        public override IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps()
        {
            //var bilinearMap =
            //    GaNumMapBilinearTree.Create(
            //        DomainVSpaceDimension1, 
            //        DomainVSpaceDimension2, 
            //        TargetVSpaceDimension
            //    );

            //foreach (var coefSum in _coefSumsTable.Values)
            //{
            //    var targetId = coefSum.TargetBasisBladeId;

            //    foreach (var factor in coefSum)
            //    {
            //        var id1 = factor.Item1;
            //        var id2 = factor.Item2;
            //        var coef = factor.Item3;

            //        bilinearMap.SetBasisBladesMap(id1, id2, null);
            //    }
            //}

            for (var id1 = 0; id1 < DomainGaSpaceDimension; id1++)
                for (var id2 = 0; id2 < DomainGaSpaceDimension; id2++)
                {
                    var mv = this[id1, id2];

                    if (!mv.IsNullOrEmpty())
                        yield return new Tuple<int, int, IGaNumMultivector>(id1, id2, mv);
                }
        }

        public MarkdownTable ToMarkdownTable()
        {
            var mdComposer = new MarkdownTable();

            foreach (var term in _coefSumsTable)
            {
                var mdColumn = mdComposer.AddColumn(
                    term.Key.BasisBladeIndexedName(),
                    MarkdownTableColumnAlignment.Left,
                    term.Key.BasisBladeIndexedName()
                    );

                mdColumn.AddRange(term.Value.TermsText);
            }

            return mdComposer;
        }

        public override string ToString()
        {
            return
                _coefSumsTable
                .Values
                .Select(d => d.ToString())
                .Concatenate(", " + Environment.NewLine);
        }
    }
}
