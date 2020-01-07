using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Bilinear;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Products.NonOrthogonal
{
    public sealed class GaNumNonOrthogonalGp : GaNumBilinearProductNonOrthogonal
    {
        public override IGaNumMultivector this[int id1, int id2]
        {
            get
            {
                var resultMv = GaNumMultivector.CreateZero(TargetGaSpaceDimension);

                var f1 = NonOrthogonalMetric.DerivedToBaseCba[id1].ToMultivector();
                var f2 = NonOrthogonalMetric.DerivedToBaseCba[id2].ToMultivector();

                resultMv.AddFactors(
                    NonOrthogonalMetric.BaseFrame.Gp[f1, f2]
                );

                return NonOrthogonalMetric.BaseToDerivedCba[resultMv];
            }
        }

        public override GaNumMultivector this[GaNumMultivector mv1, GaNumMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                    throw new GaNumericsException("Multivector size mismatch");

                var resultMv = GaNumMultivector.CreateZero(TargetGaSpaceDimension);

                foreach (var term1 in mv1.Terms)
                {
                    var f1 = NonOrthogonalMetric.DerivedToBaseCba[term1.Key];

                    foreach (var term2 in mv2.Terms)
                    {
                        var f2 = NonOrthogonalMetric.DerivedToBaseCba[term2.Key];

                        foreach (var basisTerm1 in f1.Terms)
                        {
                            foreach (var basisTerm2 in f2.Terms)
                            {
                                var id = basisTerm1.Key ^ basisTerm2.Key;
                                var scalar =
                                    term1.Value * term2.Value *
                                    basisTerm1.Value * basisTerm2.Value *
                                    NonOrthogonalMetric.BaseMetric.GetBasisBladeSignature(basisTerm1.Key & basisTerm2.Key);

                                if (GaNumFrameUtils.IsNegativeEGp(basisTerm1.Key, basisTerm2.Key))
                                    scalar = -scalar;

                                resultMv.UpdateTerm(id, scalar);
                            }
                        }
                    }
                }

                return NonOrthogonalMetric.BaseToDerivedCba[resultMv];
            }
        }


        internal GaNumNonOrthogonalGp(GaNumMetricNonOrthogonal basisBladesSignatures)
            : base(basisBladesSignatures, GaNumMapBilinearAssociativity.LeftRightAssociative)
        {
        }
    }
}