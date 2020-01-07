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
        private static cga0001Blade DP_00(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_000(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(0, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_01(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_011(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_02(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_022(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_03(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_033(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_04(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_044(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_05(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_055(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(5, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_10(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_101(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_11(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_112(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(2, coefs);
            
            
            coefs = EGP_110(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(0, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_12(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_123(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            coefs = EGP_121(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_13(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_134(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            coefs = EGP_132(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_14(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_145(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(5, coefs);
            
            
            coefs = EGP_143(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_15(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_154(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_20(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_202(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_21(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_213(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            coefs = EGP_211(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_22(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_224(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            coefs = EGP_222(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(2, coefs);
            
            
            coefs = EGP_220(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(0, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_23(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_235(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(5, coefs);
            
            
            coefs = EGP_233(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            coefs = EGP_231(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_24(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_244(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            coefs = EGP_242(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_25(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_255(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(5, coefs);
            
            
            coefs = EGP_253(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_30(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_303(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_31(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_314(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            coefs = EGP_312(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_32(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_325(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(5, coefs);
            
            
            coefs = EGP_323(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            coefs = EGP_321(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_33(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_334(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            coefs = EGP_332(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(2, coefs);
            
            
            coefs = EGP_330(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(0, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_34(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_345(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(5, coefs);
            
            
            coefs = EGP_343(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            coefs = EGP_341(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_35(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_354(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            coefs = EGP_352(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_40(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_404(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_41(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_415(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(5, coefs);
            
            
            coefs = EGP_413(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_42(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_424(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            coefs = EGP_422(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_43(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_435(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(5, coefs);
            
            
            coefs = EGP_433(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            coefs = EGP_431(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_44(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_444(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            coefs = EGP_442(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(2, coefs);
            
            
            coefs = EGP_440(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(0, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_45(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_455(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(5, coefs);
            
            
            coefs = EGP_453(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            coefs = EGP_451(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_50(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_505(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(5, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_51(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_514(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_52(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_525(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(5, coefs);
            
            
            coefs = EGP_523(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_53(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_534(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            coefs = EGP_532(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_54(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_545(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(5, coefs);
            
            
            coefs = EGP_543(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(3, coefs);
            
            
            coefs = EGP_541(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        
        private static cga0001Blade DP_55(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGP_554(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga0001Blade(4, coefs);
            
            
            coefs = EGP_552(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga0001Blade(2, coefs);
            
            
            coefs = EGP_550(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga0001Blade(0, coefs);
            
            
            return ZeroBlade;
        }
        
        
        public cga0001Blade DP(cga0001Blade blade2)
        {
            if (IsZero || blade2.IsZero)
                return ZeroBlade;
        
            var id = Grade + blade2.Grade * (MaxGrade + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return DP_00(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 1
                case 6:
                    return DP_01(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 2
                case 12:
                    return DP_02(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 3
                case 18:
                    return DP_03(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 4
                case 24:
                    return DP_04(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 5
                case 30:
                    return DP_05(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 0
                case 1:
                    return DP_10(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 1
                case 7:
                    return DP_11(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 2
                case 13:
                    return DP_12(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 3
                case 19:
                    return DP_13(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 4
                case 25:
                    return DP_14(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 5
                case 31:
                    return DP_15(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 0
                case 2:
                    return DP_20(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 1
                case 8:
                    return DP_21(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 2
                case 14:
                    return DP_22(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 3
                case 20:
                    return DP_23(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 4
                case 26:
                    return DP_24(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 5
                case 32:
                    return DP_25(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 0
                case 3:
                    return DP_30(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 1
                case 9:
                    return DP_31(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 2
                case 15:
                    return DP_32(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 3
                case 21:
                    return DP_33(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 4
                case 27:
                    return DP_34(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 5
                case 33:
                    return DP_35(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 0
                case 4:
                    return DP_40(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 1
                case 10:
                    return DP_41(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 2
                case 16:
                    return DP_42(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 3
                case 22:
                    return DP_43(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 4
                case 28:
                    return DP_44(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 5
                case 34:
                    return DP_45(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 0
                case 5:
                    return DP_50(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 1
                case 11:
                    return DP_51(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 2
                case 17:
                    return DP_52(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 3
                case 23:
                    return DP_53(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 4
                case 29:
                    return DP_54(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 5
                case 35:
                    return DP_55(Coefs, blade2.Coefs);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
    }
}
