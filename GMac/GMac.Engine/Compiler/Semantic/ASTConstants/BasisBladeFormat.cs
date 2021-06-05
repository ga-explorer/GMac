namespace GMac.Engine.Compiler.Semantic.ASTConstants
{
    /// <summary>
    /// Methods for displaying the name of a basis blade
    /// </summary>
    public enum BasisBladeFormat
    {
        /// <summary>
        /// Canonical: using the outer product of basis vectors in canonical order (e1^e2^e4)
        /// </summary>
        Canonical = 0,

        /// <summary>
        /// Binary Indexed: using the binary representation (B1011)
        /// </summary>
        BinaryIndexed = 1,

        /// <summary>
        /// Indexed: using the direct basis blade ID (E11)
        /// </summary>
        Indexed = 2,

        /// <summary>
        /// Grade plus Index: using the grade plus index (G3I5)
        /// </summary>
        GradePlusIndex = 3
    }
}
