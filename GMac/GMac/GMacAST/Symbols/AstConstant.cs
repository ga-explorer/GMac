using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Symbol;
using DataStructuresLib.SimpleTree;
using GMac.GMacAST.Expressions;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacScripting;
using Wolfram.NETLink;

namespace GMac.GMacAST.Symbols
{
    public sealed class AstConstant : AstSymbol, IAstObjectWithValue, IAstObjectWithDatastoreValueAccess
    {
        #region Static members
        #endregion


        internal GMacConstant AssociatedConstant { get; }

        internal override LanguageSymbol AssociatedSymbol => AssociatedConstant;


        public override bool IsValidConstant => AssociatedConstant != null;

        public override bool IsValidDatastore => AssociatedConstant != null;

        public override bool IsValidConstantDatastore => AssociatedConstant != null;

        public AstType GMacType => new AstType(AssociatedConstant.SymbolType);

        public AstValue Value => AssociatedConstant.AssociatedValue.ToAstValue();

        public SimpleTreeNode<Expr> ValueSimpleTree => AssociatedConstant.AssociatedValue.ToSimpleExprTree();

        public string GMacTypeSignature => AssociatedConstant.SymbolTypeSignature;

        public AstExpression Expression => AssociatedConstant.AssociatedValue.ToAstExpression();

        public AstDatastoreValueAccess DatastoreValueAccess => new AstDatastoreValueAccess(LanguageValueAccess.Create(AssociatedConstant));


        internal AstConstant(GMacConstant constant)
        {
            AssociatedConstant = constant;
        }
    }
}
