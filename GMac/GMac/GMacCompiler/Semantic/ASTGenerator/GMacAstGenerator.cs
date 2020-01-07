using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.Semantic.Translator;
using CodeComposerLib.Irony.SourceCode;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacCompiler.Syntax;
using Irony.Parsing;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    /// <summary>
    /// The starting point for DSL translation
    /// </summary>
    internal sealed class GMacAstGenerator : GMacAstSymbolGenerator
    {
        #region Static Members

        internal static GMacAst Translate(GMacSymbolTranslatorContext context, bool translateNamespaceFirst = true)
        {
            var translator = new GMacAstGenerator()
            {
                _translateNamespaceFirst = translateNamespaceFirst
            };

            translator.SetContext(context);
            translator.Translate();

            var result = translator._generatedGMacSymbolTable;

            //MasterPool.Release(translator);

            return result;
        }

        #endregion


        private GMacAst _generatedGMacSymbolTable;

        private bool _translateNamespaceFirst = true;


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _generatedGMacSymbolTable = null;
        //}


        private void SetContext(GMacSymbolTranslatorContext context)
        {
            SetContext((SymbolTranslatorContext)context);
            _generatedGMacSymbolTable = context.GMacRootAst;
        }


        private void translate_GMacDSL_Items_List(ParseTreeNode node)
        {
            if (CompilationLog.HasErrors && CompilationLog.StopOnFirstError)
                return;

            var subnode = node.ChildNodes[0];

            if (_translateNamespaceFirst && subnode.Term.ToString() != GMacParseNodeNames.Namespace)
                CompilationLog.RaiseGeneratorError<int>("DSL definitions must start with a namespace", subnode);

            _translateNamespaceFirst = false;

            switch (subnode.Term.ToString())
            {
                case GMacParseNodeNames.Breakpoint:
                    GMacRootAst.EnableBreakpoints = true;
                    break;

                case GMacParseNodeNames.Namespace:
                    GMacNamespaceGenerator.Translate(Context, subnode);
                    break;

                case GMacParseNodeNames.OpenNamespace:
                    //Only namespaces and frames can be used in the 'open' statement
                    Context.OpenScope(
                        (SymbolWithScope)GMacValueAccessGenerator.Translate_Direct(
                            Context, 
                            subnode.ChildNodes[0], 
                            new[] { RoleNames.Namespace, RoleNames.Frame }
                            )
                        );

                    break;

                case GMacParseNodeNames.Frame:
                    GMacFrameGenerator.Translate(Context, subnode);
                    break;

                case GMacParseNodeNames.Constant:
                    GMacConstantGenerator.Translate(Context, subnode);
                    break;

                case GMacParseNodeNames.Structure:
                    GMacStructureGenerator.Translate(Context, subnode);
                    break;

                case GMacParseNodeNames.Transform:
                    GMacTransformGenerator.Translate(Context, subnode);
                    break;

                case GMacParseNodeNames.Macro:
                    GMacMacroGenerator.Translate(Context, subnode);
                    break;

                case GMacParseNodeNames.MacroTemplate:
                    GMacMacroTemplateGenerator.Translate(Context, subnode);
                    break;

                case GMacParseNodeNames.TemplatesImplementation:
                    GMacTemplatesImplementationGenerator.Translate(Context, subnode);
                    break;

                default:
                    CompilationLog.RaiseGeneratorError<int>("DSL definitions item not recognized", subnode);
                    break;
            }
        }

        protected override void Translate()
        {
            //Read the DSL_DefinitionList node
            var gmacDslItemsListNode = RootParseNode.ChildNodes[0];

            //Generate the problem domain items
            foreach (var subnode in gmacDslItemsListNode.ChildNodes)
                translate_GMacDSL_Items_List(subnode);
        }
    }
}
