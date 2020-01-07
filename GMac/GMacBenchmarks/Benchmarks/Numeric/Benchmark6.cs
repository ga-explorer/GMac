using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;

namespace GMacBenchmarks.Benchmarks.Numeric
{
    /// <summary>
    /// Benchmark read\write operations to multivectors
    /// </summary>
    public class Benchmark6
    {
        private GaRandomGenerator _randGen;
        private GaNumFrame _frame;
        private GaNumMultivector _mv1;
        private Dictionary<int, double> _mv2;

        private GaNumMultivector _mv3;

        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            _frame = GaNumFrame.CreateQuadricConformal();

            _mv1 = _randGen.GetNumBlade(_frame.GaSpaceDimension, 8);
            _mv2 = _mv1.Terms.ToDictionary(t => t.Key, t => t.Value);

            _mv3 = GaNumMultivector.CreateZero(_frame.GaSpaceDimension);
            //_mv4 = new Dictionary<int, double>();
            //_mv5 = new double[_frame.GaSpaceDimension];
        }

        [Benchmark]
        public double[] ReadTermsFromDictionary()
        {
            return Enumerable
                .Range(0, _frame.GaSpaceDimension)
                .Select(id => 
                    _mv2.TryGetValue(id, out var value) ? value : 0.0d
                ).ToArray();
        }

        [Benchmark]
        public double[] ReadTermsFromTree()
        {
            return Enumerable
                .Range(0, _frame.GaSpaceDimension)
                .Select(id => _mv1[id]).ToArray();
        }

        [Benchmark]
        public void WriteTermsToDictionary()
        {
            _mv2.Clear();
            foreach (var id in Enumerable.Range(0, _frame.GaSpaceDimension))
            {
                if (_mv2.ContainsKey(id))
                    _mv2[id] = 1;
                else
                    _mv2.Add(id, 1);
            }
        }

        [Benchmark]
        public void WriteTermsToTree()
        {
            _mv3.ResetToZero();
            foreach (var id1 in Enumerable.Range(0, _frame.GaSpaceDimension))
            {
                _mv3.SetTerm(id1, 1);
            }
        }

        //[Benchmark]
        public void WriteTermsToArray()
        {
            var mv5 = new double[_frame.GaSpaceDimension];
            foreach (var id1 in Enumerable.Range(0, _frame.GaSpaceDimension))
            {
                if (!_mv2.TryGetValue(id1, out var c1)) 
                    continue;

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
                    _mv3.SetTerm(id, c);
            }
        }
    }
}