using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Products.NonOrthogonal
{
    public sealed class GaNumNonOrthogonalGp : GaNumBilinearProductNonOrthogonal
    {
        public override IGaNumMultivector this[int id1, int id2]
        {
            get
            {
                var resultMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);

                var f1 = NonOrthogonalMetric.DerivedToBaseCba[id1].GetSarMultivector();
                var f2 = NonOrthogonalMetric.DerivedToBaseCba[id2].GetSarMultivector();

                resultMv.AddTerms(
                    NonOrthogonalMetric.BaseFrame.Gp[f1, f2]
                );

                return NonOrthogonalMetric.BaseToDerivedCba[resultMv.GetSarMultivector()];
            }
        }

        public override GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                var resultMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);

                foreach (var term1 in mv1.GetNonZeroTerms())
                {
                    var f1 = NonOrthogonalMetric.DerivedToBaseCba[term1.BasisBladeId];

                    foreach (var term2 in mv2.GetNonZeroTerms())
                    {
                        var f2 = NonOrthogonalMetric.DerivedToBaseCba[term2.BasisBladeId];

                        foreach (var basisTerm1 in f1.GetNonZeroTerms())
                        {
                            foreach (var basisTerm2 in f2.GetNonZeroTerms())
                            {
                                var id = basisTerm1.BasisBladeId ^ basisTerm2.BasisBladeId;
                                var scalar =
                                    term1.ScalarValue * term2.ScalarValue *
                                    basisTerm1.ScalarValue * basisTerm2.ScalarValue *
                                    NonOrthogonalMetric.BaseMetric.GetBasisBladeSignature(basisTerm1.BasisBladeId & basisTerm2.BasisBladeId);

                                if (GaFrameUtils.IsNegativeEGp(basisTerm1.BasisBladeId, basisTerm2.BasisBladeId))
                                    scalar = -scalar;

                                resultMv.AddTerm(id, scalar);
                            }
                        }
                    }
                }

                return NonOrthogonalMetric.BaseToDerivedCba[resultMv.GetSarMultivector()];
            }
        }

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                var resultMv = new GaNumDgrMultivectorFactory(TargetGaSpaceDimension);

                foreach (var term1 in mv1.GetNonZeroTerms())
                {
                    var f1 = NonOrthogonalMetric.DerivedToBaseCba[term1.BasisBladeId];

                    foreach (var term2 in mv2.GetNonZeroTerms())
                    {
                        var f2 = NonOrthogonalMetric.DerivedToBaseCba[term2.BasisBladeId];

                        foreach (var basisTerm1 in f1.GetNonZeroTerms())
                        {
                            foreach (var basisTerm2 in f2.GetNonZeroTerms())
                            {
                                var id = basisTerm1.BasisBladeId ^ basisTerm2.BasisBladeId;
                                var scalar =
                                    term1.ScalarValue * term2.ScalarValue *
                                    basisTerm1.ScalarValue * basisTerm2.ScalarValue *
                                    NonOrthogonalMetric.BaseMetric.GetBasisBladeSignature(basisTerm1.BasisBladeId & basisTerm2.BasisBladeId);

                                if (GaFrameUtils.IsNegativeEGp(basisTerm1.BasisBladeId, basisTerm2.BasisBladeId))
                                    scalar = -scalar;

                                resultMv.AddTerm(id, scalar);
                            }
                        }
                    }
                }

                return NonOrthogonalMetric.BaseToDerivedCba[resultMv.GetDgrMultivector()];
            }
        }

        internal GaNumNonOrthogonalGp(GaNumMetricNonOrthogonal basisBladesSignatures)
            : base(basisBladesSignatures, GaNumMapBilinearAssociativity.LeftRightAssociative)
        {
        }
    }
}