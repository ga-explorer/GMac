using System;
using CodeComposerLib.Irony.DSLException;
using CodeComposerLib.Irony.SourceCode;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacCompiler.Syntax;
using Irony.Parsing;
using TextComposerLib.Logs.Progress;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    /// <summary>
    /// Translate a GMac macro definition with its parameters, return type, and body command block
    /// </summary>
    internal sealed class GMacMacroGenerator : GMacAstSymbolGenerator
    {
        #region Static Members

        public static GMacMacro Translate(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            //var eventId = context.CompilationLog.TimeCounter.StartEvent("macro gen");

            context.PushState(RoleNames.Macro, node);

            var translator = new GMacMacroGenerator();//new GMacMacroGenerator(context);

            translator.SetContext(context);
            translator.Translate();

            context.PopState();

            var result = translator._generatedMacro;

            //MasterPool.Release(translator);

            //context.CompilationLog.TimeCounter.EndEvent(eventId);

            return result;
        }

        #endregion


        private GMacMacro _generatedMacro;


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _generatedMacro = null;
        //}


        private void translate_Identifier_Declaration(ParseTreeNode node)
        {
            try
            {
                Context.MarkCheckPointState();

                //Read the name of the parameter
                var identifierName = TranslateChildSymbolName(_generatedMacro, node.ChildNodes[0]);

                //Read the type of the parameter
                var identifierType = GMacValueAccessGenerator.Translate_Direct_LanguageType(Context, node.ChildNodes[1]);

                //Define the macro parameter in the symbol table
                _generatedMacro.DefineInputParameter(identifierName, identifierType);

                Context.UnmarkCheckPointState();
                Context.CompilationLog.ReportNormal("Translated Macro Parameter: " + identifierName, ProgressEventArgsResult.Success);
            }
            catch (CompilerException)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportNormal("Translate Macro Parameter Failed: " + _generatedMacro.SymbolAccessName, ProgressEventArgsResult.Failure);
            }
            catch (Exception e)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportError("Translate Macro Parameter Failed: " + _generatedMacro.SymbolAccessName, e);
            }
        }

        //Translate the list of macro parameters
        private void translate_Macro_Inputs(ParseTreeNode node)
        {
            node.Assert(GMacParseNodeNames.MacroInputs);

            foreach (var subnode in node.ChildNodes)
                translate_Identifier_Declaration(subnode);
        }

        private void translate_Macro_OutputType(ParseTreeNode node)
        {
            try
            {
                Context.MarkCheckPointState();

                var macroOutputType = GMacValueAccessGenerator.Translate_Direct_LanguageType(Context, node);

                _generatedMacro.DefineOutputParameter(GeneralConstants.MacroOutputParameterName, macroOutputType);

                Context.UnmarkCheckPointState();
                Context.CompilationLog.ReportNormal("Translated Macro Output Type: " + _generatedMacro.SymbolAccessName, ProgressEventArgsResult.Success);
            }
            catch (CompilerException)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportNormal("Translate Macro Output Type: " + _generatedMacro.SymbolAccessName, ProgressEventArgsResult.Failure);
            }
            catch (Exception e)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportError("Translate Macro Output Type Failed: " + _generatedMacro.SymbolAccessName, e);
            }
        }

        private GMacMacro Create_Namespace_Macro(GMacNamespace nameSpace, string macroName)
        {
            if (GMacCompilerFeatures.CanDefineNamespaceMacros == false)
                CompilationLog.RaiseGeneratorError<int>("Can't define a macro inside a namespace", RootParseNode);

            if (nameSpace.CanDefineChildSymbol(macroName) == false)
                CompilationLog.RaiseGeneratorError<int>("Symbol name already used", RootParseNode);

            //Create the macro and add it to the symbol table
            return nameSpace.DefineNamespaceMacro(macroName);
        }

        private GMacMacro Create_Structure_Macro(GMacStructure structure, string macroName)
        {
            if (structure.CanDefineChildSymbol(macroName) == false)
                CompilationLog.RaiseGeneratorError<int>("Symbol name already used", RootParseNode);

            //Create the macro and add it to the symbol table
            return structure.DefineStructureMacro(macroName);

            //TODO: add the 'this' parameter after the result parameter for structure macros
        }

        private GMacMacro Create_Frame_Macro(GMacFrame frame, string macroName)
        {
            if (frame.CanDefineChildSymbol(macroName) == false)
                CompilationLog.RaiseGeneratorError<int>("Symbol name already used", RootParseNode);

            //Create the macro and add it to the symbol table
            return frame.DefineFrameMacro(macroName);
        }

        private void translate_Macro()
        {
            try
            {
                Context.MarkCheckPointState();

                Translate_ParentSymbolAndChildSymbolName(RootParseNode.ChildNodes[0], out var parentSymbol, out var childSymbolName);

                //Determine type of macro (namespace, frame, or structure) and create the macro
                var nameSpace = parentSymbol as GMacNamespace;

                if (nameSpace != null)
                    _generatedMacro = Create_Namespace_Macro(nameSpace, childSymbolName);

                else
                {
                    var structure = parentSymbol as GMacStructure;

                    if (structure != null)
                        _generatedMacro = Create_Structure_Macro(structure, childSymbolName);

                    else
                    {
                        var frame = parentSymbol as GMacFrame;

                        if (frame != null)
                            _generatedMacro = Create_Frame_Macro(frame, childSymbolName);

                        else
                            CompilationLog.RaiseGeneratorError<int>("Expecting a Structure, Frame, or Namespace scope", RootParseNode.ChildNodes[0]);
                    }
                }

                _generatedMacro.CodeLocation = Context.GetCodeLocation(RootParseNode);

                Context.UnmarkCheckPointState();
                Context.CompilationLog.ReportNormal("Translated Macro: " + _generatedMacro.SymbolAccessName, ProgressEventArgsResult.Success);
            }
            catch (CompilerException)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportNormal("Translate Macro Failed", ProgressEventArgsResult.Failure);
            }
            catch (Exception e)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportError("Translate Macro Failed With Error", e);
            }
        }

        private void translate_Macro_Body_CommandBlock(ParseTreeNode node)
        {
            try
            {
                Context.MarkCheckPointState();

                _generatedMacro.SymbolBody = GMacCommandBlockGenerator.Translate(Context, node);

                Context.UnmarkCheckPointState();
                Context.CompilationLog.ReportNormal("Translated Macro Body: " + _generatedMacro.SymbolAccessName, ProgressEventArgsResult.Success);
            }
            catch (CompilerException)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportNormal("Translate Macro Body: " + _generatedMacro.SymbolAccessName, ProgressEventArgsResult.Failure);
            }
            catch (Exception e)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportError("Translate Macro Body Failed With Error: " + _generatedMacro.SymbolAccessName, e);
            }
        }

        protected override void Translate()
        {
            var progressId = Context.CompilationLog.ReportStart("Translate Macro");

            translate_Macro();

            if (_generatedMacro == null)
            {
                Context.CompilationLog.ReportFinish(progressId);
                return;
            }

            //Translate macro return type
            Context.PushState(_generatedMacro.ChildSymbolScope, RoleNames.MacroParameter, RootParseNode.ChildNodes[2]);

            translate_Macro_OutputType(RootParseNode.ChildNodes[2]);

            //Translate macro input parameters
            Context.SetActiveState(_generatedMacro.ChildSymbolScope, RoleNames.MacroParameter, RootParseNode.ChildNodes[1]);

            translate_Macro_Inputs(RootParseNode.ChildNodes[1]);

            Context.PopState();

            //Translate macro body expression block
            Context.PushState(_generatedMacro.ChildSymbolScope);

            translate_Macro_Body_CommandBlock(RootParseNode.ChildNodes[3]);

            Context.PopState();

            Context.CompilationLog.ReportFinish(progressId);
        }
    }
}
