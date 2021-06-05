using Irony.Interpreter;
using Irony.Interpreter.Evaluator;
using Irony.Parsing;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public class GaPoTSymBiversorConstructorGrammar : InterpretedLanguageGrammar
    {
        //Examples:
        //GAPoT bivector using terms form:
        //  '-1.3'<>, '1.2'<1,2>, '-4.6'<3,4>
        public GaPoTSymBiversorConstructorGrammar()
            : base(caseSensitive: true)
        {
            // 1. Terminals
            var number = TerminalFactory.CreateCSharpNumber("number");
            number.Options = NumberOptions.AllowSign;
            
            var scalar = TerminalFactory.CreateCSharpString("scalar");
            scalar.AddStartEnd("'", StringOptions.NoEscapes | StringOptions.AllowsDoubledQuote);

            var comma1 = ToTerm(",");

            // 2. Non-terminals
            var bivector = new NonTerminal("bivector");
            var bivectorTerm = new NonTerminal("bivectorTerm");
            var bivectorTerm0 = new NonTerminal("bivectorTerm0");
            var bivectorTerm2 = new NonTerminal("bivectorTerm2");

            bivectorTerm0.Rule = scalar + "<" + ">";
            bivectorTerm2.Rule = scalar + "<" + number + comma1 + number + ">";
            bivectorTerm.Rule = bivectorTerm0 | bivectorTerm2;
            bivector.Rule = MakePlusRule(bivector, comma1, bivectorTerm);

            // Set grammar root
            Root = bivector;

            // 5. Punctuation and transient terms
            MarkPunctuation("<", ">", ",");
            RegisterBracePair("<", ">");
            MarkTransient(bivectorTerm);

            // 7. Syntax error reporting
            AddToNoReportGroup("<");
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