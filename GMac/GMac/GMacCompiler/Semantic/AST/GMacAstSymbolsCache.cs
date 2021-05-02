using System.Collections.Generic;
using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.Semantic.Type;

namespace GMac.GMacCompiler.Semantic.AST
{
    public sealed class GMacAstSymbolsCache
    {
        internal GMacAst GMacRootAst { get; }

        internal LanguageSymbol RootSymbol { get; }

        internal Dictionary<string, LanguageSymbol> MainSymbols { get; private set; }

        internal Dictionary<string, GMacNamespace> Namespaces { get; private set; }

        internal Dictionary<string, GMacFrame> Frames { get; private set; }

        internal Dictionary<string, GMacFrameBasisVector> FrameBasisVectors { get; private set; }

        internal Dictionary<string, GMacFrameMultivector> FrameMultivectors { get; private set; }

        internal Dictionary<string, GMacFrameSubspace> FrameSubspaces { get; private set; }

        internal Dictionary<string, SymbolNamedValue> NamedValues { get; private set; }

        internal Dictionary<string, GMacConstant> Constants { get; private set; }

        internal Dictionary<string, GMacStructure> Structures { get; private set; }

        internal Dictionary<string, GMacMacro> Macros { get; private set; }

        internal Dictionary<string, GMacMultivectorTransform> Transforms { get; private set; }

        internal Dictionary<string, ILanguageType> Types { get; private set; }


        internal bool HasRootSymbol => RootSymbol != null;

        internal bool HasNamespaceRootSymbol => ReferenceEquals(RootSymbol as GMacNamespace, null) == false;

        internal bool HasFrameRootSymbol => ReferenceEquals(RootSymbol as GMacFrame, null) == false;

        internal string RootSymbolAccessName => HasRootSymbol ? RootSymbol.SymbolAccessName : string.Empty;


        internal GMacAstSymbolsCache(GMacAst rootAst)
        {
            GMacRootAst = rootAst;

            RootSymbol = null;

            InitializeDictionaries(GMacRootAst.MainSymbols());
        }

        internal GMacAstSymbolsCache(GMacNamespace rootSymbol)
        {
            GMacRootAst = rootSymbol.GMacRootAst;

            RootSymbol = rootSymbol;

            InitializeDictionaries(rootSymbol.MainSymbols());
        }

        internal GMacAstSymbolsCache(GMacFrame rootSymbol)
        {
            GMacRootAst = rootSymbol.GMacRootAst;

            RootSymbol = rootSymbol;

            InitializeDictionaries(rootSymbol.MainSymbols());
        }


        private string RelativeSymbolAccessName(LanguageSymbol symbol)
        {
            return
                HasRootSymbol
                ? symbol.SymbolAccessName.Substring(RootSymbolAccessName.Length + 1)
                : symbol.SymbolAccessName;
        }


        private bool TryAddSymbol(string symbolName, LanguageSymbol langSymbol)
        {
            if (MainSymbols.ContainsKey(symbolName))
                return false;

            MainSymbols.Add(symbolName, langSymbol);

            return true;
        }

        private bool TryAddAsNamespace(string symbolName, LanguageSymbol langSymbol)
        {
            var symbol = langSymbol as GMacNamespace;

            if (symbol == null)
                return false;

            Namespaces.Add(symbolName, symbol);

            return true;
        }

        private bool TryAddAsFrame(string symbolName, LanguageSymbol langSymbol)
        {
            var symbol = langSymbol as GMacFrame;

            if (symbol == null)
                return false;

            Frames.Add(symbolName, symbol);

            return true;
        }

        private bool TryAddAsFrameBasisVector(string symbolName, LanguageSymbol langSymbol)
        {
            var symbol = langSymbol as GMacFrameBasisVector;

            if (symbol == null)
                return false;

            FrameBasisVectors.Add(symbolName, symbol);

            NamedValues.Add(symbolName, symbol);

            return true;
        }

        private bool TryAddAsFrameMultivector(string symbolName, LanguageSymbol langSymbol)
        {
            var symbol = langSymbol as GMacFrameMultivector;

            if (symbol == null)
                return false;

            FrameMultivectors.Add(symbolName, symbol);

            Types.Add(symbolName, symbol);

            return true;
        }

        private bool TryAddAsFrameSubspace(string symbolName, LanguageSymbol langSymbol)
        {
            var symbol = langSymbol as GMacFrameSubspace;

            if (symbol == null)
                return false;

            FrameSubspaces.Add(symbolName, symbol);

            return true;
        }

        private bool TryAddAsConstant(string symbolName, LanguageSymbol langSymbol)
        {
            var symbol = langSymbol as GMacConstant;

            if (symbol == null)
                return false;

            Constants.Add(symbolName, symbol);

            NamedValues.Add(symbolName, symbol);

            return true;
        }

        private bool TryAddAsNamedValue(string symbolName, LanguageSymbol langSymbol)
        {
            var symbol = langSymbol as SymbolNamedValue;

            if (symbol == null)
                return false;

            NamedValues.Add(symbolName, symbol);

            return true;
        }

        private bool TryAddAsStructure(string symbolName, LanguageSymbol langSymbol)
        {
            var symbol = langSymbol as GMacStructure;

            if (symbol == null)
                return false;

            Structures.Add(symbolName, symbol);

            Types.Add(symbolName, symbol);

            return true;
        }

        private bool TryAddAsMacro(string symbolName, LanguageSymbol langSymbol)
        {
            var symbol = langSymbol as GMacMacro;

            if (symbol == null)
                return false;

            Macros.Add(symbolName, symbol);

            return true;
        }

        private bool TryAddAsTransform(string symbolName, LanguageSymbol langSymbol)
        {
            var symbol = langSymbol as GMacMultivectorTransform;

            if (symbol == null)
                return false;

            Transforms.Add(symbolName, symbol);

            return true;
        }


        private void CreateDictionaries()
        {
            MainSymbols =
                new Dictionary<string, LanguageSymbol>();

            Namespaces =
                new Dictionary<string, GMacNamespace>();

            Frames =
                new Dictionary<string, GMacFrame>();
            
            FrameBasisVectors =
                new Dictionary<string, GMacFrameBasisVector>();

            FrameMultivectors =
                new Dictionary<string, GMacFrameMultivector>();
            
            FrameSubspaces =
                new Dictionary<string, GMacFrameSubspace>();
            
            NamedValues =
                new Dictionary<string, SymbolNamedValue>();
            
            Constants =
                new Dictionary<string, GMacConstant>();
            
            Structures =
                new Dictionary<string, GMacStructure>();
            
            Macros =
                new Dictionary<string, GMacMacro>();
            
            Transforms =
                new Dictionary<string, GMacMultivectorTransform>();

            Types =
                new Dictionary<string, ILanguageType>();
        }

        private void AddToSubDictionary(string symbolName, LanguageSymbol langSymbol)
        {
            if (TryAddAsNamespace(symbolName, langSymbol))
                return;

            if (TryAddAsFrame(symbolName, langSymbol))
                return;

            if (TryAddAsFrameBasisVector(symbolName, langSymbol))
                return;

            if (TryAddAsFrameMultivector(symbolName, langSymbol))
                return;

            if (TryAddAsFrameSubspace(symbolName, langSymbol))
                return;

            if (TryAddAsConstant(symbolName, langSymbol))
                return;

            if (TryAddAsNamedValue(symbolName, langSymbol))
                return;

            if (TryAddAsStructure(symbolName, langSymbol))
                return;

            if (TryAddAsMacro(symbolName, langSymbol))
                return;

            TryAddAsTransform(symbolName, langSymbol);
        }

        private void InitializeDictionaries(IEnumerable<LanguageSymbol> symbols)
        {
            CreateDictionaries();

            foreach (var langSymbol in symbols)
            {
                var symbolName = RelativeSymbolAccessName(langSymbol);

                if (TryAddSymbol(symbolName, langSymbol))
                    AddToSubDictionary(symbolName, langSymbol);
            }
        }
    }
}
