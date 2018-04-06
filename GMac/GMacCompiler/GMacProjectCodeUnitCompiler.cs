using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTGenerator;
using Irony.Parsing;
using IronyGrammars.Compiler;
using IronyGrammars.SourceCode;
using TextComposerLib.Logs.Progress;

namespace GMac.GMacCompiler
{
    internal sealed class GMacProjectCodeUnitCompiler : LanguageProjectCodeUnitCompiler
    {

        public override string ProgressSourceId => "GMac Project Code Unit Compiler";

        public override ProgressComposer Progress => GMacSystemUtils.Progress;

        public GMacAst RootGMacAst => (GMacAst)RootAst;

        public GMacSymbolTranslatorContext Context => (GMacSymbolTranslatorContext)TranslatorContext;


        internal GMacProjectCodeUnitCompiler(GMacProjectCompiler parentCompiler, ISourceCodeUnit codeUnit, ParseTreeNode rootParseNode)
            : base(parentCompiler, codeUnit, rootParseNode)
        {
        }

        protected override void TranslateToAst()
        {
            Context.PushState(RootParseNode);

            GMacAstGenerator.Translate(Context);

            Context.PopState();
        }
    }
}
