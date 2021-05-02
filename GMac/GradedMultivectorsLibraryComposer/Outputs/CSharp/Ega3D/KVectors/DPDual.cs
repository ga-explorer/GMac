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
        private static Ega3DkVector DPDual_00(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_003(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(3, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_01(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_012(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(2, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_02(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_021(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(1, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_03(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_030(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(0, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_10(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_102(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(2, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_11(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_111(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(1, scalars);
            
            
            scalars = EGPDual_113(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(3, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_12(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_120(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(0, scalars);
            
            
            scalars = EGPDual_122(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(2, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_13(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_131(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(1, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_20(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_201(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(1, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_21(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_210(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(0, scalars);
            
            
            scalars = EGPDual_212(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(2, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_22(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_221(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(1, scalars);
            
            
            scalars = EGPDual_223(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(3, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_23(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_230(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(0, scalars);
            
            
            scalars = EGPDual_232(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(2, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_30(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_300(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(0, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_31(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_311(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(1, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_32(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_320(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(0, scalars);
            
            
            scalars = EGPDual_322(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(2, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        private static Ega3DkVector DPDual_33(double[] scalars1, double[] scalars2)
        {
            //Try all Euclidean geometric products for these two input grades starting from largest to smallest
            //output grade
        
            double[] scalars;
        
            scalars = EGPDual_331(scalars1, scalars2);
            if (IsNearZero3(scalars) == false)
                return new Ega3DkVector(1, scalars);
            
            
            scalars = EGPDual_333(scalars1, scalars2);
            if (IsNearZero1(scalars) == false)
                return new Ega3DkVector(3, scalars);
            
            
            return Ega3DMultivector.Zero;
        }
        
        
        public Ega3DkVector DPDual(Ega3DkVector blade2)
        {
            if (IsZero || blade2.IsZero)
                return Ega3DMultivector.Zero;
        
            var id = Grade + blade2.Grade * (Ega3DUtils.VectorSpaceDimensions + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return DPDual_00(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 1
                case 4:
                    return DPDual_01(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 2
                case 8:
                    return DPDual_02(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 3
                case 12:
                    return DPDual_03(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 0
                case 1:
                    return DPDual_10(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 1
                case 5:
                    return DPDual_11(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 2
                case 9:
                    return DPDual_12(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 3
                case 13:
                    return DPDual_13(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 0
                case 2:
                    return DPDual_20(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 1
                case 6:
                    return DPDual_21(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 2
                case 10:
                    return DPDual_22(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 3
                case 14:
                    return DPDual_23(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 0
                case 3:
                    return DPDual_30(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 1
                case 7:
                    return DPDual_31(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 2
                case 11:
                    return DPDual_32(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 3
                case 15:
                    return DPDual_33(Scalars, blade2.Scalars);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
    }
}
