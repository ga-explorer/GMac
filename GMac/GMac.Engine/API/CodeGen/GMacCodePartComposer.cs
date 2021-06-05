using CodeComposerLib.Languages;
using GMac.Engine.API.Target;
using GMac.Engine.AST;
using GMac.Engine.AST.Symbols;
using TextComposerLib.Logs.Progress;
using TextComposerLib.Text.Parametric;

namespace GMac.Engine.API.CodeGen
{
    /// <summary>
    /// This abstract class can be used to implement a sub-process of code generation using the main
    /// code library generator composnents
    /// </summary>
    public abstract class GMacCodePartComposer : IProgressReportSource
    {
        public GMacCodeLibraryComposer LibraryComposer { get; }

        public ParametricTextComposerCollection Templates => LibraryComposer.Templates;

        public AstRoot Root => LibraryComposer.Root;

        public AstSymbolsCollection SelectedSymbols => LibraryComposer.SelectedSymbols;

        public virtual string ProgressSourceId => GetType().FullName;

        public ProgressComposer Progress => GMacEngineUtils.Progress;

        public GMacLanguageServer GMacLanguage => LibraryComposer.GMacLanguage;

        public LanguageCodeGenerator CodeGenerator => LibraryComposer.GMacLanguage.CodeGenerator;

        public LanguageSyntaxFactory SyntaxFactory => LibraryComposer.GMacLanguage.SyntaxFactory;


        protected GMacCodePartComposer(GMacCodeLibraryComposer libGen)
        {
            LibraryComposer = libGen;
        }
    }
}
