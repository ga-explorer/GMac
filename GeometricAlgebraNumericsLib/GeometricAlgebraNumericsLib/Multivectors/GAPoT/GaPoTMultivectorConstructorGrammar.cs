using Irony.Interpreter;
using Irony.Interpreter.Evaluator;
using Irony.Parsing;

namespace GeometricAlgebraNumericsLib.Multivectors.GAPoT
{
    //public class GaPoTMultivectorConstructorGrammar : InterpretedLanguageGrammar
    //{
    //    //Examples:
    //    //Scaled Bases Blade terms:
    //    //  (-1.3<>)<>
    //    //  (-1.3<>, 1.2<1>, -4.6<1,3>)<>
    //    //  (-1.3<>, 1.2<1>, -4.6<1,3>)<>; (-9.3<>, 12.4<1,2,3>, -14.2<2,4>)<a,b,c>
    //    //  
    //    //Exponential terms:
    //    //  (233.92 exp(−1.57 <1,2>) <1>, 0.46 exp(−2.61 <3,4>) <3>)<>
    //    public GaPoTMultivectorConstructorGrammar()
    //        : base(caseSensitive: true)
    //    {
    //        // 1. Terminals
    //        var number = TerminalFactory.CreateCSharpNumber("number");
    //        number.Options = NumberOptions.AllowSign;

    //        var comma1 = ToTerm(",");
    //        var comma2 = ToTerm(";");

    //        // 2. Non-terminals
    //        var basisVectorName1 = new NonTerminal("basisVectorName1");
    //        var basisVectorName2 = new NonTerminal("basisVectorName2");
            
    //        var basisBladeId1 = new NonTerminal("basisBladeId1");
    //        var basisBladeId2 = new NonTerminal("basisBladeId2");

    //        var basisBladeDef1 = new NonTerminal("basisBladeDef1");
    //        var basisBladeDef2 = new NonTerminal("basisBladeDef2");

    //        var multivector = new NonTerminal("multivector");

    //        var multivector1 = new NonTerminal("multivector1");
    //        var multivector2 = new NonTerminal("multivector2");

    //        var expTerm1 = new NonTerminal("expTerm1");
    //        var sbbTerm1 = new NonTerminal("sbbTerm1");

    //        var term1 = new NonTerminal("term1");
    //        var term2 = new NonTerminal("term2");

    //        basisVectorName1.Rule = ToTerm("1") | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" | "10" | "11" | "12" | "13" | "14" | "15" | "16";
    //        basisVectorName2.Rule = ToTerm("a") | "b" | "c" | "d" | "e" | "f" | "g" | "h" | "i" | "j" | "k" | "l" | "m" | "n" | "o" | "p";

    //        basisBladeId1.Rule = MakeStarRule(basisBladeId1, comma1, basisVectorName1);
    //        basisBladeId2.Rule = MakeStarRule(basisBladeId2, comma1, basisVectorName2);

    //        basisBladeDef1.Rule = ToTerm("<") + basisBladeId1 + ">";
    //        basisBladeDef2.Rule = ToTerm("<") + basisBladeId2 + ">";

    //        sbbTerm1.Rule = number + basisBladeDef1;
    //        expTerm1.Rule = number + ToTerm("exp") + "(" + number + "<" + basisVectorName1 + "," + basisVectorName1 + ">" + ")" + "<" + basisVectorName1 + ">";

    //        term1.Rule = sbbTerm1 | expTerm1;
    //        term2.Rule = ToTerm("(") + multivector1 + ")" + basisBladeDef2;

    //        multivector1.Rule = MakePlusRule(multivector1, comma1, term1);
    //        multivector2.Rule = MakePlusRule(multivector2, comma2, term2);

    //        multivector.Rule = multivector1 | multivector2;

    //        // Set grammar root
    //        Root = multivector;

    //        // 5. Punctuation and transient terms
    //        MarkPunctuation("(", ")", "<", ">", ",", ";", "exp");
    //        RegisterBracePair("(", ")");
    //        RegisterBracePair("<", ">");
    //        MarkTransient(term1, basisBladeDef1, basisBladeDef2, basisVectorName1, basisVectorName2);

    //        // 7. Syntax error reporting
    //        AddToNoReportGroup("(", "<");
    //        AddToNoReportGroup(NewLine);

    //        //9. Language flags. 
    //        // Automatically add NewLine before EOF so that our BNF rules work correctly when there's no final line break in source
    //        LanguageFlags = LanguageFlags.NewLineBeforeEOF;
    //    }

    //    public override LanguageRuntime CreateRuntime(LanguageData language)
    //    {
    //        return new ExpressionEvaluatorRuntime(language);
    //    }

    //    //#region Running in Grammar Explorer
    //    //private static GaPoTMultivectorFactory _evaluator;
    //    //public override string RunSample(RunSampleArgs args)
    //    //{
    //    //    if (_evaluator == null)
    //    //    {
    //    //        _evaluator = new GaPoTMultivectorFactory(this);
    //    //        _evaluator.Globals.Add("null", _evaluator.Runtime.NoneValue);
    //    //        _evaluator.Globals.Add("true", true);
    //    //        _evaluator.Globals.Add("false", false);

    //    //    }
    //    //    _evaluator.ClearOutput();
    //    //    //for (int i = 0; i < 1000; i++)  //for perf measurements, to execute 1000 times
    //    //    _evaluator.Evaluate(args.ParsedSample);
    //    //    return _evaluator.GetOutput();
    //    //}
    //    //#endregion
    //}
}