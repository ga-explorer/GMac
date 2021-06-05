using CodeComposerLib.Irony.Semantic.Command;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.Semantic.Type;
using GMac.Engine.Compiler.Semantic.ASTConstants;
using GMac.Engine.Compiler.Semantic.ASTInterpreter.HighLevel;

namespace GMac.Engine.Compiler.Semantic.AST
{
    /// <summary>
    /// A GMac macro
    /// </summary>
    public sealed class GMacMacro : SymbolProcedure
    {
        private CommandBlock _compiledBody;

        private CommandBlock _optimizedCompiledBody;

        internal GMacAst GMacRootAst => (GMacAst)RootAst;

        /// <summary>
        /// The macro body command block after compilation (unfolding of child macro calls, child blocks, and composite expressions)
        /// </summary>
        internal CommandBlock CompiledBody
        {
            get
            {
                if (ReferenceEquals(_compiledBody, null))
                    _compiledBody = HlMacroBodyCompiler.Compile(SymbolBody);

                return _compiledBody;
            }
        }

        /// <summary>
        /// The macro body command block after compilation (unfolding of child macro calls, child blocks, and composite expressions)
        /// </summary>
        internal CommandBlock OptimizedCompiledBody
        {
            get
            {
                if (ReferenceEquals(_optimizedCompiledBody, null))
                    _optimizedCompiledBody = HlOptimizer.OptimizeMacro(this);

                return _optimizedCompiledBody;
            }
        }

        /// <summary>
        /// The output parameter of the macro
        /// </summary>
        internal SymbolProcedureParameter OutputParameter => ChildSymbolScope.GetSymbol(GeneralConstants.MacroOutputParameterName, RoleNames.MacroParameter) as SymbolProcedureParameter;

        /// <summary>
        /// The language type of the output parameter of the macro (i.e. the return type of the macro)
        /// </summary>
        internal ILanguageType OutputParameterType => OutputParameter.SymbolType;

        /// <summary>
        /// True if this macro is defined inside a structure
        /// </summary>
        internal bool IsStructureMacro => ParentLanguageSymbol is GMacStructure;

        /// <summary>
        /// True if this macro is defined inside a namespace
        /// </summary>
        internal bool IsNamespaceMacro => ParentLanguageSymbol is GMacNamespace;

        /// <summary>
        /// True if this macro is defined inside a frame
        /// </summary>
        internal bool IsFrameMacro => ParentLanguageSymbol is GMacFrame;

        /// <summary>
        /// The parent namespace (for namespace macro only)
        /// </summary>
        internal GMacNamespace DirectParentNamespace => ParentLanguageSymbol as GMacNamespace;

        /// <summary>
        /// The parent structure (for structure macro only)
        /// </summary>
        internal GMacStructure ParentStructure => ParentLanguageSymbol as GMacStructure;

        /// <summary>
        /// The parent frame (for frame macro only)
        /// </summary>
        internal GMacFrame ParentFrame => ParentLanguageSymbol as GMacFrame;


        internal GMacMacro(string macroName, LanguageScope problemDomainScope)
            : base(macroName, problemDomainScope)
        {
        }


        internal bool LookupParameter(string symbolName, out SymbolProcedureParameter macroParam)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.MacroParameter, out macroParam);
        }

        internal bool LookupParameterType(string symbolName, out ILanguageType paramType)
        {
            if (ChildSymbolScope.LookupSymbol(symbolName, RoleNames.MacroParameter, out SymbolProcedureParameter macroParam))
            {
                paramType = macroParam.SymbolType;
                return true;
            }

            paramType = null;
            return false;
        }

        /// <summary>
        /// Get the type of a given local variable in the main body composite expression of the macro
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal ILanguageType LocalVariableType(string name)
        {
            return 
                SymbolBody.ChildCommandBlockScope.LookupSymbol(name, RoleNames.LocalVariable, out SymbolLocalVariable localVar) 
                ? localVar.SymbolType 
                : null;
        }
    }
}
