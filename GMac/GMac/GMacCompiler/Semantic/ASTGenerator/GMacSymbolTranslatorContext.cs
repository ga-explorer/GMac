using CodeComposerLib.Irony.Compiler;
using CodeComposerLib.Irony.Semantic.Translator;
using CodeComposerLib.Irony.Semantic.Type;
using CodeComposerLib.Irony.SourceCode;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    internal sealed class GMacSymbolTranslatorContext : SymbolTranslatorContext
    {
        #region Static Members

        //public static GMacSymbolTranslatorContext Create(GMacAst dsl, LanguageCompilationLog log)
        //{
        //    return new GMacSymbolTranslatorContext(dsl, log);
        //}

        public static GMacSymbolTranslatorContext Create(GMacProjectCompiler parentCompiler)
        {
            return new GMacSymbolTranslatorContext(parentCompiler);
        }

        public static GMacSymbolTranslatorContext Create(GMacTempCodeCompiler parentCompiler)
        {
            return new GMacSymbolTranslatorContext(parentCompiler);
        }

        #endregion


        public GMacProjectCompiler ParentGMacProjectCompiler => ParentCompiler as GMacProjectCompiler;

        public GMacTempCodeCompiler ParentGMacTempCodeCompiler => ParentCompiler as GMacTempCodeCompiler;


        private GMacSymbolTranslatorContext(LanguageCompiler parentCompiler)
            : base(parentCompiler)
        {
        }

        //private GMacSymbolTranslatorContext(GMacAst dsl, LanguageCompilationLog log)
        //    : base(dsl, log)
        //{
        //}


        public bool HasParentNamespace => ActiveParentSymbolRoleName == RoleNames.Namespace;

        public bool HasParentFrame => ActiveParentSymbolRoleName == RoleNames.Frame;

        public bool HasParentTransform => ActiveParentSymbolRoleName == RoleNames.Transform;

        public bool HasParentStructure => ActiveParentSymbolRoleName == RoleNames.Structure;

        public bool HasParentMacro => ActiveParentSymbolRoleName == RoleNames.Macro;

        public bool HasParentNamespaceMacro => HasParentMacro && ((GMacMacro)ActiveParentSymbol).IsNamespaceMacro;

        public bool HasParentStructureMacro => HasParentMacro && ((GMacMacro)ActiveParentSymbol).IsStructureMacro;

        public bool HasParentFrameMacro => HasParentMacro && ((GMacMacro)ActiveParentSymbol).IsFrameMacro;

        public bool HasParentFrameMultivector => ActiveParentSymbolRoleName == RoleNames.FrameMultivector;


        public GMacNamespace ParentNamespace => HasParentNamespace ? (GMacNamespace)ActiveParentSymbol : null;

        public GMacFrame ParentFrame => HasParentFrame ? (GMacFrame)ActiveParentSymbol : null;

        public GMacMultivectorTransform ParentTransform => HasParentTransform ? (GMacMultivectorTransform)ActiveParentSymbol : null;

        public GMacStructure ParentStructure => HasParentStructure ? (GMacStructure)ActiveParentSymbol : null;

        public GMacMacro ParentMacro => HasParentMacro ? (GMacMacro)ActiveParentSymbol : null;

        internal GMacAst GMacRootAst => (GMacAst)RootAst;

        public GMacNamespace NearsestParentNamespace => (GMacNamespace)ActiveParentScope.NearsestParentNamespace;

        public GMacMacro NearestParentMacro => ActiveParentScope.GetNearsestParentSymbolWithRole(RoleNames.Macro) as GMacMacro;


        public bool IsDefiningNamespace => ActiveSymbolRoleName == RoleNames.Namespace;

        public bool IsDefiningFrame => ActiveSymbolRoleName == RoleNames.Frame;

        public bool IsDefiningTransform => ActiveSymbolRoleName == RoleNames.Transform;

        public bool IsDefiningStructure => ActiveSymbolRoleName == RoleNames.Structure;

        public bool IsDefiningNamespaceStructure => HasParentNamespace && IsDefiningStructure;

        public bool IsDefiningFrameStructure => HasParentFrame && IsDefiningStructure;

        public bool IsDefiningBasisVector => ActiveSymbolRoleName == RoleNames.FrameBasisVector;

        public bool IsDefiningSubspace => ActiveSymbolRoleName == RoleNames.FrameSubspace;

        public bool IsDefiningMultivectorClass => ActiveSymbolRoleName == RoleNames.FrameMultivector;

        public bool IsDefiningConstant => ActiveSymbolRoleName == RoleNames.Constant;

        public bool IsDefiningNamespaceConstant => HasParentNamespace && IsDefiningConstant;

        public bool IsDefiningFrameConstant => HasParentFrame && IsDefiningConstant;

        public bool IsDefiningMacro => ActiveSymbolRoleName == RoleNames.Macro;

        public bool IsDefiningNamespaceMacro => HasParentNamespace && IsDefiningMacro;

        public bool IsDefiningStructureMacro => HasParentStructure && IsDefiningMacro;

        public bool IsDefiningFrameMacro => HasParentFrame && IsDefiningMacro;

        public bool IsDefiningStructureMember => ActiveSymbolRoleName == RoleNames.StructureDataMember;

        public bool IsDefiningMacroParameter => ActiveSymbolRoleName == RoleNames.MacroParameter;

        public bool IsDefiningMacroVariable => ActiveSymbolRoleName == RoleNames.LocalVariable;

        public bool IsDefiningExpressionBlock => ActiveParentCommandBlock != null;


        /// <summary>
        /// Raise a type mismatch error
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ILanguageType CreateTypeMismatch(string message)
        {
            return CompilationLog.RaiseTypeMismatchError<ILanguageType>(message, ActiveParseNode);
        }

        ///// <summary>
        ///// Convert the given expression into an atomic expression by adding an assignment command if necessary 
        ///// and returning the lhs of the assignment as the new atomic expression. If the given expression is atomic 
        ///// it is returned directly
        ///// </summary>
        ///// <param name="expr"></param>
        ///// <returns></returns>
        //public ILanguageExpressionAtomic ExpressionToAtomicExpression(ILanguageExpression expr)
        //{
        //    //If expr is an atomic expression return expr directly
        //    if (expr is ILanguageExpressionAtomic)
        //        return (ILanguageExpressionAtomic)expr;

        //    //Else create a loical variable to hold value and return the local variable as a direct value access object
        //    LanguageValueAccess lhs =
        //        LanguageValueAccess.Create(
        //            SymbolLocalVariable.Create(expr.ExpressionType, this.ActiveParentScope)
        //        );

        //    this.ActiveParentCommandBlock.AddCommand_Assign(lhs, expr);

        //    return lhs;
        //}

        ///// <summary>
        ///// Convert the given expression into a local variable by adding an assignment command if necessary 
        ///// and returning the lhs local variable of the assignment as the new atomic expression. If the given expression 
        ///// is a local variable of this command block it is returned directly
        ///// </summary>
        ///// <param name="expr"></param>
        ///// <returns></returns>
        //public SymbolLocalVariable ExpressionToLocalVariable(ILanguageExpression expr)
        //{
        //    SymbolLocalVariable local_variable;

        //    //If expr is a local variable of this active scope return the local variable
        //    if (expr is LanguageValueAccess)
        //    {
        //        local_variable = ((LanguageValueAccess)expr).AsDirectLocalVariable;

        //        if (!ReferenceEquals(local_variable, null) && local_variable.ParentScope.ObjectID == ActiveParentScope.ObjectID)
        //            return local_variable;
        //    }

        //    //Else create a loical variable to hold value and return the local variable as a direct value access object
        //    local_variable = SymbolLocalVariable.Create(expr.ExpressionType, this.ActiveParentScope);

        //    LanguageValueAccess lhs = LanguageValueAccess.Create(local_variable);

        //    this.ActiveParentCommandBlock.AddCommand_Assign(lhs, expr);

        //    return local_variable;
        //}
    }
}
