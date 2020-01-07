using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.SourceCode;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacCompiler.Syntax;
using Irony.Parsing;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    internal sealed class GMacExpressionGenerator : GMacAstSymbolGenerator
    {
        public static ILanguageExpression Translate(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            context.PushState(node);

            var translator = new GMacExpressionGenerator();//new GMacExpressionGenerator(context);

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

            var translator = new GMacExpressionGenerator();//new GMacExpressionGenerator(basicExprGen);

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


        private ILanguageExpression translate_Expression_Product(ParseTreeNode node)
        {
            var subNode = node.ChildNodes[0];

            if (subNode.ToString() == GMacParseNodeNames.ExpressionAtomic)
                //A 'Expression_Atomic' node
                return GMacExpressionAtomicGenerator.Translate(BasicExpressionGenerator, subNode);


            //A 'Expression_Product + GA_Binary_Product + Expression_Atomic' node
            var gaBinaryProductNode = node.ChildNodes[1];
            var biOp = gaBinaryProductNode.ChildNodes[0].Term.ToString();

            var expr1 = translate_Expression_Product(node.ChildNodes[0]);
            var expr2 = GMacExpressionAtomicGenerator.Translate(BasicExpressionGenerator, node.ChildNodes[2]);

            Context.PushState(node);

            ILanguageExpression finalExpr;

            switch (biOp)
            {
                case "/":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryDivideByScalar(expr1, expr2);
                    break;

                case "*":
                    finalExpr =
                        BasicExpressionGenerator
                        .Generate_BinaryTimesWithScalar(expr1, expr2);
                    break;

                case "^":
                case "op":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryEuclideanProduct(GMacOpInfo.BinaryOp, expr1, expr2);
                    break;

                case "sp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryScalarProduct(GMacOpInfo.BinarySp, expr1, expr2);
                    break;

                case "gp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryProduct(GMacOpInfo.BinaryGp, expr1, expr2);
                    break;

                case "lcp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryProduct(GMacOpInfo.BinaryLcp, expr1, expr2);
                    break;

                case "rcp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryProduct(GMacOpInfo.BinaryRcp, expr1, expr2);
                    break;

                case "fdp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryProduct(GMacOpInfo.BinaryFdp, expr1, expr2);
                    break;

                case "hip":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryProduct(GMacOpInfo.BinaryHip, expr1, expr2);
                    break;

                case "cp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryProduct(GMacOpInfo.BinaryCp, expr1, expr2);
                    break;

                case "acp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryProduct(GMacOpInfo.BinaryAcp, expr1, expr2);
                    break;

                case "esp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryEuclideanScalarProduct(GMacOpInfo.BinaryESp, expr1, expr2);
                    break;

                case "egp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryEuclideanProduct(GMacOpInfo.BinaryEGp, expr1, expr2);
                    break;

                case "elcp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryEuclideanProduct(GMacOpInfo.BinaryELcp, expr1, expr2);
                    break;

                case "ercp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryEuclideanProduct(GMacOpInfo.BinaryERcp, expr1, expr2);
                    break;

                case "efdp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryEuclideanProduct(GMacOpInfo.BinaryEFdp, expr1, expr2);
                    break;

                case "ehip":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryEuclideanProduct(GMacOpInfo.BinaryEHip, expr1, expr2);
                    break;

                case "ecp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryEuclideanProduct(GMacOpInfo.BinaryECp, expr1, expr2);
                    break;

                case "eacp":
                    finalExpr = 
                        BasicExpressionGenerator
                        .Generate_BinaryEuclideanProduct(GMacOpInfo.BinaryEAcp, expr1, expr2);
                    break;

                default:
                    return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Binary product not recognized", gaBinaryProductNode.ChildNodes[0]);
            }

            Context.PopState();

            return finalExpr;
        }

        private ILanguageExpression translate_Expression_Sum(ParseTreeNode node)
        {
            var subNode = node.ChildNodes[0];

            if (subNode.ToString() == GMacParseNodeNames.ExpressionProduct)
                //A 'Expression_Product' node
                return translate_Expression_Product(subNode);

            //A 'Expression_Sum + GA_Binary_Sum + Expression_Product' node
            var gaBinarySumNode = node.ChildNodes[1];
            var biOp = gaBinarySumNode.ChildNodes[0].FindTokenAndGetText();

            var expr1 = translate_Expression_Sum(node.ChildNodes[0]);
            var expr2 = translate_Expression_Product(node.ChildNodes[2]);

            Context.PushState(node);

            ILanguageExpression finalExpr;
            switch (biOp)
            {
                case "+":
                    finalExpr = BasicExpressionGenerator.Generate_BinaryPlus(expr1, expr2);
                    break;
                case "-":
                    finalExpr = BasicExpressionGenerator.Generate_BinarySubtract(expr1, expr2);
                    break;
                default:
                    return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Binary operator not recognized", subNode.ChildNodes[0]);
            }

            Context.PopState();

            return finalExpr;
        }

        /// <summary>
        /// This is the main function for translation of expressions
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public ILanguageExpression translate_Expression(ParseTreeNode node)
        {
            node.Assert(GMacParseNodeNames.Expression);

            var subNode = node.ChildNodes[0];

            if (subNode.ToString() == GMacParseNodeNames.ExpressionSum)
                //A 'Expression_Sum' node
                return translate_Expression_Sum(subNode);

            return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Expression not recognized", subNode);

            //TODO: Fix unary expressions later
            ////A 'GA_Unary_Operation + Expression_Sum' node
            //var gaUnaryOperation = subNode.ChildNodes[0].FindTokenAndGetText();

            //var expr = translate_Expression_Sum(RootParseNode.ChildNodes[1]);

            //Context.PushState(node);

            //ILanguageExpression finalExpr;
            
            //switch (gaUnaryOperation)
            //{
            //    case "+":
            //        finalExpr = BasicExpressionGenerator.Generate_UnaryPlus(expr);
            //        break;
            //    case "-":
            //        finalExpr = BasicExpressionGenerator.Generate_UnaryMinus(expr);
            //        break;
            //    default:
            //        return CompilationLog.RaiseGeneratorError<ILanguageExpression>("Unary operator not recognized", subNode.ChildNodes[0]);
            //}

            //Context.PopState();

            //return finalExpr;
        }

        protected override void Translate()
        {
            if (Context.ActiveParentCommandBlock != null)
            {
                //If a parent command block is already present, just translate the expression and return it
                _generatedExpression = translate_Expression(RootParseNode);

                return;
            }

            //If there is no parent command block yet, create a composite expression command block
            var newCompositeExpr = CompositeExpression.Create(Context.ActiveParentScope);

            Context.PushState(newCompositeExpr.ChildCommandBlockScope);

            //Begin translation of the expression
            var finalExpr = translate_Expression(RootParseNode);

            //If there is any command in the composite expression, create and set the output local variable
            newCompositeExpr.OutputVariable = newCompositeExpr.ExpressionToLocalVariable(finalExpr);

            //Return the whole composite expression as the translated language expression
            _generatedExpression = newCompositeExpr;

            Context.PopState();
        }
    }
}
