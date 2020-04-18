using GeometricAlgebraNumericsLib.Validators;
using GMacBenchmarks2.Samples.Computations;

namespace GMacBenchmarks2.Samples.Validations
{
    public sealed class GaMultivectorValidation : IGMacSample
    {
        public string Title 
            => "Validate several methods of storing multivectors";

        public string Description
            => "Validate several methods of storing multivectors";


        public string Execute()
        {
            var validation = new GaNumMultivectorValidator();

            validation.ShowValidatedResults = false;

            return validation.Validate();
        }
    }
}