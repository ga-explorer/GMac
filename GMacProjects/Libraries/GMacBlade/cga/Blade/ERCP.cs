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
        public cga0001Blade ERCP(cga0001Blade blade2)
        {
            if (IsZero || blade2.IsZero || Grade < blade2.Grade)
                return ZeroBlade;
        
            var id = Grade + blade2.Grade * (MaxGrade + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return new cga0001Blade(0, ERCP_000(Coefs, blade2.Coefs));
                
                //grade1: 1, grade2: 0
                case 1:
                    return new cga0001Blade(1, ERCP_101(Coefs, blade2.Coefs));
                
                //grade1: 1, grade2: 1
                case 7:
                    return new cga0001Blade(0, ERCP_110(Coefs, blade2.Coefs));
                
                //grade1: 2, grade2: 0
                case 2:
                    return new cga0001Blade(2, ERCP_202(Coefs, blade2.Coefs));
                
                //grade1: 2, grade2: 1
                case 8:
                    return new cga0001Blade(1, ERCP_211(Coefs, blade2.Coefs));
                
                //grade1: 2, grade2: 2
                case 14:
                    return new cga0001Blade(0, ERCP_220(Coefs, blade2.Coefs));
                
                //grade1: 3, grade2: 0
                case 3:
                    return new cga0001Blade(3, ERCP_303(Coefs, blade2.Coefs));
                
                //grade1: 3, grade2: 1
                case 9:
                    return new cga0001Blade(2, ERCP_312(Coefs, blade2.Coefs));
                
                //grade1: 3, grade2: 2
                case 15:
                    return new cga0001Blade(1, ERCP_321(Coefs, blade2.Coefs));
                
                //grade1: 3, grade2: 3
                case 21:
                    return new cga0001Blade(0, ERCP_330(Coefs, blade2.Coefs));
                
                //grade1: 4, grade2: 0
                case 4:
                    return new cga0001Blade(4, ERCP_404(Coefs, blade2.Coefs));
                
                //grade1: 4, grade2: 1
                case 10:
                    return new cga0001Blade(3, ERCP_413(Coefs, blade2.Coefs));
                
                //grade1: 4, grade2: 2
                case 16:
                    return new cga0001Blade(2, ERCP_422(Coefs, blade2.Coefs));
                
                //grade1: 4, grade2: 3
                case 22:
                    return new cga0001Blade(1, ERCP_431(Coefs, blade2.Coefs));
                
                //grade1: 4, grade2: 4
                case 28:
                    return new cga0001Blade(0, ERCP_440(Coefs, blade2.Coefs));
                
                //grade1: 5, grade2: 0
                case 5:
                    return new cga0001Blade(5, ERCP_505(Coefs, blade2.Coefs));
                
                //grade1: 5, grade2: 1
                case 11:
                    return new cga0001Blade(4, ERCP_514(Coefs, blade2.Coefs));
                
                //grade1: 5, grade2: 2
                case 17:
                    return new cga0001Blade(3, ERCP_523(Coefs, blade2.Coefs));
                
                //grade1: 5, grade2: 3
                case 23:
                    return new cga0001Blade(2, ERCP_532(Coefs, blade2.Coefs));
                
                //grade1: 5, grade2: 4
                case 29:
                    return new cga0001Blade(1, ERCP_541(Coefs, blade2.Coefs));
                
                //grade1: 5, grade2: 5
                case 35:
                    return new cga0001Blade(0, ERCP_550(Coefs, blade2.Coefs));
                
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
