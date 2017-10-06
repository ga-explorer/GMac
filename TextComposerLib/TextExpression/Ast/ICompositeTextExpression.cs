using System.Collections.Generic;

namespace TextComposerLib.TextExpression.Ast
{
    public interface ICompositeTextExpression : ITextExpression
    {
        TeIdentifier Name { get; }

        bool IsNamed { get; }

        bool IsNameless { get; }

        IEnumerable<ITextExpression> ChildExpressions { get; }
    }
}