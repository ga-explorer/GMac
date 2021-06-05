using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;
using GeometricAlgebraSymbolicsLib.Products;
using TextComposerLib.Text;
using TextComposerLib.Text.Markdown.Tables;
using TextComposerLib.Text.Structured;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Maps.Bilinear
{
    /// <summary>
    /// This class represents a general linear map on multivectors. The mapping factors are stored
    /// separately in a way useful for analysis but not for efficient computations.
    /// </summary>
    public sealed class GaSymMapBilinearCoefSums : GaSymMapBilinear
    {
        internal sealed class GaSymMapBilinearCoefSumsTerm : IEnumerable<Tuple<ulong, ulong, Expr>>
        {
            private readonly List<Tuple<ulong, ulong, Expr>> _factorsList
                = new List<Tuple<ulong, ulong, Expr>>();


            public ulong TargetBasisBladeId { get; private set; }

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

            public Expr this[ulong domainBasisBladeId1, ulong domainBasisBladeId2]
                => _factorsList
                       .FirstOrDefault(
                           factor =>
                               factor.Item1 == domainBasisBladeId1 &&
                               factor.Item2 == domainBasisBladeId2
                       )?.Item3 ?? Expr.INT_ZERO;

            public Expr this[IGaSymMultivector mv1, IGaSymMultivector mv2]
            {
                get
                {
                    if (_factorsList.Count == 0)
                        return Expr.INT_ZERO;

                    var termsExprArray =
                        _factorsList
                            .Select(factor => Mfs.ProductExpr(
                                mv1[factor.Item1],
                                mv2[factor.Item2],
                                factor.Item3
                            )).ToArray();

                    return termsExprArray.Length == 1
                        ? termsExprArray[0]
                        : Mfs.SumExpr(termsExprArray);
                }
            }


            internal GaSymMapBilinearCoefSumsTerm(ulong basisBladeId)
            {
                TargetBasisBladeId = basisBladeId;
            }


            public string ToExpressionText(GaSymMultivector mv1, GaSymMultivector mv2)
            {
                return this[mv1, mv2].ToString();
            }

            public string ToExpressionText(GaSymMultivectorTempTree mv1, GaSymMultivectorTempTree mv2)
            {
                return this[mv1, mv2].ToString();
            }

            public MathematicaScalar ToSymbolicScalar(GaSymMultivector mv1, GaSymMultivector mv2)
            {
                return MathematicaScalar.Create(GaSymbolicsUtils.Cas, this[mv1, mv2]);
            }

            public MathematicaScalar ToSymbolicScalar(GaSymMultivectorTempTree mv1, GaSymMultivectorTempTree mv2)
            {
                return MathematicaScalar.Create(GaSymbolicsUtils.Cas, this[mv1, mv2]);
            }

            
            internal GaSymMapBilinearCoefSumsTerm Reset()
            {
                _factorsList.Clear();

                return this;
            }

            internal GaSymMapBilinearCoefSumsTerm AddFactor(ulong domainBasisBladeId1, ulong domainBasisBladeId2, bool isNegative = false)
            {
                _factorsList.Add(
                    Tuple.Create(
                        domainBasisBladeId1,
                        domainBasisBladeId2,
                        isNegative ? Expr.INT_MINUSONE : Expr.INT_ONE
                    ));

                return this;
            }

            internal GaSymMapBilinearCoefSumsTerm AddFactor(ulong domainBasisBladeId1, ulong domainBasisBladeId2, Expr factorValue)
            {
                _factorsList.Add(
                    Tuple.Create(
                        domainBasisBladeId1,
                        domainBasisBladeId2,
                        factorValue
                    ));

                return this;
            }

            internal GaSymMapBilinearCoefSumsTerm RemoveFactor(ulong domainBasisBladeId1, ulong domainBasisBladeId2)
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

            public IEnumerator<Tuple<ulong, ulong, Expr>> GetEnumerator()
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


        public static GaSymMapBilinearCoefSums Create(int targetVSpaceDim)
        {
            return new GaSymMapBilinearCoefSums(
                targetVSpaceDim
            );
        }

        public static GaSymMapBilinearCoefSums Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaSymMapBilinearCoefSums(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }

        public static GaSymMapBilinearCoefSums CreateFromOuterProduct(int vSpaceDimension)
        {
            return new GaSymOp(vSpaceDimension).ToCoefSumsMap();
        }

        public static GaSymMapBilinearCoefSums CreateFromOuterProduct(IGaFrame frame)
        {
            return new GaSymOp(frame.VSpaceDimension).ToCoefSumsMap();
        }


        private readonly GaSparseTable1D<ulong, GaSymMapBilinearCoefSumsTerm> _coefSumsTable
            = new GaSparseTable1D<ulong, GaSymMapBilinearCoefSumsTerm>();


        public override int TargetVSpaceDimension { get; }

        public override int DomainVSpaceDimension { get; }

        public override IGaSymMultivector this[ulong id1, ulong id2] 
            => MapToTemp(id1, id2).ToMultivector();


        private GaSymMapBilinearCoefSums(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaSymMapBilinearCoefSums(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaSymMapBilinearCoefSums SetBasisBladesMap(ulong domainBasisBladeId1, ulong domainBasisBladeId2, IGaSymMultivector targetMv)
        {
            Debug.Assert(targetMv.VSpaceDimension == TargetVSpaceDimension);

            foreach (var term in targetMv.NonZeroExprTerms)
                SetFactor(term.Key, domainBasisBladeId1, domainBasisBladeId2, term.Value);

            return this;
        }

        public GaSymMapBilinearCoefSums SetFactor(ulong targetBasisBladeId, ulong domainBasisBladeId1, ulong domainBasisBladeId2, bool isNegative = false)
        {
            if (!_coefSumsTable.TryGetValue(targetBasisBladeId, out var sum))
            {
                sum = new GaSymMapBilinearCoefSumsTerm(targetBasisBladeId);
                _coefSumsTable[targetBasisBladeId] = sum;
            }

            sum.AddFactor(domainBasisBladeId1, domainBasisBladeId2, isNegative);

            return this;
        }

        public GaSymMapBilinearCoefSums SetFactor(ulong targetBasisBladeId, ulong domainBasisBladeId1, ulong domainBasisBladeId2, Expr factorValue)
        {
            if (!_coefSumsTable.TryGetValue(targetBasisBladeId, out var sum))
            {
                sum = new GaSymMapBilinearCoefSumsTerm(targetBasisBladeId);
                _coefSumsTable[targetBasisBladeId] = sum;
            }

            sum.AddFactor(domainBasisBladeId1, domainBasisBladeId2, factorValue);

            return this;
        }

        public GaSymMapBilinearCoefSums RemoveFactor(ulong targetBasisBladeId, ulong domainBasisBladeId1, ulong domainBasisBladeId2)
        {
            if (!_coefSumsTable.TryGetValue(targetBasisBladeId, out var sum))
                return this;

            sum.RemoveFactor(domainBasisBladeId1, domainBasisBladeId2);

            return this;
        }


        public override IGaSymMultivectorTemp MapToTemp(ulong domainBasisBladeId1, ulong domainBasisBladeId2)
        {
            var resultMv = GaSymMultivector.CreateZeroTemp(TargetVSpaceDimension);

            foreach (var terms in _coefSumsTable.Values)
                resultMv.AddFactor(
                    terms.TargetBasisBladeId,
                    terms[domainBasisBladeId1, domainBasisBladeId2]
                );

            return resultMv;
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var resultMv = GaSymMultivector.CreateZeroTemp(TargetVSpaceDimension);

            foreach (var termValue in _coefSumsTable.Values)
                resultMv.AddFactor(
                    termValue.TargetBasisBladeId,
                    termValue[mv1, mv2]
                );

            return resultMv;
        }

        public override IEnumerable<Tuple<ulong, ulong, IGaSymMultivector>> BasisBladesMaps()
        {
            for (var id1 = 0UL; id1 < DomainGaSpaceDimension; id1++)
            for (var id2 = 0UL; id2 < DomainGaSpaceDimension; id2++)
            {
                var mv = this[id1, id2];

                if (!mv.IsNullOrZero())
                    yield return new Tuple<ulong, ulong, IGaSymMultivector>(id1, id2, mv);
            }
        }


        public MarkdownTable ToMarkdownTable()
        {
            var mdComposer = new MarkdownTable();

            foreach (var term in _coefSumsTable)
            {
                var mdColumn = 
                    mdComposer.AddColumn(
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
