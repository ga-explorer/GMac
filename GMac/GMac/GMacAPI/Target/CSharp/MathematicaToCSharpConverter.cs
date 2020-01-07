using System;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Languages.CSharp;
using CodeComposerLib.SyntaxTree.Expressions;

namespace GMac.GMacAPI.Target.CSharp
{
    public sealed class MathematicaToCSharpConverter : GMacMathematicaExpressionConverter
    {
        public MathematicaToCSharpConverter()
            : base(CSharpUtils.CSharp4Info)
        {
            
        }


        private static SteExpression ConvertFunction(string functionName, params SteExpression[] arguments)
        {

            switch (functionName)
            {
                //case "Rational":
                //    return SteExpressionUtils.CreateOperator(
                //        CSharpUtils.Operators.Divide, 
                //        SteExpressionUtils.CreateLiteralNumber(arguments[0].ToString()),
                //        SteExpressionUtils.CreateLiteralNumber(arguments[1].ToString())
                //        );

                case "Plus":
                    return SteExpressionUtils.CreateOperator(
                        CSharpUtils.Operators.Add, arguments
                        );

                case "Minus":
                    return SteExpressionUtils.CreateOperator(
                        CSharpUtils.Operators.UnaryMinus, arguments
                        );

                case "Subtract":
                    return SteExpressionUtils.CreateOperator(
                        CSharpUtils.Operators.Subtract, arguments
                        );

                case "Times":
                    if (arguments[0].ToString() == "-1" && arguments.Length == 2)
                        return SteExpressionUtils.CreateOperator(
                            CSharpUtils.Operators.UnaryMinus, arguments[1]
                        );

                    return SteExpressionUtils.CreateOperator(
                        CSharpUtils.Operators.Multiply, arguments
                        );

                case "Divide":
                    return SteExpressionUtils.CreateOperator(
                        CSharpUtils.Operators.Divide, arguments
                        );

                case "Power":
                    if (arguments[1].ToString() == "-1")
                        return SteExpressionUtils.CreateOperator(
                            CSharpUtils.Operators.Divide,
                            SteExpressionUtils.CreateLiteralNumber(1),
                            arguments[0]
                        );

                    if (arguments[1].ToString() == "2")
                        return SteExpressionUtils.CreateOperator(
                            CSharpUtils.Operators.Multiply,
                            arguments[0],
                            arguments[0]
                        );

                    if (arguments[1].ToString() == "3")
                        return SteExpressionUtils.CreateOperator(
                            CSharpUtils.Operators.Multiply,
                            arguments[0],
                            arguments[0],
                            arguments[0]
                        );

                    return SteExpressionUtils.CreateFunction("Math.Pow", arguments);

                case "Abs":
                    return SteExpressionUtils.CreateFunction("Math.Abs", arguments);

                case "Exp":
                    return SteExpressionUtils.CreateFunction("Math.Exp", arguments);

                case "Sin":
                    return SteExpressionUtils.CreateFunction("Math.Sin", arguments);

                case "Cos":
                    return SteExpressionUtils.CreateFunction("Math.Cos", arguments);

                case "Tan":
                    return SteExpressionUtils.CreateFunction("Math.Tan", arguments);

                case "ArcSin":
                    return SteExpressionUtils.CreateFunction("Math.Asin", arguments);

                case "ArcCos":
                    return SteExpressionUtils.CreateFunction("Math.Acos", arguments);

                case "ArcTan":
                    return SteExpressionUtils.CreateFunction(
                        arguments.Length == 1 ? "Math.Atan" : "Math.Atan2",
                        arguments
                        );

                case "Sinh":
                    return SteExpressionUtils.CreateFunction("Math.Sinh", arguments);

                case "Cosh":
                    return SteExpressionUtils.CreateFunction("Math.Cosh", arguments);

                case "Tanh":
                    return SteExpressionUtils.CreateFunction("Math.Tanh", arguments);

                case "Log":
                    return SteExpressionUtils.CreateFunction(
                        "Math.Log", 
                        arguments.Length == 1 ? arguments : arguments.Reverse()
                        );

                case "Log10":
                    return SteExpressionUtils.CreateFunction("Math.Log10", arguments);

                case "Sqrt":
                    return SteExpressionUtils.CreateFunction("Math.Sqrt", arguments);

                case "Floor":
                    return SteExpressionUtils.CreateFunction(
                        arguments.Length == 1 ? "Math.Floor" : "MathHelper.Floor",
                        arguments
                        );

                case "Ceiling":
                    return SteExpressionUtils.CreateFunction(
                        arguments.Length == 1 ? "Math.Ceiling" : "MathHelper.Ceiling",
                        arguments
                        );

                case "Round":
                    return SteExpressionUtils.CreateFunction(
                        arguments.Length == 1 ? "Math.Round" : "MathHelper.Round",
                        arguments
                        );

                case "Min":
                    return SteExpressionUtils.CreateFunction(
                        arguments.Length == 1 ? "Math.Min" : "MathHelper.Min",
                        arguments
                        );

                case "Max":
                    return SteExpressionUtils.CreateFunction(
                        arguments.Length == 1 ? "Math.Max" : "MathHelper.Max",
                        arguments
                        );

                case "Sign":
                    return SteExpressionUtils.CreateFunction("Math.Sign", arguments);

                case "IntegerPart":
                    return SteExpressionUtils.CreateFunction("Math.Truncate", arguments);
            }

            return SteExpressionUtils.CreateFunction("MathHelper." + functionName, arguments);
        }

        private static SteExpression ConvertNumber(SteExpression numberExpr)
        {
            var rationalHeadIndex = numberExpr.HeadText.IndexOf(@"Rational[", StringComparison.Ordinal);

            //This is an ordinary number, return as-is
            if (rationalHeadIndex < 0)
            {
                return numberExpr.CreateCopy();
            }

            //This is a rational atomic number; for example Rational[1, 2]. 
            //Extract components and convert to floating point
            var numberTextFull = numberExpr.HeadText.Substring(@"Rational[".Length);
            var commaIndex = numberTextFull.IndexOf(',');
            var bracketIndex = numberTextFull.IndexOf(']');

            var num1Text = numberTextFull.Substring(0, commaIndex);
            var num2Text = numberTextFull.Substring(commaIndex + 1, bracketIndex - commaIndex - 1);

            return SteExpressionUtils.CreateLiteralNumber(
                double.Parse(num1Text) / double.Parse(num2Text)
                );
        }

        public override SteExpression Convert(SteExpression expr)
        {
            //A number
            if (expr.IsNumberLiteral) 
                return ConvertNumber(expr);
            //This has a problem with the Rational[] numbers
            //expr.CreateCopy(); 


            //A variable
            if (expr.IsVariable)
            {
                //Try convert a low-level Mathematica variable name into a target variable name

                if (ActiveCodeBlock != null && ActiveCodeBlock.VariablesDictionary.TryGetValue(expr.HeadText, out var targetVar))
                    return SteExpressionUtils.CreateVariable(targetVar.TargetVariableName);

                return expr.CreateCopy();
            }

            //A symbolic constant
            if (expr.IsNumberSymbol)
            {
                switch (expr.HeadText)
                {
                    case "Pi":
                        return SteExpressionUtils.CreateSymbolicNumber("Math.PI");

                    case "E":
                        return SteExpressionUtils.CreateSymbolicNumber("Math.E");
                }

                return expr.CreateCopy();
            }

            //A function; the arguments are converted before creating the main function expression
            return expr.IsFunction 
                ? ConvertFunction(
                    expr.HeadText, 
                    expr.Arguments.Select(Convert).ToArray()
                    ) 
                : expr.CreateCopy();
        }

        public override SteExpression Convert(SteExpression expr, IDictionary<string, string> targetVarsDictionary)
        {
            //A number
            if (expr.IsNumberLiteral)
                return ConvertNumber(expr);
            //This has a problem with the Rational[] numbers
            //expr.CreateCopy(); 

            //A variable
            if (expr.IsVariable)
            {
                //Try convert a low-level Mathematica variable name into a target variable name

                if (targetVarsDictionary != null && targetVarsDictionary.TryGetValue(expr.HeadText, out var targetVarName))
                    return SteExpressionUtils.CreateVariable(targetVarName);

                return expr.CreateCopy();
            }

            //A symbolic constant
            if (expr.IsNumberSymbol)
            {
                switch (expr.HeadText)
                {
                    case "Pi":
                        return SteExpressionUtils.CreateSymbolicNumber("Math.PI");

                    case "E":
                        return SteExpressionUtils.CreateSymbolicNumber("Math.E");
                }

                return expr.CreateCopy();
            }

            //A function; the arguments are converted before creating the main function expression
            return expr.IsFunction
                ? ConvertFunction(
                    expr.HeadText,
                    expr.Arguments.Select(a => Convert(a, targetVarsDictionary)).ToArray()
                )
                : expr.CreateCopy();
        }
    }
}
