using GMac.Engine.AST.Expressions;

namespace GMac.Engine.AST
{
    public interface IAstObjectWithExpression : IAstObjectWithType
    {
        /// <summary>
        /// The AST expression of this object
        /// </summary>
        AstExpression Expression { get; }
    }
}
