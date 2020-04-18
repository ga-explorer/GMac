using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraNumericsLib.Structures;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees.NodeInfo;

namespace GeometricAlgebraNumericsLib.Maps.Bilinear
{
    public sealed class GaNumMapBilinearTree : GaNumMapBilinear
    {
        public static GaNumMapBilinearTree Create(int targetVSpaceDim, GaNumMapBilinearAssociativity associativity)
        {
            return new GaNumMapBilinearTree(
                targetVSpaceDim,
                associativity
            );
        }

        public static GaNumMapBilinearTree Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaNumMapBilinearTree(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }

        public static GaNumMapBilinearTree CreateFromOuterProduct(int vSpaceDimension)
        {
            return new GaNumOp(vSpaceDimension).ToTreeMap();
        }

        public static GaNumMapBilinearTree CreateFromOuterProduct(IGaFrame frame)
        {
            return new GaNumOp(frame.VSpaceDimension).ToTreeMap();
        }


        internal GaQuadTreeInternalNode<IGaNumMultivector> BasisBladesMapTree { get; }


        public IEnumerable<IGaNumMultivector> Multivectors
            => BasisBladesMapTree.LeafValues;

        public override int DomainVSpaceDimension
            => BasisBladesMapTree.TreeDepth;

        public override int TargetVSpaceDimension { get; }

        //public int FactorsCount
        //    => BasisBladesMapping
        //        .LeafValues
        //        .SelectMany(v => v.RootNode.LeafValues.Select(t => t.NonZeroTerms.Count()))
        //        .Sum();

        public override IGaNumMultivector this[int id1, int id2]
        {
            get
            {
                BasisBladesMapTree.TryGetLeafValue((ulong)id1, (ulong)id2, out var mv);

                return mv
                       ?? GaNumTerm.CreateZero(TargetGaSpaceDimension);
            }
        }

        public override GaNumSarMultivector this[GaNumSarMultivector mv1, GaNumSarMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var tempMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);

                var mapNodeStack = BasisBladesMapTree.CreateNodesStack();

                var mvNodeStack1 = mv1.BtrRootNode.CreateNodesStack();
                var mvNodeStack2 = mv2.BtrRootNode.CreateNodesStack();

                while (mapNodeStack.Count > 0)
                {
                    var mapNode = mapNodeStack.Pop();

                    var mvNode1 = mvNodeStack1.Pop();
                    var mvNode2 = mvNodeStack2.Pop();

                    if (mapNode.IsLeafNode)
                    {
                        tempMv.AddTerms(
                            mvNode1.Value * mvNode2.Value,
                            mapNode.Value
                        );

                        continue;
                    }

                    if (mvNode1.HasChildNode0)
                    {
                        if (mvNode2.HasChildNode0 && mapNode.HasChildNode00)
                        {
                            mapNodeStack.Push(mapNode.ChildNode00);

                            mvNodeStack1.Push(mvNode1.ChildNode0);
                            mvNodeStack2.Push(mvNode2.ChildNode0);
                        }

                        if (mvNode2.HasChildNode1 && mapNode.HasChildNode10)
                        {
                            mapNodeStack.Push(mapNode.ChildNode10);

                            mvNodeStack1.Push(mvNode1.ChildNode0);
                            mvNodeStack2.Push(mvNode2.ChildNode1);
                        }
                    }

                    if (mvNode1.HasChildNode1)
                    {
                        if (mvNode2.HasChildNode0 && mapNode.HasChildNode01)
                        {
                            mapNodeStack.Push(mapNode.ChildNode01);

                            mvNodeStack1.Push(mvNode1.ChildNode1);
                            mvNodeStack2.Push(mvNode2.ChildNode0);
                        }

                        if (mvNode2.HasChildNode1 && mapNode.HasChildNode11)
                        {
                            mapNodeStack.Push(mapNode.ChildNode11);

                            mvNodeStack1.Push(mvNode1.ChildNode1);
                            mvNodeStack2.Push(mvNode2.ChildNode1);
                        }
                    }
                }

                return tempMv.GetSarMultivector();
            }
        }

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv1, GaNumDgrMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var tempMv = new GaNumDgrMultivectorFactory(TargetVSpaceDimension);

                var mapNodeStack = BasisBladesMapTree.CreateNodesStack();

                var mvNodeInfoStack = GaBinaryTreeGradedNodeInfo2<double>.CreateStack(
                    DomainVSpaceDimension,  
                    (ulong)mv1.GetStoredGradesBitPattern(),
                    (ulong)mv2.GetStoredGradesBitPattern()
                );

                while (mapNodeStack.Count > 0)
                {
                    var mapNode = mapNodeStack.Pop();
                    var mvNodeInfo = mvNodeInfoStack.Pop();

                    if (mapNode.IsLeafNode)
                    {
                        var scalar = 
                            mv1[(int)mvNodeInfo.Id1] * 
                            mv2[(int)mvNodeInfo.Id2];

                        tempMv.AddTerms(scalar, mapNode.Value);

                        continue;
                    }

                    if (mvNodeInfo.HasChildNode10)
                    {
                        if (mvNodeInfo.HasChildNode20 && mapNode.HasChildNode00)
                        {
                            mapNodeStack.Push(mapNode.ChildNode00);

                            mvNodeInfoStack.Push(mvNodeInfo.GetChildNodeInfo00());
                        }

                        if (mvNodeInfo.HasChildNode21 && mapNode.HasChildNode10)
                        {
                            mapNodeStack.Push(mapNode.ChildNode10);

                            mvNodeInfoStack.Push(mvNodeInfo.GetChildNodeInfo01());
                        }
                    }

                    if (mvNodeInfo.HasChildNode11)
                    {
                        if (mvNodeInfo.HasChildNode20 && mapNode.HasChildNode01)
                        {
                            mapNodeStack.Push(mapNode.ChildNode01);

                            mvNodeInfoStack.Push(mvNodeInfo.GetChildNodeInfo10()); 
                        }

                        if (mvNodeInfo.HasChildNode21 && mapNode.HasChildNode11)
                        {
                            mapNodeStack.Push(mapNode.ChildNode11);

                            mvNodeInfoStack.Push(mvNodeInfo.GetChildNodeInfo11());
                        }
                    }
                }

                return tempMv.GetDgrMultivector();
            }
        }


        private GaNumMapBilinearTree(int targetVSpaceDim, GaNumMapBilinearAssociativity associativity)
            : base(associativity)
        {
            BasisBladesMapTree = new GaQuadTreeInternalNode<IGaNumMultivector>(
                0ul,
                0ul,
                targetVSpaceDim
            );

            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaNumMapBilinearTree(int domainVSpaceDim, int targetVSpaceDim)
            : base(GaNumMapBilinearAssociativity.NoneAssociative)
        {
            BasisBladesMapTree = new GaQuadTreeInternalNode<IGaNumMultivector>(
                0ul,
                0ul,
                domainVSpaceDim
            );

            TargetVSpaceDimension = targetVSpaceDim;
        }


        public GaNumMapBilinearTree ClearBasisBladesMaps()
        {
            BasisBladesMapTree.RemoveChildNodes();

            return this;
        }

        public GaNumMapBilinearTree SetBasisBladesMap(int id1, int id2, IGaNumMultivector targetMv)
        {
            Debug.Assert(ReferenceEquals(targetMv, null) || targetMv.VSpaceDimension == TargetVSpaceDimension);

            targetMv = targetMv.Compactify(true);

            if (ReferenceEquals(targetMv, null))
                return this;

            BasisBladesMapTree.SetLeafValue((ulong)id1, (ulong)id2, targetMv);

            return this;
        }


        public override IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps()
        {
            return BasisBladesMapTree
                .LeafValuePairs
                .Where(pair => !pair.Item3.IsNullOrEmpty())
                .Select(pair => new Tuple<int, int, IGaNumMultivector>(
                    (int)pair.Item1,
                    (int)pair.Item2,
                    pair.Item3
                ));
        }
    }
}
