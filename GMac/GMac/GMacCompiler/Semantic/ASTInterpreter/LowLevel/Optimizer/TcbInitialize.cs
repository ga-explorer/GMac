using System;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.SyntaxTree.Expressions;
using GMac.GMacAPI.CodeBlock;
using GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Generator;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Optimizer
{
    /// <summary>
    /// Initialize the target code block data structures from the generated low-level items
    /// </summary>
    internal sealed class TcbInitialize : TcbProcessor
    {
        internal static GMacCodeBlock Process(GMacCodeBlock codeBlock, LlDataTable dataTable)
        {
            var processor = new TcbInitialize(codeBlock, dataTable);

            processor.BeginProcessing();

            return codeBlock;
        }


        private readonly LlDataTable _dataTable;

        private string _lastUsedVarName = string.Empty;

        private int _computationOrder = int.MaxValue;

        private readonly List<string> _activeItemsList = new List<string>();


        private TcbInitialize(GMacCodeBlock codeBlock, LlDataTable generatedDataTable)
            : base(codeBlock)
        {
            _dataTable = generatedDataTable;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rhsExpr"></param>
        /// <returns></returns>
        private SteExpression BybassSingleTempVariableRhsExpr(SteExpression rhsExpr)
        {
            while (true)
            {
                //The RHS expression is not a variable name. Return the full expression
                if (rhsExpr.IsVariable == false)
                    return rhsExpr;

                //The RHS expression is a variable name
                var rhsExprVarName = rhsExpr.HeadText;

                //The RHS expression is not a low-level variable name. Return the full expression
                if (LowLevelUtils.IsLowLevelVariableName(rhsExprVarName) == false)
                    return rhsExpr;

                var llItem = _dataTable.GetItemByName(rhsExprVarName);

                //The RHS expression is not a temp item. Return the full expression
                if (llItem.IsTemp == false)
                    return rhsExpr;

                //Iterate over the RHS of the temp low-level item whos name is given in rhsExprVarName
                rhsExpr = llItem.AssignedRhsSymbolicScalar.ToSymbolicTextExpression();
            }
        }

        /// <summary>
        /// Add all input variables to the target code block
        /// </summary>
        private void AddInputVariables()
        {
            foreach (var item in _dataTable.Inputs)
            {
                CodeBlock.AddInputVariable(
                    new GMacCbInputVariable(
                        item.ItemName, 
                        item.ItemId, 
                        item.AssociatedValueAccess
                        )
                    );

                if (string.Compare(item.ItemName, _lastUsedVarName, StringComparison.Ordinal) > 0)
                    _lastUsedVarName = item.ItemName;
            }
        }

        /// <summary>
        /// Add a single output variable to the code block
        /// </summary>
        /// <param name="item"></param>
        private void AddOutputVariable(LlDataItem item)
        {
            //Find the correct RHS for this output variable
            var rhsExpr =
                BybassSingleTempVariableRhsExpr(
                    item.AssignedRhsSymbolicScalar.ToSymbolicTextExpression()
                    );

            //Create the output variable and add it to the target language block
            var outputVar =
                new GMacCbOutputVariable(
                    item.ItemName,
                    item.ItemId,
                    item.AssociatedValueAccess,
                    rhsExpr
                    ) { ComputationOrder = _computationOrder-- };

            CodeBlock.ComputedVariables.Add(outputVar);

            //This item is required for computation. 
            //All its RHS items must be added to the list of active items
            _activeItemsList.AddRange(rhsExpr.GetLowLevelVariablesNames());

            if (string.Compare(item.ItemName, _lastUsedVarName, StringComparison.Ordinal) > 0)
                _lastUsedVarName = item.ItemName;
        }

        /// <summary>
        /// Test if the given temp item is required to be added
        /// </summary>
        /// <param name="item"></param>
        private void TryAddTempVariable(LlDataItem item)
        {
            //If this temp item is not in the list of required items for computation just ignore it
            if (_activeItemsList.Contains(item.ItemName) == false)
                return;

            var rhsExpr =
                item.AssignedRhsSymbolicScalar.ToSymbolicTextExpression();

            //Create the temp variable and add it to the target language block
            var tempVar =
                new GMacCbTempVariable(
                    item.ItemName,
                    rhsExpr,
                    false
                    ) { ComputationOrder = _computationOrder-- };

            CodeBlock.ComputedVariables.Add(tempVar);

            //This item is required for computation. 
            //All its RHS items must be added to the list of active items
            _activeItemsList.AddRange(rhsExpr.GetLowLevelVariablesNames());

            if (string.Compare(item.ItemName, _lastUsedVarName, StringComparison.Ordinal) > 0)
                _lastUsedVarName = item.ItemName;
        }

        /// <summary>
        /// Add temp and output variables to the code block
        /// </summary>
        private void AddComputedVariables()
        {
            _computationOrder = int.MaxValue;

            CodeBlock.ComputedVariables.Clear();

            _activeItemsList.Clear();

            //Add temp and output variables
            foreach (var item in _dataTable.ComputedByEvaluationOrder.Reverse())
                if (item.IsOutput)
                    AddOutputVariable(item);

                else
                    TryAddTempVariable(item);

            CodeBlock.ComputedVariables.Reverse();
        }

        protected override void BeginProcessing()
        {
            _lastUsedVarName = string.Empty;

            AddInputVariables();

            AddComputedVariables();

            CodeBlock.SetLastUsedVarName(_lastUsedVarName);

            TcbDependencyUpdate.Process(CodeBlock);
        }
    }
}
