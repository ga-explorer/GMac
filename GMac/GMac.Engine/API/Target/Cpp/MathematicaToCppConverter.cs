using System;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Languages.Cpp;
using CodeComposerLib.SyntaxTree.Expressions;

namespace GMac.Engine.API.Target.Cpp
{
    public sealed class MathematicaToCppConverter : GMacMathematicaExpressionConverter
    {
        public MathematicaToCppConverter()
            : base(CppUtils.Cpp11Info)
        {
            
        }


        private static SteExpression ConvertFunction(string functionName, params SteExpression[] arguments)
        {

            switch (functionName)
            {
                //case "Rational":
                //    return SteExpressionUtils.CreateOperator(
                //        CppUtils.Operators.Divide, 
                //        SteExpressionUtils.CreateLiteralNumber(arguments[0].ToString()),
                //        SteExpressionUtils.CreateLiteralNumber(arguments[1].ToString())
                //        );

                case "Plus":
                    return SteExpressionUtils.CreateOperator(
                        CppUtils.Operators.Add, arguments
                    );

                case "Minus":
                    return SteExpressionUtils.CreateOperator(
                        CppUtils.Operators.UnaryMinus, arguments
                    );

                case "Subtract":
                    return SteExpressionUtils.CreateOperator(
                        CppUtils.Operators.Subtract, arguments
                    );

                case "Times":
                    if (arguments[0].ToString() == "-1" && arguments.Length == 2)
                        return SteExpressionUtils.CreateOperator(
                            CppUtils.Operators.UnaryMinus, arguments[1]
                        );

                    return SteExpressionUtils.CreateOperator(
                        CppUtils.Operators.Multiply, arguments
                    );

                case "Divide":
                    return SteExpressionUtils.CreateOperator(
                        CppUtils.Operators.Divide, arguments
                    );

                case "Power":
                    if (arguments[1].ToString() == "-1")
                        return SteExpressionUtils.CreateOperator(
                            CppUtils.Operators.Divide,
                            SteExpressionUtils.CreateLiteralNumber(1),
                            arguments[0]
                        );

                    if (arguments[1].ToString() == "2")
                        return SteExpressionUtils.CreateOperator(
                            CppUtils.Operators.Multiply,
                            arguments[0],
                            arguments[0]
                        );

                    if (arguments[1].ToString() == "3")
                        return SteExpressionUtils.CreateOperator(
                            CppUtils.Operators.Multiply,
                            arguments[0],
                            arguments[0],
                            arguments[0]
                        );

                    return SteExpressionUtils.CreateFunction("pow", arguments);

                case "Abs":
                    return SteExpressionUtils.CreateFunction("fabs", arguments);

                case "Exp":
                    return SteExpressionUtils.CreateFunction("exp", arguments);

                case "Sin":
                    return SteExpressionUtils.CreateFunction("sin", arguments);

                case "Cos":
                    return SteExpressionUtils.CreateFunction("cos", arguments);

                case "Tan":
                    return SteExpressionUtils.CreateFunction("tan", arguments);

                case "ArcSin":
                    return SteExpressionUtils.CreateFunction("asin", arguments);

                case "ArcCos":
                    return SteExpressionUtils.CreateFunction("acos", arguments);

                case "ArcTan":
                    return SteExpressionUtils.CreateFunction(
                        arguments.Length == 1 ? "atan" : "atan2",
                        arguments
                    );

                case "Sinh":
                    return SteExpressionUtils.CreateFunction("sinh", arguments);

                case "Cosh":
                    return SteExpressionUtils.CreateFunction("cosh", arguments);

                case "Tanh":
                    return SteExpressionUtils.CreateFunction("tanh", arguments);

                case "Log":
                    return SteExpressionUtils.CreateFunction(
                        "log", 
                        arguments.Length == 1 ? arguments : arguments.Reverse()
                    );

                case "Log10":
                    return SteExpressionUtils.CreateFunction("log10", arguments);

                case "Sqrt":
                    return SteExpressionUtils.CreateFunction("sqrt", arguments);

                case "Floor":
                    return SteExpressionUtils.CreateFunction("floor", arguments);

                case "Ceiling":
                    return SteExpressionUtils.CreateFunction("ceil", arguments);

                case "Round":
                    return SteExpressionUtils.CreateFunction("round", arguments);

                case "Min":
                    return SteExpressionUtils.CreateFunction("fmin", arguments);

                case "Max":
                    return SteExpressionUtils.CreateFunction("fmax", arguments);

                //case "Sign":
                //    return SteExpressionUtils.CreateFunction("Math.Sign", arguments);

                case "IntegerPart":
                    return SteExpressionUtils.CreateFunction("trunc", arguments);
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
                        return SteExpressionUtils.CreateSymbolicNumber("M_PI");

                    case "E":
                        return SteExpressionUtils.CreateSymbolicNumber("M_E");
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
                        return SteExpressionUtils.CreateSymbolicNumber("M_PI");

                    case "E":
                        return SteExpressionUtils.CreateSymbolicNumber("M_E");
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