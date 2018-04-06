﻿using System;
using System.IO;
using System.Text;
using GMac.GMacAST;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTGenerator;
using GMac.GMacCompiler.Syntax;
using GMac.GMacIDE;
using Irony.Parsing;
using IronyGrammars.Compiler;
using IronyGrammars.SourceCode;
using TextComposerLib.Logs.Progress;

namespace GMac.GMacCompiler
{
    public sealed class GMacProjectCompiler : LanguageProjectCompiler
    {
        /// <summary>
        /// This class automatically creates a dsl code file on disk, adds it to a project file, 
        /// and compiles the project returning a GMacProjectCompiler object holding all this information.
        /// </summary>
        /// <param name="dslCode"></param>
        /// <param name="mainFolder"></param>
        /// <param name="baseFileName"></param>
        /// <returns></returns>
        public static GMacProjectCompiler CompileDslCode(string dslCode, string mainFolder, string baseFileName)
        {
            var compiler = new GMacProjectCompiler();

            if (compiler.SetProgressRunning() == false) return compiler;

            try
            {
                var projectFilePath = Path.Combine(mainFolder, baseFileName + ".gmacproj");

                var codeFilePath = Path.Combine(mainFolder, baseFileName + ".gmac");

                File.WriteAllText(codeFilePath, dslCode, Encoding.Unicode);

                var project = GMacProject.CreateNew(projectFilePath);

                project.AddSourceFile(codeFilePath, Encoding.Unicode);

                project.SaveProjectToXmlFile();

                compiler.Compile(project);
            }
            catch (Exception e)
            {
                compiler.ReportError(e);
            }
            finally
            {
                compiler.SetProgressNotRunning();
            }

            return compiler;
        }


        internal GMacAst RootGMacAst => (GMacAst)RootAst;

        internal GMacSymbolTranslatorContext Context => (GMacSymbolTranslatorContext) TranslatorContext;


        public override string ProgressSourceId => "GMac Project Compiler";

        public override ProgressComposer Progress => GMacSystemUtils.Progress;

        public AstRoot Root => RootGMacAst.ToAstRoot();


        protected override bool CodeChanged(LanguageCodeProject dslProject)
        {
            return ReferenceEquals(RootGMacAst, null) || dslProject.ModifiedAfter(RootGMacAst.CreationTime);
        }

        protected override void InitializeRootAst()
        {
            RootAst = new GMacAst();

            RootGMacAst.InitializeAst();
        }

        protected override void InitializeTranslatorContext()
        {
            TranslatorContext = GMacSymbolTranslatorContext.Create(this);

            Context.PushState(RootGMacAst.RootScope);

            Context.CompilationLog.ReportNormal("AST and compilation context Initialized");
        }

        protected override LanguageProjectCodeUnitCompiler InitializeCodeUnitCompiler(ISourceCodeUnit codeUnit, ParseTreeNode rootParseNode)
        {
            return new GMacProjectCodeUnitCompiler(this, codeUnit, rootParseNode);
        }

        public override ParseTreeNode ParseCodeUnit(ISourceCodeUnit codeUnit)
        {
            try
            {
                return GMacSourceParser.ParseAst(codeUnit.CodeText, CompilationLog);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override void FinalizeRootAst()
        {
            RootAst.FinalizeAst();

            Context.CompilationLog.ReportNormal("AST Finalized");

            Context.CompilationLog.ReportNormal("AST Verification Report", RootGMacAst.VerifyAst());
        }
    }
}
