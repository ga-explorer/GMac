using System.Collections.Generic;
using GeometricAlgebraStructuresLib.Storage;

namespace GeometricAlgebraStructuresLib.Multivectors
{
    public interface IGaMultivector<T> : IEnumerable<IGaTerm<T>>
    {
        /// <summary>
        /// The dimension of the base Vector Space of the multivector
        /// </summary>
        int VSpaceDimension { get; }

        /// <summary>
        /// The dimension of the Geometric Algebra space of the multivector
        /// </summary>
        int GaSpaceDimension { get; }

        /// <summary>
        /// The internal storage structure holding the terms of this multivector
        /// </summary>
        IGaMultivectorStorage<T> Storage { get; }

        /// <summary>
        /// Get the scalar value coefficient of the given basis blade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T this[int id] { get; }

        /// <summary>
        /// Get the scalar value coefficient of the given basis blade
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        T this[int grade, int index] { get; }

        /// <summary>
        /// True if this multivector is a single term
        /// </summary>
        /// <returns></returns>
        bool IsTerm();

        /// <summary>
        /// True if this multivector is a single scalar
        /// </summary>
        /// <returns></returns>
        bool IsScalar();

        /// <summary>
        /// True if this multivector is zero
        /// </summary>
        /// <returns></returns>
        bool IsZero();

        /// <summary>
        /// True if this multivector is almost zero
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        bool IsNearZero(T epsilon);
        
        /// <summary>
        /// Extract the vector part of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        IGaMultivector<T> GetVectorPart();

        /// <summary>
        /// Extract the vector part of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        IGaMultivector<T> GetKVectorPart(int grade);

        /// <summary>
        /// Get all k-vectors inside this multivector
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGaMultivector<T>> GetKVectorParts();
        
        IGaMultivector<T> GetNegative();

        IGaMultivector<T> GetReverse();

        IGaMultivector<T> GetGradeInv();

        IGaMultivector<T> GetCliffConj();
    }
}