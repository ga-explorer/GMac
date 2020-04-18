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
        private static Ega3DkVector[] GP_00(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(0, GP_000(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_01(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, GP_011(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_02(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, GP_022(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_03(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(3, GP_033(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_10(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, GP_101(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_11(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(0, GP_110(scalars1, scalars2)),
                new Ega3DkVector(2, GP_112(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_12(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, GP_121(scalars1, scalars2)),
                new Ega3DkVector(3, GP_123(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_13(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, GP_132(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_20(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, GP_202(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_21(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, GP_211(scalars1, scalars2)),
                new Ega3DkVector(3, GP_213(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_22(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(0, GP_220(scalars1, scalars2)),
                new Ega3DkVector(2, GP_222(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_23(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, GP_231(scalars1, scalars2)),
                new Ega3DkVector(3, GP_233(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_30(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(3, GP_303(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_31(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, GP_312(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_32(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, GP_321(scalars1, scalars2)),
                new Ega3DkVector(3, GP_323(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] GP_33(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(0, GP_330(scalars1, scalars2)),
                new Ega3DkVector(2, GP_332(scalars1, scalars2))
            };
        }
        
        public Ega3DkVector[] GP(Ega3DkVector blade2)
        {
            if (IsZero || blade2.IsZero)
                return new Ega3DkVector[0];
        
            var id = Grade + blade2.Grade * (Ega3DUtils.VectorSpaceDimensions + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return GP_00(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 1
                case 4:
                    return GP_01(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 2
                case 8:
                    return GP_02(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 3
                case 12:
                    return GP_03(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 0
                case 1:
                    return GP_10(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 1
                case 5:
                    return GP_11(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 2
                case 9:
                    return GP_12(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 3
                case 13:
                    return GP_13(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 0
                case 2:
                    return GP_20(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 1
                case 6:
                    return GP_21(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 2
                case 10:
                    return GP_22(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 3
                case 14:
                    return GP_23(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 0
                case 3:
                    return GP_30(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 1
                case 7:
                    return GP_31(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 2
                case 11:
                    return GP_32(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 3
                case 15:
                    return GP_33(Scalars, blade2.Scalars);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
