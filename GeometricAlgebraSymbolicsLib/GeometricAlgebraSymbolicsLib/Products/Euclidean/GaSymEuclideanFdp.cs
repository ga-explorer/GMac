﻿using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Products.Euclidean
{
    public sealed class GaSymEuclideanFdp : GaSymBilinearProductEuclidean
    {
        internal GaSymEuclideanFdp(int targetVSpaceDim) : base(targetVSpaceDim)
        {
        }


        public override IGaSymMultivectorTemp MapToTemp(ulong id1, ulong id2)
        {
            var tempMultivector = GaSymMultivector.CreateZeroTemp(TargetVSpaceDimension);

            if (GaFrameUtils.IsNonZeroEFdp(id1, id2))
                tempMultivector.SetTermCoef(
                    id1 ^ id2,
                    GaFrameUtils.IsNegativeEGp(id1, id2), 
                    Expr.INT_ONE
                );

            return tempMultivector;
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension2)
                throw new GaSymbolicsException("Multivector size mismatch");

            return GaSymMultivector
                .CreateZeroTemp(TargetVSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEFdp(mv2));
        }

        public override GaSymMultivectorTerm MapToTerm(ulong id1, ulong id2)
        {
            return GaSymMultivectorTerm.CreateTerm(
                TargetVSpaceDimension,
                id1 ^ id2,
                GaFrameUtils.IsNonZeroEFdp(id1, id2)
                    ? (GaFrameUtils.IsNegativeEGp(id1, id2) ? Expr.INT_MINUSONE : Expr.INT_ONE)
                    : Expr.INT_ZERO
            );
        }
    }
}