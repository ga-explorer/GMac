using CodeComposerLib.Irony.Semantic.Expression.Value;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Symbol;
using GMac.GMacCompiler.Semantic.ASTConstants;

namespace GMac.GMacCompiler.Semantic.AST
{
    /// <summary>
    /// A GMac named constant
    /// </summary>
    public sealed class GMacConstant : SymbolNamedValue
    {
        /// <summary>
        /// The value associated with this constant
        /// </summary>
        public override ILanguageValue AssociatedValue { get; }

        internal GMacAst GMacRootAst => (GMacAst)RootAst;

        /// <summary>
        /// True if defined inside the scope of a frame
        /// </summary>
        internal bool IsFrameConstant => ParentLanguageSymbol.SymbolRoleName == RoleNames.Frame;

        /// <summary>
        /// True if defined inside the scope of a namespace
        /// </summary>
        internal bool IsNamespaceConstant => ParentLanguageSymbol.SymbolRoleName == RoleNames.Namespace;

        /// <summary>
        /// The parent frame (for a frame constant only)
        /// </summary>
        internal GMacFrame ParentFrame => IsFrameConstant ? ParentLanguageSymbol as GMacFrame : null;


        internal GMacConstant(string constantName, LanguageScope parentScope, ILanguageValue rhsValue)
            : base(constantName, parentScope, RoleNames.Constant, rhsValue.ExpressionType)
        {
            AssociatedValue = rhsValue;
        }
    }
}
