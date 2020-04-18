using System;
using GeometricAlgebraNumericsLib.Rendering.LaTeX;
using GeometricAlgebraSymbolicsLib.Frames;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Rendering.LaTeX;

namespace GMacBenchmarks2.Samples.Visualizations.Symbolic
{
    public static class Sample2
    {
        public static void Execute()
        {
            var frame = GaSymFrame.CreateOrthonormal("++++-");

            var renderer = new GaSymLaTeXRenderer(
                GaLaTeXBasisBladeForm.BasisVectorsSubscripts,
                "x", "y", "z", "p", "n"
            );

            var ep = GaSymMultivector.CreateBasisVector(
                frame.VSpaceDimension, 
                3
            );

            var en = GaSymMultivector.CreateBasisVector(
                frame.VSpaceDimension, 
                4
            );

            var no = (en - ep) / 2;
            var ni = en + ep;

            var mvA = GaSymMultivector.CreateVectorFromScalars(
                "Subscript[a,x]", "Subscript[a,y]", "Subscript[a,z]", "0", "0"
            );

            var mvB = no + mvA + (frame.Sp[mvA, mvA] / 2)[0] * ni;

            var latexno = "n_{o} = " + renderer.FormatMultivector(no);
            var latexni = "n_{\\infty} = " + renderer.FormatMultivector(ni);
            var latexA = "A = " + renderer.FormatMultivector(mvA);
            var latexB = "B = " + renderer.FormatMultivector(mvB);

            renderer.RenderMathToImageFile(
                "math.png",
                latexno, 
                latexni,
                latexA,
                latexB
            );

            Console.Out.WriteLine(latexno);
            Console.Out.WriteLine(latexni);
            Console.Out.WriteLine(latexA);
            Console.Out.WriteLine(latexB);
        }
    }
}