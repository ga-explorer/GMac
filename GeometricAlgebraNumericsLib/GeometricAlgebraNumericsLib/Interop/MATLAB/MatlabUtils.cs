using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Interop.MATLAB
{
    public static class GaNumMatlabUtils
    {
        public static GaNumMatlabSparseMatrixData GetMatlabSparseColumnMatrixData(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            return GetMatlabSparseColumnMatrixData( 
                termsList.SumAsSarMultivector(vSpaceDim)
            );
        }

        public static GaNumMatlabSparseMatrixData GetMatlabSparseColumnMatrixData(this IGaNumMultivector mv)
        {
            var termsArray = mv
                .GetStoredTerms()
                .OrderBy(t => t.BasisBladeId)
                .ToArray();

            var result = GaNumMatlabSparseMatrixData.CreateColumnMatrix(
                mv.GaSpaceDimension,
                termsArray.Length
            );

            var sparseIndex = 0;
            foreach (var term in termsArray)
            {
                result.SetItem(
                    sparseIndex, 
                    term.BasisBladeId + 1, //MATLAB array indices start at 1 not 0
                    term.ScalarValue
                );

                sparseIndex++;
            }

            return result;
        }


        public static IEnumerable<GaTerm<double>> GetNumTerms(this GaNumMatlabSparseMatrixData matrixData)
        {
            for (var i = 0; i < matrixData.ItemsCount; i++)
                yield return new GaTerm<double>(
                    matrixData.RowIndicesArray[i] - 1, //MATLAB array indices start at 1 not 0
                    matrixData.ValuesArray[i]
                );
        }


        public static GaNumDarMultivector CreateNumDarMultivector(this GaNumMatlabSparseMatrixData matrixData)
        {
            return matrixData
                .GetNumTerms()
                .SumAsDarMultivector(
                    matrixData.RowsCount.ToVSpaceDimension()
                );
        }

        public static GaNumSarMultivector CreateNumSarMultivector(this GaNumMatlabSparseMatrixData matrixData)
        {
            return matrixData
                .GetNumTerms()
                .SumAsSarMultivector(
                    matrixData.RowsCount.ToVSpaceDimension()
                );
        }

        public static GaNumDgrMultivector CreateNumDgrMultivector(this GaNumMatlabSparseMatrixData matrixData)
        {
            return matrixData
                .GetNumTerms()
                .SumAsDgrMultivector(
                    matrixData.RowsCount.ToVSpaceDimension()
                );
        }

        public static GaNumSgrMultivector CreateNumSgrMultivector(this GaNumMatlabSparseMatrixData matrixData)
        {
            return matrixData
                .GetNumTerms()
                .SumAsSgrMultivector(
                    matrixData.RowsCount.ToVSpaceDimension()
                );
        }
    }
}
