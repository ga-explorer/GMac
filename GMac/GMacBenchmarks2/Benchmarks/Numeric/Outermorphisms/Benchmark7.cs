using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMacBenchmarks2.Benchmarks.Numeric.Outermorphisms
{
    /// <summary>
    /// Benchmark implementation method of sandwich product on binary tree multivectors
    /// </summary>
    public class Benchmark7
    {
        private GaRandomGenerator _randGen;

        private GaNumFrameEuclidean _frame;

        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13)]
        public int VSpaceDim { get; set; }

        public int GaSpaceDim
            => VSpaceDim.ToGaSpaceDimension();

        public GaNumSarMultivector[] VersorsArray { get; set; }

        public Matrix[] MatricesArray { get; set; }


        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            _frame = GaNumFrame.CreateEuclidean(VSpaceDim);

            VersorsArray = new GaNumSarMultivector[VSpaceDim + 1];
            MatricesArray = new Matrix[VSpaceDim + 1];

            VersorsArray[0] = GaNumSarMultivector.CreateScalar(VSpaceDim, 1);
            VersorsArray[0].GetBtrRootNode();

            for (var i = 0; i < VSpaceDim; i++)
            {
                var vector = _randGen.GetNumFullVectorTerms(VSpaceDim).CreateSarMultivector(VSpaceDim);
                
                VersorsArray[i + 1] = _frame.Gp[VersorsArray[0], vector];
                VersorsArray[i + 1].GetBtrRootNode();
            }
        }


        [Benchmark]
        public Matrix[] SandwichProductMatrix1()
        {
            for (var i = 0; i <= VSpaceDim; i++)
                MatricesArray[i] = _frame.GpGpToVectorMappingMatrix(VersorsArray[i], 1);

            return MatricesArray;
        }

        [Benchmark]
        public Matrix[] SandwichProductMatrix2()
        {
            for (var i = 0; i <= VSpaceDim; i++)
            {
                var versor = VersorsArray[i];
                var mvArray = new double[VSpaceDim][];

                for (var basisVectorIndex = 0; basisVectorIndex < VSpaceDim; basisVectorIndex++)
                {
                    var basisVector = GaNumSarMultivector.CreateBasisVector(VSpaceDim, basisVectorIndex);

                    mvArray[basisVectorIndex] = _frame.Gp[_frame.Gp[versor, basisVector], versor].GetVectorPartValues();
                }

                MatricesArray[i] = DenseMatrix.OfColumns(mvArray);
            }

            return MatricesArray;
        }
    }
}