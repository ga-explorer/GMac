using System.Text;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using GMac.GMacAPI.CodeBlock;
using GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Optimizer.Evaluator;
using Wolfram.NETLink;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Optimizer
{
    internal sealed class TcbEvaluateCodeBlock : TcbProcessor
    {
        internal static GMacCodeBlock Process(GMacCodeBlock codeBlock, TlCodeBlockEvaluation evaluationData)
        {
            var processor = new TcbEvaluateCodeBlock(codeBlock, evaluationData);

            processor.BeginProcessing();

            return codeBlock;
        }


        private readonly TlCodeBlockEvaluation _evaluationData;


        private TcbEvaluateCodeBlock(GMacCodeBlock codeBlock, TlCodeBlockEvaluation evaluationData)
            : base(codeBlock)
        {
            _evaluationData = evaluationData;
        }


        private string ExpressionToString(SteExpression expr)
        {
            if (expr.IsAtomic)
            {
                if (expr.IsVariable)
                    return _evaluationData[expr.HeadText].ToString();
                
                return expr.HeadText;
            }

            var s = new StringBuilder();

            s.Append(expr.HeadText)
                .Append("[");

            for (var i = 0; i < expr.ArgumentsCount; i++)
            {
                if (i > 0)
                    s.Append(",");
                
                s.Append(ExpressionToString(expr[i]));
            }

            s.Append("]");

            return s.ToString();
        }

        private Expr EvaluateExpression(SteExpression expr)
        {
            return GaSymbolicsUtils.Cas[Mfs.Simplify[ExpressionToString(expr)]];
        }

        protected override void BeginProcessing()
        {
            foreach (var computedVar in CodeBlock.ComputedVariables)
                _evaluationData[computedVar.LowLevelName] = EvaluateExpression(computedVar.RhsExpr);
        }
    }
}
