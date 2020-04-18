using GeometricAlgebraNumericsLib.Validators;
using GMacBenchmarks2.Samples.Computations;

namespace GMacBenchmarks2.Samples.Validations
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