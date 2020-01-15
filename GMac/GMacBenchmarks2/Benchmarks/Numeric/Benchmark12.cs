using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Products;
using TextComposerLib.Text.Linear;

namespace GMacBenchmarks2.Benchmarks.Numeric
{
    /// <summary>
    /// Compare performance of dynamic-tree multivectors vs array based
    /// static-tree multivectors
    /// </summary>
    public class Benchmark12
    {
        private GaRandomGenerator _randGen;
        private int _size;
        private KeyValuePair<int, double>[][] _sourceTermsArray;
        private GaNumMultivector[] _dynamicMultivectorsArray;
        private GaNumImmutableMultivector[] _staticMultivectorsArray;
        
        //[Params(2, 3, 4, 5, 6)]
        public int VSpaceDimension { get; set; }
            = 6;

        public int GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();


        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            var sourceTermsArray = 
                new List<KeyValuePair<int, double>[]>();

            //for (var id = 0; id < GaSpaceDimension; id++)
            //{
            //    sourceTermsArray.Add(
            //    _randGen
            //            .GetNumTerm(GaSpaceDimension, id)
            //            .Terms
            //            .ToArray()
            //    );
            //}

            //for (var grade = 0; grade <= VSpaceDimension; grade++)
            //{
            //    sourceTermsArray.Add(
            //    _randGen
            //            .GetNumKVector(GaSpaceDimension, grade)
            //            .Terms
            //            .ToArray()
            //    );
            //}

            sourceTermsArray.Add(
                _randGen
                    .GetNumMultivector(GaSpaceDimension)
                    .Terms
                    .ToArray()
            );

            _size = sourceTermsArray.Count;

            _sourceTermsArray = sourceTermsArray.ToArray();
            _dynamicMultivectorsArray = new GaNumMultivector[_size];
            _staticMultivectorsArray = new GaNumImmutableMultivector[_size];

            StaticTreeMultivectors();
            DynamicTreeMultivectors();
        }

        [Benchmark]
        public GaNumImmutableMultivector[,] StaticOp()
        {
            var result = new GaNumImmutableMultivector[_size, _size];

            for (var i = 0; i < _size; i++)
            for (var j = 0; j < _size; j++)
            {
                result[i, j] = _staticMultivectorsArray[i].Op(
                    _staticMultivectorsArray[j]
                );
            }

            return result;
        }

        [Benchmark]
        public GaNumMultivector[,] DynamicOp()
        {
            var result = new GaNumMultivector[_size, _size];

            for (var i = 0; i < _size; i++)
            for (var j = 0; j < _size; j++)
            {
                result[i, j] = _dynamicMultivectorsArray[i].Op(
                    _dynamicMultivectorsArray[j]
                );
            }

            return result;
        }

        //[Benchmark]
        public void StaticTreeMultivectors()
        {
            for (var i = 0; i < _size; i++)
            {
                //var term = _sourceTermsArray[i][0];

                //_staticMultivectorsArray[i] =
                //    GaNumImmutableMultivector.CreateTerm(
                //        GaSpaceDimension,
                //        term.Key,
                //        term.Value
                //    );

                _staticMultivectorsArray[i] =
                    GaNumImmutableMultivector.CreateFromTerms(
                        GaSpaceDimension,
                        _sourceTermsArray[i]
                    );
            }
        }

        //[Benchmark]
        public void DynamicTreeMultivectors()
        {
            for (var i = 0; i < _size; i++)
                _dynamicMultivectorsArray[i] = 
                    GaNumMultivector.CreateFromTerms(
                        GaSpaceDimension,
                        _sourceTermsArray[i]
                    );
        }

        public void Validate()
        {
            StaticTreeMultivectors();
            DynamicTreeMultivectors();

            for (var i = 0; i < _size; i++)
            {
                var staticMv = _staticMultivectorsArray[i];
                var dynamicMv = _dynamicMultivectorsArray[i];
                var diffMv = GaNumMultivectorHash.Create(GaSpaceDimension);

                foreach (var term in staticMv.Terms)
                    diffMv.SetTerm(term.Key, term.Value);

                foreach (var term in dynamicMv.Terms)
                    diffMv.UpdateTerm(term.Key, -term.Value);

                if (diffMv.Terms.Any(t => !t.Value.IsNearZero()))
                {
                    var composer = new LinearTextComposer();

                    composer
                        .Append("Invalid difference at i = ")
                        .AppendLine(i)
                        .Append("Static multivector = ")
                        .AppendLine(staticMv.ToString())
                        .AppendLineAtNewLine()
                        .Append("Dynamic multivector = ")
                        .AppendLine(dynamicMv.ToString());

                    Console.WriteLine(composer.ToString());
                }
            }
        }
    }
}