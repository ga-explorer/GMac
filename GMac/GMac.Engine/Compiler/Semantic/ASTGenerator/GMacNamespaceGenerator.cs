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
    /// Generate the definition of a new GMac namespace or set the active GMac name space
    /// </summary>
    internal sealed class GMacNamespaceGenerator : GMacAstSymbolGenerator
    {
        #region Static Members

        /// <summary>
        /// Translate the given parse tree node into a GMac namespace
        /// </summary>
        /// <param name="context"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static GMacNamespace Translate(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            context.PushState(RoleNames.Namespace, node);

            var translator = new GMacNamespaceGenerator();//new GMacNamespaceGenerator(context);

            translator.SetContext(context); 
            translator.Translate();

            context.PopState();

            //Clear all opened scopes
            context.ClearOpenedScopes();

            //After translation is completed change the current scope to the translated namespace to make it the current namespace
            context.SetActiveState(translator._generatedNamespace.ChildSymbolScope, RoleNames.Namespace, node);

            var result = translator._generatedNamespace;

            //MasterPool.Release(translator);

            return result;
        }

        #endregion


        private GMacNamespace _generatedNamespace;


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _generatedNamespace = null;
        //}


        private void translate_Namespace()
        {
            try
            {
                Context.MarkCheckPointState();

                //Read the namespace name: for example main.conformal.cga5d
                var qualList = GenUtils.Translate_Qualified_Identifier(RootParseNode.ChildNodes[0]);

                //Find the root namespace inside the root global scope of the GMacDSL (search for a namespace called 'main')

                if (GMacRootAst.LookupRootNamespace(qualList.FirstItem, out var nameSpace) == false)
                {
                    if (GMacRootAst.RootScope.SymbolExists(qualList.FirstItem))
                        CompilationLog.RaiseGeneratorError<int>("Namespace name already used", RootParseNode.ChildNodes[0]);

                    nameSpace = GMacRootAst.DefineRootNamespace(qualList.FirstItem);
                }

                //Starting from the created\found root namespace, repeat the previous operation for each child namespace in qual_list
                for (var i = 1; i < qualList.ActiveLength; i++)
                {
                    if (nameSpace.LookupNamespace(qualList[i], out var childNamespace))
                        nameSpace = childNamespace;

                    else
                    {
                        if (nameSpace.CanDefineChildSymbol(qualList[i]) == false)
                            CompilationLog.RaiseGeneratorError<int>("Symbol with same name already exists", RootParseNode.ChildNodes[0]);

                        nameSpace = nameSpace.DefineNamespace(qualList[i]);
                    }
                }

                _generatedNamespace = nameSpace;

                _generatedNamespace.AddCodeLocation(Context.GetCodeLocation(RootParseNode));

                Context.UnmarkCheckPointState();

                Context.CompilationLog.ReportNormal("Translated Namespace: " + _generatedNamespace.SymbolAccessName, ProgressEventArgsResult.Success);
            }
            catch (CompilerException)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportNormal("Translate Namespace Failed", ProgressEventArgsResult.Failure);
            }
            catch (Exception e)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportError("Translate Namespace Failed With Error", e);
            }
        }

        protected override void Translate()
        {
            var progressId = Context.CompilationLog.ReportStart("Translate Namespace");

            translate_Namespace();

            Context.CompilationLog.ReportFinish(progressId);
        }
    }
}
