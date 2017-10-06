using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMac.GMacScripting
{
    internal sealed class GMacScriptShortcuts
    {
        private readonly Dictionary<string, string> _shortcutsDictionary =
            new Dictionary<string, string>();


        public string this[string shortcut]
        {
            get
            {
                if (String.IsNullOrEmpty(shortcut)) return String.Empty;

                string methodName;

                return
                    _shortcutsDictionary.TryGetValue(shortcut.ToLower(), out methodName)
                    ? methodName : shortcut;
            }
            set
            {
                if (String.IsNullOrEmpty(shortcut)) return;

                var command = shortcut.Trim().ToLower();

                if (String.IsNullOrEmpty(shortcut)) return;

                if (_shortcutsDictionary.ContainsKey(command))
                    _shortcutsDictionary[command] = value;

                else
                    _shortcutsDictionary.Add(command, value);
            }
        }


        public GMacScriptShortcuts()
        {
            ResetShortcuts();
        }

        
        public void ResetShortcuts(Dictionary<string, string> shortcutsDict)
        {
            _shortcutsDictionary.Clear();

            foreach (var pair in shortcutsDict)
                _shortcutsDictionary.Add(pair.Key, pair.Value);
        }

        public void ResetShortcuts(IEnumerable<string> shortcutsDict)
        {
            _shortcutsDictionary.Clear();

            var shortcutsList = shortcutsDict.ToArray();

            if (shortcutsList.Length == 0)
            {
                ResetShortcuts();
                return;
            }

            foreach (var pair in shortcutsList)
            {
                var idx = pair.IndexOf(':');

                if (idx < 1 || idx > pair.Length - 2) continue;

                var key = pair.Substring(idx + 1).Trim().ToLower();
                var value = pair.Substring(0, idx).Trim().ToLower();

                if (String.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) continue;

                _shortcutsDictionary.Add(key, value);
            }
        }

        public void ResetShortcuts()
        {
            _shortcutsDictionary.Clear();

            //Initialization of the GMacDSL Code’s Computational Context
            SetShortcuts("Ipr.Reset", "reset", "rst");
            SetShortcuts("Ipr.OpenScope", "open", "opn");
            SetShortcuts("Ipr.CloseScope", "close", "cls");

            //Accessing GMacAST Information
            SetShortcuts("Ipr.Namespace", "namespace", "ns");
            SetShortcuts("Ipr.Frame", "frame", "fr");
            SetShortcuts("Ipr.BasisVector", "basisvector", "bv");
            SetShortcuts("Ipr.FrameMultivector", "multivector", "mv");
            SetShortcuts("Ipr.Subspace", "subspace", "ss");
            SetShortcuts("Ipr.Constant", "constant", "ct");
            SetShortcuts("Ipr.Structure", "structure", "st");
            SetShortcuts("Ipr.Macro", "macro", "mc");
            SetShortcuts("Ipr.LocalVariable", "variable", "lv");
            SetShortcuts("Ipr.Symbol", "symbol", "sy");
            SetShortcuts("Ipr.GMacType", "type", "ty");
            //SetShortcuts("Ipr.DataMember", "member", "mb");
            //SetShortcuts("Ipr.Parameter", "parameter", "pr");

            //Compilation, Execution, and Evaluation of GMacDSL Commands and Expressions
            SetShortcuts("Ipr.ValueAccess", "valueaccess", "vla");
            SetShortcuts("Ipr.GMacTypeOf", "typeof", "tyo");
            SetShortcuts("Ipr.ValueAccessExists", "valueexists", "vle");
            SetShortcuts("Ipr.Expression", "expression", "expr");
            SetShortcuts("Ipr.Declare", "declare", "dclr");
            SetShortcuts("Ipr.Assign", "assign", "asn");
            SetShortcuts("Ipr.Execute", "execute", "exec");
            SetShortcuts("Ipr.ValueOf", "valueof", "vlo");
            SetShortcuts("Ipr.Evaluate", "evaluate", "eval", "evl");

            //Initialize Multivector Values from Patterns and Subspaces
            SetShortcuts("Ipr.SubspaceToMultivector", "subspacetomultivector", "ss2mv");

            //Low-Level Communication with the Symbolic Engine
            SetShortcuts("Ipr.ComputeToExpr", "computetoexpr", "cte");
            SetShortcuts("Ipr.ComputeToString", "computetostring", "cts");
            SetShortcuts("Ipr.ComputeToInputForm", "computetoinputform", "ctif");
            SetShortcuts("Ipr.ComputeToOutputForm", "computetooutputform", "ctof");
            SetShortcuts("Ipr.ComputeToTypeset", "computetotypeset", "ctt");
            SetShortcuts("Ipr.ComputeToImage", "computetoimage", "cti");

            //Report Values Generated During Script Execution
            SetShortcuts("Ipr.AsString", "asstring", "as");
            SetShortcuts("Ipr.Output.Store", "store", "str");
            SetShortcuts("Ipr.Output.Log.IncreaseIndentation", "incindent", "ii");
            SetShortcuts("Ipr.Output.Log.DecreaseIndentation", "decindent", "di");
            SetShortcuts("Ipr.Output.Log.Append", "append", "ap");
            SetShortcuts("Ipr.Output.Log.AppendLine", "appendline", "apl");
            SetShortcuts("Ipr.Output.Log.AppendNewLine", "appendnewline", "apnl");
            SetShortcuts("Ipr.Output.Log.AppendAtNewLine", "appendatnewline", "apanl");
            SetShortcuts("Ipr.Output.Log.AppendLineAtNewLine", "appendlineatnewline", "aplanl");

            //Set Shortcut Names for Methods
            SetShortcuts("Ipr.SetShortcuts", "setshortcuts", "ssc");
            SetShortcuts("Ipr.ResetShortcuts", "resetshortcuts", "rsc");
        }

        public void SetShortcuts(string methodName, params string[] shortcuts)
        {
            var commands =
                shortcuts.Where(
                    s => String.IsNullOrEmpty(s) == false && String.IsNullOrEmpty(s.Trim()) == false
                    )
                .Select(s => s.ToLower());

            foreach (var command in commands)
            {
                if (_shortcutsDictionary.ContainsKey(command))
                    _shortcutsDictionary[command] = methodName;

                else
                    _shortcutsDictionary.Add(command, methodName);
            }
        }

        public bool TryGetMethodName(string shortcut, out string methodName)
        {
            return _shortcutsDictionary.TryGetValue(shortcut, out methodName);
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            foreach (var pair in _shortcutsDictionary.OrderBy(p => p.Value))
                s.Append(pair.Value).Append(" : ").AppendLine(pair.Key);

            return s.ToString();
        }
    }
}
