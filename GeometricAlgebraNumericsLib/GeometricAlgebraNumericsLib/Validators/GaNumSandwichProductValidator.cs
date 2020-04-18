using System.Linq;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Validators
{
    public sealed class GaNumSandwichProductValidator : GaNumValidator
    {
        public override string Validate()
        {
            ReportComposer.AppendHeader("Numeric Sandwich Product Validations");

            var vSpaceDim = 6;
            var gaSpaceDim = 1 << vSpaceDim;

            //Initialize multivectors with random coefficients
            var frame = GaNumFrame.CreateEuclidean(vSpaceDim);

            var v = GaNumSarMultivector.CreateScalar(vSpaceDim, 1);
            for (var i = 0; i < vSpaceDim; i++)
            {
                var vector = RandomGenerator.GetNumFullVectorTerms(vSpaceDim).CreateSarMultivector(vSpaceDim);
                v = frame.Gp[v, vector];
            }

            var matrix1 = frame.GpGpToVectorMappingMatrix(v, 1);

            var mvArray = new GaNumSarMultivector[vSpaceDim];
            for (var basisVectorIndex = 0; basisVectorIndex < vSpaceDim; basisVectorIndex++)
            {
                var basisVector = GaNumSarMultivector.CreateBasisVector(vSpaceDim, basisVectorIndex);

                mvArray[basisVectorIndex] = frame.Gp[frame.Gp[v, basisVector], v];
            }

            var matrix2 = DenseMatrix.OfColumns(
                mvArray.Select(a => a.GetVectorPartValues())
            );

            ValidateEqual("Linear Mapping Matrix", matrix1, matrix2);

            return ReportComposer.ToString();
        }
    }
}