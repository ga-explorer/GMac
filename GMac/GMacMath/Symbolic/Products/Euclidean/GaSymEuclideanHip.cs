using System.Collections.Generic;
using GMac.GMacMath.Symbolic.Multivectors;
using GMac.GMacMath.Symbolic.Multivectors.Intermediate;
using Wolfram.NETLink;

namespace GMac.GMacMath.Symbolic.Products.Euclidean
{
    public sealed class GaSymEuclideanHip : GaSymBilinearProductEuclidean
    {
        internal GaSymEuclideanHip(int targetVSpaceDim) : base(targetVSpaceDim)
        {
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1, int id2)
        {
            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            if (GMacMathUtils.IsNonZeroEHip(id1, id2))
                tempMultivector.SetTermCoef(
                    id1 ^ id2,
                    GMacMathUtils.IsNegativeEGp(id1, id2), 
                    Expr.INT_ONE
                );

            return tempMultivector;
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                throw new GMacSymbolicException("Multivector size mismatch");

            return GaSymMultivector
                .CreateZeroTemp(TargetGaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEHip(mv2));
        }

        public override GaSymMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaSymMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                id1 ^ id2,
                GMacMathUtils.IsNonZeroEHip(id1, id2)
                    ? (GMacMathUtils.IsNegativeEGp(id1, id2) ? Expr.INT_MINUSONE : Expr.INT_ONE)
                    : Expr.INT_ZERO
            );
        }
    }
}