using GMac.GMacAST.Expressions;
using UtilLib.DataStructures.SimpleTree;
using Wolfram.NETLink;

namespace GMac.GMacAST
{
    public interface IAstObjectWithValue : IAstObjectWithExpression
    {
        /// <summary>
        /// The AST value of this object
        /// </summary>
        AstValue Value { get; }

        /// <summary>
        /// The AST value of this object represented as a simple tree of symbolic expressions
        /// </summary>
        SimpleTreeNode<Expr> ValueSimpleTree { get; }
    }
}