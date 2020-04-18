using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Outermorphisms;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Products;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;

namespace GeometricAlgebraNumericsLib.Products
{
    public static class GaNumBilinearProductsUtils
    {
        public static IEnumerable<Tuple<double, GaNumDarKVector>> GetGbtOutermorphismScaledKVectors(this IGaNumMultivector mv, IReadOnlyList<IGaNumVector> basisVectorMappings)
        {
            return GaGbtNumMultivectorOutermorphismStack
                .Create(basisVectorMappings, mv)
                .TraverseForScaledKVectors();
        }

        public static IEnumerable<GaTerm<double>> GetGbtOutermorphismTerms(this IGaNumMultivector mv, IReadOnlyList<IGaNumVector> basisVectorMappings)
        {
            var factorsList = 
                mv.GetGbtOutermorphismScaledKVectors(basisVectorMappings);

            foreach (var (scalingFactor, kVector) in factorsList)
            {
                if (scalingFactor == 0.0d)
                    continue;

                foreach (var term in kVector.GetNonZeroTerms())
                    yield return term.GaScaleBy(scalingFactor);
            }
        }


        #region Euclidean bilinear products on multivectors using Guided Binary Traversal
        /// <summary>
        /// Compute the Outer Product terms of two multivectors using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtOpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForOpTerms();
        }

        /// <summary>
        /// Compute the Geometric Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtEGpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForEGpTerms();

            //var mvTree1 = mv1.GetGbtRootNode();
            //var mvTree2 = mv2.GetGbtRootNode();

            //var stack = GaGbtNode2.CreateStack(mvTree1, mvTree2);

            //while (stack.Count > 0)
            //{
            //    var node = stack.Pop();

            //    if (node.IsLeafNode())
            //    {
            //        yield return node.GetNumEGpTerm(mv1.GaSpaceDimension);

            //        continue;
            //    }

            //    if (node.HasChild11())
            //    {
            //        if (node.HasChild21())
            //            stack.Push(node.GetChild11());

            //        if (node.HasChild20())
            //            stack.Push(node.GetChild10());
            //    }

            //    if (node.HasChild10())
            //    {
            //        if (node.HasChild21())
            //            stack.Push(node.GetChild01());

            //        if (node.HasChild20())
            //            stack.Push(node.GetChild00());
            //    }
            //}
        }

        /// <summary>
        /// Compute the Left Contraction Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtELcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForELcpTerms();
        }

        /// <summary>
        /// Compute the Right Contraction Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtERcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForERcpTerms();
        }

        /// <summary>
        /// Compute the Scalar Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtESpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForESpTerms();
        }

        /// <summary>
        /// Compute the Fat-Dot Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtEFdpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForEFdpTerms();

            //var mvTree1 = mv1.GetGbtRootNode();
            //var mvTree2 = mv2.GetGbtRootNode();

            //var stack = GaGbtNode2.CreateStack(mvTree1, mvTree2);

            //while (stack.Count > 0)
            //{
            //    var node = stack.Pop();

            //    if (node.IsLeafNode())
            //    {
            //        if (node.IsNonZeroEFdp)
            //            yield return node.GetNumEGpTerm(mv1.GaSpaceDimension);

            //        continue;
            //    }

            //    if (node.HasChild11())
            //    {
            //        if (node.HasChild21())
            //            stack.Push(node.GetChild11());

            //        if (node.HasChild20())
            //            stack.Push(node.GetChild10());
            //    }

            //    if (node.HasChild10())
            //    {
            //        if (node.HasChild21())
            //            stack.Push(node.GetChild01());

            //        if (node.HasChild20())
            //            stack.Push(node.GetChild00());
            //    }
            //}
        }

        /// <summary>
        /// Compute the Hestenes Inner Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtEHipTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForEHipTerms();
        }

        /// <summary>
        /// Compute the Anti-Commutator Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtEAcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForEAcpTerms();
        }

        /// <summary>
        /// Compute the Commutator Product terms of two multivectors on the Euclidean metric using Guided Binary Traversal
        /// </summary>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtECpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForECpTerms();
        }

        public static IEnumerable<GaTerm<double>> GetGbtENorm2Terms(this IGaNumMultivector mv)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv, mv.GetReverse());

            return stack.TraverseForESpTerms();
        }

        public static double ENorm2(this IGaNumMultivector mv)
        {
            return mv.GetGbtENorm2Terms().Select(t => t.ScalarValue).Sum();
        }

        public static double ENorm(this IGaNumMultivector mv)
        {
            return Math.Sqrt(
                mv.GetGbtENorm2Terms().Select(t => t.ScalarValue).Sum()
            );
        }

        public static IEnumerable<GaTerm<double>> GetGptEInverseTerms(this IGaNumMultivector mv)
        {
            var mvReverse = mv.GetReverse();

            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv, mvReverse);

            var norm2 = stack
                .TraverseForESpTerms()
                .Select(t => t.ScalarValue)
                .Sum();

            return mvReverse.GetScaledTerms(1 / norm2);
        }
        #endregion


        #region Orthogonal metric bilinear products on multivectors using Guided Binary Traversal
        /// <summary>
        /// Compute the Geometric Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtGpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForGpTerms(metric);

            //var gaSpaceDimension = mv1.GaSpaceDimension;

            //var mvTree1 = mv1.GetGbtRootNode();
            //var mvTree2 = mv2.GetGbtRootNode();

            //var stack = GaGbtNode2.CreateStack(mvTree1, mvTree2);
            //var metricStack = new Stack<double>();
            //metricStack.Push(1);

            //var metricBasisVectorsSignaturesList = metric.BasisVectorsSignatures;

            //while (stack.Count > 0)
            //{
            //    var node = stack.Pop();
            //    var basisBladeSignature = metricStack.Pop();

            //    if (node.IsLeafNode())
            //    {
            //        yield return node.GetNumEGpTerm(gaSpaceDimension, basisBladeSignature);

            //        continue;
            //    }

            //    if (node.HasChild11())
            //    {
            //        if (node.HasChild21())
            //        {
            //            var basisVectorSignature = metricBasisVectorsSignaturesList[node.TreeDepth - 1];

            //            if (basisVectorSignature != 0)
            //            {
            //                stack.Push(node.GetChild11());
            //                metricStack.Push(basisBladeSignature * basisVectorSignature);
            //            }
            //        }

            //        if (node.HasChild20())
            //        {
            //            stack.Push(node.GetChild10());
            //            metricStack.Push(basisBladeSignature);
            //        }
            //    }

            //    if (node.HasChild10())
            //    {
            //        if (node.HasChild21())
            //        {
            //            stack.Push(node.GetChild01());
            //            metricStack.Push(basisBladeSignature);
            //        }

            //        if (node.HasChild20())
            //        {
            //            stack.Push(node.GetChild00());
            //            metricStack.Push(basisBladeSignature);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Compute the Left Contraction Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtLcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForLcpTerms(metric);
        }

        /// <summary>
        /// Compute the Right Contraction Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtRcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForRcpTerms(metric);
        }

        /// <summary>
        /// Compute the Scalar Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtSpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForSpTerms(metric);
        }

        /// <summary>
        /// Compute the Fat-Dot Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtFdpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForFdpTerms(metric);
        }

        /// <summary>
        /// Compute the Hestenes Inner Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtHipTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForHipTerms(metric);
        }

        /// <summary>
        /// Compute the Anti-Commutator Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtAcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForAcpTerms(metric);
        }

        /// <summary>
        /// Compute the Commutator Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtCpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv1, mv2);

            return stack.TraverseForCpTerms(metric);
        }

        public static IEnumerable<GaTerm<double>> GetGbtNorm2Terms(this IGaNumMultivector mv, IGaNumMetricOrthogonal metric)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv, mv.GetReverse());

            return stack.TraverseForSpTerms(metric);
        }

        public static double Norm2(this IGaNumMultivector mv, IGaNumMetricOrthogonal metric)
        {
            return mv.GetGbtNorm2Terms(metric).Select(t => t.ScalarValue).Sum();
        }

        public static double Norm(this IGaNumMultivector mv, IGaNumMetricOrthogonal metric)
        {
            return Math.Sqrt(
                mv.GetGbtNorm2Terms(metric).Select(t => t.ScalarValue).Sum()
            );
        }

        public static IEnumerable<GaTerm<double>> GetGptInverseTerms(this IGaNumMultivector mv, IGaNumMetricOrthogonal metric)
        {
            var mvReverse = mv.GetReverse();

            var stack = GaGbtNumMultivectorOrthogonalProductsStack2.Create(mv, mvReverse);

            var norm2 = stack
                .TraverseForSpTerms(metric)
                .Select(t => t.ScalarValue)
                .Sum();

            return mvReverse.GetScaledTerms(1 / norm2);
        }
        #endregion


        #region Non-Orthogonal metric bilinear products on multivectors using Guided Binary Traversal
        /// <summary>
        /// Compute the Geometric Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtGpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, GaNumMetricNonOrthogonal metric)
        {
            var orthoMv1 = 
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv1);

            var orthoMv2 = 
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv2);

            var orthoMv = 
                GetGbtGpTerms(orthoMv1, orthoMv2, metric.BaseMetric).SumAsDgrMultivector(mv1.VSpaceDimension);

            return metric.BaseToDerivedCba.MapIntoDgrMultivector(orthoMv).GetNonZeroTerms();
        }

        /// <summary>
        /// Compute the Left Contraction Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtLcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, GaNumMetricNonOrthogonal metric)
        {
            var orthoMv1 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv1);

            var orthoMv2 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv2);

            var orthoMv =
                GetGbtLcpTerms(orthoMv1, orthoMv2, metric.BaseMetric).SumAsDgrMultivector(mv1.VSpaceDimension);

            return metric.BaseToDerivedCba.MapIntoDgrMultivector(orthoMv).GetNonZeroTerms();
        }

        /// <summary>
        /// Compute the Right Contraction Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtRcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, GaNumMetricNonOrthogonal metric)
        {
            var orthoMv1 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv1);

            var orthoMv2 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv2);

            var orthoMv =
                GetGbtRcpTerms(orthoMv1, orthoMv2, metric.BaseMetric).SumAsDgrMultivector(mv1.VSpaceDimension);

            return metric.BaseToDerivedCba.MapIntoDgrMultivector(orthoMv).GetNonZeroTerms();
        }

        /// <summary>
        /// Compute the Scalar Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtSpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, GaNumMetricNonOrthogonal metric)
        {
            var orthoMv1 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv1);

            var orthoMv2 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv2);

            var orthoMv =
                GetGbtSpTerms(orthoMv1, orthoMv2, metric.BaseMetric).SumAsDgrMultivector(mv1.VSpaceDimension);

            return metric.BaseToDerivedCba.MapIntoDgrMultivector(orthoMv).GetNonZeroTerms();
        }

        /// <summary>
        /// Compute the Fat-Dot Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtFdpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, GaNumMetricNonOrthogonal metric)
        {
            var orthoMv1 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv1);

            var orthoMv2 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv2);

            var orthoMv =
                GetGbtFdpTerms(orthoMv1, orthoMv2, metric.BaseMetric).SumAsDgrMultivector(mv1.VSpaceDimension);

            return metric.BaseToDerivedCba.MapIntoDgrMultivector(orthoMv).GetNonZeroTerms();
        }

        /// <summary>
        /// Compute the Hestenes Inner Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtHipTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, GaNumMetricNonOrthogonal metric)
        {
            var orthoMv1 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv1);

            var orthoMv2 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv2);

            var orthoMv =
                GetGbtHipTerms(orthoMv1, orthoMv2, metric.BaseMetric).SumAsDgrMultivector(mv1.VSpaceDimension);

            return metric.BaseToDerivedCba.MapIntoDgrMultivector(orthoMv).GetNonZeroTerms();
        }

        /// <summary>
        /// Compute the Anti-Commutator Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtAcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, GaNumMetricNonOrthogonal metric)
        {
            var orthoMv1 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv1);

            var orthoMv2 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv2);

            var orthoMv =
                GetGbtAcpTerms(orthoMv1, orthoMv2, metric.BaseMetric).SumAsDgrMultivector(mv1.VSpaceDimension);

            return metric.BaseToDerivedCba.MapIntoDgrMultivector(orthoMv).GetNonZeroTerms();
        }

        /// <summary>
        /// Compute the Commutator Product terms of two multivectors on the given metric using Guided Binary Traversal
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="mv1"></param>
        /// <param name="mv2"></param>
        /// <returns></returns>
        public static IEnumerable<GaTerm<double>> GetGbtCpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, GaNumMetricNonOrthogonal metric)
        {
            var orthoMv1 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv1);

            var orthoMv2 =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv2);

            var orthoMv =
                GetGbtCpTerms(orthoMv1, orthoMv2, metric.BaseMetric).SumAsDgrMultivector(mv1.VSpaceDimension);

            return metric.BaseToDerivedCba.MapIntoDgrMultivector(orthoMv).GetNonZeroTerms();
        }

        public static IEnumerable<GaTerm<double>> GetGbtNorm2Terms(this IGaNumMultivector mv, GaNumMetricNonOrthogonal metric)
        {
            var orthoMv =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mv);

            return GetGbtNorm2Terms(orthoMv, metric.BaseMetric).SumAsDgrMultivector(mv.VSpaceDimension);
        }

        public static double Norm2(this IGaNumMultivector mv, GaNumMetricNonOrthogonal metric)
        {
            return mv.GetGbtNorm2Terms(metric).Select(t => t.ScalarValue).Sum();
        }

        public static double Norm(this IGaNumMultivector mv, GaNumMetricNonOrthogonal metric)
        {
            return Math.Sqrt(
                mv.GetGbtNorm2Terms(metric).Select(t => t.ScalarValue).Sum()
            );
        }

        public static IEnumerable<GaTerm<double>> GetGbtInverseTerms(this IGaNumMultivector mv, GaNumMetricNonOrthogonal metric)
        {
            var mvReverse = mv.GetReverse();

            var orthoMv =
                metric.DerivedToBaseCba.MapIntoDgrMultivector(mvReverse);

            var norm2 = 
                GetGbtNorm2Terms(orthoMv, metric.BaseMetric)
                    .Select(t => t.ScalarValue)
                    .Sum();

            return mvReverse.GetScaledTerms(1 / norm2);
        }
        #endregion


        public static IEnumerable<GaTerm<double>> GetGbtGpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetric metric)
        {
            if (metric is GaNumMetricNonOrthogonal nonOrthogonalMetric)
                return mv1.GetGbtGpTerms(mv2, nonOrthogonalMetric);

            var orthogonalMetric = (IGaNumMetricOrthogonal)metric;
            return mv1.GetGbtGpTerms(mv2, orthogonalMetric);
        }

        public static double GetGbtNorm2(this IGaNumMultivector mv, IGaNumMetric metric)
        {
            if (metric is GaNumMetricNonOrthogonal nonOrthogonalMetric)
                return mv.GetGbtNorm2Terms(nonOrthogonalMetric).Select(t => t.ScalarValue).Sum();

            var orthogonalMetric = (IGaNumMetricOrthogonal)metric;
            return mv.GetGbtNorm2Terms(orthogonalMetric).Select(t => t.ScalarValue).Sum();
        }


        #region Euclidean bilinear products on multivectors using Simple Looping
        public static IEnumerable<GaTerm<double>> GetLoopOpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroOp(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    var value =
                        term1.ScalarValue * term2.ScalarValue *
                        (GaNumFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

                    yield return new GaTerm<double>(term1.BasisBladeId ^ term2.BasisBladeId, value);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopEGpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    var value =
                        term1.ScalarValue * term2.ScalarValue *
                        (GaNumFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

                    yield return new GaTerm<double>(term1.BasisBladeId ^ term2.BasisBladeId, value);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopELcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroELcp(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    var value =
                        term1.ScalarValue * term2.ScalarValue *
                        (GaNumFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

                    yield return new GaTerm<double>(term1.BasisBladeId ^ term2.BasisBladeId, value);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopERcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroERcp(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    var value =
                        term1.ScalarValue * term2.ScalarValue *
                        (GaNumFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

                    yield return new GaTerm<double>(term1.BasisBladeId ^ term2.BasisBladeId, value);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopESpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroESp(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    var value =
                        term1.ScalarValue * term2.ScalarValue *
                        (GaNumFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

                    yield return new GaTerm<double>(term1.BasisBladeId ^ term2.BasisBladeId, value);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopEFdpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroEFdp(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    var value =
                        term1.ScalarValue * term2.ScalarValue *
                        (GaNumFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

                    yield return new GaTerm<double>(term1.BasisBladeId ^ term2.BasisBladeId, value);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopEHipTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroEHip(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    var value =
                        term1.ScalarValue * term2.ScalarValue *
                        (GaNumFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

                    yield return new GaTerm<double>(term1.BasisBladeId ^ term2.BasisBladeId, value);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopEAcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroEAcp(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    var value =
                        term1.ScalarValue * term2.ScalarValue *
                        (GaNumFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

                    yield return new GaTerm<double>(term1.BasisBladeId ^ term2.BasisBladeId, value);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopECpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroECp(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    var value =
                        term1.ScalarValue * term2.ScalarValue *
                        (GaNumFrameUtils.IsNegativeEGp(term1.BasisBladeId, term2.BasisBladeId) ? -1.0d : 1.0d);

                    yield return new GaTerm<double>(term1.BasisBladeId ^ term2.BasisBladeId, value);
                }
            }
        }
        #endregion


        #region Orthogonal metric bilinear products on multivectors using Simple Looping
        public static IEnumerable<GaTerm<double>> GetLoopGpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.ScalarValue * term2.ScalarValue); 
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopLcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroELcp(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.ScalarValue * term2.ScalarValue);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopRcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroERcp(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.ScalarValue * term2.ScalarValue);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopSpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroESp(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.ScalarValue * term2.ScalarValue);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopFdpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroEFdp(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.ScalarValue * term2.ScalarValue);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopHipTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroEHip(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.ScalarValue * term2.ScalarValue);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopAcpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroEAcp(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.ScalarValue * term2.ScalarValue);
                }
            }
        }

        public static IEnumerable<GaTerm<double>> GetLoopCpTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            foreach (var term1 in mv1.GetNonZeroTerms())
            {
                foreach (var term2 in mv2.GetNonZeroTerms())
                {
                    if (!GaNumFrameUtils.IsNonZeroECp(term1.BasisBladeId, term2.BasisBladeId))
                        continue;

                    yield return metric.ScaledGp(term1.BasisBladeId, term2.BasisBladeId, term1.ScalarValue * term2.ScalarValue);
                }
            }
        }
        #endregion


        #region Euclidean bilinear products on Sparse Array Represntation Multivectors
        public static GaNumSarMultivector Op(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtOpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector EGp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtEGpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector ESp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtESpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector ELcp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtELcpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector ERcp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtERcpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector EFdp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtEFdpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector EHip(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtEHipTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector EAcp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtEAcpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector ECp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtECpTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector EInverse(this GaNumSarMultivector mv)
        {
            return mv.GetGptEInverseTerms().SumAsSarMultivector(mv.VSpaceDimension);
        }
        #endregion


        #region Euclidean bilinear products on Dense Graded Represntation Multivectors
        public static GaNumDgrMultivector Op(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtOpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDgrMultivector EGp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtEGpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDgrMultivector ESp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtESpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDgrMultivector ELcp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtELcpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDgrMultivector ERcp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtERcpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDgrMultivector EFdp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtEFdpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDgrMultivector EHip(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtEHipTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        }
        
        public static GaNumDgrMultivector EAcp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtEAcpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDgrMultivector ECp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtECpTerms(mv2).SumAsDgrMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDgrMultivector EInverse(this GaNumDgrMultivector mv)
        {
            return mv.GetGptEInverseTerms().SumAsDgrMultivector(mv.VSpaceDimension);
        }
        #endregion


        #region Euclidean bilinear products on Dense Array Represntation Multivectors
        public static GaNumDarMultivector Op(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtOpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector EGp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtEGpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector ESp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtESpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector ELcp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtELcpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector ERcp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtERcpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector EFdp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtEFdpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector EHip(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtEHipTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector EAcp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtEAcpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector ECp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtECpTerms(mv2).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector EInverse(this GaNumDarMultivector mv)
        {
            return mv.GetGptEInverseTerms().SumAsDarMultivector(mv.VSpaceDimension);
        }
        #endregion


        #region Orthogonal metric bilinear products on Sparse Array Represntation Multivectors
        public static GaNumSarMultivector Gp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtGpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector Sp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtSpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector Lcp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtLcpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector Rcp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtRcpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector Fdp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtFdpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector Hip(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtHipTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector Acp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtAcpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector Cp(this GaNumSarMultivector mv1, GaNumSarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtCpTerms(mv2, metric).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector Inverse(this GaNumSarMultivector mv, IGaNumMetricOrthogonal metric)
        {
            return mv.GetGptInverseTerms(metric).SumAsSarMultivector(mv.VSpaceDimension);
        }
        #endregion


        #region Orthogonal metric bilinear products on Dense Graded Represntation Multivectors
        public static GaNumDgrMultivector Gp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.VSpaceDimension != mv2.VSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

            factory.AddGbtGpTerms(mv1, mv2, metric);

            return factory.GetDgrMultivector();
        }

        public static GaNumDgrMultivector Sp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.VSpaceDimension != mv2.VSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

            factory.AddGbtSpTerms(mv1, mv2, metric);

            return factory.GetDgrMultivector();
        }

        public static GaNumDgrMultivector Lcp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.VSpaceDimension != mv2.VSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

            factory.AddGbtLcpTerms(mv1, mv2, metric);

            return factory.GetDgrMultivector();
        }

        public static GaNumDgrMultivector Rcp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.VSpaceDimension != mv2.VSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

            factory.AddGbtRcpTerms(mv1, mv2, metric);

            return factory.GetDgrMultivector();
        }

        public static GaNumDgrMultivector Fdp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.VSpaceDimension != mv2.VSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

            factory.AddGbtFdpTerms(mv1, mv2, metric);

            return factory.GetDgrMultivector();
        }

        public static GaNumDgrMultivector Hip(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.VSpaceDimension != mv2.VSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

            factory.AddGbtHipTerms(mv1, mv2, metric);

            return factory.GetDgrMultivector();
        }

        public static GaNumDgrMultivector Acp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.VSpaceDimension != mv2.VSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

            factory.AddGbtAcpTerms(mv1, mv2, metric);

            return factory.GetDgrMultivector();
        }

        public static GaNumDgrMultivector Cp(this GaNumDgrMultivector mv1, GaNumDgrMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.VSpaceDimension != mv2.VSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            var factory = new GaNumDgrMultivectorFactory(mv1.VSpaceDimension);

            factory.AddGbtCpTerms(mv1, mv2, metric);

            return factory.GetDgrMultivector();
        }

        public static GaNumDgrMultivector Inverse(this GaNumDgrMultivector mv, IGaNumMetricOrthogonal metric)
        {
            return mv.GetGptInverseTerms(metric).SumAsDgrMultivector(mv.VSpaceDimension);
        }
        #endregion


        #region Orthogonal metric bilinear products on Dense Array Represntation Multivectors
        public static GaNumDarMultivector Gp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtGpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector Sp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtSpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector Lcp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtLcpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector Rcp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtRcpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector Fdp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtFdpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector Hip(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtHipTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector Acp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtAcpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector Cp(this GaNumDarMultivector mv1, GaNumDarMultivector mv2, IGaNumMetricOrthogonal metric)
        {
            if (mv1.GaSpaceDimension != mv2.GaSpaceDimension)
                throw new GaNumericsException("Multivector size mismatch");

            return mv1.GetGbtCpTerms(mv2, metric).SumAsDarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumDarMultivector Inverse(this GaNumDarMultivector mv, IGaNumMetricOrthogonal metric)
        {
            return mv.GetGptInverseTerms(metric).SumAsDarMultivector(mv.VSpaceDimension);
        }
        #endregion
    }
}
