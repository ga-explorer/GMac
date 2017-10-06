using System.Collections.Generic;
using System.Linq;
using GMac.GMacCompiler.Semantic.ASTConstants;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;
using IronyGrammars.Semantic.Scope;
using IronyGrammars.Semantic.Symbol;
using IronyGrammars.Semantic.Type;

namespace GMac.GMacCompiler.Semantic.AST
{
    /// <summary>
    /// A GMac structure holding several data members of any acceptable GMac type
    /// </summary>
    public sealed class GMacStructure : TypeStructure
    {
        internal GMacAst GMacRootAst => (GMacAst)RootAst;

        /// <summary>
        /// All child macros
        /// </summary>
        internal IEnumerable<GMacMacro> Macros { get; private set; }

        /// <summary>
        /// True if defined inside the scope of a frame
        /// </summary>
        internal bool IsFrameStructure => ParentLanguageSymbol.SymbolRoleName == RoleNames.Frame;

        /// <summary>
        /// True if defined inside the scope of a namespace
        /// </summary>
        internal bool IsNamespaceStructure => ParentLanguageSymbol.SymbolRoleName == RoleNames.Namespace;

        /// <summary>
        /// The parent frame (for a frame structures only)
        /// </summary>
        internal GMacFrame ParentFrame => IsFrameStructure ? ParentLanguageSymbol as GMacFrame : null;


        internal GMacStructure(string structureName, LanguageScope parentScope)
            : base(structureName, parentScope)
        {
            Macros = ChildSymbolScope.Symbols(RoleNames.Macro).Cast<GMacMacro>();
        }


        /// <summary>
        /// Create a child macro
        /// </summary>
        /// <param name="macroName"></param>
        /// <returns></returns>
        internal GMacMacro DefineStructureMacro(string macroName)
        {
            return new GMacMacro(macroName, ChildSymbolScope);
        }


        internal bool LookupDataMember(string symbolName, out SymbolStructureDataMember outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.StructureDataMember, out outSymbol);
        }

        internal bool LookupStructureMacro(string symbolName, out GMacMacro outSymbol)
        {
            return ChildSymbolScope.LookupSymbol(symbolName, RoleNames.Macro, out outSymbol);
        }


        internal GMacStructureConstructor CreateConstructorOperator()
        {
            return GMacStructureConstructor.Create(this);
        }

        internal GMacStructureConstructor CreateConstructorOperator(ILanguageExpressionAtomic defaultValueSource)
        {
            return GMacStructureConstructor.Create(this, defaultValueSource);
        }

        internal BasicPolyadic CreateConstructorExpression()
        {
            return 
                BasicPolyadic.Create(
                    this, 
                    GMacStructureConstructor.Create(this)
                    );
        }

        internal BasicPolyadic CreateConstructorExpression(OperandsByValueAccess operands)
        {
            return
                BasicPolyadic.Create(
                    this,
                    GMacStructureConstructor.Create(this),
                    operands
                    );
        }

        internal BasicPolyadic CreateConstructorExpression(ILanguageExpressionAtomic defaultValueSource)
        {
            return
                BasicPolyadic.Create(
                    this,
                    GMacStructureConstructor.Create(this, defaultValueSource)
                    );
        }

        internal BasicPolyadic CreateConstructorExpression(ILanguageExpressionAtomic defaultValueSource, OperandsByValueAccess operands)
        {
            return
                BasicPolyadic.Create(
                    this,
                    GMacStructureConstructor.Create(this, defaultValueSource),
                    operands
                    );
        }
    }
}
