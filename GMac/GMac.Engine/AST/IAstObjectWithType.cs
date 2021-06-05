namespace GMac.Engine.AST
{
    public interface IAstObjectWithType : IAstObject
    {
        /// <summary>
        /// The AST type information for this object
        /// </summary>
        AstType GMacType { get; }

        /// <summary>
        /// The AST type signature of this object
        /// </summary>
        string GMacTypeSignature { get; }
    }
}