using CodeComposerLib.Irony.Semantic;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.Semantic.Translator;
using CodeComposerLib.Irony.SourceCode;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GMac.GMacCompiler.Semantic.AST;
using Irony.Parsing;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    /// <summary>
    /// The base class for all GMac symbol translators
    /// </summary>
    internal abstract class GMacAstSymbolGenerator : SymbolTranslator
    {
        public GMacSymbolTranslatorContext Context => (GMacSymbolTranslatorContext)TranslatorContext;

        public GMacAst GMacRootAst => (GMacAst)ParentDsl;

        public MathematicaInterface Cas => GaSymbolicsUtils.Cas;

        public MathematicaEvaluator CasEval => GaSymbolicsUtils.Cas.Evaluator;


        //protected void SetContext(GMacSymbolTranslatorContext context)
        //{
        //    this.SetContext((SymbolTranslatorContext)context);
        //}

        /// <summary>
        /// Take a qualified indetifier node and separate it into a parent symbol and a child symbol name
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parentSymbol"></param>
        /// <param name="childSymbolName"></param>
        protected bool Translate_ParentSymbolAndChildSymbolName(ParseTreeNode node, out SymbolWithScope parentSymbol, out string childSymbolName)
        {
            var qualList = GenUtils.Translate_Qualified_Identifier(node);

            childSymbolName = qualList.LastItem;

            if (qualList.ActiveLength == 1)
                parentSymbol = Context.ActiveParentSymbol;

            else
            {
                qualList.DecreaseActiveEndOffset();

                parentSymbol = GMacValueAccessGenerator.Translate_Direct(Context, node, qualList) as SymbolWithScope;
            }

            return ReferenceEquals(parentSymbol, null) == false;
        }

        /// <summary>
        /// For the given parent symbol this method extracts an identifier from the given node and
        /// returns it if it isn't already used for a child symbol of the parent symbol's scope. 
        /// Else it raises a generator exception
        /// </summary>
        /// <param name="parentObject"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected string TranslateChildSymbolName(IIronyAstObjectWithScope parentObject, ParseTreeNode node)
        {
            //Read the name of the new child symbol
            var childSymbolName = GenUtils.Translate_Identifier(node);

            //Make sure the child symbol name is not used inside the parent scope
            if (parentObject.ChildScope.SymbolExists(childSymbolName))
                CompilationLog.RaiseGeneratorError<int>("Child symbol name already used", node);

            return childSymbolName;
        }

        /// <summary>
        /// For the given parent scope this method extracts an identifier from the given node and
        /// returns it if it isn't already used for a child symbol of the scope. 
        /// Else it raises a generator exception
        /// </summary>
        /// <param name="parentScope"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected string TranslateChildSymbolName(LanguageScope parentScope, ParseTreeNode node)
        {
            //Read the name of the new child symbol
            var childSymbolName = GenUtils.Translate_Identifier(node);

            //Make sure the child symbol name is not used inside the parent scope
            if (parentScope.SymbolExists(childSymbolName))
                CompilationLog.RaiseGeneratorError<int>("Child symbol name already used", node);

            return childSymbolName;
        }

        /// <summary>
        /// For the current active parent scope this method extracts an identifier from the given node and
        /// returns it if it isn't already used for a child symbol of the scope. 
        /// Else it raises a generator exception
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected string TranslateChildSymbolName(ParseTreeNode node)
        {
            //Read the name of the new child symbol
            var childSymbolName = GenUtils.Translate_Identifier(node);

            //Make sure the child symbol name is not used inside the parent scope
            if (TranslatorContext.ActiveParentScope.SymbolExists(childSymbolName))
                CompilationLog.RaiseGeneratorError<int>("Child symbol name already used", node);

            return childSymbolName;
        }
    }
}
