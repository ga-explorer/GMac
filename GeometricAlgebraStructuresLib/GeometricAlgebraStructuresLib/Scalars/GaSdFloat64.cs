using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraStructuresLib.Scalars
{
    public sealed class GaSdFloat64 : IGaScalarDomain<double>
    {
        public double ZeroEpsilon { get; set; }
            = 1e-13d;

        
        public double GetZero()
        {
            return 0d;
        }
        
        public double GetOne()
        {
            return 1d;
        }
        
        //Will this be better? [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Add(double scalar1, double scalar2)
        {
            return scalar1 + scalar2;
        }

        public double Add(params double[] scalarsList)
        {
            return scalarsList.Sum();
        }

        public double Add(IEnumerable<double> scalarsList)
        {
            return scalarsList.Sum();
        }

        public double Subtract(double scalar1, double scalar2)
        {
            return scalar1 - scalar2;
        }

        public double Times(double scalar1, double scalar2)
        {
            return scalar1 * scalar2;
        }

        public double Times(params double[] scalarsList)
        {
            return scalarsList.Aggregate(
                1d, 
                (current, scalar) => current * scalar
            );
        }

        public double Times(IEnumerable<double> scalarsList)
        {
            return scalarsList.Aggregate(
                1d, 
                (current, scalar) => current * scalar
            );
        }

        public double Divide(double scalar1, double scalar2)
        {
            return scalar1 / scalar2;
        }

        public double Positive(double scalar)
        {
            return scalar;
        }

        public double Negative(double scalar)
        {
            return -scalar;
        }

        public double Inverse(double scalar)
        {
            return 1d / scalar;
        }

        public bool IsZero(double scalar)
        {
            return scalar == 0d;
        }

        public bool IsZero(double scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar > -ZeroEpsilon && scalar < ZeroEpsilon
                : scalar == 0d;
        }

        public bool IsNearZero(double scalar)
        {
            return scalar > -ZeroEpsilon && scalar < ZeroEpsilon;
        }
        
        public bool IsNotZero(double scalar)
        {
            return scalar != 0d;
        }

        public bool IsNotZero(double scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar <= -ZeroEpsilon || scalar >= ZeroEpsilon
                : scalar != 0d;
        }

        public bool IsNotNearZero(double scalar)
        {
            return scalar <= -ZeroEpsilon || scalar >= ZeroEpsilon;
        }
    }
}