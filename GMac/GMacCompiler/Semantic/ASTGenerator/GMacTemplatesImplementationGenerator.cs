using System;
using System.Collections.Generic;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;
using Irony.Parsing;
using IronyGrammars.DSLException;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    /// <summary>
    /// Implement each macro given in a list of macro templates inside each frame given in a list of frames
    /// </summary>
    internal sealed class GMacTemplatesImplementationGenerator : GMacAstSymbolGenerator
    {
        #region Static Members

        public static List<GMacMacro> Translate(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            context.PushState(RoleNames.MacroTemplate, node);

            var translator = new GMacTemplatesImplementationGenerator();//new GMacTemplatesImplementationGenerator(context);

            translator.SetContext(context);
            translator.Translate();

            context.PopState();

            var result = translator._generatedMacros;

            //MasterPool.Release(translator);

            return result;
        }

        #endregion


        private readonly List<GMacMacro> _generatedMacros = new List<GMacMacro>();


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _generatedMacros.Clear();
        //}


        private List<GMacMacroTemplate> translate_Templates_List(ParseTreeNode node)
        {
            var templatesList = new List<GMacMacroTemplate>(node.ChildNodes.Count);

            foreach (var subnode in node.ChildNodes)
            {
                try
                {
                    Context.MarkCheckPointState();

                    var template = 
                        (GMacMacroTemplate)GMacValueAccessGenerator.Translate_Direct(Context, subnode, RoleNames.MacroTemplate);

                    templatesList.Add(template);

                    Context.UnmarkCheckPointState();
                }
                catch (CompilerException)
                {
                    Context.RestoreToCheckPointState();
                }
                catch (Exception e)
                {
                    Context.RestoreToCheckPointState();
                    throw (new Exception("Unhandled Exception", e));
                }
            }

            return templatesList;
        }

        private List<GMacFrame> translate_Frames_List(ParseTreeNode node)
        {
            var framesList = new List<GMacFrame>(node.ChildNodes.Count);

            foreach (var subnode in node.ChildNodes)
            {
                try
                {
                    Context.MarkCheckPointState();

                    var frame = 
                        (GMacFrame)GMacValueAccessGenerator.Translate_Direct(Context, subnode, RoleNames.Frame);

                    framesList.Add(frame);

                    Context.UnmarkCheckPointState();
                }
                catch (CompilerException)
                {
                    Context.RestoreToCheckPointState();
                }
                catch (Exception e)
                {
                    Context.RestoreToCheckPointState();
                    throw (new Exception("Unhandled Exception", e));
                }

            }

            return framesList;
        }

        private void Implement_Template(GMacMacroTemplate macroTemplate, GMacFrame frame)
        {
            try
            {
                Context.MarkCheckPointState();

                Context.PushState(frame.ChildSymbolScope);

                var macro = GMacMacroGenerator.Translate(Context, macroTemplate.TemplateParseNode);
                _generatedMacros.Add(macro);

                Context.PopState();

                Context.UnmarkCheckPointState();

            }
            catch (CompilerException)
            {
                Context.RestoreToCheckPointState();
            }
            catch (Exception e)
            {
                Context.RestoreToCheckPointState();
                throw (new Exception("Unhandled Exception", e));
            }
        }

        private void translate_TemplatesImplementation()
        {
            //Read the list of macro templates to be implemented
            var templatesList = translate_Templates_List(RootParseNode.ChildNodes[0]);

            //Read the list of frames that macros will be created inside
            var framesList = translate_Frames_List(RootParseNode.ChildNodes[1]);

            //Implement each macro inside each frame
            foreach (var macroTemplate in templatesList)
                foreach (var frame in framesList)
                    Implement_Template(macroTemplate, frame);
        }

        protected override void Translate()
        {
            translate_TemplatesImplementation();
        }
    }
}
