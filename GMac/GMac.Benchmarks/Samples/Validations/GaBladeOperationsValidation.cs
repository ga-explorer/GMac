using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Frames;
using GMac.Benchmarks.Samples.Computations;
using GMac.Engine.Math.Validators;

namespace GMac.Benchmarks.Samples.Validations
{
    public sealed class GaBladeOperationsValidation : IGMacSample
    {
        public string Title 
            => "Validate main operations on GA Blades";

        public string Description
            => "Validate main operations on GA Blades";


        public string Execute()
        {
            var validation = new GaBladeOperationsValidator();

            validation.SymbolicFrame = GaSymFrame.CreateConformal(5);
            validation.NumericFrame = GaNumFrame.CreateConformal(5);

            validation.ShowValidatedResults = false;

            return validation.Validate();
        }
    }
}
