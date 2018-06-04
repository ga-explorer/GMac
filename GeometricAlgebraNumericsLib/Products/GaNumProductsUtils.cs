using System;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GeometricAlgebraNumericsLib.Products
{
    public static class GaNumProductsUtils
    {
        public static GaNumMultivector Op(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForOp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector EGp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEGp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector ESp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForESp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector ELcp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForELcp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector ERcp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForERcp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector EFdp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEFdp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector EHip(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEHip(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector EAcp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEAcp(mv2))
                .ToMultivector();
        }

        public static GaNumMultivector ECp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return GaNumMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForECp(mv2))
                .ToMultivector();
        }

        public static double EMagnitude(this GaNumMultivector mv)
        {
            return Math.Sqrt(ESp(mv, mv.Reverse())[0]);
        }

        public static double EMagnitude2(this GaNumMultivector mv)
        {
            return ESp(mv, mv.Reverse())[0];
        }
    }
}
