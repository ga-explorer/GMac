using System;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacCompiler.Semantic.ASTInterpreter.Evaluator;
using Irony.Parsing;
using IronyGrammars.DSLException;
using IronyGrammars.Semantic.Symbol;
using IronyGrammars.SourceCode;
using TextComposerLib.Logs.Progress;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    /// <summary>
    /// Translate a GMac frame or namespace constant
    /// </summary>
    internal sealed class GMacConstantGenerator : GMacAstSymbolGenerator
    {
        #region Static Members

        public static GMacConstant Translate(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            context.PushState(RoleNames.Constant, node);

            var translator = new GMacConstantGenerator();//new GMacConstantGenerator(context);

            translator.SetContext(context);
            translator.Translate();

            context.PopState();

            var result = translator._generatedConstant;

            //MasterPool.Release(translator);

            return result;
        }

        #endregion


        private GMacConstant _generatedConstant;


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _generatedConstant = null;
        //}


        /// <summary>
        /// Generate a constant defined inside a namespace
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="constantName"></param>
        /// <returns></returns>
        private GMacConstant Create_Namespace_Constant(GMacNamespace nameSpace, string constantName)
        {
            if (GMacCompilerFeatures.CanDefineNamespaceConstants == false)
                CompilationLog.RaiseGeneratorError<int>("Can't define a constant inside a namespace", RootParseNode);

            Context.PushState(nameSpace.ChildSymbolScope);

            if (nameSpace.CanDefineChildSymbol(constantName) == false)
                CompilationLog.RaiseGeneratorError<int>("Symbol name already used", RootParseNode);

            var constantExpr = GMacExpressionGenerator.Translate(Context, RootParseNode.ChildNodes[1]);

            var constantValue = GMacExpressionEvaluator.EvaluateExpression(Context.ActiveParentScope, constantExpr);

            Context.PopState();

            return nameSpace.DefineNamespaceConstant(constantName, constantValue);
        }

        /// <summary>
        /// Generate a constant defined inside a frame
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="constantName"></param>
        /// <returns></returns>
        private GMacConstant Create_Frame_Constant(GMacFrame frame, string constantName)
        {
            Context.PushState(frame.ChildSymbolScope);

            if (frame.CanDefineChildSymbol(constantName) == false)
                CompilationLog.RaiseGeneratorError<int>("Symbol name already used", RootParseNode);

            var constantExpr = GMacExpressionGenerator.Translate(Context, RootParseNode.ChildNodes[1]);

            var constantValue = GMacExpressionEvaluator.EvaluateExpression(Context.ActiveParentScope, constantExpr);

            Context.PopState();

            return frame.DefineFrameConstant(constantName, constantValue);
        }


        private void translate_Constant()
        {
            try
            {
                Context.MarkCheckPointState();

                string childSymbolName;
                SymbolWithScope parentSymbol;

                Translate_ParentSymbolAndChildSymbolName(RootParseNode.ChildNodes[0], out parentSymbol, out childSymbolName);

                var nameSpace = parentSymbol as GMacNamespace;

                if (nameSpace != null)
                    _generatedConstant = Create_Namespace_Constant(nameSpace, childSymbolName);

                else
                {
                    var frame = parentSymbol as GMacFrame;

                    if (frame != null)
                        _generatedConstant = Create_Frame_Constant(frame, childSymbolName);

                    else
                        CompilationLog.RaiseGeneratorError<int>("Expecting a Frame or Namespace scope", RootParseNode.ChildNodes[0]);
                }

                _generatedConstant.CodeLocation = Context.GetCodeLocation(RootParseNode);

                Context.UnmarkCheckPointState();
                Context.CompilationLog.ReportNormal("Translated Constant: " + _generatedConstant.SymbolAccessName, ProgressEventArgsResult.Success);
            }
            catch (CompilerException)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportNormal("Translate Constant", ProgressEventArgsResult.Failure);
            }
            catch (Exception e)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportError("Translate Constant Failed With Error", e);
            }
        }

        protected override void Translate()
        {
            var progressId = Context.CompilationLog.ReportStart("Translate Constant");

            translate_Constant();

            Context.CompilationLog.ReportFinish(progressId);
        }
    }
}
