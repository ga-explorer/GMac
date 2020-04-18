using System;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using MathNet.Numerics.LinearAlgebra.Double;
using TextComposerLib.Text.Markdown;

namespace GeometricAlgebraNumericsLib.Validators
{
    public abstract class GaNumValidator
    {
        public static double NumericEpsilon { get; set; } 
            = Math.Pow(2, -26);


        protected GaRandomGenerator RandomGenerator { get; }
            = new GaRandomGenerator(10);

        public MarkdownComposer ReportComposer { get; } 
            = new MarkdownComposer();

        public bool ShowValidatedMessage { get; set; } = true;

        public bool ShowValidatedResults { get; set; } = true;


        public abstract string Validate();


        protected void ValidateEqual(string msg, Matrix m1, Matrix m2)
        {
            var diff = m2 - m1;
            if (diff.ForAll(i => i.IsNearZero(NumericEpsilon)))
            {
                if (!ShowValidatedMessage) 
                    return;

                ReportComposer
                    .AppendLineAtNewLine()
                    .AppendAtNewLine(msg)
                    .AppendLine("... << Validated >>");

                if (ShowValidatedResults)
                {
                    ReportComposer
                        .IncreaseIndentation();

                    ReportComposer
                        .AppendLineAtNewLine("Result: ")
                        .IncreaseIndentation()
                        .AppendLine(m1.ToString())
                        .DecreaseIndentation()
                        .AppendLine();

                    ReportComposer
                        .DecreaseIndentation();
                }

                return;
            }

            ReportComposer
                .AppendLineAtNewLine()
                .AppendAtNewLine(msg)
                .AppendLine("... << Invalid >>");

            ReportComposer
                .IncreaseIndentation();

            ReportComposer
                .AppendLineAtNewLine("First Value: ")
                .IncreaseIndentation()
                .AppendLine(m1.ToString())
                .DecreaseIndentation()
                .AppendLine();

            ReportComposer
                .AppendLineAtNewLine("Second Value: ")
                .IncreaseIndentation()
                .AppendLine(m2.ToString())
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

        protected void ValidateEqual(string msg, GaNumTerm mv1, GaNumTerm mv2)
        {
            if (mv1.GaSpaceDimension == mv2.GaSpaceDimension && mv1.BasisBladeId == mv2.BasisBladeId && (mv1.ScalarValue - mv2.ScalarValue).IsNearZero(NumericEpsilon))
            {
                if (!ShowValidatedMessage)
                    return;

                ReportComposer
                    .AppendLineAtNewLine()
                    .AppendAtNewLine(msg)
                    .AppendLine("... << Validated >>");

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

            ReportComposer
                .AppendLineAtNewLine()
                .AppendAtNewLine(msg)
                .AppendLine("... << Invalid >>");

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
                .DecreaseIndentation();
        }
        
        protected void ValidateEqual(string msg, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            var diff = mv2.GetSubtractionTerms(mv1).SumAsSarMultivector(mv1.VSpaceDimension);
            if (diff.IsNearZero(NumericEpsilon))
            {
                if (!ShowValidatedMessage)
                    return;

                ReportComposer
                    .AppendLineAtNewLine()
                    .AppendAtNewLine(msg)
                    .AppendLine("... << Validated >>");

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

            ReportComposer
                .AppendLineAtNewLine()
                .AppendAtNewLine(msg)
                .AppendLine("... << Invalid >>");

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

        protected void ValidateEqual(string msg, GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        {
            var diff = mv2 - mv1;
            if (diff.IsNearZero(NumericEpsilon))
            {
                if (!ShowValidatedMessage)
                    return;

                ReportComposer
                    .AppendLineAtNewLine()
                    .AppendAtNewLine(msg)
                    .AppendLine("... << Validated >>");

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

            ReportComposer
                .AppendLineAtNewLine()
                .AppendAtNewLine(msg)
                .AppendLine("... << Invalid >>");

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

        protected void ValidateEqual(string msg, GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        {
            var diff = mv2 - mv1;
            if (diff.IsNearZero(NumericEpsilon))
            {
                if (!ShowValidatedMessage)
                    return;

                ReportComposer
                    .AppendLineAtNewLine()
                    .AppendAtNewLine(msg)
                    .AppendLine("... << Validated >>");

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

            ReportComposer
                .AppendLineAtNewLine()
                .AppendAtNewLine(msg)
                .AppendLine("... << Invalid >>");

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
    }
}
