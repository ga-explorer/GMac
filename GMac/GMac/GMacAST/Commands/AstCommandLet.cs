using CodeComposerLib.Irony.Semantic.Command;
using GMac.GMacAST.Expressions;

namespace GMac.GMacAST.Commands
{
    public sealed class AstCommandLet : AstCommand, IAstObjectWithDatastoreValueAccess
    {
        internal CommandAssign AssociatedCommandLet { get; }

        internal override LanguageCommand AssociatedCommand => AssociatedCommandLet;


        public override bool IsValidCommandLet => AssociatedCommandLet != null;

        /// <summary>
        /// The RHS of this let command
        /// </summary>
        public AstExpression Expression => AssociatedCommandLet.RhsExpression.ToAstExpression();

        /// <summary>
        /// The type of the LHS of this let command
        /// </summary>
        public AstType GMacType => new AstType(AssociatedCommandLet.LhsValueAccess.ExpressionType);

        /// <summary>
        /// The type signature of the LHS of this let command
        /// </summary>
        public string GMacTypeSignature => AssociatedCommandLet.LhsValueAccess.ExpressionType.TypeSignature;

        /// <summary>
        /// The LHS of this let command
        /// </summary>
        public AstDatastoreValueAccess DatastoreValueAccess => new AstDatastoreValueAccess(AssociatedCommandLet.LhsValueAccess);

        /// <summary>
        /// True if this let command assigns value to a local variable
        /// </summary>
        public bool IsLocalAssignment => IsValid && AssociatedCommandLet.LhsValueAccess.IsLocalVariable;

        /// <summary>
        /// True if this let command assigns value to an input parameter of a macro
        /// </summary>
        public bool IsInputAssignment => IsValid && AssociatedCommandLet.LhsValueAccess.IsInputParameter;

        /// <summary>
        /// True if this let command assigns value to the output parameter of a macro
        /// </summary>
        public bool IsOutputAssignment => IsValid && AssociatedCommandLet.LhsValueAccess.IsOutputParameter;


        internal AstCommandLet(CommandAssign command)
        {
            AssociatedCommandLet = command;
        }
    }
}
