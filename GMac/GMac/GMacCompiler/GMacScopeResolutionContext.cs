using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic;
using CodeComposerLib.Irony.Semantic.Scope;
using GMac.GMacAST.Symbols;

namespace GMac.GMacCompiler
{
    /// <summary>
    /// This class holds information about the main scope and possibly some opened scopes to be used for
    /// reference resolution of identifiers
    /// </summary>
    public sealed class GMacScopeResolutionContext
    {
        internal LanguageScope MainScope { get; private set; }

        private readonly Dictionary<string, LanguageScope> _openedScopes =
            new Dictionary<string, LanguageScope>();


        public bool HasMainScope 
            => MainScope != null;

        public bool HasOpenedScopes 
            => _openedScopes.Count > 0;

        public int OpenedScopesCount 
            => _openedScopes.Count;

        internal IEnumerable<LanguageScope> OpenedScopes
            => _openedScopes.Select(pair => pair.Value);


        public GMacScopeResolutionContext()
        {
        }

        public GMacScopeResolutionContext(AstNamespace scopeSymbol)
        {
            MainScope = scopeSymbol.AssociatedNamespace.ChildScope;
        }

        public GMacScopeResolutionContext(AstFrame scopeSymbol)
        {
            MainScope = scopeSymbol.AssociatedFrame.ChildScope;
        }

        public GMacScopeResolutionContext(AstMacro scopeSymbol)
        {
            MainScope = scopeSymbol.AssociatedMacro.ChildScope;
        }

        internal GMacScopeResolutionContext(IIronyAstObjectWithScope scopeSymbol)
        {
            MainScope = scopeSymbol.ChildScope;
        }

        internal GMacScopeResolutionContext(LanguageScope scopeSymbol)
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


        public GMacScopeResolutionContext SetMainScope(AstNamespace scopeSymbol)
        {
            return SetMainScope(scopeSymbol.AssociatedNamespace.ChildScope);
        }

        public GMacScopeResolutionContext SetMainScope(AstFrame scopeSymbol)
        {
            return SetMainScope(scopeSymbol.AssociatedFrame.ChildScope);
        }

        public GMacScopeResolutionContext SetMainScope(AstMacro scopeSymbol)
        {
            return SetMainScope(scopeSymbol.AssociatedMacro.ChildScope);
        }

        internal GMacScopeResolutionContext SetMainScope(IIronyAstObjectWithScope scopeSymbol)
        {
            return SetMainScope(scopeSymbol.ChildScope);
        }

        internal GMacScopeResolutionContext SetMainScope(LanguageScope scopeSymbol)
        {
            //If the main scope is one of the opened scopes, close it first
            CloseScope(scopeSymbol);

            MainScope = scopeSymbol;

            return this;
        }


        public GMacScopeResolutionContext Clear(AstNamespace scopeSymbol)
        {
            return Clear(scopeSymbol.AssociatedNamespace.ChildScope);
        }

        public GMacScopeResolutionContext Clear(AstFrame scopeSymbol)
        {
            return Clear(scopeSymbol.AssociatedFrame.ChildScope);
        }

        public GMacScopeResolutionContext Clear(AstMacro scopeSymbol)
        {
            return Clear(scopeSymbol.AssociatedMacro.ChildScope);
        }

        internal GMacScopeResolutionContext Clear(IIronyAstObjectWithScope scopeSymbol)
        {
            return Clear(scopeSymbol.ChildScope);
        }

        internal GMacScopeResolutionContext Clear(LanguageScope scopeSymbol)
        {
            MainScope = scopeSymbol;
            _openedScopes.Clear();

            return this;
        }

        public GMacScopeResolutionContext CloseScopes()
        {
            _openedScopes.Clear();

            return this;
        }


        public GMacScopeResolutionContext OpenScope(AstNamespace scopeSymbol)
        {
            return OpenScope(scopeSymbol.AssociatedNamespace.ChildScope);
        }

        public GMacScopeResolutionContext OpenScope(AstFrame scopeSymbol)
        {
            return OpenScope(scopeSymbol.AssociatedFrame.ChildScope);
        }

        public GMacScopeResolutionContext OpenScope(AstMacro scopeSymbol)
        {
            return OpenScope(scopeSymbol.AssociatedMacro.ChildScope);
        }

        internal GMacScopeResolutionContext OpenScope(IIronyAstObjectWithScope scopeSymbol)
        {
            return OpenScope(scopeSymbol.ChildScope);
        }

        internal GMacScopeResolutionContext OpenScope(LanguageScope scopeSymbol)
        {
            var scopeName = scopeSymbol.QualifiedScopeName;

            if (MainScope.QualifiedScopeName == scopeName)
                return this;

            if (_openedScopes.ContainsKey(scopeName))
                _openedScopes.Remove(scopeName);

            _openedScopes.Add(scopeName, scopeSymbol);

            return this;
        }


        public GMacScopeResolutionContext CloseScope(AstNamespace scopeSymbol)
        {
            return CloseScope(scopeSymbol.AssociatedNamespace.ChildScope);
        }

        public GMacScopeResolutionContext CloseScope(AstFrame scopeSymbol)
        {
            return CloseScope(scopeSymbol.AssociatedFrame.ChildScope);
        }

        public GMacScopeResolutionContext CloseScope(AstMacro scopeSymbol)
        {
            return CloseScope(scopeSymbol.AssociatedMacro.ChildScope);
        }

        internal GMacScopeResolutionContext CloseScope(IIronyAstObjectWithScope scopeSymbol)
        {
            return CloseScope(scopeSymbol.ChildScope);
        }

        internal GMacScopeResolutionContext CloseScope(LanguageScope scopeSymbol)
        {
            _openedScopes.Remove(scopeSymbol.QualifiedScopeName);

            return this;
        }
    }
}
