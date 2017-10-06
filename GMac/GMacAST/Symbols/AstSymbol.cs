using System;
using System.Linq;
using GMac.GMacCompiler.Semantic.AST;
using IronyGrammars.Semantic.Symbol;
using TextComposerLib.Text;

namespace GMac.GMacAST.Symbols
{
    /// <summary>
    /// This class is the base class for all symbols in GMacAST
    /// </summary>
    public abstract class AstSymbol : AstObject
    {
        internal abstract LanguageSymbol AssociatedSymbol { get; }


        public override bool IsValid => AssociatedSymbol != null;

        public override AstRoot Root => new AstRoot((GMacAst)AssociatedSymbol.RootAst);

        public override bool IsValidSymbol => AssociatedSymbol != null;

        /// <summary>
        /// The name of this symbol
        /// </summary>
        public virtual string Name => AssociatedSymbol.ObjectName;

        /// <summary>
        /// The qualified access name of this symbol
        /// </summary>
        public virtual string AccessName => AssociatedSymbol.SymbolAccessName;

        /// <summary>
        /// The qualified access name of this symbol divided by the dot character '.' into separate parts
        /// </summary>
        public string[] AccessNameParts => AccessName.Split('.');

        /// <summary>
        /// The internal role name of this symbol
        /// </summary>
        public virtual string RoleName => AssociatedSymbol.SymbolRoleName;

        /// <summary>
        /// The number of parent symbols of this symbol up to a root namespace
        /// </summary>
        public int ParentSymbolsCount => AssociatedSymbol.ParentSymbolsCount;

        /// <summary>
        /// The parent symbol of this symbol
        /// </summary>
        public AstSymbol ParentSymbol
        {
            get
            {
                var parent = AssociatedSymbol.ParentLanguageSymbol;

                return
                    ReferenceEquals(parent, null)
                    ? null
                    : parent.ToAstSymbol();
            }
        }

        /// <summary>
        /// The parent namespace of this symbol if this symbol is a direct child of a namespace
        /// </summary>
        public AstNamespace ParentNamespace
        {
            get
            {
                var parent = AssociatedSymbol.ParentLanguageSymbol as GMacNamespace;

                return 
                    ReferenceEquals(parent, null)
                    ? null
                    : new AstNamespace(parent);
            }
        }

        /// <summary>
        /// The parent frame of this symbol if this symbol is a direct child of a frame
        /// </summary>
        public AstFrame ParentFrame
        {
            get
            {
                var parent = AssociatedSymbol.ParentLanguageSymbol as GMacFrame;

                return
                    ReferenceEquals(parent, null)
                    ? null
                    : new AstFrame(parent);
            }
        }

        /// <summary>
        /// Go up the parent symbols until a parent namespace if found
        /// </summary>
        public AstNamespace NearestParentNamespace
        {
            get
            {
                var parent = AssociatedSymbol.NearsestParentNamespace as GMacNamespace;

                return
                    ReferenceEquals(parent, null)
                    ? null
                    : new AstNamespace(parent);
            }
        }

        /// <summary>
        /// If this is a namespace return it else go up the parents until a parent namespace is found
        /// </summary>
        public AstNamespace NearestNamespace => IsValidNamespace 
            ? (AstNamespace)this 
            : new AstNamespace((GMacNamespace)AssociatedSymbol.NearsestNamespace);

        /// <summary>
        /// True if this symbol is a direct child of a namespace
        /// </summary>
        public bool HasParentNamespace => ReferenceEquals(AssociatedSymbol.ParentLanguageSymbol as GMacNamespace, null) == false;

        /// <summary>
        /// True if this symbol is a direct child of a frame
        /// </summary>
        public bool HasParentFrame => ReferenceEquals(AssociatedSymbol.ParentLanguageSymbol as GMacFrame, null) == false;

        /// <summary>
        /// True if this symbol is not a root namespace
        /// </summary>
        public bool HasParentSymbol => AssociatedSymbol.HasParentSymbol;

        /// <summary>
        /// True if this is a valid namespace
        /// </summary>
        public virtual bool IsValidNamespace => false;

        /// <summary>
        /// True if this is a valid frame
        /// </summary>
        public virtual bool IsValidFrame => false;

        /// <summary>
        /// True if this is a valid frame multivector type
        /// </summary>
        public virtual bool IsValidFrameMultivector => false;

        /// <summary>
        /// True if this is a valid frame basis vector
        /// </summary>
        public virtual bool IsValidBasisVector => false;

        /// <summary>
        /// True if this is a valid frame basis blade
        /// </summary>
        public virtual bool IsValidBasisBlade => false;

        /// <summary>
        /// True if this is a valid frame subspace
        /// </summary>
        public virtual bool IsValidSubspace => false;

        /// <summary>
        /// True if this is a valid constant
        /// </summary>
        public virtual bool IsValidConstant => false;

        /// <summary>
        /// True if this is a valid structure
        /// </summary>
        public virtual bool IsValidStructure => false;

        /// <summary>
        /// True if this is a valid structure data member
        /// </summary>
        public virtual bool IsValidStructureDataMember => false;

        /// <summary>
        /// True if this is a valid macro
        /// </summary>
        public virtual bool IsValidMacro => false;

        /// <summary>
        /// True if this is a valid macro
        /// </summary>
        public virtual bool IsValidTransform => false;

        /// <summary>
        /// True if this is a valid macro parameter
        /// </summary>
        public virtual bool IsValidMacroParameter => false;

        /// <summary>
        /// True if this is a valid local variable
        /// </summary>
        public virtual bool IsValidLocalVariable => false;

        /// <summary>
        /// True if this is a valid data store (ex. constant, local variable, macro parameter, or basis vector)
        /// </summary>
        public virtual bool IsValidDatastore => false;

        /// <summary>
        /// True if this is a valid constant data store (ex. constant or basis vector)
        /// </summary>
        public virtual bool IsValidConstantDatastore => false;

        /// <summary>
        /// True if this is a valid variable data store (ex. local variable or macro parameter)
        /// </summary>
        public virtual bool IsValidVariableDatastore => false;

        /// <summary>
        /// The symbolic math name associated with this symbol
        /// </summary>
        public virtual string SymbolicMathName 
        {
            get
            {
                var root = ((GMacAst)AssociatedSymbol.RootAst);

                var mathName = root.SymbolicMathNames[AssociatedSymbol];

                return String.IsNullOrEmpty(mathName) ? AssociatedSymbol.ObjectName : mathName;
            }
        }

        /// <summary>
        /// Get the qualified access name of this symbol relative to the access name of the given symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        internal string RelativeAccessName(ILanguageSymbol symbol)
        {
            return RelativeAccessName(symbol.SymbolAccessName);
        }

        /// <summary>
        /// Get the qualified access name of this symbol relative to the access name of the given symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public string RelativeAccessName(AstSymbol symbol)
        {
            return RelativeAccessName(symbol.AccessName);
        }

        /// <summary>
        /// Get the qualified access name of this symbol relative to the given access name of some other symbol
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public string RelativeAccessName(string accessName)
        {
            var name1 = AssociatedSymbol.SymbolAccessName.Split('.');
            var name2 = accessName.Split('.');

            var n = Math.Min(name1.Length, name2.Length);
            var index = 0;

            for (var i = 0; i < n; i++)
                if (name1[i] != name2[i])
                {
                    index = i;
                    break;
                }

            if (index == 0)
                return AssociatedSymbol.SymbolAccessName;

            return name1.Skip(index).Concatenate(".");
        }

        /// <summary>
        /// Sets the symbolic math name of this GMacAST symbol
        /// </summary>
        /// <param name="mathName"></param>
        public virtual void SetSymbolicMathName(string mathName)
        {
            var root = ((GMacAst) AssociatedSymbol.RootAst);

            root.SymbolicMathNames[AssociatedSymbol] = mathName;
        }

        /// <summary>
        /// Sets the symbolic math name of this GMacAST symbol to nothing
        /// </summary>
        public virtual void SetSymbolicMathName()
        {
            var root = ((GMacAst)AssociatedSymbol.RootAst);

            root.SymbolicMathNames.RemoveName(AssociatedSymbol.SymbolAccessName);
        }


        public override string ToString()
        {
            return AssociatedSymbol.SymbolAccessName;
        }
    }
}
