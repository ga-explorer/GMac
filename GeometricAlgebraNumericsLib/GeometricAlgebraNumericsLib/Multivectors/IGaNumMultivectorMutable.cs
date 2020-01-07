namespace GeometricAlgebraNumericsLib.Multivectors
{
    /// <summary>
    /// This interface represents a multivector used in intermediate computations
    /// Updating the scalar coefficients of intermediate multivectors is more
    /// efficient than regular multivectors
    /// </summary>
    public interface IGaNumMultivectorMutable : IGaNumMultivector
    {
        /// <summary>
        /// Update the given basis blade coefficient by adding the given scalar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scalarValue"></param>
        /// <returns></returns>
        IGaNumMultivectorMutable UpdateTerm(int id, double scalarValue);

        /// <summary>
        /// Update the given basis blade coefficient by adding or subtracting the given scalar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isNegative"></param>
        /// <param name="scalarValue"></param>
        /// <returns></returns>
        IGaNumMultivectorMutable UpdateTerm(int id, bool isNegative, double scalarValue);

        /// <summary>
        /// Set the given basis blade coefficient
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scalarValue"></param>
        /// <returns></returns>
        IGaNumMultivectorMutable SetTerm(int id, double scalarValue);

        /// <summary>
        /// Set the given basis blade coefficient
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isNegative"></param>
        /// <param name="scalarValue"></param>
        /// <returns></returns>
        IGaNumMultivectorMutable SetTerm(int id, bool isNegative, double scalarValue);
    }
}