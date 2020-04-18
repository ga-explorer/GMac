using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using DataStructuresLib.Basic;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Outermorphisms;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Outermorphisms;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMacBenchmarks2.Benchmarks.Numeric.Outermorphisms
{
    /// <summary>
    /// Benchmark outermorphisms on multiple k-vectors multivectors at once
    /// </summary>
    public class Benchmark1
    {
        private GaRandomGenerator _randGen;

        private GaNumOutermorphism _omComputed;

        private GaNumSarMultivector[] _btrMultivectorsArray;
        private GaNumDgrMultivector[] _jarMultivectorsArray;


        [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13)]
        //[Params(13, 14, 15)]
        public int VSpaceDim { get; set; }
        //= 12;

        public int GaSpaceDim
            => VSpaceDim.ToGaSpaceDimension();

        public int MultivectorsCount
            => VSpaceDim + 1;


        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            var matrix = DenseMatrix.Create(
                VSpaceDim,
                VSpaceDim,
                (i, j) => _randGen.GetScalar()
            );

            _omComputed = GaNumOutermorphism.Create(matrix);
            
            _btrMultivectorsArray = new GaNumSarMultivector[MultivectorsCount];
            _jarMultivectorsArray = new GaNumDgrMultivector[MultivectorsCount];

            for (var i = 0; i < MultivectorsCount; i++)
            {
                _btrMultivectorsArray[i] = _randGen.GetNumFullKVectorTerms(VSpaceDim, i).CreateSarMultivector(VSpaceDim);

                _btrMultivectorsArray[i].GetBtrRootNode();

                _jarMultivectorsArray[i] = _btrMultivectorsArray[i].GetDgrMultivector();
            }
        }


        //[Benchmark]
        public Pair<GaNumSarMultivector> OmComputed_BtrMultivectorsSeparate()
        {
            Pair<GaNumSarMultivector> result = null;

            for (var i1 = 0; i1 < MultivectorsCount; i1++)
            for (var i2 = 0; i2 < MultivectorsCount; i2++)
                result = new Pair<GaNumSarMultivector>(
                    _omComputed[_btrMultivectorsArray[i1]],
                    _omComputed[_btrMultivectorsArray[i2]]
                );

            return result;
        }

        //[Benchmark]
        public Tuple<double, GaNumDarKVector> OmComputed_BtrMultivectorsSeparateFactors()
        {
            Tuple<double, GaNumDarKVector> result = null;
            
            for (var i1 = 0; i1 < MultivectorsCount; i1++)
            for (var i2 = 0; i2 < MultivectorsCount; i2++)
            {
                var stack1 =
                    GaGbtNumMultivectorOutermorphismStack.Create(
                        _omComputed.BasisVectorMappingsList,
                        _btrMultivectorsArray[i1]
                    );

                var stack2 =
                    GaGbtNumMultivectorOutermorphismStack.Create(
                        _omComputed.BasisVectorMappingsList,
                        _btrMultivectorsArray[i2]
                    );

                var results = 
                    stack1.TraverseForScaledKVectors().Concat(stack2.TraverseForScaledKVectors());

                foreach (var item in results)
                    result = item;
            }

            return result;
        }

        //[Benchmark]
        public Pair<GaNumSarMultivector> OmComputed_BtrMultivectorsCombined()
        {
            Pair<GaNumSarMultivector> result = null;

            for (var i1 = 0; i1 < MultivectorsCount; i1++)
            for (var i2 = 0; i2 < MultivectorsCount; i2++)
                result = _omComputed[
                    _btrMultivectorsArray[i1],
                    _btrMultivectorsArray[i2]
                ];

            return result;
        }

        [Benchmark]
        public Pair<GaNumDgrMultivector> OmComputed_JarMultivectorsSeparate()
        {
            Pair<GaNumDgrMultivector> result = null;

            for (var i1 = 0; i1 < MultivectorsCount; i1++)
            for (var i2 = 0; i2 < MultivectorsCount; i2++)
                result = new Pair<GaNumDgrMultivector>(
                    _omComputed[_jarMultivectorsArray[i1]],
                    _omComputed[_jarMultivectorsArray[i2]]
                );

            return result;
        }

        [Benchmark]
        public Tuple<double, GaNumDarKVector> OmComputed_JarMultivectorsSeparateFactors()
        {
            Tuple<double, GaNumDarKVector> result = null;

            for (var i1 = 0; i1 < MultivectorsCount; i1++)
            for (var i2 = 0; i2 < MultivectorsCount; i2++)
            {
                var stack1 =
                    GaGbtNumMultivectorOutermorphismStack.Create(
                        _omComputed.BasisVectorMappingsList,
                        _jarMultivectorsArray[i1]
                    );

                var stack2 =
                    GaGbtNumMultivectorOutermorphismStack.Create(
                        _omComputed.BasisVectorMappingsList,
                        _jarMultivectorsArray[i2]
                    );

                var results =
                    stack1.TraverseForScaledKVectors().Concat(stack2.TraverseForScaledKVectors());

                foreach (var item in results)
                    result = item;
            }

            return result;
        }

        //[Benchmark]
        //public Pair<GaNumJarMultivector> OmComputed_JarMultivectorsCombined()
        //{
        //    Pair<GaNumJarMultivector> result = null;

        //    for (var i1 = 0; i1 < MultivectorsCount; i1++)
        //    for (var i2 = 0; i2 < MultivectorsCount; i2++)
        //        result = _omComputed[
        //            _jarMultivectorsArray[i1],
        //            _jarMultivectorsArray[i2]
        //        ];

        //    return result;
        //}
    }
}