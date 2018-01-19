using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.AST.Extensions;
using GMac.GMacCompiler.Symbolic;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Expression.ValueAccess;
using IronyGrammars.Semantic.Symbol;
using IronyGrammars.Semantic.Type;
using SymbolicInterface.Mathematica.Expression;
using Wolfram.NETLink;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Generator
{
    /// <summary>
    /// The data table containing all low-level items during low-level code generation and optimization
    /// </summary>
    internal sealed class LlDataTable
    {
        /// <summary>
        /// Parent GMac DSL
        /// </summary>
        public GMacAst GMacRootAst { get; }

        /// <summary>
        /// Low level variables index used for automatic naming of new variables
        /// </summary>
        private int _llVarIndex;

        /// <summary>
        /// Low level commands evaluation order counter
        /// </summary>
        private int _llVarEvaluationOrder;

        /// <summary>
        /// Dictionary for holding refernces to low-level commands by their corresponding high-level LHS value access variables names.
        /// Note: Not all commands result from high-level value access so not all low-level commands are accessible from this
        /// dictionary
        /// </summary>
        private readonly Dictionary<string, LlDataItem> _hlDictionary = new Dictionary<string, LlDataItem>();

        /// <summary>
        /// Dictionary for holding references to low-level commands by their low-level LHS variables' names. All low-level
        /// commands are accessible from this dictionary
        /// </summary>
        private readonly Dictionary<string, LlDataItem> _llDictionary = new Dictionary<string, LlDataItem>();


        public LlDataTable(GMacAst dsl)
        {
            GMacRootAst = dsl;
        }


        /// <summary>
        /// Retrieve a low-level item by its low-level name
        /// </summary>
        /// <param name="llName"></param>
        /// <returns></returns>
        public LlDataItem GetItemByName(string llName)
        {
            return _llDictionary[llName];
        }

        /// <summary>
        /// Get the name for a new item in the table using the low level variables index member
        /// </summary>
        /// <returns></returns>
        public string GetNewDataItemName()
        {
            _llVarIndex++;

            return LowLevelUtils.LlVarNamePrefix + _llVarIndex.ToString("X4");
        }

        /// <summary>
        /// Attach the given item to the public dictionaries of the table
        /// </summary>
        /// <param name="llDataItem"></param>
        /// <returns></returns>
        private LlDataItem AttachDataItem(LlDataItem llDataItem)
        {
            _hlDictionary.Add(llDataItem.HlItemName, llDataItem);

            _llDictionary.Add(llDataItem.ItemName, llDataItem);

            return llDataItem;
        }

        /// <summary>
        /// Clear all public table data
        /// </summary>
        public void Clear()
        {
            _llVarIndex = 0;
            _llVarEvaluationOrder = 0;
            _hlDictionary.Clear();
            _llDictionary.Clear();
        }

        /// <summary>
        /// Define a collection of variable low-level input items based on the given input macro parameter
        /// </summary>
        /// <param name="param"></param>
        /// <param name="testValueExpr"></param>
        public void DefineVariableInputParameter(SymbolProcedureParameter param)//, Expr testValueExpr = null)
        {
            DefineVariableInputParameter(LanguageValueAccess.Create(param));//, testValueExpr);
        }

        /// <summary>
        /// Define a collection of variable low-level input items based on the given input macro parameter value access
        /// </summary>
        /// <param name="hlValueAccess"></param>
        /// <param name="testValueExpr"></param>
        public void DefineVariableInputParameter(LanguageValueAccess hlValueAccess)//, Expr testValueExpr = null)
        {
            if (hlValueAccess.ExpressionType is TypePrimitive)
            {
                AttachDataItem(
                    LlDataItem.CreateVariableInputParameter(GetNewDataItemName(), hlValueAccess)//, testValueExpr)
                    );
            }
            else
            {
                var valueAccessList = hlValueAccess.ExpandAll();

                foreach (var childValueAccess in valueAccessList)
                    AttachDataItem(
                        LlDataItem.CreateVariableInputParameter(GetNewDataItemName(), childValueAccess)//, testValueExpr)
                        );
            }
        }

        /// <summary>
        /// Define a single constant low-level input item based on the given primitive input macro parameter value access
        /// </summary>
        /// <param name="hlValueAccess"></param>
        /// <param name="assignedValue"></param>
        /// <returns></returns>
        public LlDataItem DefineConstantInputParameter(LanguageValueAccess hlValueAccess, ILanguageValuePrimitive assignedValue)
        {
            if (!(hlValueAccess.ExpressionType is TypePrimitive))
                throw new InvalidOperationException("The given parameter is not of primitive type");

            if (!(hlValueAccess.ExpressionType.IsSameType(assignedValue.ExpressionType)))
                throw new InvalidOperationException("The given constant value must be of type " + hlValueAccess.ExpressionType.TypeSignature);

            var llAssignedValue = assignedValue.ToScalarValue();

            var llDataItem = LlDataItem.CreateConstantInputParameter(GetNewDataItemName(), hlValueAccess, llAssignedValue);

            return AttachDataItem(llDataItem);
        }

        /// <summary>
        /// Define a collection of variable low-level output items based on the given output macro parameter
        /// </summary>
        /// <param name="param"></param>
        public void DefineOutputParameter(SymbolProcedureParameter param)
        {
            DefineOutputParameter(LanguageValueAccess.Create(param));
        }

        /// <summary>
        /// Define a collection of variable low-level output items based on the given output macro parameter value access
        /// </summary>
        /// <param name="hlValueAccess"></param>
        public void DefineOutputParameter(LanguageValueAccess hlValueAccess)
        {
            if (hlValueAccess.ExpressionType is TypePrimitive)
            {
                AttachDataItem(
                    LlDataItem.CreateOutputParameter(GetNewDataItemName(), hlValueAccess)
                    );
            }
            else
            {
                var valueAccessList = hlValueAccess.ExpandAll();

                foreach (var childValueAccess in valueAccessList)
                    AttachDataItem(
                        LlDataItem.CreateOutputParameter(GetNewDataItemName(), childValueAccess)
                        );
            }
        }

        /// <summary>
        /// Define a single low-level temp item based on the given primitive local variable value access
        /// </summary>
        /// <param name="hlValueAccess"></param>
        /// <param name="assignedValue"></param>
        /// <returns></returns>
        private LlDataItem DefineTemp(LanguageValueAccess hlValueAccess, ValuePrimitive<MathematicaScalar> assignedValue)
        {
            var llDataItem = LlDataItem.CreateTemp(GetNewDataItemName(), hlValueAccess, assignedValue, _llVarEvaluationOrder++);

            return AttachDataItem(llDataItem);
        }


        public IEnumerable<LlDataItem> Inputs
        {
            get 
            {
                return
                    _hlDictionary
                    .Where(pair => pair.Value.IsInput && pair.Value.IsDeleted == false)
                    .Select(pair => pair.Value);
            }
        }

        public IEnumerable<LlDataItem> NonInputs
        {
            get 
            {
                return
                    _llDictionary
                    .Where(pair => !pair.Value.IsInput && pair.Value.IsDeleted == false)
                    .Select(pair => pair.Value);
            }
        }

        public IEnumerable<LlDataItem> ComputedByEvaluationOrder
        {
            get
            {
                return
                    _llDictionary
                    .Values
                    .Where(item => (item.IsOutput || item.IsVariableTemp) && item.IsDeleted == false)
                    .OrderBy(item => item.EvaluationOrder)
                    .ThenBy(item => item.ItemName);
            }
        }

        public IEnumerable<LlDataItem> NonInputVariablesByEvaluationOrder
        {
            get
            {
                return
                    _llDictionary
                    .Values
                    .Where(item => !item.IsInput && item.IsVariable && item.IsDeleted == false)
                    .OrderBy(item => item.EvaluationOrder)
                    .ThenBy(item => item.ItemName);
            }
        }

        public IEnumerable<LlDataItem> Outputs
        {
            get 
            {
                return
                    _hlDictionary
                    .Where(pair => pair.Value.IsOutput && pair.Value.IsDeleted == false)
                    .Select(pair => pair.Value);
            }
        }

        public IEnumerable<LlDataItem> Temps
        {
            get 
            {
                return
                    _llDictionary
                    .Where(pair => pair.Value.IsTemp && pair.Value.IsDeleted == false)
                    .Select(pair => pair.Value);
            }
        }

        public IEnumerable<LlDataItem> Variables
        {
            get 
            {
                return
                    _llDictionary
                    .Where(pair => pair.Value.IsVariable && pair.Value.IsDeleted == false)
                    .Select(pair => pair.Value);
            }
        }

        public IEnumerable<LlDataItem> VariableInputs
        {
            get 
            {
                return
                    _hlDictionary
                    .Where(pair => pair.Value.IsVariableInput && pair.Value.IsDeleted == false)
                    .Select(pair => pair.Value);
            }
        }

        public IEnumerable<LlDataItem> VariableTemps
        {
            get 
            {
                return
                    _llDictionary
                    .Where(pair => pair.Value.IsVariableTemp && pair.Value.IsDeleted == false)
                    .Select(pair => pair.Value);
            }
        }

        public IEnumerable<LlDataItem> ConstantInputs
        {
            get 
            {
                return
                    _hlDictionary
                    .Where(pair => pair.Value.IsConstantInput && pair.Value.IsDeleted == false)
                    .Select(pair => pair.Value);
            }
        }

        public IEnumerable<LlDataItem> ConstantTemps
        {
            get 
            {
                return
                    _llDictionary
                    .Where(pair => pair.Value.IsConstantTemp && pair.Value.IsDeleted == false)
                    .Select(pair => pair.Value);
            }
        }


        /// <summary>
        /// Replace the given value by a processed version depending on some GMac compiler options
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static ValuePrimitive<MathematicaScalar> ProcessRhsValue(ValuePrimitive<MathematicaScalar> value)
        {
            //No simplification or sub-expression refactoring required; just return the value without any processing
            if (GMacCompilerOptions.SimplifyLowLevelRhsValues == false)
                return value;

            //Symbolically simplify the value if indicated. This takes a lot of Mathematica processing and slows 
            //compilation but produces target code with better processing performance in some cases
            var finalExpr = SymbolicUtils.Evaluator.Simplify(value.Value.MathExpr);

            return
                ValuePrimitive<MathematicaScalar>.Create(
                    (TypePrimitive)value.ExpressionType, 
                    MathematicaScalar.Create(SymbolicUtils.Cas, finalExpr)
                    );
        }


        /// <summary>
        /// Finds the value of a primitive value access if exists; else returns null
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <returns></returns>
        private ValuePrimitive<MathematicaScalar> ReadRHSPrimitiveValue_ExistingOnly(LanguageValueAccess valueAccess)
        {
            var hlName = valueAccess.GetName();
            LlDataItem dataInfo;

            if (_hlDictionary.TryGetValue(hlName, out dataInfo))
            {
                return ValuePrimitive<MathematicaScalar>.Create(
                    (TypePrimitive)valueAccess.ExpressionType,
                    dataInfo.RhsUsableSymbolicScalar
                    );
            }

            return null;
        }

        /// <summary>
        /// Finds the value of a primitive value access if exists; else returns the default primitive value
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <returns></returns>
        public ValuePrimitive<MathematicaScalar> ReadRhsPrimitiveValue(LanguageValueAccess valueAccess)
        {
            //Try to read the value from the existing table low-level entries
            var result = ReadRHSPrimitiveValue_ExistingOnly(valueAccess);

            //If an entry is found just return the value
            if (ReferenceEquals(result, null) == false)
                return result;

            //If an entry is not found create and return a default value
            if (valueAccess.ExpressionType.IsBoolean())
                ValuePrimitive<MathematicaScalar>.Create(
                    (TypePrimitive)valueAccess.ExpressionType,
                    MathematicaScalar.Create(SymbolicUtils.Cas, "False")
                    );

            return 
                ValuePrimitive<MathematicaScalar>.Create(
                    (TypePrimitive)valueAccess.ExpressionType, 
                    SymbolicUtils.Constants.Zero
                    );
        }

        /// <summary>
        /// Construct the composite value of a multivector value access from the table items. 
        /// If no low-level items are found this method returns null
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <returns></returns>
        private GMacValueMultivector ReadRHSMultivectorValue_ExistingOnly(LanguageValueAccess valueAccess)
        {
            var mv = ReadRhsMultivectorValue(valueAccess);

            //If all coefficients of multivector are zero it's a zero multivector with no existing items in the table
            return 
                mv.MultivectorCoefficients.Count > 0 
                ? mv 
                : null;
        }

        /// <summary>
        /// Construct the composite value of a multivector value access from the table items. 
        /// If no low-level items are found this method returns a zero multivector value
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <returns></returns>
        public GMacValueMultivector ReadRhsMultivectorValue(LanguageValueAccess valueAccess)
        {
            var mv = GMacValueMultivector.CreateZero((GMacFrameMultivector)valueAccess.ExpressionType);

            var valueAccessList = valueAccess.ExpandAll();

            foreach (var childValueAccess in valueAccessList)
            {
                var scalarValue = ReadRHSPrimitiveValue_ExistingOnly(childValueAccess);

                if (ReferenceEquals(scalarValue, null)) 
                    continue;

                var id = ((ValueAccessStepByKey<int>)childValueAccess.LastAccessStep).AccessKey;

                mv.MultivectorCoefficients[id] = scalarValue.Value;
            }

            return mv;
        }

        /// <summary>
        /// Construct the composite value of a structure value access from the table items. 
        /// If no low-level items are found this method returns null
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <returns></returns>
        private ValueStructureSparse ReadRHSStructureValue_ExistingOnly(LanguageValueAccess valueAccess)
        {
            var structValue = ReadRhsStructureValue(valueAccess);

            return 
                structValue.Count > 0 
                ? structValue 
                : null;
        }

        /// <summary>
        /// Construct the composite value of a structure value access from the table items. 
        /// If no low-level items are found this method returns a default structure sparse value
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <returns></returns>
        public ValueStructureSparse ReadRhsStructureValue(LanguageValueAccess valueAccess)
        {
            var structure = (GMacStructure)valueAccess.ExpressionType;

            var structValue = ValueStructureSparse.Create(structure);

            foreach (var dataMember in structure.DataMembers)
            {
                ILanguageValue dataMemberValue = null;
                var childValueAccess = valueAccess.Duplicate().Append(dataMember.ObjectName, dataMember.SymbolType);

                if (dataMember.SymbolType is TypePrimitive)
                    dataMemberValue = ReadRHSPrimitiveValue_ExistingOnly(childValueAccess);
                
                else if (dataMember.SymbolType is GMacFrameMultivector)
                    dataMemberValue = ReadRHSMultivectorValue_ExistingOnly(childValueAccess);

                else if (dataMember.SymbolType is GMacStructure)
                    dataMemberValue = ReadRHSStructureValue_ExistingOnly(childValueAccess);

                if (ReferenceEquals(dataMemberValue, null) == false)
                    structValue[dataMember.ObjectName] = dataMemberValue;
            }

            return structValue;
        }


        /// <summary>
        /// Set the value associated with a primitive local variable or output parameter's low-level item
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="value"></param>
        public void WriteLhsPrimitiveValue(LanguageValueAccess valueAccess, ValuePrimitive<MathematicaScalar> value)
        {
            //For a local variable define a new low-level temp with the given value (perhaps after sub-expression refactoring)
            //Note: Before low-level code generation the macro code must be in SSA form
            if (valueAccess.IsLocalVariable)
            {
                DefineTemp(valueAccess, ProcessRhsValue(value));

                return;
            }
            
            //For output parameters
            if (!valueAccess.IsOutputParameter)
                throw new InvalidOperationException(
                    "Cannot assign a value to a macro input parameter after its definition");
            
            var hlName = valueAccess.GetName();
            LlDataItem llDataItem;

            //If the low-level output parameter is not defined it's value is ignored
            if (_hlDictionary.TryGetValue(hlName, out llDataItem))
                llDataItem.SetAssignedRhsValue(ProcessRhsValue(value), _llVarEvaluationOrder++);
        }

        /// <summary>
        /// Set the values associated with a composite multivector local variable or output parameter's low-level items
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="mvValue"></param>
        public void WriteLhsMultivectorValue(LanguageValueAccess valueAccess, GMacValueMultivector mvValue)
        {
            var valueAccessList = valueAccess.ExpandAll();

            if (valueAccess.IsLocalVariable)
            {
                foreach (var childValueAccess in valueAccessList)
                {
                    var id = ((ValueAccessStepByKey<int>)childValueAccess.LastAccessStep).AccessKey;

                    if (mvValue.MultivectorCoefficients.ContainsKey(id))
                        DefineTemp(childValueAccess, ProcessRhsValue(mvValue[id]));
                }

                return;
            }

            if (!valueAccess.IsOutputParameter)
                throw new InvalidOperationException(
                    "Cannot assign a value to a macro input parameter after its definition");
            
            foreach (var childValueAccess in valueAccessList)
            {
                var id = ((ValueAccessStepByKey<int>)childValueAccess.LastAccessStep).AccessKey;

                var hlName = childValueAccess.GetName();
                LlDataItem llDataItem;

                //If the low-level output parameter is not defined it's value is ignored
                if (_hlDictionary.TryGetValue(hlName, out llDataItem))
                    llDataItem.SetAssignedRhsValue(ProcessRhsValue(mvValue[id]), _llVarEvaluationOrder++);
            }
        }

        /// <summary>
        /// Set the values associated with a composite structure local variable or output parameter's low-level items
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="structValue"></param>
        public void WriteLhsStructureValue(LanguageValueAccess valueAccess, ValueStructureSparse structValue)
        {
            var structure = (GMacStructure)valueAccess.ExpressionType;

            foreach (var dataMember in structure.DataMembers)
            {
                ILanguageValue dataMemberValue;

                if (!structValue.TryGetValue(dataMember.ObjectName, out dataMemberValue)) 
                    continue;
                
                var childValueAccess = valueAccess.Duplicate().Append(dataMember.ObjectName, dataMember.SymbolType);

                if (dataMember.SymbolType is TypePrimitive)
                    WriteLhsPrimitiveValue(childValueAccess, (ValuePrimitive<MathematicaScalar>)dataMemberValue);

                else if (dataMember.SymbolType is GMacFrameMultivector)
                    WriteLhsMultivectorValue(childValueAccess, (GMacValueMultivector)dataMemberValue);

                else if (dataMember.SymbolType is GMacStructure)
                    WriteLhsStructureValue(childValueAccess, (ValueStructureSparse)dataMemberValue);

                else
                    throw new InvalidOperationException();
            }
        }


        /// <summary>
        /// Returns true if there is a variable input low-level item in this table having the given name
        /// </summary>
        /// <param name="llItemName"></param>
        /// <returns></returns>
        public bool IsVariableInput(string llItemName)
        {
            LlDataItem llItem;

            return (_llDictionary.TryGetValue(llItemName, out llItem) && llItem.IsVariableInput);
        }

        /// <summary>
        /// Returns true if there is an output low-level item in this table having the given name
        /// </summary>
        /// <param name="llItemName"></param>
        /// <returns></returns>
        public bool IsOutput(string llItemName)
        {
            LlDataItem llItem;

            return (_llDictionary.TryGetValue(llItemName, out llItem) && llItem.IsOutput);
        }

        public bool IsVariableTemp(string llItemName)
        {
            LlDataItem llItem;

            return (_llDictionary.TryGetValue(llItemName, out llItem) && llItem.IsVariableTemp);
        }

        /// <summary>
        /// Create a copy of this table with all items marked for delete being removed
        /// </summary>
        /// <returns></returns>
        public LlDataTable Duplicate()
        {
            var llDataTable = 
                new LlDataTable(GMacRootAst) 
                { 
                    _llVarEvaluationOrder = _llVarEvaluationOrder, 
                    _llVarIndex = _llVarIndex 
                };

            foreach (var llDataItem in
                _llDictionary.Where(pair => !pair.Value.IsDeleted).Select(pair => pair.Value.Duplicate())
                )
            {
                llDataTable._llDictionary.Add(llDataItem.ItemName, llDataItem);

                if (llDataItem.AssociatedValueAccess != null)
                    llDataTable._hlDictionary.Add(llDataItem.HlItemName, llDataItem);
            }

            return llDataTable;
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            s.AppendLine("Begin low-level items table");

            foreach (var dataItem in Inputs)
                s.Append("   ").AppendLine(dataItem.ToString());

            foreach (var dataItem in ComputedByEvaluationOrder)
                s.Append("   ").AppendLine(dataItem.ToString());

            s.AppendLine("End low-level block");

            return s.ToString();
        }
    }
}
