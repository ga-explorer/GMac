﻿using Irony.Interpreter;
using Irony.Interpreter.Evaluator;
using Irony.Parsing;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public class GaPoTSymVectorConstructorGrammar : InterpretedLanguageGrammar
    {
        //Examples:
        //Single Phase GAPoT vector using terms form:
        //  '-1.3'<1>, '1.2'<3>, '-4.6'<5>
        //
        //Single Phase GAPoT vector using polar form:
        //  p('233.92', '−1.57') <1,2>, p('120', '0') <3,4>
        //
        //Single Phase GAPoT vector using rectangular form:
        //  r('10', '20') <1,2>, r('30', '0') <3,4>
        //
        //All the above can be mixed together
        //
        //Multi-phase GAPoT vector:
        //  ['-1.3'<1>, '1.2'<3>, '-4.6'<5>] <a>; [p('233.92', '−1.57') <1,2>] <b>; [r('10', '20') <1,2>, r('30', '0') <3,4>] <c>
        public GaPoTSymVectorConstructorGrammar()
            : base(caseSensitive: true)
        {
            // 1. Terminals
            var number = TerminalFactory.CreateCSharpNumber("number");
            number.Options = NumberOptions.AllowSign;

            var scalar = TerminalFactory.CreateCSharpString("scalar");
            scalar.AddStartEnd("'", StringOptions.NoEscapes | StringOptions.AllowsDoubledQuote);

            var comma1 = ToTerm(",");
            var comma2 = ToTerm(";");

            // 2. Non-terminals
            var vector = new NonTerminal("vector");

            var spVector = new NonTerminal("spVector");
            var mpVector = new NonTerminal("mpVector");

            var spTerm = new NonTerminal("spTerm");
            var spPolarPhasor = new NonTerminal("spPolarPhasor");
            var spRectPhasor = new NonTerminal("spRectPhasor");

            var mpBasisVectorName = new NonTerminal("mpBasisVectorName");

            var spVectorElement = new NonTerminal("spVectorElement");
            var mpVectorElement = new NonTerminal("mpVectorElement");

            spTerm.Rule = scalar + "<" + number + ">";

            spPolarPhasor.Rule =
                ToTerm("p") + "(" + scalar + comma1 + scalar + ")" +
                "<" + number + comma1 + number + ">";
            
            spRectPhasor.Rule =
                ToTerm("r") + "(" + scalar + comma1 + scalar + ")" +
                "<" + number + comma1 + number + ">";

            spVectorElement.Rule = spTerm | spPolarPhasor | spRectPhasor;

            spVector.Rule = MakePlusRule(spVector, comma1, spVectorElement);

            mpBasisVectorName.Rule = 
                ToTerm("a") | "b" | "c" | "d" | "e" | "f" | "g" | "h" | "i" | "j" | "k" | "l" |
                "m" | "n" | "o" | "p" | "q" | "r" | "s" | "t" | "u" | "v" | "w" | "x" | "y" | "z";

            mpVectorElement.Rule = 
                ToTerm("[") + spVector + "]" +
                "<" + mpBasisVectorName + ">";

            mpVector.Rule = MakePlusRule(mpVector, comma2, mpVectorElement);

            vector.Rule = spVector | mpVector;

            // Set grammar root
            Root = vector;

            // 5. Punctuation and transient terms
            MarkPunctuation("(", ")", "[", "]", "<", ">", ",", ";");
            RegisterBracePair("(", ")");
            RegisterBracePair("[", "]");
            RegisterBracePair("<", ">");
            MarkTransient(vector, mpBasisVectorName, spVectorElement);

            // 7. Syntax error reporting
            AddToNoReportGroup("(", "<", "[");
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