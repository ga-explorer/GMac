using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GMacBenchmarks.Benchmarks.Numeric
{
    public class GMacNumericBenchmark2
    {
        private GaRandomGenerator _randGen;
        private GaNumFrame _frame;
        private GaNumMultivector _mv1;
        private Dictionary<int, double> _mv2;

        private GaNumMultivector _mv3;

        //[Params(
        //    GaBilinearProductImplementation.ComputedLoop,
        //    GaBilinearProductImplementation.ComputedTree,
        //    GaBilinearProductImplementation.LookupArray,
        //    GaBilinearProductImplementation.LookupCoefSums,
        //    GaBilinearProductImplementation.LookupHash,
        //    GaBilinearProductImplementation.LookupTree
        //)]
        //public GaBilinearProductImplementation ProductsImplementation { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            _frame = GaNumFrame.CreateConformal(5);
            //_frame.SelectBilinearProductsImplementation(ProductsImplementation);

            _mv1 = _randGen.GetNumMultivectorFull(_frame.GaSpaceDimension);
            _mv2 = _mv1.Terms.ToDictionary(t => t.Key, t => t.Value);

            _mv3 = GaNumMultivector.CreateZero(_frame.GaSpaceDimension);
            //_mv4 = new Dictionary<int, double>();
            //_mv5 = new double[_frame.GaSpaceDimension];
        }

        //[Benchmark]
        public double[] ReadTermsFromTree()
        {
            return Enumerable
                .Range(0, _frame.GaSpaceDimension)
                .Select(id => _mv1[id]).ToArray();
        }

        //[Benchmark]
        public double[] ReadTermsFromDictionary()
        {
            return Enumerable
                .Range(0, _frame.GaSpaceDimension)
                .Select(id => _mv2[id]).ToArray();
        }

        [Benchmark]
        public void WriteTermsToTree()
        {
            _mv3.ResetToZero();
            foreach (var id1 in Enumerable.Range(0, _frame.GaSpaceDimension))
            {
                var c1 = _mv2[id1];

                foreach (var id2 in Enumerable.Range(0, _frame.GaSpaceDimension))
                    _mv3.AddFactor(id1 ^ id2, c1 * _mv1[id2]);
            }
        }

        [Benchmark]
        public void WriteTermsToDictionary()
        {
            var mv4 = new Dictionary<int, double>();
            foreach (var id1 in Enumerable.Range(0, _frame.GaSpaceDimension))
            {
                var c1 = _mv2[id1];

                foreach (var id2 in Enumerable.Range(0, _frame.GaSpaceDimension))
                {
                    var id = id1 ^ id2;
                    var c2 = _mv2[id2];

                    double oldValue;
                    if (mv4.TryGetValue(id, out oldValue))
                        mv4[id] = oldValue + c1 * c2;
                    else
                        mv4.Add(id, c1 * c2);
                }
            }

            foreach (var term in mv4.Where(p => !p.Value.IsNearZero()))
                _mv3.SetTermCoef(term.Key, term.Value);
        }

        [Benchmark]
        public void WriteTermsToArray()
        {
            var mv5 = new double[_frame.GaSpaceDimension];
            foreach (var id1 in Enumerable.Range(0, _frame.GaSpaceDimension))
            {
                var c1 = _mv2[id1];

                foreach (var id2 in Enumerable.Range(0, _frame.GaSpaceDimension))
                {
                    var id = id1 ^ id2;
                    var c2 = _mv2[id2];

                    mv5[id] += c1 * c2;
                }
            }

            _mv3.ResetToZero();
            foreach (var id in Enumerable.Range(0, _frame.GaSpaceDimension))
            {
                var c = mv5[id];

                if (!c.IsNearZero())
                    _mv3.SetTermCoef(id, c);
            }
        }
    }
}