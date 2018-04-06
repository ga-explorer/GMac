using System.Collections.Generic;
using System.Linq;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacMath.Symbolic.Frames;
using GMac.GMacMath.Symbolic.Maps.Unilinear;
using Irony.Parsing;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Scope;
using IronyGrammars.Semantic.Symbol;

namespace GMac.GMacCompiler.Semantic.AST
{
    /// <summary>
    /// This class represents a GMac namespace language symbol to hold other language symbols thus creating a heirarchy of GMac code structures
    /// </summary>
    public sealed class GMacNamespace : SymbolNamespace
    {
        internal GMacAst GMacRootAst => (GMacAst)RootAst;

        /// <summary>
        ///All child namespaces
        /// </summary>
        internal IEnumerable<GMacNamespace> ChildNamespaces { get; private set; }

        /// <summary>
        ///All child frames
        /// </summary>
        internal IEnumerable<GMacFrame> ChildFrames { get; private set; }

        /// <summary>
        ///All child constants
        /// </summary>
        internal IEnumerable<GMacConstant> ChildConstants { get; private set; }

        /// <summary>
        ///All child macros
        /// </summary>
        internal IEnumerable<GMacMacro> ChildMacros { get; private set; }

        /// <summary>
        ///All child macro templates
        /// </summary>
        internal IEnumerable<GMacMacroTemplate> ChildMacroTemplates { get; private set; }

        /// <summary>
        ///All child transforms
        /// </summary>
        internal IEnumerable<GMacMultivectorTransform> ChildTransforms { get; private set; }

        /// <summary>
        ///All child structures
        /// </summary>
        internal IEnumerable<GMacStructure> ChildStructures { get; private set; }

        /// <summary>
        /// The symbols cache used to speed access to sub-symbols
        /// </summary>
        internal GMacAstSymbolsCache SymbolsCache { get; private set; }


        internal GMacNamespace(string namespaceName, LanguageScope parentScope)
            : base(namespaceName, parentScope)
        {
            ChildNamespaces = 
                ChildSymbolScope.Symbols(RoleNames.Namespace).Cast<GMacNamespace>();

            ChildFrames = 
                ChildSymbolScope.Symbols(RoleNames.Frame).Cast<GMacFrame>();

            ChildConstants = 
                ChildSymbolScope.Symbols(RoleNames.Constant).Cast<GMacConstant>();

            ChildMacros = 
                ChildSymbolScope.Symbols(RoleNames.Macro).Cast<GMacMacro>();

            ChildMacroTemplates = 
                ChildSymbolScope.Symbols(RoleNames.MacroTemplate).Cast<GMacMacroTemplate>();

            ChildTransforms = 
                ChildSymbolScope.Symbols(RoleNames.Transform).Cast<GMacMultivectorTransform>();

            ChildStructures = 
                ChildSymbolScope.Symbols(RoleNames.Structure).Cast<GMacStructure>();
        }


        /// <summary>
        /// Create a child namespace
        /// </summary>
        /// <param name="namespaceName"></param>
        /// <returns></returns>
        internal GMacNamespace DefineNamespace(string namespaceName)
        {
            return new GMacNamespace(namespaceName, ChildSymbolScope);
        }

        /// <summary>
        /// Create a child frame
        /// </summary>
        /// <param name="frameName"></param>
        /// <param name="basisVectorsNames"></param>
        /// <param name="attachedSymbolicFrame"></param>
        /// <returns></returns>
        internal GMacFrame DefineFrame(string frameName, string[] basisVectorsNames, GaSymFrame attachedSymbolicFrame)
        {
            var newFrame = new GMacFrame(frameName, ChildSymbolScope, attachedSymbolicFrame);

            GMacRootAst.FramesCount++;

            newFrame.DefineFrameMultivector();

            newFrame.DefineBasisVectors(basisVectorsNames);

            return newFrame;
        }

        /// <summary>
        /// Create a child macro template
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="templateParseNode"></param>
        /// <returns></returns>
        internal GMacMacroTemplate DefineMacroTemplate(string templateName, ParseTreeNode templateParseNode)
        {
            return new GMacMacroTemplate(templateName, ChildSymbolScope, templateParseNode);
        }

        /// <summary>
        /// Create a child macro
        /// </summary>
        /// <param name="macroName"></param>
        /// <returns></returns>
        internal GMacMacro DefineNamespaceMacro(string macroName)
        {
            return new GMacMacro(macroName, ChildSymbolScope);
        }

        /// <summary>
        /// Create a child transform
        /// </summary>
        /// <param name="transformName"></param>
        /// <param name="sourceFrame"></param>
        /// <param name="targetFrame"></param>
        /// <param name="baseTransform"></param>
        /// <returns></returns>
        internal GMacMultivectorTransform DefineTransform(string transformName, GMacFrame sourceFrame, GMacFrame targetFrame, GaSymMapUnilinear baseTransform)
        {
            return new GMacMultivectorTransform(transformName, ChildSymbolScope, sourceFrame, targetFrame, baseTransform);
        }

        /// <summary>
        /// Create a child structure
        /// </summary>
        /// <param name="structureName"></param>
        /// <returns></returns>
        internal GMacStructure DefineNamespaceStructure(string structureName)
        {
            return new GMacStructure(structureName, ChildSymbolScope);
        }

        /// <summary>
        /// Create child constant
        /// </summary>
        /// <param name="constantName"></param>
        /// <param name="constantValue"></param>
        /// <returns></returns>
        internal GMacConstant DefineNamespaceConstant(string constantName, ILanguageValue constantValue)
        {
            return new GMacConstant(constantName, ChildSymbolScope, constantValue);
        }


        internal bool LookupNamespace(string symbolName, out GMacNamespace outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.Namespace, out outSymbol);
        }

        internal bool LookupFrame(string symbolName, out GMacFrame outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.Frame, out outSymbol);
        }

        internal bool LookupNamespaceConstant(string symbolName, out GMacConstant outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.Constant, out outSymbol);
        }

        internal bool LookupNamespaceMacro(string symbolName, out GMacMacro outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.Macro, out outSymbol);
        }

        internal bool LookupNamespaceStructure(string symbolName, out GMacStructure outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.Structure, out outSymbol);
        }

        internal bool LookupMacroTemplate(string symbolName, out GMacMacroTemplate outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.MacroTemplate, out outSymbol);
        }

        internal bool LookupTransform(string symbolName, out GMacMultivectorTransform outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.Transform, out outSymbol);
        }

        /// <summary>
        /// Create the symbols cache for this namespace
        /// </summary>
        internal void CreateSymbolsCache()
        {
            SymbolsCache = new GMacAstSymbolsCache(this);
        }
    }
}
