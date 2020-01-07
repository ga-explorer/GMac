using System.Diagnostics;
using System.Text;

namespace GeometryComposerLib.BasicMath.Tuples.Immutable
{
    /// <summary>
    /// An immutable 2-tuple of double precision numbers
    /// </summary>
    public struct Tuple2D : ITuple2D
    {
        public static Tuple2D Zero { get; } = new Tuple2D(0, 0);

        public static Tuple2D E1 { get; } = new Tuple2D(1, 0);

        public static Tuple2D E2 { get; } = new Tuple2D(0, 1);


        public static Tuple2D operator -(Tuple2D v1)
        {
            Debug.Assert(!v1.HasNaNComponent);

            return new Tuple2D(-v1.X, -v1.Y);
        }

        public static Tuple2D operator +(Tuple2D v1, Tuple2D v2)
        {
            Debug.Assert(!v1.HasNaNComponent && !v2.HasNaNComponent);

            return new Tuple2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Tuple2D operator -(Tuple2D v1, Tuple2D v2)
        {
            Debug.Assert(!v1.HasNaNComponent && !v2.HasNaNComponent);

            return new Tuple2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Tuple2D operator *(Tuple2D v1, double s)
        {
            Debug.Assert(!v1.HasNaNComponent && !double.IsNaN(s));

            return new Tuple2D(v1.X * s, v1.Y * s);
        }

        public static Tuple2D operator *(double s, Tuple2D v1)
        {
            Debug.Assert(!v1.HasNaNComponent && !double.IsNaN(s));

            return new Tuple2D(v1.X * s, v1.Y * s);
        }

        public static Tuple2D operator /(Tuple2D v1, double s)
        {
            Debug.Assert(!v1.HasNaNComponent && !double.IsNaN(s));

            s = 1.0d / s;

            return new Tuple2D(v1.X * s, v1.Y * s);
        }

        public static bool operator ==(Tuple2D v1, Tuple2D v2)
        {
            Debug.Assert(!v1.HasNaNComponent && !v2.HasNaNComponent);

            return
                v1.X.IsAlmostEqual(v2.X) &&
                v1.Y.IsAlmostEqual(v2.Y);
        }

        public static bool operator !=(Tuple2D v1, Tuple2D v2)
        {
            Debug.Assert(!v1.HasNaNComponent && !v2.HasNaNComponent);

            return
                !v1.X.IsAlmostEqual(v2.X) ||
                !v1.Y.IsAlmostEqual(v2.Y);
        }


        public double X { get; }

        public double Y { get; }

        public double Item1
            => X;

        public double Item2
            => Y;

        /// <summary>
        /// Get the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                Debug.Assert(index == 0 || index == 1);

                if (index == 0) return X;
                if (index == 1) return Y;

                return 0.0d;
            }

        }

        /// <summary>
        /// True if this tuple contains any NaN components
        /// </summary>
        public bool HasNaNComponent =>
            double.IsNaN(X) || double.IsNaN(Y);


        public Tuple2D(double x, double y)
        {
            X = x;
            Y = y;

            Debug.Assert(!HasNaNComponent);
        }

        public Tuple2D(ITuple2D tuple)
        {
            Debug.Assert(!tuple.HasNaNComponent);

            X = tuple.X;
            Y = tuple.Y;
        }


        public Tuple2D ToTuple2D()
            => this;

        public bool Equals(Tuple2D tuple)
        {
            return X.Equals(tuple.X) && Y.Equals(tuple.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is Tuple2D && Equals((Tuple2D)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .Append(X.ToString("G"))
                .Append(", ")
                .Append(Y.ToString("G"))
                .Append(")")
                .ToString();
        }
    }
}
