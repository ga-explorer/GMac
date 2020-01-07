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
        private static cga0001Blade[] EGPDual_00(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(5, EGPDual_005(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_01(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGPDual_014(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_02(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGPDual_023(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_03(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGPDual_032(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_04(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGPDual_041(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_05(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, EGPDual_050(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_10(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGPDual_104(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_11(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(5, EGPDual_115(coefs1, coefs2)),
                new cga0001Blade(3, EGPDual_113(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_12(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGPDual_124(coefs1, coefs2)),
                new cga0001Blade(2, EGPDual_122(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_13(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGPDual_133(coefs1, coefs2)),
                new cga0001Blade(1, EGPDual_131(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_14(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGPDual_142(coefs1, coefs2)),
                new cga0001Blade(0, EGPDual_140(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_15(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGPDual_151(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_20(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGPDual_203(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_21(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGPDual_214(coefs1, coefs2)),
                new cga0001Blade(2, EGPDual_212(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_22(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(5, EGPDual_225(coefs1, coefs2)),
                new cga0001Blade(3, EGPDual_223(coefs1, coefs2)),
                new cga0001Blade(1, EGPDual_221(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_23(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGPDual_234(coefs1, coefs2)),
                new cga0001Blade(2, EGPDual_232(coefs1, coefs2)),
                new cga0001Blade(0, EGPDual_230(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_24(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGPDual_243(coefs1, coefs2)),
                new cga0001Blade(1, EGPDual_241(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_25(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGPDual_252(coefs1, coefs2)),
                new cga0001Blade(0, EGPDual_250(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_30(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGPDual_302(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_31(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGPDual_313(coefs1, coefs2)),
                new cga0001Blade(1, EGPDual_311(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_32(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGPDual_324(coefs1, coefs2)),
                new cga0001Blade(2, EGPDual_322(coefs1, coefs2)),
                new cga0001Blade(0, EGPDual_320(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_33(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(5, EGPDual_335(coefs1, coefs2)),
                new cga0001Blade(3, EGPDual_333(coefs1, coefs2)),
                new cga0001Blade(1, EGPDual_331(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_34(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGPDual_344(coefs1, coefs2)),
                new cga0001Blade(2, EGPDual_342(coefs1, coefs2)),
                new cga0001Blade(0, EGPDual_340(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_35(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGPDual_353(coefs1, coefs2)),
                new cga0001Blade(1, EGPDual_351(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_40(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGPDual_401(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_41(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGPDual_412(coefs1, coefs2)),
                new cga0001Blade(0, EGPDual_410(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_42(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGPDual_423(coefs1, coefs2)),
                new cga0001Blade(1, EGPDual_421(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_43(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGPDual_434(coefs1, coefs2)),
                new cga0001Blade(2, EGPDual_432(coefs1, coefs2)),
                new cga0001Blade(0, EGPDual_430(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_44(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(5, EGPDual_445(coefs1, coefs2)),
                new cga0001Blade(3, EGPDual_443(coefs1, coefs2)),
                new cga0001Blade(1, EGPDual_441(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_45(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGPDual_454(coefs1, coefs2)),
                new cga0001Blade(2, EGPDual_452(coefs1, coefs2)),
                new cga0001Blade(0, EGPDual_450(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_50(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(0, EGPDual_500(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_51(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(1, EGPDual_511(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_52(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(2, EGPDual_522(coefs1, coefs2)),
                new cga0001Blade(0, EGPDual_520(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_53(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(3, EGPDual_533(coefs1, coefs2)),
                new cga0001Blade(1, EGPDual_531(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_54(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(4, EGPDual_544(coefs1, coefs2)),
                new cga0001Blade(2, EGPDual_542(coefs1, coefs2)),
                new cga0001Blade(0, EGPDual_540(coefs1, coefs2))
            };
        }
        
        private static cga0001Blade[] EGPDual_55(double[] coefs1, double[] coefs2)
        {
            return new[]
            {
                new cga0001Blade(5, EGPDual_555(coefs1, coefs2)),
                new cga0001Blade(3, EGPDual_553(coefs1, coefs2)),
                new cga0001Blade(1, EGPDual_551(coefs1, coefs2))
            };
        }
        
        public cga0001Blade[] EGPDual(cga0001Blade blade2)
        {
            if (IsZero || blade2.IsZero)
                return new cga0001Blade[0];
        
            var id = Grade + blade2.Grade * (MaxGrade + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return EGPDual_00(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 1
                case 6:
                    return EGPDual_01(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 2
                case 12:
                    return EGPDual_02(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 3
                case 18:
                    return EGPDual_03(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 4
                case 24:
                    return EGPDual_04(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 5
                case 30:
                    return EGPDual_05(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 0
                case 1:
                    return EGPDual_10(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 1
                case 7:
                    return EGPDual_11(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 2
                case 13:
                    return EGPDual_12(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 3
                case 19:
                    return EGPDual_13(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 4
                case 25:
                    return EGPDual_14(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 5
                case 31:
                    return EGPDual_15(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 0
                case 2:
                    return EGPDual_20(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 1
                case 8:
                    return EGPDual_21(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 2
                case 14:
                    return EGPDual_22(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 3
                case 20:
                    return EGPDual_23(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 4
                case 26:
                    return EGPDual_24(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 5
                case 32:
                    return EGPDual_25(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 0
                case 3:
                    return EGPDual_30(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 1
                case 9:
                    return EGPDual_31(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 2
                case 15:
                    return EGPDual_32(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 3
                case 21:
                    return EGPDual_33(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 4
                case 27:
                    return EGPDual_34(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 5
                case 33:
                    return EGPDual_35(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 0
                case 4:
                    return EGPDual_40(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 1
                case 10:
                    return EGPDual_41(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 2
                case 16:
                    return EGPDual_42(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 3
                case 22:
                    return EGPDual_43(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 4
                case 28:
                    return EGPDual_44(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 5
                case 34:
                    return EGPDual_45(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 0
                case 5:
                    return EGPDual_50(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 1
                case 11:
                    return EGPDual_51(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 2
                case 17:
                    return EGPDual_52(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 3
                case 23:
                    return EGPDual_53(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 4
                case 29:
                    return EGPDual_54(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 5
                case 35:
                    return EGPDual_55(Coefs, blade2.Coefs);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
