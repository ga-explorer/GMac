using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Scope;
using IronyGrammars.Semantic.Type;

namespace IronyGrammars.Semantic.Symbol
{
    /// <summary>
    /// This class represents a named value (a named constant) data store language symbol
    /// </summary>
    public abstract class SymbolNamedValue : SymbolDataStore
    {
        public abstract ILanguageValue AssociatedValue { get; }


        protected SymbolNamedValue(string symbolName, LanguageScope parentScope, string symbolRoleName, ILanguageType valueType)
            : base(symbolName, valueType, parentScope, symbolRoleName)
        {
        }
    }
}
