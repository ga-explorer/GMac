using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.SourceCode;
using Irony.Parsing;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    internal sealed class GMacExpressionCompositeGenerator : GMacAstSymbolGenerator
    {
        public static CompositeExpression Translate(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            context.PushState(node);

            var translator = new GMacExpressionCompositeGenerator();//new GMacExpressionCompositeGenerator(context);

            translator.SetContext(context);
            translator.Translate();

            context.PopState();

            var result = translator._generatedExpression;

            //MasterPool.Release(translator);

            return result;
        }


        private CompositeExpression _generatedExpression;


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _generatedExpression = null;
        //}


        //public GMacExpressionBasicGenerator BasicExpressionGenerator { get; private set; }


        //private GMacExpressionCompositeGenerator(GMacSymbolTranslatorContext context)
        //    : base(context)
        //{
        //    //BasicExpressionGenerator = new GMacExpressionBasicGenerator(context);
        //}

        //private GMacExpressionCompositeGenerator(GMacExpressionBasicGenerator basic_expr_gen)
        //    : base(basic_expr_gen.Context)
        //{
        //    BasicExpressionGenerator = basic_expr_gen;
        //}


        private SymbolLocalVariable translate_Identifier_Declaration(ParseTreeNode node)
        {
            //Read the name of the member
            var identifierName = GenUtils.Translate_Identifier(node.ChildNodes[0]);

            if (Context.ActiveParentScope.SymbolExists(identifierName))
                return CompilationLog.RaiseGeneratorError<SymbolLocalVariable>("Identifier name already used", node.ChildNodes[0]);

            var identifierType = GMacValueAccessGenerator.Translate_Direct_LanguageType(Context, node.ChildNodes[1]);

            //Create the member in the symbol table
            return 
                Context
                .ActiveParentCompositeExpression
                .DefineLocalVariable(identifierName, identifierType)
                .LocalVariable;
        }


        protected override void Translate()
        {
            //Create the composite expression
            _generatedExpression = CompositeExpression.Create(Context.ActiveParentScope);

            Context.PushState(_generatedExpression.ChildCommandBlockScope);

            //Create the output variable for the composite expression
            Context.ActiveParentCompositeExpression.OutputVariable = 
                translate_Identifier_Declaration(RootParseNode.ChildNodes[0]);

            //Translate the commands of the composite expression
            var nodeCommandBlockCommandsList = RootParseNode.ChildNodes[1];

            foreach (var subNode in nodeCommandBlockCommandsList.ChildNodes)
                GMacCommandGenerator.Translate(Context, subNode);

            Context.PopState();
        }
    }
}
