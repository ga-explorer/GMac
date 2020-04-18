using System;

namespace Ega3D
{
    /// <summary>
    /// This class represents a mutable vector in the Ega3D frame
    /// </summary>
    public sealed class Ega3DVector
    {
        public static Ega3DVector[] BasisVectors()
        {
            return new[]
            {
                new Ega3DVector(1.0D, 0.0D, 0.0D),
                new Ega3DVector(0.0D, 1.0D, 0.0D),
                new Ega3DVector(0.0D, 0.0D, 1.0D)
            };
        }


        public double C1 { get; set; }
        public double C2 { get; set; }
        public double C3 { get; set; }

        public double NormSquared
        {
            get { return C1 * C1 + C2 * C2 + C3 * C3; }
        }

        public double EuclideanNormSquared
        {
            get { return C1 * C1 + C2 * C2 + C3 * C3; }
        }

        public double EuclideanNorm
        {
            get { return Math.Sqrt(C1 * C1 + C2 * C2 + C3 * C3); }
        }


        public Ega3DVector()
        {
        }

        public Ega3DVector(double c1, double c2, double c3)
        {
            C1 = c1;
            C2 = c2;
            C3 = c3;
        }

        public Ega3DVector(double[] c)
        {
            C1 = c[0];
            C2 = c[1];
            C3 = c[2];
        }

        /// <summary>
        /// Convert this to a unit-vector in the Euclidean space
        /// </summary>
        /// <returns></returns>
        public double Normalize()
        {
            var scalar = EuclideanNorm;
            var invScalar = 1.0D / scalar;

            C1 *= invScalar;
            C2 *= invScalar;
            C3 *= invScalar;

            return scalar;
        }

        public double[] ToArray()
        {
            return new[] { C1, C2, C3 };
        }

        public Ega3DkVector ToBlade()
        {
            return new Ega3DkVector(1, new[] { C1, C2, C3 });
        }
    }
}
