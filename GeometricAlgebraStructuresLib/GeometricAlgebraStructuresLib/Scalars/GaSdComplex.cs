using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace GeometricAlgebraStructuresLib.Scalars
{
    public sealed class GaSdComplex : IGaScalarDomain<Complex>
    {
        public double ZeroEpsilon { get; set; }
            = 1e-13d;

        
        public Complex GetZero()
        {
            return Complex.Zero;
        }

        public Complex GetOne()
        {
            return Complex.One;
        }

        public Complex Add(Complex scalar1, Complex scalar2)
        {
            return scalar1 + scalar2;
        }

        public Complex Add(params Complex[] scalarsList)
        {
            var real = scalarsList.Aggregate(
                0d, 
                (current, s) => current + s.Real
            );
            
            var imaginary = scalarsList.Aggregate(
                0d, 
                (current, s) => current + s.Imaginary
            );
            
            return new Complex(real, imaginary);
        }

        public Complex Add(IEnumerable<Complex> scalarsList)
        {
            return scalarsList.Aggregate(
                Complex.Zero, 
                (current, s) => current + s
            );
        }

        public Complex Subtract(Complex scalar1, Complex scalar2)
        {
            return scalar1 - scalar2;
        }

        public Complex Times(Complex scalar1, Complex scalar2)
        {
            return scalar1 * scalar2;
        }

        public Complex Times(params Complex[] scalarsList)
        {
            return scalarsList.Aggregate(
                Complex.One, 
                (current, s) => current * s
            );
        }

        public Complex Times(IEnumerable<Complex> scalarsList)
        {
            return scalarsList.Aggregate(
                Complex.One, 
                (current, s) => current * s
            );
        }

        public Complex Divide(Complex scalar1, Complex scalar2)
        {
            return scalar1 / scalar2;
        }

        public Complex Positive(Complex scalar)
        {
            return scalar;
        }

        public Complex Negative(Complex scalar)
        {
            return -scalar;
        }

        public Complex Inverse(Complex scalar)
        {
            return 1d / scalar;
        }

        public bool IsZero(Complex scalar)
        {
            return scalar.Real == 0d && scalar.Imaginary == 0d;
        }

        public bool IsZero(Complex scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar.Real > -ZeroEpsilon && scalar.Real < ZeroEpsilon && 
                  scalar.Imaginary > -ZeroEpsilon && scalar.Imaginary < ZeroEpsilon
                : scalar.Real == 0d && scalar.Imaginary == 0d;
        }

        public bool IsNearZero(Complex scalar)
        {
            return scalar.Real > -ZeroEpsilon && scalar.Real < ZeroEpsilon &&
                   scalar.Imaginary > -ZeroEpsilon && scalar.Imaginary < ZeroEpsilon;
        }

        public bool IsNotZero(Complex scalar)
        {
            return scalar.Real == 0d && scalar.Imaginary == 0d;
        }

        public bool IsNotZero(Complex scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar.Real <= -ZeroEpsilon || scalar.Real >= ZeroEpsilon || 
                  scalar.Imaginary <= -ZeroEpsilon || scalar.Imaginary >= ZeroEpsilon
                : scalar.Real != 0d && scalar.Imaginary != 0d;
        }

        public bool IsNotNearZero(Complex scalar)
        {
            return scalar.Real <= -ZeroEpsilon || scalar.Real >= ZeroEpsilon || 
                scalar.Imaginary <= -ZeroEpsilon || scalar.Imaginary >= ZeroEpsilon;
        }
    }
}