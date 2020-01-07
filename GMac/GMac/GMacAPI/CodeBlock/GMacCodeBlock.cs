using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.GMacAPI.Binding;
using GMac.GMacAST;
using GMac.GMacAST.Expressions;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel;

namespace GMac.GMacAPI.CodeBlock
{
    /// <summary>
    /// This class holds the final intermediate low-level optimized computations generated from a macro after
    /// binding its parameters to variables or constants. The terget code is generated based on the information
    /// in this class
    /// </summary>
    public sealed class GMacCodeBlock
    {
        private int _lowLevelVarIndex;


        /// <summary>
        /// The inout variables defined in this block
        /// </summary>
        private readonly List<GMacCbInputVariable> _inputVariables 
            = new List<GMacCbInputVariable>();

        /// <summary>
        /// The computed variables (temp and output) defined in this block
        /// </summary>
        internal List<GMacCbComputedVariable> ComputedVariables 
            = new List<GMacCbComputedVariable>();

        /// <summary>
        /// All the variables (input, output and temp) of this block stored by their low-level variable name
        /// </summary>
        internal readonly Dictionary<string, GMacCbVariable> VariablesDictionary =
            new Dictionary<string, GMacCbVariable>();

        /// <summary>
        /// All the input and output variables of this block stored by their associated value access name
        /// </summary>
        private readonly Dictionary<string, IGMacCbParameterVariable> _parametersDictionary =
            new Dictionary<string, IGMacCbParameterVariable>();


        /// <summary>
        /// The base macro for this code block
        /// </summary>
        public AstMacro BaseMacro { get; private set; }

        /// <summary>
        /// The variables (input, output and temp) of this block
        /// </summary>
        public IEnumerable<GMacCbVariable> Variables 
            => _inputVariables.Concat(ComputedVariables.Cast<GMacCbVariable>());

        /// <summary>
        /// The input variables of this code block
        /// </summary>
        public IEnumerable<GMacCbInputVariable> InputVariables 
            => _inputVariables;

        /// <summary>
        /// The number of input and computed variables of this block
        /// </summary>
        public int VariablesCount
            => _inputVariables.Count + ComputedVariables.Count;

        /// <summary>
        /// The number of input variables of this code block
        /// </summary>
        public int InputVariablesCount 
            => _inputVariables.Count;

        /// <summary>
        /// The input variables used in computations in this block
        /// </summary>
        public IEnumerable<GMacCbInputVariable> UsedInputVariables 
            => _inputVariables.Where(inputVar => inputVar.IsUsed);

        /// <summary>
        /// The input variables not used in any computations in this block
        /// </summary>
        public IEnumerable<GMacCbInputVariable> NotUsedInputVariables 
            => _inputVariables.Where(inputVar => ! inputVar.IsUsed);

        /// <summary>
        /// The temporary (intermediate) variables used in this block
        /// </summary>
        public IEnumerable<GMacCbTempVariable> TempVariables 
            => ComputedVariables
                .Where(item => item is GMacCbTempVariable)
                .Cast<GMacCbTempVariable>();

        /// <summary>
        /// The output variables of this block
        /// </summary>
        public IEnumerable<GMacCbOutputVariable> OutputVariables 
            => ComputedVariables
                .Where(item => item is GMacCbOutputVariable)
                .Cast<GMacCbOutputVariable>();

        /// <summary>
        /// The output variables with constant values
        /// </summary>
        public IEnumerable<GMacCbOutputVariable> ConstantOutputVariables 
            => OutputVariables.Where(outputVar => outputVar.IsConstant);

        /// <summary>
        /// The output variables with non-zero constant values
        /// </summary>
        public IEnumerable<GMacCbOutputVariable> ConstantNonZeroOutputVariables 
            => OutputVariables.Where(outputVar => outputVar.IsNonZeroConstant);

        /// <summary>
        /// The output variables depending on temp or input variables
        /// </summary>
        public IEnumerable<GMacCbOutputVariable> NonConstantOutputVariables 
            => OutputVariables.Where(outputVar => outputVar.IsConstant == false);

        /// <summary>
        /// The output variables having non-zero constant values or depending on temp or input variables
        /// </summary>
        public IEnumerable<GMacCbOutputVariable> NonZeroOutputVariables 
            => OutputVariables.Where(outputVar => outputVar.IsNonZero);

        /// <summary>
        /// The output variables having zero values
        /// </summary>
        public IEnumerable<GMacCbOutputVariable> ZeroOutputVariables 
            => OutputVariables.Where(outputVar => outputVar.IsZero);

        /// <summary>
        /// Returns a list of input and output varfiables in this code block
        /// </summary>
        public IEnumerable<IGMacCbParameterVariable> ParameterVariables 
            => _parametersDictionary.Count == 0 
                ? _inputVariables.Cast<IGMacCbParameterVariable>().Concat(OutputVariables) 
                : _parametersDictionary.Values;

        /// <summary>
        /// The temporary (intermediate) variables introduced during common sub-expression elimination
        /// optimiation stage of this block
        /// </summary>
        public IEnumerable<GMacCbTempVariable> TempSubExpressions 
            => TempVariables.Where(item => item.IsFactoredSubExpression);

        /// <summary>
        /// The temporary (intermediate) variables not resulting from common sub-expression elimination
        /// optimiation stage of this block
        /// </summary>
        public IEnumerable<GMacCbTempVariable> TempNonSubExpressions 
            => TempVariables.Where(item => !item.IsFactoredSubExpression);

        /// <summary>
        /// The maximum number of temporary target variables required for the computations in this block
        /// </summary>
        public int TargetTempVarsCount 
            => TempVariables.Any()
                ? TempVariables.Max(item => item.NameIndex) + 1
                : 0;


        internal GMacCodeBlock(AstMacro baseMacro)
        {
            BaseMacro = baseMacro;
        }


        /// <summary>
        /// Adds an input variable to this block
        /// </summary>
        /// <param name="inputVar"></param>
        internal void AddInputVariable(GMacCbInputVariable inputVar)
        {
            _inputVariables.Add(inputVar);
        }

        /// <summary>
        /// Used during initialization of block to setup the _lowLevelVarIndex initial value  to be used for
        /// adding new temp variables as needed during optimization stages
        /// </summary>
        /// <param name="lastVarName"></param>
        internal void SetLastUsedVarName(string lastVarName)
        {
            _lowLevelVarIndex = 
                1 + Convert.ToInt32(lastVarName.Substring(LowLevelUtils.LlVarNamePrefix.Length), 16);
        }

        /// <summary>
        /// Create a new name for a new temp variable
        /// </summary>
        /// <returns></returns>
        internal string GetNewVarName()
        {
            _lowLevelVarIndex++;

            return LowLevelUtils.LlVarNamePrefix + _lowLevelVarIndex.ToString("X4");
        }

        /// <summary>
        /// Update the computation order index of each computed variable in the block
        /// </summary>
        internal void UpdateComputationOrder()
        {
            for (var i = 0; i < ComputedVariables.Count; i++)
                ComputedVariables[i].ComputationOrder = i;
        }

        /// <summary>
        /// Fill the internal ParametersDictionary with input and output variables
        /// </summary>
        internal void UpdateParametersDictionary()
        {
            _parametersDictionary.Clear();

            var paramVarsList = 
                _inputVariables
                .Cast<IGMacCbParameterVariable>()
                .Concat(OutputVariables)
                .ToArray();

            foreach (var paramVar in paramVarsList)
                _parametersDictionary.Add(
                    paramVar.ValueAccess.ValueAccessName, paramVar
                    );
        }


        /// <summary>
        /// True if the given name is for an input variable
        /// </summary>
        /// <param name="lowLevelVarName"></param>
        /// <returns></returns>
        public bool IsInputVariable(string lowLevelVarName)
        {
            return _inputVariables.Exists(inputVar => inputVar.LowLevelName == lowLevelVarName);
        }

        /// <summary>
        /// Test if the given primitive macro parameter is used as a variable in the code block
        /// </summary>
        /// <param name="paramValueAccess"></param>
        /// <returns></returns>
        public bool IsParameterVariable(AstDatastoreValueAccess paramValueAccess)
        {
            var name = paramValueAccess.ValueAccessName;

            if (_parametersDictionary.Count > 0)
                return _parametersDictionary.ContainsKey(name);

            return ParameterVariables.FirstOrDefault(
                item => item.ValueAccess.ValueAccessName == name
                ) != null;
        }

        /// <summary>
        /// Try to get a temporary variable by its low-level name
        /// </summary>
        /// <param name="lowLevelVarName"></param>
        /// <param name="tempVar"></param>
        /// <returns></returns>
        public bool TryGetTempVariable(string lowLevelVarName, out GMacCbTempVariable tempVar)
        {
            if (VariablesDictionary.Count == 0)
            {
                tempVar = TempVariables.FirstOrDefault(item => item.LowLevelName == lowLevelVarName);
                return (tempVar != null);
            }

            if (VariablesDictionary.TryGetValue(lowLevelVarName, out var result) == false)
            {
                tempVar = null;
                return false;
            }

            tempVar = result as GMacCbTempVariable;
            return (tempVar != null);
        }

        /// <summary>
        /// Try to get an input variable by its low-level name
        /// </summary>
        /// <param name="lowLevelVarName"></param>
        /// <param name="inputVar"></param>
        /// <returns></returns>
        public bool TryGetInputVariable(string lowLevelVarName, out GMacCbInputVariable inputVar)
        {
            if (VariablesDictionary.Count == 0)
            {
                inputVar = InputVariables.FirstOrDefault(item => item.LowLevelName == lowLevelVarName);
                return (inputVar != null);
            }

            if (VariablesDictionary.TryGetValue(lowLevelVarName, out var result) == false)
            {
                inputVar = null;
                return false;
            }

            inputVar = result as GMacCbInputVariable;
            return (inputVar != null);
        }

        /// <summary>
        /// Try to get an input variable by its low-level name
        /// </summary>
        /// <param name="lowLevelVarName"></param>
        /// <param name="outputVar"></param>
        /// <returns></returns>
        public bool TryGetOutputVariable(string lowLevelVarName, out GMacCbOutputVariable outputVar)
        {
            if (VariablesDictionary.Count == 0)
            {
                outputVar = OutputVariables.FirstOrDefault(item => item.LowLevelName == lowLevelVarName);
                return (outputVar != null);
            }

            if (VariablesDictionary.TryGetValue(lowLevelVarName, out var result) == false)
            {
                outputVar = null;
                return false;
            }

            outputVar = result as GMacCbOutputVariable;
            return (outputVar != null);
        }

        /// <summary>
        /// Try to get a variable by its low-level name
        /// </summary>
        /// <param name="lowLevelVarName"></param>
        /// <param name="blockVar"></param>
        /// <returns></returns>
        public bool TryGetVariable(string lowLevelVarName, out GMacCbVariable blockVar)
        {
            if (VariablesDictionary.Count > 0) 
                return VariablesDictionary.TryGetValue(lowLevelVarName, out blockVar);

            blockVar = Variables.FirstOrDefault(item => item.LowLevelName == lowLevelVarName);
            
            return (blockVar != null);
        }

        /// <summary>
        /// Try to get a parameter variable by its associated datastore value access
        /// </summary>
        /// <param name="paramValueAccess"></param>
        /// <param name="paramVar"></param>
        /// <returns></returns>
        public bool TryGetParameterVariable(AstDatastoreValueAccess paramValueAccess, out IGMacCbParameterVariable paramVar)
        {
            var name = paramValueAccess.ValueAccessName;

            if (_parametersDictionary.Count > 0)
                return _parametersDictionary.TryGetValue(name, out paramVar);

            paramVar = ParameterVariables.FirstOrDefault(
                item => item.ValueAccess.ValueAccessName == name
                );

            return (paramVar != null);
        }

        /// <summary>
        /// Try to get an input parameter variable by its associated datastore value access
        /// </summary>
        /// <param name="paramValueAccess"></param>
        /// <param name="inputParamVar"></param>
        /// <returns></returns>
        public bool TryGetInputParameterVariable(AstDatastoreValueAccess paramValueAccess, out GMacCbInputVariable inputParamVar)
        {
            inputParamVar = null;

            if (!TryGetParameterVariable(paramValueAccess, out var paramVar))
                return false;

            inputParamVar = paramVar as GMacCbInputVariable;
            return inputParamVar != null;
        }

        /// <summary>
        /// Find all primitive parameters in this code block that are sub-components of the given
        /// macro parameter value access component
        /// </summary>
        /// <param name="paramValueAccess"></param>
        /// <returns></returns>
        public IEnumerable<IGMacCbParameterVariable> GetParameters(AstDatastoreValueAccess paramValueAccess)
        {
            var result = new List<IGMacCbParameterVariable>();

            if (paramValueAccess.IsNullOrInvalid()) return result;

            var name = paramValueAccess.ValueAccessName;

            //A single parameter is to be found
            if (paramValueAccess.IsPrimitive)
            {
                var item = ParameterVariables.FirstOrDefault(
                    p => p.ValueAccessName == name
                    );

                if (ReferenceEquals(item, null) == false)
                    result.Add(item);

                return result;
            }

            //Partial multivectors require special treatment
            if (paramValueAccess.IsPartialMultivector)
            {
                var primitiveValueAccessList = paramValueAccess.ExpandAll();

                foreach (var primitiveValueAccess in primitiveValueAccessList)
                {
                    if (TryGetParameterVariable(primitiveValueAccess, out var paramVar))
                        result.Add(paramVar);
                }

                return result;
            }

            //All other cases can be searched by name
            name = name + ".";

            result.AddRange(
                ParameterVariables.Where(
                    p => p.ValueAccessName.IndexOf(name, StringComparison.Ordinal) == 0
                    )
                );

            return result;
        }

        /// <summary>
        /// Find all primitive parameters in this code block that are sub-components of the given
        /// macro parameter value access component and return their associated value access
        /// </summary>
        /// <param name="paramValueAccess"></param>
        /// <returns></returns>
        public IEnumerable<AstDatastoreValueAccess> GetParametersValueAccess(AstDatastoreValueAccess paramValueAccess)
        {
            return GetParameters(paramValueAccess).Select(p => p.ValueAccess);
        }

        /// <summary>
        /// Find all primitive parameters in this code block that are sub-components of the given
        /// macro parameter value access component and return their associated value access names
        /// </summary>
        /// <param name="paramValueAccess"></param>
        /// <returns></returns>
        public IEnumerable<string> GetParametersValueAccessNames(AstDatastoreValueAccess paramValueAccess)
        {
            return GetParameters(paramValueAccess).Select(p => p.ValueAccessName);
        }

        /// <summary>
        /// Constructs a binding pattern from the parameters actual values.
        /// </summary>
        /// <param name="paramValueAccess"></param>
        /// <returns></returns>
        public IGMacTypedBinding GetParameterBindingPattern(AstDatastoreValueAccess paramValueAccess)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Constructs a binding pattern from the output parameters actual values. This may be used
        /// to find the smallest pattern that can contain the output parameters for the givin inputs binding
        /// </summary>
        /// <returns></returns>
        public IGMacTypedBinding GetOutputBindingPattern()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Constructs a macro binding pattern from the macro parameters actual values.
        /// </summary>
        /// <returns></returns>
        public GMacMacroTreeBinding GetMacroBindingPattern()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create and populate a dependency graph for the variables in this code block
        /// </summary>
        /// <returns></returns>
        public GMacCbDependencyGraph GetDependencyGraph()
        {
            var depGraph = new GMacCbDependencyGraph(this);

            depGraph.Populate();

            return depGraph;
        }

        /// <summary>
        /// The statistics related to the variables and computations in this block
        /// </summary>
        public Dictionary<string, string> GetStatistics()
        {
            var stats = new Dictionary<string, string>();

            var inputVarsTotalCount = _inputVariables.Count;
            var inputVarsUsedCount = _inputVariables.Count(inputVar => inputVar.IsUsed);
            var inputVarsUnUsedCount = inputVarsTotalCount - inputVarsUsedCount;

            stats.Add("Used Input Variables: ", inputVarsUsedCount.ToString());
            stats.Add("Unused Input Variables: ", inputVarsUnUsedCount.ToString());
            stats.Add("Total Input Variables: ", inputVarsTotalCount.ToString());

            var tempVarsCount = TempVariables.Count();
            var tempVarsSubExprCount = TempVariables.Count(item => item.IsFactoredSubExpression);
            var tempVarsNonSubExprCount = tempVarsCount - tempVarsSubExprCount;

            stats.Add("Common Subexpressions Temp Variables: ", tempVarsSubExprCount.ToString());
            stats.Add("Generated Temp Variables: ", tempVarsNonSubExprCount.ToString());
            stats.Add("Total Temp Variables: ", tempVarsCount.ToString());

            stats.Add("Total Output Variables: ", OutputVariables.Count().ToString());

            stats.Add("Total Computed Variables: ", ComputedVariables.Count.ToString());

            stats.Add("Target Temp Variables: ", TargetTempVarsCount.ToString());

            var computationsCountTotal =
                ComputedVariables
                .Select(computedVar => computedVar.RhsExpr.ComputationsCount)
                .Sum();

            var computationsCountAverage =
                computationsCountTotal / (double)ComputedVariables.Count;

            stats.Add("Avg. Computations Count: ", computationsCountAverage.ToString("0.000"));
            stats.Add("Total Computations Count: ", computationsCountTotal.ToString());

            var memreadsCountTotal =
                ComputedVariables
                .Select(computedVar => computedVar.RhsVariablesCount)
                .Sum();

            var memreadsCountAverage =
                memreadsCountTotal / (double)ComputedVariables.Count;

            stats.Add("Avg. Memory Reads: ", memreadsCountAverage.ToString("0.000"));
            stats.Add("Total Memory Reads: ", memreadsCountTotal.ToString());

            return stats;
        }

        /// <summary>
        /// The statistics related to the variables and computations in this block as a single string
        /// </summary>
        /// <returns></returns>
        public string GetStatisticsReport()
        {
            var s = new StringBuilder();

            var inputVarsTotalCount = _inputVariables.Count;
            var inputVarsUsedCount = _inputVariables.Count(inputVar => inputVar.IsUsed);
            var inputVarsUnUsedCount = inputVarsTotalCount - inputVarsUsedCount;

            s.Append("Input Variables: ")
                .Append(inputVarsUsedCount)
                .Append(" used, ")
                .Append(inputVarsUnUsedCount)
                .Append(" not used, ")
                .Append(inputVarsTotalCount)
                .AppendLine(" total.")
                .AppendLine();

            var tempVarsCount = TempVariables.Count();
            var tempVarsSubExprCount = TempVariables.Count(item => item.IsFactoredSubExpression);
            var tempVarsNonSubExprCount = tempVarsCount - tempVarsSubExprCount;

            s.Append("Temp Variables: ")
                .Append(tempVarsSubExprCount)
                .Append(" sub-expressions, ")
                .Append(tempVarsNonSubExprCount)
                .Append(" generated temps, ")
                .Append(tempVarsCount)
                .AppendLine(" total.")
                .AppendLine();

            if (TargetTempVarsCount > 0)
                s.Append("Target Temp Variables: ")
                    .Append(TargetTempVarsCount)
                    .AppendLine(" total.")
                    .AppendLine();

            var outputVarsCount = OutputVariables.Count();

            s.Append("Output Variables: ")
                .Append(outputVarsCount)
                .AppendLine(" total.")
                .AppendLine();

            var computationsCountTotal =
                ComputedVariables
                .Select(computedVar => computedVar.RhsExpr.ComputationsCount)
                .Sum();
            
            var computationsCountAverage = 
                computationsCountTotal/(double) ComputedVariables.Count;

            s.Append("Computations: ")
                .Append(computationsCountAverage)
                .Append(" average, ")
                .Append(computationsCountTotal)
                .AppendLine(" total.")
                .AppendLine();

            var memreadsCountTotal =
                ComputedVariables
                .Select(computedVar => computedVar.RhsVariablesCount)
                .Sum();

            var memreadsCountAverage =
                memreadsCountTotal / (double)ComputedVariables.Count;

            s.Append("Memory Reads: ")
                .Append(memreadsCountAverage)
                .Append(" average, ")
                .Append(memreadsCountTotal)
                .AppendLine(" total.")
                .AppendLine();

            s.Append("Memory Writes: ")
                .Append(ComputedVariables.Count)
                .AppendLine(" total.")
                .AppendLine();

            return s.ToString();
        }

        public override string ToString()
        {
            var s = new StringBuilder();

            s.Append(GetStatisticsReport());

            s.AppendLine("BEGIN");

            foreach (var inputVar in _inputVariables)
                s.Append("   ").Append(inputVar);

            s.AppendLine();

            foreach (var computedVar in ComputedVariables)
                s.Append("   ").AppendLine(computedVar.ToString());
            
            s.AppendLine("END");

            return s.ToString();
        }
    }
}
