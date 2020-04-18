using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Structures.BinaryTraversal;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Products
{
    public static class GaNumTrilinearProductsUtils
    {
        /// <summary>
        /// Compute the sandwich product '(v gp a) gp v' scaled by scalingFactor using the given orthogonal metric's geometric product
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="v"></param>
        /// <param name="a"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        public static GaNumSarMultivector ComputeGpGpLa(this IGaNumMetricOrthogonal metric, GaNumSarMultivector v, GaNumSarMultivector a, double scalingFactor)
        {
            var resultMv = new GaNumSarMultivectorFactory(v.VSpaceDimension);

            var vTree = v.GetGbtRootNode();
            var aTree = a.GetGbtRootNode();

            var stack = GaGbtNode3.CreateStack(vTree, aTree, vTree);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                if (node.IsLeafNode())
                {
                    var term = metric.Gp((int)node.Id1, (int)node.Id2, (int)node.Id3);

                    resultMv.AddTerm(
                        term.BasisBladeId, 
                        term.ScalarValue * node.GetValuesProduct()
                    );

                    continue;
                }

                if (node.HasChild11())
                {
                    if (node.HasChild21())
                    {
                        if (node.HasChild31())
                            stack.Push(node.GetChild111());

                        if (node.HasChild30())
                            stack.Push(node.GetChild110());
                    }

                    if (node.HasChild20())
                    {
                        if (node.HasChild31())
                            stack.Push(node.GetChild101());

                        if (node.HasChild30())
                            stack.Push(node.GetChild100());
                    }
                }

                if (node.HasChild10())
                {
                    if (node.HasChild21())
                    {
                        if (node.HasChild31())
                            stack.Push(node.GetChild011());

                        if (node.HasChild30())
                            stack.Push(node.GetChild010());
                    }

                    if (node.HasChild20())
                    {
                        if (node.HasChild31())
                            stack.Push(node.GetChild001());

                        if (node.HasChild30())
                            stack.Push(node.GetChild000());
                    }
                }
            }

            if (scalingFactor != 1)
                resultMv.ApplyScaling(scalingFactor);

            return resultMv.GetSarMultivector();
        }

        /// <summary>
        /// Compute the triple product 'v * a * w' scaled by scalingFactor using the given associative product
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="v"></param>
        /// <param name="a"></param>
        /// <param name="w"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        public static GaNumSarMultivector ComputeGpGpLa(this IGaNumMetricOrthogonal metric, GaNumSarMultivector v, GaNumSarMultivector a, GaNumSarMultivector w, double scalingFactor)
        {
            var resultMv = new GaNumSarMultivectorFactory(v.VSpaceDimension);

            var vTree = v.GetGbtRootNode();
            var aTree = a.GetGbtRootNode();
            var wTree = w.GetGbtRootNode();

            var stack = GaGbtNode3.CreateStack(vTree, aTree, wTree);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                if (node.IsLeafNode())
                {
                    var term = metric.Gp((int)node.Id1, (int)node.Id2, (int)node.Id3);

                    resultMv.AddTerm(
                        term.BasisBladeId,
                        term.ScalarValue * node.GetValuesProduct()
                    );

                    continue;
                }

                if (node.HasChild11())
                {
                    if (node.HasChild21())
                    {
                        if (node.HasChild31())
                            stack.Push(node.GetChild111());

                        if (node.HasChild30())
                            stack.Push(node.GetChild110());
                    }

                    if (node.HasChild20())
                    {
                        if (node.HasChild31())
                            stack.Push(node.GetChild101());

                        if (node.HasChild30())
                            stack.Push(node.GetChild100());
                    }
                }

                if (node.HasChild10())
                {
                    if (node.HasChild21())
                    {
                        if (node.HasChild31())
                            stack.Push(node.GetChild011());

                        if (node.HasChild30())
                            stack.Push(node.GetChild010());
                    }

                    if (node.HasChild20())
                    {
                        if (node.HasChild31())
                            stack.Push(node.GetChild001());

                        if (node.HasChild30())
                            stack.Push(node.GetChild000());
                    }
                }
            }

            if (scalingFactor != 1)
                resultMv.ApplyScaling(scalingFactor);

            return resultMv.GetSarMultivector();
        }

        public static Matrix ComputeGpGpLaVectorMappingMatrix(this IGaNumMetricOrthogonal metric, GaNumSarMultivector v, double scalingFactor)
        {
            var array = new double[v.VSpaceDimension, v.VSpaceDimension];

            for (var basisVectorIndex = 0; basisVectorIndex < v.VSpaceDimension; basisVectorIndex++)
            {
                var vTree = v.GetGbtRootNode();
                var basisVectorTree = basisVectorIndex.GetBasisVectorGbtRootNode(v.VSpaceDimension);

                var stack = GaGbtNode3.CreateStack(vTree, basisVectorTree, vTree);

                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node.IsLeafNode())
                    {
                        var term = metric.Gp((int)node.Id1, (int)node.Id2, (int)node.Id3);

                        array[term.BasisBladeId.BasisBladeIndex(), basisVectorIndex] +=
                            term.ScalarValue * node.GetValuesProduct();

                        continue;
                    }

                    if (node.HasChild11())
                    {
                        if (node.HasChild21())
                        {
                            if (node.HasChild31())
                            {
                                if (node.ChildMayContainGrade1(node.ChildIdXorGrade111))
                                    stack.Push(node.GetChild111());
                            }

                            if (node.HasChild30())
                            {
                                if (node.ChildMayContainGrade1(node.ChildIdXorGrade110))
                                    stack.Push(node.GetChild110());
                            }
                        }

                        if (node.HasChild20())
                        {
                            if (node.HasChild31())
                            {
                                if (node.ChildMayContainGrade1(node.ChildIdXorGrade101))
                                    stack.Push(node.GetChild101());
                            }

                            if (node.HasChild30())
                            {
                                if (node.ChildMayContainGrade1(node.ChildIdXorGrade100))
                                    stack.Push(node.GetChild100());
                            }
                        }
                    }

                    if (node.HasChild10())
                    {
                        if (node.HasChild21())
                        {
                            if (node.HasChild31())
                            {
                                if (node.ChildMayContainGrade1(node.ChildIdXorGrade011))
                                    stack.Push(node.GetChild011());
                            }

                            if (node.HasChild30())
                            {
                                if (node.ChildMayContainGrade1(node.ChildIdXorGrade010))
                                    stack.Push(node.GetChild010());
                            }
                        }

                        if (node.HasChild20())
                        {
                            if (node.HasChild31())
                            {
                                if (node.ChildMayContainGrade1(node.ChildIdXorGrade001))
                                    stack.Push(node.GetChild001());
                            }

                            if (node.HasChild30())
                            {
                                if (node.ChildMayContainGrade1(node.ChildIdXorGrade000))
                                    stack.Push(node.GetChild000());
                            }
                        }
                    }
                }
            }

            return scalingFactor == 1
                ? DenseMatrix.OfArray(array)
                : DenseMatrix.OfArray(array) * scalingFactor;
        }


        /// <summary>
        /// Compute the sandwich product 'v a v' and scale the final multivector by 'scalingFactor'
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="v"></param>
        /// <param name="a"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        public static GaNumSarMultivector GpGpLa(this GaNumFrameEuclidean frame, GaNumSarMultivector v, GaNumSarMultivector a, double scalingFactor)
        {
            return ComputeGpGpLa(frame.EuclideanMetric, v, a, scalingFactor);
        }

        /// <summary>
        /// Compute the sandwich product 'v a v' and scale the final multivector by 'scalingFactor'
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="v"></param>
        /// <param name="a"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        public static GaNumSarMultivector GpGpLa(this GaNumFrameOrthonormal frame, GaNumSarMultivector v, GaNumSarMultivector a, double scalingFactor)
        {
            var gp = (IGaNumOrthogonalGeometricProduct)frame.Gp;

            return ComputeGpGpLa(frame.OrthonormalMetric, v, a, scalingFactor);
        }

        /// <summary>
        /// Compute the sandwich product 'v a v' and scale the final multivector by 'scalingFactor'
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="v"></param>
        /// <param name="a"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        public static GaNumSarMultivector GpGpLa(this GaNumFrameOrthogonal frame, GaNumSarMultivector v, GaNumSarMultivector a, double scalingFactor)
        {
            var gp = (IGaNumOrthogonalGeometricProduct)frame.Gp;

            return ComputeGpGpLa(frame.OrthogonalMetric, v, a, scalingFactor);
        }

        /// <summary>
        /// Compute the sandwich product 'v a v' and scale the final multivector by 'scalingFactor'
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="v"></param>
        /// <param name="a"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        public static GaNumSarMultivector GpGpLa(this GaNumFrameNonOrthogonal frame, GaNumSarMultivector v, GaNumSarMultivector a, double scalingFactor)
        {
            var v1 = frame.ThisToBaseFrameCba[v];
            var a1 = frame.ThisToBaseFrameCba[a];

            var mv1 = ComputeGpGpLa(frame.BaseOrthogonalMetric, v1, a1, scalingFactor);

            return frame.BaseFrameToThisCba[mv1];
        }


        /// <summary>
        /// Compute the Triple Geometric Product (TGP) 'v a w' and scale the final multivector by 'scalingFactor'
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="v"></param>
        /// <param name="a"></param>
        /// <param name="w"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        public static GaNumSarMultivector GpGp(this GaNumFrameEuclidean frame, GaNumSarMultivector v, GaNumSarMultivector a, GaNumSarMultivector w, double scalingFactor)
        {
            return ComputeGpGpLa(frame.EuclideanMetric, v, a, w, scalingFactor);
        }

        /// <summary>
        /// Compute the Triple Geometric Product (TGP) 'v a w' and scale the final multivector by 'scalingFactor'
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="v"></param>
        /// <param name="a"></param>
        /// <param name="w"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        public static GaNumSarMultivector GpGp(this GaNumFrameOrthonormal frame, GaNumSarMultivector v, GaNumSarMultivector a, GaNumSarMultivector w, double scalingFactor)
        {
            return ComputeGpGpLa(frame.OrthonormalMetric, v, a, w, scalingFactor);
        }

        /// <summary>
        /// Compute the Triple Geometric Product (TGP) 'v a w' and scale the final multivector by 'scalingFactor'
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="v"></param>
        /// <param name="a"></param>
        /// <param name="w"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        public static GaNumSarMultivector GpGp(this GaNumFrameOrthogonal frame, GaNumSarMultivector v, GaNumSarMultivector a, GaNumSarMultivector w, double scalingFactor)
        {
            return ComputeGpGpLa(frame.OrthogonalMetric, v, a, w, scalingFactor);
        }

        /// <summary>
        /// Compute the Triple Geometric Product (TGP) 'v a w' and scale the final multivector by 'scalingFactor'
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="v"></param>
        /// <param name="a"></param>
        /// <param name="w"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        public static GaNumSarMultivector GpGp(this GaNumFrameNonOrthogonal frame, GaNumSarMultivector v, GaNumSarMultivector a, GaNumSarMultivector w, double scalingFactor)
        {
            var v1 = frame.ThisToBaseFrameCba[v];
            var a1 = frame.ThisToBaseFrameCba[a];
            var w1 = frame.ThisToBaseFrameCba[w];

            var mv1 = ComputeGpGpLa(frame.BaseOrthogonalMetric, v1, a1, w1, scalingFactor);

            return frame.BaseFrameToThisCba[mv1];
        }


        /// <summary>
        /// Apply the sandwich product 'v a v' to all basis vectors 'a' to create a linear map using multivector 'v'.
        /// The linear map matrix is scaled by scalingFactor
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="v"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        public static Matrix GpGpToVectorMappingMatrix(this GaNumFrameEuclidean frame, GaNumSarMultivector v, double scalingFactor)
        {
            return ComputeGpGpLaVectorMappingMatrix(frame.EuclideanMetric, v, scalingFactor);
        }

        /// <summary>
        /// Apply the sandwich product 'v a v' to all basis vectors 'a' to create a linear map using multivector 'v'.
        /// The linear map matrix is scaled by scalingFactor
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="v"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        public static Matrix GpGpToVectorMappingMatrix(this GaNumFrameOrthonormal frame, GaNumSarMultivector v, double scalingFactor)
        {
            return ComputeGpGpLaVectorMappingMatrix(frame.OrthonormalMetric, v, scalingFactor);
        }

        /// <summary>
        /// Apply the sandwich product 'v a v' to all basis vectors 'a' to create a linear map using multivector 'v'.
        /// The linear map matrix is scaled by scalingFactor
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="v"></param>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        public static Matrix GpGpToVectorMappingMatrix(this GaNumFrameOrthogonal frame, GaNumSarMultivector v, double scalingFactor)
        {
            return ComputeGpGpLaVectorMappingMatrix(frame.OrthogonalMetric, v, scalingFactor);
        }
    }
}
