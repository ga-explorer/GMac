using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using MathNet.Numerics;
using TextComposerLib;

namespace EuclideanGeometryLib.BasicMath.Tuples.Mutable
{
    public sealed class MutableComplexTuple2D : IComplexTuple2D, IEnumerable<Complex>
    {
        public double ItemXReal { get; set; }

        public double ItemXImaginary { get; set; }

        public double ItemYReal { get; set; }

        public double ItemYImaginary { get; set; }


        public Complex X
            => new Complex(ItemXReal, ItemXImaginary);

        public Complex Y
            => new Complex(ItemYReal, ItemYImaginary);

        public Complex Item1
            => X;

        public Complex Item2
            => Y;


        public bool HasNaNComponent
            => double.IsNaN(ItemXReal) ||
               double.IsNaN(ItemXImaginary) ||
               double.IsNaN(ItemYReal) ||
               double.IsNaN(ItemYImaginary);


        /// <summary>
        /// Get or set the ith component of this tuple
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
            set
            {
                Debug.Assert(!value.IsNaN());
                Debug.Assert(index == 0 || index == 1);

                if (index == 0)
                {
                    ItemXReal = value.Real;
                    ItemXImaginary = value.Imaginary;
                }
                else if (index == 1)
                {
                    ItemYReal = value.Real;
                    ItemYImaginary = value.Imaginary;
                }
            }
        }


        public MutableComplexTuple2D()
        {
        }

        public MutableComplexTuple2D(double x, double y)
        {
            ItemXReal = x;
            ItemYReal = y;

            Debug.Assert(!HasNaNComponent);
        }

        public MutableComplexTuple2D(Complex x, Complex y)
        {
            ItemXReal = x.Real;
            ItemYReal = y.Real;

            ItemXImaginary = x.Imaginary;
            ItemYImaginary = y.Imaginary;

            Debug.Assert(!HasNaNComponent);
        }

        public MutableComplexTuple2D(ITuple2D tuple)
        {
            Debug.Assert(!tuple.HasNaNComponent);

            ItemXReal = tuple.X;
            ItemYReal = tuple.Y;
        }

        public MutableComplexTuple2D(IComplexTuple2D tuple)
        {
            Debug.Assert(!tuple.HasNaNComponent);

            ItemXReal = tuple.X.Real;
            ItemYReal = tuple.Y.Real;

            ItemXImaginary = tuple.X.Imaginary;
            ItemYImaginary = tuple.Y.Imaginary;
        }


        public MutableComplexTuple2D SetTuple(double x, double y)
        {
            ItemXReal = x;
            ItemYReal = y;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableComplexTuple2D SetTuple(Complex x, Complex y)
        {
            ItemXReal = x.Real;
            ItemYReal = y.Real;

            ItemXImaginary = x.Imaginary;
            ItemYImaginary = y.Imaginary;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableComplexTuple2D SetTuple(ITuple2D tuple)
        {
            ItemXReal = tuple.X;
            ItemYReal = tuple.Y;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableComplexTuple2D SetTuple(IComplexTuple2D tuple)
        {
            ItemXReal = tuple.X.Real;
            ItemYReal = tuple.Y.Real;

            ItemXImaginary = tuple.X.Imaginary;
            ItemYImaginary = tuple.Y.Imaginary;

            Debug.Assert(!HasNaNComponent);

            return this;
        }


        public IEnumerator<Complex> GetEnumerator()
        {
            yield return new Complex(ItemXReal, ItemXImaginary);
            yield return new Complex(ItemYReal, ItemYImaginary);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return new Complex(ItemXReal, ItemXImaginary);
            yield return new Complex(ItemYReal, ItemYImaginary);
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .AppendComplexNumber(ItemXReal, ItemXImaginary, "G")
                .Append(", ")
                .AppendComplexNumber(ItemYReal, ItemYImaginary, "G")
                .Append(")")
                .ToString();
        }
    }
}