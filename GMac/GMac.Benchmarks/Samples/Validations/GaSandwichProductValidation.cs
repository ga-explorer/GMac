using GeometricAlgebraNumericsLib.Validators;
using GMac.Benchmarks.Samples.Computations;

namespace GMac.Benchmarks.Samples.Validations
{
    public sealed class GaSandwichProductValidation : IGMacSample
    {
        public string Title
            => "Validate computation of sandwich product linear map";

        public string Description
            => "Validate computation of sandwich product linear map";


        public string Execute()
        {
            var validation = new GaNumSandwichProductValidator();

            validation.ShowValidatedResults = true;

            return validation.Validate();
        }
    }
}