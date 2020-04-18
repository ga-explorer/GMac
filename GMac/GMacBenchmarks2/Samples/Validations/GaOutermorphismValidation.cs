using GMacBenchmarks2.Samples.Computations;
using GeometricAlgebraNumericsLib.Validators;

namespace GMacBenchmarks2.Samples.Validations
{
    public sealed class GaOutermorphismValidation : IGMacSample
    {
        public string Title 
            => "Validate several methods of applying outermorphisms";

        public string Description
            => "Validate several methods of applying outermorphisms";


        public string Execute()
        {
            var validation = new GaNumOutermorphismValidator();

            validation.ShowValidatedResults = false;

            return validation.Validate();
        }
    }
}
