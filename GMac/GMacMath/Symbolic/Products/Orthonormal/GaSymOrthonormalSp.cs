using System.Collections.Generic;
using GMac.GMacMath.Symbolic.Metrics;
using GMac.GMacMath.Symbolic.Multivectors;
using GMac.GMacMath.Symbolic.Multivectors.Intermediate;
using SymbolicInterface.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GMac.GMacMath.Symbolic.Products.Orthonormal
{
    public sealed class GaSymOrthonormalSp : GaSymBilinearProductOrthonormal
    {
        internal GaSymOrthonormalSp(GaSymMetricOrthonormal basisBladesSignatures)
            : base(basisBladesSignatures)
        {
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1, int id2)
        {
            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            if (GMacMathUtils.IsNonZeroESp(id1, id2))
                tempMultivector.SetTermCoef(
                    0,
                    GMacMathUtils.IsNegativeEGp(id1, id1),
                    OrthonormalMetric.GetExprSignature(id1)
                );

            return tempMultivector;
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                throw new GMacSymbolicException("Multivector size mismatch");

            return
                GaSymMultivector
                    .CreateZeroTemp(TargetGaSpaceDimension)
                    .AddFactors(
                        mv1.GetBiTermsForESp(mv2),
                        OrthonormalMetric
                    );
        }

        public override GaSymMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaSymMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                0,
                GMacMathUtils.IsNonZeroESp(id1, id2)
                    ? (GMacMathUtils.IsNegativeEGp(id1, id1)
                        ? Mfs.Minus[OrthonormalMetric.GetExprSignature(id1)]
                        : OrthonormalMetric.GetExprSignature(id1))
                    : Expr.INT_ZERO
            );
        }
    }
}