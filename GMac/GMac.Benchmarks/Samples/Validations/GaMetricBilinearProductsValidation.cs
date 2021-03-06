﻿using GeometricAlgebraNumericsLib.Frames;
using GMac.Benchmarks.Samples.Computations;
using GMac.Engine.Math.Validators;

namespace GMac.Benchmarks.Samples.Validations
{
    public sealed class GaMetricBilinearProductsValidation : IGMacSample
    {
        public string Title
            => "Validate main bilinear products on GA Multivectors";

        public string Description
            => "Validate main bilinear products on GA Multivectors";


        public string Execute()
        {
            var validation = new GaMetricBilinearProductsValidator();

            //validation.NumericFrame = GaNumFrame.CreateOrthonormal("++--+");
            validation.NumericFrame = GaNumFrame.CreateOrthogonal(2.4, -1.1, 0.98, 1.8, -2.3, -1.4);

            validation.ShowValidatedResults = false;

            return validation.Validate();
        }
    }
}