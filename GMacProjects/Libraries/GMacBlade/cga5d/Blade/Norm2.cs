using System.IO;

namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents an immutable blade in the cga5d frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of the blade as a 
    /// linear combination of basis blades of the same grade (i.e. it's actually a k-vector representation).
    /// </summary>
    public sealed partial class cga5dBlade
    {
        public double Norm2
        {
            get
            {
                if (IsZeroBlade)
                    return 0.0D;
        
                switch (Grade)
                {
                    case 0:
                        return Norm2_0(Coefs);
                    case 1:
                        return Norm2_1(Coefs);
                    case 2:
                        return Norm2_2(Coefs);
                    case 3:
                        return Norm2_3(Coefs);
                    case 4:
                        return Norm2_4(Coefs);
                    case 5:
                        return Norm2_5(Coefs);
                }
        
                throw new InvalidDataException("Internal error. Blade grade not acceptable!");
            }
        }
        
    }
}
