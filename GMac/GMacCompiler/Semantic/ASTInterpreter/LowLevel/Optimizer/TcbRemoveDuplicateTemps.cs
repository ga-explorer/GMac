using System.Collections.Generic;
using System.Linq;
using GMac.GMacAPI.CodeBlock;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Optimizer
{
    internal sealed class TcbRemoveDuplicateTemps : TcbProcessor
    {
        internal static GMacCodeBlock Process(GMacCodeBlock codeBlock)
        {
            var processor = new TcbRemoveDuplicateTemps(codeBlock);

            processor.BeginProcessing();

            return codeBlock;
        }


        private readonly Dictionary<string, List<GMacCbTempVariable>> _rhsExprUseDictionary = 
            new Dictionary<string, List<GMacCbTempVariable>>();

        
        private TcbRemoveDuplicateTemps(GMacCodeBlock codeBlock)
            : base(codeBlock)
        {
        }


        private void AddRhsExprUse(GMacCbTempVariable tempVar)
        {
            var rhsExprText = tempVar.RhsExpr.ToString();

            List<GMacCbTempVariable> rhsExprUseList;

            if (_rhsExprUseDictionary.TryGetValue(rhsExprText, out rhsExprUseList) == false)
            {
                rhsExprUseList = new List<GMacCbTempVariable>();

                _rhsExprUseDictionary.Add(rhsExprText, rhsExprUseList);
            }

            rhsExprUseList.Add(tempVar);
        }

        protected override void BeginProcessing()
        {
            //Fill dictionary of RHS expressions uses
            foreach (var tempVar in CodeBlock.TempVariables)
                AddRhsExprUse(tempVar);

            //Select sub-expressions with multiple uses
            var selectedLists =
                _rhsExprUseDictionary
                .Select(pair => pair.Value)
                .Where(list => list.Count > 1);

            //A list for marking duplicate temp variables to be deleted from code block
            var removedTempsList = new List<GMacCbTempVariable>();

            foreach (var tempVarList in selectedLists)
            {
                //The temp variable to keep
                var tempVarNew = tempVarList[0];

                //Iterate over all duplicate temp variables to be replaced and removed
                foreach (var tempVarOld in tempVarList.Skip(1))
                {
                    //Mark the duplicate for removal
                    removedTempsList.Add(tempVarOld);

                    //Replace the duplicate temp by the temp variable to keep
                    foreach (var computedVar in tempVarOld.UserVariables)
                        computedVar.ReplaceRhsTempVariable(tempVarOld, tempVarNew);
                }
            }

            //Remove all duplicate temps from code block
            for (var j = removedTempsList.Count - 1; j >= 0; j--)
                CodeBlock.ComputedVariables.RemoveAt(removedTempsList[j].ComputationOrder);

            //Update dependency information if needed
            if (removedTempsList.Count > 0)
                TcbDependencyUpdate.Process(CodeBlock);
        }
    }
}
