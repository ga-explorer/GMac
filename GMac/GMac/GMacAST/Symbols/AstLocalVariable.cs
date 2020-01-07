using System.Linq;
using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Symbol;
using GMac.GMacAST.Commands;
using GMac.GMacAST.Expressions;

namespace GMac.GMacAST.Symbols
{
    public sealed class AstLocalVariable : AstSymbol, IAstObjectWithDatastoreValueAccess
    {
        #region Static members
        #endregion


        internal SymbolLocalVariable AssociatedVariable { get; }

        internal override LanguageSymbol AssociatedSymbol => AssociatedVariable;


        public override bool IsValidLocalVariable => AssociatedVariable != null;

        public override bool IsValidDatastore => AssociatedVariable != null;

        public override bool IsValidVariableDatastore => AssociatedVariable != null;

        public AstType GMacType => new AstType(AssociatedVariable.SymbolType);

        public string GMacTypeSignature => AssociatedVariable.SymbolTypeSignature;

        public AstExpression Expression => new AstDatastoreValueAccess(LanguageValueAccess.Create(AssociatedVariable));

        public AstDatastoreValueAccess DatastoreValueAccess => new AstDatastoreValueAccess(LanguageValueAccess.Create(AssociatedVariable));

        /// <summary>
        /// The Command Block where this local variable is declared
        /// </summary>
        public AstCommandBlock ParentBlock
        {
            get
            {
                var scope = AssociatedVariable.ParentScope as ScopeCommandBlockChild;

                return
                    ReferenceEquals(scope, null)
                    ? null
                    : new AstCommandBlock(scope.ParentCommandBlock);
            }
        }

        /// <summary>
        /// The command where this local variable is declared
        /// </summary>
        public AstCommandDeclare ParentCommandDeclare
        {
            get
            {
                var scope = AssociatedVariable.ParentScope as ScopeCommandBlockChild;

                if (ReferenceEquals(scope, null))
                    return null;

                var command = 
                    scope
                    .ParentCommandBlock
                    .DeclareVariableCommands
                    .FirstOrDefault(
                        item => ReferenceEquals(item.LocalVariable, AssociatedVariable)
                    );

                return
                    ReferenceEquals(command, null)
                        ? null
                        : new AstCommandDeclare(command);
            }
        }

        internal AstLocalVariable(SymbolLocalVariable parameter)
        {
            AssociatedVariable = parameter;
        }
    }
}
