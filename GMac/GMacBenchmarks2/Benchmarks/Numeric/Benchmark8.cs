using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Outermorphisms;
using GeometricAlgebraNumericsLib.Multivectors;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMacBenchmarks2.Benchmarks.Numeric
{
    /// <summary>
    /// Benchmark outermorphisms on multiple multivectors at once
    /// </summary>
    public class Benchmark8
    {
        private GaRandomGenerator _randGen;

        private GaNumOutermorphism _omComputed;
        private GaNumMixedOutermorphism _omMixed;


        private GaNumMultivector[] _termsArray;
        private GaNumMultivector[] _kVectorsArray;
        private GaNumMultivector _fullMultivector;


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

            var matrix = DenseMatrix.Create(
                VSpaceDim,
                VSpaceDim,
                (i, j) => _randGen.GetScalar(-10, 10)
            );

            _omComputed = GaNumOutermorphism.Create(matrix);
            _omMixed = GaNumMixedOutermorphism.Create(matrix);
            
            _termsArray = new GaNumMultivector[GaSpaceDim];
            for (var id = 0; id < GaSpaceDim; id++)
            {
                _termsArray[id] = _randGen.GetNumTerm(GaSpaceDim, id);

                _termsArray[id].GetInternalTermsTree();
            }

            _kVectorsArray = new GaNumMultivector[VSpaceDim + 1];
            for (var grade = 0; grade <= VSpaceDim; grade++)
            {
                _kVectorsArray[grade] = _randGen.GetNumKVector(GaSpaceDim, grade);

                _kVectorsArray[grade].GetInternalTermsTree();
            }

            _fullMultivector = _randGen.GetNumMultivectorFull(GaSpaceDim);

            _fullMultivector.GetInternalTermsTree();
        }


        [Benchmark]
        public GaNumMultivector[] OmComputed_Terms1()
        {
            return _omComputed.Map(_termsArray);
        }

        [Benchmark]
        public GaNumMultivector[] OmComputed_Terms2()
        {
            var n = _termsArray.Length;
            var result = new GaNumMultivector[n];

            for (var i = 0; i < n; i++)
                result[i] = _omComputed[_termsArray[i]];

            return result;
        }

        [Benchmark]
        public GaNumMultivector[] OmComputed_KVectors1()
        {
            return _omComputed.Map(_kVectorsArray);
        }

        [Benchmark]
        public GaNumMultivector[] OmComputed_KVectors2()
        {
            var n = _kVectorsArray.Length;
            var result = new GaNumMultivector[n];

            for (var i = 0; i < n; i++)
                result[i] = _omComputed[_kVectorsArray[i]];

            return result;
        }

        [Benchmark]
        public GaNumMultivector[] OmComputed_FullMultivectors1()
        {
            return _omComputed.Map(_fullMultivector, _fullMultivector, _fullMultivector);
        }

        [Benchmark]
        public GaNumMultivector[] OmComputed_FullMultivectors2()
        {
            var n = 3;
            var result = new GaNumMultivector[n];

            for (var i = 0; i < n; i++)
                result[i] = _omComputed[_fullMultivector];

            return result;
        }
    }
}