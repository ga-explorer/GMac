using System.IO;

namespace GradedMultivectorsLibraryComposer.Outputs.CSharp.Ega3D
{
    /// <summary>
    /// This class represents a k-vector in the Ega3D frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of 
    /// the k-vector as a linear combination of basis blades of the same grade.
    /// </summary>
    public sealed partial class Ega3DkVector
        : IEga3DMultivector
    {
        private static Ega3DkVector DP_00(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_000(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(0, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_01(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_011(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(1, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_02(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_022(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(2, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_03(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_033(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(3, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_10(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_101(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(1, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_11(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_112(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(2, scalars);
            
            
            scalars = EGP_110(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(0, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_12(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_123(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(3, scalars);
            
            
            scalars = EGP_121(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(1, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_13(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_132(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(2, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_20(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_202(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(2, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_21(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_213(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(3, scalars);
            
            
            scalars = EGP_211(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(1, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_22(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_222(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(2, scalars);
            
            
            scalars = EGP_220(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(0, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_23(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_233(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(3, scalars);
            
            
            scalars = EGP_231(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(1, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_30(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_303(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(3, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_31(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_312(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(2, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_32(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_323(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(3, scalars);
            
            
            scalars = EGP_321(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(1, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DP_33(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGP_332(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(2, scalars);
            
            
            scalars = EGP_330(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(0, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        public Ega3DkVector DP(Ega3DkVector blade2)
        {
            if (IsZero || blade2.IsZero)
                return Ega3DMultivector.Zero;
        
            var id = Grade + blade2.Grade * (Ega3DUtils.VectorSpaceDimensions + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return DP_00(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 1
                case 4:
                    return DP_01(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 2
                case 8:
                    return DP_02(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 3
                case 12:
                    return DP_03(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 0
                case 1:
                    return DP_10(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 1
                case 5:
                    return DP_11(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 2
                case 9:
                    return DP_12(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 3
                case 13:
                    return DP_13(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 0
                case 2:
                    return DP_20(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 1
                case 6:
                    return DP_21(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 2
                case 10:
                    return DP_22(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 3
                case 14:
                    return DP_23(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 0
                case 3:
                    return DP_30(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 1
                case 7:
                    return DP_31(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 2
                case 11:
                    return DP_32(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 3
                case 15:
                    return DP_33(Scalars, blade2.Scalars);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
    }
}
