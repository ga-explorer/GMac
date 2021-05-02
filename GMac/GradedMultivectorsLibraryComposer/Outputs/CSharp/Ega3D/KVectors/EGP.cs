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
        private static Ega3DkVector[] EGP_00(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(0, EGP_000(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_01(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, EGP_011(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_02(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, EGP_022(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_03(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(3, EGP_033(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_10(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, EGP_101(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_11(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(0, EGP_110(scalars1, scalars2)),
                new Ega3DkVector(2, EGP_112(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_12(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, EGP_121(scalars1, scalars2)),
                new Ega3DkVector(3, EGP_123(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_13(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, EGP_132(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_20(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, EGP_202(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_21(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, EGP_211(scalars1, scalars2)),
                new Ega3DkVector(3, EGP_213(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_22(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(0, EGP_220(scalars1, scalars2)),
                new Ega3DkVector(2, EGP_222(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_23(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, EGP_231(scalars1, scalars2)),
                new Ega3DkVector(3, EGP_233(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_30(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(3, EGP_303(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_31(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(2, EGP_312(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_32(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(1, EGP_321(scalars1, scalars2)),
                new Ega3DkVector(3, EGP_323(scalars1, scalars2))
            };
        }
        
        private static Ega3DkVector[] EGP_33(double[] scalars1, double[] scalars2)
        {
            return new[]
            {
                new Ega3DkVector(0, EGP_330(scalars1, scalars2)),
                new Ega3DkVector(2, EGP_332(scalars1, scalars2))
            };
        }
        
        public Ega3DkVector[] EGP(Ega3DkVector blade2)
        {
            if (IsZero || blade2.IsZero)
                return new Ega3DkVector[0];
        
            var id = Grade + blade2.Grade * (Ega3DUtils.VectorSpaceDimensions + 1);
        
            switch (id)
            {
                //grade1: 0, grade2: 0
                case 0:
                    return EGP_00(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 1
                case 4:
                    return EGP_01(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 2
                case 8:
                    return EGP_02(Scalars, blade2.Scalars);
                //grade1: 0, grade2: 3
                case 12:
                    return EGP_03(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 0
                case 1:
                    return EGP_10(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 1
                case 5:
                    return EGP_11(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 2
                case 9:
                    return EGP_12(Scalars, blade2.Scalars);
                //grade1: 1, grade2: 3
                case 13:
                    return EGP_13(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 0
                case 2:
                    return EGP_20(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 1
                case 6:
                    return EGP_21(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 2
                case 10:
                    return EGP_22(Scalars, blade2.Scalars);
                //grade1: 2, grade2: 3
                case 14:
                    return EGP_23(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 0
                case 3:
                    return EGP_30(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 1
                case 7:
                    return EGP_31(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 2
                case 11:
                    return EGP_32(Scalars, blade2.Scalars);
                //grade1: 3, grade2: 3
                case 15:
                    return EGP_33(Scalars, blade2.Scalars);
            }
        
            throw new InvalidDataException("Internal error. Blade grade not acceptable!");
        }
        
    }
}
