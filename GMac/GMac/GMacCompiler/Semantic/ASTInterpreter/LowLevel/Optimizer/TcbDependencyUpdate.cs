using System.Collections.Generic;
using System.Linq;
using GMac.GMacAPI.CodeBlock;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Optimizer
{
    internal sealed class TcbDependencyUpdate : TcbProcessor
    {
        internal static GMacCodeBlock Process(GMacCodeBlock codeBlock)
        {
            var processor = new TcbDependencyUpdate(codeBlock);

            processor.BeginProcessing();

            return codeBlock;
        }


        //private readonly Dictionary<string, TlVariable> _variablesDictionary = 
        //    new Dictionary<string, TlVariable>();


        private TcbDependencyUpdate(GMacCodeBlock codeBlock)
            : base(codeBlock)
        {
            
        }


        /// <summary>
        /// Find the inout and temp variables used in the RHS expression of the given computed variable
        /// </summary>
        /// <param name="computedVar"></param>
        /// <returns></returns>
        private IEnumerable<IGMacCbRhsVariable> GetRhsVariablesInfo(GMacCbComputedVariable computedVar)
        {
            return
                computedVar
                .RhsExpr
                .GetLowLevelVariablesNames()
                .Select(item => CodeBlock.VariablesDictionary[item] as IGMacCbRhsVariable);
        }

        private void AddUserVariable(IGMacCbRhsVariable rhsVar, GMacCbComputedVariable computedVar)
        {
            var inputVar = rhsVar as GMacCbInputVariable;

            if (ReferenceEquals(inputVar, null) == false)
            {
                inputVar.AddUserVariable(computedVar);

                return;
            }

            var tempVar = rhsVar as GMacCbTempVariable;

            if (ReferenceEquals(tempVar, null)) return;

            tempVar.AddUserVariable(computedVar);
        }

        protected override void BeginProcessing()
        {
            //Clear dependency lists for all variables
            CodeBlock.VariablesDictionary.Clear();

            foreach (var variable in CodeBlock.Variables)
            {
                variable.ClearDependencyData();
                CodeBlock.VariablesDictionary.Add(variable.LowLevelName, variable);
            }

            //Fill dependency lists of all variables
            foreach (var computedVar in CodeBlock.ComputedVariables)
            {
                //Find the variables used in the RHS computation of this computed variable
                var rhsVarsList = GetRhsVariablesInfo(computedVar);

                //Iterate over all RHS variables of this computed variable
                foreach (var rhsVar in rhsVarsList)
                {
                    //Add the RHS variable to the list of RHS variables of this computed variable
                    computedVar.AddRhsVariable(rhsVar);

                    //Add this computed variables to the list of variables that depend on the RHS variable
                    AddUserVariable(rhsVar, computedVar);
                }

                //Update the maximum computation level for this computed variable.
                computedVar.MaxComputationLevel =
                    (computedVar.RhsVariablesCount == 0)
                    ? 0
                    : computedVar.RhsVariables.Max(item => item.MaxComputationLevel + 1);
            }

            //Update ComputationOrder property for each computed variable in the block
            CodeBlock.UpdateComputationOrder();
        }
    }
}
