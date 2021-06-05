using GMac.Engine.AST;

namespace GMac.Engine.API.Binding
{
    /// <summary>
    /// This is the main interface for tree binding patterns having types (scalars, multivectors, and structures)
    /// </summary>
    public interface IGMacTypedBinding : IGMacBinding
    {
        /// <summary>
        /// The language type of this pattern
        /// </summary>
        AstType GMacType { get; }
    }
}