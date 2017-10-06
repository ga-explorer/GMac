namespace GMac.GMacAPI.Target.CSharp
{
    //public sealed class CSharpExpressionConverter : GMacTargetExpressionConverter
    //{
    //    internal CSharpExpressionConverter(TccCSharpCodeComposer langComposer)
    //        : base(langComposer)
    //    {
    //    }


    //    protected override string Constant_Pi()
    //    {
    //        return "Math.PI";
    //    }

    //    protected override string Constant_E()
    //    {
    //        return "Math.E";
    //    }


    //    private string GeneralSymbolicFunction(string funcName, IEnumerable<Expr> args, string separator = ", ")
    //    {
    //        var exprCode = new StringBuilder();

    //        exprCode
    //            .Append(funcName)
    //            .Append("(")
    //            .Append(ToCode(args, separator))
    //            .Append(")");

    //        return exprCode.ToString();
    //    }

    //    private string UnarySymbolicFunction(string funcName, Expr arg)
    //    {
    //        var exprCode = new StringBuilder();

    //        exprCode
    //            .Append(funcName)
    //            .Append("(")
    //            .Append(ToCode(arg))
    //            .Append(")");

    //        return exprCode.ToString();
    //    }


    //    protected override string GeneralSymbolicFunction(Expr expr)
    //    {
    //        return GeneralSymbolicFunction("MathHelper." + expr.Head, expr.Args);
    //    }

    //    protected override string Rational(Expr[] args)
    //    {
    //        var a = double.Parse(args[0].ToString());
            
    //        var b = double.Parse(args[1].ToString());

    //        return (a / b).ToString("G");
    //    }

    //    protected override string Plus(Expr[] args)
    //    {
    //        return GeneralSymbolicFunction("", args, " + ");
    //    }

    //    protected override string Minus(Expr[] args)
    //    {
    //        return "-" + ToCode(args[0]);
    //    }

    //    protected override string Subtract(Expr[] args)
    //    {
    //        return "(" + ToCode(args[0]) + " - " + ToCode(args[1]) + ")";
    //    }

    //    protected override string Times(Expr[] args)
    //    {
    //        return GeneralSymbolicFunction("", args, " * ");
    //    }

    //    protected override string Divide(Expr[] args)
    //    {
    //        return "(" + ToCode(args[0]) + " / " + ToCode(args[1]) + ")";
    //    }

    //    protected override string Power(Expr[] args)
    //    {
    //        return GeneralSymbolicFunction("Math.Pow", args);
    //    }

    //    protected override string Abs(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Abs", args[0]);
    //    }

    //    protected override string Exp(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Exp", args[0]);
    //    }

    //    protected override string Sin(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Sin", args[0]);
    //    }

    //    protected override string Cos(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Cos", args[0]);
    //    }

    //    protected override string Tan(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Tan", args[0]);
    //    }

    //    protected override string ArcSin(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Asin", args[0]);
    //    }

    //    protected override string ArcCos(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Acos", args[0]);
    //    }

    //    protected override string ArcTan(Expr[] args)
    //    {
    //        return 
    //            args.Length == 1 
    //            ? UnarySymbolicFunction("Math.Atan", args[0]) 
    //            : GeneralSymbolicFunction("Math.Atan2", args.Reverse());
    //    }

    //    protected override string Sinh(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Sinh", args[0]);
    //    }

    //    protected override string Cosh(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Cosh", args[0]);
    //    }

    //    protected override string Tanh(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Tanh", args[0]);
    //    }

    //    protected override string Log(Expr[] args)
    //    {
    //        return 
    //            args.Length == 1 
    //            ? UnarySymbolicFunction("Math.Log", args[0]) 
    //            : GeneralSymbolicFunction("Math.Log", args.Reverse());
    //    }

    //    protected override string Log10(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Log10", args[0]);
    //    }

    //    protected override string Sqrt(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Sqrt", args[0]);
    //    }

    //    protected override string Floor(Expr[] args)
    //    {
    //        return 
    //            args.Length == 1 
    //            ? UnarySymbolicFunction("Math.Floor", args[0]) 
    //            : GeneralSymbolicFunction("MathHelper.Floor", args);
    //    }

    //    protected override string Ceiling(Expr[] args)
    //    {
    //        return 
    //            args.Length == 1 
    //            ? UnarySymbolicFunction("Math.Ceiling", args[0]) 
    //            : GeneralSymbolicFunction("MathHelper.Ceiling", args);
    //    }

    //    protected override string Round(Expr[] args)
    //    {
    //        return 
    //            args.Length == 1 
    //            ? UnarySymbolicFunction("Math.Round", args[0]) 
    //            : GeneralSymbolicFunction("MathHelper.Round", args);
    //    }

    //    protected override string Min(Expr[] args)
    //    {
    //        return 
    //            GeneralSymbolicFunction(args.Length == 2 ? "Math.Min" : "MathHelper.Min", args);
    //    }

    //    protected override string Max(Expr[] args)
    //    {
    //        return GeneralSymbolicFunction(args.Length == 2 ? "Math.Max" : "MathHelper.Max", args);
    //    }

    //    protected override string Sign(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Sign", args[0]);
    //    }

    //    protected override string IntegerPart(Expr[] args)
    //    {
    //        return UnarySymbolicFunction("Math.Truncate", args[0]);
    //    }
    //}
}
