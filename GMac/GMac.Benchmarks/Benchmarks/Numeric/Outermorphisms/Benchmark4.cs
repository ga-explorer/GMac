using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Outermorphisms;
using GeometricAlgebraStructuresLib.Frames;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMac.Benchmarks.Benchmarks.Numeric.Outermorphisms
{
    /// <summary>
    /// Benchmark implementation method of outermorphisms on graded multivectors
    /// </summary>
    public class Benchmark4
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

        private GaNumDgrMultivector[] _termsArray;
        private GaNumDgrMultivector[] _kVectorsArray;
        private GaNumDgrMultivector _fullMultivector;

        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12)]
        //[Params(12)]
        public int VSpaceDim { get; set; }
        //= 12;

        public ulong GaSpaceDim
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

            _termsArray = new GaNumDgrMultivector[GaSpaceDim];
            for (var id = 0UL; id < GaSpaceDim; id++)
            {
                _termsArray[id] = 
                    _randGen
                        .GetNumTerm(VSpaceDim, id)
                        .CreateDgrMultivector(VSpaceDim);
            }

            _kVectorsArray = new GaNumDgrMultivector[VSpaceDim + 1];
            for (var grade = 0; grade <= VSpaceDim; grade++)
            {
                _kVectorsArray[grade] = 
                    _randGen
                        .GetNumFullKVectorTerms(VSpaceDim, grade)
                        .CreateDgrMultivector(VSpaceDim);
            }

            _fullMultivector = 
                _randGen
                    .GetNumFullMultivectorTerms(VSpaceDim)
                    .CreateDgrMultivector(VSpaceDim);
        }


        //[Benchmark]
        public GaNumDgrMultivector OmComputed_Terms()
        {
            GaNumDgrMultivector mv = null;

            for (var id1 = 0UL; id1 < GaSpaceDim; id1++)
                mv = _omComputed[_termsArray[id1]];

            return mv;
        }

        [Benchmark]
        public GaNumDgrMultivector OmComputed_KVectors()
        {
            GaNumDgrMultivector mv = null;

            for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
                mv = _omComputed[_kVectorsArray[grade1]];

            return mv;
        }

        [Benchmark]
        public GaNumDgrMultivector OmComputed_FullMultivectors()
        {
            return _omComputed[_fullMultivector];
        }


        //[Benchmark]
        //public GaNumMultivectorGraded OmMixed_Terms()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omMixed[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmMixed_KVectors()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omMixed[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmMixed_FullMultivectors()
        //{
        //    return _omMixed[_fullMultivector];
        //}


        //[Benchmark]
        //public GaNumMultivectorGraded OmComputed_Terms()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omComputed[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmComputed_KVectors()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omComputed[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmComputed_FullMultivectors()
        //{
        //    return _omComputed[_fullMultivector];
        //}


        //[Benchmark]
        //public GaNumMultivectorGraded OmTree_Terms()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omTree[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmTree_KVectors()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omTree[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmTree_FullMultivectors()
        //{
        //    return _omTree[_fullMultivector];
        //}


        //[Benchmark]
        //public GaNumMultivectorGraded OmSparseRows_Terms()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omSparseRows[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmSparseRows_KVectors()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omSparseRows[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmSparseRows_FullMultivectors()
        //{
        //    return _omSparseRows[_fullMultivector];
        //}


        //[Benchmark]
        //public GaNumMultivectorGraded OmSparseColumns_Terms()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omSparseColumns[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmSparseColumns_KVectors()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omSparseColumns[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmSparseColumns_FullMultivectors()
        //{
        //    return _omSparseColumns[_fullMultivector];
        //}


        //[Benchmark]
        //public GaNumMultivectorGraded OmArray_Terms()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omArray[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmArray_KVectors()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omArray[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmArray_FullMultivectors()
        //{
        //    return _omArray[_fullMultivector];
        //}


        //[Benchmark]
        //public GaNumMultivectorGraded OmCoefSums_Terms()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omCoefSums[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmCoefSums_KVectors()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omCoefSums[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmCoefSums_FullMultivectors()
        //{
        //    return _omCoefSums[_fullMultivector];
        //}


        //[Benchmark]
        //public GaNumMultivectorGraded OmMatrix_Terms()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var id1 = 0; id1 < GaSpaceDim; id1++)
        //        mv = _omMatrix[_termsArray[id1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmMatrix_KVectors()
        //{
        //    GaNumMultivectorGraded mv = null;

        //    for (var grade1 = 0; grade1 <= VSpaceDim; grade1++)
        //        mv = _omMatrix[_kVectorsArray[grade1]];

        //    return mv;
        //}

        //[Benchmark]
        //public GaNumMultivectorGraded OmMatrix_FullMultivectors()
        //{
        //    return _omMatrix[_fullMultivector];
        //}
    }
}