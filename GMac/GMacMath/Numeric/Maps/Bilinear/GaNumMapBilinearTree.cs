using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Multivectors.Intermediate;
using GMac.GMacMath.Numeric.Products;
using GMac.GMacMath.Structures;

namespace GMac.GMacMath.Numeric.Maps.Bilinear
{
    public sealed class GaNumMapBilinearTree : GaNumMapBilinear
    {
        public static GaNumMapBilinearTree Create(int targetVSpaceDim)
        {
            return new GaNumMapBilinearTree(
                targetVSpaceDim
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

        public static GaNumMapBilinearTree CreateFromOuterProduct(IGMacFrame frame)
        {
            return new GaNumOp(frame.VSpaceDimension).ToTreeMap();
        }


        internal GMacQuadTree<IGaNumMultivector> BasisBladesMapTree { get; }


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
                IGaNumMultivector mv;
                BasisBladesMapTree.TryGetLeafValue((ulong)id1, (ulong)id2, out mv);

                return mv
                       ?? GaNumMultivectorTerm.CreateZero(TargetGaSpaceDimension);
            }
        }


        private GaNumMapBilinearTree(int targetVSpaceDim)
        {
            BasisBladesMapTree = new GMacQuadTree<IGaNumMultivector>(targetVSpaceDim);
            TargetVSpaceDimension = targetVSpaceDim;
        }

        private GaNumMapBilinearTree(int domainVSpaceDim, int targetVSpaceDim)
        {
            BasisBladesMapTree = new GMacQuadTree<IGaNumMultivector>(domainVSpaceDim);
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


        public override IGaNumMultivectorTemp MapToTemp(GaNumMultivector mv1, GaNumMultivector mv2)
        {
            if (mv1.GaSpaceDimension != DomainGaSpaceDimension || mv2.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GMacNumericException("Multivector size mismatch");

            var tempMv = GaNumMultivector.CreateZeroTemp(TargetGaSpaceDimension);

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

        public override IEnumerable<Tuple<int, int, IGaNumMultivector>> BasisBladesMaps()
        {
            return BasisBladesMapTree
                .LeafValuePairs
                .Where(pair => !pair.Item3.IsNullOrZero())
                .Select(pair => new Tuple<int, int, IGaNumMultivector>(
                    (int)pair.Item1,
                    (int)pair.Item2,
                    pair.Item3
                ));
        }
    }
}
