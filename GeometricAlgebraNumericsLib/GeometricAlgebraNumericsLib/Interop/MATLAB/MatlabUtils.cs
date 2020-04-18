using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;

namespace GeometricAlgebraNumericsLib.Interop.MATLAB
{
    public static class MatlabUtils
    {
        public static MatlabSparseColumnMatrixData GetMatlabSparseColumnMatrixData(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            return GetMatlabSparseColumnMatrixData( 
                termsList.SumAsSarMultivector(vSpaceDim)
            );
        }

        public static MatlabSparseColumnMatrixData GetMatlabSparseColumnMatrixData(this IGaNumMultivector mv)
        {
            var termsArray = mv
                .GetStoredTerms()
                .OrderBy(t => t.BasisBladeId)
                .ToArray();

            var result = new MatlabSparseColumnMatrixData(
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


        public static IEnumerable<GaTerm<double>> GetNumTerms(this MatlabSparseColumnMatrixData matrixData)
        {
            for (var i = 0; i < matrixData.ItemsCount; i++)
                yield return new GaTerm<double>(
                    matrixData.IndicesArray[i] - 1, //MATLAB array indices start at 1 not 0
                    matrixData.ValuesArray[i]
                );
        }


        public static GaNumDarMultivector CreateNumDarMultivector(this MatlabSparseColumnMatrixData matrixData)
        {
            return matrixData
                .GetNumTerms()
                .SumAsDarMultivector(
                    matrixData.MaxSize.ToVSpaceDimension()
                );
        }

        public static GaNumSarMultivector CreateNumSarMultivector(this MatlabSparseColumnMatrixData matrixData)
        {
            return matrixData
                .GetNumTerms()
                .SumAsSarMultivector(
                    matrixData.MaxSize.ToVSpaceDimension()
                );
        }

        public static GaNumDgrMultivector CreateNumDgrMultivector(this MatlabSparseColumnMatrixData matrixData)
        {
            return matrixData
                .GetNumTerms()
                .SumAsDgrMultivector(
                    matrixData.MaxSize.ToVSpaceDimension()
                );
        }

        public static GaNumSgrMultivector CreateNumSgrMultivector(this MatlabSparseColumnMatrixData matrixData)
        {
            return matrixData
                .GetNumTerms()
                .SumAsSgrMultivector(
                    matrixData.MaxSize.ToVSpaceDimension()
                );
        }
    }
}
