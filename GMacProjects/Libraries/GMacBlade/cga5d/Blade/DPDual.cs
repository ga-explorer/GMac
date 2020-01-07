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
        private static cga5dBlade DPDual_00(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_005(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(5, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_01(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_014(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_02(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_023(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_03(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_032(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_04(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_041(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_05(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_050(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(0, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_10(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_104(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_11(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_113(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(3, coefs);
            
            
            coefs = EGPDual_115(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(5, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_12(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_122(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            coefs = EGPDual_124(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_13(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_131(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            coefs = EGPDual_133(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_14(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_140(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(0, coefs);
            
            
            coefs = EGPDual_142(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_15(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_151(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_20(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_203(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_21(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_212(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            coefs = EGPDual_214(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_22(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_221(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            coefs = EGPDual_223(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(3, coefs);
            
            
            coefs = EGPDual_225(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(5, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_23(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_230(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(0, coefs);
            
            
            coefs = EGPDual_232(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            coefs = EGPDual_234(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_24(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_241(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            coefs = EGPDual_243(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_25(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_250(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(0, coefs);
            
            
            coefs = EGPDual_252(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_30(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_302(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_31(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_311(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            coefs = EGPDual_313(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_32(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_320(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(0, coefs);
            
            
            coefs = EGPDual_322(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            coefs = EGPDual_324(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_33(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_331(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            coefs = EGPDual_333(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(3, coefs);
            
            
            coefs = EGPDual_335(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(5, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_34(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_340(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(0, coefs);
            
            
            coefs = EGPDual_342(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            coefs = EGPDual_344(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_35(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_351(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            coefs = EGPDual_353(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_40(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_401(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_41(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_410(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(0, coefs);
            
            
            coefs = EGPDual_412(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_42(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_421(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            coefs = EGPDual_423(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_43(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_430(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(0, coefs);
            
            
            coefs = EGPDual_432(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            coefs = EGPDual_434(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_44(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_441(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            coefs = EGPDual_443(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(3, coefs);
            
            
            coefs = EGPDual_445(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(5, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_45(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_450(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(0, coefs);
            
            
            coefs = EGPDual_452(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            coefs = EGPDual_454(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_50(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_500(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(0, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_51(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_511(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_52(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_520(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(0, coefs);
            
            
            coefs = EGPDual_522(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_53(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_531(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            coefs = EGPDual_533(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(3, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_54(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_540(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(0, coefs);
            
            
            coefs = EGPDual_542(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(2, coefs);
            
            
            coefs = EGPDual_544(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(4, coefs);
            
            
            return ZeroBlade;
        }
        
        private static cga5dBlade DPDual_55(double[] coefs1, double[] coefs2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] coefs;
        
            coefs = EGPDual_551(coefs1, coefs2);
            if (IsZero5(coefs) == false)
                return new cga5dBlade(1, coefs);
            
            
            coefs = EGPDual_553(coefs1, coefs2);
            if (IsZero10(coefs) == false)
                return new cga5dBlade(3, coefs);
            
            
            coefs = EGPDual_555(coefs1, coefs2);
            if (IsZero1(coefs) == false)
                return new cga5dBlade(5, coefs);
            
            
            return ZeroBlade;
        }
        
        public cga5dBlade DPDual(cga5dBlade blade2)
        {
            if (IsZero || blade2.IsZero)
                return ZeroBlade;
        
            var id = Grade + blade2.Grade * (MaxGrade + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return DPDual_00(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 1
                case 6:
                    return DPDual_01(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 2
                case 12:
                    return DPDual_02(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 3
                case 18:
                    return DPDual_03(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 4
                case 24:
                    return DPDual_04(Coefs, blade2.Coefs);
                //grade1: 0, grade2: 5
                case 30:
                    return DPDual_05(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 0
                case 1:
                    return DPDual_10(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 1
                case 7:
                    return DPDual_11(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 2
                case 13:
                    return DPDual_12(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 3
                case 19:
                    return DPDual_13(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 4
                case 25:
                    return DPDual_14(Coefs, blade2.Coefs);
                //grade1: 1, grade2: 5
                case 31:
                    return DPDual_15(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 0
                case 2:
                    return DPDual_20(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 1
                case 8:
                    return DPDual_21(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 2
                case 14:
                    return DPDual_22(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 3
                case 20:
                    return DPDual_23(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 4
                case 26:
                    return DPDual_24(Coefs, blade2.Coefs);
                //grade1: 2, grade2: 5
                case 32:
                    return DPDual_25(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 0
                case 3:
                    return DPDual_30(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 1
                case 9:
                    return DPDual_31(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 2
                case 15:
                    return DPDual_32(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 3
                case 21:
                    return DPDual_33(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 4
                case 27:
                    return DPDual_34(Coefs, blade2.Coefs);
                //grade1: 3, grade2: 5
                case 33:
                    return DPDual_35(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 0
                case 4:
                    return DPDual_40(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 1
                case 10:
                    return DPDual_41(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 2
                case 16:
                    return DPDual_42(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 3
                case 22:
                    return DPDual_43(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 4
                case 28:
                    return DPDual_44(Coefs, blade2.Coefs);
                //grade1: 4, grade2: 5
                case 34:
                    return DPDual_45(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 0
                case 5:
                    return DPDual_50(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 1
                case 11:
                    return DPDual_51(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 2
                case 17:
                    return DPDual_52(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 3
                case 23:
                    return DPDual_53(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 4
                case 29:
                    return DPDual_54(Coefs, blade2.Coefs);
                //grade1: 5, grade2: 5
                case 35:
                    return DPDual_55(Coefs, blade2.Coefs);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
}}
}
