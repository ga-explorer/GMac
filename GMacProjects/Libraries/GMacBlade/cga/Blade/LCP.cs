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
        public cga0001Blade LCP(cga0001Blade blade2)
        {
            if (IsZero || blade2.IsZero || Grade > blade2.Grade)
                return ZeroBlade;
        
            var id = Grade + blade2.Grade * (MaxGrade + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return new cga0001Blade(0, LCP_000(Coefs, blade2.Coefs));
                
                //grade1: 0, grade2: 1
                case 6:
                    return new cga0001Blade(1, LCP_011(Coefs, blade2.Coefs));
                
                //grade1: 0, grade2: 2
                case 12:
                    return new cga0001Blade(2, LCP_022(Coefs, blade2.Coefs));
                
                //grade1: 0, grade2: 3
                case 18:
                    return new cga0001Blade(3, LCP_033(Coefs, blade2.Coefs));
                
                //grade1: 0, grade2: 4
                case 24:
                    return new cga0001Blade(4, LCP_044(Coefs, blade2.Coefs));
                
                //grade1: 0, grade2: 5
                case 30:
                    return new cga0001Blade(5, LCP_055(Coefs, blade2.Coefs));
                
                //grade1: 1, grade2: 1
                case 7:
                    return new cga0001Blade(0, LCP_110(Coefs, blade2.Coefs));
                
                //grade1: 1, grade2: 2
                case 13:
                    return new cga0001Blade(1, LCP_121(Coefs, blade2.Coefs));
                
                //grade1: 1, grade2: 3
                case 19:
                    return new cga0001Blade(2, LCP_132(Coefs, blade2.Coefs));
                
                //grade1: 1, grade2: 4
                case 25:
                    return new cga0001Blade(3, LCP_143(Coefs, blade2.Coefs));
                
                //grade1: 1, grade2: 5
                case 31:
                    return new cga0001Blade(4, LCP_154(Coefs, blade2.Coefs));
                
                //grade1: 2, grade2: 2
                case 14:
                    return new cga0001Blade(0, LCP_220(Coefs, blade2.Coefs));
                
                //grade1: 2, grade2: 3
                case 20:
                    return new cga0001Blade(1, LCP_231(Coefs, blade2.Coefs));
                
                //grade1: 2, grade2: 4
                case 26:
                    return new cga0001Blade(2, LCP_242(Coefs, blade2.Coefs));
                
                //grade1: 2, grade2: 5
                case 32:
                    return new cga0001Blade(3, LCP_253(Coefs, blade2.Coefs));
                
                //grade1: 3, grade2: 3
                case 21:
                    return new cga0001Blade(0, LCP_330(Coefs, blade2.Coefs));
                
                //grade1: 3, grade2: 4
                case 27:
                    return new cga0001Blade(1, LCP_341(Coefs, blade2.Coefs));
                
                //grade1: 3, grade2: 5
                case 33:
                    return new cga0001Blade(2, LCP_352(Coefs, blade2.Coefs));
                
                //grade1: 4, grade2: 4
                case 28:
                    return new cga0001Blade(0, LCP_440(Coefs, blade2.Coefs));
                
                //grade1: 4, grade2: 5
                case 34:
                    return new cga0001Blade(1, LCP_451(Coefs, blade2.Coefs));
                
                //grade1: 5, grade2: 5
                case 35:
                    return new cga0001Blade(0, LCP_550(Coefs, blade2.Coefs));
                
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
