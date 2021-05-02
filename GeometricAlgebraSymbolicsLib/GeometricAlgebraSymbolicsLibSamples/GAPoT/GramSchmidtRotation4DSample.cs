using System;
using CodeComposerLib.LaTeX;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;

namespace GeometricAlgebraSymbolicsLibSamples.GAPoT
{
    public static class GramSchmidtRotation4DSample
    {
        public static void ComputePower()
        {
            var n = 4;

            var uFrame = GaPoTSymFrame.CreateBasisFrame(n);
            var cFrame = GaPoTSymFrame.CreateGramSchmidtFrame(n, n - 1, out var eFrame);

            var rotorsSequence = 
                GaPoTSymRotorsSequence.CreateFromOrthonormalFrames(
                    cFrame, 
                    uFrame
                );

            //var finalRotor = 
            //    rotorsSequence.GetFinalRotor();

            var v = "'-4'<1>, '8'<2>, '-3'<3>, '-1'<4>".GaPoTSymParseVector();
            var i = "'-9'<1>, '2'<2>, '-5'<3>, '12'<4>".GaPoTSymParseVector();

            var m = v.Gp(i);

            var vr = rotorsSequence.Rotate(v);
            var ir = rotorsSequence.Rotate(i);

            var mr = vr.Gp(ir);

            Console.WriteLine($"m  = {m.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine($"mr = {mr.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();
        }

        public static void Execute()
        {
            var n = 4;

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

            var finalNumericalRotor =
                finalRotor.ScalarsToNumerical();

            var R1 = rotorsSequence[1].Gp(rotorsSequence[0]);
            var R2 = rotorsSequence[2];

            var autoVector = GaPoTSymVector.CreateAutoVector(n);

            var rotatedAutoVector =
                rotorsSequence.Rotate(autoVector);

            var v1 = "Sqrt[2] * U * Cos[w * t]".SimplifyToExpr();
            var v2 = "Sqrt[2] * U * Cos[w * t - 1 * 2 * Pi / 4]".SimplifyToExpr();
            var v3 = "Sqrt[2] * U * Cos[w * t - 2 * 2 * Pi / 4]".SimplifyToExpr();
            var v4 = Mfs.Minus[Mfs.Plus[v1, v2, v3]].Evaluate();

            var inputVector = new GaPoTSymVector()
                .AddTerm(1, v1)
                .AddTerm(2, v2)
                .AddTerm(3, v3)
                .AddTerm(4, v4);

            Console.WriteLine("Final Rotor:");
            Console.WriteLine($"{finalRotor.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            //Console.WriteLine($"{finalRotor.TermsToText()}");
            Console.WriteLine($"{finalNumericalRotor.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine("R1:");
            Console.WriteLine($"{R1.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine("R2:");
            Console.WriteLine($"{R2.TermsToLaTeX().GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine("R2 R1 - R:");
            Console.WriteLine($"{(R2.Gp(R1) - finalRotor).TermsToText()}");
            Console.WriteLine($"{(R1.Gp(R2) - finalRotor).TermsToText()}");
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