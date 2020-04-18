using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib;
using DataStructuresLib.Basic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Outermorphisms;
using GeometricAlgebraNumericsLib.Maps.Unilinear;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Multivectors.VectorKVectorOp;
using GeometricAlgebraNumericsLib.Structures.BinaryTraversal;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Outermorphisms
{
    public sealed class GaNumOutermorphism : GaNumMapUnilinear
    {
        public static GaNumOutermorphism Create(Matrix vectorsMappingMatrix)
        {
            return new GaNumOutermorphism(vectorsMappingMatrix);
        }


        private readonly IGaNumVector[] _basisVectorMappingsArray;


        public IReadOnlyList<IGaNumVector> BasisVectorMappingsList 
            => _basisVectorMappingsArray;

        public override int DomainVSpaceDimension
            => VectorsMappingMatrix.ColumnCount;

        public override int TargetVSpaceDimension
            => VectorsMappingMatrix.RowCount;

        public Matrix VectorsMappingMatrix { get; }

        public double Determinant 
            => _basisVectorMappingsArray.Op()[0];

        public override IGaNumMultivector this[int id1]
        {
            get
            {
                if (id1 == 0)
                    return GaNumTerm.CreateScalar(TargetGaSpaceDimension,1);

                if (id1.IsBasicPattern())
                    return _basisVectorMappingsArray[id1.BasisBladeIndex()];

                return _basisVectorMappingsArray.PickUsingPattern(id1).ToArray().Op();
            }
        }
        
        public override GaNumSarMultivector this[GaNumSarMultivector mv1]
        {
            get
            {
                //var sizeCounter = 0L;
                //var maxStackSizeCounter = 0;

                //var resultMv = 
                //    GaNumBtrMultivector.CreateZero(TargetGaSpaceDimension);

                //var nodesStack = 
                //    GaGbtBtrMultivectorOutermorphismNode.CreateFixedStack(_basisVectorsMaps, mv1);

                //GaNumVectorKVectorOpUtils.SetActiveVSpaceDimension(DomainVSpaceDimension);

                //while (nodesStack.Count > 0)
                //{
                //    //maxStackSizeCounter = Math.Max(maxStackSizeCounter, nodesStack.Count);

                //    var node = nodesStack.Pop();

                //    if (node.IsLeafNode())
                //    {
                //        var kVector = ((GaGbtBtrMultivectorOutermorphismNode)node).KVector;

                //        resultMv.AddFactors(node.Value, kVector);

                //        continue;
                //    }

                //    if (node.HasChild1())
                //        nodesStack.Push(
                //            node.GetValueChild1()
                //        );

                //    if (node.HasChild0())
                //        nodesStack.Push(
                //            node.GetValueChild0()
                //        );


                //    //var stackSize = opStack.SizeInBytes();
                //    //if (sizeCounter < stackSize) sizeCounter = stackSize;
                //}

                return GaGbtNumMultivectorOutermorphismStack
                    .Create(_basisVectorMappingsArray, mv1)
                    .TraverseForScaledKVectors()
                    .SumIntoSparseMultivectorFactory(TargetVSpaceDimension)
                    .GetSarMultivector();
            }
        }

        public Pair<GaNumSarMultivector> this[GaNumSarMultivector mv1, GaNumSarMultivector mv2]
        {
            get
            {
                //var sizeCounter = 0L;

                var resultMv1 =
                    new GaNumSarMultivectorFactory(TargetVSpaceDimension);

                var resultMv2 =
                    new GaNumSarMultivectorFactory(TargetVSpaceDimension);

                var nodesStack =
                    GaGbtBtrMultivectorOutermorphismPairNode.CreateStack(_basisVectorMappingsArray, mv1, mv2);

                GaNumVectorKVectorOpUtils.SetActiveVSpaceDimension(DomainVSpaceDimension);

                while (nodesStack.Count > 0)
                {
                    var node = nodesStack.Pop();

                    if (node.IsLeafNode())
                    {
                        var kVector = ((GaGbtBtrMultivectorOutermorphismPairNode)node).KVector;

                        if (node.Value.Item1 != 0)
                            resultMv1.AddTerms(node.Value.Item1, kVector);

                        if (node.Value.Item2 != 0)
                            resultMv1.AddTerms(node.Value.Item2, kVector);

                        continue;
                    }

                    if (node.HasChild1())
                        nodesStack.Push(
                            node.GetValueChild1()
                        );

                    if (node.HasChild0())
                        nodesStack.Push(
                            node.GetValueChild0()
                        );

                    //var stackSize = opStack.SizeInBytes();
                    //if (sizeCounter < stackSize) sizeCounter = stackSize;
                }

                //Console.WriteLine("Max Stack Size: " + sizeCounter.ToString("###,###,###,###,###,##0"));

                return new Pair<GaNumSarMultivector>(
                    resultMv1.GetSarMultivector(), 
                    resultMv2.GetSarMultivector()
                );
            }
        }

        /// <summary>
        /// Maps the given graded multivector to another graded multivector using this outermorphism
        /// </summary>
        /// <param name="mv1"></param>
        /// <returns></returns>
        public override GaNumDgrMultivector this[GaNumDgrMultivector mv1]
            => GaGbtNumMultivectorOutermorphismStack
                .Create(_basisVectorMappingsArray, mv1)
                .TraverseForScaledKVectors()
                .SumIntoDenseGradedMultivectorFactory(TargetVSpaceDimension)
                .GetDgrMultivector();


        private GaNumOutermorphism(Matrix vectorsMappingMatrix)
        {
            _basisVectorMappingsArray = new IGaNumVector[vectorsMappingMatrix.RowCount];

            VectorsMappingMatrix = vectorsMappingMatrix;
            
            for (var col = 0; col < vectorsMappingMatrix.ColumnCount; col++)
                _basisVectorMappingsArray[col] =
                    GaNumVector.CreateFromColumn(vectorsMappingMatrix, col);
        }

        
        public GaNumSarMultivector[] Map(params GaNumSarMultivector[] mvArray)
        {
            var mvCount = mvArray.Length;

            var mvNodesArray = new IGaBtrNode<double>[mvCount];
            for (var i = 0; i < mvCount; i++)
                mvNodesArray[i] = mvArray[i].BtrRootNode;
            
            var mvStack = new Stack<IGaBtrNode<double>[]>(DomainVSpaceDimension);
            mvStack.Push(mvNodesArray);

            var opStack = new Stack<GaNumDarKVector>(DomainVSpaceDimension);
            opStack.Push(
                GaNumDarKVector.CreateScalar(TargetGaSpaceDimension, 1)
            );

            var depthStack = new Stack<int>(DomainVSpaceDimension);
            depthStack.Push(DomainVSpaceDimension);

            var resultMvArray = new GaNumSarMultivectorFactory[mvCount];
            for (var i = 0; i < mvCount; i++) 
                resultMvArray[i] = new GaNumSarMultivectorFactory(TargetVSpaceDimension);

            while (mvStack.Count > 0)
            {
                mvNodesArray = mvStack.Pop();
                var opNode = opStack.Pop();
                var depth = depthStack.Pop();

                if (depth == 0)
                {
                    for (var i = 0; i < mvCount; i++)
                    {
                        if (!ReferenceEquals(mvNodesArray[i], null))
                            resultMvArray[i].AddTerms(mvNodesArray[i].Value, opNode);
                    }

                    continue;
                }

                var hasChild = false;
                var childNodesArray1 = new IGaBtrNode<double>[mvCount];
                for (var i = 0; i < mvCount; i++)
                {
                    var parentNode = mvNodesArray[i];
                    if (!ReferenceEquals(parentNode, null) && parentNode.HasChildNode1)
                    {
                        childNodesArray1[i] = parentNode.ChildNode1;
                        hasChild = true;
                    }
                }

                if (hasChild)
                {
                    mvStack.Push(childNodesArray1);
                    opStack.Push(_basisVectorMappingsArray[depth - 1].Op(opNode));
                    depthStack.Push(depth - 1);
                }

                hasChild = false;
                var childNodesArray0 = new IGaBtrNode<double>[mvCount];
                for (var i = 0; i < mvCount; i++)
                {
                    var parentNode = mvNodesArray[i];
                    if (!ReferenceEquals(parentNode, null) && parentNode.HasChildNode0)
                    {
                        childNodesArray0[i] = parentNode.ChildNode0;
                        hasChild = true;
                    }
                }

                if (hasChild)
                {
                    mvStack.Push(childNodesArray0);
                    opStack.Push(opNode);
                    depthStack.Push(depth - 1);
                }
            }

            return resultMvArray.Select(f => f.GetSarMultivector()).ToArray();
        }

        public GaNumDgrMultivector MapIntoDgrMultivector(IGaNumMultivector mv1)
        {
            return GaGbtNumMultivectorOutermorphismStack
                .Create(_basisVectorMappingsArray, mv1)
                .TraverseForScaledKVectors()
                .SumIntoDenseGradedMultivectorFactory(TargetVSpaceDimension)
                .GetDgrMultivector();
        }

        public override GaNumMapUnilinear Adjoint()
        {
            return new GaNumOutermorphism(
                VectorsMappingMatrix.Transpose() as Matrix
            );
        }

        public override GaNumMapUnilinear Inverse()
        {
            return new GaNumOutermorphism(
                VectorsMappingMatrix.Inverse() as Matrix
            );
        }

        public override GaNumMapUnilinear InverseAdjoint()
        {
            return new GaNumOutermorphism(
                VectorsMappingMatrix.Inverse().Transpose() as Matrix
            );
        }
        

        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
        {
            var mvStack = new Stack<Tuple<int, int>>();
            mvStack.Push(
                Tuple.Create(0, DomainVSpaceDimension)
            );

            var opStack = new Stack<GaNumDarKVector>();
            opStack.Push(
                GaNumDarKVector.CreateScalar(TargetGaSpaceDimension, 1)
            );

            while (mvStack.Count > 0)
            {
                var mvNode = mvStack.Pop();
                var opNode = opStack.Pop();

                if (mvNode.Item2 == 0)
                {
                    yield return Tuple.Create(
                        mvNode.Item1, 
                        (IGaNumMultivector) opNode
                    );

                    continue;
                }

                var childNodeTreeDepth = mvNode.Item2 - 1;
                var basisVectorMv = 
                    _basisVectorMappingsArray[mvNode.Item2 - 1];

                mvStack.Push(Tuple.Create(
                    mvNode.Item1 | (1 << childNodeTreeDepth), 
                    childNodeTreeDepth
                ));
                opStack.Push(basisVectorMv.Op(opNode));

                mvStack.Push(Tuple.Create(
                    mvNode.Item1, 
                    childNodeTreeDepth
                ));
                opStack.Push(opNode);
            }
        }

        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisVectorMaps()
        {
            return _basisVectorMappingsArray.Select(
                (mv, i) => Tuple.Create(i, (IGaNumMultivector) mv)
            );
        }
    }
}