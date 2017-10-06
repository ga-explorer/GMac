using System.Collections.Generic;
using System.Linq;
using GMac.GMacAST.Symbols;
using IronyGrammars.Semantic.Command;

namespace GMac.GMacAST.Commands
{
    public sealed class AstCommandBlock : AstCommand
    {
        internal CommandBlock AssociatedCommandBlock { get; }

        internal override LanguageCommand AssociatedCommand => AssociatedCommandBlock;


        public override bool IsValidCommandBlock => AssociatedCommandBlock != null;

        /// <summary>
        /// The local variables declared inside this command block
        /// </summary>
        public IEnumerable<AstLocalVariable> LocalVariables
        {
            get
            {
                return 
                    AssociatedCommandBlock
                    .LocalVariables
                    .Select(localVar => localVar.ToAstLocalVariable());
            }
        }

        /// <summary>
        /// The commands inside this command block
        /// </summary>
        public IEnumerable<AstCommand> Commands
        {
            get
            {
                return 
                    AssociatedCommandBlock
                    .Commands
                    .Select(command => command.ToAstCommand());
            }
        }

        /// <summary>
        /// The let commands inside this command block
        /// </summary>
        public IEnumerable<AstCommandLet> LetCommands
        {
            get
            {
                return
                    AssociatedCommandBlock
                    .Commands
                    .Select(c => c as CommandAssign)
                    .Where(c => ReferenceEquals(c, null) == false)
                    .Select(command => command.ToAstCommandLet());
            }
        }

        /// <summary>
        /// The declare commands inside this command block
        /// </summary>
        public IEnumerable<AstCommandDeclare> DeclareCommands
        {
            get
            {
                return
                    AssociatedCommandBlock
                    .Commands
                    .Select(c => c as CommandDeclareVariable)
                    .Where(c => ReferenceEquals(c, null) == false)
                    .Select(command => command.ToAstCommandDeclare());
            }
        }

        /// <summary>
        /// The commands inside this command block except for declare commands
        /// </summary>
        public IEnumerable<AstCommand> NonDeclareCommands
        {
            get
            {
                return
                    AssociatedCommandBlock
                    .Commands
                    .Where(c => ReferenceEquals(c as CommandDeclareVariable, null))
                    .Select(command => command.ToAstCommand());
            }
        }

        /// <summary>
        /// The block commands inside this command block
        /// </summary>
        public IEnumerable<AstCommandBlock> BlockCommands
        {
            get
            {
                return
                    AssociatedCommandBlock
                    .Commands
                    .Select(c => c as CommandBlock)
                    .Where(c => ReferenceEquals(c, null) == false)
                    .Select(command => command.ToAstCommandBlock());
            }
        }


        //public bool IsCompositeExpression
        //{
        //    get { return AssociatedCommandBlock is CompositeExpression; }
        //}

        //public AstCompositeExpression ToCompositeExpression
        //{
        //    get
        //    {
        //        var expr = AssociatedCommandBlock as CompositeExpression;

        //        return
        //            ReferenceEquals(expr, null)
        //            ? null
        //            : new AstCompositeExpression(expr);
        //    }
        //}


        internal AstCommandBlock(CommandBlock command)
        {
            AssociatedCommandBlock = command;
        }
    }
}
