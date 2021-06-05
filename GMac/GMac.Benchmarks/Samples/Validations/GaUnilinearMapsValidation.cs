using GMac.Benchmarks.Samples.Computations;
using GMac.Engine.Math.Validators;

namespace GMac.Benchmarks.Samples.Validations
{
    public sealed class GaUnilinearMapsValidation : IGMacSample
    {
        public string Title
            => "Validate unilinear maps on GA Multivectors";

        public string Description
            => "Validate unilinear maps on GA Multivectors";


        public string Execute()
        {
            var validation = new GaUnilinearMapsValidator();

            validation.VSpaceDimension = 6;
            
            validation.ShowValidatedResults = false;

            return validation.Validate();
        }
    }
}