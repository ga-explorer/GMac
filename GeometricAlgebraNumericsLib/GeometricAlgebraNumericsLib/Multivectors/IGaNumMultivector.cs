using System.Collections.Generic;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    /// <summary>
    /// This interface represents a multivector with floating point scalar
    /// coefficients
    /// </summary>
    public interface IGaNumMultivector : IEnumerable<KeyValuePair<int, double>>
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
        /// The basis blade coefficient of the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        double this[int id] { get; }

        /// <summary>
        /// The basis blade coefficient of the given grade and index
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        double this[int grade, int index] { get; }

        /// <summary>
        /// The basis blade ids with coefficients stored in this multivector
        /// </summary>
        IEnumerable<int> BasisBladeIds { get; }

        /// <summary>
        /// The basis blade ids with non-zero coefficients stored in this multivector
        /// </summary>
        IEnumerable<int> NonZeroBasisBladeIds { get; }

        /// <summary>
        /// The basis blade coefficients stored in this multivector
        /// </summary>
        IEnumerable<double> BasisBladeScalars { get; }

        /// <summary>
        /// The basis blade non-zero coefficients stored in this multivector
        /// </summary>
        IEnumerable<double> NonZeroBasisBladeScalars { get; }

        /// <summary>
        /// The terms stored in this multivector
        /// </summary>
        IEnumerable<KeyValuePair<int, double>> Terms { get; }

        /// <summary>
        /// The non-zero terms stored in this multivector
        /// </summary>
        IEnumerable<KeyValuePair<int, double>> NonZeroTerms { get; }

        /// <summary>
        /// True if this is a temporary multivector
        /// </summary>
        bool IsTemp { get; }

        /// <summary>
        /// The number of terms stored in this multivector
        /// </summary>
        int TermsCount { get; }

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
        /// True if this multivector holds no terms internally
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();

        /// <summary>
        /// True if this multivector is almost zero
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        bool IsNearZero(double epsilon);

        /// <summary>
        /// True if this multivector contains the given basis blade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ContainsBasisBlade(int id);

        /// <summary>
        /// Simplify the internal structure of this multivector
        /// </summary>
        void Simplify();

        /// <summary>
        /// Convert this multivector into an array of scalars
        /// </summary>
        /// <returns></returns>
        double[] TermsToArray();

        /// <summary>
        /// Convert this multivector into a new multivector. If this is already
        /// of type GaNumMultivector this same multivector is returned
        /// </summary>
        /// <returns></returns>
        GaNumMultivector ToMultivector();

        /// <summary>
        /// Extract the vector part of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        GaNumMultivector GetVectorPart();
    }
}