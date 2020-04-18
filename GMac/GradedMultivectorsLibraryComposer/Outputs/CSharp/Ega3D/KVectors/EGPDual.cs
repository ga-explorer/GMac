using System.IO;

namespace Ega3D
{
    /// <summary>
    /// This class represents a k-vector in the Ega3D frame with arbitrary grade 
    /// (i.e. grade is determined at runtime) based on additive representation of 
    /// the k-vector as a linear combination of basis blades of the same grade.
    /// </summary>
    public sealed partial class Ega3DkVector
        : IEga3DMultivector
    {
        private static Ega3DkVector[] EGPDual_00(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(3, EGPDual_003(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_01(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, EGPDual_012(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_02(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, EGPDual_021(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_03(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(0, EGPDual_030(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_10(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, EGPDual_102(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_11(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(3, EGPDual_113(scalars1, scalars2)),
                new Ega3DkVector(1, EGPDual_111(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_12(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, EGPDual_122(scalars1, scalars2)),
                new Ega3DkVector(0, EGPDual_120(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_13(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, EGPDual_131(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_20(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, EGPDual_201(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_21(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, EGPDual_212(scalars1, scalars2)),
                new Ega3DkVector(0, EGPDual_210(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_22(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(3, EGPDual_223(scalars1, scalars2)),
                new Ega3DkVector(1, EGPDual_221(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_23(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, EGPDual_232(scalars1, scalars2)),
                new Ega3DkVector(0, EGPDual_230(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_30(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(0, EGPDual_300(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_31(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, EGPDual_311(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_32(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, EGPDual_322(scalars1, scalars2)),
                new Ega3DkVector(0, EGPDual_320(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGPDual_33(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(3, EGPDual_333(scalars1, scalars2)),
                new Ega3DkVector(1, EGPDual_331(scalars1, scalars2))
            };
        }
        
        public Ega3DkVector[] EGPDual(Ega3DkVector blade2)
        {
            if (IsZero || blade2.IsZero)
                return new Ega3DkVector[0];
        
            var id = Grade + blade2.Grade * (Ega3DUtils.VectorSpaceDimensions + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return EGPDual_00(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 1
                case 4:
                    return EGPDual_01(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 2
                case 8:
                    return EGPDual_02(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 3
                case 12:
                    return EGPDual_03(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 0
                case 1:
                    return EGPDual_10(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 1
                case 5:
                    return EGPDual_11(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 2
                case 9:
                    return EGPDual_12(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 3
                case 13:
                    return EGPDual_13(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 0
                case 2:
                    return EGPDual_20(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 1
                case 6:
                    return EGPDual_21(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 2
                case 10:
                    return EGPDual_22(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 3
                case 14:
                    return EGPDual_23(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 0
                case 3:
                    return EGPDual_30(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 1
                case 7:
                    return EGPDual_31(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 2
                case 11:
                    return EGPDual_32(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 3
                case 15:
                    return EGPDual_33(Scalars, blade2.Scalars);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
