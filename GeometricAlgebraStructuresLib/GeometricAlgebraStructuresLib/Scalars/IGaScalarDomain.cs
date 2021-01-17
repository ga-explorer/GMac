using System.Collections.Generic;

namespace GeometricAlgebraStructuresLib.Scalars
{
    public interface IGaScalarDomain<T>
    {
        T GetZero();
        
        T GetOne();
        
        T Add(T scalar1, T scalar2);

        T Add(params T[] scalarsList);

        T Add(IEnumerable<T> scalarsList);
        
        T Subtract(T scalar1, T scalar2);

        T Times(T scalar1, T scalar2);
        
        T Times(params T[] scalarsList);

        T Times(IEnumerable<T> scalarsList);

        T Divide(T scalar1, T scalar2);

        /// <summary>
        /// Get same value of given scalar
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        T Positive(T scalar);
        
        /// <summary>
        /// Get negative value of given scalar
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        T Negative(T scalar);

        T Inverse(T scalar);

        bool IsZero(T scalar);

        bool IsZero(T scalar, bool nearZeroFlag);

        bool IsNearZero(T scalar);

        bool IsNotZero(T scalar);

        bool IsNotZero(T scalar, bool nearZeroFlag);

        bool IsNotNearZero(T scalar);
    }
}