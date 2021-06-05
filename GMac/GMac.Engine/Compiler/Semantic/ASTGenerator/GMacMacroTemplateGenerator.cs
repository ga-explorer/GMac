using System;
using CodeComposerLib.Irony.DSLException;
using CodeComposerLib.Irony.SourceCode;
using GMac.Engine.Compiler.Semantic.AST;
using GMac.Engine.Compiler.Semantic.ASTConstants;
using Irony.Parsing;

namespace GMac.Engine.Compiler.Semantic.ASTGenerator
{
    /// <summary>
    /// Translate a macro template
    /// </summary>
    internal sealed class GMacMacroTemplateGenerator : GMacAstSymbolGenerator
    {
        #region Static Members

        public static GMacMacroTemplate Translate(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            context.PushState(RoleNames.MacroTemplate, node);

            var translator = new GMacMacroTemplateGenerator();//new GMacMacroTemplateGenerator(context);

            translator.SetContext(context);
            translator.Translate();

            context.PopState();

            var result = translator._generatedMacroTemplate;

            //MasterPool.Release(translator);

            return result;
        }

        #endregion


        private GMacMacroTemplate _generatedMacroTemplate;


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _generatedMacroTemplate = null;
        //}


        private void translate_MacroTemplate()
        {
            try
            {
                Context.MarkCheckPointState();

                var nodeMacro = RootParseNode.ChildNodes[0];

                //Read the name of the new macro template
                var qualList = GenUtils.Translate_Qualified_Identifier(nodeMacro.ChildNodes[0]);

                if (qualList.ActiveLength > 1)
                    CompilationLog.RaiseGeneratorError<int>("Template name cannot be a qualified name", RootParseNode.ChildNodes[0]);

                var templateName = qualList.FirstItem;

                if (Context.ParentNamespace.CanDefineChildSymbol(templateName) == false)
                    CompilationLog.RaiseGeneratorError<int>("Symbol name already used", RootParseNode.ChildNodes[0]);

                _generatedMacroTemplate =
                    Context.ParentNamespace.DefineMacroTemplate(templateName, nodeMacro);

                _generatedMacroTemplate.CodeLocation = Context.GetCodeLocation(RootParseNode);

                Context.UnmarkCheckPointState();
            }
            catch (CompilerException)
            {
                Context.RestoreToCheckPointState();
            }
            catch (Exception e)
            {
                Context.RestoreToCheckPointState();
                throw new Exception("Unhandled Exception", e);
            }
        }

        protected override void Translate()
        {
            translate_MacroTemplate();
        }
    }
}
