using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;

namespace GMac.Engine.Math.Validators
{
    public sealed class GaMetricTrilinearProductsValidator : GMacMathValidator
    {
        public GaNumFrame NumericFrame { get; set; } 
            = GaNumFrame.CreateEuclidean(5);

        private void ValidateNumericFrame()
        {
            if (NumericFrame == null)
                return;

            ReportComposer.AppendHeader("Numeric Trilinear Products Validations");

            var metric = NumericFrame.BaseOrthogonalMetric;

            var mv1 = 
                RandomGenerator
                    .GetNumFullMultivectorTerms(NumericFrame.VSpaceDimension)
                    .CreateSarMultivector(NumericFrame.VSpaceDimension);

            var mv2 = 
                RandomGenerator
                    .GetNumFullMultivectorTerms(NumericFrame.VSpaceDimension)
                    .CreateSarMultivector(NumericFrame.VSpaceDimension);

            var mv3 = 
                RandomGenerator
                    .GetNumFullMultivectorTerms(NumericFrame.VSpaceDimension)
                    .CreateSarMultivector(NumericFrame.VSpaceDimension);

            var resultMv1 = mv1
                .GetLoopEGpTerms(mv2)
                .SumAsSarMultivector(NumericFrame.VSpaceDimension)
                .GetLoopEGpTerms(mv3)
                .SumAsSarMultivector(NumericFrame.VSpaceDimension);

            var resultMv2 = mv1
                .GetGbtEGpGpLaTerms(mv2, mv3)
                .SumAsSarMultivector(NumericFrame.VSpaceDimension);

            ValidateEqual("(mv1 egp mv2) egp mv3", resultMv1, resultMv2);


            resultMv1 = mv1
                .GetLoopOpTerms(mv2)
                .SumAsSarMultivector(NumericFrame.VSpaceDimension)
                .GetLoopELcpTerms(mv3)
                .SumAsSarMultivector(NumericFrame.VSpaceDimension);

            resultMv2 = mv1
                .GetGbtEOpLcpLaTerms(mv2, mv3)
                .SumAsSarMultivector(NumericFrame.VSpaceDimension);

            ValidateEqual("(mv1 op mv2) elcp mv3", resultMv1, resultMv2);
        }

        public override string Validate()
        {
            ValidateNumericFrame();

            return ReportComposer.ToString();
        }
    }
}