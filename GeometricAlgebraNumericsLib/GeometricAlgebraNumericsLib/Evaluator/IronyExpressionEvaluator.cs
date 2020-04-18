namespace GeometricAlgebraNumericsLib.Evaluator
{
    //public class IronyExpressionEvaluator
    //{
    //    public IronyExpressionEvaluatorGrammar Grammar { get; }

    //    public Parser Parser { get; }
        
    //    public LanguageData Language { get; }
        
    //    public LanguageRuntime Runtime { get; }
        
    //    public ScriptApp App { get; }

    //    public IDictionary<string, object> Globals 
    //        => App.Globals;


    //    //Default constructor, creates default evaluator 
    //    public IronyExpressionEvaluator() 
    //        : this(new IronyExpressionEvaluatorGrammar())
    //    {
    //    }

    //    //Default constructor, creates default evaluator 
    //    public IronyExpressionEvaluator(IronyExpressionEvaluatorGrammar grammar)
    //    {
    //        Grammar = grammar;
    //        Language = new LanguageData(Grammar);
    //        Parser = new Parser(Language);
    //        Runtime = Grammar.CreateRuntime(Language);
    //        App = new ScriptApp(Runtime);
    //    }


    //    public object Evaluate(string script)
    //    {
    //        var result = App.Evaluate(script);
    //        return result;
    //    }

    //    public object Evaluate(ParseTree parsedScript)
    //    {
    //        var result = App.Evaluate(parsedScript);
    //        return result;
    //    }


    //    //Evaluates again the previously parsed/evaluated script
    //    public object Evaluate()
    //    {
    //        return App.Evaluate();
    //    }

    //    public void ClearOutput()
    //    {
    //        App.ClearOutputBuffer();
    //    }

    //    public string GetOutput()
    //    {
    //        return App.GetOutput();
    //    }
    //}
}