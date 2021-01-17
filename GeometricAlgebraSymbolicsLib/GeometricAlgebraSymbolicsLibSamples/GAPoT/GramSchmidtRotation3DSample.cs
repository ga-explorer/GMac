using System;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;

namespace GeometricAlgebraSymbolicsLibSamples.GAPoT
{
    public static class GramSchmidtRotation3DSample
    {
        public static void Execute()
        {
            var n = 3;

            var uFrame = GaPoTSymFrame.CreateBasisFrame(n);
            var eFrame = GaPoTSymFrame.CreateKirchhoffFrame(n, 0);
            var cFrame = GaPoTSymFrame.CreateGramSchmidtFrame(n);

            var rotorsSequence = 
                GaPoTSymRotorsSequence.Create(
                    uFrame.GetRotorsToFrame(cFrame)
                );

            var finalRotor = 
                rotorsSequence.GetFinalRotor();

            
            //var v1 = "Sqrt[2] * U * Cos[w * t]".GaPoTSymSimplify();
            //var v2 = "Sqrt[2] * U * Cos[w * t - 2 * Pi / 3]".GaPoTSymSimplify();
            //var v3 = "Sqrt[2] * U * Cos[w * t + 2 * Pi / 3]".GaPoTSymSimplify();

            var autoVector = GaPoTSymVector.CreateAutoVector(n);

            var rotatedAutoVector =
                rotorsSequence.Rotate(autoVector);

            var v1 = "U * Cos[w * t]".GaPoTSymSimplify();
            var v2 = "U * Cos[w * t - 2 * Pi / 3]".GaPoTSymSimplify();
            var v3 = "U * Cos[w * t + 2 * Pi / 3]".GaPoTSymSimplify();

            var inputVector = new GaPoTSymVector()
                .AddTerm(1, v1)
                .AddTerm(2, v2)
                .AddTerm(3, v3);

            Console.WriteLine("Final Rotor:");
            Console.WriteLine($"{finalRotor.TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine("Rotated Auto Vector:");
            Console.WriteLine($"{rotatedAutoVector.TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine("Input Vector:");
            Console.WriteLine($"{inputVector.TermsToLaTeX()}");
            Console.WriteLine();

            var rotatedVector =
                rotorsSequence.Rotate(inputVector);

            Console.WriteLine("Rotated Vector:");
            Console.WriteLine($"{rotatedVector.TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine("Rotated Vector . Rotated Auto Vector:");
            Console.WriteLine($"{rotatedVector.DotProduct(rotatedAutoVector).GetLaTeXInlineEquation()}");
            Console.WriteLine();

            var eI = eFrame.GetPseudoScalar();

            Console.WriteLine("Input Vector inside e-frame: ");
            Console.WriteLine($"{inputVector.Op(eI).TermsToLaTeX()}");
            Console.WriteLine();
            
            Console.WriteLine("Rotated Vector inside e-frame: ");
            Console.WriteLine($"{rotatedVector.Op(eI).TermsToLaTeX()}");
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