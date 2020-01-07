using GMac.GMacAST.Expressions;

namespace GMac.GMacAST
{
    public interface IAstObjectWithExpression : IAstObjectWithType
    {
        /// <summary>
        /// The AST expression of this object
        /// </summary>
        AstExpression Expression { get; }
    }
}
