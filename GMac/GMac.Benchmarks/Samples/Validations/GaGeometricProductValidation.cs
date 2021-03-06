﻿using GeometricAlgebraNumericsLib.Frames;
using GMac.Benchmarks.Samples.Computations;
using GMac.Engine.Math.Validators;

namespace GMac.Benchmarks.Samples.Validations
{
    public sealed class GaGeometricProductValidation : IGMacSample
    {
        public string Title
            => "Validate geometric product of 3 basis blades";

        public string Description
            => "Validate geometric product of 3 basis blades";


        public string Execute()
        {
            var vSpaceDim = 5;
            var signature = new double[] {1.2, 3, -5.4, 0.4, -9.1};

            var validation = new GaGeometricProductValidator
            {
                NumericFrame = GaNumFrame.CreateOrthogonal(signature),
                ShowValidatedMessage = false,
                ShowValidatedResults = false
            };

            return validation.Validate();
        }
    }
}