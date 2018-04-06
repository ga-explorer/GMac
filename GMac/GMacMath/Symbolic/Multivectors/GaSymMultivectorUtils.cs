using System.Collections.Generic;
using System.Linq;
using GMac.GMacMath.Symbolic.Metrics;
using GMac.GMacMath.Symbolic.Multivectors.Hash;
using GMac.GMacMath.Symbolic.Multivectors.Intermediate;
using GMac.GMacMath.Symbolic.Multivectors.Tree;
using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.ExprFactory;
using UtilLib.DataStructures.SparseTable;
using Wolfram.NETLink;

namespace GMac.GMacMath.Symbolic.Multivectors
{
    public static class GaSymMultivectorUtils
    {
        public static GaTempMultivectorImplementation DefaultTempMultivectorKind { get; set; }
            = GaTempMultivectorImplementation.Tree;

        public static bool DisableCompactifyMultivectors { get; set; } = false;


        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForEGp(this GaSymMultivector mv1, GaSymMultivector mv2)
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
                    yield return new GaSymMultivectorBiTerm(
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

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForOp(this GaSymMultivector mv1, GaSymMultivector mv2)
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
                    yield return new GaSymMultivectorBiTerm(
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

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForESp(this GaSymMultivector mv1, GaSymMultivector mv2)
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
                    yield return new GaSymMultivectorBiTerm(
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

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForELcp(this GaSymMultivector mv1, GaSymMultivector mv2)
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
                    yield return new GaSymMultivectorBiTerm(
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

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForERcp(this GaSymMultivector mv1, GaSymMultivector mv2)
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
                    yield return new GaSymMultivectorBiTerm(
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

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForEFdp(this GaSymMultivector mv1, GaSymMultivector mv2)
        {
            return GetBiTermsForEGp(mv1, mv2).Where(t => t.IsNonZeroFdp);
        }

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForEHip(this GaSymMultivector mv1, GaSymMultivector mv2)
        {
            return GetBiTermsForEGp(mv1, mv2).Where(t => t.IsNonZeroHip);
        }

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForEAcp(this GaSymMultivector mv1, GaSymMultivector mv2)
        {
            return GetBiTermsForEGp(mv1, mv2).Where(t => t.IsNonZeroAcp);
        }

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForECp(this GaSymMultivector mv1, GaSymMultivector mv2)
        {
            return GetBiTermsForEGp(mv1, mv2).Where(t => t.IsNonZeroCp);
        }


        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForGp(this GaSymMultivector mv1, GaSymMultivector mv2, GaSymMetricOrthogonal orthogonalMetric)
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
                    yield return new GaSymMultivectorBiTerm(
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

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForSp(this GaSymMultivector mv1, GaSymMultivector mv2, GaSymMetricOrthogonal orthogonalMetric)
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
                    yield return new GaSymMultivectorBiTerm(
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

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForLcp(this GaSymMultivector mv1, GaSymMultivector mv2, GaSymMetricOrthogonal orthogonalMetric)
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
                    yield return new GaSymMultivectorBiTerm(
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

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForRcp(this GaSymMultivector mv1, GaSymMultivector mv2, GaSymMetricOrthogonal orthogonalMetric)
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
                    yield return new GaSymMultivectorBiTerm(
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

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForFdp(this GaSymMultivector mv1, GaSymMultivector mv2, GaSymMetricOrthogonal orthogonalMetric)
        {
            return GetBiTermsForGp(mv1, mv2, orthogonalMetric).Where(t => t.IsNonZeroFdp);
        }

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForHip(this GaSymMultivector mv1, GaSymMultivector mv2, GaSymMetricOrthogonal orthogonalMetric)
        {
            return GetBiTermsForGp(mv1, mv2, orthogonalMetric).Where(t => t.IsNonZeroHip);
        }

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForAcp(this GaSymMultivector mv1, GaSymMultivector mv2, GaSymMetricOrthogonal orthogonalMetric)
        {
            return GetBiTermsForGp(mv1, mv2, orthogonalMetric).Where(t => t.IsNonZeroAcp);
        }

        public static IEnumerable<GaSymMultivectorBiTerm> GetBiTermsForCp(this GaSymMultivector mv1, GaSymMultivector mv2, GaSymMetricOrthogonal orthogonalMetric)
        {
            return GetBiTermsForGp(mv1, mv2, orthogonalMetric).Where(t => t.IsNonZeroCp);
        }


        public static GaSymMultivector SetTerms(this GaSymMultivector mv, IEnumerable<KeyValuePair<int, Expr>> terms)
        {
            foreach (var term in terms)
                mv.SetTermCoef(term.Key, term.Value);

            return mv;
        }

        public static GaSymMultivector SetTerms(this GaSymMultivector mv, ISymbolicVector termsMv)
        {
            for (var id = 0; id < termsMv.Size; id++)
            {
                var coef = termsMv[id].Expression;

                if (!coef.IsNullOrZero())
                    mv.SetTermCoef(id, coef);
            }

            return mv;
        }


        public static IGaSymMultivectorTemp SetTerms(this IGaSymMultivectorTemp tempMv, IEnumerable<KeyValuePair<int, Expr>> terms)
        {
            foreach (var term in terms)
                tempMv.SetTermCoef(term.Key, term.Value);

            return tempMv;
        }

        public static IGaSymMultivectorTemp SetTerms(this IGaSymMultivectorTemp tempMv, GaSymMultivector termsMv)
        {
            foreach (var term in termsMv.NonZeroExprTerms)
                tempMv.SetTermCoef(term.Key, term.Value);

            return tempMv;
        }

        public static IGaSymMultivectorTemp SetTerms(this IGaSymMultivectorTemp tempMv, IGaSymMultivectorTemp termsMv)
        {
            foreach (var term in termsMv.NonZeroExprTerms)
                tempMv.SetTermCoef(term.Key, term.Value);

            return tempMv;
        }

        public static IGaSymMultivectorTemp SetTerms(this IGaSymMultivectorTemp tempMv, ISymbolicVector termsMv)
        {
            for (var id = 0; id < termsMv.Size; id++)
            {
                var coef = termsMv[id].Expression;

                if (!coef.IsNullOrZero())
                    tempMv.SetTermCoef(id, coef);
            }

            return tempMv;
        }


        public static IGaSymMultivectorTemp AddFactors(this IGaSymMultivectorTemp tempMv, Expr scalar, IEnumerable<KeyValuePair<int, Expr>> terms)
        {
            foreach (var term in terms)
                tempMv.AddFactor(term.Key, Mfs.Times[term.Value, scalar]);

            return tempMv;
        }

        public static IGaSymMultivectorTemp AddFactors(this IGaSymMultivectorTemp tempMv, Expr scalar, GaSymMultivector termsMv)
        {
            foreach (var term in termsMv.NonZeroExprTerms)
                tempMv.AddFactor(term.Key, Mfs.Times[term.Value, scalar]);

            return tempMv;
        }

        public static IGaSymMultivectorTemp AddFactors(this IGaSymMultivectorTemp tempMv, Expr scalar, IGaSymMultivectorTemp termsMv)
        {
            foreach (var term in termsMv.NonZeroExprTerms)
                tempMv.AddFactor(term.Key, Mfs.Times[term.Value, scalar]);

            return tempMv;
        }

        public static IGaSymMultivectorTemp AddFactors(this IGaSymMultivectorTemp tempMv, IEnumerable<GaSymMultivectorBiTerm> biTerms)
        {
            foreach (var biTerm in biTerms)
                tempMv.AddFactor(
                    biTerm.IdXor,
                    biTerm.IsNegativeEGp,
                    biTerm.TotalProduct
                );

            return tempMv;
        }

        public static IGaSymMultivectorTemp AddFactors(this IGaSymMultivectorTemp tempMv, IEnumerable<GaSymMultivectorBiTerm> biTerms, GaSymMetricOrthonormal orthonormalMetric)
        {
            foreach (var biTerm in biTerms)
                tempMv.AddFactor(
                    biTerm.IdXor,
                    biTerm.IsNegativeEGp,
                    Mfs.Times[
                        biTerm.Value1, 
                        biTerm.Value2, 
                        orthonormalMetric.GetExprSignature(biTerm.IdAnd)
                    ]
                );

            return tempMv;
        }

        public static IGaSymMultivectorTemp AddFactors(this IGaSymMultivectorTemp tempMv, IEnumerable<GaSymMultivectorBiTerm> biTerms, GaSymMetricOrthogonal orthogonalMetric)
        {
            foreach (var biTerm in biTerms)
                tempMv.AddFactor(
                    biTerm.IdXor,
                    biTerm.IsNegativeEGp,
                    Mfs.Times[biTerm.Value1, biTerm.Value2, orthogonalMetric[biTerm.IdAnd]]
                );

            return tempMv;
        }


        public static bool IsNullOrZero(this IGaSymMultivector mv)
        {
            return ReferenceEquals(mv, null) || mv.IsZero();
        }

        public static bool IsNullOrNearNumericZero(this GaSymMultivector mv, double epsilon)
        {
            return ReferenceEquals(mv, null) || mv.IsNearNumericZero(epsilon);
        }

        public static bool IsNullOrEqualZero(this IGaSymMultivector mv)
        {
            return ReferenceEquals(mv, null) || mv.IsEqualZero();
        }

        public static bool IsNullOrZero(this IReadOnlyList<Expr> coef)
        {
            return coef.Simplify().IsZero();
        }

        public static bool IsNullOrEqualZero(this IReadOnlyList<Expr> coef)
        {
            return coef.Simplify().IsEqualZero(SymbolicUtils.Cas);
        }

        public static Expr Simplify(this IReadOnlyList<Expr> coef)
        {
            if (ReferenceEquals(coef, null) || coef.Count == 0)
                return Expr.INT_ZERO;

            return SymbolicUtils.Cas[
                coef.Count == 1 
                    ? coef[0] 
                    : Mfs.Plus[coef.Cast<object>().ToArray()]
            ];
        }


        public static IGaSymMultivectorTemp ToTempMultivector(this IGaSymMultivector mv)
        {
            if (ReferenceEquals(mv, null))
                return null;

            var tempMv = GaSymMultivector.CreateZeroTemp(mv.GaSpaceDimension);

            foreach (var term in mv.NonZeroExprTerms)
                tempMv.SetTermCoef(term.Key, term.Value);

            return tempMv;
        }

        public static GaSymMultivectorHash ToHashMultivector(this IGaSymMultivector mv)
        {
            var sparseMv = GaSymMultivectorHash.CreateZero(mv.GaSpaceDimension);

            foreach (var term in mv.NonZeroTerms)
                sparseMv.Add(term.Key, term.Value);

            return sparseMv;
        }

        public static GaTreeMultivector ToTreeMultivector(this GaSymMultivector sparseMv)
        {
            var treeMv = GaTreeMultivector.CreateZero(sparseMv.GaSpaceDimension);

            foreach (var term in sparseMv.NonZeroExprTerms)
                treeMv[term.Key] = term.Value;

            return treeMv;
        }

        public static GaTreeMultivector ToTreeMultivector(this IGaSymMultivectorTemp sparseMv)
        {
            var treeMv = GaTreeMultivector.CreateZero(sparseMv.GaSpaceDimension);

            foreach (var term in sparseMv.NonZeroExprTerms)
                treeMv[term.Key] = term.Value;

            return treeMv;
        }

        public static GaTreeMultivector ToTreeMultivector(this GaSymMultivectorHash sparseMv)
        {
            var treeMv = GaTreeMultivector.CreateZero(sparseMv.GaSpaceDimension);

            foreach (var term in sparseMv.NonZeroExprTerms)
                treeMv[term.Key] = term.Value;

            return treeMv;
        }


        public static IGaSymMultivector Compactify(this IGaSymMultivector mv, bool returnZeroMvAsNull = false)
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
                    : GaSymMultivectorTerm.CreateZero(mv.GaSpaceDimension);

            //If it's a non-zero term return it as is
            var termMv = mv as GaSymMultivectorTerm;
            if (!ReferenceEquals(termMv, null))
                return termMv;

            //It's a full multivector
            var fullMv = (GaSymMultivector)mv;

            //If it's not a term return it after simplification
            if (!mv.IsTerm())
            {
                fullMv.Simplify();
                return fullMv;
            }

            //It's a full multivector containing a single term
            var term = mv.NonZeroExprTerms.FirstOrDefault();
            return GaSymMultivectorTerm.CreateTerm(mv.GaSpaceDimension, term.Key, term.Value);
        }

        public static GaSymMultivectorHashTable1D Compactify(this SparseTable1D<int, GaSymMultivector> mvTable)
        {
            var resultTable = new GaSymMultivectorHashTable1D();

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
