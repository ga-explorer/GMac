using System;
using System.Linq;
using CodeComposerLib.LaTeX;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Samples.GAPoT
{
    public static class GramSchmidtRotationSample
    {
        public static void Execute2()
        {
            var n = 4;

            var uFrame = GaPoTSymFrame.CreateBasisFrame(n);
            var cFrame = GaPoTSymFrame.CreateGramSchmidtFrame(n, 0, out var eFrame);

            var cFrame1 = GaPoTSymFrame.CreateBasisFrame(n);
            var cFrame2 = GaPoTSymFrame.CreateBasisFrame(n);

            cFrame1[0] = cFrame[1];
            cFrame1[1] = cFrame[0];

            cFrame2[2] = cFrame[2];
            cFrame2[3] = cFrame[3];

            var rs1 = GaPoTSymRotorsSequence.CreateFromFrames(n, cFrame1, uFrame);
            var rs2 = GaPoTSymRotorsSequence.CreateFromFrames(n, cFrame2, uFrame);

            Console.WriteLine("Rotors sequence 1:");
            for (var i = 0; i < rs1.Count; i++)
            {
                Console.WriteLine(rs1[i].ToLaTeXEquationsArray($"R^1_{{{i + 1}}}", @"\mu"));
                Console.WriteLine();
            }

            Console.WriteLine("Rotors sequence 2:");
            for (var i = 0; i < rs2.Count; i++)
            {
                Console.WriteLine(rs2[i].ToLaTeXEquationsArray($"R^2_{{{i + 1}}}", @"\mu"));
                Console.WriteLine();
            }
        }

        public static void Execute1()
        {
            var n = 4;

            var uFrame = GaPoTSymFrame.CreateBasisFrame(n);
            var cFrame = GaPoTSymFrame.CreateGramSchmidtFrame(n, 0, out var eFrame);

            var uFrameVectors = uFrame.Select(v => v.ScalarsToNumerical()).ToArray();
            var cFrameVectors = cFrame.Select(v => v.ScalarsToNumerical()).ToArray();

            //var uFrameVectors = uFrame.ToArray();
            //var cFrameVectors = cFrame.ToArray();

            var pseudoScalar = 
                GaPoTSymMultivector.CreateTerm(
                    (1UL << n) - 1, 
                    Expr.INT_ONE
                );

            var rotor1 = 
                cFrameVectors[0].GetRotorToVector(uFrameVectors[0]);

            for (var i = 1; i < n; i++) 
                cFrameVectors[i] = cFrameVectors[i].ApplyRotor(rotor1);

            var rotor2 = 
                cFrameVectors[1].GetRotorToVector(uFrameVectors[1]);

            for (var i = 2; i < n; i++) 
                cFrameVectors[i] = cFrameVectors[i].ApplyRotor(rotor2);

            var rotor21 = rotor2.Gp(rotor1);
            var (angle21, blade21) = 
                rotor21.GetSimpleRotorAngleBlade();

            var rotorSequence = GaPoTSymRotorsSequence.Create(rotor21);

            var uFrame1 = rotorSequence.Rotate(cFrame);

            Console.WriteLine("Rotated cFrame:");
            Console.WriteLine(uFrame1.ToLaTeXEquationsArray("u", @"\mu"));
            Console.WriteLine();

            pseudoScalar = blade21.Lcp(pseudoScalar.Inverse());

            for (var i = 2; i < n; i++)
            {
                uFrameVectors[i] = uFrameVectors[i].GetProjectionOnBlade(pseudoScalar);
                cFrameVectors[i] = cFrameVectors[i].GetProjectionOnBlade(pseudoScalar);
            }

            var rotor3 = 
                cFrameVectors[2].GetRotorToVector(uFrameVectors[2]);

            for (var i = 3; i < n; i++) 
                cFrameVectors[i] = cFrameVectors[i].ApplyRotor(rotor3);

            Console.WriteLine("Rotors sequence:");
            Console.WriteLine(rotor1.ToLaTeXEquationsArray($"R_{{1}}", @"\mu"));
            Console.WriteLine();
            Console.WriteLine(rotor2.ToLaTeXEquationsArray($"R_{{2}}", @"\mu"));
            Console.WriteLine();
            Console.WriteLine(rotor21.ToLaTeXEquationsArray($"R_{{2,1}}", @"\mu"));
            Console.WriteLine();
            Console.WriteLine(rotor3.ToLaTeXEquationsArray($"R_{{3}}", @"\mu"));
            Console.WriteLine();

            //var rotorSequence = GaPoTSymRotorsSequence.Create(rotor1, rotor2, rotor3);

            //var uFrame1 = rotorSequence.Rotate(cFrame);

            //Console.WriteLine("Rotated cFrame:");
            //Console.WriteLine(uFrame1.ToLaTeXEquationsArray("u", @"\mu"));
            //Console.WriteLine();

            //var diff = rotor3.Gp(rotor21) - rotor21.Gp(rotor3);

            //Console.WriteLine("Rotors diff:");
            //Console.WriteLine(diff.TermsToText());
            //Console.WriteLine();
        }

        public static void Execute()
        {
            for (var n = 2; n <= 8; n++)
            {
                Console.WriteLine($"Dimensions: {n}");
                Console.WriteLine();

                var uFrame = GaPoTSymFrame.CreateBasisFrame(n);
                var cFrame = GaPoTSymFrame.CreateGramSchmidtFrame(n, 0, out var eFrame);

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
                        uFrame,
                        Enumerable.Range(0, n - 1).ToArray()
                        //Enumerable.Range(1, n - 1).ToArray()
                    );

                var rotationMatrix = 
                    rotorsSequence.GetFinalMatrixExpr(n).FullSimplify();

                Console.WriteLine("Rotation matrix:");
                Console.WriteLine(rotationMatrix.GetLaTeXDisplayEquation());
                Console.WriteLine();

                Console.WriteLine("Rotors sequence:");

                for (var i = 0; i < rotorsSequence.Count; i++)
                {
                    var rotorEquation = rotorsSequence[i].ToLaTeXEquationsArray(
                        $"R_{{{i + 1}}}", 
                        @"\mu"
                    );

                    Console.WriteLine(rotorEquation);
                    Console.WriteLine();
                }

                var autoVector = GaPoTSymVector.CreateUnitAutoVector(n);

                var rotatedAutoVector =
                    rotorsSequence.Rotate(autoVector);

                Console.WriteLine("Rotated Auto Vector:");
                Console.WriteLine($"{rotatedAutoVector.TermsToLaTeX().GetLaTeXDisplayEquation()}");
                Console.WriteLine();

                continue;

                Console.WriteLine("Rotors pairs sequence:");

                var rotorsSequence1 = 
                    rotorsSequence.JoinRotorPairs();
                
                for (var i = 0; i < rotorsSequence1.Count; i++)
                {
                    var rotorEquation = rotorsSequence1[i].ToLaTeXEquationsArray(
                        $"P_{{{i + 1}}}", 
                        @"\mu"
                    );

                    Console.WriteLine(rotorEquation);
                    Console.WriteLine();
                }

                if (rotorsSequence1.Count > 1)
                {
                    var diff =
                        rotorsSequence1[1].Gp(rotorsSequence1[0]) -
                        rotorsSequence1[0].Gp(rotorsSequence1[1]);

                    var diffEquation =
                        diff.ToLaTeXEquationsArray("diff", @"\mu");

                    Console.WriteLine("Diff:");
                    Console.WriteLine(diffEquation);
                    Console.WriteLine();
                }
            }
        }
    }
}