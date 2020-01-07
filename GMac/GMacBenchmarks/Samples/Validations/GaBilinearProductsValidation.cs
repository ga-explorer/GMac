﻿using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraSymbolicsLib.Frames;
using GMac.GMacMath.Validators;
using GMacBenchmarks.Samples.Computations;

namespace GMacBenchmarks.Samples.Validations
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

            validation.SymbolicFrame = GaSymFrame.CreateConformal(5);
            validation.NumericFrame = GaNumFrame.CreateConformal(5);

            validation.ShowValidatedResults = false;

            return validation.Validate();
        }
    }
}