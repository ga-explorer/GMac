using System;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Symbolic.Multivectors;
using TextComposerLib.Text.Markdown;

namespace GMac.GMacMath.Validators
{
    public abstract class GMacMathValidator
    {
        public static double NumericEpsilon { get; set; } 
            = Math.Pow(2, -26);


        protected GMacRandomGenerator RandomGenerator { get; }
            = new GMacRandomGenerator(10);

        public MarkdownComposer ReportComposer { get; } 
            = new MarkdownComposer();

        public bool ShowValidatedResults { get; set; } = true;


        public abstract string Validate();


        protected void ValidateEqual(string msg, GaSymMultivector mv1, GaSymMultivector mv2)
        {
            ReportComposer.AppendLineAtNewLine();
            ReportComposer.AppendAtNewLine(msg);

            var diff = mv2 - mv1;
            if (diff.IsNearNumericZero(NumericEpsilon))
            {
                ReportComposer.AppendLine("... << Validated >>");

                if (ShowValidatedResults)
                {
                    ReportComposer
                        .IncreaseIndentation();

                    ReportComposer
                        .AppendLineAtNewLine("Result: ")
                        .IncreaseIndentation()
                        .AppendLine(mv1.ToString())
                        .DecreaseIndentation()
                        .AppendLine();

                    ReportComposer
                        .DecreaseIndentation();
                }

                return;
            }

            ReportComposer.AppendLine("... << Invalid >>");

            ReportComposer
                .IncreaseIndentation();

            ReportComposer
                .AppendLineAtNewLine("First Value: ")
                .IncreaseIndentation()
                .AppendLine(mv1.ToString())
                .DecreaseIndentation()
                .AppendLine();

            ReportComposer
                .AppendLineAtNewLine("Second Value: ")
                .IncreaseIndentation()
                .AppendLine(mv2.ToString())
                .DecreaseIndentation()
                .AppendLine();

            ReportComposer
                .AppendLineAtNewLine("Difference: ")
                .IncreaseIndentation()
                .AppendLine(diff.ToString())
                .DecreaseIndentation()
                .AppendLine();

            ReportComposer
                .DecreaseIndentation();
        }

        protected void ValidateEqual(string msg, GaNumMultivector mv1, GaNumMultivector mv2)
        {
            ReportComposer.AppendLineAtNewLine();
            ReportComposer.AppendAtNewLine(msg);

            var diff = mv2 - mv1;
            if (diff.IsNearZero(NumericEpsilon))
            {
                ReportComposer.AppendLine("... << Validated >>");

                if (ShowValidatedResults)
                {
                    ReportComposer
                        .IncreaseIndentation();

                    ReportComposer
                        .AppendLineAtNewLine("Result: ")
                        .IncreaseIndentation()
                        .AppendLine(mv1.ToString())
                        .DecreaseIndentation()
                        .AppendLine();

                    ReportComposer
                        .DecreaseIndentation();
                }

                return;
            }

            ReportComposer.AppendLine("... << Invalid >>");

            ReportComposer
                .IncreaseIndentation();

            ReportComposer
                .AppendLineAtNewLine("First Value: ")
                .IncreaseIndentation()
                .AppendLine(mv1.ToString())
                .DecreaseIndentation()
                .AppendLine();

            ReportComposer
                .AppendLineAtNewLine("Second Value: ")
                .IncreaseIndentation()
                .AppendLine(mv2.ToString())
                .DecreaseIndentation()
                .AppendLine();

            ReportComposer
                .AppendLineAtNewLine("Difference: ")
                .IncreaseIndentation()
                .AppendLine(diff.ToString())
                .DecreaseIndentation()
                .AppendLine();

            ReportComposer
                .DecreaseIndentation();
        }

        protected void ValidateEqual(string msg, GaNumMultivector mv1, GaSymMultivector mv2)
        {
            ValidateEqual(msg, mv1, mv2.ToNumeric());
        }

        protected void ValidateEqual(string msg, GaSymMultivector mv1, GaNumMultivector mv2)
        {
            ValidateEqual(msg, mv1, mv2.ToSymbolic());
        }
    }
}