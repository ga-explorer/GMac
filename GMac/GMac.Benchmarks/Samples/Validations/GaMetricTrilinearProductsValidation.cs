using GeometricAlgebraNumericsLib.Frames;
using GMac.Benchmarks.Samples.Computations;
using GMac.Engine.Math.Validators;

namespace GMac.Benchmarks.Samples.Validations
{
    public sealed class GaMetricTrilinearProductsValidation : IGMacSample
    {
        public string Title
            => "Validate trilinear products on GA Multivectors";

        public string Description
            => "Validate trilinear products on GA Multivectors";


        public string Execute()
        {
            var validation = new GaMetricTrilinearProductsValidator();

            validation.NumericFrame = GaNumFrame.CreateEuclidean(5);
            //validation.NumericFrame = GaNumFrame.CreateOrthonormal("++--+");
            //validation.NumericFrame = GaNumFrame.CreateOrthogonal(2.4, -1.1, 0.98, 1.8, -2.3, -1.4);

            validation.ShowValidatedResults = false;

            return validation.Validate();
        }
    }
}