using System;

namespace GMacBlade.cga5d
{
    /// <summary>
    /// This class represents a mutable vector in the cga5d frame
    /// </summary>
    public sealed class cga5dVector
    {
        public static cga5dVector[] BasisVectors()
        {
            return new[]
            {
                new cga5dVector(1.0D, 0.0D, 0.0D, 0.0D, 0.0D),
                new cga5dVector(0.0D, 1.0D, 0.0D, 0.0D, 0.0D),
                new cga5dVector(0.0D, 0.0D, 1.0D, 0.0D, 0.0D),
                new cga5dVector(0.0D, 0.0D, 0.0D, 1.0D, 0.0D),
                new cga5dVector(0.0D, 0.0D, 0.0D, 0.0D, 1.0D)
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


        public cga5dVector()
        {
        }

        public cga5dVector(double c1, double c2, double c3, double c4, double c5)
        {
            C1 = c1;
            C2 = c2;
            C3 = c3;
            C4 = c4;
            C5 = c5;
        }

        public cga5dVector(double[] c)
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

        public cga5dBlade ToBlade()
        {
            return new cga5dBlade(1, new[] { C1, C2, C3, C4, C5 });
        }
    }
}
