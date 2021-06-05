using CodeComposerLib.Irony.Compiler;
using CodeComposerLib.Irony.SourceCode;
using GMac.Engine.Compiler.Semantic.AST;
using GMac.Engine.Compiler.Semantic.ASTGenerator;
using Irony.Parsing;
using TextComposerLib.Logs.Progress;

namespace GMac.Engine.Compiler
{
    internal sealed class GMacProjectCodeUnitCompiler : LanguageProjectCodeUnitCompiler
    {

        public override string ProgressSourceId => "GMac Project Code Unit Compiler";

        public override ProgressComposer Progress => GMacEngineUtils.Progress;

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
