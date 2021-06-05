using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Expression;
using GMac.Engine.AST.Commands;
using GMac.Engine.AST.Symbols;

namespace GMac.Engine.AST.Expressions
{
    public sealed class AstCompositeExpression : AstExpression
    {
        internal CompositeExpression AssociatedCompositeExpression { get; }

        internal override ILanguageExpression AssociatedExpression => AssociatedCompositeExpression;


        public override bool IsValidCompositeExpression => AssociatedCompositeExpression != null;

        /// <summary>
        /// The local variables declared inside this composite expression
        /// </summary>
        public IEnumerable<AstLocalVariable> LocalVariables
        {
            get
            {
                return
                    AssociatedCompositeExpression
                    .LocalVariables
                    .Select(localVar => localVar.ToAstLocalVariable());
            }
        }

        /// <summary>
        /// The commands used inside this composite expression
        /// </summary>
        public IEnumerable<AstCommand> Commands
        {
            get
            {
                return
                    AssociatedCompositeExpression
                    .Commands
                    .Select(command => command.ToAstCommand());
            }
        }

        /// <summary>
        /// Convert this composite expression into a command block object
        /// </summary>
        public AstCommandBlock ToCommandBlock => new AstCommandBlock(AssociatedCompositeExpression);

        /// <summary>
        /// The output local variable of this command block
        /// </summary>
        public AstLocalVariable OutputVariable => new AstLocalVariable(AssociatedCompositeExpression.OutputVariable);


        internal AstCompositeExpression(CompositeExpression expr)
        {
            AssociatedCompositeExpression = expr;
        }
    }
}
