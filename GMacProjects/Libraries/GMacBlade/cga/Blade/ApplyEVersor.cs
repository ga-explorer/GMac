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
        public cga0001Blade ApplyEVersor(cga0001Blade blade2)
        {
            if (blade2.IsZero)
                return ZeroBlade;
        
            var id = Grade + blade2.Grade * (MaxGrade + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return new cga0001Blade(0, ApplyEVersor_000(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 1
                case 6:
                    return new cga0001Blade(1, ApplyEVersor_011(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 2
                case 12:
                    return new cga0001Blade(2, ApplyEVersor_022(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 3
                case 18:
                    return new cga0001Blade(3, ApplyEVersor_033(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 4
                case 24:
                    return new cga0001Blade(4, ApplyEVersor_044(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 5
                case 30:
                    return new cga0001Blade(5, ApplyEVersor_055(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 0
                case 1:
                    return new cga0001Blade(0, ApplyEVersor_100(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 1
                case 7:
                    return new cga0001Blade(1, ApplyEVersor_111(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 2
                case 13:
                    return new cga0001Blade(2, ApplyEVersor_122(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 3
                case 19:
                    return new cga0001Blade(3, ApplyEVersor_133(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 4
                case 25:
                    return new cga0001Blade(4, ApplyEVersor_144(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 5
                case 31:
                    return new cga0001Blade(5, ApplyEVersor_155(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 0
                case 2:
                    return new cga0001Blade(0, ApplyEVersor_200(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 1
                case 8:
                    return new cga0001Blade(1, ApplyEVersor_211(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 2
                case 14:
                    return new cga0001Blade(2, ApplyEVersor_222(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 3
                case 20:
                    return new cga0001Blade(3, ApplyEVersor_233(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 4
                case 26:
                    return new cga0001Blade(4, ApplyEVersor_244(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 5
                case 32:
                    return new cga0001Blade(5, ApplyEVersor_255(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 0
                case 3:
                    return new cga0001Blade(0, ApplyEVersor_300(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 1
                case 9:
                    return new cga0001Blade(1, ApplyEVersor_311(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 2
                case 15:
                    return new cga0001Blade(2, ApplyEVersor_322(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 3
                case 21:
                    return new cga0001Blade(3, ApplyEVersor_333(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 4
                case 27:
                    return new cga0001Blade(4, ApplyEVersor_344(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 5
                case 33:
                    return new cga0001Blade(5, ApplyEVersor_355(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 0
                case 4:
                    return new cga0001Blade(0, ApplyEVersor_400(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 1
                case 10:
                    return new cga0001Blade(1, ApplyEVersor_411(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 2
                case 16:
                    return new cga0001Blade(2, ApplyEVersor_422(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 3
                case 22:
                    return new cga0001Blade(3, ApplyEVersor_433(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 4
                case 28:
                    return new cga0001Blade(4, ApplyEVersor_444(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 5
                case 34:
                    return new cga0001Blade(5, ApplyEVersor_455(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 0
                case 5:
                    return new cga0001Blade(0, ApplyEVersor_500(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 1
                case 11:
                    return new cga0001Blade(1, ApplyEVersor_511(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 2
                case 17:
                    return new cga0001Blade(2, ApplyEVersor_522(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 3
                case 23:
                    return new cga0001Blade(3, ApplyEVersor_533(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 4
                case 29:
                    return new cga0001Blade(4, ApplyEVersor_544(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 5
                case 35:
                    return new cga0001Blade(5, ApplyEVersor_555(Coefs, blade2.Coefs));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
