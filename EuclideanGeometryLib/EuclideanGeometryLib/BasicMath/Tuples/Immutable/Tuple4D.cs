using System.Diagnostics;
using System.Text;

namespace EuclideanGeometryLib.BasicMath.Tuples.Immutable
{
    public struct Tuple4D : ITuple4D
    {
        public Tuple4D CreateAffineVector(double x, double y, double z)
        {
            return new Tuple4D(x, y, z, 0);
        }

        public Tuple4D CreateAffinePoint(double x, double y, double z)
        {
            return new Tuple4D(x, y, z, 1);
        }


        public static Tuple4D operator -(Tuple4D v1)
        {
            Debug.Assert(!v1.HasNaNComponent);

            return new Tuple4D(-v1.X, -v1.Y, -v1.Z, -v1.W);
        }

        public static Tuple4D operator +(Tuple4D v1, Tuple4D v2)
        {
            Debug.Assert(!v1.HasNaNComponent && !v2.HasNaNComponent);

            return new Tuple4D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        public static Tuple4D operator -(Tuple4D v1, Tuple4D v2)
        {
            Debug.Assert(!v1.HasNaNComponent && !v2.HasNaNComponent);

            return new Tuple4D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }

        public static Tuple4D operator *(Tuple4D v1, double s)
        {
            Debug.Assert(!v1.HasNaNComponent && !double.IsNaN(s));

            return new Tuple4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
        }

        public static Tuple4D operator *(double s, Tuple4D v1)
        {
            Debug.Assert(!v1.HasNaNComponent && !double.IsNaN(s));

            return new Tuple4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
        }

        public static Tuple4D operator /(Tuple4D v1, double s)
        {
            Debug.Assert(!v1.HasNaNComponent && !double.IsNaN(s));

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;
            return new Tuple4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
        }

        public static bool operator ==(Tuple4D v1, Tuple4D v2)
        {
            Debug.Assert(!v1.HasNaNComponent && !v2.HasNaNComponent);

            return
                v1.X.IsAlmostEqual(v2.X) &&
                v1.Y.IsAlmostEqual(v2.Y) &&
                v1.Z.IsAlmostEqual(v2.Z) &&
                v1.W.IsAlmostEqual(v2.W);
        }

        public static bool operator !=(Tuple4D v1, Tuple4D v2)
        {
            Debug.Assert(!v1.HasNaNComponent && !v2.HasNaNComponent);

            return
                !v1.X.IsAlmostEqual(v2.X) ||
                !v1.Y.IsAlmostEqual(v2.Y) ||
                !v1.Z.IsAlmostEqual(v2.Z) ||
                !v1.W.IsAlmostEqual(v2.W);
        }

        
        /// <summary>
        /// The 1st component of this tuple. If this tuple holds a quaternion, this is the 1st component
        /// of its imaginary (i.e. vector) part
        /// </summary>
        public double X { get; }

        /// <summary>
        /// The 2nd component of this tuple. If this tuple holds a quaternion, this is the 2nd component
        /// of its imaginary (i.e. vector) part
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// The 3rd component of this tuple. If this tuple holds a quaternion, this is the 3rd component
        /// of its imaginary (i.e. vector) part
        /// </summary>
        public double Z { get; }

        /// <summary>
        /// The 4th component of this tuple. If this tuple holds a quaternion, this is its scalar part
        /// </summary>
        public double W { get; }

        public double Item1
            => X;

        public double Item2
            => Y;

        public double Item3
            => Z;

        public double Item4
            => W;

        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                Debug.Assert(index >= 0 && index <= 3);

                if (index == 0) return X;
                if (index == 1) return Y;
                if (index == 2) return Z;
                if (index == 3) return W;

                return 0.0d;
            }
        }

        /// <summary>
        /// True if this tuple contains any NaN components
        /// </summary>
        public bool HasNaNComponent =>
            double.IsNaN(X) || double.IsNaN(Y) || double.IsNaN(Z) || double.IsNaN(W);


        public Tuple4D(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Tuple4D(ITuple4D v)
        {
            Debug.Assert(!v.HasNaNComponent);

            X = v.X;
            Y = v.Y;
            Z = v.Z;
            W = v.W;
        }


        public Tuple4D ToTuple4D()
            => this;

        public bool Equals(Tuple4D tuple)
        {
            return X.Equals(tuple.X) && Y.Equals(tuple.Y) && Z.Equals(tuple.Z) && W.Equals(tuple.W);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Tuple4D && Equals((Tuple4D)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                hashCode = (hashCode * 397) ^ W.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .Append(X.ToString("G"))
                .Append(", ")
                .Append(Y.ToString("G"))
                .Append(", ")
                .Append(Z.ToString("G"))
                .Append(", ")
                .Append(W.ToString("G"))
                .Append(")")
                .ToString();
        }
    }
}
