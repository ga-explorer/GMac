using System.Linq;
using System.Text;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.GMacCompiler.Syntax;
using Irony.Parsing;
using IronyGrammars.Semantic.Expression;
using IronyGrammars.Semantic.Expression.Basic;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    internal sealed class GMacExpressionAtomicSymbolicGenerator : GMacAstSymbolGenerator
    {
        public static ILanguageExpression Translate(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            context.PushState(node);

            var translator = new GMacExpressionAtomicSymbolicGenerator();// new GMacExpressionAtomicSymbolicGenerator(context);

            var basicExprGen = new GMacExpressionBasicGenerator();

            basicExprGen.SetContext(context);

            translator.SetContext(basicExprGen);
            translator.Translate();

            context.PopState();

            var result = translator._generatedExpression;

            //MasterPool.Release(basicExprGen);
            //MasterPool.Release(translator);

            return result;
        }

        public static ILanguageExpression Translate(GMacExpressionBasicGenerator basicExprGen, ParseTreeNode node)
        {
            var context = basicExprGen.Context;

            context.PushState(node);

            var translator = new GMacExpressionAtomicSymbolicGenerator();//new GMacExpressionAtomicSymbolicGenerator(basicExprGen);

            translator.SetContext(basicExprGen);
            translator.Translate();

            context.PopState();

            var result = translator._generatedExpression;

            //MasterPool.Release(translator);

            return result;
        }


        private ILanguageExpression _generatedExpression;

        public GMacExpressionBasicGenerator BasicExpressionGenerator { get; private set; }


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _generatedExpression = null;
        //    BasicExpressionGenerator = null;
        //}


        private void SetContext(GMacExpressionBasicGenerator basicExprGen)
        {
            SetContext(basicExprGen.Context);
            BasicExpressionGenerator = basicExprGen;
        }


        private ILanguageExpression translate_LanguageExpression(string expressionText)
        {
            var rootParseNode = GMacSourceParser.ParseExpression(expressionText, CompilationLog);

            return GMacExpressionGenerator.Translate(Context, rootParseNode);
        }

        private void translate_Dependency_List(string expressionText, out MathematicaScalar scalar, out OperandsByName operands)
        {
            var finalScalarText = new StringBuilder(expressionText);

            operands = OperandsByName.Create(); 
            
            var varIdx = 1;

            var allMatches = GenUtils.ExtractDistinctInternalExpressions(expressionText);

            foreach (var rgexMatch in allMatches)
            {
                var rhsExprText = rgexMatch.Value.Substring(1, rgexMatch.Value.Length - 2);

                var rhsExpr = 
                    BasicExpressionGenerator.Generate_PolyadicOperand(
                        GMacRootAst.ScalarType, 
                        translate_LanguageExpression(rhsExprText)
                        );

                var lhsVarName = "var" + (varIdx++).ToString("0000");

                finalScalarText = finalScalarText.Replace(rgexMatch.Value, lhsVarName);

                operands.AddOperand(lhsVarName, rhsExpr);
            }

            scalar = MathematicaScalar.Create(Cas, finalScalarText.ToString());
        }

        protected override void Translate()
        {
            var expressionText = GenUtils.Translate_StringLiteral(RootParseNode);

            if (
                expressionText.First() == '$' &&
                expressionText.Last() == '$' &&
                expressionText.Count(c => c == '$') == 2
            )
            {
                //If the expression is on the form '$ anything $' convert it into a normal multivector expression
                //based on (anything) alone; not a symbolic expression
                expressionText = expressionText.Substring(1, expressionText.Length - 2).Trim();

                _generatedExpression = translate_LanguageExpression(expressionText);

                return;
            }

            MathematicaScalar scalar;

            OperandsByName operands;

            translate_Dependency_List(expressionText, out scalar, out operands);

            _generatedExpression = BasicExpressionGenerator.Generate_SymbolicExpression(scalar, operands);
        }
    }
}
