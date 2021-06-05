using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;

namespace GMac.Engine.Math.Validators
{
    public sealed class GaMetricBilinearProductsValidator : GMacMathValidator
    {
        public GaNumFrame NumericFrame { get; set; }

        private void ValidateNumericFrame()
        {
            if (NumericFrame == null)
                return;

            ReportComposer.AppendHeader("Numeric Bilinear Products Validations");

            var metric = NumericFrame.BaseOrthogonalMetric;

            var mv1 = 
                RandomGenerator
                    .GetNumFullMultivectorTerms(NumericFrame.VSpaceDimension)
                    .CreateSarMultivector(NumericFrame.VSpaceDimension);

            var mv2 = 
                RandomGenerator
                    .GetNumFullMultivectorTerms(NumericFrame.VSpaceDimension)
                    .CreateSarMultivector(NumericFrame.VSpaceDimension);

            var resultMv1 = mv1.GetGbtOpTerms(mv2).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            var resultMv2 = mv1.GetLoopOpTerms(mv2).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            ValidateEqual("Op", resultMv1, resultMv2);

            resultMv1 = mv1.GetGbtGpTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            resultMv2 = mv1.GetLoopGpTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            ValidateEqual("EGp", resultMv1, resultMv2);

            resultMv1 = mv1.GetGbtLcpTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            resultMv2 = mv1.GetLoopLcpTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            ValidateEqual("Lcp", resultMv1, resultMv2);

            resultMv1 = mv1.GetGbtRcpTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            resultMv2 = mv1.GetLoopRcpTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            ValidateEqual("Rcp", resultMv1, resultMv2);

            resultMv1 = mv1.GetGbtFdpTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            resultMv2 = mv1.GetLoopFdpTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            ValidateEqual("Fdp", resultMv1, resultMv2);

            resultMv1 = mv1.GetGbtHipTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            resultMv2 = mv1.GetLoopHipTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            ValidateEqual("Hip", resultMv1, resultMv2);

            resultMv1 = mv1.GetGbtAcpTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            resultMv2 = mv1.GetLoopAcpTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            ValidateEqual("Acp", resultMv1, resultMv2);

            resultMv1 = mv1.GetGbtCpTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            resultMv2 = mv1.GetLoopCpTerms(mv2, metric).SumAsSarMultivector(NumericFrame.VSpaceDimension);
            ValidateEqual("Cp", resultMv1, resultMv2);
        }

        public override string Validate()
        {
            ValidateNumericFrame();

            return ReportComposer.ToString();
        }
    }
}