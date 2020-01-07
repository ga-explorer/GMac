using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Structures;

namespace GeometricAlgebraNumericsLib.Multivectors
{
    public static class GaNumMultivectorUtils
    {
        public static GaMultivectorMutableImplementation DefaultTempMultivectorKind { get; set; } 
            = GaMultivectorMutableImplementation.Array;

        public static bool DisableCompactifyMultivectors { get; set; } = false;


        ///// <summary>
        ///// Element-wise product of two multivectors. The result is a multivector with each coefficient
        ///// equal to the product of corresponding coefficients of the two multivectors. This is mainly
        ///// used for implementing efficient linear maps on multivectors.
        ///// </summary>
        ///// <param name="mv1"></param>
        ///// <param name="mv2"></param>
        ///// <returns></returns>
        //public static GaNumMultivector Ewp(this GaNumMultivector mv1, GaNumMultivector mv2)
        //{
        //    if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
        //        throw new GaNumericsException("Multivector size mismatch");

        //    var resultMv = GaNumMultivector.CreateZero(mv1.GaSpaceDimension);

        //    var mvStack1 = mv1.TermsTree.CreateNodesStack();
        //    var mvStack2 = mv2.TermsTree.CreateNodesStack();

        //    while (mvStack1.Count > 0)
        //    {
        //        var node1 = mvStack1.Pop();
        //        var node2 = mvStack2.Pop();
        //        var id = node1.Id;
                
        //        if (node1.IsLeafNode)
        //        {
        //            resultMv.AddTerm((int)id, node1.Value * node2.Value);
                    
        //            continue;
        //        }

        //        if (node1.HasChildNode0 && node2.HasChildNode0)
        //        {
        //            mvStack1.Push(node1.ChildNode0);
        //            mvStack2.Push(node2.ChildNode0);
        //        }

        //        if (node1.HasChildNode1 && node2.HasChildNode1)
        //        {
        //            mvStack1.Push(node1.ChildNode1);
        //            mvStack2.Push(node2.ChildNode1);
        //        }
        //    }

        //    return resultMv;
        //}

        /// <summary>
        /// This product treats the first multivector as a row and the second as a column and apply
        /// standard matrix multiplication to get a single scalar. This is mainly used for implementing
        /// efficient linear maps on multivectors.
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static double RowColumnProduct(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            var result = 0.0d;

            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                
                if (node1.IsLeafNode)
                {
                    result += node1.Value * node2.Value;

                    continue;
                }

                if (node1.HasChildNode0 && node2.HasChildNode0)
                {
                    mvStack1.Push(node1.ChildNode0);
                    mvStack2.Push(node2.ChildNode0);
                }

                if (node1.HasChildNode1 && node2.HasChildNode1)
                {
                    mvStack1.Push(node1.ChildNode1);
                    mvStack2.Push(node2.ChildNode1);
                }
            }

            return result;
        }


        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForEGp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            foreach (var term1 in mv1.Terms)
            foreach (var term2 in mv2.Terms)
                yield return new GaNumMultivectorBiTerm(
                    term1.Key,
                    term2.Key,
                    term1.Value,
                    term2.Value
                );
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForOp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = node1.Id;
                var id2 = node2.Id;

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
                    }

                    if (node2.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode1);
                    }
                }

                if (node1.HasChildNode1 && node2.HasChildNode0)
                {
                    mvStack1.Push(node1.ChildNode1);
                    mvStack2.Push(node2.ChildNode0);
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForESp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = node1.Id;
                var id2 = node2.Id;

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
                }

                if (node1.HasChildNode1 && node2.HasChildNode1)
                {
                    mvStack1.Push(node1.ChildNode1);
                    mvStack2.Push(node2.ChildNode1);
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForELcp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = node1.Id;
                var id2 = node2.Id;

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
                    }

                    if (node2.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode1);
                    }
                }

                if (node1.HasChildNode1 && node2.HasChildNode1)
                {
                    mvStack1.Push(node1.ChildNode1);
                    mvStack2.Push(node2.ChildNode1);
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForERcp(this GaNumMultivector mv1, GaNumMultivector mv2)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = node1.Id;
                var id2 = node2.Id;

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
                }

                if (node1.HasChildNode1)
                {
                    if (node2.HasChildNode0)
                    {
                        mvStack1.Push(node1.ChildNode1);
                        mvStack2.Push(node2.ChildNode0);
                    }

                    if (node2.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode1);
                        mvStack2.Push(node2.ChildNode1);
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

            var metricStack = orthogonalMetric.RootNode.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = node1.Id;
                var id2 = node2.Id;
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

                        //0 and 0 = 0
                        metricStack.Push(metricNode.ChildNode0);
                    }

                    if (node2.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode1);

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

                        //0 and 1 = 0
                        metricStack.Push(metricNode.ChildNode0);
                    }

                    if (node2.HasChildNode1 && metricNode.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode1);
                        mvStack2.Push(node2.ChildNode1);

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

            var metricStack = orthogonalMetric.RootNode.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = node1.Id;
                var id2 = node2.Id;
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
                }

                if (node1.HasChildNode1 && node2.HasChildNode1 && metricNode.HasChildNode1)
                {
                    mvStack1.Push(node1.ChildNode1);
                    mvStack2.Push(node2.ChildNode1);

                    //1 and 1 = 1
                    metricStack.Push(metricNode.ChildNode1);
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForLcp(this GaNumMultivector mv1, GaNumMultivector mv2, GaNumMetricOrthogonal orthogonalMetric)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            var metricStack = orthogonalMetric.RootNode.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = node1.Id;
                var id2 = node2.Id;
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

                        //0 and 0 = 0
                        metricStack.Push(metricNode.ChildNode0);
                    }

                    if (node2.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode0);
                        mvStack2.Push(node2.ChildNode1);

                        //0 and 1 = 0
                        metricStack.Push(metricNode.ChildNode0);
                    }
                }

                if (node1.HasChildNode1 && node2.HasChildNode1 && metricNode.HasChildNode1)
                {
                    mvStack1.Push(node1.ChildNode1);
                    mvStack2.Push(node2.ChildNode1);

                    //1 and 1 = 1
                    metricStack.Push(metricNode.ChildNode1);
                }
            }
        }

        public static IEnumerable<GaNumMultivectorBiTerm> GetBiTermsForRcp(this GaNumMultivector mv1, GaNumMultivector mv2, GaNumMetricOrthogonal orthogonalMetric)
        {
            var mvStack1 = mv1.TermsTree.CreateNodesStack();
            var mvStack2 = mv2.TermsTree.CreateNodesStack();

            var metricStack = orthogonalMetric.RootNode.CreateNodesStack();

            while (mvStack1.Count > 0)
            {
                var node1 = mvStack1.Pop();
                var node2 = mvStack2.Pop();
                var id1 = node1.Id;
                var id2 = node2.Id;
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
                }

                if (node1.HasChildNode1)
                {
                    if (node2.HasChildNode0 && metricNode.HasChildNode0)
                    {
                        mvStack1.Push(node1.ChildNode1);
                        mvStack2.Push(node2.ChildNode0);

                        //0 and 1 = 0
                        metricStack.Push(metricNode.ChildNode0);
                    }

                    if (node2.HasChildNode1 && metricNode.HasChildNode1)
                    {
                        mvStack1.Push(node1.ChildNode1);
                        mvStack2.Push(node2.ChildNode1);

                        //1 and 1 = 1
                        metricStack.Push(metricNode.ChildNode1);
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
                mv.SetTerm(term.Key, term.Value);

            return mv;
        }

        public static IGaNumMultivectorMutable SetTerms(this IGaNumMultivectorMutable tempMv, IEnumerable<KeyValuePair<int, double>> terms)
        {
            foreach (var term in terms)
                tempMv.SetTerm(term.Key, term.Value);

            return tempMv;
        }

        public static IGaNumMultivectorMutable SetTerms(this IGaNumMultivectorMutable tempMv, GaNumMultivector termsMv)
        {
            foreach (var term in termsMv.NonZeroTerms)
                tempMv.SetTerm(term.Key, term.Value);

            return tempMv;
        }

        public static IGaNumMultivectorMutable SetTerms(this IGaNumMultivectorMutable tempMv, IGaNumMultivectorMutable termsMv)
        {
            foreach (var term in termsMv.NonZeroTerms)
                tempMv.SetTerm(term.Key, term.Value);

            return tempMv;
        }


        public static GaNumMultivector AddTerms(this GaNumMultivector mv, IEnumerable<KeyValuePair<int, double>> terms)
        {
            foreach (var term in terms)
                mv.AddTerm(term.Key, term.Value);

            return mv;
        }


        public static GaNumMultivector AddFactors(this GaNumMultivector tempMv, double scalar, IEnumerable<KeyValuePair<int, double>> terms)
        {
            foreach (var term in terms)
                tempMv.UpdateTerm(term.Key, term.Value * scalar);

            return tempMv;
        }

        public static GaNumMultivector AddFactors(this GaNumMultivector tempMv, double scalar, GaNumMultivector termsMv)
        {
            foreach (var term in termsMv.NonZeroTerms)
                tempMv.UpdateTerm(term.Key, term.Value * scalar);

            return tempMv;
        }

        public static GaNumMultivector AddFactors(this GaNumMultivector tempMv, GaNumMultivector termsMv)
        {
            foreach (var term in termsMv.NonZeroTerms)
                tempMv.UpdateTerm(term.Key, term.Value);

            return tempMv;
        }

        public static GaNumMultivector AddFactors(this GaNumMultivector tempMv, double scalar, IGaNumMultivectorMutable termsMv)
        {
            foreach (var term in termsMv.NonZeroTerms)
                tempMv.UpdateTerm(term.Key, term.Value * scalar);

            return tempMv;
        }

        public static GaNumMultivector AddFactors(this GaNumMultivector tempMv, IEnumerable<GaNumMultivectorBiTerm> biTerms)
        {
            foreach (var biTerm in biTerms)
                tempMv.UpdateTerm(
                    biTerm.IdXor,
                    biTerm.IsNegativeEGp,
                    biTerm.TotalProduct
                );

            return tempMv;
        }

        public static GaNumMultivector AddFactors(this GaNumMultivector tempMv, IEnumerable<GaNumMultivectorBiTerm> biTerms, GaNumMetricOrthonormal orthonormalMetric)
        {
            foreach (var biTerm in biTerms)
                tempMv.UpdateTerm(
                    biTerm.IdXor,
                    biTerm.IsNegativeEGp,
                    biTerm.ValuesProduct * orthonormalMetric[biTerm.IdAnd]
                );

            return tempMv;
        }

        public static GaNumMultivector AddFactors(this GaNumMultivector tempMv, IEnumerable<GaNumMultivectorBiTerm> biTerms, GaNumMetricOrthogonal orthogonalMetric)
        {
            foreach (var biTerm in biTerms)
                tempMv.UpdateTerm(
                    biTerm.IdXor,
                    biTerm.IsNegativeEGp,
                    biTerm.TotalProduct * biTerm.Value2 * orthogonalMetric[biTerm.IdAnd]
                );

            return tempMv;
        }


        public static IGaNumMultivectorMutable AddFactors(this IGaNumMultivectorMutable tempMv, double scalar, IEnumerable<KeyValuePair<int, double>> terms)
        {
            foreach (var term in terms)
                tempMv.UpdateTerm(term.Key, term.Value * scalar);

            return tempMv;
        }

        public static IGaNumMultivectorMutable AddFactors(this IGaNumMultivectorMutable tempMv, double scalar, GaNumMultivector termsMv)
        {
            foreach (var term in termsMv.NonZeroTerms)
                tempMv.UpdateTerm(term.Key, term.Value * scalar);

            return tempMv;
        }

        public static IGaNumMultivectorMutable AddFactors(this IGaNumMultivectorMutable tempMv, double scalar, IGaNumMultivectorMutable termsMv)
        {
            foreach (var term in termsMv.NonZeroTerms)
                tempMv.UpdateTerm(term.Key, term.Value * scalar);

            return tempMv;
        }

        public static IGaNumMultivectorMutable AddFactors(this IGaNumMultivectorMutable tempMv, IEnumerable<GaNumMultivectorBiTerm> biTerms)
        {
            foreach (var biTerm in biTerms)
                tempMv.UpdateTerm(
                    biTerm.IdXor,
                    biTerm.IsNegativeEGp,
                    biTerm.TotalProduct
                );

            return tempMv;
        }

        public static IGaNumMultivectorMutable AddFactors(this IGaNumMultivectorMutable tempMv, IEnumerable<GaNumMultivectorBiTerm> biTerms, GaNumMetricOrthonormal orthonormalMetric)
        {
            foreach (var biTerm in biTerms)
                tempMv.UpdateTerm(
                    biTerm.IdXor,
                    biTerm.IsNegativeEGp,
                    biTerm.ValuesProduct * orthonormalMetric[biTerm.IdAnd]
                );

            return tempMv;
        }

        public static IGaNumMultivectorMutable AddFactors(this IGaNumMultivectorMutable tempMv, IEnumerable<GaNumMultivectorBiTerm> biTerms, GaNumMetricOrthogonal orthogonalMetric)
        {
            foreach (var biTerm in biTerms)
                tempMv.UpdateTerm(
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

        public static bool IsNullOrEmpty(this IGaNumMultivector mv)
        {
            return ReferenceEquals(mv, null) || mv.IsEmpty();
        }

        //public static IGaNumMultivectorTemp ToTempMultivector(this IGaNumMultivector mv)
        //{
        //    if (ReferenceEquals(mv, null))
        //        return null;

        //    var tempMv = GaNumMultivector.CreateZero(mv.GaSpaceDimension);

        //    foreach (var term in mv.NonZeroTerms)
        //        tempMv.SetTermCoef(term.Key, term.Value);

        //    return tempMv;
        //}

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
