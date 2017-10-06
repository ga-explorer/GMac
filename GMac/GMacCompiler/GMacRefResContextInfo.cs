using System.Collections.Generic;
using System.Linq;
using GMac.GMacAST.Symbols;
using IronyGrammars.Semantic;
using IronyGrammars.Semantic.Scope;

namespace GMac.GMacCompiler
{
    /// <summary>
    /// This class hols information about the main scope and possibly some opened scopes to be used for
    /// reference resolution of identifiers
    /// </summary>
    public sealed class GMacRefResContextInfo
    {
        internal LanguageScope MainScope { get; private set; }

        private readonly Dictionary<string, LanguageScope> _openedScopes =
            new Dictionary<string, LanguageScope>();


        public bool HasMainScope => MainScope != null;

        public bool HasOpenedScopes => _openedScopes.Count > 0;

        public int OpenedScopesCount => _openedScopes.Count;

        internal IEnumerable<LanguageScope> OpenedScopes
        {
            get { return _openedScopes.Select(pair => pair.Value); }
        }


        public GMacRefResContextInfo()
        {
        }

        public GMacRefResContextInfo(AstNamespace scopeSymbol)
        {
            MainScope = scopeSymbol.AssociatedNamespace.ChildScope;
        }

        public GMacRefResContextInfo(AstFrame scopeSymbol)
        {
            MainScope = scopeSymbol.AssociatedFrame.ChildScope;
        }

        public GMacRefResContextInfo(AstMacro scopeSymbol)
        {
            MainScope = scopeSymbol.AssociatedMacro.ChildScope;
        }

        internal GMacRefResContextInfo(IIronyAstObjectWithScope scopeSymbol)
        {
            MainScope = scopeSymbol.ChildScope;
        }

        internal GMacRefResContextInfo(LanguageScope scopeSymbol)
        {
            MainScope = scopeSymbol;
        }


        public bool ContainsScope(AstNamespace scopeSymbol)
        {
            return ContainsScope(scopeSymbol.AssociatedNamespace.ChildScope);
        }

        public bool ContainsScope(AstFrame scopeSymbol)
        {
            return ContainsScope(scopeSymbol.AssociatedFrame.ChildScope);
        }

        public bool ContainsScope(AstMacro scopeSymbol)
        {
            return ContainsScope(scopeSymbol.AssociatedMacro.ChildScope);
        }

        internal bool ContainsScope(IIronyAstObjectWithScope scopeSymbol)
        {
            return ContainsScope(scopeSymbol.ChildScope);
        }

        internal bool ContainsScope(LanguageScope scopeSymbol)
        {
            return 
                (HasMainScope && MainScope.QualifiedScopeName == scopeSymbol.QualifiedScopeName) ||
                _openedScopes.Any(pair => pair.Value.QualifiedScopeName == scopeSymbol.QualifiedScopeName);
        }


        public bool ContainsMainScope(AstNamespace scopeSymbol)
        {
            return ContainsMainScope(scopeSymbol.AssociatedNamespace.ChildScope);
        }

        public bool ContainsMainScope(AstFrame scopeSymbol)
        {
            return ContainsMainScope(scopeSymbol.AssociatedFrame.ChildScope);
        }

        public bool ContainsMainScope(AstMacro scopeSymbol)
        {
            return ContainsMainScope(scopeSymbol.AssociatedMacro.ChildScope);
        }

        internal bool ContainsMainScope(IIronyAstObjectWithScope scopeSymbol)
        {
            return ContainsMainScope(scopeSymbol.ChildScope);
        }

        internal bool ContainsMainScope(LanguageScope scopeSymbol)
        {
            return
                (HasMainScope && MainScope.QualifiedScopeName == scopeSymbol.QualifiedScopeName);
        }


        public bool ContainsOpenedScope(AstNamespace scopeSymbol)
        {
            return ContainsScope(scopeSymbol.AssociatedNamespace.ChildScope);
        }

        public bool ContainsOpenedScope(AstFrame scopeSymbol)
        {
            return ContainsScope(scopeSymbol.AssociatedFrame.ChildScope);
        }

        public bool ContainsOpenedScope(AstMacro scopeSymbol)
        {
            return ContainsScope(scopeSymbol.AssociatedMacro.ChildScope);
        }

        internal bool ContainsOpenedScope(IIronyAstObjectWithScope scopeSymbol)
        {
            return ContainsScope(scopeSymbol.ChildScope);
        }

        internal bool ContainsOpenedScope(LanguageScope scopeSymbol)
        {
            return
                _openedScopes.Any(pair => pair.Value.QualifiedScopeName == scopeSymbol.QualifiedScopeName);
        }


        public GMacRefResContextInfo SetMainScope(AstNamespace scopeSymbol)
        {
            return SetMainScope(scopeSymbol.AssociatedNamespace.ChildScope);
        }

        public GMacRefResContextInfo SetMainScope(AstFrame scopeSymbol)
        {
            return SetMainScope(scopeSymbol.AssociatedFrame.ChildScope);
        }

        public GMacRefResContextInfo SetMainScope(AstMacro scopeSymbol)
        {
            return SetMainScope(scopeSymbol.AssociatedMacro.ChildScope);
        }

        internal GMacRefResContextInfo SetMainScope(IIronyAstObjectWithScope scopeSymbol)
        {
            return SetMainScope(scopeSymbol.ChildScope);
        }

        internal GMacRefResContextInfo SetMainScope(LanguageScope scopeSymbol)
        {
            //If the main scope is one of the opened scopes, close it first
            CloseScope(scopeSymbol);

            MainScope = scopeSymbol;

            return this;
        }


        public GMacRefResContextInfo Clear(AstNamespace scopeSymbol)
        {
            return Clear(scopeSymbol.AssociatedNamespace.ChildScope);
        }

        public GMacRefResContextInfo Clear(AstFrame scopeSymbol)
        {
            return Clear(scopeSymbol.AssociatedFrame.ChildScope);
        }

        public GMacRefResContextInfo Clear(AstMacro scopeSymbol)
        {
            return Clear(scopeSymbol.AssociatedMacro.ChildScope);
        }

        internal GMacRefResContextInfo Clear(IIronyAstObjectWithScope scopeSymbol)
        {
            return Clear(scopeSymbol.ChildScope);
        }

        internal GMacRefResContextInfo Clear(LanguageScope scopeSymbol)
        {
            MainScope = scopeSymbol;
            _openedScopes.Clear();

            return this;
        }

        public GMacRefResContextInfo CloseScopes()
        {
            _openedScopes.Clear();

            return this;
        }


        public GMacRefResContextInfo OpenScope(AstNamespace scopeSymbol)
        {
            return OpenScope(scopeSymbol.AssociatedNamespace.ChildScope);
        }

        public GMacRefResContextInfo OpenScope(AstFrame scopeSymbol)
        {
            return OpenScope(scopeSymbol.AssociatedFrame.ChildScope);
        }

        public GMacRefResContextInfo OpenScope(AstMacro scopeSymbol)
        {
            return OpenScope(scopeSymbol.AssociatedMacro.ChildScope);
        }

        internal GMacRefResContextInfo OpenScope(IIronyAstObjectWithScope scopeSymbol)
        {
            return OpenScope(scopeSymbol.ChildScope);
        }

        internal GMacRefResContextInfo OpenScope(LanguageScope scopeSymbol)
        {
            var scopeName = scopeSymbol.QualifiedScopeName;

            if (MainScope.QualifiedScopeName == scopeName)
                return this;

            if (_openedScopes.ContainsKey(scopeName))
                _openedScopes.Remove(scopeName);

            _openedScopes.Add(scopeName, scopeSymbol);

            return this;
        }


        public GMacRefResContextInfo CloseScope(AstNamespace scopeSymbol)
        {
            return CloseScope(scopeSymbol.AssociatedNamespace.ChildScope);
        }

        public GMacRefResContextInfo CloseScope(AstFrame scopeSymbol)
        {
            return CloseScope(scopeSymbol.AssociatedFrame.ChildScope);
        }

        public GMacRefResContextInfo CloseScope(AstMacro scopeSymbol)
        {
            return CloseScope(scopeSymbol.AssociatedMacro.ChildScope);
        }

        internal GMacRefResContextInfo CloseScope(IIronyAstObjectWithScope scopeSymbol)
        {
            return CloseScope(scopeSymbol.ChildScope);
        }

        internal GMacRefResContextInfo CloseScope(LanguageScope scopeSymbol)
        {
            _openedScopes.Remove(scopeSymbol.QualifiedScopeName);

            return this;
        }
    }
}
