using System;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Samples.GAPoT
{
    public static class SimpleKirchhoffRotationSample
    {
        public static void Execute1()
        {
            var refVectorIndex = 0;

            for (var n = 2; n <= 7; n++)
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

                var ePseudoScalar = 
                    eFrame.GetPseudoScalar();

                var rFrame = 
                    uFrame.GetProjectionOnFrame(eFrame);

                var uVector1 = 
                    uFrame[0];

                var rVector1 = 
                    rFrame[0].DivideByNorm();

                var kirchhoffVector = 
                    GaPoTSymVector.CreateUnitAutoVector(n);

                Console.WriteLine(@"Kirchhoff Vector:");
                Console.WriteLine(kirchhoffVector.ToLaTeXDisplayEquation("k", @"\mu"));
                Console.WriteLine();

                Console.WriteLine(@"Projected unit $r_1$ Vector:");
                Console.WriteLine(rVector1.ToLaTeXDisplayEquation("r_1", @"\mu"));
                Console.WriteLine();


                var rotor1 = GaPoTSymMultivector.CreateSimpleRotor(
                    kirchhoffVector, 
                    uFrame[n - 1]
                ).FullSimplifyScalars();

                var rotorMatrix1 = 
                    GaPoTSymRotorsSequence
                        .Create(rotor1)
                        .GetFinalMatrixExpr(n)
                        .FullSimplify();

                Console.WriteLine(@"Simple Rotor 1:");
                Console.WriteLine();
                
                Console.WriteLine(rotorMatrix1.GetLaTeXDisplayEquation());
                Console.WriteLine();

                Console.WriteLine(rotor1.ToLaTeXEquationsArray("R_{1}", @"\mu"));
                Console.WriteLine();

                var (angle1, blade1) = 
                    rotor1.GetSimpleRotorAngleBlade();
                    
                //angle1 = Mfs.N[angle1].Evaluate();
                blade1 = blade1.FullSimplifyScalars();
                    
                Console.WriteLine($"Rotation Angle: {angle1.GetLaTeXInlineEquation()}");
                Console.WriteLine();

                Console.WriteLine("Rotation Blade:");
                Console.WriteLine();
                Console.WriteLine(blade1.ToLaTeXDisplayEquation("", @"\mu"));
                Console.WriteLine();
            }
        }

        public static void Execute()
        {
            var refVectorIndex = 0;

            for (var n = 2; n <= 7; n++)
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

                var ePseudoScalar = 
                    eFrame.GetPseudoScalar();

                var rFrame = 
                    uFrame.GetProjectionOnFrame(eFrame);

                var uVector1 = 
                    uFrame[0];

                var rVector1 = 
                    rFrame[0].DivideByNorm();

                var kirchhoffVector = 
                    GaPoTSymVector.CreateUnitAutoVector(n);

                Console.WriteLine(@"Kirchhoff Vector:");
                Console.WriteLine(kirchhoffVector.ToLaTeXDisplayEquation("k", @"\mu"));
                Console.WriteLine();

                Console.WriteLine(@"Projected unit $r_1$ Vector:");
                Console.WriteLine(rVector1.ToLaTeXDisplayEquation("r_1", @"\mu"));
                Console.WriteLine();


                var rotor1 = GaPoTSymMultivector.CreateSimpleRotor(
                    kirchhoffVector, 
                    uFrame[n - 1]
                ).FullSimplifyScalars();

                var rotorMatrix1 = 
                    GaPoTSymRotorsSequence
                        .Create(rotor1)
                        .GetFinalMatrixExpr(n)
                        .FullSimplify();

                Console.WriteLine(@"Simple Rotor 1:");
                Console.WriteLine();
                
                Console.WriteLine(rotorMatrix1.GetLaTeXDisplayEquation());
                Console.WriteLine();

                Console.WriteLine(rotor1.ToLaTeXEquationsArray("R_{1}", @"\mu"));
                Console.WriteLine();

                if (n == 3)
                {
                    var (angle1, blade1) = 
                        rotor1.GetSimpleRotorAngleBlade();
                    
                    angle1 = Mfs.N[angle1].Evaluate();

                    var normal1 = blade1
                        .OrthogonalComplement(uPseudoScalar)
                        .GetVectorPart()
                        .ScalarsToNumerical();
                    
                    Console.WriteLine($"Rotation Angle: {angle1.GetLaTeXInlineEquation()}");
                    Console.WriteLine();

                    Console.WriteLine($"Rotation Direction: {normal1.ToLaTeXInlineEquation("", @"\mu")}");
                    Console.WriteLine();
                }

                var rotatedKirchhoffVector1 =
                    kirchhoffVector.ApplyRotor(rotor1).FullSimplifyScalars();

                var rotatedProjectedVector1 =
                    rVector1.ApplyRotor(rotor1).FullSimplifyScalars();

                Console.WriteLine(@"Rotation of Kirchhoff Vector:");
                Console.WriteLine();
                Console.WriteLine(rotatedKirchhoffVector1.ToLaTeXDisplayEquation(@"R_{1} k R_{1}^{\dagger}", @"\mu"));
                Console.WriteLine();

                Console.WriteLine(@"Rotation of projected $r_1$ Vector:");
                Console.WriteLine();
                Console.WriteLine(rotatedProjectedVector1.ToLaTeXDisplayEquation(@"R_{1} r_{1} R_{1}^{\dagger}", @"\mu"));
                Console.WriteLine();

                var cFrameMatrix1 = 
                    uFrame.ApplyRotor(rotor1.Reverse()).GetMatrix().ToArrayExpr().FullSimplify();

                var hFrameMatrix1 =
                    rFrame.ApplyRotor(rotor1).GetMatrix().ToArrayExpr().FullSimplify();

                Console.WriteLine(@"Gram-Schmidt Vectors Matrix:");
                Console.WriteLine();
                Console.WriteLine(cFrameMatrix1.GetLaTeXDisplayEquation());
                Console.WriteLine();

                Console.WriteLine(@"Hyper-Vectors Matrix:");
                Console.WriteLine();
                Console.WriteLine(hFrameMatrix1.GetLaTeXDisplayEquation());
                Console.WriteLine();


                var rotor2 = GaPoTSymMultivector.CreateSimpleRotor(
                    rVector1,
                    kirchhoffVector,
                    uVector1,
                    uFrame[n - 1]
                ).FullSimplifyScalars();

                var rotorMatrix2 = 
                    GaPoTSymRotorsSequence
                        .Create(rotor2)
                        .GetFinalMatrixExpr(n)
                        .FullSimplify();

                Console.WriteLine(@"Simple Rotor 2:");
                Console.WriteLine();
                
                Console.WriteLine(rotorMatrix2.GetLaTeXDisplayEquation());
                Console.WriteLine();

                Console.WriteLine(rotor2.ToLaTeXEquationsArray("R_{2}", @"\mu"));
                Console.WriteLine();

                if (n == 3)
                {
                    var (angle2, blade2) = 
                        rotor2.GetSimpleRotorAngleBlade();
                    
                    angle2 = Mfs.N[angle2].Evaluate();

                    var normal2 = blade2
                        .OrthogonalComplement(uPseudoScalar)
                        .GetVectorPart()
                        .ScalarsToNumerical();
                    
                    Console.WriteLine($"Rotation Angle: {angle2.GetLaTeXInlineEquation()}");
                    Console.WriteLine();

                    Console.WriteLine($"Rotation Direction: {normal2.ToLaTeXInlineEquation("", @"\mu")}");
                    Console.WriteLine();
                }

                var rotatedKirchhoffVector2 =
                    kirchhoffVector.ApplyRotor(rotor2).FullSimplifyScalars();

                var rotatedProjectedVector2 =
                    rVector1.ApplyRotor(rotor2).FullSimplifyScalars();

                Console.WriteLine(@"Rotation of Kirchhoff Vector:");
                Console.WriteLine();
                Console.WriteLine(rotatedKirchhoffVector2.ToLaTeXDisplayEquation(@"R_{2} k R_{2}^{\dagger}", @"\mu"));
                Console.WriteLine();

                Console.WriteLine(@"Rotation of projected unit $r_1$ Vector:");
                Console.WriteLine();
                Console.WriteLine(rotatedProjectedVector2.ToLaTeXDisplayEquation(@"R_{2} r_{1} R_{2}^{\dagger}", @"\mu"));
                Console.WriteLine();

                var cFrameMatrix2 = 
                    uFrame.ApplyRotor(rotor2.Reverse()).GetMatrix().ToArrayExpr().FullSimplify();

                var hFrameMatrix2 =
                    rFrame.ApplyRotor(rotor2).GetMatrix().ToArrayExpr().FullSimplify();

                Console.WriteLine(@"Gram-Schmidt Vectors Matrix:");
                Console.WriteLine();
                Console.WriteLine(cFrameMatrix2.GetLaTeXDisplayEquation());
                Console.WriteLine();

                Console.WriteLine(@"Hyper-Vectors Matrix:");
                Console.WriteLine();
                Console.WriteLine(hFrameMatrix2.GetLaTeXDisplayEquation());
                Console.WriteLine();
            }
        }
    }
}