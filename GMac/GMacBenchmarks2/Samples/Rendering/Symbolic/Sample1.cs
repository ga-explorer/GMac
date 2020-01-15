using System;
using GeometricAlgebraNumericsLib.Rendering.LaTeX;
using GeometricAlgebraSymbolicsLib.Frames;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Rendering.LaTeX;

namespace GMacBenchmarks2.Samples.Rendering.Symbolic
{
    public static class Sample1
    {
        public static void Execute()
        {
            var frame = GaSymFrame.CreateEuclidean(3);

            var renderer = new GaSymLaTeXRenderer(
                GaLaTeXBasisBladeForm.BasisVectorsOuterProduct,
                "e_x", "e_y", "e_z"
            );

            var mvA = GaSymMultivector.CreateVectorFromScalars(
                "Subscript[a,1]", "Subscript[a,2]", "Subscript[a,3]"
            );

            var mvB = GaSymMultivector.CreateVectorFromScalars(
                "Subscript[b,1]", "Subscript[b,2]", "Subscript[b,3]"
            );

            var mvAB = frame.Gp[mvA, mvB];

            var latexA = "A=" + renderer.FormatMultivector(mvA);
            var latexB = "B=" + renderer.FormatMultivector(mvB);
            var latexAB = "AB=" + renderer.FormatMultivector(mvAB);

            Console.Out.WriteLine(latexA);
            Console.Out.WriteLine(latexB);
            Console.Out.WriteLine(latexAB);
        }
    }
}
