using System;
using CodeComposerLib.Irony.DSLException;
using CodeComposerLib.Irony.SourceCode;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;
using Irony.Parsing;
using TextComposerLib.Logs.Progress;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    internal sealed class GMacTransformGenerator : GMacAstSymbolGenerator
    {
        public static GMacMultivectorTransform Translate(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            context.PushState(RoleNames.Transform, node);

            var translator = new GMacTransformGenerator();//new GMacTransformGenerator(context);

            translator.SetContext(context);
            translator.Translate();

            context.PopState();

            var result = translator._generatedTransform;

            //MasterPool.Release(translator);

            return result;
        }


        private GMacMultivectorTransform _generatedTransform;


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _generatedTransform = null;
        //}

        private GMacMultivectorTransform Create_Namespace_Transform(GMacNamespace nameSpace, string constantName)
        {
            //TODO: Complete this
            return null;
        }

        private GMacMultivectorTransform Create_Frame_Transform(GMacFrame frame, string constantName)
        {
            //TODO: Complete this
            return null;
        }

        private void translate_Transform()
        {
            try
            {
                Context.MarkCheckPointState();

                Translate_ParentSymbolAndChildSymbolName(RootParseNode.ChildNodes[0], out var parentSymbol, out var childSymbolName);

                var nameSpace = parentSymbol as GMacNamespace;

                if (nameSpace != null)
                    _generatedTransform = Create_Namespace_Transform(nameSpace, childSymbolName);

                else
                {
                    var frame = parentSymbol as GMacFrame;

                    if (frame != null)
                        _generatedTransform = Create_Frame_Transform(frame, childSymbolName);

                    else
                        CompilationLog.RaiseGeneratorError<int>("Expecting a Frame or Namespace scope", RootParseNode.ChildNodes[0]);
                }

                _generatedTransform.CodeLocation = Context.GetCodeLocation(RootParseNode);

                Context.UnmarkCheckPointState();
                Context.CompilationLog.ReportNormal("Translated Transform: " + _generatedTransform.SymbolAccessName, ProgressEventArgsResult.Success);
            }
            catch (CompilerException)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportNormal("Translate Transform", ProgressEventArgsResult.Failure);
            }
            catch (Exception e)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportError("Translate Transform Failed With Error", e);
            }
        }

        protected override void Translate()
        {
            var progressId = Context.CompilationLog.ReportStart("Translate Transform");

            translate_Transform();

            Context.CompilationLog.ReportFinish(progressId);
        }
    }
}
