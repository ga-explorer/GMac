﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using MathNet.Numerics;
using TextComposerLib;

namespace EuclideanGeometryLib.BasicMath.Tuples.Immutable
{
    /// <summary>
    /// An immutable 2-tuple of double precision numbers
    /// </summary>
    public readonly struct ComplexTuple2D : IComplexTuple2D, IEnumerable<Complex>
    {
        public static ComplexTuple2D Zero { get; } 
            = new ComplexTuple2D(0, 0);


        public static ComplexTuple2D operator -(ComplexTuple2D v1)
        {
            Debug.Assert(!v1.IsInvalid);

            return new ComplexTuple2D(-v1.X, -v1.Y);
        }

        public static ComplexTuple2D operator +(ComplexTuple2D v1, ComplexTuple2D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return new ComplexTuple2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static ComplexTuple2D operator -(ComplexTuple2D v1, ComplexTuple2D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return new ComplexTuple2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static ComplexTuple2D operator *(ComplexTuple2D v1, double s)
        {
            Debug.Assert(!v1.IsInvalid && !double.IsNaN(s));

            return new ComplexTuple2D(v1.X * s, v1.Y * s);
        }

        public static ComplexTuple2D operator *(double s, ComplexTuple2D v1)
        {
            Debug.Assert(!v1.IsInvalid && !double.IsNaN(s));

            return new ComplexTuple2D(v1.X * s, v1.Y * s);
        }

        public static ComplexTuple2D operator /(ComplexTuple2D v1, Complex s)
        {
            Debug.Assert(!v1.IsInvalid && !s.IsNaN());

            s = 1.0d / s;

            return new ComplexTuple2D(v1.X * s, v1.Y * s);
        }

        public static bool operator ==(ComplexTuple2D v1, ComplexTuple2D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return
                v1.X.IsAlmostEqual(v2.X) &&
                v1.Y.IsAlmostEqual(v2.Y);
        }

        public static bool operator !=(ComplexTuple2D v1, ComplexTuple2D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return
                !v1.X.IsAlmostEqual(v2.X) ||
                !v1.Y.IsAlmostEqual(v2.Y);
        }


        public Complex X { get; }

        public Complex Y { get; }

        public Complex Item1
            => X;

        public Complex Item2
            => Y;

        /// <summary>
        /// Get the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Complex this[int index]
        {
            get
            {
                Debug.Assert(index == 0 || index == 1);

                if (index == 0) return X;
                if (index == 1) return Y;

                return 0.0d;
            }

        }

        public bool IsValid 
            => !X.IsNaN() && !Y.IsNaN();

        /// <summary>
        /// True if this tuple contains any NaN components
        /// </summary>
        public bool IsInvalid =>
            X.IsNaN() || Y.IsNaN();


        public ComplexTuple2D(double x, double y)
        {
            X = x;
            Y = y;

            Debug.Assert(!IsInvalid);
        }

        public ComplexTuple2D(Complex x, Complex y)
        {
            X = x;
            Y = y;

            Debug.Assert(!IsInvalid);
        }

        public ComplexTuple2D(IComplexTuple2D tuple)
        {
            Debug.Assert(!tuple.IsInvalid);

            X = tuple.X;
            Y = tuple.Y;
        }


        public bool Equals(ComplexTuple2D tuple)
        {
            return X.Equals(tuple.X) && Y.Equals(tuple.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is ComplexTuple2D && Equals((ComplexTuple2D)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public IEnumerator<Complex> GetEnumerator()
        {
            yield return X;
            yield return Y;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return X;
            yield return Y;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .AppendComplexNumber(X.Real, X.Imaginary, "G")
                .Append(", ")
                .AppendComplexNumber(Y.Real, Y.Imaginary, "G")
                .Append(")")
                .ToString();
        }
    }
}