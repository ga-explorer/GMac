using System;
using System.Linq;
using CodeComposerLib.LaTeX;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Samples.GAPoT
{
    public static class OrthogonalRotorsDecompositionSample
    {
        private static MathematicaInterface _casInterface 
            = MathematicaInterface.DefaultCas;


        public static GaPoTSymMultivector ComplexEigenPairToBivector(Expr eigenValue, Expr eigenVector)
        {
            var realValue = MathematicaUtils.Evaluate(Mfs.Re[eigenValue]);
            var imagValue = MathematicaUtils.Evaluate(Mfs.Im[eigenValue]);

            var realVector = new GaPoTSymVector(MathematicaUtils.Evaluate(Mfs.Re[eigenVector]).Args);
            var imagVector = new GaPoTSymVector(MathematicaUtils.Evaluate(Mfs.Im[eigenVector]).Args);

            var scalar =
                MathematicaUtils.Evaluate(Mfs.Plus[
                    Mfs.Times[realValue, realValue],
                    Mfs.Times[imagValue, imagValue]
                ]);

            var angle = Mfs.ArcTan[realValue, imagValue];

            Console.WriteLine($"Eigen value real part: {realValue.GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine($"Eigen value imag part: {imagValue.GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine($"Eigen value length: {scalar.GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine($"Eigen value angle: {angle.GetLaTeXDisplayEquation()}");
            Console.WriteLine();

            Console.WriteLine("Eigen vector real part:");
            Console.WriteLine(realVector.TermsToLaTeX().GetLaTeXDisplayEquation());
            Console.WriteLine();

            Console.WriteLine("Eigen vector imag part:");
            Console.WriteLine(imagVector.TermsToLaTeX().GetLaTeXDisplayEquation());
            Console.WriteLine();


            var blade = realVector.Op(imagVector);

            Console.WriteLine("Blade:");
            Console.WriteLine(blade.ToLaTeXEquationsArray("B", @"\mu"));
            Console.WriteLine();

            var rotor = GaPoTSymMultivector.CreateSimpleRotor(angle, blade);

            Console.WriteLine("Final rotor:");
            Console.WriteLine(rotor.ToLaTeXEquationsArray("R", @"\mu"));
            Console.WriteLine();

            Console.WriteLine($"Is simple rotor? {rotor.IsSimpleRotor()}");
            Console.WriteLine();

            Console.WriteLine();

            return rotor;
        }
        
        public static void Execute(int n, bool numericFlag)
        {
            var uFrame = 
                GaPoTSymFrame.CreateBasisFrame(n);

            var cFrame = 
                GaPoTSymFrame.CreateGramSchmidtFrame(n, 0);

            var rs1 = 
                GaPoTSymRotorsSequence.CreateFromOrthonormalFrames(cFrame, uFrame);

            var rotationMatrixExpr = 
                rs1.GetFinalMatrixExpr(n);

            //Make sure the rotation matrix is correct
            var m =
                MathematicaUtils.Evaluate(Mfs.Dot[rotationMatrixExpr, cFrame.GetMatrix().ToArrayExpr()]);

            var finalRotor = 
                GaPoTSymRotorsSequence.Create(
                    rs1.Select(r => r.ScalarsToNumerical())
                ).GetFinalRotor();

            Console.WriteLine("Rotation matrix:");
            Console.WriteLine("MatrixForm[" + rotationMatrixExpr + "]");
            Console.WriteLine();

            Console.WriteLine("Final rotor:");
            Console.WriteLine(finalRotor.ToLaTeXEquationsArray("R", @"\mu"));
            Console.WriteLine();

            Console.WriteLine("Rotated cFrame using matrix:");
            Console.WriteLine("MatrixForm[" + m + "]");
            Console.WriteLine();

            
            rotationMatrixExpr.EigenDecomposition(out var eigenValuesExpr, out var eigenVectorsExpr);

            if (numericFlag)
            {
                eigenValuesExpr = eigenValuesExpr.Select(e => e.N()).ToArray();
                eigenVectorsExpr = eigenVectorsExpr.Select(e => e.N()).ToArray();
            }


            var eigenRotorsArray = new GaPoTSymMultivector[n];

            for (var i = 0; i < n; i++)
            {
                eigenRotorsArray[i] = ComplexEigenPairToBivector(
                    eigenValuesExpr[i], 
                    eigenVectorsExpr[i]
                );
            }

            //Make sure the blades of these rotors are eigen blades of the rotation matrix
            for (var i = 0; i < n; i++)
            {
                var (angleExpr, blade) =
                    eigenRotorsArray[i].GetSimpleRotorAngleBlade();

                var bladeDiff = (rs1.Rotate(blade) - blade).Round(7);

                Console.WriteLine("Angle:");
                Console.WriteLine(angleExpr.GetLaTeXDisplayEquation());
                Console.WriteLine();

                Console.WriteLine("Blade:");
                Console.WriteLine(blade.ToLaTeXEquationsArray("b_1", @"\mu"));
                Console.WriteLine();

                Console.WriteLine("Rotated Blade - Blade:");
                Console.WriteLine(bladeDiff.ToLaTeXEquationsArray("b_1", @"\mu"));
                Console.WriteLine();
            }

            var diff = 
                (eigenRotorsArray[0].Gp(eigenRotorsArray[1]) -
                eigenRotorsArray[1].Gp(eigenRotorsArray[0]))
                .Round(7);

            Console.WriteLine("Difference:");
            Console.WriteLine(diff.TermsToLaTeX().GetLaTeXDisplayEquation());
            Console.WriteLine();


            var rs = GaPoTSymRotorsSequence.Create(
                eigenRotorsArray[0],
                eigenRotorsArray[1]
            );

            //var rs = GaPoTSymRotorsSequence.Create(
            //    eigenRotorsArray[0]
            //);

            var uFrame1 = rs.Rotate(cFrame);

            Console.WriteLine("Rotated cFrame:");

            for (var i = 0; i < n; i++)
            {
                Console.WriteLine(uFrame1[i].Round(7).TermsToLaTeX().GetLaTeXDisplayEquation());
                Console.WriteLine();
            }
        }

        public static void Execute()
        {
            Execute(4, true);
        }
    }
}