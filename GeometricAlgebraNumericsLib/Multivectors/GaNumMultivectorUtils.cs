using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Intermediate;
using GeometricAlgebraNumericsLib.Structures;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    public static class GaNumMultivectorUtils
    {
        public static GaTempMultivectorImplementation DefaultTempMultivectorKind { get; set; } 
            = GaTempMultivectorImplementation.Array;

        public static bool DisableCompactifyMultivectors { get; set; } = false;


        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForEGp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            var idStack1 = mv1.TermsTree.CreateNodeIDsStack();
            var idStack2 = mv2.TermsTree.CreateNodeIDsStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();

                var id1 = idStack1.Pop();
                var id2 = idStack2.Pop();

                if (node1.IsLeafNode)
                {
                    yield return new GaNumMultivectorBiTerm(
                        (int)id1, 
                        (int)id2, 
                        node1.Value, 
                        node2.Value
                    );

                    continue;
                }

                if (node1.HasChildNode0)
                {
                    if (node2.HasChildNode0)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode0);

                        idStack1.Push(id1);
                        idStack2.Push(id2);
                    }

                    if (node2.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode1);

                        idStack1.Push(id1);
                        idStack2.Push(id2 | node2.ChildNode1.BitMask);
                    }
                }

                if (node1.HasChildNode1)
                {
                    if (node2.HasChildNode0)
                    {
                        mvStack1.Push(node1.ChildNode1);
                        mvStack2.Push(node2.ChildNode0);

                        idStack1.Push(id1 | node1.ChildNode1.BitMask);
                        idStack2.Push(id2);
                    }

                    if (node2.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode1);
                        mvStack2.Push(node2.ChildNode1);

                        idStack1.Push(id1 | node1.ChildNode1.BitMask);
                        idStack2.Push(id2 | node2.ChildNode1.BitMask);
                    }
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForOp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            var idStack1 = mv1.TermsTree.CreateNodeIDsStack();
            var idStack2 = mv2.TermsTree.CreateNodeIDsStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = idStack1.Pop();
                var id2 = idStack2.Pop();

                if (node1.IsLeafNode)
                {
                    yield return new GaNumMultivectorBiTerm(
                        (int)id1,
                        (int)id2,
                        node1.Value,
                        node2.Value
                    );

                    continue;
                }

                if (node1.HasChildNode0)
                {
                    if (node2.HasChildNode0)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode0);

                        idStack1.Push(id1);
                        idStack2.Push(id2);
                    }

                    if (node2.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode1);

                        idStack1.Push(id1);
                        idStack2.Push(id2 | node2.ChildNode1.BitMask);
                    }
                }

                if (node1.HasChildNode1 && node2.HasChildNode0)
                {
                    mvStack1.Push(node1.ChildNode1);
                    mvStack2.Push(node2.ChildNode0);

                    idStack1.Push(id1 | node1.ChildNode1.BitMask);
                    idStack2.Push(id2);
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForESp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            var idStack1 = mv1.TermsTree.CreateNodeIDsStack();
            var idStack2 = mv2.TermsTree.CreateNodeIDsStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = idStack1.Pop();
                var id2 = idStack2.Pop();

                if (node1.IsLeafNode)
                {
                    yield return new GaNumMultivectorBiTerm(
                        (int)id1,
                        (int)id2,
                        node1.Value,
                        node2.Value
                    );

                    continue;
                }

                if (node1.HasChildNode0 && node2.HasChildNode0)
                {
                    mvStack1.Push(node1.ChildNode0);
                    mvStack2.Push(node2.ChildNode0);

                    idStack1.Push(id1);
                    idStack2.Push(id2);
                }

                if (node1.HasChildNode1 && node2.HasChildNode1)
                {
                    mvStack1.Push(node1.ChildNode1);
                    mvStack2.Push(node2.ChildNode1);

                    idStack1.Push(id1 | node1.ChildNode1.BitMask);
                    idStack2.Push(id2 | node2.ChildNode1.BitMask);
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForELcp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            var idStack1 = mv1.TermsTree.CreateNodeIDsStack();
            var idStack2 = mv2.TermsTree.CreateNodeIDsStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = idStack1.Pop();
                var id2 = idStack2.Pop();

                if (node1.IsLeafNode)
                {
                    yield return new GaNumMultivectorBiTerm(
                        (int)id1,
                        (int)id2,
                        node1.Value,
                        node2.Value
                    );

                    continue;
                }

                if (node1.HasChildNode0)
                {
                    if (node2.HasChildNode0)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode0);

                        idStack1.Push(id1);
                        idStack2.Push(id2);
                    }

                    if (node2.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode1);

                        idStack1.Push(id1);
                        idStack2.Push(id2 | node2.ChildNode1.BitMask);
                    }
                }

                if (node1.HasChildNode1 && node2.HasChildNode1)
                {
                    mvStack1.Push(node1.ChildNode1);
                    mvStack2.Push(node2.ChildNode1);

                    idStack1.Push(id1 | node1.ChildNode1.BitMask);
                    idStack2.Push(id2 | node2.ChildNode1.BitMask);
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForERcp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            var idStack1 = mv1.TermsTree.CreateNodeIDsStack();
            var idStack2 = mv2.TermsTree.CreateNodeIDsStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = idStack1.Pop();
                var id2 = idStack2.Pop();

                if (node1.IsLeafNode)
                {
                    yield return new GaNumMultivectorBiTerm(
                        (int)id1,
                        (int)id2,
                        node1.Value,
                        node2.Value
                    );

                    continue;
                }

                if (node1.HasChildNode0 && node2.HasChildNode0)
                {
                    mvStack1.Push(node1.ChildNode0);
                    mvStack2.Push(node2.ChildNode0);

                    idStack1.Push(id1);
                    idStack2.Push(id2);
                }

                if (node1.HasChildNode1)
                {
                    if (node2.HasChildNode0)
                    {
                        mvStack1.Push(node1.ChildNode1);
                        mvStack2.Push(node2.ChildNode0);

                        idStack1.Push(id1 | node1.ChildNode1.BitMask);
                        idStack2.Push(id2);
                    }

                    if (node2.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode1);
                        mvStack2.Push(node2.ChildNode1);

                        idStack1.Push(id1 | node1.ChildNode1.BitMask);
                        idStack2.Push(id2 | node2.ChildNode1.BitMask);
                    }
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForEFdp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            return GetBiTermsForEGp(mv1, mv2).Where(t => t.IsNonZeroFdp);
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForEHip(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            return GetBiTermsForEGp(mv1, mv2).Where(t => t.IsNonZeroHip);
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForEAcp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            return GetBiTermsForEGp(mv1, mv2).Where(t => t.IsNonZeroAcp);
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForECp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            return GetBiTermsForEGp(mv1, mv2).Where(t => t.IsNonZeroCp);
        }


        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForGp(this GaNumMultivector mv1, GaNumMultivector mv2, GaNumMetricOrthogonal orthogonalMetric)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            var idStack1 = mv1.TermsTree.CreateNodeIDsStack();
            var idStack2 = mv2.TermsTree.CreateNodeIDsStack();

            var metricStack = orthogonalMetric.RootNode.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = idStack1.Pop();
                var id2 = idStack2.Pop();
                var metricNode = metricStack.Pop();

                if (node1.IsLeafNode)
                {
                    yield return new GaNumMultivectorBiTerm(
                        (int)id1,
                        (int)id2,
                        node1.Value,
                        node2.Value,
                        metricNode.Value
                    );

                    continue;
                }

                if (node1.HasChildNode0 && metricNode.HasChildNode0)
                {
                    if (node2.HasChildNode0)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode0);

                        idStack1.Push(id1);
                        idStack2.Push(id2);

                        //0 and 0 = 0
                        metricStack.Push(metricNode.ChildNode0);
                    }

                    if (node2.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode1);

                        idStack1.Push(id1);
                        idStack2.Push(id2 | node2.ChildNode1.BitMask);

                        //1 and 0 = 0
                        metricStack.Push(metricNode.ChildNode0);
                    }
                }

                if (node1.HasChildNode1)
                {
                    if (node2.HasChildNode0 && metricNode.HasChildNode0)
                    {
                        mvStack1.Push(node1.ChildNode1);
                        mvStack2.Push(node2.ChildNode0);

                        idStack1.Push(id1 | node1.ChildNode1.BitMask);
                        idStack2.Push(id2);

                        //0 and 1 = 0
                        metricStack.Push(metricNode.ChildNode0);
                    }

                    if (node2.HasChildNode1 && metricNode.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode1);
                        mvStack2.Push(node2.ChildNode1);

                        idStack1.Push(id1 | node1.ChildNode1.BitMask);
                        idStack2.Push(id2 | node2.ChildNode1.BitMask);

                        //1 and 1 = 1
                        metricStack.Push(metricNode.ChildNode1);
                    }
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForSp(this GaNumMultivector mv1, GaNumMultivector mv2, GaNumMetricOrthogonal orthogonalMetric)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            var idStack1 = mv1.TermsTree.CreateNodeIDsStack();
            var idStack2 = mv2.TermsTree.CreateNodeIDsStack();

            var metricStack = orthogonalMetric.RootNode.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = idStack1.Pop();
                var id2 = idStack2.Pop();
                var metricNode = metricStack.Pop();

                if (node1.IsLeafNode)
                {
                    yield return new GaNumMultivectorBiTerm(
                        (int)id1,
                        (int)id2,
                        node1.Value,
                        node2.Value,
                        metricNode.Value
                    );

                    continue;
                }

                if (node1.HasChildNode0 && node2.HasChildNode0 && metricNode.HasChildNode0)
                {
                    mvStack1.Push(node1.ChildNode0);
                    mvStack2.Push(node2.ChildNode0);

                    idStack1.Push(id1);
                    idStack2.Push(id2);

                    //0 and 0 = 0
                    metricStack.Push(metricNode.ChildNode0);
                }

                if (node1.HasChildNode1 && node2.HasChildNode1 && metricNode.HasChildNode1)
                {
                    mvStack1.Push(node1.ChildNode1);
                    mvStack2.Push(node2.ChildNode1);

                    idStack1.Push(id1 | node1.ChildNode1.BitMask);
                    idStack2.Push(id2 | node2.ChildNode1.BitMask);

                    //1 and 1 = 1
                    metricStack.Push(metricNode.ChildNode1);
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForLcp(this GaNumMultivector mv1, GaNumMultivector mv2, GaNumMetricOrthogonal orthogonalMetric)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            var idStack1 = mv1.TermsTree.CreateNodeIDsStack();
            var idStack2 = mv2.TermsTree.CreateNodeIDsStack();

            var metricStack = orthogonalMetric.RootNode.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = idStack1.Pop();
                var id2 = idStack2.Pop();
                var metricNode = metricStack.Pop();

                if (node1.IsLeafNode)
                {
                    yield return new GaNumMultivectorBiTerm(
                        (int)id1,
                        (int)id2,
                        node1.Value,
                        node2.Value,
                        metricNode.Value
                    );

                    continue;
                }

                if (node1.HasChildNode0 && metricNode.HasChildNode0)
                {
                    if (node2.HasChildNode0)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode0);

                        idStack1.Push(id1);
                        idStack2.Push(id2);

                        //0 and 0 = 0
                        metricStack.Push(metricNode.ChildNode0);
                    }

                    if (node2.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode1);

                        idStack1.Push(id1);
                        idStack2.Push(id2 | node2.ChildNode1.BitMask);

                        //0 and 1 = 0
                        metricStack.Push(metricNode.ChildNode0);
                    }
                }

                if (node1.HasChildNode1 && node2.HasChildNode1 && metricNode.HasChildNode1)
                {
                    mvStack1.Push(node1.ChildNode1);
                    mvStack2.Push(node2.ChildNode1);

                    idStack1.Push(id1 | node1.ChildNode1.BitMask);
                    idStack2.Push(id2 | node2.ChildNode1.BitMask);

                    //1 and 1 = 1
                    metricStack.Push(metricNode.ChildNode1);
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForRcp(this GaNumMultivector mv1, GaNumMultivector mv2, GaNumMetricOrthogonal orthogonalMetric)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            var idStack1 = mv1.TermsTree.CreateNodeIDsStack();
            var idStack2 = mv2.TermsTree.CreateNodeIDsStack();

            var metricStack = orthogonalMetric.RootNode.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = idStack1.Pop();
                var id2 = idStack2.Pop();
                var metricNode = metricStack.Pop();

                if (node1.IsLeafNode)
                {
                    yield return new GaNumMultivectorBiTerm(
                        (int)id1,
                        (int)id2,
                        node1.Value,
                        node2.Value,
                        metricNode.Value
                    );

                    continue;
                }

                if (node1.HasChildNode0 && node2.HasChildNode0 && metricNode.HasChildNode0)
                {
                    mvStack1.Push(node1.ChildNode0);
                    mvStack2.Push(node2.ChildNode0);

                    //0 and 0 = 0
                    metricStack.Push(metricNode.ChildNode0);

                    idStack1.Push(id1);
                    idStack2.Push(id2);
                }

                if (node1.HasChildNode1)
                {
                    if (node2.HasChildNode0 && metricNode.HasChildNode0)
                    {
                        mvStack1.Push(node1.ChildNode1);
                        mvStack2.Push(node2.ChildNode0);

                        //0 and 1 = 0
                        metricStack.Push(metricNode.ChildNode0);

                        idStack1.Push(id1 | node1.ChildNode1.BitMask);
                        idStack2.Push(id2);
                    }

                    if (node2.HasChildNode1 && metricNode.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode1);
                        mvStack2.Push(node2.ChildNode1);

                        //1 and 1 = 1
                        metricStack.Push(metricNode.ChildNode1);

                        idStack1.Push(id1 | node1.ChildNode1.BitMask);
                        idStack2.Push(id2 | node2.ChildNode1.BitMask);
                    }
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForFdp(this GaNumMultivector mv1, GaNumMultivector mv2, GaNumMetricOrthogonal orthogonalMetric)
        {
            return GetBiTermsForGp(mv1, mv2, orthogonalMetric).Where(t => t.IsNonZeroFdp);
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForHip(this GaNumMultivector mv1, GaNumMultivector mv2, GaNumMetricOrthogonal orthogonalMetric)
        {
            return GetBiTermsForGp(mv1, mv2, orthogonalMetric).Where(t => t.IsNonZeroHip);
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForAcp(this GaNumMultivector mv1, GaNumMultivector mv2, GaNumMetricOrthogonal orthogonalMetric)
        {
            return GetBiTermsForGp(mv1, mv2, orthogonalMetric).Where(t => t.IsNonZeroAcp);
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForCp(this GaNumMultivector mv1, GaNumMultivector mv2, GaNumMetricOrthogonal orthogonalMetric)
        {
            return GetBiTermsForGp(mv1, mv2, orthogonalMetric).Where(t => t.IsNonZeroCp);
        }


        public static GaNumMultivector SetTerms(this GaNumMultivector mv, IEnumerable<KeyValuePair<int, double>> terms)
        {
            foreach (var term in terms)
                mv.SetTermCoef(term.Key, term.Value);

            return mv;
        }

        public static IGaNumMultivectorTemp SetTerms(this IGaNumMultivectorTemp tempMv, IEnumerable<KeyValuePair<int, double>> terms)
        {
            foreach (var term in terms)
                tempMv.SetTermCoef(term.Key, term.Value);

            return tempMv;
        }

        public static IGaNumMultivectorTemp SetTerms(this IGaNumMultivectorTemp tempMv, GaNumMultivector termsMv)
        {
            foreach (var term in termsMv.NonZeroTerms)
                tempMv.SetTermCoef(term.Key, term.Value);

            return tempMv;
        }

        public static IGaNumMultivectorTemp SetTerms(this IGaNumMultivectorTemp tempMv, IGaNumMultivectorTemp termsMv)
        {
            foreach (var term in termsMv.NonZeroTerms)
                tempMv.SetTermCoef(term.Key, term.Value);

            return tempMv;
        }


        public static IGaNumMultivectorTemp AddFactors(this IGaNumMultivectorTemp tempMv, double scalar, IEnumerable<KeyValuePair<int, double>> terms)
        {
            foreach (var term in terms)
                tempMv.AddFactor(term.Key, term.Value * scalar);

            return tempMv;
        }

        public static IGaNumMultivectorTemp AddFactors(this IGaNumMultivectorTemp tempMv, double scalar, GaNumMultivector termsMv)
        {
            foreach (var term in termsMv.NonZeroTerms)
                tempMv.AddFactor(term.Key, term.Value * scalar);

            return tempMv;
        }

        public static IGaNumMultivectorTemp AddFactors(this IGaNumMultivectorTemp tempMv, double scalar, IGaNumMultivectorTemp termsMv)
        {
            foreach (var term in termsMv.NonZeroTerms)
                tempMv.AddFactor(term.Key, term.Value * scalar);

            return tempMv;
        }

        public static IGaNumMultivectorTemp AddFactors(this IGaNumMultivectorTemp tempMv, IEnumerable<GaNumMultivectorBiTerm> biTerms)
        {
            foreach (var biTerm in biTerms)
                tempMv.AddFactor(
                    biTerm.IdXor,
                    biTerm.IsNegativeEGp,
                    biTerm.TotalProduct
                );

            return tempMv;
        }

        public static IGaNumMultivectorTemp AddFactors(this IGaNumMultivectorTemp tempMv, IEnumerable<GaNumMultivectorBiTerm> biTerms, GaNumMetricOrthonormal orthonormalMetric)
        {
            foreach (var biTerm in biTerms)
                tempMv.AddFactor(
                    biTerm.IdXor,
                    biTerm.IsNegativeEGp,
                    biTerm.ValuesProduct * orthonormalMetric[biTerm.IdAnd]
                );

            return tempMv;
        }

        public static IGaNumMultivectorTemp AddFactors(this IGaNumMultivectorTemp tempMv, IEnumerable<GaNumMultivectorBiTerm> biTerms, GaNumMetricOrthogonal orthogonalMetric)
        {
            foreach (var biTerm in biTerms)
                tempMv.AddFactor(
                    biTerm.IdXor,
                    biTerm.IsNegativeEGp,
                    biTerm.TotalProduct * biTerm.Value2 * orthogonalMetric[biTerm.IdAnd]
                );

            return tempMv;
        }


        public static bool IsNullOrZero(this IGaNumMultivector mv)
        {
            return ReferenceEquals(mv, null) || mv.IsZero();
        }


        public static IGaNumMultivectorTemp ToTempMultivector(this IGaNumMultivector mv)
        {
            if (ReferenceEquals(mv, null))
                return null;

            var tempMv = GaNumMultivector.CreateZeroTemp(mv.GaSpaceDimension);

            foreach (var term in mv.NonZeroTerms)
                tempMv.SetTermCoef(term.Key, term.Value);

            return tempMv;
        }

        public static IGaNumMultivector Compactify(this IGaNumMultivector mv, bool returnZeroMvAsNull = false)
        {
            if (DisableCompactifyMultivectors)
                return mv;

            if (ReferenceEquals(mv, null))
                return null;

            //Make sure this is not a temp multivector
            if (mv.IsTemp)
                mv = mv.ToMultivector();

            //If it's zero return null or a zero term depending on returnZeroMvAsNull
            if (mv.IsZero())
                return returnZeroMvAsNull 
                    ? null 
                    : GaNumMultivectorTerm.CreateZero(mv.GaSpaceDimension);

            //If it's a non-zero term return it as is
            var termMv = mv as GaNumMultivectorTerm;
            if (!ReferenceEquals(termMv, null))
                return termMv;

            //It's a full multivector
            var fullMv = (GaNumMultivector)mv;

            //If it's not a term return it after simplification
            if (!mv.IsTerm())
            {
                fullMv.Simplify();
                return fullMv;
            }

            //It's a full multivector containing a single term
            var term = mv.NonZeroTerms.FirstOrDefault();
            return GaNumMultivectorTerm.CreateTerm(mv.GaSpaceDimension, term.Key, term.Value);
        }

        public static GaNumMultivectorHashTable1D Compactify(this GaSparseTable1D<int, GaNumMultivector> mvTable)
        {
            var resultTable = new GaNumMultivectorHashTable1D();

            foreach (var pair in mvTable)
            {
                var id = pair.Key;
                var mv = pair.Value.Compactify(true);

                if (!ReferenceEquals(mv, null))
                    resultTable[id] = mv;
            }

            return resultTable;
        }

    }
}
