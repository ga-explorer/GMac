using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Multivectors;

namespace GeometricAlgebraSymbolicsLib.Products
{
    public static class GaSymProductsUtils
    {
        public static GaSymMultivector Op(this IGaSymMultivector mv1, IGaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            return GaSymMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.ToMultivector().GetBiTermsForOp(mv2.ToMultivector()))
                .ToMultivector();
        }

        public static GaSymMultivector Op(this GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            return GaSymMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForOp(mv2))
                .ToMultivector();
        }

        public static GaSymMultivector EGp(this GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            return GaSymMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEGp(mv2))
                .ToMultivector();
        }

        public static GaSymMultivector ESp(this GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            return GaSymMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForESp(mv2))
                .ToMultivector();
        }

        public static GaSymMultivector ELcp(this GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            return GaSymMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForELcp(mv2))
                .ToMultivector();
        }

        public static GaSymMultivector ERcp(this GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            return GaSymMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForERcp(mv2))
                .ToMultivector();
        }

        public static GaSymMultivector EFdp(this GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            return GaSymMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEFdp(mv2))
                .ToMultivector();
        }

        public static GaSymMultivector EHip(this GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            return GaSymMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEHip(mv2))
                .ToMultivector();
        }

        public static GaSymMultivector EAcp(this GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            return GaSymMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForEAcp(mv2))
                .ToMultivector();
        }

        public static GaSymMultivector ECp(this GaSymMultivector mv1, GaSymMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            return GaSymMultivector
                .CreateZeroTemp(mv1.GaSpaceDimension)
                .AddFactors(mv1.GetBiTermsForECp(mv2))
                .ToMultivector();
        }

        public static MathematicaScalar EMagnitude(this GaSymMultivector mv)
        {
            return ESp(mv, mv.Reverse())[0].ToMathematicaScalar().Sqrt();
        }

        public static MathematicaScalar EMagnitude2(this GaSymMultivector mv)
        {
            return ESp(mv, mv.Reverse())[0].ToMathematicaScalar();
        }
    }
}
