using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Outermorphisms;
using GeometricAlgebraStructuresLib.Frames;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMacBenchmarks2.Benchmarks.Numeric.Outermorphisms
{
    /// <summary>
    /// Benchmark implementation method of outermorphisms on binary tree multivectors
    /// </summary>
    public class Benchmark3
    {
        private GaRandomGenerator _randGen;

        private GaNumOutermorphism _omComputed;
        //private GaNumMixedOutermorphism _omMixed;
        //private GaNumStoredOutermorphism _omTree;
        //private GaNumStoredOutermorphism _omSparseRows;
        //private GaNumStoredOutermorphism _omSparseColumns;
        //private GaNumStoredOutermorphism _omArray;
        //private GaNumStoredOutermorphism _omCoefSums;
        private GaNumStoredOutermorphism _omMatrix;

        private GaNumSarMultivector[] _termsArray;
        private GaNumSarMultivector[] _kVectorsArray;
        private GaNumSarMultivector _fullMultivector;

        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13)]
        //[Params(12)]
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
            //_omMixed = GaNumMixedOutermorphism.Create(matrix);
            //_omTree = GaNumStoredOutermorphism.CreateTree(matrix);
            //_omSparseRows = GaNumStoredOutermorphism.CreateSparseRows(matrix);
            //_omSparseColumns = GaNumStoredOutermorphism.CreateSparseColumns(matrix);
            //_omArray = GaNumStoredOutermorphism.CreateArray(matrix);
            //_omCoefSums = GaNumStoredOutermorphism.CreateCoefSums(matrix);
            _omMatrix = GaNumStoredOutermorphism.CreateMatrix(matrix);

            _termsArray = new GaNumSarMultivector[GaSpaceDim];
            for (var id = 0; id < GaSpaceDim; id++)
            {
                _termsArray[id] = _randGen.GetNumTerm(id).CreateSarMultivector(VSpaceDim);

                _termsArray[id].GetBtrRootNode();
            }

            _kVectorsArray = new GaNumSarMultivector[VSpaceDim + 1];
            for (var grade = 0; grade <= VSpaceDim; grade++)
            {
                _kVectorsArray[grade] = _randGen.GetNumFullKVectorTerms(VSpaceDim, grade).CreateSarMultivector(VSpaceDim);

                _kVectorsArray[grade].GetBtrRootNode();
            }

            _fullMultivector = 
                _randGen
                    .GetNumFullMultivectorTerms(VSpaceDim)
                    .CreateSarMultivector(VSpaceDim);

            _fullMultivector.GetBtrRootNode();
        }


        //[Benchmark]
        public GaNumSarMultivector OmComputed_Terms()
        {
            GaNumSarMultivector mv = null;

            for (var id1 = 0; id1 < GaSpaceDim; id1++)
                mv = _omComputed[_termsArray[id1]];

            return mv;
        }

        [Benchmark]
        public GaNumSarMultivector OmComputed_KVectors()
        {
            GaNumSarMultivector mv = null;

            for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
                mv = _omComputed[_kVectorsArray[grade1]];

            return mv;
        }

        [Benchmark]
        public GaNumSarMultivector OmComputed_FullMultivectors()
        {
            return _omComputed[_fullMultivector];
        }


        //[Benchmark]
        //public GaNumMultivector OmMixed_Terms()
        //{
        //    GaNumMultivector mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omMixed[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmMixed_KVectors()
        //{
        //    GaNumMultivector mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omMixed[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmMixed_FullMultivectors()
        //{
        //    return _omMixed[_fullMultivector];
        //}


        //[Benchmark]
        //public GaNumMultivector OmComputed_Terms()
        //{
        //    GaNumMultivector mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omComputed[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmComputed_KVectors()
        //{
        //    GaNumMultivector mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omComputed[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmComputed_FullMultivectors()
        //{
        //    return _omComputed[_fullMultivector];
        //}


        //[Benchmark]
        //public GaNumMultivector OmTree_Terms()
        //{
        //    GaNumMultivector mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omTree[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmTree_KVectors()
        //{
        //    GaNumMultivector mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omTree[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmTree_FullMultivectors()
        //{
        //    return _omTree[_fullMultivector];
        //}


        //[Benchmark]
        //public GaNumMultivector OmSparseRows_Terms()
        //{
        //    GaNumMultivector mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omSparseRows[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmSparseRows_KVectors()
        //{
        //    GaNumMultivector mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omSparseRows[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmSparseRows_FullMultivectors()
        //{
        //    return _omSparseRows[_fullMultivector];
        //}


        //[Benchmark]
        //public GaNumMultivector OmSparseColumns_Terms()
        //{
        //    GaNumMultivector mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omSparseColumns[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmSparseColumns_KVectors()
        //{
        //    GaNumMultivector mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omSparseColumns[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmSparseColumns_FullMultivectors()
        //{
        //    return _omSparseColumns[_fullMultivector];
        //}


        //[Benchmark]
        //public GaNumMultivector OmArray_Terms()
        //{
        //    GaNumMultivector mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omArray[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmArray_KVectors()
        //{
        //    GaNumMultivector mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omArray[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmArray_FullMultivectors()
        //{
        //    return _omArray[_fullMultivector];
        //}


        //[Benchmark]
        //public GaNumMultivector OmCoefSums_Terms()
        //{
        //    GaNumMultivector mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omCoefSums[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmCoefSums_KVectors()
        //{
        //    GaNumMultivector mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omCoefSums[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivector OmCoefSums_FullMultivectors()
        //{
        //    return _omCoefSums[_fullMultivector];
        //}


        //[Benchmark]
        public GaNumSarMultivector OmMatrix_Terms()
        {
            GaNumSarMultivector mv = null;

            for (var id1 = 0; id1 < GaSpaceDim; id1++)
                mv = _omMatrix[_termsArray[id1]];

            return mv;
        }

        [Benchmark]
        public GaNumSarMultivector OmMatrix_KVectors()
        {
            GaNumSarMultivector mv = null;

            for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
                mv = _omMatrix[_kVectorsArray[grade1]];

            return mv;
        }

        [Benchmark]
        public GaNumSarMultivector OmMatrix_FullMultivectors()
        {
            return _omMatrix[_fullMultivector];
        }
    }
}