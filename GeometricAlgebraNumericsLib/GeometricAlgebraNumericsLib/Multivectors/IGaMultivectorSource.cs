using System.Collections.Generic;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    public interface IGaMultivectorSource<T>
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
        /// The number of terms stored in this multivector
        /// </summary>
        int StoredTermsCount { get; }

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
        /// Try to get the given term scalar value if stored in this multivector
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGetValue(int id, out T value);

        /// <summary>
        /// Try to get the given term scalar value if stored in this multivector
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGetValue(int grade, int index, out T value);


        /// <summary>
        /// The basis blade ids with coefficients stored in this multivector
        /// </summary>
        IEnumerable<int> GetStoredTermIds();

        /// <summary>
        /// The basis blade ids with non-zero coefficients stored in this multivector
        /// </summary>
        IEnumerable<int> GetNonZeroTermIds();

        /// <summary>
        /// The basis blade coefficients stored in this multivector
        /// </summary>
        IEnumerable<T> GetStoredTermScalars();

        /// <summary>
        /// The basis blade non-zero coefficients stored in this multivector
        /// </summary>
        IEnumerable<T> GetNonZeroTermScalars();


        /// <summary>
        /// True if this multivector stores no terms internally
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();

        /// <summary>
        /// Test if this factory contains the given term
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ContainsStoredTerm(int id);

        /// <summary>
        /// Test if this factory contains the given term
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        bool ContainsStoredTerm(int grade, int index);

        /// <summary>
        /// Test if this factory contains term of the given grade
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        bool ContainsStoredKVector(int grade);


        /// <summary>
        /// Try to get the given term if stored in this multivector
        /// </summary>
        /// <param name="id"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        bool TryGetTerm(int id, out GaTerm<T> term);

        /// <summary>
        /// Try to get the given term if stored in this multivector
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        bool TryGetTerm(int grade, int index, out GaTerm<T> term);


        /// <summary>
        /// Get all terms currently stored inside this factory
        /// </summary>
        /// <returns></returns>
        IEnumerable<GaTerm<T>> GetStoredTerms();

        /// <summary>
        /// Get all terms of given grade currently stored inside this factory
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        IEnumerable<GaTerm<T>> GetStoredTerms(int grade);

        /// <summary>
        /// Get all non-zero terms currently stored inside this factory
        /// </summary>
        /// <returns></returns>
        IEnumerable<GaTerm<T>> GetNonZeroTerms();

        /// <summary>
        /// Get all non-zero terms of given grade currently stored inside this factory
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        IEnumerable<GaTerm<T>> GetNonZeroTerms(int grade);
    }
}