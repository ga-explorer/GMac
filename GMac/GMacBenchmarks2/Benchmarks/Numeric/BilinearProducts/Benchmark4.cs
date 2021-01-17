using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GeometricAlgebraNumericsLib;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraStructuresLib.Frames;

namespace GMacBenchmarks2.Benchmarks.Numeric.BilinearProducts
{
    /// <summary>
    /// Benchmark computed implementation method of standard products on orthogonal frames for sparse multivectors
    /// </summary>
    public class Benchmark4
    {
        private GaRandomGenerator _randGen;
        private GaNumMetricOrthogonal _metric;

        private IGaNumMultivector[] _multivectorsList1;
        private IGaNumMultivector[] _multivectorsList2;

        [Params("dgr", "sgr", "sar")]
        public string MultivectorName { get; set; } = "dgr";

        [Params("op", "gp", "lcp", "sp")]
        public string ProductName { get; set; }

        [Params(3, 4, 5, 6, 7, 8, 9, 10)]
        public int VSpaceDimension { get; set; }

        public int GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();

        public Func<IGaNumMultivector, IGaNumMultivector, IEnumerable<GaTerm<double>>> Product { get; set; }

        public int MultivectorsCount { get; }
            = 10;


        private IEnumerable<GaTerm<double>> Gp(IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return mv1.GetGbtGpTerms(mv2, _metric);
        }

        private IEnumerable<GaTerm<double>> Op(IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return mv1.GetGbtOpTerms(mv2);
        }

        private IEnumerable<GaTerm<double>> Lcp(IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return mv1.GetGbtLcpTerms(mv2, _metric);
        }

        private IEnumerable<GaTerm<double>> Sp(IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return mv1.GetGbtSpTerms(mv2, _metric);
        }


        [GlobalSetup]
        public void Setup()
        {
            _randGen = new GaRandomGenerator(10);

            var basisVectorsSignatures = _randGen.GetScalars(VSpaceDimension).ToArray();
            //basisVectorsSignatures[0] = 0;
            //basisVectorsSignatures[1] = 0;
            _metric = GaNumMetricOrthogonal.Create(basisVectorsSignatures);

            _multivectorsList1 = new IGaNumMultivector[VSpaceDimension + 1];
            _multivectorsList2 = new IGaNumMultivector[VSpaceDimension + 1];

            for (var grade = 0; grade < VSpaceDimension + 1; grade++)
            {
                var termsList1 = _randGen.GetNumSparseMultivectorTerms(VSpaceDimension).ToArray();
                var termsList2 = _randGen.GetNumSparseMultivectorTerms(VSpaceDimension).ToArray();

                if (MultivectorName == "sar")
                {
                    _multivectorsList1[grade] = termsList1.CreateSarMultivector(VSpaceDimension);
                    _multivectorsList2[grade] = termsList2.CreateSarMultivector(VSpaceDimension);

                    _multivectorsList1[grade].GetBtrRootNode();
                    _multivectorsList2[grade].GetBtrRootNode();
                }
                else if (MultivectorName == "dgr")
                {
                    _multivectorsList1[grade] = termsList1.CreateDgrMultivector(VSpaceDimension);
                    _multivectorsList2[grade] = termsList2.CreateDgrMultivector(VSpaceDimension);
                }
                else
                {
                    _multivectorsList1[grade] = termsList1.CreateSgrMultivector(VSpaceDimension);
                    _multivectorsList2[grade] = termsList2.CreateSgrMultivector(VSpaceDimension);
                }
            }

            if (ProductName == "gp")
                Product = Gp;

            else if (ProductName == "lcp")
                Product = Lcp;

            else if (ProductName == "sp")
                Product = Sp;

            else
                Product = Op;
        }


        [Benchmark]
        public GaTerm<double> OrthogonalProduct()
        {
            GaTerm<double> term = null;

            for (var grade1 = 0; grade1 < VSpaceDimension + 1; grade1++)
            {
                for (var grade2 = 0; grade2 < VSpaceDimension + 1; grade2++)
                {
                    var termsList = Product(
                        _multivectorsList1[grade1],
                        _multivectorsList2[grade2]
                    );

                    foreach (var t in termsList)
                        term = t;
                }
            }

            return term;
        }
    }
}