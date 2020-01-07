using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Maps.Unilinear;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraNumericsLib.Structures;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraNumericsLib.Maps.Outermorphisms
{
    public sealed class GaNumOutermorphism : GaNumMapUnilinear
    {
        public static GaNumOutermorphism Create(Matrix vectorsMappingMatrix)
        {
            return new GaNumOutermorphism(vectorsMappingMatrix);
        }


        private readonly GaNumKVector[] _basisVectorsMaps;


        public override int DomainVSpaceDimension
            => VectorsMappingMatrix.ColumnCount;

        public override int TargetVSpaceDimension
            => VectorsMappingMatrix.RowCount;

        public Matrix VectorsMappingMatrix { get; }

        public double Determinant 
            => _basisVectorsMaps.Op()[0];

        public override IGaNumMultivector this[int id1]
        {
            get
            {
                if (id1 == 0)
                    return GaNumMultivectorTerm.CreateScalar(TargetGaSpaceDimension,1);

                if (id1.IsBasicPattern())
                    return _basisVectorsMaps[id1.BasisBladeIndex()];

                return _basisVectorsMaps.PickUsingPattern(id1).ToArray().Op();
            }
        }
        
        public override GaNumMultivector this[GaNumMultivector mv1]
        {
            get
            {
                //var sizeCounter = 0L;

                var mvStack = mv1.TermsTree.CreateNodesStack();
                var opStack = new Stack<GaNumKVector>();
                opStack.Push(
                    GaNumKVector.CreateScalar(TargetGaSpaceDimension, 1)
                );

                var resultMv = GaNumMultivector.CreateZero(TargetGaSpaceDimension);

                while (mvStack.Count > 0)
                {
                    var mvNode = mvStack.Pop();
                    var opNode = opStack.Pop();

                    if (mvNode.IsLeafNode)
                    {
                        resultMv.AddFactors(mvNode.Value, opNode);

                        continue;
                    }

                    if (mvNode.HasChildNode1)
                    {
                        var basisVectorMv =
                            _basisVectorsMaps[mvNode.TreeDepth - 1];

                        mvStack.Push(mvNode.ChildNode1);
                        opStack.Push(basisVectorMv.VectorKVectorOp(opNode));
                    }

                    if (mvNode.HasChildNode0)
                    {
                        mvStack.Push(mvNode.ChildNode0);
                        opStack.Push(opNode);
                    }


                    //var stackSize = opStack.SizeInBytes();
                    //if (sizeCounter < stackSize) sizeCounter = stackSize;
                }

                //Console.WriteLine("Max Stack Size: " + sizeCounter.ToString("###,###,###,###,###,##0"));

                return resultMv;
            }
        }


        private GaNumOutermorphism(Matrix vectorsMappingMatrix)
        {
            _basisVectorsMaps = new GaNumKVector[vectorsMappingMatrix.RowCount];

            VectorsMappingMatrix = vectorsMappingMatrix;
            
            for (var col = 0; col < vectorsMappingMatrix.ColumnCount; col++)
                _basisVectorsMaps[col] =
                    GaNumKVector.CreateVectorFromColumn(vectorsMappingMatrix, col);
        }

        
        public GaNumMultivector[] Map(params GaNumMultivector[] mvArray)
        {
            var mvCount = mvArray.Length;

            var mvNodesArray = new IGaBinaryTreeNode<double>[mvCount];
            for (var i = 0; i < mvCount; i++)
                mvNodesArray[i] = mvArray[i].TermsTree;
            
            var mvStack = new Stack<IGaBinaryTreeNode<double>[]>(DomainVSpaceDimension);
            mvStack.Push(mvNodesArray);

            var opStack = new Stack<GaNumKVector>(DomainVSpaceDimension);
            opStack.Push(
                GaNumKVector.CreateScalar(TargetGaSpaceDimension, 1)
            );

            var depthStack = new Stack<int>(DomainVSpaceDimension);
            depthStack.Push(DomainVSpaceDimension);

            var resultMvArray = new GaNumMultivector[mvCount];
            for (var i = 0; i < mvCount; i++) 
                resultMvArray[i] = GaNumMultivector.CreateZero(TargetGaSpaceDimension);

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
                            resultMvArray[i].AddFactors(mvNodesArray[i].Value, opNode);
                    }

                    continue;
                }

                var hasChild = false;
                var childNodesArray1 = new IGaBinaryTreeNode<double>[mvCount];
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
                    opStack.Push(_basisVectorsMaps[depth - 1].VectorKVectorOp(opNode));
                    depthStack.Push(depth - 1);
                }

                hasChild = false;
                var childNodesArray0 = new IGaBinaryTreeNode<double>[mvCount];
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

            return resultMvArray;
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

            var opStack = new Stack<GaNumKVector>();
            opStack.Push(
                GaNumKVector.CreateScalar(TargetGaSpaceDimension, 1)
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
                    _basisVectorsMaps[mvNode.Item2 - 1];

                mvStack.Push(Tuple.Create(
                    mvNode.Item1 | (1 << childNodeTreeDepth), 
                    childNodeTreeDepth
                ));
                opStack.Push(basisVectorMv.VectorKVectorOp(opNode));

                mvStack.Push(Tuple.Create(
                    mvNode.Item1, 
                    childNodeTreeDepth
                ));
                opStack.Push(opNode);
            }
        }

        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisVectorMaps()
        {
            return _basisVectorsMaps.Select(
                (mv, i) => Tuple.Create(i, (IGaNumMultivector) mv)
            );
        }
    }
}