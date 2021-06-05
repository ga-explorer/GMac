namespace GMac.Engine.AST
{
    /// <summary>
    /// The base class for all GMacAST objects
    /// </summary>
    public abstract class AstObject : IAstObject
    {
        /// <summary>
        /// The root of the AST
        /// </summary>
        public abstract AstRoot Root { get; }

        /// <summary>
        /// True if this object holds useful information
        /// </summary>
        public abstract bool IsValid { get; }

        /// <summary>
        /// True if this objects holds no useful information
        /// </summary>
        public bool IsInvalid => IsValid == false;

        /// <summary>
        /// True if this object is a valid AstRoot object
        /// </summary>
        public virtual bool IsValidAstRoot => false;

        /// <summary>
        /// True if this object is a valid AstSymbol object
        /// </summary>
        public virtual bool IsValidSymbol => false;

        /// <summary>
        /// True if this object is a valid AstExpression object
        /// </summary>
        public virtual bool IsValidExpression => false;

        /// <summary>
        /// True if this object is a valid AstCommand object
        /// </summary>
        public virtual bool IsValidCommand => false;

        /// <summary>
        /// True if this object is a valid AstType object
        /// </summary>
        public virtual bool IsValidType => false;
    }
}
