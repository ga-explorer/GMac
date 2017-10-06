using System.Collections.Generic;
using System.Linq;
using IronyGrammars.Semantic.Symbol;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.HighLevel
{
    /// <summary>
    /// The table holding information of all l-values for high-level optimization process
    /// </summary>
    internal sealed class HlLValuesTable
    {
        /// <summary>
        /// The dictionary containing all l-values information. The key is the name of the l-value and the value
        /// is a list of all definitions for that l-value. If the table is in SSA form each list has no more 
        /// than one definition
        /// </summary>
        private readonly Dictionary<string, List<HlLValueDefinitionInfo>> _lValuesDictionary = new Dictionary<string, List<HlLValueDefinitionInfo>>();


        /// <summary>
        /// Clear the table data
        /// </summary>
        public void Clear()
        {
            _lValuesDictionary.Clear();
        }

        /// <summary>
        /// Add an entry in this table for a macro input parameter without a defining command
        /// </summary>
        /// <param name="lvalue"></param>
        /// <returns></returns>
        public HlLValueDefinitionInfo AddDefinition(SymbolProcedureParameter lvalue)
        {
            List<HlLValueDefinitionInfo> lvalueDefList;

            if (_lValuesDictionary.TryGetValue(lvalue.ObjectName, out lvalueDefList) == false)
            {
                lvalueDefList = new List<HlLValueDefinitionInfo>();

                _lValuesDictionary.Add(lvalue.ObjectName, lvalueDefList);
            }

            var defInfo = new HlLValueDefinitionInfo(lvalueDefList.Count, lvalue, null);

            lvalueDefList.Add(defInfo);

            return defInfo;
        }

        /// <summary>
        /// Add definition for an l-value
        /// </summary>
        /// <param name="defSt"></param>
        /// <returns></returns>
        public HlLValueDefinitionInfo AddDefinition(HlCommandInfo defSt)
        {
            var lvalue = defSt.LhslValue;
            List<HlLValueDefinitionInfo> lvalueDefList;

            if (_lValuesDictionary.TryGetValue(lvalue.ObjectName, out lvalueDefList) == false)
            {
                lvalueDefList = new List<HlLValueDefinitionInfo>();

                _lValuesDictionary.Add(lvalue.ObjectName, lvalueDefList);
            }

            var defInfo = new HlLValueDefinitionInfo(lvalueDefList.Count, lvalue, defSt);

            lvalueDefList.Add(defInfo);

            return defInfo;
        }

        ///// <summary>
        ///// Get the information of the first definition for the l-value with the given name
        ///// </summary>
        ///// <param name="lvalue_name"></param>
        ///// <returns></returns>
        //public HLOLValueDefinitionInfo GetFirstDefinitionInfo(string lvalue_name)
        //{
        //    if (_LValuesDictionary.ContainsKey(lvalue_name))
        //        return _LValuesDictionary[lvalue_name].FirstOrDefault();

        //    return null;
        //}

        /// <summary>
        /// Get the information of the first definition for the given l-value
        /// </summary>
        /// <param name="lvalue"></param>
        /// <returns></returns>
        public HlLValueDefinitionInfo GetFirstDefinitionInfo(SymbolLValue lvalue)
        {
            return 
                _lValuesDictionary.ContainsKey(lvalue.ObjectName) 
                ? _lValuesDictionary[lvalue.ObjectName].FirstOrDefault() 
                : null;
        }

        /// <summary>
        /// Get the information of the first command for the given l-value
        /// </summary>
        /// <param name="lvalue"></param>
        /// <returns></returns>
        public HlCommandInfo GetFirstDefiningCommandInfo(SymbolLValue lvalue)
        {
            var defInfo = GetFirstDefinitionInfo(lvalue);

            return ReferenceEquals(defInfo, null) ? null : defInfo.DefiningCommand;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="lvalue"></param>
        ///// <returns></returns>
        //public int GetFirstDefiningCommandInfoID(SymbolLValue lvalue)
        //{
        //    var def_info = GetFirstDefinitionInfo(lvalue);

        //    return ReferenceEquals(def_info, null) ? -1 : def_info.DefiningCommand.CommandInfoID;
        //}


        //public HLOLValueDefinitionInfo GetLastDefinitionInfo(string lvalue_name)
        //{
        //    if (_LValuesDictionary.ContainsKey(lvalue_name))
        //        return _LValuesDictionary[lvalue_name].LastOrDefault();

        //    return null;
        //}

        /// <summary>
        /// Get the information of the last definition for the given l-value
        /// </summary>
        /// <param name="lvalue"></param>
        /// <returns></returns>
        public HlLValueDefinitionInfo GetLastDefinitionInfo(SymbolLValue lvalue)
        {
            return 
                _lValuesDictionary.ContainsKey(lvalue.ObjectName) 
                ? _lValuesDictionary[lvalue.ObjectName].LastOrDefault() 
                : null;
        }

        //public HLOCommandInfo GetLastDefiningCommandInfo(SymbolLValue lvalue)
        //{
        //    var def_info = GetLastDefinitionInfo(lvalue);

        //    return ReferenceEquals(def_info, null) ? null : def_info.DefiningCommand;
        //}

        //public IEnumerable<HLOLValueDefinitionInfo> GetDefinitions(string lvalue_name)
        //{
        //    if (_LValuesDictionary.ContainsKey(lvalue_name))
        //        return _LValuesDictionary[lvalue_name];

        //    return Enumerable.Empty<HLOLValueDefinitionInfo>();
        //}

        //public IEnumerable<HLOLValueDefinitionInfo> GetDefinitions(SymbolLValue lvalue)
        //{
        //    if (_LValuesDictionary.ContainsKey(lvalue.ObjectName))
        //        return _LValuesDictionary[lvalue.ObjectName];

        //    return Enumerable.Empty<HLOLValueDefinitionInfo>();
        //}

        //public IEnumerable<HLOLValueDefinitionInfo> GetDefinitions()
        //{
        //    foreach (var pair in _LValuesDictionary)
        //        foreach (var lvalue_def in pair.Value)
        //            yield return lvalue_def;
        //}

        /// <summary>
        /// Get all definitions information for all l-values having more than one definition in the table. The first
        /// definition for each of these l-values is not returned but only the follwoing ones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HlLValueDefinitionInfo> GetNonSsaFormLValuesDefinitionsInfo()
        {
            var lvalueDefListList = 
                _lValuesDictionary
                .Select(pair => pair.Value)
                .Where(lvalueDefList => lvalueDefList.Count > 1);

            foreach (var lvalueDefList in lvalueDefListList)
                for (var i = 1; i < lvalueDefList.Count; i++)
                    yield return lvalueDefList[i];
        }

        //public int GetDefinitionsCount(SymbolLValue lvalue)
        //{
        //    if (_LValuesDictionary.ContainsKey(lvalue.ObjectName))
        //        return _LValuesDictionary[lvalue.ObjectName].Count;

        //    return 0;
        //}

        /// <summary>
        /// True if each l-value in this table has only one definig command
        /// </summary>
        public bool IsSsaForm
        {
            get
            {
                //For all l-values get max count of all definitions
                var maxCount =
                    _lValuesDictionary.Max(
                        pair => ReferenceEquals(pair.Value, null) ? 0 : pair.Value.Count
                    );

                //SSA form requires that each l-value is defined at most once.
                return maxCount <= 1;
            }
        }
    }
}
