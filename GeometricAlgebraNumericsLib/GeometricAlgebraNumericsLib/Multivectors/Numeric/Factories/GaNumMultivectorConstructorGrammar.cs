using Irony.Interpreter;
using Irony.Interpreter.Evaluator;
using Irony.Parsing;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories
{
    public class GaNumMultivectorConstructorGrammar : InterpretedLanguageGrammar
    {
        //Examples:
        //
        //  -1.3<>
        //  -1.3<>, 1.2<1>, -4.6<1,3>

        public GaNumMultivectorConstructorGrammar()
            : base(caseSensitive: true)
        {
            // 1. Terminals
            var number = TerminalFactory.CreateCSharpNumber("number");
            number.Options = NumberOptions.AllowSign;

            var comma1 = ToTerm(",");

            // 2. Non-terminals
            var basisVectorName1 = new NonTerminal("basisVectorName1");
            
            var basisBladeId1 = new NonTerminal("basisBladeId1");

            var basisBladeDef1 = new NonTerminal("basisBladeDef1");

            var multivector1 = new NonTerminal("multivector1");

            var term1 = new NonTerminal("term1");

            basisVectorName1.Rule = 
                ToTerm("1") | "2" | "3" | "4" | "5" | "6" | "7" | "8" | 
                "9" | "10" | "11" | "12" | "13" | "14" | "15" | "16" | 
                "17" | "18" | "19" | "20" | "21" | "22" | "23" | "24" |
                "25" | "26" | "27" | "28" | "29" | "30";

            basisBladeId1.Rule = MakeStarRule(basisBladeId1, comma1, basisVectorName1);

            basisBladeDef1.Rule = ToTerm("<") + basisBladeId1 + ">";

            term1.Rule = number + basisBladeDef1;

            multivector1.Rule = MakePlusRule(multivector1, comma1, term1);

            // Set grammar root
            Root = multivector1;

            // 5. Punctuation and transient terms
            MarkPunctuation("(", ")", "<", ">", ",", ";");
            RegisterBracePair("(", ")");
            RegisterBracePair("<", ">");
            MarkTransient(basisBladeDef1, basisVectorName1);

            // 7. Syntax error reporting
            AddToNoReportGroup("(", "<");
            AddToNoReportGroup(NewLine);

            //9. Language flags. 
            // Automatically add NewLine before EOF so that our BNF rules work correctly when there's no final line break in source
            LanguageFlags = LanguageFlags.NewLineBeforeEOF;
        }

        public override LanguageRuntime CreateRuntime(LanguageData language)
        {
            return new ExpressionEvaluatorRuntime(language);
        }

        //#region Running in Grammar Explorer
        //private static GaPoTMultivectorFactory _evaluator;
        //public override string RunSample(RunSampleArgs args)
        //{
        //    if (_evaluator == null)
        //    {
        //        _evaluator = new GaPoTMultivectorFactory(this);
        //        _evaluator.Globals.Add("null", _evaluator.Runtime.NoneValue);
        //        _evaluator.Globals.Add("true", true);
        //        _evaluator.Globals.Add("false", false);

        //    }
        //    _evaluator.ClearOutput();
        //    //for (int i = 0; i < 1000; i++)  //for perf measurements, to execute 1000 times
        //    _evaluator.Evaluate(args.ParsedSample);
        //    return _evaluator.GetOutput();
        //}
        //#endregion
    }
}