using GeometricAlgebraNumericsLib.Maps.Unilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Outermorphisms;
using GeometricAlgebraStructuresLib.Frames;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMac.GMacMath.Validators
{
    public sealed class GaUnilinearMapsValidator : GMacMathValidator
    {
        private readonly GaUnilinearMapImplementation[] _methods =
        {
            GaUnilinearMapImplementation.Array,
            GaUnilinearMapImplementation.Tree,
            GaUnilinearMapImplementation.SparseColumns,
            GaUnilinearMapImplementation.SparseColumns,
            GaUnilinearMapImplementation.Matrix,
            GaUnilinearMapImplementation.CoefSums
        };

        private readonly string[] _methodNames = 
        {
            "Array",
            "Tree",
            "Sparse Columns",
            "Sparse Rows",
            "Matrix",
            "Coef Sums"
        };


        public int VSpaceDimension { get; set; } = 6;

        public int GaSpaceDimension => VSpaceDimension.ToGaSpaceDimension();


        private void ValidateNumericOutermorphisms()
        {
            ReportComposer.AppendHeader("Numeric Outermorphism Validations");

            //Initialize multivectors with random coefficients
            var mv1 = 
                RandomGenerator
                    .GetNumFullMultivectorTerms(VSpaceDimension)
                    .CreateSarMultivector(VSpaceDimension);

            var mv2 = 
                RandomGenerator
                    .GetNumFullKVectorTerms(VSpaceDimension, VSpaceDimension >> 1)
                    .CreateSarMultivector(VSpaceDimension);

            //Initialize outermorphism vector mapping matrix
            var vsMatrix = DenseMatrix.Create(
                VSpaceDimension, 
                VSpaceDimension,
                (i, j) => RandomGenerator.GetScalar(-10, 10)
            );

            var omBaseMethod = GaNumOutermorphism.Create(vsMatrix);
            
            var mvMatrix1 = omBaseMethod[mv1];
            var mvMatrix2 = omBaseMethod[mv2];
            
            //Compute their outermorphisms using several methods
            for (var i = 0; i < _methods.Length; i++)
            {
                var method = _methods[i];
                var methodName = _methodNames[i];

                var omCurrentMethod = GaNumStoredOutermorphism.Create(vsMatrix, method);

                ReportComposer.AppendLineAtNewLine();
                ReportComposer.AppendHeader(methodName, 2);

                ValidateEqual("Mapping: ", omBaseMethod.MappingMatrix, omCurrentMethod.MappingMatrix);

                ValidateEqual("Full Multivector: ", mvMatrix1, omCurrentMethod[mv1]);
                ValidateEqual("K-Vector: ", mvMatrix2, omCurrentMethod[mv2]);

                ReportComposer.AppendLineAtNewLine();
            }

            ReportComposer.AppendLineAtNewLine();
        }


        public override string Validate()
        {
            //ValidateSymbolicFrame();

            ValidateNumericOutermorphisms();

            //ValidateBothFrame();

            return ReportComposer.ToString();
        }
    }
}