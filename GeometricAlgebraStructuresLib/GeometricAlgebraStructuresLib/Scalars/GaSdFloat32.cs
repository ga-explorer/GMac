using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraStructuresLib.Scalars
{
    public sealed class GaSdFloat32 : IGaScalarDomain<float>
    {
        public float ZeroEpsilon { get; set; }
            = 1e-7f;
        
        
        public float GetZero()
        {
            return 0f;
        }
        
        public float GetOne()
        {
            return 1f;
        }
        
        public float Add(float scalar1, float scalar2)
        {
            return scalar1 + scalar2;
        }

        public float Add(params float[] scalarsList)
        {
            return scalarsList.Sum();
        }

        public float Add(IEnumerable<float> scalarsList)
        {
            return scalarsList.Sum();
        }

        public float Subtract(float scalar1, float scalar2)
        {
            return scalar1 - scalar2;
        }

        public float Times(float scalar1, float scalar2)
        {
            return scalar1 * scalar2;
        }

        public float Times(params float[] scalarsList)
        {
            return scalarsList.Aggregate(
                1f, 
                (current, scalar) => current * scalar
            );
        }

        public float Times(IEnumerable<float> scalarsList)
        {
            return scalarsList.Aggregate(
                1f, 
                (current, scalar) => current * scalar
            );
        }

        public float Divide(float scalar1, float scalar2)
        {
            return scalar1 / scalar2;
        }

        public float Positive(float scalar)
        {
            return scalar;
        }

        public float Negative(float scalar)
        {
            return -scalar;
        }

        public float Inverse(float scalar)
        {
            return 1f / scalar;
        }
        
        public bool IsZero(float scalar)
        {
            return scalar == 0f;
        }

        public bool IsZero(float scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar > -ZeroEpsilon && scalar < ZeroEpsilon
                : scalar == 0f;
        }

        public bool IsNearZero(float scalar)
        {
            return scalar > -ZeroEpsilon && scalar < ZeroEpsilon;
        }
        
        public bool IsNotZero(float scalar)
        {
            return scalar != 0f;
        }

        public bool IsNotZero(float scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar <= -ZeroEpsilon || scalar >= ZeroEpsilon
                : scalar != 0f;
        }
        
        public bool IsNotNearZero(float scalar)
        {
            return scalar <= -ZeroEpsilon || scalar >= ZeroEpsilon;
        }
    }
}