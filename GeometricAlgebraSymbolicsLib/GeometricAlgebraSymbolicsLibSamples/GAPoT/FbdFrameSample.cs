using System;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLibSamples.GAPoT
{
    public static class FbdFrameSample
    {
        public static void DisplayFrame(GaPoTSymFrame frame)
        {
            var n = frame.Count;

            Console.WriteLine("Frame Matrix:");
            Console.WriteLine(frame.GetMatrix().ToArrayExpr().GetLaTeXDisplayEquation());
            Console.WriteLine();

            Console.WriteLine("Frame Inner Products Matrix:");
            Console.WriteLine(frame.GetInnerProductsMatrix().ToArrayExpr().GetLaTeXDisplayEquation());
            Console.WriteLine();

            Console.WriteLine("Frame Inner Angles Matrix:");
            Console.WriteLine(frame.GetInnerAnglesMatrix().ToArrayExpr().GetLaTeXDisplayEquation());
            Console.WriteLine();

            Console.WriteLine("Auto Vector Projection on Frame:");
            Console.WriteLine("$" + GaPoTSymVector.CreateAutoVector(n).GetProjectionOnFrame(frame).TermsToLaTeX() + "$");
            Console.WriteLine();
        }

        public static void Execute()
        {
            var refVectorIndex = 0;

            for (var n = 3; n <= 5; n++)
            {
                var uFrame = 
                    GaPoTSymFrame.CreateBasisFrame(n);

                var uPseudoScalar = 
                    GaPoTSymMultivector
                        .CreateZero()
                        .SetTerm((1 << n) - 1, Expr.INT_ONE);

                var eFrame = 
                    GaPoTSymFrame.CreateKirchhoffFrame(n, refVectorIndex);

                var pFrame = 
                    uFrame.GetProjectionOnFrame(eFrame);

                var fbdFrame = GaPoTSymFrame.CreateFbdFrame(n);

                //TODO: Can you get the rotation without the orthogonalization?
                var pFrame1 = pFrame
                    .GetSubFrame(0, n - 1)
                    .PrependVector(GaPoTSymVector.CreateAutoVector(n))
                    .GetOrthogonalFrame(true);

                var fbdFrame1 = fbdFrame
                    .GetSubFrame(0, n - 1)
                    .PrependVector(GaPoTSymVector.CreateAutoVector(n))
                    .GetOrthogonalFrame(true);

                var rs = GaPoTSymRotorsSequence.Create(
                    pFrame1.GetRotorsToFrame(fbdFrame1)
                );

                var pFrame2 = rs.Rotate(pFrame);

                Console.Write(@"\section{Dimensions: " + n + "}");
                Console.WriteLine();

                Console.Write(@"\subsection{FBD Frame:}");
                Console.WriteLine();

                DisplayFrame(fbdFrame);

                Console.Write(@"\subsection{Projected Frame:}");
                Console.WriteLine();

                DisplayFrame(pFrame);

                Console.Write(@"\subsection{Rotated Projected Frame:}");
                Console.WriteLine();

                DisplayFrame(pFrame2);

                Console.WriteLine("Rotors Sequence:");

                for (var i = 0; i < rs.Count; i++)
                {
                    var rotorEquation = 
                        rs[i].ToLaTeXEquationsArray(
                            $"R_{{{i + 1}}}", 
                            @"\mu"
                        );

                    Console.WriteLine(rotorEquation);
                    Console.WriteLine();
                }
            }
        }
    }
}