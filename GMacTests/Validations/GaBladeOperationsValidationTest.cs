using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMac.GMacMath.Numeric.Frames;
using GMac.GMacMath.Symbolic.Frames;
using GMac.GMacMath.Validators;

namespace GMacTests.Validations
{
    public sealed class GaBladeOperationsValidationTest : IGMacTest
    {
        public string Title { get; } = "Validate main operations on GA Blades";

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
