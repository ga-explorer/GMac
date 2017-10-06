using GMac.GMacScripting;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Value;
using UtilLib.DataStructures.SimpleTree;
using Wolfram.NETLink;

namespace GMac.GMacAST.Expressions
{
    public abstract class AstValue : AstExpression, IAstObjectWithValue
    {
        #region Static members
        #endregion


        internal abstract ILanguageValue AssociatedValue { get; }

        internal override ILanguageExpression AssociatedExpression => AssociatedValue;


        public AstValue Value => this;

        public SimpleTreeNode<Expr> ValueSimpleTree => AssociatedValue.ToSimpleExprTree();

        public override bool IsValidValue => AssociatedValue != null;

        /// <summary>
        /// True if this is a valid boolean value
        /// </summary>
        public virtual bool IsValidBooleanValue => false;

        /// <summary>
        /// True if this is a valid integer value
        /// </summary>
        public virtual bool IsValidIntegerValue => false;

        /// <summary>
        /// True if this is a valid scalar value
        /// </summary>
        public virtual bool IsValidScalarValue => false;

        /// <summary>
        /// True if this is a valid multivector value
        /// </summary>
        public virtual bool IsValidMultivectorValue => false;

        /// <summary>
        /// True if this is a valid multivector term value
        /// </summary>
        public virtual bool IsValidMultivectorTermValue => false;

        /// <summary>
        /// True if this is a valid structure value
        /// </summary>
        public virtual bool IsValidStructureValue => false;

        /// <summary>
        /// True if this is a valid structure term value
        /// </summary>
        public virtual bool IsValidStructureTermValue => false;

        /// <summary>
        /// True if this is a valid primitive value
        /// </summary>
        public virtual bool IsValidPrimitiveValue => false;

        /// <summary>
        /// True if this is a valid composite value
        /// </summary>
        public virtual bool IsValidCompositeValue => false;
    }
}
