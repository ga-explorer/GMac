namespace GMac.GMacAST
{
    public interface IAstObject
    {
        /// <summary>
        /// True if this information structure is ready for use (i.e. holds useful information)
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// True if this information structure is not ready for use (i.e. holds no useful information)
        /// </summary>
        bool IsInvalid { get; }

        /// <summary>
        /// The root of the AST of this object
        /// </summary>
        AstRoot Root { get; }
    }
}