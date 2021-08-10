using System;
using CodeComposerLib.Irony.DSLException;
using CodeComposerLib.Irony.SourceCode;
using GMac.Engine.Compiler.Semantic.AST;
using GMac.Engine.Compiler.Semantic.ASTConstants;
using Irony.Parsing;
using TextComposerLib.Loggers.Progress;

namespace GMac.Engine.Compiler.Semantic.ASTGenerator
{
    /// <summary>
    /// Translate a GMac structure and its members
    /// </summary>
    internal sealed class GMacStructureGenerator : GMacAstSymbolGenerator
    {
        #region Static Members

        public static GMacStructure Translate(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            context.PushState(RoleNames.Structure, node);

            var translator = new GMacStructureGenerator();//new GMacStructureGenerator(context);

            translator.SetContext(context);
            translator.Translate();

            context.PopState();

            var result = translator._generatedStructure;

            //MasterPool.Release(translator);

            return result;
        }

        #endregion


        private GMacStructure _generatedStructure;


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _generatedStructure = null;
        //}


        private void translate_Identifier_Declaration(ParseTreeNode node)
        {
            try
            {
                Context.MarkCheckPointState();

                //Read the name of the member
                var identifierName = TranslateChildSymbolName(_generatedStructure, node.ChildNodes[0]);

                //Read the type of the member
                var identifierType = GMacValueAccessGenerator.Translate_Direct_LanguageType(Context, node.ChildNodes[1]);

                //Create the member in the symbol table
                _generatedStructure.DefineReadWriteDataMember(identifierName, identifierType);

                Context.UnmarkCheckPointState();
                Context.CompilationLog.ReportNormal("Translated Structure Member: " + identifierName, ProgressEventArgsResult.Success);
            }
            catch (CompilerException)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportNormal("Translate Structure Member Failed: " + _generatedStructure.SymbolAccessName, ProgressEventArgsResult.Failure);
            }
            catch (Exception e)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportError("Translate Structure Member Failed:" + _generatedStructure.SymbolAccessName, e);
            }
        }

        private void translate_Structure_Members(ParseTreeNode node)
        {
            Context.PushState(_generatedStructure.ChildSymbolScope, node);

            foreach (var subnode in node.ChildNodes)
                translate_Identifier_Declaration(subnode);

            Context.PopState();
        }


        private GMacStructure Create_Namespace_Structure(GMacNamespace nameSpace, string structureName)
        {
            if (GMacCompilerFeatures.CanDefineNamespaceStructures == false)
                CompilationLog.RaiseGeneratorError<int>("Can't define a structure inside a namespace", RootParseNode);

            Context.PushState(nameSpace.ChildSymbolScope);

            if (nameSpace.CanDefineChildSymbol(structureName) == false)
                CompilationLog.RaiseGeneratorError<int>("Symbol name already used", RootParseNode);

            Context.PopState();

            return nameSpace.DefineNamespaceStructure(structureName);
        }

        private GMacStructure Create_Frame_Structure(GMacFrame frame, string structureName)
        {
            if (GMacCompilerFeatures.CanDefineFrameStructures == false)
                CompilationLog.RaiseGeneratorError<int>("Can't define a structure inside a frame", RootParseNode);

            Context.PushState(frame.ChildSymbolScope);

            if (frame.CanDefineChildSymbol(structureName) == false)
                CompilationLog.RaiseGeneratorError<int>("Symbol name already used", RootParseNode);

            Context.PopState();

            return frame.DefineFrameStructure(structureName);
        }


        private void translate_Structure()
        {
            try
            {
                Context.MarkCheckPointState();

                Translate_ParentSymbolAndChildSymbolName(RootParseNode.ChildNodes[0], out var parentSymbol, out var childSymbolName);

                var nameSpace = parentSymbol as GMacNamespace;

                if (nameSpace != null)
                    _generatedStructure = Create_Namespace_Structure(nameSpace, childSymbolName);

                else
                {
                    var frame = parentSymbol as GMacFrame;

                    if (frame != null)
                        _generatedStructure = Create_Frame_Structure(frame, childSymbolName);

                    else
                        CompilationLog.RaiseGeneratorError<int>("Expecting a Frame or Namespace scope", RootParseNode.ChildNodes[0]);
                }

                _generatedStructure.CodeLocation = Context.GetCodeLocation(RootParseNode);

                Context.UnmarkCheckPointState();
                Context.CompilationLog.ReportNormal("Translated Structure: " + _generatedStructure.SymbolAccessName, ProgressEventArgsResult.Success);
            }
            catch (CompilerException)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportNormal("Translate Structure", ProgressEventArgsResult.Failure);
            }
            catch (Exception e)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportError("Translate Structure Failed With Error", e);
            }
        }

        //private void translate_Structure()
        //{
        //    try
        //    {
        //        Context.MarkCheckPointState();

        //        //Read name of structur
        //        var structName = TranslateChildSymbolName(RootParseNode.ChildNodes[0]);

        //        //Create structure and add it to symbol table
        //        _generatedStructure = Context.ParentNamespace.DefineNamespaceStructure(structName);

        //        _generatedStructure.ParseNode = RootParseNode;

        //        Context.UnmarkCheckPointState();

        //    }
        //    catch (CompilerException)
        //    {
        //        Context.RestoreToCheckPointState();
        //    }
        //    catch (Exception e)
        //    {
        //        Context.RestoreToCheckPointState();
        //        throw (new Exception("Unhandled Exception", e));
        //    }
        //}

        protected override void Translate()
        {
            var progressId = Context.CompilationLog.ReportStart("Translate Structure");

            translate_Structure();

            if (_generatedStructure != null)
                translate_Structure_Members(RootParseNode.ChildNodes[1]);

            Context.CompilationLog.ReportFinish(progressId);
        }
    }
}
