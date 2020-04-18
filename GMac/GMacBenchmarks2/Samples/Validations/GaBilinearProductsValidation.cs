using GeometricAlgebraNumericsLib.Frames;
using GMac.GMacMath.Validators;
using GMacBenchmarks2.Samples.Computations;

namespace GMacBenchmarks2.Samples.Validations
{
    public sealed class GaBilinearProductsValidation : IGMacSample
    {
        public string Title 
            => "Validate main bilinear products on GA Multivectors";

        public string Description 
            => "Validate main bilinear products on GA Multivectors";


        public string Execute()
        {
            var validation = new GaBilinearProductsValidator();

            //validation.SymbolicFrame = GaSymFrame.CreateConformal(5);
            validation.NumericFrame = GaNumFrame.CreateOrthonormal("++--+");

            validation.ShowValidatedResults = false;

            return validation.Validate();
        }
    }
}