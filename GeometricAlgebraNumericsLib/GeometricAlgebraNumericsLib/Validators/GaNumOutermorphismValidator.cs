using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Outermorphisms;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Validators
{
    public sealed class GaNumOutermorphismValidator : GaNumValidator
    {
        public override string Validate()
        {
            ReportComposer.AppendHeader("Numeric Outermorphism Validations");

            var vSpaceDim = 4;
            var gaSpaceDim = 1 << vSpaceDim;

            var randGen = new GaRandomGenerator(10);

            //Initialize multivectors with random coefficients
            var inMv1 = RandomGenerator.GetNumFullMultivectorTerms(vSpaceDim).CreateSarMultivector(vSpaceDim);
            var inMv2 = inMv1.GetDgrMultivector();

            //Create outermorphisms from same matrix
            var matrix = DenseMatrix.Create(
                vSpaceDim, 
                vSpaceDim, 
                (i, j) => randGen.GetScalar(-10, 10)
            );

            var omComputed = GaNumOutermorphism.Create(matrix);
            //var omMixed = GaNumMixedOutermorphism.Create(matrix);
            //var omTree = GaNumStoredOutermorphism.CreateTree(matrix);
            //var omSparseRows = GaNumStoredOutermorphism.CreateSparseRows(matrix);
            //var omSparseColumns = GaNumStoredOutermorphism.CreateSparseColumns(matrix);
            //var omArray = GaNumStoredOutermorphism.CreateArray(matrix);
            //var omCoefSums = GaNumStoredOutermorphism.CreateCoefSums(matrix);
            var omMatrix = GaNumStoredOutermorphism.CreateMatrix(matrix);

            //var outMv0 = omMatrix[inMv1];
            var outMv1 = omComputed[inMv1];
            var outMv2 = omComputed[inMv2].GetSarMultivector();

            //ValidateEqual("Input Multivectors", inMv1, inMv2.ToMultivector());

            //ValidateEqual("Computed-Matrix Outermorphism, Tree Multivectors", outMv0, outMv1);
            ValidateEqual("Computed Outermorphism, Tree-Graded Multivectors", outMv1, outMv2);

            return ReportComposer.ToString();
        }
    }
}
