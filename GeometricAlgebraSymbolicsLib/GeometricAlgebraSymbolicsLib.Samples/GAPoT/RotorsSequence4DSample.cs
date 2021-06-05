using System;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Samples.GAPoT
{
    public static class RotorsSequence4DSample
    {
        private static void DisplayFrames(GaPoTSymFrame sourceFrame, GaPoTSymFrame kirchhoffFrame, GaPoTSymFrame targetFrame)
        {
            var n = sourceFrame.Count;
            var matrix = targetFrame.GetMatrix(n).ToArrayExpr();
            
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
            
            var orthoFrameEquation = targetFrame.ToLaTeXEquationsArray(
                "c", 
                @"\mu"
            );

            Console.WriteLine(orthoFrameEquation);
            Console.WriteLine();

            Console.WriteLine("Rotation Matrix:");
            Console.WriteLine();

            Console.WriteLine(@"\[");
            Console.WriteLine($"{matrix.GetLaTeX()}");
            Console.WriteLine(@"\]");
            Console.WriteLine();

            var anglesTextList = sourceFrame
                .GetAnglesToFrame(targetFrame)
                .Select(a => a.GetLaTeX())
                .ToArray();

            Console.WriteLine("Angles between frames vectors:");
            foreach (var angleText in anglesTextList)
            {
                Console.WriteLine(@"\[");
                Console.WriteLine($"{angleText}");
                Console.WriteLine(@"\]");
                Console.WriteLine();
            }

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

            var rotor12Equation = rotorsSequence[1].Gp(rotorsSequence[0]).ToLaTeXEquationsArray(
                $"R_{{12}}",
                @"\mu"
            );

            Console.WriteLine(rotor12Equation);
            Console.WriteLine();

            var rotor23Equation = rotorsSequence[2].Gp(rotorsSequence[1]).ToLaTeXEquationsArray(
                $"R_{{23}}",
                @"\mu"
            );

            Console.WriteLine(rotor23Equation);
            Console.WriteLine();

            Console.WriteLine("Final Rotor:");
            Console.WriteLine();

            var finalRotorEquation = rotorsSequence.GetFinalRotor().ToLaTeXEquationsArray(
                "R", 
                @"\mu"
            );

            Console.WriteLine(finalRotorEquation);
            Console.WriteLine();
        }

        public static void Execute()
        {
            var n = 4;
            var sourceFrame = GaPoTSymFrame.CreateBasisFrame(n);

            var uPseudoScalar = GaPoTSymMultivector
                .CreateZero()
                .SetTerm((1UL << n) - 1, Expr.INT_ONE);

            for (var refVectorIndex = 0; refVectorIndex < 4; refVectorIndex++)
            {
                Console.Write(@"\section{Reference vector: $\mu_" + (refVectorIndex + 1) + "$}");
                Console.WriteLine();
            
                var kirchhoffFrameBase = GaPoTSymFrame.CreateEmptyFrame();

                var refVector = sourceFrame[refVectorIndex];
                for (var i = 0; i < n; i++)
                {
                    if (i == refVectorIndex)
                        continue;

                    kirchhoffFrameBase.AppendVector(sourceFrame[i] - refVector);
                }

                var kirchhoffFramesList = 
                    kirchhoffFrameBase.GetFramePermutations();

                var j = 1;
                foreach (var kirchhoffFrame in kirchhoffFramesList)
                {
                    Console.Write(@"\subsection{Kirchhoff Frame Permutation " + j + "}");
                    Console.WriteLine();
            
                    var targetFrame = kirchhoffFrame.GetOrthogonalFrame(true);
            
                    targetFrame.AppendVector(
                        -GaPoTSymUtils
                            .OuterProduct(targetFrame)
                            .Gp(uPseudoScalar.CliffordConjugate())
                            .GetVectorPart()
                    );

                    Debug.Assert(
                        targetFrame.IsOrthonormal()
                    );

                    Debug.Assert(
                        sourceFrame.HasSameHandedness(targetFrame)
                    );

                    DisplayFrames(
                        sourceFrame,
                        kirchhoffFrame,
                        targetFrame
                    );

                    j++;
                }
            }
        }
    }
}