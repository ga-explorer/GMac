using System.Linq;
using CodeComposerLib.Irony.Semantic.Command;
using CodeComposerLib.Irony.Semantic.Scope;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;

namespace GMac.GMacAST.Commands
{
    public abstract class AstCommand : AstObject
    {
        internal abstract LanguageCommand AssociatedCommand { get; }


        public override bool IsValid => AssociatedCommand != null;

        public override AstRoot Root => new AstRoot((GMacAst)AssociatedCommand.RootAst);

        public override bool IsValidCommand => AssociatedCommand != null;

        /// <summary>
        /// True if this command is a block of commands
        /// </summary>
        public virtual bool IsValidCommandBlock => false;

        /// <summary>
        /// True if this command is a declare command
        /// </summary>
        public virtual bool IsValidCommandDeclare => false;

        /// <summary>
        /// True if this command is a let command
        /// </summary>
        public virtual bool IsValidCommandLet => false;

        /// <summary>
        /// True if this command has a parent command block
        /// </summary>
        public bool HasParentBlock
        {
            get
            {
                var scope = AssociatedCommand.ParentScope as ScopeCommandBlockChild;

                return ReferenceEquals(scope, null) == false;
            }
        }

        /// <summary>
        /// True if this command has a parent symbol (like a macro body command block)
        /// </summary>
        public bool HasParentSymbol
        {
            get
            {
                var scope = AssociatedCommand.ParentScope as ScopeSymbolChild;

                return ReferenceEquals(scope, null) == false;
            }
        }

        /// <summary>
        /// True if this command has a parent macro
        /// </summary>
        public bool HasParentMacro
        {
            get
            {
                var scope = AssociatedCommand.ParentScope as ScopeSymbolChild;

                if (ReferenceEquals(scope, null))
                    return false;

                var macro = scope.ParentLanguageSymbol as GMacMacro;

                return ReferenceEquals(macro, null) == false;
            }
        }

        /// <summary>
        /// Go up the static chain until a parent symbol is found
        /// </summary>
        public AstSymbol NearestParentSymbol
        {
            get
            {
                var scope =
                    AssociatedCommand
                    .ParentScope
                    .AncestorScopes
                    .FirstOrDefault(s => s is ScopeSymbolChild);

                return 
                    ReferenceEquals(scope, null)
                    ? null
                    : ((ScopeSymbolChild) scope).ParentLanguageSymbol.ToAstSymbol();
            }
        }

        /// <summary>
        /// Go up the static chain until a parent macro is found, if any
        /// </summary>
        public AstSymbol NearestParentMacro
        {
            get
            {
                var scope =
                    AssociatedCommand
                    .ParentScope
                    .AncestorScopes
                    .FirstOrDefault(s => s is ScopeSymbolChild);

                if (ReferenceEquals(scope, null))
                    return null;

                var macro = ((ScopeSymbolChild) scope).ParentLanguageSymbol as GMacMacro;

                return
                    ReferenceEquals(macro, null)
                    ? null
                    : new AstMacro(macro);
            }
        }

        /// <summary>
        /// The parent command block of this command, if any
        /// </summary>
        public AstCommand ParentBlock
        {
            get
            {
                var scope = AssociatedCommand.ParentScope as ScopeCommandBlockChild;

                return 
                    ReferenceEquals(scope, null) 
                    ? null 
                    : new AstCommandBlock(scope.ParentCommandBlock);
            }
        }

        /// <summary>
        /// The parent symbol of this command, if any
        /// </summary>
        public AstSymbol ParentSymbol
        {
            get
            {
                var scope = AssociatedCommand.ParentScope as ScopeSymbolChild;

                return
                    ReferenceEquals(scope, null)
                    ? null
                    : scope.ParentLanguageSymbol.ToAstSymbol();
            }
        }

        /// <summary>
        /// The parent macro of this command, if any
        /// </summary>
        public AstMacro ParentMacro
        {
            get
            {
                var scope = AssociatedCommand.ParentScope as ScopeSymbolChild;

                if (ReferenceEquals(scope, null))
                    return null;

                var macro = scope.ParentLanguageSymbol as GMacMacro;

                return
                    ReferenceEquals(macro, null)
                    ? null
                    : macro.ToAstMacro();
            }
        }


        public override string ToString()
        {
            return AssociatedCommand.ToString();
        }
    }
}
