using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.Collections;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories
{
    public static class GaNumMultivectorFactoryUtils
    {
        public static IReadOnlyDictionary<int, double> GetNegative(this IReadOnlyDictionary<int, double> scalarValues)
        {
            var dict = new Dictionary<int, double>(scalarValues.Count);

            foreach (var pair in scalarValues)
                dict.Add(pair.Key, pair.Value);

            return dict;
        }

        public static IReadOnlyList<IReadOnlyDictionary<int, double>> ToSgrKVectorsList(this IEnumerable<Dictionary<int, double>> kVectors)
        {
            return kVectors.Cast<IReadOnlyDictionary<int, double>>().ToArray();
        }


        public static GaNumDarMultivector CreateDarMultivector(this GaBtrInternalNode<double> btrRootNode)
        {
            var treeDepth = btrRootNode.GetTreeDepth();

            return btrRootNode.GetTerms(treeDepth).CreateDarMultivector(treeDepth);
        }

        public static GaNumDgrMultivector CreateDgrMultivector(this GaBtrInternalNode<double> btrRootNode)
        {
            var treeDepth = btrRootNode.GetTreeDepth();

            return btrRootNode.GetTerms(treeDepth).CreateDgrMultivector(treeDepth);
        }

        public static GaNumSarMultivector CreateSarMultivector(this GaBtrInternalNode<double> btrRootNode)
        {
            var treeDepth = btrRootNode.GetTreeDepth();

            return btrRootNode.GetTerms(treeDepth).CreateSarMultivector(treeDepth);
        }

        public static GaNumSgrMultivector CreateSgrMultivector(this GaBtrInternalNode<double> btrRootNode)
        {
            var treeDepth = btrRootNode.GetTreeDepth();

            return btrRootNode.GetTerms(treeDepth).CreateSgrMultivector(treeDepth);
        }

        public static IEnumerable<GaNumDarKVector> CreateDarKVectors(this GaBtrInternalNode<double> btrRootNode)
        {
            var treeDepth = btrRootNode.GetTreeDepth();

            return btrRootNode.GetTerms(treeDepth).CreateDarKVectors(treeDepth);
        }


        public static GaNumDarMultivector CreateDarMultivector(this GaBtrInternalNode<double> btrRootNode, int treeDepth)
        {
            return btrRootNode.GetTerms(treeDepth).CreateDarMultivector(treeDepth);
        }

        public static GaNumDgrMultivector CreateDgrMultivector(this GaBtrInternalNode<double> btrRootNode, int treeDepth)
        {
            return btrRootNode.GetTerms(treeDepth).CreateDgrMultivector(treeDepth);
        }

        public static GaNumSarMultivector CreateSarMultivector(this GaBtrInternalNode<double> btrRootNode, int treeDepth)
        {
            return btrRootNode.GetTerms(treeDepth).CreateSarMultivector(treeDepth);
        }

        public static GaNumSgrMultivector CreateSgrMultivector(this GaBtrInternalNode<double> btrRootNode, int treeDepth)
        {
            return btrRootNode.GetTerms(treeDepth).CreateSgrMultivector(treeDepth);
        }

        public static IEnumerable<GaNumDarKVector> CreateDarKVectors(this GaBtrInternalNode<double> btrRootNode, int treeDepth)
        {
            return btrRootNode.GetTerms(treeDepth).CreateDarKVectors(treeDepth);
        }


        public static GaNumDarMultivector CreateDarMultivector(this GaTerm<double> term, int vSpaceDim)
        {
            var scalarValues = new SingleItemReadOnlyList<double>(
                vSpaceDim.ToGaSpaceDimension(),
                term.BasisBladeId,
                term.ScalarValue
            );

            return new GaNumDarMultivector(scalarValues);
        }

        public static GaNumDgrMultivector CreateDgrMultivector(this GaTerm<double> term, int vSpaceDim)
        {
            term.BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

            var kVectorsArray = new IReadOnlyList<double>[vSpaceDim + 1];

            kVectorsArray[grade] = new SingleItemReadOnlyList<double>(
                GaFrameUtils.KvSpaceDimension(vSpaceDim, grade),
                index,
                term.ScalarValue
            );

            return new GaNumDgrMultivector(kVectorsArray);
        }

        public static GaNumSarMultivector CreateSarMultivector(this GaTerm<double> term, int vSpaceDim)
        {
            var scalarValues = new Dictionary<int, double>
            {
                {term.BasisBladeId, term.ScalarValue}
            };

            return new GaNumSarMultivector(vSpaceDim, scalarValues);
        }

        public static GaNumSgrMultivector CreateSgrMultivector(this GaTerm<double> term, int vSpaceDim)
        {
            var kVectors = new Dictionary<int, double>[vSpaceDim + 1];

            for (var grade = 0; grade <= vSpaceDim; grade++)
                kVectors[grade] = new Dictionary<int, double>();

            kVectors[1].Add(term.BasisBladeId, term.ScalarValue);
            
            return new GaNumSgrMultivector(
                kVectors.ToSgrKVectorsList()
            );
        }

        public static GaNumDarKVector CreateDarKVector(this GaTerm<double> term, int vSpaceDim)
        {
            term.BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

            var scalarValues = new SingleItemReadOnlyList<double>(
                GaFrameUtils.KvSpaceDimension(vSpaceDim, grade),
                index,
                term.ScalarValue
            );

            return new GaNumDarKVector(vSpaceDim, grade, scalarValues);
        }



        public static GaNumDarMultivector CreateDarMultivector(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            Debug.Assert(termsList.ContainsUniqueIDs());

            var factory = new GaNumDarMultivectorFactory(vSpaceDim);

            factory.SetTerms(termsList);

            return factory.GetDarMultivector();
        }

        public static GaNumDgrMultivector CreateDgrMultivector(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            Debug.Assert(termsList.ContainsUniqueIDs());

            var factory = new GaNumDgrMultivectorFactory(vSpaceDim);

            factory.SetTerms(termsList);

            return factory.GetDgrMultivector();
        }

        public static GaNumSgrMultivector CreateSgrMultivector(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            Debug.Assert(termsList.ContainsUniqueIDs());

            var factory = new GaNumSgrMultivectorFactory(vSpaceDim);

            factory.SetTerms(termsList);

            return factory.GetSgrMultivector();
        }

        public static GaNumSarMultivector CreateSarMultivector(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            Debug.Assert(termsList.ContainsUniqueIDs());

            var factory = new GaNumSarMultivectorFactory(vSpaceDim);

            factory.SetTerms(termsList);

            return factory.GetSarMultivector();
        }

        public static IEnumerable<GaNumDarKVector> CreateDarKVectors(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            Debug.Assert(termsList.ContainsUniqueIDs());

            var factory = new GaNumDgrMultivectorFactory(vSpaceDim);

            factory.SetTerms(termsList);

            return factory.GetStoredDarKVectors();
        }


        public static GaNumDarMultivector SumAsDarMultivector(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            var factory = new GaNumDarMultivectorFactory(vSpaceDim);

            factory.AddTerms(termsList);

            return factory.GetDarMultivector();
        }

        public static GaNumDgrMultivector SumAsDgrMultivector(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            var factory = new GaNumDgrMultivectorFactory(vSpaceDim);

            factory.AddTerms(termsList);

            return factory.GetDgrMultivector();
        }

        public static GaNumSarMultivector SumAsSarMultivector(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            var factory = new GaNumSarMultivectorFactory(vSpaceDim);

            factory.AddTerms(termsList);

            return factory.GetSarMultivector();
        }

        public static GaNumSgrMultivector SumAsSgrMultivector(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            var factory = new GaNumSgrMultivectorFactory(vSpaceDim);

            factory.AddTerms(termsList);

            return factory.GetSgrMultivector();
        }

        public static IEnumerable<GaNumDarKVector> SumAsDarKVectors(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            var factory = new GaNumDgrMultivectorFactory(vSpaceDim);

            factory.AddTerms(termsList);

            return factory.GetStoredDarKVectors();
        }


        public static GaNumDarMultivectorFactory CreateDenseMultivectorFactory(this GaTerm<double> term, int vSpaceDim)
        {
            var factory = new GaNumDarMultivectorFactory(vSpaceDim);

            factory.SetTerm(term);

            return factory;
        }

        public static GaNumDgrMultivectorFactory CreateDenseGradedMultivectorFactory(this GaTerm<double> term, int vSpaceDim)
        {
            var factory = new GaNumDgrMultivectorFactory(vSpaceDim);

            factory.SetTerm(term);

            return factory;
        }

        public static GaNumSarMultivectorFactory CreateIntoSparseMultivectorFactory(this GaTerm<double> term, int vSpaceDim)
        {
            var factory = new GaNumSarMultivectorFactory(vSpaceDim);

            factory.SetTerm(term);

            return factory;
        }

        public static GaNumSgrMultivectorFactory CreateSparseGradedMultivectorFactory(this GaTerm<double> term, int vSpaceDim)
        {
            var factory = new GaNumSgrMultivectorFactory(vSpaceDim);

            factory.SetTerm(term);

            return factory;
        }

        
        public static GaNumDarMultivectorFactory CreateDenseMultivectorFactory(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            Debug.Assert(termsList.ContainsUniqueIDs());

            var factory = new GaNumDarMultivectorFactory(vSpaceDim);

            factory.SetTerms(termsList);

            return factory;
        }

        public static GaNumDgrMultivectorFactory CreateDenseGradedMultivectorFactory(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            Debug.Assert(termsList.ContainsUniqueIDs());

            var factory = new GaNumDgrMultivectorFactory(vSpaceDim);

            factory.SetTerms(termsList);

            return factory;
        }

        public static GaNumSarMultivectorFactory CreateIntoSparseMultivectorFactory(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            Debug.Assert(termsList.ContainsUniqueIDs());

            var factory = new GaNumSarMultivectorFactory(vSpaceDim);

            factory.SetTerms(termsList);

            return factory;
        }

        public static GaNumSgrMultivectorFactory CreateSparseGradedMultivectorFactory(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            Debug.Assert(termsList.ContainsUniqueIDs());

            var factory = new GaNumSgrMultivectorFactory(vSpaceDim);

            factory.SetTerms(termsList);

            return factory;
        }


        public static GaNumDarMultivectorFactory SumIntoDenseMultivectorFactory(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            var factory = new GaNumDarMultivectorFactory(vSpaceDim);

            factory.AddTerms(termsList);

            return factory;
        }

        public static GaNumDgrMultivectorFactory SumIntoDenseGradedMultivectorFactory(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            var factory = new GaNumDgrMultivectorFactory(vSpaceDim);

            factory.AddTerms(termsList);

            return factory;
        }

        public static GaNumSarMultivectorFactory SumIntoSparseMultivectorFactory(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            var factory = new GaNumSarMultivectorFactory(vSpaceDim);

            factory.AddTerms(termsList);

            return factory;
        }

        public static GaNumSgrMultivectorFactory SumIntoSparseGradedMultivectorFactory(this IEnumerable<GaTerm<double>> termsList, int vSpaceDim)
        {
            var factory = new GaNumSgrMultivectorFactory(vSpaceDim);

            factory.AddTerms(termsList);

            return factory;
        }


        public static GaNumDarMultivectorFactory SumIntoDenseMultivectorFactory(this IEnumerable<GaNumDarKVector> kVectorsList, int vSpaceDim)
        {
            var factory = new GaNumDarMultivectorFactory(vSpaceDim);

            factory.AddKVectors(kVectorsList);

            return factory;
        }

        public static GaNumDgrMultivectorFactory SumIntoDenseGradedMultivectorFactory(this IEnumerable<GaNumDarKVector> kVectorsList, int vSpaceDim)
        {
            var factory = new GaNumDgrMultivectorFactory(vSpaceDim);

            factory.AddKVectors(kVectorsList);

            return factory;
        }

        public static GaNumSarMultivectorFactory SumIntoSparseMultivectorFactory(this IEnumerable<GaNumDarKVector> kVectorsList, int vSpaceDim)
        {
            var factory = new GaNumSarMultivectorFactory(vSpaceDim);

            factory.AddKVectors(kVectorsList);

            return factory;
        }

        public static GaNumSgrMultivectorFactory SumIntoSparseGradedMultivectorFactory(this IEnumerable<GaNumDarKVector> kVectorsList, int vSpaceDim)
        {
            var factory = new GaNumSgrMultivectorFactory(vSpaceDim);

            factory.AddKVectors(kVectorsList);

            return factory;
        }


        public static GaNumDarMultivectorFactory SumIntoDenseMultivectorFactory(this IEnumerable<KeyValuePair<double, GaNumDarKVector>> kVectorsList, int vSpaceDim)
        {
            var factory = new GaNumDarMultivectorFactory(vSpaceDim);

            factory.AddKVectors(kVectorsList);

            return factory;
        }

        public static GaNumDgrMultivectorFactory SumIntoDenseGradedMultivectorFactory(this IEnumerable<KeyValuePair<double, GaNumDarKVector>> kVectorsList, int vSpaceDim)
        {
            var factory = new GaNumDgrMultivectorFactory(vSpaceDim);

            factory.AddKVectors(kVectorsList);

            return factory;
        }

        public static GaNumSarMultivectorFactory SumIntoSparseMultivectorFactory(this IEnumerable<KeyValuePair<double, GaNumDarKVector>> kVectorsList, int vSpaceDim)
        {
            var factory = new GaNumSarMultivectorFactory(vSpaceDim);

            factory.AddKVectors(kVectorsList);

            return factory;
        }

        public static GaNumSgrMultivectorFactory SumIntoSparseGradedMultivectorFactory(this IEnumerable<KeyValuePair<double, GaNumDarKVector>> kVectorsList, int vSpaceDim)
        {
            var factory = new GaNumSgrMultivectorFactory(vSpaceDim);

            factory.AddKVectors(kVectorsList);

            return factory;
        }


        public static GaNumDarMultivectorFactory SumIntoDenseMultivectorFactory(this IEnumerable<Tuple<double, GaNumDarKVector>> kVectorsList, int vSpaceDim)
        {
            var factory = new GaNumDarMultivectorFactory(vSpaceDim);

            factory.AddKVectors(kVectorsList);

            return factory;
        }

        public static GaNumDgrMultivectorFactory SumIntoDenseGradedMultivectorFactory(this IEnumerable<Tuple<double, GaNumDarKVector>> kVectorsList, int vSpaceDim)
        {
            var factory = new GaNumDgrMultivectorFactory(vSpaceDim);

            factory.AddKVectors(kVectorsList);

            return factory;
        }

        public static GaNumSarMultivectorFactory SumIntoSparseMultivectorFactory(this IEnumerable<Tuple<double, GaNumDarKVector>> kVectorsList, int vSpaceDim)
        {
            var factory = new GaNumSarMultivectorFactory(vSpaceDim);

            factory.AddKVectors(kVectorsList);

            return factory;
        }

        public static GaNumSgrMultivectorFactory SumIntoSparseGradedMultivectorFactory(this IEnumerable<Tuple<double, GaNumDarKVector>> kVectorsList, int vSpaceDim)
        {
            var factory = new GaNumSgrMultivectorFactory(vSpaceDim);

            factory.AddKVectors(kVectorsList);

            return factory;
        }


        public static GaNumMultivectorFactory AddSubtractionTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            factory.AddTerms(mv1.GetNonZeroTerms());
            factory.AddTerms(-1.0d, mv2.GetNonZeroTerms());

            return factory;
        }


        public static GaNumMultivectorFactory AddGbtOpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetGbtOpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddGbtEGpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetGbtEGpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddGbtELcpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetGbtELcpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddGbtERcpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetGbtERcpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddGbtESpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetGbtESpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddGbtEFdpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetGbtEFdpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddGbtEHipTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetGbtEHipTerms(mv2));
        }

        public static GaNumMultivectorFactory AddGbtEAcpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetGbtEAcpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddGbtECpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetGbtECpTerms(mv2));
        }


        public static GaNumMultivectorFactory AddGbtGpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetGbtGpTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddGbtLcpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetGbtLcpTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddGbtRcpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetGbtRcpTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddGbtSpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetGbtSpTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddGbtFdpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetGbtFdpTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddGbtHipTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetGbtHipTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddGbtAcpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetGbtAcpTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddGbtCpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetGbtCpTerms(mv2, metric));
        }


        public static GaNumMultivectorFactory AddLoopOpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetLoopOpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddLoopEGpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetLoopEGpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddLoopELcpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetLoopELcpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddLoopERcpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetLoopERcpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddLoopESpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetLoopESpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddLoopEFdpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetLoopEFdpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddLoopEHipTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetLoopEHipTerms(mv2));
        }

        public static GaNumMultivectorFactory AddLoopEAcpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetLoopEAcpTerms(mv2));
        }

        public static GaNumMultivectorFactory AddLoopECpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            return factory.AddTerms(mv1.GetLoopECpTerms(mv2));
        }


        public static GaNumMultivectorFactory AddLoopGpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetLoopGpTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddLoopLcpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetLoopLcpTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddLoopRcpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetLoopRcpTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddLoopSpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetLoopSpTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddLoopFdpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetLoopFdpTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddLoopHipTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetLoopHipTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddLoopAcpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetLoopAcpTerms(mv2, metric));
        }

        public static GaNumMultivectorFactory AddLoopCpTerms(this GaNumMultivectorFactory factory, IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            return factory.AddTerms(mv1.GetLoopCpTerms(mv2, metric));
        }
    }
}
