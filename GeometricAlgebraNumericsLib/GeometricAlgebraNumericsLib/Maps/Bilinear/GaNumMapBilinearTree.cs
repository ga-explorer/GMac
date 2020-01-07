using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraNumericsLib.Structures;

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
                       ?? GaNumMultivectorTerm.CreateZero(TargetGaSpaceDimension);
            }
        }

        public override GaNumMultivector this[GaNumMultivector mv1, GaNumMultivector mv2]
        {
            get
            {
                if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var tempMv = GaNumMultivector.CreateZero(TargetGaSpaceDimension);

                var mapNodeStack = BasisBladesMapTree.CreateNodesStack();

                var mvNodeStack1 = mv1.TermsTree.CreateNodesStack();
                var mvNodeStack2 = mv2.TermsTree.CreateNodesStack();

                while (mapNodeStack.Count > 0)
                {
                    var mapNode = mapNodeStack.Pop();

                    var mvNode1 = mvNodeStack1.Pop();
                    var mvNode2 = mvNodeStack2.Pop();

                    if (mapNode.IsLeafNode)
                    {
                        tempMv.AddFactors(
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

                return tempMv;
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
