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
        public cga5dBlade ApplyVersor(cga5dBlade blade2)
        {
            if (blade2.IsZero)
                return ZeroBlade;
        
            var id = Grade + blade2.Grade * (MaxGrade + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return new cga5dBlade(0, ApplyVersor_000(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 1
                case 6:
                    return new cga5dBlade(1, ApplyVersor_011(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 2
                case 12:
                    return new cga5dBlade(2, ApplyVersor_022(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 3
                case 18:
                    return new cga5dBlade(3, ApplyVersor_033(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 4
                case 24:
                    return new cga5dBlade(4, ApplyVersor_044(Coefs, blade2.Coefs));
                //grade1: 0, grade2: 5
                case 30:
                    return new cga5dBlade(5, ApplyVersor_055(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 0
                case 1:
                    return new cga5dBlade(0, ApplyVersor_100(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 1
                case 7:
                    return new cga5dBlade(1, ApplyVersor_111(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 2
                case 13:
                    return new cga5dBlade(2, ApplyVersor_122(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 3
                case 19:
                    return new cga5dBlade(3, ApplyVersor_133(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 4
                case 25:
                    return new cga5dBlade(4, ApplyVersor_144(Coefs, blade2.Coefs));
                //grade1: 1, grade2: 5
                case 31:
                    return new cga5dBlade(5, ApplyVersor_155(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 0
                case 2:
                    return new cga5dBlade(0, ApplyVersor_200(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 1
                case 8:
                    return new cga5dBlade(1, ApplyVersor_211(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 2
                case 14:
                    return new cga5dBlade(2, ApplyVersor_222(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 3
                case 20:
                    return new cga5dBlade(3, ApplyVersor_233(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 4
                case 26:
                    return new cga5dBlade(4, ApplyVersor_244(Coefs, blade2.Coefs));
                //grade1: 2, grade2: 5
                case 32:
                    return new cga5dBlade(5, ApplyVersor_255(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 0
                case 3:
                    return new cga5dBlade(0, ApplyVersor_300(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 1
                case 9:
                    return new cga5dBlade(1, ApplyVersor_311(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 2
                case 15:
                    return new cga5dBlade(2, ApplyVersor_322(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 3
                case 21:
                    return new cga5dBlade(3, ApplyVersor_333(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 4
                case 27:
                    return new cga5dBlade(4, ApplyVersor_344(Coefs, blade2.Coefs));
                //grade1: 3, grade2: 5
                case 33:
                    return new cga5dBlade(5, ApplyVersor_355(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 0
                case 4:
                    return new cga5dBlade(0, ApplyVersor_400(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 1
                case 10:
                    return new cga5dBlade(1, ApplyVersor_411(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 2
                case 16:
                    return new cga5dBlade(2, ApplyVersor_422(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 3
                case 22:
                    return new cga5dBlade(3, ApplyVersor_433(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 4
                case 28:
                    return new cga5dBlade(4, ApplyVersor_444(Coefs, blade2.Coefs));
                //grade1: 4, grade2: 5
                case 34:
                    return new cga5dBlade(5, ApplyVersor_455(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 0
                case 5:
                    return new cga5dBlade(0, ApplyVersor_500(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 1
                case 11:
                    return new cga5dBlade(1, ApplyVersor_511(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 2
                case 17:
                    return new cga5dBlade(2, ApplyVersor_522(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 3
                case 23:
                    return new cga5dBlade(3, ApplyVersor_533(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 4
                case 29:
                    return new cga5dBlade(4, ApplyVersor_544(Coefs, blade2.Coefs));
                //grade1: 5, grade2: 5
                case 35:
                    return new cga5dBlade(5, ApplyVersor_555(Coefs, blade2.Coefs));
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
    }
}
