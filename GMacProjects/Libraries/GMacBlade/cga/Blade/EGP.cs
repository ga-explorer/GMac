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
        private static cga0001Blade[] EGP_00(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, EGP_000(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_01(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGP_011(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_02(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGP_022(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_03(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGP_033(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_04(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGP_044(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_05(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(5, EGP_055(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_10(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGP_101(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_11(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, EGP_110(coefs1, coefs2)),
                new cga0001Blade(2, EGP_112(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_12(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGP_121(coefs1, coefs2)),
                new cga0001Blade(3, EGP_123(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_13(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGP_132(coefs1, coefs2)),
                new cga0001Blade(4, EGP_134(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_14(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGP_143(coefs1, coefs2)),
                new cga0001Blade(5, EGP_145(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_15(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGP_154(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_20(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGP_202(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_21(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGP_211(coefs1, coefs2)),
                new cga0001Blade(3, EGP_213(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_22(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, EGP_220(coefs1, coefs2)),
                new cga0001Blade(2, EGP_222(coefs1, coefs2)),
                new cga0001Blade(4, EGP_224(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_23(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGP_231(coefs1, coefs2)),
                new cga0001Blade(3, EGP_233(coefs1, coefs2)),
                new cga0001Blade(5, EGP_235(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_24(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGP_242(coefs1, coefs2)),
                new cga0001Blade(4, EGP_244(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_25(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGP_253(coefs1, coefs2)),
                new cga0001Blade(5, EGP_255(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_30(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGP_303(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_31(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGP_312(coefs1, coefs2)),
                new cga0001Blade(4, EGP_314(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_32(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGP_321(coefs1, coefs2)),
                new cga0001Blade(3, EGP_323(coefs1, coefs2)),
                new cga0001Blade(5, EGP_325(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_33(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, EGP_330(coefs1, coefs2)),
                new cga0001Blade(2, EGP_332(coefs1, coefs2)),
                new cga0001Blade(4, EGP_334(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_34(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGP_341(coefs1, coefs2)),
                new cga0001Blade(3, EGP_343(coefs1, coefs2)),
                new cga0001Blade(5, EGP_345(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_35(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGP_352(coefs1, coefs2)),
                new cga0001Blade(4, EGP_354(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_40(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGP_404(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_41(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGP_413(coefs1, coefs2)),
                new cga0001Blade(5, EGP_415(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_42(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGP_422(coefs1, coefs2)),
                new cga0001Blade(4, EGP_424(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_43(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGP_431(coefs1, coefs2)),
                new cga0001Blade(3, EGP_433(coefs1, coefs2)),
                new cga0001Blade(5, EGP_435(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_44(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, EGP_440(coefs1, coefs2)),
                new cga0001Blade(2, EGP_442(coefs1, coefs2)),
                new cga0001Blade(4, EGP_444(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_45(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGP_451(coefs1, coefs2)),
                new cga0001Blade(3, EGP_453(coefs1, coefs2)),
                new cga0001Blade(5, EGP_455(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_50(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(5, EGP_505(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_51(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGP_514(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_52(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGP_523(coefs1, coefs2)),
                new cga0001Blade(5, EGP_525(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_53(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGP_532(coefs1, coefs2)),
                new cga0001Blade(4, EGP_534(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_54(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGP_541(coefs1, coefs2)),
                new cga0001Blade(3, EGP_543(coefs1, coefs2)),
                new cga0001Blade(5, EGP_545(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGP_55(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, EGP_550(coefs1, coefs2)),
                new cga0001Blade(2, EGP_552(coefs1, coefs2)),
                new cga0001Blade(4, EGP_554(coefs1, coefs2))
            };
        }
        
        public cga0001Blade[] EGP(cga0001Blade blade2)
        {
            if (IsZero || blade2.IsZero)
                return new cga0001Blade[0];
        
            var id = Grade + blade2.Grade * (MaxGrade + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return EGP_00(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 1
                case 6:
                    return EGP_01(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 2
                case 12:
                    return EGP_02(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 3
                case 18:
                    return EGP_03(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 4
                case 24:
                    return EGP_04(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 5
                case 30:
                    return EGP_05(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 0
                case 1:
                    return EGP_10(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 1
                case 7:
                    return EGP_11(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 2
                case 13:
                    return EGP_12(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 3
                case 19:
                    return EGP_13(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 4
                case 25:
                    return EGP_14(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 5
                case 31:
                    return EGP_15(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 0
                case 2:
                    return EGP_20(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 1
                case 8:
                    return EGP_21(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 2
                case 14:
                    return EGP_22(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 3
                case 20:
                    return EGP_23(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 4
                case 26:
                    return EGP_24(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 5
                case 32:
                    return EGP_25(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 0
                case 3:
                    return EGP_30(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 1
                case 9:
                    return EGP_31(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 2
                case 15:
                    return EGP_32(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 3
                case 21:
                    return EGP_33(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 4
                case 27:
                    return EGP_34(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 5
                case 33:
                    return EGP_35(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 0
                case 4:
                    return EGP_40(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 1
                case 10:
                    return EGP_41(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 2
                case 16:
                    return EGP_42(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 3
                case 22:
                    return EGP_43(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 4
                case 28:
                    return EGP_44(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 5
                case 34:
                    return EGP_45(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 0
                case 5:
                    return EGP_50(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 1
                case 11:
                    return EGP_51(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 2
                case 17:
                    return EGP_52(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 3
                case 23:
                    return EGP_53(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 4
                case 29:
                    return EGP_54(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 5
                case 35:
                    return EGP_55(Coefs, blade2.Coefs);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
