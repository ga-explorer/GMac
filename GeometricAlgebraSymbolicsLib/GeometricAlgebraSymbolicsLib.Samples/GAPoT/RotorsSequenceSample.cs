using System;
using System.Linq;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;

namespace GeometricAlgebraSymbolicsLib.Samples.GAPoT
{
    public static class RotorsSequenceSample
    {
        public static void DisplayFrame(GaPoTSymFrame sourceFrame, GaPoTSymFrame targetFrame)
        {
            var n = targetFrame.Count;

            var matrixExpr = 
                Mfs.ToRadicals[Mfs.MatrixForm[targetFrame.GetMatrix(n).ToArrayExpr()]];

            var orthoFrameEquation = targetFrame.ToLaTeXEquationsArray(
                    "c", 
                    @"\mu"
                );

                Console.WriteLine(orthoFrameEquation);
                Console.WriteLine();

                Console.WriteLine("Rotation Matrix:");
                Console.WriteLine();

                Console.WriteLine($@"\[{matrixExpr.GetLaTeX()}\]");
                Console.WriteLine();
                
                var anglesTextList = sourceFrame
                    .GetAnglesToFrame(targetFrame)
                    .Select(a => a.GetLaTeX())
                    .ToArray();

                Console.WriteLine("Angles between frames vectors:");
                foreach (var angleText in anglesTextList)
                {
                    Console.WriteLine($@"\[{angleText}\]");
                    Console.WriteLine();
                }

                var autoVectorProjection = GaPoTSymVector
                    .CreateAutoVector(n)
                    .GetProjectionOnFrame(targetFrame);

                Console.WriteLine("Projection of auto-vector:");
                Console.WriteLine($@"\[{autoVectorProjection.TermsToLaTeX()}\]");
                Console.WriteLine();

                var rotorsSequence = 
                    GaPoTSymRotorsSequence.CreateFromOrthonormalFrames(
                        sourceFrame,
                        targetFrame
                    );

                //if (!rotorsSequence.ValidateRotation(sourceFrame, targetFrame))
                //    throw new InvalidOperationException("Error in rotation sequence");

                //for (var i = 0; i < sourceFrame.Count - 1; i++)
                //{
                //    var f1 = sourceFrame.GetSubFrame(0, i + 1);
                //    var f2 = targetFrame.GetSubFrame(0, i + 1);
                //    var rs = rotorsSequence.GetSubSequence(0, i + 1);

                //    if (!rs.ValidateRotation(f1, f2))
                //        throw new InvalidOperationException("Error in rotation sequence");
                //}

                Console.WriteLine("Rotors Sequence:");
                Console.WriteLine();

                for (var i = 0; i < rotorsSequence.Count; i++)
                {
                    var rotorEquation = rotorsSequence[i].ToLaTeXEquationsArray(
                        $"R_{i + 1}",
                        @"\mu"
                    );

                    Console.WriteLine(rotorEquation);
                    Console.WriteLine();
                }

                if (n > 4) 
                    return;
                
                //var rotor21 = rotorsSequence[1].Gp(rotorsSequence[0]);
                //var rotor21Equation = rotor21.ToLaTeXEquationsArray(
                //    $"R_{{21}}=R_2 R_1",
                //    @"\mu"
                //);

                //Console.WriteLine(rotor21Equation);
                //Console.WriteLine();

                //var rotor32 = rotorsSequence[2].Gp(rotorsSequence[1]);
                //var rotor32Equation = rotor32.ToLaTeXEquationsArray(
                //    $"R_{{32}}=R_3 R_2",
                //    @"\mu"
                //);

                //Console.WriteLine(rotor32Equation);
                //Console.WriteLine();

                Console.WriteLine("Final Rotor $R = R_3 R_{21} = R_{32} R_1$:");
                Console.WriteLine();

                var finalRotor = rotorsSequence.GetFinalRotor();
                var finalRotorEquation = finalRotor.ToLaTeXEquationsArray(
                    "R", 
                    @"\mu"
                );

                Console.WriteLine(finalRotorEquation);
                Console.WriteLine();

                //Console.WriteLine($"m{n} = {matrixExpr};");
                //Console.WriteLine($"MatrixForm[FullSimplify[m{n}]]");
                //Console.WriteLine($"MatrixForm[FullSimplify[Dot[m{n}, Transpose[m{n}]]]]");
                //Console.WriteLine($"Pseudo-Scalar = {frame.GetPseudoScalar()}");
                //Console.WriteLine();
        }

        public static void DisplayClarkeFrames()
        {
            for (var n = 2; n <= 5; n++)
            {
                var sourceFrame = GaPoTSymFrame.CreateBasisFrame(n);
                var targetFrame = GaPoTSymFrame.CreateClarkeFrame(n);
                
                Console.Write(@"\section{" + n + "-Dimensional Case}");
                Console.WriteLine();
                
                Console.WriteLine("Orthonormal Frame:");
                Console.WriteLine();
                
                DisplayFrame(sourceFrame, targetFrame);
            }
        }

        public static void DisplayGramSchmidtFrames()
        {
            for (var n = 2; n <= 4; n++)
            {
                var sourceFrame = GaPoTSymFrame.CreateBasisFrame(n);

                var targetFrame = GaPoTSymFrame.CreateGramSchmidtFrame(
                    n, 
                    n - 1, 
                    out var kirchhoffFrame
                );
                
                Console.Write(@"\section{" + n + "-Dimensional Case}");
                Console.WriteLine();
                
                Console.WriteLine("Kirchhoff Frame:");
                Console.WriteLine();
                
                var kirchhoffFrameEquation = kirchhoffFrame.ToLaTeXEquationsArray(
                    "e", 
                    @"\mu"
                );

                Console.WriteLine(kirchhoffFrameEquation);
                Console.WriteLine();
                
                Console.WriteLine("Orthonormal Frame:");
                Console.WriteLine();
                
                DisplayFrame(sourceFrame, targetFrame);
            }
        }

        public static void DisplayGramSchmidtToClarkeMatrices()
        {
            for (var n = 2; n <= 12; n++)
            {
                var matrixExpr1 = GaPoTSymFrame.CreateGramSchmidtFrame(n).GetMatrix(n).ToArrayExpr();
                var matrixExpr2 = GaPoTSymFrame.CreateClarkeFrame(n).GetMatrix(n).ToArrayExpr();
                var matrixExpr = Mfs.Dot[matrixExpr2, Mfs.Transpose[matrixExpr1]];

                Console.WriteLine($"m{n} = {matrixExpr};");
                Console.WriteLine($"MatrixForm[FullSimplify[m{n}]]");
                Console.WriteLine($"MatrixForm[FullSimplify[Dot[m{n}, Transpose[m{n}]]]]");
                Console.WriteLine();
            }
        }

        public static void DisplayGramSchmidtToClarkeRotors()
        {
            var n = 4;

            var frame1 = GaPoTSymFrame.CreateGramSchmidtFrame(n).GetSwappedPairsFrame();
            var frame2 = GaPoTSymFrame.CreateClarkeFrame(n);

            var rotorsSequence = GaPoTSymRotorsSequence.CreateFromOrthonormalFrames(
                frame1, 
                frame2
            );

            var i = 1;
            foreach (var rotor in rotorsSequence)
            {
                Console.WriteLine($"R_{i} = {rotor.TermsToLaTeX()}");
                Console.WriteLine();

                i++;
            }
        }

        public static void Execute()
        {
            DisplayClarkeFrames();
        }
    }
}