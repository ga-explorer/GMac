using System.Collections.Generic;
using IronyGrammars.Semantic.Operator;

namespace IronyGrammars.Semantic.Expression
{
    public interface ILanguageExpressionBasic : ILanguageExpression
    {
        ILanguageOperator Operator { get; }

        IEnumerable<ILanguageExpressionAtomic> RhsOperands { get; }
    }
}
