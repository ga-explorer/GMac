using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using MathNet.Numerics;
using TextComposerLib;

namespace GeometryComposerLib.BasicMath.Tuples.Mutable
{
    public sealed class MutableComplexTuple3D : IComplexTuple3D, IEnumerable<Complex>
    {
        public double RealX { get; set; }

        public double RealY { get; set; }

        public double RealZ { get; set; }


        public double ImaginaryX { get; set; }

        public double ImaginaryY { get; set; }

        public double ImaginaryZ { get; set; }


        public Complex X 
            => new Complex(RealX, ImaginaryX);

        public Complex Y
            => new Complex(RealY, ImaginaryY);

        public Complex Z 
            => new Complex(RealZ, ImaginaryZ);

        public Complex Item1
            => X;

        public Complex Item2
            => Y;

        public Complex Item3
            => Z;


        public bool HasNaNComponent
            => double.IsNaN(RealX) ||
               double.IsNaN(ImaginaryX) ||
               double.IsNaN(RealY) ||
               double.IsNaN(ImaginaryY) ||
               double.IsNaN(RealZ) ||
               double.IsNaN(ImaginaryZ);


        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Complex this[int index]
        {
            get
            {
                Debug.Assert(index == 0 || index == 1 || index == 2);

                if (index == 0) return X;
                if (index == 1) return Y;
                if (index == 2) return Z;

                return 0.0d;
            }
            set
            {
                Debug.Assert(!value.IsNaN());
                Debug.Assert(index == 0 || index == 1 || index == 2);

                if (index == 0)
                {
                    RealX = value.Real;
                    ImaginaryX = value.Imaginary;
                }
                else if (index == 1)
                {
                    RealY = value.Real;
                    ImaginaryY = value.Imaginary;
                }
                else if (index == 2)
                {
                    RealZ = value.Real;
                    ImaginaryZ = value.Imaginary;
                }
            }
        }


        public MutableComplexTuple3D()
        {
        }

        public MutableComplexTuple3D(double x, double y, double z)
        {
            RealX = x;
            RealY = y;
            RealZ = z;

            Debug.Assert(!HasNaNComponent);
        }

        public MutableComplexTuple3D(Complex x, Complex y, Complex z)
        {
            RealX = x.Real;
            RealY = y.Real;
            RealZ = z.Real;

            ImaginaryX = x.Imaginary;
            ImaginaryY = y.Imaginary;
            ImaginaryZ = z.Imaginary;

            Debug.Assert(!HasNaNComponent);
        }

        public MutableComplexTuple3D(ITuple3D tuple)
        {
            Debug.Assert(!tuple.HasNaNComponent);

            RealX = tuple.X;
            RealY = tuple.Y;
            RealZ = tuple.Z;
        }

        public MutableComplexTuple3D(IComplexTuple3D tuple)
        {
            Debug.Assert(!tuple.HasNaNComponent);

            RealX = tuple.X.Real;
            RealY = tuple.Y.Real;
            RealZ = tuple.Z.Real;

            ImaginaryX = tuple.X.Imaginary;
            ImaginaryY = tuple.Y.Imaginary;
            ImaginaryZ = tuple.Z.Imaginary;
        }


        public MutableComplexTuple3D SetTuple(double x, double y, double z)
        {
            RealX = x;
            RealY = y;
            RealZ = z;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableComplexTuple3D SetRealTuple(double x, double y, double z)
        {
            RealX = x;
            RealY = y;
            RealZ = z;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableComplexTuple3D SetImaginaryTuple(double x, double y, double z)
        {
            ImaginaryX = x;
            ImaginaryY = y;
            ImaginaryZ = z;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableComplexTuple3D SetTuple(Complex x, Complex y, Complex z)
        {
            RealX = x.Real;
            RealY = y.Real;
            RealZ = z.Real;

            ImaginaryX = x.Imaginary;
            ImaginaryY = y.Imaginary;
            ImaginaryZ = z.Imaginary;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableComplexTuple3D SetTuple(ITuple3D tuple)
        {
            RealX = tuple.X;
            RealY = tuple.Y;
            RealZ = tuple.Z;

            ImaginaryX = 0;
            ImaginaryY = 0;
            ImaginaryZ = 0;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableComplexTuple3D SetRealTuple(ITuple3D tuple)
        {
            RealX = tuple.X;
            RealY = tuple.Y;
            RealZ = tuple.Z;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableComplexTuple3D SetImaginaryTuple(ITuple3D tuple)
        {
            ImaginaryX = tuple.X;
            ImaginaryY = tuple.Y;
            ImaginaryZ = tuple.Z;

            Debug.Assert(!HasNaNComponent);

            return this;
        }

        public MutableComplexTuple3D SetTuple(IComplexTuple3D tuple)
        {
            RealX = tuple.X.Real;
            RealY = tuple.Y.Real;
            RealZ = tuple.Z.Real;

            ImaginaryX = tuple.X.Imaginary;
            ImaginaryY = tuple.Y.Imaginary;
            ImaginaryZ = tuple.Z.Imaginary;

            Debug.Assert(!HasNaNComponent);

            return this;
        }


        public IEnumerator<Complex> GetEnumerator()
        {
            yield return new Complex(RealX, ImaginaryX);
            yield return new Complex(RealY, ImaginaryY);
            yield return new Complex(RealZ, ImaginaryZ);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return new Complex(RealX, ImaginaryX);
            yield return new Complex(RealY, ImaginaryY);
            yield return new Complex(RealZ, ImaginaryZ);
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .AppendComplexNumber(RealX, ImaginaryX, "G")
                .Append(", ")
                .AppendComplexNumber(RealY, ImaginaryY, "G")
                .Append(", ")
                .AppendComplexNumber(RealZ, ImaginaryZ, "G")
                .Append(")")
                .ToString();
        }
    }
}
