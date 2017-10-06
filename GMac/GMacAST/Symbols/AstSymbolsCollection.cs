using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TextComposerLib.Text;
using UtilLib.DataStructures;

namespace GMac.GMacAST.Symbols
{
    /// <summary>
    /// This class represents a collection of GMacAST symbols
    /// </summary>
    public sealed class AstSymbolsCollection : ICollection<AstSymbol>
    {
        private readonly Dictionary<string, AstSymbol> _symbols =
            new ADictionary<string, AstSymbol>();

        private readonly Dictionary<string, string> _symbolsParentsNames =
            new Dictionary<string, string>();


        /// <summary>
        /// The AST root node
        /// </summary>
        public AstRoot Root { get; private set; }


        /// <summary>
        /// The access names of the symbols in the collection
        /// </summary>
        public IEnumerable<string> SymbolAccessNames
        {
            get { return _symbols.Select(pair => pair.Value.AccessName); }
        }


        public AstSymbolsCollection(AstRoot ast)
        {
            Root = ast;
        }

        public AstSymbolsCollection(AstRoot ast, IEnumerable<AstSymbol> symbols)
        {
            Root = ast;

            AddRange(symbols);
        }


        private void UpdateSymbolsParents(string symbolAccessName)
        {
            var dotIndex = symbolAccessName.IndexOf('.');

            while (dotIndex > 0)
            {
                var parentName = symbolAccessName.Substring(0, dotIndex);

                if (_symbolsParentsNames.ContainsKey(parentName) == false)
                    _symbolsParentsNames.Add(parentName, parentName);

                dotIndex = symbolAccessName.IndexOf('.', dotIndex + 1);
            }
        }


        /// <summary>
        /// Set the symbols in the collection using a given list of symbols.
        /// </summary>
        /// <param name="symbols"></param>
        public void SetSymbols(IEnumerable<AstSymbol> symbols)
        {
            Clear();

            AddRange(symbols);
        }

        /// <summary>
        /// Add a set of symbols to the collection
        /// </summary>
        /// <param name="symbols"></param>
        public void AddRange(IEnumerable<AstSymbol> symbols)
        {
            foreach (var symbol in symbols)
                Add(symbol);
        }


        /// <summary>
        /// Add a symbol to the collection
        /// </summary>
        /// <param name="item"></param>
        public void Add(AstSymbol item)
        {
            _symbols.Add(item.AccessName, item);

            UpdateSymbolsParents(item.AccessName);
        }

        /// <summary>
        /// Clear all symbols from the collection
        /// </summary>
        public void Clear()
        {
            _symbols.Clear();

            _symbolsParentsNames.Clear();
        }

        /// <summary>
        /// True if the collection contains the given symbol
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(AstSymbol item)
        {
            return
                _symbols.ContainsKey(item.AccessName);
        }

        /// <summary>
        /// True if the collection contains a symbol with the given access name
        /// </summary>
        /// <param name="itemAccessName"></param>
        /// <returns></returns>
        public bool Contains(string itemAccessName)
        {
            return
                _symbols.ContainsKey(itemAccessName);
        }

        /// <summary>
        /// True if the collection contains one or more child symbols of the given symbol. The parent symbol
        /// itself may or may not be in the collection
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool ContainsChildOf(AstSymbol parent)
        {
            return
                _symbolsParentsNames.ContainsKey(parent.AccessName);
        }

        /// <summary>
        /// True if the collection contains one or more child symbols of a parent symbol with the given 
        /// access name. The parent symbol itself may or may not be in the collection
        /// </summary>
        /// <param name="parentAccessName"></param>
        /// <returns></returns>
        public bool ContainsChildOf(string parentAccessName)
        {
            return
                _symbolsParentsNames.ContainsKey(parentAccessName);
        }

        /// <summary>
        /// True if the collection is empty or contains the given symbol
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool IsEmptyOrContains(AstSymbol item)
        {
            return
                _symbols.Count == 0 ||
                _symbols.ContainsKey(item.AccessName);
        }

        /// <summary>
        /// True if the collection is empty or contains a symbol with the given access name
        /// </summary>
        /// <param name="itemAccessName"></param>
        /// <returns></returns>
        public bool IsEmptyOrContains(string itemAccessName)
        {
            return
                _symbols.Count == 0 ||
                _symbols.ContainsKey(itemAccessName);
        }

        /// <summary>
        /// True if the collection is empty or contains one or more child symbols of the given symbol. 
        /// The parent symbol itself may or may not be in the collection
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool IsEmptyOrContainsChildOf(AstSymbol parent)
        {
            return
                _symbols.Count == 0 ||
                _symbolsParentsNames.ContainsKey(parent.AccessName);
        }

        /// <summary>
        /// True if the collection is empty or contains one or more child symbols of a parent symbol with the  
        /// given access name. The parent symbol itself may or may not be in the collection
        /// </summary>
        /// <param name="parentAccessName"></param>
        /// <returns></returns>
        public bool IsEmptyOrContainsChildOf(string parentAccessName)
        {
            return
                _symbols.Count == 0 ||
                _symbolsParentsNames.ContainsKey(parentAccessName);
        }

        /// <summary>
        /// Copy the contents of the collection to the given array starting at the given array index
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(AstSymbol[] array, int arrayIndex)
        {
            _symbols.Values.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// The number of symbols in the collection
        /// </summary>
        public int Count => _symbols.Count;

        public bool IsReadOnly => false;

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(AstSymbol item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<AstSymbol> GetEnumerator()
        {
            return _symbols.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _symbols.Values.GetEnumerator();
        }


        public override string ToString()
        {
            return
                SymbolAccessNames.Concatenate(Environment.NewLine);
        }
    }
}
