using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Structures;

namespace GeometricAlgebraNumericsLib.Maps.Unilinear
{
    public sealed class GaNumMapUnilinearTree : GaNumMapUnilinear
    {
        public static GaNumMapUnilinearTree Create(int targetVSpaceDim)
        {
            return new GaNumMapUnilinearTree(
                targetVSpaceDim
            );
        }

        public static GaNumMapUnilinearTree Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaNumMapUnilinearTree(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }
        

        internal GaBinaryTreeInternalNode<IGaNumMultivector> BasisBladesMapTree { get; }


        public IEnumerable<IGaNumMultivector> Multivectors
            => BasisBladesMapTree.LeafValues;

        public override int DomainVSpaceDimension { get; }

        public override int TargetVSpaceDimension
            => BasisBladesMapTree.TreeDepth;

        public override IGaNumMultivector this[int id1]
        {
            get
            {
                BasisBladesMapTree.TryGetLeafValue((ulong)id1, out var mv);

                return mv 
                       ?? GaNumMultivectorTerm.CreateZero(TargetGaSpaceDimension);
            }
        }

        public override GaNumMultivector this[GaNumMultivector mv]
        {
            get
            {
                if (mv.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var tempMv = GaNumMultivector.CreateZero(TargetGaSpaceDimension);

                var nodeStack1 = BasisBladesMapTree.CreateNodesStack();
                var nodeStack2 = mv.TermsTree.CreateNodesStack();

                while (nodeStack1.Count > 0)
                {
                    var node1 = nodeStack1.Pop();
                    var node2 = nodeStack2.Pop();

                    if (node1.IsLeafNode)
                    {
                        tempMv.AddFactors(node2.Value, node1.Value);

                        continue;
                    }

                    if (node1.HasChildNode0 && node2.HasChildNode0)
                    {
                        nodeStack1.Push(node1.ChildNode0);
                        nodeStack2.Push(node2.ChildNode0);
                    }

                    if (node1.HasChildNode1 && node2.HasChildNode1)
                    {
                        nodeStack1.Push(node1.ChildNode1);
                        nodeStack2.Push(node2.ChildNode1);
                    }
                }

                return tempMv;
            }
        }


        private GaNumMapUnilinearTree(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;

            BasisBladesMapTree = new GaBinaryTreeInternalNode<IGaNumMultivector>(
                0,
                targetVSpaceDim
            );
        }

        private GaNumMapUnilinearTree(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;

            BasisBladesMapTree = new GaBinaryTreeInternalNode<IGaNumMultivector>(
                0, 
                targetVSpaceDim
            );
        }


        public GaNumMapUnilinearTree ClearBasisBladeMaps()
        {
            BasisBladesMapTree.RemoveChildNodes();

            return this;
        }

        public GaNumMapUnilinearTree SetBasisBladeMap(int basisBladeId, IGaNumMultivector targetMv)
        {
            Debug.Assert(ReferenceEquals(targetMv, null) || targetMv.VSpaceDimension == TargetVSpaceDimension);

            targetMv = targetMv.Compactify(true);

            if (ReferenceEquals(targetMv, null))
                return this;

            BasisBladesMapTree.SetLeafValue((ulong)basisBladeId, targetMv);

            return this;
        }

        
        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
        {
            return BasisBladesMapTree
                .LeafValuePairs
                .Where(pair => !pair.Value.IsNullOrEmpty())
                .Select(pair => new Tuple<int, IGaNumMultivector>((int)pair.Key, pair.Value));
        }
    }
}
