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
        private static double[] Negative1(double[] coefs)
        {
            return new[]
            {
                -coefs[0]
            };
        }
        
        
        private static double[] Negative5(double[] coefs)
        {
            return new[]
            {
                -coefs[0],
                -coefs[1],
                -coefs[2],
                -coefs[3],
                -coefs[4]
            };
        }
        
        
        private static double[] Negative10(double[] coefs)
        {
            return new[]
            {
                -coefs[0],
                -coefs[1],
                -coefs[2],
                -coefs[3],
                -coefs[4],
                -coefs[5],
                -coefs[6],
                -coefs[7],
                -coefs[8],
                -coefs[9]
            };
        }
        
        
        public cga0001Blade Negative
        {
            get
            {
                if (IsZeroBlade)
                    return this;
        
                switch (Grade)
                {
                    case 0:
                        return new cga0001Blade(0, Negative1(Coefs));
                    
                    case 1:
                        return new cga0001Blade(1, Negative5(Coefs));
                    
                    case 2:
                        return new cga0001Blade(2, Negative10(Coefs));
                    
                    case 3:
                        return new cga0001Blade(3, Negative10(Coefs));
                    
                    case 4:
                        return new cga0001Blade(4, Negative5(Coefs));
                    
                    case 5:
                        return new cga0001Blade(5, Negative1(Coefs));
                    
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        public cga0001Blade Reverse
        {
            get
            {
                if (IsZeroBlade)
                    return this;
        
                switch (Grade)
                {
                    case 0:
                        return this;
                    
                    case 1:
                        return this;
                    
                    case 2:
                        return new cga0001Blade(2, Negative10(Coefs));
                    
                    case 3:
                        return new cga0001Blade(3, Negative10(Coefs));
                    
                    case 4:
                        return this;
                    
                    case 5:
                        return this;
                    
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        public cga0001Blade GradeInv
        {
            get
            {
                if (IsZeroBlade)
                    return this;
        
                switch (Grade)
                {
                    case 0:
                        return this;
                    
                    case 1:
                        return new cga0001Blade(1, Negative5(Coefs));
                    
                    case 2:
                        return this;
                    
                    case 3:
                        return new cga0001Blade(3, Negative10(Coefs));
                    
                    case 4:
                        return this;
                    
                    case 5:
                        return new cga0001Blade(5, Negative1(Coefs));
                    
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
        public cga0001Blade CliffConj
        {
            get
            {
                if (IsZeroBlade)
                    return this;
        
                switch (Grade)
                {
                    case 0:
                        return this;
                    
                    case 1:
                        return new cga0001Blade(1, Negative5(Coefs));
                    
                    case 2:
                        return new cga0001Blade(2, Negative10(Coefs));
                    
                    case 3:
                        return this;
                    
                    case 4:
                        return this;
                    
                    case 5:
                        return new cga0001Blade(5, Negative1(Coefs));
                    
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
        
    }
}
