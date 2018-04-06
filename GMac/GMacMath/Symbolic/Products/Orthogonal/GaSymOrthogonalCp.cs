﻿using System.Collections.Generic;
using GMac.GMacMath.Symbolic.Metrics;
using GMac.GMacMath.Symbolic.Multivectors;
using GMac.GMacMath.Symbolic.Multivectors.Intermediate;
using SymbolicInterface.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GMac.GMacMath.Symbolic.Products.Orthogonal
{
    public sealed class GaSymOrthogonalCp : GaSymBilinearProductOrthogonal
    {
        internal GaSymOrthogonalCp(GaSymMetricOrthogonal basisBladesSignatures)
            : base(basisBladesSignatures)
        {
        }


        public override IGaSymMultivectorTemp MapToTemp(int id1, int id2)
        {
            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetGaSpaceDimension);

            if (GMacMathUtils.IsNonZeroECp(id1, id2))
                tempMultivector.SetTermCoef(
                    id1 ^ id2,
                    GMacMathUtils.IsNegativeEGp(id1, id2),
                    OrthogonalMetric[id1 & id2]
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
                    .AddFactors(mv1.GetBiTermsForCp(mv2, OrthogonalMetric));
        }

        public override GaSymMultivectorTerm MapToTerm(int id1, int id2)
        {
            return GaSymMultivectorTerm.CreateTerm(
                TargetGaSpaceDimension,
                id1 ^ id2,
                GMacMathUtils.IsNonZeroECp(id1, id2)
                    ? (GMacMathUtils.IsNegativeEGp(id1, id2)
                        ? Mfs.Minus[OrthogonalMetric[id1 & id2]]
                        : OrthogonalMetric[id1 & id2])
                    : Expr.INT_ZERO
            );
        }
    }
}