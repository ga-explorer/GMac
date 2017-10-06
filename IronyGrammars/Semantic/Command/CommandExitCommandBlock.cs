using IronyGrammars.Semantic.Scope;

namespace IronyGrammars.Semantic.Command
{
    /// <summary>
    /// This class represents an exit command from a target command block
    /// </summary>
    public class CommandExitCommandBlock : LanguageCommand
    {
        /// <summary>
        /// The command block to be exited
        /// </summary>
        public CommandBlock TargetBlock { get; private set; }


        public CommandExitCommandBlock(LanguageScope parentScope, CommandBlock targetBlock)
            : base(parentScope)
        {
            TargetBlock = targetBlock;
        }
    }
}
