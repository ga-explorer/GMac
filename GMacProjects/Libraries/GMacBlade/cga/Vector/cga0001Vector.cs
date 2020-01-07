using System;

namespace GMacBlade.cga0001
{
    /// <summary>
    /// This class represents a mutable vector in the cga0001 frame
    /// </summary>
    public sealed class cga0001Vector
    {
        public static cga0001Vector[] BasisVectors()
        {
            return new[]
            {
                new cga0001Vector(1.0D, 0.0D, 0.0D, 0.0D, 0.0D),
                new cga0001Vector(0.0D, 1.0D, 0.0D, 0.0D, 0.0D),
                new cga0001Vector(0.0D, 0.0D, 1.0D, 0.0D, 0.0D),
                new cga0001Vector(0.0D, 0.0D, 0.0D, 1.0D, 0.0D),
                new cga0001Vector(0.0D, 0.0D, 0.0D, 0.0D, 1.0D)
            };
        }


        public double C1 { get; set; }
        public double C2 { get; set; }
        public double C3 { get; set; }
        public double C4 { get; set; }
        public double C5 { get; set; }

        public double NormSquared
        {
            get { return C1 * C1 + C2 * C2 + C3 * C3 + C4 * C4 + C5 * C5; }
        }

        public double EuclideanNormSquared
        {
            get { return C1 * C1 + C2 * C2 + C3 * C3 + C4 * C4 + C5 * C5; }
        }

        public double EuclideanNorm
        {
            get { return Math.Sqrt(C1 * C1 + C2 * C2 + C3 * C3 + C4 * C4 + C5 * C5); }
        }


        public cga0001Vector()
        {
        }

        public cga0001Vector(double c1, double c2, double c3, double c4, double c5)
        {
            C1 = c1;
            C2 = c2;
            C3 = c3;
            C4 = c4;
            C5 = c5;
        }

        public cga0001Vector(double[] c)
        {
            C1 = c[0];
            C2 = c[1];
            C3 = c[2];
            C4 = c[3];
            C5 = c[4];
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
            C4 *= invScalar;
            C5 *= invScalar;

            return scalar;
        }

        public double[] ToArray()
        {
            return new[] { C1, C2, C3, C4, C5 };
        }

        public cga0001Blade ToBlade()
        {
            return new cga0001Blade(1, new[] { C1, C2, C3, C4, C5 });
        }
    }
}
