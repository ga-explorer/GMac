using System;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Symbol;
using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler.Semantic.ASTGenerator;
using GMac.Engine.Compiler.Syntax;
using TextComposerLib.Loggers.Progress;
using TextComposerLib.Text;

namespace GMac.Engine.Compiler
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

        public GMacScopeResolutionContext ScopeResolutionContext { get; }

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
            ScopeResolutionContext = new GMacScopeResolutionContext();
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
            if (InitializeCompiler(codeText, GMacSourceParser.ParseStructure, ScopeResolutionContext) == false)
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
            if (InitializeCompiler(codeText, GMacSourceParser.ParseMacro, ScopeResolutionContext) == false)
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
