using System;
using System.Collections.Generic;
using IronyGrammars.Semantic.Symbol;
using UtilLib.DataStructures;

namespace GMac.GMacCompiler.Semantic.AST
{
    internal sealed class GMacSymbolicMathNames 
    {
        private readonly Dictionary<string, string> _namesDictionary = 
            new ADictionary<string, string>();

        public GMacAst GMacRootAst { get; private set; }


        internal GMacSymbolicMathNames(GMacAst root)
        {
            GMacRootAst = root;
        }


        public void Clear()
        {
            _namesDictionary.Clear();
        }

        public void RemoveName(string symbolAccessName)
        {
            _namesDictionary.Remove(symbolAccessName);
        }

        public string this[string symbolAccessName]
        {
            get
            {
                string mathName;

                return 
                    _namesDictionary.TryGetValue(symbolAccessName, out mathName) 
                    ? mathName : String.Empty;
            }
            set
            {
                var mathName = value.Trim();

                if (String.IsNullOrEmpty(mathName))
                {
                    _namesDictionary.Remove(symbolAccessName);
                    return;
                }

                if (_namesDictionary.ContainsKey(symbolAccessName))
                    _namesDictionary[symbolAccessName] = mathName;
                else
                    _namesDictionary.Add(symbolAccessName, mathName);
            }
        }

        public string this[ILanguageSymbol symbol]
        {
            get
            {
                var symbolAccessName = symbol.SymbolAccessName;
                string mathName;

                return
                    _namesDictionary.TryGetValue(symbolAccessName, out mathName)
                    ? mathName : String.Empty;
            }
            set
            {
                var symbolAccessName = symbol.SymbolAccessName;
                var mathName = value.Trim();

                if (String.IsNullOrEmpty(mathName))
                {
                    _namesDictionary.Remove(symbolAccessName);
                    return;
                }

                if (_namesDictionary.ContainsKey(symbolAccessName))
                    _namesDictionary[symbolAccessName] = mathName;
                else
                    _namesDictionary.Add(symbolAccessName, mathName);
            }
        }
    }
}
