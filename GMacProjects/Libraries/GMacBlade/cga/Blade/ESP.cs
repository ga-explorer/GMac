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
        public cga0001Blade ESP(cga0001Blade blade2)
        {
            if (IsZero || blade2.IsZero || Grade != blade2.Grade)
                return ZeroBlade;
        
            var id = Grade + blade2.Grade * (MaxGrade + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return new cga0001Blade(0, ESP_000(Coefs, blade2.Coefs));
                
                //grade1: 1, grade2: 1
                case 7:
                    return new cga0001Blade(0, ESP_110(Coefs, blade2.Coefs));
                
                //grade1: 2, grade2: 2
                case 14:
                    return new cga0001Blade(0, ESP_220(Coefs, blade2.Coefs));
                
                //grade1: 3, grade2: 3
                case 21:
                    return new cga0001Blade(0, ESP_330(Coefs, blade2.Coefs));
                
                //grade1: 4, grade2: 4
                case 28:
                    return new cga0001Blade(0, ESP_440(Coefs, blade2.Coefs));
                
                //grade1: 5, grade2: 5
                case 35:
                    return new cga0001Blade(0, ESP_550(Coefs, blade2.Coefs));
                
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
