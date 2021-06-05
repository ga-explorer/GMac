using System;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Languages.Excel;
using CodeComposerLib.SyntaxTree.Expressions;

namespace GMac.Engine.API.Target.Excel
{
    public sealed class MathematicaToExcelConverter : GMacMathematicaExpressionConverter
    {
        public MathematicaToExcelConverter()
            : base(ExcelUtils.Excel2007Info)
        {

        }

        /// <summary>
        /// https://www.excelfunctions.net/Excel-Math-Functions.html
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private static SteExpression ConvertFunction(string functionName, params SteExpression[] arguments)
        {
            switch (functionName)
            {
                //case "Rational":
                //    return SteExpressionUtils.CreateOperator(
                //        ExcelUtils.Operators.Divide, 
                //        SteExpressionUtils.CreateLiteralNumber(arguments[0].ToString()),
                //        SteExpressionUtils.CreateLiteralNumber(arguments[1].ToString())
                //        );

                case "Plus":
                    return SteExpressionUtils.CreateOperator(
                        ExcelUtils.Operators.Add, arguments
                    );

                case "Minus":
                    return SteExpressionUtils.CreateOperator(
                        ExcelUtils.Operators.UnaryMinus, arguments
                    );

                case "Subtract":
                    return SteExpressionUtils.CreateOperator(
                        ExcelUtils.Operators.Subtract, arguments
                    );

                case "Times":
                    if (arguments[0].ToString() == "-1" && arguments.Length == 2)
                        return SteExpressionUtils.CreateOperator(
                            ExcelUtils.Operators.UnaryMinus, arguments[1]
                        );

                    return SteExpressionUtils.CreateOperator(
                        ExcelUtils.Operators.Multiply, arguments
                    );

                case "Divide":
                    return SteExpressionUtils.CreateOperator(
                        ExcelUtils.Operators.Divide, arguments
                    );

                case "Power":
                    if (arguments[1].ToString() == "-1")
                        return SteExpressionUtils.CreateOperator(
                            ExcelUtils.Operators.Divide,
                            SteExpressionUtils.CreateLiteralNumber(1),
                            arguments[0]
                        );

                    if (arguments[1].ToString() == "2")
                        return SteExpressionUtils.CreateOperator(
                            ExcelUtils.Operators.Multiply,
                            arguments[0],
                            arguments[0]
                        );

                    if (arguments[1].ToString() == "3")
                        return SteExpressionUtils.CreateOperator(
                            ExcelUtils.Operators.Multiply,
                            arguments[0],
                            arguments[0],
                            arguments[0]
                        );

                    return SteExpressionUtils.CreateFunction("POWER", arguments);

                case "Abs":
                    return SteExpressionUtils.CreateFunction("ABS", arguments);

                case "Exp":
                    return SteExpressionUtils.CreateFunction("EXP", arguments);

                case "Sin":
                    return SteExpressionUtils.CreateFunction("SIN", arguments);

                case "Cos":
                    return SteExpressionUtils.CreateFunction("COS", arguments);

                case "Tan":
                    return SteExpressionUtils.CreateFunction("TAN", arguments);

                case "ArcSin":
                    return SteExpressionUtils.CreateFunction("ASIN", arguments);

                case "ArcCos":
                    return SteExpressionUtils.CreateFunction("ACOS", arguments);

                case "ArcTan":
                    return SteExpressionUtils.CreateFunction(
                        arguments.Length == 1 ? "ATAN" : "ATAN2",
                        arguments
                    );

                case "Sinh":
                    return SteExpressionUtils.CreateFunction("SINH", arguments);

                case "Cosh":
                    return SteExpressionUtils.CreateFunction("COSH", arguments);

                case "Tanh":
                    return SteExpressionUtils.CreateFunction("TANH", arguments);

                case "Log":
                    return SteExpressionUtils.CreateFunction(
                        arguments.Length == 1 ? "LN" : "LOG",
                        arguments.Length == 1 ? arguments : arguments.Reverse()
                    );

                case "Log10":
                    return SteExpressionUtils.CreateFunction("LOG10", arguments);

                case "Sqrt":
                    return SteExpressionUtils.CreateFunction("SQRT", arguments);

                case "Floor":
                    return SteExpressionUtils.CreateFunction(
                        arguments.Length == 1 ? "FLOOR" : "FLOOR.MATH",
                        arguments
                    );

                case "Ceiling":
                    return SteExpressionUtils.CreateFunction(
                        arguments.Length == 1 ? "CEILING" : "CEILING.MATH",
                        arguments
                    );

                case "Round":
                    return SteExpressionUtils.CreateFunction(
                        arguments.Length == 1 ? "ROUND" : "MROUND",
                        arguments
                    );

                case "Min":
                    return SteExpressionUtils.CreateFunction("MIN", arguments);

                case "Max":
                    return SteExpressionUtils.CreateFunction("MAX", arguments);

                case "Sign":
                    return SteExpressionUtils.CreateFunction("SIGN", arguments);

                case "IntegerPart":
                    return SteExpressionUtils.CreateFunction("TRUNC", arguments);
            }

            return SteExpressionUtils.CreateFunction(functionName, arguments);
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
                        return SteExpressionUtils.CreateSymbolicNumber("PI");

                    case "E":
                        return SteExpressionUtils.CreateSymbolicNumber("EXP(1)");
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
                        return SteExpressionUtils.CreateSymbolicNumber("PI");

                    case "E":
                        return SteExpressionUtils.CreateSymbolicNumber("EXP(1)");
                }

                return expr.CreateCopy();
            }

            //A function; the arguments are converted before creating the main function expression
            return expr.IsFunction
                ? ConvertFunction(
                    expr.HeadText, 
                    expr.Arguments.Select(a => Convert(a, targetVarsDictionary)).ToArray())
                : expr.CreateCopy();
        }
    }
}