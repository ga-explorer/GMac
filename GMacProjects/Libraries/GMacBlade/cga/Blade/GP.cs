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
        private static cga0001Blade[] GP_00(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, GP_000(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_01(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, GP_011(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_02(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, GP_022(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_03(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, GP_033(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_04(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, GP_044(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_05(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(5, GP_055(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_10(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, GP_101(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_11(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, GP_110(coefs1, coefs2)),
                new cga0001Blade(2, GP_112(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_12(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, GP_121(coefs1, coefs2)),
                new cga0001Blade(3, GP_123(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_13(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, GP_132(coefs1, coefs2)),
                new cga0001Blade(4, GP_134(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_14(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, GP_143(coefs1, coefs2)),
                new cga0001Blade(5, GP_145(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_15(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, GP_154(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_20(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, GP_202(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_21(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, GP_211(coefs1, coefs2)),
                new cga0001Blade(3, GP_213(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_22(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, GP_220(coefs1, coefs2)),
                new cga0001Blade(2, GP_222(coefs1, coefs2)),
                new cga0001Blade(4, GP_224(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_23(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, GP_231(coefs1, coefs2)),
                new cga0001Blade(3, GP_233(coefs1, coefs2)),
                new cga0001Blade(5, GP_235(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_24(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, GP_242(coefs1, coefs2)),
                new cga0001Blade(4, GP_244(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_25(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, GP_253(coefs1, coefs2)),
                new cga0001Blade(5, GP_255(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_30(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, GP_303(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_31(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, GP_312(coefs1, coefs2)),
                new cga0001Blade(4, GP_314(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_32(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, GP_321(coefs1, coefs2)),
                new cga0001Blade(3, GP_323(coefs1, coefs2)),
                new cga0001Blade(5, GP_325(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_33(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, GP_330(coefs1, coefs2)),
                new cga0001Blade(2, GP_332(coefs1, coefs2)),
                new cga0001Blade(4, GP_334(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_34(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, GP_341(coefs1, coefs2)),
                new cga0001Blade(3, GP_343(coefs1, coefs2)),
                new cga0001Blade(5, GP_345(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_35(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, GP_352(coefs1, coefs2)),
                new cga0001Blade(4, GP_354(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_40(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, GP_404(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_41(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, GP_413(coefs1, coefs2)),
                new cga0001Blade(5, GP_415(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_42(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, GP_422(coefs1, coefs2)),
                new cga0001Blade(4, GP_424(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_43(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, GP_431(coefs1, coefs2)),
                new cga0001Blade(3, GP_433(coefs1, coefs2)),
                new cga0001Blade(5, GP_435(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_44(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, GP_440(coefs1, coefs2)),
                new cga0001Blade(2, GP_442(coefs1, coefs2)),
                new cga0001Blade(4, GP_444(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_45(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, GP_451(coefs1, coefs2)),
                new cga0001Blade(3, GP_453(coefs1, coefs2)),
                new cga0001Blade(5, GP_455(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_50(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(5, GP_505(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_51(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, GP_514(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_52(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, GP_523(coefs1, coefs2)),
                new cga0001Blade(5, GP_525(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_53(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, GP_532(coefs1, coefs2)),
                new cga0001Blade(4, GP_534(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_54(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, GP_541(coefs1, coefs2)),
                new cga0001Blade(3, GP_543(coefs1, coefs2)),
                new cga0001Blade(5, GP_545(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] GP_55(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, GP_550(coefs1, coefs2)),
                new cga0001Blade(2, GP_552(coefs1, coefs2)),
                new cga0001Blade(4, GP_554(coefs1, coefs2))
            };
        }
        
        public cga0001Blade[] GP(cga0001Blade blade2)
        {
            if (IsZero || blade2.IsZero)
                return new cga0001Blade[0];
        
            var id = Grade + blade2.Grade * (MaxGrade + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return GP_00(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 1
                case 6:
                    return GP_01(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 2
                case 12:
                    return GP_02(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 3
                case 18:
                    return GP_03(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 4
                case 24:
                    return GP_04(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 5
                case 30:
                    return GP_05(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 0
                case 1:
                    return GP_10(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 1
                case 7:
                    return GP_11(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 2
                case 13:
                    return GP_12(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 3
                case 19:
                    return GP_13(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 4
                case 25:
                    return GP_14(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 5
                case 31:
                    return GP_15(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 0
                case 2:
                    return GP_20(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 1
                case 8:
                    return GP_21(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 2
                case 14:
                    return GP_22(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 3
                case 20:
                    return GP_23(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 4
                case 26:
                    return GP_24(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 5
                case 32:
                    return GP_25(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 0
                case 3:
                    return GP_30(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 1
                case 9:
                    return GP_31(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 2
                case 15:
                    return GP_32(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 3
                case 21:
                    return GP_33(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 4
                case 27:
                    return GP_34(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 5
                case 33:
                    return GP_35(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 0
                case 4:
                    return GP_40(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 1
                case 10:
                    return GP_41(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 2
                case 16:
                    return GP_42(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 3
                case 22:
                    return GP_43(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 4
                case 28:
                    return GP_44(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 5
                case 34:
                    return GP_45(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 0
                case 5:
                    return GP_50(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 1
                case 11:
                    return GP_51(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 2
                case 17:
                    return GP_52(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 3
                case 23:
                    return GP_53(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 4
                case 29:
                    return GP_54(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 5
                case 35:
                    return GP_55(Coefs, blade2.Coefs);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
