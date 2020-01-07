﻿using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Maps.Outermorphisms;
using GMacBenchmarks.Samples.Computations;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using TextComposerLib.Text.Linear;

namespace GMacBenchmarks.Samples.Memory.Numeric
{
    public sealed class Sample2 : IGMacSample
    {
        public string Title { get; }
            = "Memory requirements of outermorphism stack when mapping multivectors";

        public string Description { get; }
            = "Memory requirements of outermorphism stack when mapping multivectors";


        public string Execute()
        {
            var randGen = new GaRandomGenerator(10);
            var composer = new LinearTextComposer();

            composer.AppendLine("Outermorphism Stack Sizes:");
            for (var n = 3; n <= 15; n++)
            {
                var matrix = DenseMatrix.Create(n, n, 0);
                for (var row = 0; row < n; row++)
                    for (var col = 0; col < n; col++)
                        matrix[row, col] = randGen.GetScalar(-10, 10);

                var om = GaNumOutermorphism.Create(matrix);

                Console.WriteLine("n = " + n);

                //for (var id = 0; id <= n; id++)
                //{
                //    var mv = randGen.GetNumKVector(1 << n, id);
                //    var result = om[mv];
                //}

                var mv = randGen.GetNumMultivectorFull(1 << n);
                var result = om[mv];
            }

            return composer.ToString();
        }
    }
}