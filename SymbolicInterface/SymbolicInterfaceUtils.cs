using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.ExprFactory;
using TextComposerLib.Code.SyntaxTree.Expressions;
using Wolfram.NETLink;

namespace SymbolicInterface
{
    public static class SymbolicInterfaceUtils
    {
        /// <summary>
        /// Convert the given Mathematica Expr object into a SymbolicExpr object
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static SteExpression ToTextExpressionTree(this Expr expr)
        {
            var isNumber = expr.NumberQ();
            var isSymbol = expr.SymbolQ();

            if (isNumber)
            {
                return isSymbol 
                    ? SteExpressionUtils.CreateSymbolicNumber(expr.ToString()) 
                    : SteExpressionUtils.CreateLiteralNumber(expr.ToString());
            }
            
            if (isSymbol)
                return SteExpressionUtils.CreateVariable(expr.ToString());

            if (expr.Args.Length == 0)
                return SteExpressionUtils.CreateFunction(expr.ToString());
                //return new SymbolicExpr(expr.ToString(), isSymbol, isNumber);

            var args = new SteExpression[expr.Args.Length];

            for (var i = 0; i < expr.Args.Length; i++)
                args[i] = ToTextExpressionTree(expr.Args[i]);

            return SteExpressionUtils.CreateFunction(expr.Head.ToString(), args);
        }


        /// <summary>
        /// Convert this symbolic expression into a Mathematica expression object
        /// </summary>
        /// <param name="symbolicExpr"></param>
        /// <param name="cas"></param>
        /// <returns></returns>
        public static Expr ToExpr(this SteExpression symbolicExpr, MathematicaInterface cas)
        {
            return cas[symbolicExpr.ToString()];
        }

        /// <summary>
        /// Convert this symbolic expression into a Mathematica expression object
        /// </summary>
        /// <param name="symbolicExpr"></param>
        /// <param name="cas"></param>
        /// <returns></returns>
        public static Expr SimplifyToExpr(this SteExpression symbolicExpr, MathematicaInterface cas)
        {
            return cas[Mfs.Simplify[symbolicExpr.ToString()]];
        }



    }
}
