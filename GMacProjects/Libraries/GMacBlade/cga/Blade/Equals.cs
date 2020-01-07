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
        private static bool Equals1(double[] coefs1, double[] coefs2)
        {
            double c;
        
            c = coefs1[0] - coefs2[0];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            return true;
        }
        
        
        private static bool Equals5(double[] coefs1, double[] coefs2)
        {
            double c;
        
            c = coefs1[0] - coefs2[0];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            c = coefs1[1] - coefs2[1];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            c = coefs1[2] - coefs2[2];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            c = coefs1[3] - coefs2[3];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            c = coefs1[4] - coefs2[4];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            return true;
        }
        
        
        private static bool Equals10(double[] coefs1, double[] coefs2)
        {
            double c;
        
            c = coefs1[0] - coefs2[0];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            c = coefs1[1] - coefs2[1];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            c = coefs1[2] - coefs2[2];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            c = coefs1[3] - coefs2[3];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            c = coefs1[4] - coefs2[4];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            c = coefs1[5] - coefs2[5];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            c = coefs1[6] - coefs2[6];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            c = coefs1[7] - coefs2[7];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            c = coefs1[8] - coefs2[8];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            c = coefs1[9] - coefs2[9];
            if (c <= -Epsilon || c >= Epsilon) return false;
            
            return true;
        }
        
        
        public bool Equals(cga0001Blade blade2)
        {
            if ((object)blade2 == null) 
                return false;
        
            if (ReferenceEquals(this, blade2)) 
                return true;
        
            if (IsZeroBlade) 
                return blade2.IsZero;
        
            if (blade2.IsZeroBlade) 
                return IsZero;
        
            if (Grade != blade2.Grade) 
                return false;
        
            switch (Grade)
            {
                case 0:
                    return Equals1(Coefs, blade2.Coefs);
                case 1:
                    return Equals5(Coefs, blade2.Coefs);
                case 2:
                    return Equals10(Coefs, blade2.Coefs);
                case 3:
                    return Equals10(Coefs, blade2.Coefs);
                case 4:
                    return Equals5(Coefs, blade2.Coefs);
                case 5:
                    return Equals1(Coefs, blade2.Coefs);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
