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
        public cga0001Blade ApplyERotor(cga0001Blade blade2)
        {
            if (blade2.IsZero)
                return ZeroBlade;
        
            var id = Grade + blade2.Grade * (MaxGrade + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return new cga0001Blade(0, ApplyERotor_000(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 1
                case 6:
                    return new cga0001Blade(1, ApplyERotor_011(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 2
                case 12:
                    return new cga0001Blade(2, ApplyERotor_022(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 3
                case 18:
                    return new cga0001Blade(3, ApplyERotor_033(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 4
                case 24:
                    return new cga0001Blade(4, ApplyERotor_044(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 5
                case 30:
                    return new cga0001Blade(5, ApplyERotor_055(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 0
                case 1:
                    return new cga0001Blade(0, ApplyERotor_100(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 1
                case 7:
                    return new cga0001Blade(1, ApplyERotor_111(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 2
                case 13:
                    return new cga0001Blade(2, ApplyERotor_122(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 3
                case 19:
                    return new cga0001Blade(3, ApplyERotor_133(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 4
                case 25:
                    return new cga0001Blade(4, ApplyERotor_144(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 5
                case 31:
                    return new cga0001Blade(5, ApplyERotor_155(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 0
                case 2:
                    return new cga0001Blade(0, ApplyERotor_200(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 1
                case 8:
                    return new cga0001Blade(1, ApplyERotor_211(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 2
                case 14:
                    return new cga0001Blade(2, ApplyERotor_222(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 3
                case 20:
                    return new cga0001Blade(3, ApplyERotor_233(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 4
                case 26:
                    return new cga0001Blade(4, ApplyERotor_244(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 5
                case 32:
                    return new cga0001Blade(5, ApplyERotor_255(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 0
                case 3:
                    return new cga0001Blade(0, ApplyERotor_300(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 1
                case 9:
                    return new cga0001Blade(1, ApplyERotor_311(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 2
                case 15:
                    return new cga0001Blade(2, ApplyERotor_322(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 3
                case 21:
                    return new cga0001Blade(3, ApplyERotor_333(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 4
                case 27:
                    return new cga0001Blade(4, ApplyERotor_344(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 5
                case 33:
                    return new cga0001Blade(5, ApplyERotor_355(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 0
                case 4:
                    return new cga0001Blade(0, ApplyERotor_400(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 1
                case 10:
                    return new cga0001Blade(1, ApplyERotor_411(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 2
                case 16:
                    return new cga0001Blade(2, ApplyERotor_422(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 3
                case 22:
                    return new cga0001Blade(3, ApplyERotor_433(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 4
                case 28:
                    return new cga0001Blade(4, ApplyERotor_444(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 5
                case 34:
                    return new cga0001Blade(5, ApplyERotor_455(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 0
                case 5:
                    return new cga0001Blade(0, ApplyERotor_500(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 1
                case 11:
                    return new cga0001Blade(1, ApplyERotor_511(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 2
                case 17:
                    return new cga0001Blade(2, ApplyERotor_522(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 3
                case 23:
                    return new cga0001Blade(3, ApplyERotor_533(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 4
                case 29:
                    return new cga0001Blade(4, ApplyERotor_544(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 5
                case 35:
                    return new cga0001Blade(5, ApplyERotor_555(Coefs, blade2.Coefs));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
