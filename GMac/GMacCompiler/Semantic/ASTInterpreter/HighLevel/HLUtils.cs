using IronyGrammars.Semantic.Command;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Expression.ValueAccess;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.HighLevel
{
    internal static class HlUtils
    {
        /// <summary>
        /// Create a copy of the given expression. If the expression is not a value access or a basic expression the
        /// original expression is returned
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static ILanguageExpression Duplicate(this ILanguageExpression expr)
        {
            var e1 = expr as LanguageValueAccess;

            if (e1 != null)
                return e1.Duplicate();

            var e2 = expr as LanguageExpressionBasic;

            if (e2 != null) 
                return e2.Duplicate();

            return expr;
        }

        /// <summary>
        /// Create a copy of an assignment command including its LHS value access and its RHS expression
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CommandAssign Duplicate(this CommandAssign command)
        {
            return new CommandAssign(
                command.ParentScope,
                command.LhsValueAccess.Duplicate(), 
                Duplicate(command.RhsExpression)
                );
        }
    }
}
