using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Frames;
using GMac.GMacMath.Validators;

namespace GMacTests.Validations
{
    public sealed class GaBilinearProductsValidationTest : IGMacTest
    {
        public string Title { get; } = "Validate main bilinear products on GA Multivectors";

        public string Execute()
        {
            var validation = new GaBilinearProductsValidator();

            validation.SymbolicFrame = GaSymFrame.CreateConformal(5);
            validation.NumericFrame = GaNumFrame.CreateConformal(5);

            validation.ShowValidatedResults = false;

            return validation.Validate();
        }
    }
}