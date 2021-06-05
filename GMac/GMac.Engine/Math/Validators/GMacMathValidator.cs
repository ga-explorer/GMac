using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Validators;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Multivectors;

namespace GMac.Engine.Math.Validators
{
    public abstract class GMacMathValidator : GaNumValidator
    {
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

        protected void ValidateEqual(string msg, GaNumSarMultivector mv1, GaSymMultivector mv2)
        {
            ValidateEqual(msg, mv1, mv2.ToNumeric());
        }

        protected void ValidateEqual(string msg, GaSymMultivector mv1, GaNumSarMultivector mv2)
        {
            ValidateEqual(msg, mv1, mv2.ToSymbolic());
        }
    }
}