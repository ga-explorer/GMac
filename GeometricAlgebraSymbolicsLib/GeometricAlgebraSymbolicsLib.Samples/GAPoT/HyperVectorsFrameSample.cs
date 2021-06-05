using System;
using CodeComposerLib.LaTeX;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Samples.GAPoT
{
    public static class HyperVectorsFrameSample
    {
        public static void DisplayFrame(GaPoTSymFrame frame)
        {
            var n = frame.Count;

            Console.WriteLine("Frame Matrix:");
            Console.WriteLine(frame.GetMatrix().ToArrayExpr().FullSimplify().GetLaTeXDisplayEquation());
            Console.WriteLine();

            Console.WriteLine("Frame Inner Products Matrix:");
            Console.WriteLine(frame.GetInnerProductsMatrix().ToArrayExpr().FullSimplify().GetLaTeXDisplayEquation());
            Console.WriteLine();

            Console.WriteLine("Frame Inner Angles Matrix:");
            Console.WriteLine(frame.GetInnerAnglesMatrix().ToArrayExpr().FullSimplify().GetLaTeXDisplayEquation());
            Console.WriteLine();

            //Console.WriteLine("Auto Vector Projection on Frame:");
            //Console.WriteLine("$" + GaPoTSymVector.CreateAutoVector(n).GetProjectionOnFrame(frame).TermsToLaTeX() + "$");
            //Console.WriteLine();
        }

        public static void Execute1()
        {
            var refVectorIndex = 0;

            for (var n = 2; n <= 10; n++)
            {
                Console.Write(@"\section*{" + n + "-Dimensions}");
                Console.WriteLine();

                var uFrame = 
                    GaPoTSymFrame.CreateBasisFrame(n);

                var uPseudoScalar = 
                    GaPoTSymMultivector
                        .CreateZero()
                        .SetTerm((1UL << n) - 1, Expr.INT_ONE);

                var eFrame = 
                    GaPoTSymFrame.CreateKirchhoffFrame(n, refVectorIndex);

                //var pFrame = 
                //    uFrame.GetProjectionOnFrame(eFrame);

                var hFrame = GaPoTSymFrame.CreateHyperVectorsFrame(n);

                var cFrame = GaPoTSymFrame.CreateGramSchmidtFrame(n, refVectorIndex);

                var rotorsSequence = GaPoTSymRotorsSequence.CreateFromFrames(
                    n, 
                    cFrame,
                    uFrame
                );

                var rotationMatrix = rotorsSequence.GetFinalMatrixExpr(n);
                
                Console.WriteLine("FBD Matrix:");
                Console.WriteLine(hFrame.GetMatrix().ToArrayExpr().FullSimplify().GetLaTeXDisplayEquation());
                Console.WriteLine();

                Console.WriteLine(@"Gram Schmidt Frame Rotation Matrix:");
                Console.WriteLine(rotationMatrix.FullSimplify().GetLaTeXDisplayEquation());
                Console.WriteLine();
            }
        }

        public static void Execute2()
        {
            var refVectorIndex = 0;

            for (var n = 3; n <= 5; n++)
            {
                Console.Write(@"\section*{" + n + "-Dimensions}");
                Console.WriteLine();

                var uFrame = 
                    GaPoTSymFrame.CreateBasisFrame(n);

                var uPseudoScalar = 
                    GaPoTSymMultivector
                        .CreateZero()
                        .SetTerm((1UL << n) - 1, Expr.INT_ONE);

                var eFrame = 
                    GaPoTSymFrame.CreateKirchhoffFrame(n, refVectorIndex);

                var pFrame = 
                    uFrame.GetProjectionOnFrame(eFrame);

                var hFrame = GaPoTSymFrame.CreateHyperVectorsFrame(n);

                var cFrame = GaPoTSymFrame.CreateGramSchmidtFrame(n, refVectorIndex);

                var phRotorsSequence = GaPoTSymRotorsSequence.CreateFromFrames(
                    n, 
                    pFrame,
                    hFrame
                );

                var cuRotorsSequence = GaPoTSymRotorsSequence.CreateFromOrthonormalFrames(
                    cFrame,
                    uFrame
                );

                var phRotationMatrix = phRotorsSequence.GetFinalMatrixExpr(n);
                var cuRotationMatrix = cuRotorsSequence.GetFinalMatrixExpr(n);

                Console.WriteLine("FBD Matrix:");
                Console.WriteLine(hFrame.GetMatrix().ToArrayExpr().FullSimplify().GetLaTeXDisplayEquation());
                Console.WriteLine();

                Console.WriteLine(@"p-h Rotation Matrix:");
                Console.WriteLine(phRotationMatrix.FullSimplify().GetLaTeXDisplayEquation());
                Console.WriteLine();

                Console.WriteLine(@"c-u Rotation Matrix:");
                Console.WriteLine(cuRotationMatrix.FullSimplify().GetLaTeXDisplayEquation());
                Console.WriteLine();
            }
        }

        public static void Execute3()
        {
            var refVectorIndex = 0;

            for (var n = 3; n <= 3; n++)
            {
                Console.Write(@"\section*{" + n + "-Dimensions}");
                Console.WriteLine();

                var uFrame =
                    GaPoTSymFrame.CreateBasisFrame(n);

                var uPseudoScalar =
                    GaPoTSymMultivector
                        .CreateZero()
                        .SetTerm((1UL << n) - 1, Expr.INT_ONE);

                var eFrame =
                    GaPoTSymFrame.CreateKirchhoffFrame(n, refVectorIndex);

                var pFrame =
                    uFrame.GetProjectionOnFrame(eFrame);

                var hFrame = GaPoTSymFrame.CreateHyperVectorsFrame(n);

                var cFrame = GaPoTSymFrame.CreateGramSchmidtFrame(n, refVectorIndex);
                //var cFrame = GaPoTSymFrame.CreateClarkeFrame(n);

                var kirchhoffVector = GaPoTSymVector.CreateUnitAutoVector(n);

                var phRotor0 = GaPoTSymMultivector.CreateSimpleRotor(
                    n,
                    pFrame[0],
                    kirchhoffVector,
                    hFrame[0],
                    uFrame[n - 1]
                );

                //Apply this rotor to the pFrame and cFrame
                var pFrame1 = pFrame.ApplyRotor(phRotor0);
                

                //Console.WriteLine("pFrame Matrix:");

                //Find rotor sequence to align pFrame to hFrame
                var phRotorsSequence = 
                    GaPoTSymRotorsSequence.CreateFromFrames(n, pFrame1, hFrame);

                phRotorsSequence[0] = phRotor0;

                Console.WriteLine("pFrame to hFrame Rotors Sequence:");
                Console.WriteLine(phRotorsSequence.ToLaTeXEquationsArrays("R^{ph}", @"\mu"));
                Console.WriteLine();
                
                if (n < 4)
                {
                    var phRotor = phRotorsSequence.GetFinalRotor().ScalarsToNumerical();
                    var (phAngle, phBlade) = phRotor.GetSimpleRotorAngleBlade();

                    var phNormal = phBlade.OrthogonalComplement(uPseudoScalar);

                    Console.WriteLine("pFrame to hFrame Final Rotor:");
                    
                    Console.WriteLine("Angle:");
                    Console.WriteLine(phAngle.GetLaTeX().GetLaTeXInlineEquation());
                    Console.WriteLine();

                    Console.WriteLine("Blade:");
                    Console.WriteLine(phBlade.ToLaTeXEquationsArray("B^{cu}",@"\mu"));
                    Console.WriteLine();

                    Console.WriteLine("Normal:");
                    Console.WriteLine(phNormal.ToLaTeXEquationsArray("n^{cu}",@"\mu"));
                    Console.WriteLine();
                }


                var cuRotor0 = GaPoTSymMultivector.CreateSimpleRotor(
                    n,
                    cFrame[0],
                    kirchhoffVector,
                    uFrame[0],
                    uFrame[n - 1]
                );

                var cFrame1 = cFrame.ApplyRotor(cuRotor0);

                var cuRotorsSequence = 
                    GaPoTSymRotorsSequence.CreateFromFrames(n,cFrame1, uFrame);

                cuRotorsSequence[0] = cuRotor0;

                Console.WriteLine("cFrame to uFrame Rotors Sequence:");
                Console.WriteLine(cuRotorsSequence.ToLaTeXEquationsArrays("R^{cu}", @"\mu"));
                Console.WriteLine();

                if (n < 4)
                {
                    var cuRotor = cuRotorsSequence.GetFinalRotor().ScalarsToNumerical();
                    var (cuAngle, cuBlade) = cuRotor.GetSimpleRotorAngleBlade();

                    var cuNormal = cuBlade.OrthogonalComplement(uPseudoScalar);

                    Console.WriteLine("cFrame to uFrame Final Rotor:");
                    Console.WriteLine();
                    
                    Console.WriteLine("Angle:");
                    Console.WriteLine(cuAngle.GetLaTeX().GetLaTeXInlineEquation());
                    Console.WriteLine();

                    Console.WriteLine("Blade:");
                    Console.WriteLine(cuBlade.ToLaTeXEquationsArray("B^{cu}",@"\mu"));
                    Console.WriteLine();

                    Console.WriteLine("Normal:");
                    Console.WriteLine(cuNormal.ToLaTeXEquationsArray("n^{cu}",@"\mu"));
                    Console.WriteLine();
                }
            }
        }

        public static void Execute()
        {
            var refVectorIndex = 0;

            for (var n = 3; n <= 6; n++)
            {
                Console.Write(@"\section{Dimensions: " + n + "}");
                Console.WriteLine();

                var uFrame = 
                    GaPoTSymFrame.CreateBasisFrame(n);

                var uPseudoScalar = 
                    GaPoTSymMultivector
                        .CreateZero()
                        .SetTerm((1UL << n) - 1, Expr.INT_ONE);

                var eFrame = 
                    GaPoTSymFrame.CreateKirchhoffFrame(n, refVectorIndex);

                var pFrame = 
                    uFrame.GetProjectionOnFrame(eFrame);

                Console.Write(@"\subsection{Uniform Kirchhoff Frame:}");
                Console.WriteLine();

                DisplayFrame(pFrame);
                
                continue;

                var hFrame = GaPoTSymFrame.CreateHyperVectorsFrame(n);

                Console.Write(@"\subsection{Hyper-Vectors Frame:}");
                Console.WriteLine();

                DisplayFrame(hFrame);

                //var pFrame1 = pFrame
                //    .GetSubFrame(0, n - 1)
                //    .PrependVector(GaPoTSymVector.CreateAutoVector(n))
                //    .GetOrthogonalFrame(true);

                //var fbdFrame1 = fbdFrame
                //    .GetSubFrame(0, n - 1)
                //    .PrependVector(GaPoTSymVector.CreateAutoVector(n))
                //    .GetOrthogonalFrame(true);

                //var rs = GaPoTSymRotorsSequence.Create(
                //    pFrame1.GetRotorsToFrame(fbdFrame1)
                //);


                var rotorsSequence = GaPoTSymRotorsSequence.CreateFromFrames(
                    n, 
                    pFrame,
                    hFrame
                );

                var pFrame2 = rotorsSequence.Rotate(pFrame);

                Console.Write(@"\subsection{Rotated Projected Frame:}");
                Console.WriteLine();

                DisplayFrame(pFrame2);

                Console.WriteLine("Rotors Sequence:");

                for (var i = 0; i < rotorsSequence.Count; i++)
                {
                    var rotorEquation = 
                        rotorsSequence[i].ToLaTeXEquationsArray(
                            $"R_{{{i + 1}}}", 
                            @"\mu"
                        );

                    Console.WriteLine(rotorEquation);
                    Console.WriteLine();
                }

                Console.WriteLine("Rotation Matrix:");
                Console.WriteLine(rotorsSequence.GetFinalMatrixExpr(n).FullSimplify().GetLaTeXDisplayEquation());
                Console.WriteLine();
            }
        }
    }
}