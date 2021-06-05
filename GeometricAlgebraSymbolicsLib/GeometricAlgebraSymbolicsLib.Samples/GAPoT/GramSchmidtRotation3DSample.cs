using System;
using CodeComposerLib.LaTeX;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;

namespace GeometricAlgebraSymbolicsLib.Samples.GAPoT
{
    public static class GramSchmidtRotation3DSample
    {


        public static void Execute()
        {
            var n = 3;

            var uFrame = GaPoTSymFrame.CreateBasisFrame(n);
            var cFrame = GaPoTSymFrame.CreateGramSchmidtFrame(n, n - 1, out var eFrame);

            var eFrameEquation = eFrame.ToLaTeXEquationsArray(
                "e", 
                @"\mu"
            );

            Console.WriteLine("e-frame:");
            Console.WriteLine(eFrameEquation);
            Console.WriteLine();

            var cFrameEquation = 
                cFrame.ToLaTeXEquationsArray(
                    "c", 
                    @"\mu"
                );

            Console.WriteLine("c-frame:");
            Console.WriteLine(cFrameEquation);
            Console.WriteLine();

            var cFrameMatrix = 
                cFrame.GetMatrix().ToArrayExpr().GetLaTeXDisplayEquation();

            Console.WriteLine("c-frame matrix:");
            Console.WriteLine(cFrameMatrix);
            Console.WriteLine();

            var rotorsSequence = 
                GaPoTSymRotorsSequence.CreateFromOrthonormalFrames(
                    cFrame, 
                    uFrame
                );

            var finalRotor = 
                rotorsSequence.GetFinalRotor();

            
            var autoVector = GaPoTSymVector.CreateAutoVector(n);

            var rotatedAutoVector =
                rotorsSequence.Rotate(autoVector);

            //var v1 = "Sqrt[2] * U * Cos[w * t]".GaPoTSymSimplify();
            //var v2 = "Sqrt[2] * U * Cos[w * t - 2 * Pi / 3]".GaPoTSymSimplify();
            //var v3 = "Sqrt[2] * U * Cos[w * t + 2 * Pi / 3]".GaPoTSymSimplify();

            var v1 = "U * Cos[w * t]".SimplifyToExpr();
            var v2 = "U * Cos[w * t - 2 * Pi / 3]".SimplifyToExpr();
            var v3 = "U * Cos[w * t + 2 * Pi / 3]".SimplifyToExpr();

            var inputVector = new GaPoTSymVector()
                .AddTerm(1, v1)
                .AddTerm(2, v2)
                .AddTerm(3, v3);

            Console.WriteLine("Final Rotor:");
            Console.WriteLine($"{finalRotor.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine($"{finalRotor.TermsToText()}");
            Console.WriteLine();

            Console.WriteLine("Rotated Auto Vector:");
            Console.WriteLine($"{rotatedAutoVector.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine("Input Vector:");
            Console.WriteLine($"{inputVector.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            var rotatedVector =
                rotorsSequence.Rotate(inputVector);

            Console.WriteLine("Rotated Vector:");
            Console.WriteLine($"{rotatedVector.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine("Rotated Vector . Rotated Auto Vector:");
            Console.WriteLine($"{rotatedVector.DotProduct(rotatedAutoVector).GetLaTeXInlineEquation()}");
            Console.WriteLine();

            var eI = eFrame.GetPseudoScalar();

            Console.WriteLine("Input Vector inside e-frame: ");
            Console.WriteLine($"{inputVector.Op(eI).TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();
            
            Console.WriteLine("Rotated Vector inside e-frame: ");
            Console.WriteLine($"{rotatedVector.Op(eI).TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();


            Console.WriteLine("Input Vector squared norm: ");
            Console.WriteLine($"{inputVector.Norm2()}");
            Console.WriteLine();

            Console.WriteLine("Rotated Vector squared norm: ");
            Console.WriteLine($"{rotatedVector.Norm2()}");
            Console.WriteLine();
        }
    }
}