using GeometricAlgebraNumericsLib.Validators;
using GMac.Benchmarks.Samples.Computations;

namespace GMac.Benchmarks.Samples.Validations
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
