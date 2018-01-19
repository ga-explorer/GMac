using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.GMacAPI.Binding;
using GMac.GMacAPI.CodeBlock;
using Wolfram.NETLink;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Optimizer.Evaluator
{
    internal sealed class TlCodeBlockEvaluationHistory
    {
        public GMacCodeBlock CodeBlock { get; }


        private readonly Random _randomSource = 
            new Random(DateTime.Now.Millisecond);

        private readonly Dictionary<string, Expr> _inputVarsValues;

        private readonly List<TlCodeBlockEvaluation> _evaluations = 
            new List<TlCodeBlockEvaluation>();


        internal TlCodeBlockEvaluationHistory(GMacCodeBlock codeBlock, double minValue, double maxValue)
        {
            CodeBlock = codeBlock;

            _inputVarsValues = 
                CodeBlock
                .InputVariables
                .ToDictionary(
                    item => item.LowLevelName,
                    item => new Expr(_randomSource.NextDouble() * (maxValue - minValue) + minValue)
                    );
        }

        internal TlCodeBlockEvaluationHistory(GMacCodeBlock codeBlock, Dictionary<string, GMacMacroParameterBinding> inputsWithTestValues)
        {
            CodeBlock = codeBlock;

            _inputVarsValues = new Dictionary<string, Expr>();

            foreach (var pair in inputsWithTestValues)
            {
                GMacCbInputVariable inputParamVar;

                if (!CodeBlock.TryGetInputParameterVariable(pair.Value.ValueAccess, out inputParamVar))
                    continue;

                _inputVarsValues.Add(inputParamVar.LowLevelName, pair.Value.TestValueExpr);
            }
        }


        public TlCodeBlockEvaluation AddEvaluation(string evalTitle)
        {
            var evaluationData = new TlCodeBlockEvaluation(CodeBlock, evalTitle);

            _evaluations.Add(evaluationData);

            foreach (var pair in _inputVarsValues)
                evaluationData[pair.Key] = pair.Value;

            TcbEvaluateCodeBlock.Process(CodeBlock, evaluationData);

            return evaluationData;
        }

        public override string ToString()
        {
            var s = new StringBuilder(1024);

            var maxLength = _evaluations.Max(item => item.EvaluationTitle.Length);

            foreach (var outputVar in CodeBlock.OutputVariables)
            {
                s.Append(outputVar.ValueAccessName).AppendLine(":");

                foreach (var evalData in _evaluations)
                    s.Append(evalData.EvaluationTitle.PadLeft(maxLength))
                        .Append(": ")
                        .Append(evalData[outputVar.LowLevelName])
                        .AppendLine();

                s.AppendLine();
            }

            return s.ToString();
        }
    }
}
