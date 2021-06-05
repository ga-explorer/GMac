using CodeComposerLib.Irony.Semantic.Command;
using Irony.Parsing;

namespace GMac.Engine.Compiler.Semantic.ASTGenerator
{
    internal sealed class GMacCommandBlockGenerator : GMacAstSymbolGenerator
    {
        public static CommandBlock Translate(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            context.PushState(node);

            var translator = new GMacCommandBlockGenerator();// new GMacCommandBlockGenerator(context);

            translator.SetContext(context);
            translator.Translate();

            context.PopState();

            var result = translator._generatedBlock;

            //MasterPool.Release(translator);

            return result;
        }


        private CommandBlock _generatedBlock;


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _generatedBlock = null;
        //}

        protected override void Translate()
        {
            //Create the command block
            _generatedBlock = CommandBlock.Create(Context.ActiveParentScope);

            Context.PushState(_generatedBlock.ChildCommandBlockScope);

            //Begin translation of the composite expression commands
            var nodeBlockExpressionCommandsList = RootParseNode.ChildNodes[0];

            foreach (var subnode in nodeBlockExpressionCommandsList.ChildNodes)
                GMacCommandGenerator.Translate(Context, subnode);

            Context.PopState();
        }
    }
}
