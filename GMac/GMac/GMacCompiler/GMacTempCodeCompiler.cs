using System;
using System.Collections.Generic;
using CodeComposerLib.Irony.Compiler;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.SourceCode;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTGenerator;
using Irony.Parsing;
using TextComposerLib.Logs.Progress;

namespace GMac.GMacCompiler
{
    public abstract class GMacTempCodeCompiler : LanguageTempCodeCompiler
    {
        public sealed override ProgressComposer Progress => GMacSystemUtils.Progress;

        internal GMacAst RootGMacAst => (GMacAst)RootAst;

        internal GMacSymbolTranslatorContext Context
        {
            get
            {
                return (GMacSymbolTranslatorContext)TranslatorContext;
            }
            private set
            {
                TranslatorContext = value;
            }
        }


        internal GMacTempCodeCompiler()
        {
        }

        internal GMacTempCodeCompiler(LanguageCompilationLog compilationLog)
            : base(compilationLog)
        {
        }


        protected bool InitializeCompiler(string codeText, Func<string, LanguageCompilationLog, ParseTreeNode> parsingFunction, GMacScopeResolutionContext scopeInfo)
        {
            return 
                scopeInfo.HasOpenedScopes 
                ? InitializeCompiler(codeText, parsingFunction, scopeInfo.MainScope, scopeInfo.OpenedScopes) 
                : InitializeCompiler(codeText, parsingFunction, scopeInfo.MainScope);
        }

        protected override void InitializeTranslatorContext(LanguageScope parentScope)
        {
            Context = GMacSymbolTranslatorContext.Create(this);

            Context.PushState(parentScope, RootParseNode);
        }

        protected override void InitializeTranslatorContext(LanguageScope parentScope, IEnumerable<LanguageScope> openedScopes)
        {
            Context = GMacSymbolTranslatorContext.Create(this);

            Context.PushState(parentScope, RootParseNode);

            foreach (var scope in openedScopes)
                Context.OpenScope(scope);
        }
    }
}
