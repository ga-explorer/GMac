using GMac.GMacCompiler.Semantic.ASTConstants;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Operator;
using IronyGrammars.Semantic.Scope;
using IronyGrammars.Semantic.Symbol;
using IronyGrammars.Semantic.Type;

namespace GMac.GMacCompiler.Semantic.AST
{
    /// <summary>
    /// The multivector language type associated with a frame
    /// </summary>
    public sealed class GMacFrameMultivector : LanguageSymbol, ILanguageType, ILanguageOperator
    {
        /// <summary>
        /// The parent frame of this multivector type
        /// </summary>
        internal GMacFrame ParentFrame => (GMacFrame)ParentLanguageSymbol;

        public string TypeSignature => SymbolQualifiedName;

        public string OperatorName => "cast_to<" + SymbolAccessName + ">";

        internal GMacAst GMacRootAst => (GMacAst)RootAst;


        internal GMacFrameMultivector(LanguageScope frameScope)
            : base("Multivector", frameScope, RoleNames.FrameMultivector)
        {
        }


        public ILanguageOperator DuplicateOperator()
        {
            return this;
        }


        public bool IsSameType(ILanguageType languageType)
        {
            if (!(languageType is GMacFrameMultivector))
                return false;

            return ((GMacFrameMultivector)languageType).ObjectId == ObjectId;
        }

        public bool IsCompatibleType(ILanguageType languageType)
        {
            if (!(languageType is GMacFrameMultivector))
                return false;

            return ((GMacFrameMultivector)languageType).ObjectId == ObjectId;
        }


        internal BasicUnary CreateCastExpression(ILanguageExpressionAtomic operand)
        {
            return BasicUnary.CreateTypeCast(this, this, operand);
        }

        internal BasicPolyadic CreateConstructorExpression()
        {
            return
                BasicPolyadic.Create(
                    this,
                    GMacFrameMultivectorConstructor.Create(this)
                    );
        }

        internal BasicPolyadic CreateConstructorExpression(OperandsByIndex operands)
        {
            return
                BasicPolyadic.Create(
                    this,
                    GMacFrameMultivectorConstructor.Create(this),
                    operands
                    );
        }

        internal BasicPolyadic CreateConstructorExpression(ILanguageExpressionAtomic defaultValueSource)
        {
            return
                BasicPolyadic.Create(
                    this,
                    GMacFrameMultivectorConstructor.Create(this, defaultValueSource)
                    );
        }

        internal BasicPolyadic CreateConstructorExpression(ILanguageExpressionAtomic defaultValueSource, OperandsByIndex operands)
        {
            return
                BasicPolyadic.Create(
                    this,
                    GMacFrameMultivectorConstructor.Create(this, defaultValueSource),
                    operands
                    );
        }

        
    }
}
