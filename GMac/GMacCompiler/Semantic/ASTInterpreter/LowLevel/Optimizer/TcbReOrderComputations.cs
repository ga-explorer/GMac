using System.Collections.Generic;
using System.Linq;
using GMac.GMacAPI.CodeBlock;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Optimizer
{
    internal sealed class TcbReOrderComputations : TcbProcessor
    {
        internal static GMacCodeBlock Process(GMacCodeBlock codeBlock, bool orderOutputsById)
        {
            var processor = new TcbReOrderComputations(codeBlock, orderOutputsById);

            processor.BeginProcessing();

            return codeBlock;
        }


        public bool OrderOutputsById { get; }

        private readonly Dictionary<string, GMacCbComputedVariable> _computedVariablesDictionary = 
            new Dictionary<string, GMacCbComputedVariable>();


        private TcbReOrderComputations(GMacCodeBlock codeBlock, bool orderOutputsById)
            : base(codeBlock)
        {
            OrderOutputsById = orderOutputsById;
        }


        private void AddOutputVariable(GMacCbOutputVariable outputVar)
        {
            if (outputVar.RhsTempVariables.Any())
            {
                var rhsTemVarsList =
                    outputVar
                    .GetUsedTempVariables()
                    .Where(
                        rhsTempVar =>
                            _computedVariablesDictionary.ContainsKey(rhsTempVar.LowLevelName) == false
                        );

                foreach (var rhsTempVar in rhsTemVarsList)
                    _computedVariablesDictionary.Add(rhsTempVar.LowLevelName, rhsTempVar);
            }

            _computedVariablesDictionary.Add(outputVar.LowLevelName, outputVar);
        }

        protected override void BeginProcessing()
        {
            var outputVarsList =
                CodeBlock
                .OutputVariables
                .OrderBy(item => OrderOutputsById ? item.LowLevelId : item.MaxComputationLevel)
                .ThenBy(item => item.LowLevelName);

            foreach (var outputVar in outputVarsList)
                AddOutputVariable(outputVar);

            CodeBlock.ComputedVariables.Clear();

            CodeBlock.ComputedVariables.AddRange(_computedVariablesDictionary.Select(pair => pair.Value));

            CodeBlock.UpdateComputationOrder();
        }
    }
}
