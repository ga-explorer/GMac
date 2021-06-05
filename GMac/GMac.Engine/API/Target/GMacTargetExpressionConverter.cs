namespace GMac.Engine.API.Target
{
    ///// <summary>
    ///// This class converts a symbolic expression into a target language expression
    ///// </summary>
    //public abstract class GMacTargetExpressionConverter : ITargetCodeConverter<Expr>
    //{
    //    /// <summary>
    //    /// A dictionary holding functions for converting Mathematica constants to target language constants\code
    //    /// </summary>
    //    protected Dictionary<string, Func<string>> ConstantConversionMethods = new Dictionary<string, Func<string>>();

    //    /// <summary>
    //    /// A dictionary holding functions for converting Mathematica functions into target language functions\code
    //    /// </summary>
    //    protected Dictionary<string, Func<Expr[], string>> HeaderConversionMethods = new Dictionary<string, Func<Expr[], string>>();

    //    public TccLanguageComposer LanguageCodeComposer { get; private set; }

    //    public TccLanguageSyntaxFactory SyntaxFactory { get { return LanguageCodeComposer.SyntaxFactory; } }

    //    /// <summary>
    //    /// The code block containing a dictionary used to convert low-level variables names into 
    //    /// target code variables names to be used during expression conversion to target code
    //    /// </summary>
    //    public GMacCodeBlock ActiveCodeBlock { get; set; }


    //    /// <summary>
    //    /// The target language equivalent code for Mathematica Pi constant
    //    /// </summary>
    //    /// <returns></returns>
    //    protected abstract string Constant_Pi();

    //    /// <summary>
    //    /// The target language equivalent code for Mathematica E constant
    //    /// </summary>
    //    /// <returns></returns>
    //    protected abstract string Constant_E();


    //    protected abstract string GeneralSymbolicFunction(Expr expr);


    //    protected abstract string Rational(Expr[] args);

    //    protected abstract string Plus(Expr[] args);

    //    protected abstract string Minus(Expr[] args);

    //    protected abstract string Subtract(Expr[] args);

    //    protected abstract string Times(Expr[] args);

    //    protected abstract string Divide(Expr[] args);

    //    protected abstract string Power(Expr[] args);

    //    protected abstract string Abs(Expr[] args);

    //    protected abstract string Exp(Expr[] args);

    //    protected abstract string Sin(Expr[] args);

    //    protected abstract string Cos(Expr[] args);

    //    protected abstract string Tan(Expr[] args);

    //    protected abstract string ArcSin(Expr[] args);

    //    protected abstract string ArcCos(Expr[] args);

    //    protected abstract string ArcTan(Expr[] args);

    //    protected abstract string Sinh(Expr[] args);

    //    protected abstract string Cosh(Expr[] args);

    //    protected abstract string Tanh(Expr[] args);

    //    protected abstract string Log(Expr[] args);

    //    protected abstract string Log10(Expr[] args);

    //    protected abstract string Sqrt(Expr[] args);

    //    protected abstract string Floor(Expr[] args);

    //    protected abstract string Ceiling(Expr[] args);

    //    protected abstract string Round(Expr[] args);

    //    protected abstract string Min(Expr[] args);

    //    protected abstract string Max(Expr[] args);

    //    protected abstract string Sign(Expr[] args);

    //    protected abstract string IntegerPart(Expr[] args);


    //    protected GMacTargetExpressionConverter(TccLanguageComposer langComposer)
    //    {
    //        LanguageCodeComposer = langComposer;

    //        //Initialize Mathematica constants conversion dictionary
    //        ConstantConversionMethods.Add("Pi", Constant_Pi);
    //        ConstantConversionMethods.Add("E", Constant_E);

    //        //Initialize Mathematica functions conversion dictionary
    //        HeaderConversionMethods.Add("Rational", Rational);
    //        HeaderConversionMethods.Add("Plus", Plus);
    //        HeaderConversionMethods.Add("Minus", Minus);
    //        HeaderConversionMethods.Add("Subtract", Subtract);
    //        HeaderConversionMethods.Add("Times", Times);
    //        HeaderConversionMethods.Add("Divide", Divide);
    //        HeaderConversionMethods.Add("Power", Power);
    //        HeaderConversionMethods.Add("Abs", Abs);
    //        HeaderConversionMethods.Add("Exp", Exp);
    //        HeaderConversionMethods.Add("Sin", Sin);
    //        HeaderConversionMethods.Add("Cos", Cos);
    //        HeaderConversionMethods.Add("Tan", Tan);
    //        HeaderConversionMethods.Add("ArcSin", ArcSin);
    //        HeaderConversionMethods.Add("ArcCos", ArcCos);
    //        HeaderConversionMethods.Add("ArcTan", ArcTan);
    //        HeaderConversionMethods.Add("Sinh", Sinh);
    //        HeaderConversionMethods.Add("Cosh", Cosh);
    //        HeaderConversionMethods.Add("Tanh", Tanh);
    //        HeaderConversionMethods.Add("Log", Log);
    //        HeaderConversionMethods.Add("Log10", Log10);
    //        HeaderConversionMethods.Add("Sqrt", Sqrt);
    //        HeaderConversionMethods.Add("Floor", Floor);
    //        HeaderConversionMethods.Add("Ceiling", Ceiling);
    //        HeaderConversionMethods.Add("Round", Round);
    //        HeaderConversionMethods.Add("Min", Min);
    //        HeaderConversionMethods.Add("Max", Max);
    //        HeaderConversionMethods.Add("Sign", Sign);
    //        HeaderConversionMethods.Add("IntegerPart", IntegerPart);
    //    }


    //    /// <summary>
    //    /// Return a concatenated string of the converted Mathematica expressions into target language code
    //    /// </summary>
    //    /// <param name="args"></param>
    //    /// <param name="separator"></param>
    //    /// <returns></returns>
    //    protected string ToCode(IEnumerable<Expr> args, string separator)
    //    {
    //        return
    //            args
    //            .Select(ToCode)
    //            .Concatenate(separator);
    //    }

    //    /// <summary>
    //    /// Convert a given Mathematica expression to target language code
    //    /// </summary>
    //    /// <param name="expr"></param>
    //    /// <returns></returns>
    //    public string ToCode(Expr expr)
    //    {
    //        if (expr.Args.Length == 0)
    //        {
    //            var exprText = expr.ToString();

    //            if (expr.SymbolQ() == false) 
    //                return exprText;

    //            //Try convert a low-level variable name into a target variable name
    //            GMacCbVariable targetVar;

    //            if (ActiveCodeBlock != null && ActiveCodeBlock.VariablesDictionary.TryGetValue(exprText, out targetVar))
    //                return targetVar.TargetVariableName;

    //            //Try convert the Mathematica constant using the constants conversion dictionary
    //            Func<string> constantFunc;

    //            return 
    //                ConstantConversionMethods.TryGetValue(exprText, out constantFunc) 
    //                ? constantFunc() 
    //                : exprText;
    //        }

    //        //Try convert the Mathematica function using the functions conversion dictionary
    //        Func<Expr[], string> headerFunc;

    //        return 
    //            HeaderConversionMethods.TryGetValue(expr.Head.ToString(), out headerFunc) 
    //            ? headerFunc(expr.Args) 
    //            : GeneralSymbolicFunction(expr);
    //    }
    //}
}
