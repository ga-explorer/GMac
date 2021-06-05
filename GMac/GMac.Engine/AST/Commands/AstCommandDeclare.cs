using CodeComposerLib.Irony.Semantic.Command;
using GMac.Engine.AST.Symbols;

namespace GMac.Engine.AST.Commands
{
    public sealed class AstCommandDeclare : AstCommand, IAstObjectWithType
    {
        internal CommandDeclareVariable AssociatedCommandDeclare { get; }

        internal override LanguageCommand AssociatedCommand => AssociatedCommandDeclare;


        public override bool IsValidCommandDeclare => AssociatedCommandDeclare != null;

        /// <summary>
        /// The type of the local variable declared by this command
        /// </summary>
        public AstType GMacType => new AstType(AssociatedCommandDeclare.LocalVariable.SymbolType);

        /// <summary>
        /// The type signature string of the local variable declared by this command
        /// </summary>
        public string GMacTypeSignature => AssociatedCommandDeclare.LocalVariable.SymbolTypeSignature;

        /// <summary>
        /// The local variable declared by this command
        /// </summary>
        public AstLocalVariable LocalVariable => new AstLocalVariable(AssociatedCommandDeclare.LocalVariable);


        internal AstCommandDeclare(CommandDeclareVariable command)
        {
            AssociatedCommandDeclare = command;
        }
    }
}
