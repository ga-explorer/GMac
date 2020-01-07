using System.IO;

namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents an immutable blade in the cga0001 frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga0001Blade
    {
        private static bool IsZero1(double[] coefs)
        {
            return !(
                coefs[0] <= -Epsilon || coefs[0] >= Epsilon
                );
        }
        
        private static double[] TrimCoefs1(double[] coefs)
        {
            return new[]
            {
                (coefs[0] <= -Epsilon || coefs[0] >= Epsilon) ? coefs[0] : 0.0D
            };
        }
        
        
        private static bool IsZero5(double[] coefs)
        {
            return !(
                coefs[0] <= -Epsilon || coefs[0] >= Epsilon ||
                coefs[1] <= -Epsilon || coefs[1] >= Epsilon ||
                coefs[2] <= -Epsilon || coefs[2] >= Epsilon ||
                coefs[3] <= -Epsilon || coefs[3] >= Epsilon ||
                coefs[4] <= -Epsilon || coefs[4] >= Epsilon
                );
        }
        
        private static double[] TrimCoefs5(double[] coefs)
        {
            return new[]
            {
                (coefs[0] <= -Epsilon || coefs[0] >= Epsilon) ? coefs[0] : 0.0D,
                (coefs[1] <= -Epsilon || coefs[1] >= Epsilon) ? coefs[1] : 0.0D,
                (coefs[2] <= -Epsilon || coefs[2] >= Epsilon) ? coefs[2] : 0.0D,
                (coefs[3] <= -Epsilon || coefs[3] >= Epsilon) ? coefs[3] : 0.0D,
                (coefs[4] <= -Epsilon || coefs[4] >= Epsilon) ? coefs[4] : 0.0D
            };
        }
        
        
        private static bool IsZero10(double[] coefs)
        {
            return !(
                coefs[0] <= -Epsilon || coefs[0] >= Epsilon ||
                coefs[1] <= -Epsilon || coefs[1] >= Epsilon ||
                coefs[2] <= -Epsilon || coefs[2] >= Epsilon ||
                coefs[3] <= -Epsilon || coefs[3] >= Epsilon ||
                coefs[4] <= -Epsilon || coefs[4] >= Epsilon ||
                coefs[5] <= -Epsilon || coefs[5] >= Epsilon ||
                coefs[6] <= -Epsilon || coefs[6] >= Epsilon ||
                coefs[7] <= -Epsilon || coefs[7] >= Epsilon ||
                coefs[8] <= -Epsilon || coefs[8] >= Epsilon ||
                coefs[9] <= -Epsilon || coefs[9] >= Epsilon
                );
        }
        
        private static double[] TrimCoefs10(double[] coefs)
        {
            return new[]
            {
                (coefs[0] <= -Epsilon || coefs[0] >= Epsilon) ? coefs[0] : 0.0D,
                (coefs[1] <= -Epsilon || coefs[1] >= Epsilon) ? coefs[1] : 0.0D,
                (coefs[2] <= -Epsilon || coefs[2] >= Epsilon) ? coefs[2] : 0.0D,
                (coefs[3] <= -Epsilon || coefs[3] >= Epsilon) ? coefs[3] : 0.0D,
                (coefs[4] <= -Epsilon || coefs[4] >= Epsilon) ? coefs[4] : 0.0D,
                (coefs[5] <= -Epsilon || coefs[5] >= Epsilon) ? coefs[5] : 0.0D,
                (coefs[6] <= -Epsilon || coefs[6] >= Epsilon) ? coefs[6] : 0.0D,
                (coefs[7] <= -Epsilon || coefs[7] >= Epsilon) ? coefs[7] : 0.0D,
                (coefs[8] <= -Epsilon || coefs[8] >= Epsilon) ? coefs[8] : 0.0D,
                (coefs[9] <= -Epsilon || coefs[9] >= Epsilon) ? coefs[9] : 0.0D
            };
        }
        
        
        public bool IsZero
        {
            get
            {
                if (IsZeroBlade)
                    return true;
        
                switch (Grade)
                {
                    case 0:
                        return IsZero1(Coefs);
                    case 1:
                        return IsZero5(Coefs);
                    case 2:
                        return IsZero10(Coefs);
                    case 3:
                        return IsZero10(Coefs);
                    case 4:
                        return IsZero5(Coefs);
                    case 5:
                        return IsZero1(Coefs);
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        /// <summary>
        /// Set all near-zero coefficients to zero. If all coefficients are near zero a ZeroBlade is returned
        /// </summary>
        public cga0001Blade TrimNearZero
        {
            get
            {
                if (IsZero)
                    return ZeroBlade;
        
                switch (Grade)
                {
                    case 0:
                        return new cga0001Blade(0, TrimCoefs1(Coefs));
                    case 1:
                        return new cga0001Blade(1, TrimCoefs5(Coefs));
                    case 2:
                        return new cga0001Blade(2, TrimCoefs10(Coefs));
                    case 3:
                        return new cga0001Blade(3, TrimCoefs10(Coefs));
                    case 4:
                        return new cga0001Blade(4, TrimCoefs5(Coefs));
                    case 5:
                        return new cga0001Blade(5, TrimCoefs1(Coefs));
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
    }
}
