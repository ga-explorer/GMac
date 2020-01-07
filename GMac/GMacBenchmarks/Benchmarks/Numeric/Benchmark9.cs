using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Products;

namespace GMacBenchmarks.Benchmarks.Numeric
{
    /// <summary>
    /// Benchmark vector-kvector outer product methods
    /// </summary>
    public class Benchmark9
    {
        private GaRandomGenerator _randGen;

        private GaNumKVector[] _vectorsArray;
        private GaNumKVector[] _kVectorsArray;

        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12)]
        //[Params(13, 14, 15)]
        public int VSpaceDim { get; set; }
        //= 12;

        public int GaSpaceDim
            => VSpaceDim.ToGaSpaceDimension();


        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            _vectorsArray = new GaNumKVector[VSpaceDim];
            _kVectorsArray = new GaNumKVector[VSpaceDim];
            for (var i = 0; i < VSpaceDim; i++)
            {
                _vectorsArray[i] = GaNumKVector.Create(
                    GaSpaceDim,
                    1,
                    _randGen.GetScalars(VSpaceDim, -10, 10)
                );
            }
        }


        [Benchmark]
        public GaNumKVector[] OpComputed()
        {
            _kVectorsArray[0] = _vectorsArray[0];

            for (var i = 1; i < VSpaceDim; i++)
                _kVectorsArray[i] = _vectorsArray[i].ComputeVectorKVectorOp(_kVectorsArray[i - 1]);

            return _kVectorsArray;
        }

        [Benchmark]
        public GaNumKVector[] OpGenerated()
        {
            _kVectorsArray[0] = _vectorsArray[0];

            for (var i = 1; i < VSpaceDim; i++)
                _kVectorsArray[i] = _vectorsArray[i].VectorKVectorOp(_kVectorsArray[i - 1]);

            return _kVectorsArray;
        }
    }
}