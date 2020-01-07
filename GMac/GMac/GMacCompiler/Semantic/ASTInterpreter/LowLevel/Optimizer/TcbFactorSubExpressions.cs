using System.Collections.Generic;
using System.Linq;
using GMac.GMacAPI.CodeBlock;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Optimizer
{
    internal sealed class TcbFactorSubExpressions : TcbProcessor
    {
        internal static GMacCodeBlock Process(GMacCodeBlock codeBlock)
        {
            var factoredSubExpressions = new TcbFactorSubExpressions(codeBlock);

            factoredSubExpressions.BeginProcessing();

            return codeBlock;
        }


        private readonly Dictionary<string, GMacCbTempVariable> _tempVarsDictionary = 
            new Dictionary<string, GMacCbTempVariable>();

        private List<GMacCbComputedVariable> _computedVarsList =
            new List<GMacCbComputedVariable>();


        private TcbFactorSubExpressions(GMacCodeBlock codeBlock)
            : base(codeBlock)
        {
        }


        private void AddTempVariable(string rhsExprText, GMacCbTempVariable varInfo)
        {
            varInfo.ComputationOrder = _computedVarsList.Count;

            _tempVarsDictionary.Add(rhsExprText, varInfo);

            _computedVarsList.Add(varInfo);
        }

        private void AddVariable(GMacCbComputedVariable varInfo)
        {
            varInfo.ComputationOrder = _computedVarsList.Count;

            _computedVarsList.Add(varInfo);
        }

        private void AddRhsExpression(GMacCbComputedVariable computedVar)
        {
            var expr = computedVar.RhsExpr;
            var exprText = expr.ToString();
            var tempVar = computedVar as GMacCbTempVariable;

            if (tempVar != null)
            {
                //If this whole expression is already assigned to a temp variable add the temp to the output.
                AddTempVariable(exprText, tempVar);

                return;
            }

            //If this whole expression is instead assigned to an output variabe create a factored expression
            //temp variable and add it to the output with use count 1 and add the original output
            //variable to the output
            if (expr.IsSimpleConstantOrLowLevelVariable() == false && _tempVarsDictionary.ContainsKey(exprText) == false)
            {
                tempVar = new GMacCbTempVariable(CodeBlock.GetNewVarName(), expr, true) { SubExpressionUseCount = 1 };

                AddTempVariable(exprText, tempVar);
            }

            AddVariable(computedVar);
        }

        private void AddSubExpressions(GMacCbComputedVariable computedVar)
        {
            foreach (var subExpr in computedVar.RhsSubExpressions)
            {
                var subExprText = subExpr.ToString();

                if (_tempVarsDictionary.TryGetValue(subExprText, out var tempVar))
                {
                    tempVar.SubExpressionUseCount++;
                }
                else
                {
                    tempVar = new GMacCbTempVariable(CodeBlock.GetNewVarName(), subExpr, true);

                    AddTempVariable(subExprText, tempVar);
                }
            }

            AddRhsExpression(computedVar);
        }

        private void UpdateRelevantComputations()
        {
            var finalList = new List<GMacCbComputedVariable>(_computedVarsList.Count);

            foreach (var computedVar in _computedVarsList)
            {
                if (computedVar is GMacCbOutputVariable)
                {
                    computedVar.ComputationOrder = finalList.Count;
                    finalList.Add(computedVar);
                    continue;
                }

                var tempVar = (GMacCbTempVariable)computedVar;

                if (!tempVar.HasRelevantSubExpression)
                    continue;

                tempVar.ComputationOrder = finalList.Count;
                finalList.Add(tempVar);
            }

            _computedVarsList = finalList;
        }

        private void PropagateSubExpressions()
        {
            var factoredSubExpressionsList =
                _computedVarsList
                .Where(computedVar => computedVar.IsTemp)
                .Cast<GMacCbTempVariable>()
                .Where(computedVar => computedVar.IsFactoredSubExpression)
                .ToArray();

            foreach (var tempVar in factoredSubExpressionsList)
                for (var j = tempVar.ComputationOrder + 1; j < _computedVarsList.Count; j++)
                    _computedVarsList[j].ReplaceRhsSubExpression(tempVar.RhsExpr, tempVar.LowLevelName);
        }

        protected override void BeginProcessing()
        {
            //Add all RHS expressions and subexpressions of all computed variables to a list
            foreach (var computedVar in CodeBlock.ComputedVariables)
                AddSubExpressions(computedVar);

            //Select which temp variables to add to the final computations list
            UpdateRelevantComputations();

            //Replace sub-expressions used multiple times in computations by their temp variables
            PropagateSubExpressions();

            //Update code block
            CodeBlock.ComputedVariables = _computedVarsList;

            TcbDependencyUpdate.Process(CodeBlock);
        }
    }
}
