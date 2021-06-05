using GeometricAlgebraNumericsLib.Frames;
using GMac.Benchmarks.Samples.Computations;
using GMac.Engine.Math.Validators;

namespace GMac.Benchmarks.Samples.Validations
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