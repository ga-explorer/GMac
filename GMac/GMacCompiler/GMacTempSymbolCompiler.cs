using System;
using System.Collections.Generic;
using System.Linq;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.ASTGenerator;
using GMac.GMacCompiler.Syntax;
using IronyGrammars.Semantic.Symbol;
using TextComposerLib.Logs.Progress;
using TextComposerLib.Text;

namespace GMac.GMacCompiler
{
    /// <summary>
    /// Provides services for compiling temp AST symbols that can be removed later from the AST like
    /// macros and structures
    /// </summary>
    public sealed class GMacTempSymbolCompiler : GMacTempCodeCompiler
    {
        public override string ProgressSourceId => "GMac Temp Symbols Compiler";

        public Dictionary<string, AstStructure> CompiledStructures { get; }

        public Dictionary<string, AstMacro> CompiledMacros { get; }

        public GMacRefResContextInfo RefResContext { get; }

        public IEnumerable<AstSymbol> CompiledSymbols
        {
            get
            {
                return 
                    CompiledStructures
                    .Select(pair => pair.Value as AstSymbol)
                    .Concat(
                        CompiledMacros.Select(pair => pair.Value)
                    );
            }
        }


        public GMacTempSymbolCompiler()
        {
            CompiledMacros = new Dictionary<string, AstMacro>();
            CompiledStructures = new Dictionary<string, AstStructure>();
            RefResContext = new GMacRefResContextInfo();
        }


        public void RemoveCompiledSymbolsFromAst()
        {
            var textLogItem = 
                CompiledSymbols
                .Select(s => s.AccessName)
                .Concatenate(Environment.NewLine, "", "", "    ", "");

            var progressId = this.ReportStart("Removing Compiled Symbols", textLogItem);

            foreach (var pair in CompiledMacros)
                ((SymbolWithScope)pair.Value.AssociatedMacro.ParentLanguageSymbol)
                    .RemoveChildSymbol(pair.Value.Name);

            CompiledMacros.Clear();

            foreach (var pair in CompiledStructures)
                ((SymbolWithScope)pair.Value.AssociatedStructure.ParentLanguageSymbol)
                    .RemoveChildSymbol(pair.Value.Name);

            CompiledStructures.Clear();

            this.ReportFinish(progressId, textLogItem);
        }


        public AstStructure CompileStructure(string codeText)
        {
            if (InitializeCompiler(codeText, GMacSourceParser.ParseStructure, RefResContext) == false)
            {
                this.ReportNormal(
                    "Initializing Compile Temp Structure", 
                    codeText, 
                    ProgressEventArgsResult.Failure
                    );

                return null;
            }

            this.ReportNormal(
                "Initializing Compile Temp Structure", 
                codeText, 
                ProgressEventArgsResult.Success
                );

            try
            {
                var compiledSymbol = 
                    new AstStructure(GMacStructureGenerator.Translate(Context, RootParseNode));

                CompiledStructures.Add(compiledSymbol.AccessName, compiledSymbol);

                this.ReportNormal(
                    "Compiling Temp Structure", 
                    compiledSymbol.AccessName, 
                    ProgressEventArgsResult.Success
                    );

                return compiledSymbol;
            }
            catch (Exception e)
            {
                this.ReportError("Error Compiling Temp Structure", e);

                return null;
            }
        }

        public AstMacro CompileMacro(string codeText)
        {
            if (InitializeCompiler(codeText, GMacSourceParser.ParseMacro, RefResContext) == false)
            {
                this.ReportNormal(
                    "Initializing Compile Temp Macro", 
                    codeText, 
                    ProgressEventArgsResult.Failure
                    );

                return null;
            }

            this.ReportNormal(
                "Initializing Compile Temp Macro", 
                codeText, 
                ProgressEventArgsResult.Success
                );

            try
            {
                var compiledSymbol = 
                    new AstMacro(GMacMacroGenerator.Translate(Context, RootParseNode));

                CompiledMacros.Add(compiledSymbol.AccessName, compiledSymbol);

                this.ReportNormal(
                    "Compiling Temp Macro", 
                    compiledSymbol.AccessName, 
                    ProgressEventArgsResult.Success
                    );

                return compiledSymbol;
            }
            catch (Exception e)
            {
                this.ReportError("Error Compiling Temp Macro", e); 
                
                return null;
            }
        }
    }
}
