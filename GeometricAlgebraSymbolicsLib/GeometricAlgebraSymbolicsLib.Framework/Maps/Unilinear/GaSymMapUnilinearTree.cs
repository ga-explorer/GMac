using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraSymbolicsLib.Exceptions;
using GeometricAlgebraSymbolicsLib.Multivectors;
using GeometricAlgebraSymbolicsLib.Multivectors.Intermediate;

namespace GeometricAlgebraSymbolicsLib.Maps.Unilinear
{
    public sealed class GaSymMapUnilinearTree : GaSymMapUnilinear
    {
        public static GaSymMapUnilinearTree Create(int targetVSpaceDim)
        {
            return new GaSymMapUnilinearTree(
                targetVSpaceDim
            );
        }

        public static GaSymMapUnilinearTree Create(int domainVSpaceDim, int targetVSpaceDim)
        {
            return new GaSymMapUnilinearTree(
                domainVSpaceDim,
                targetVSpaceDim
            );
        }


        private readonly GaBtrInternalNode<IGaSymMultivector> _basisBladeMaps;


        public override int DomainVSpaceDimension { get; }

        public override int TargetVSpaceDimension { get; }

        public override IGaSymMultivector this[ulong id1]
        {
            get
            {
                _basisBladeMaps.TryGetLeafValue(DomainVSpaceDimension, id1, out var mv);

                return mv
                       ?? GaSymMultivectorTerm.CreateZero(TargetVSpaceDimension);
            }
        }


        private GaSymMapUnilinearTree(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;

            _basisBladeMaps = new GaBtrInternalNode<IGaSymMultivector>();
        }

        private GaSymMapUnilinearTree(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;

            _basisBladeMaps = new GaBtrInternalNode<IGaSymMultivector>();
        }


        public GaSymMapUnilinearTree ClearBasisBladeMaps()
        {
            _basisBladeMaps.RemoveChildNodes();

            return this;
        }

        public GaSymMapUnilinearTree SetBasisBladeMap(ulong basisBladeId, IGaSymMultivector targetMv)
        {
            Debug.Assert(ReferenceEquals(targetMv, null) || targetMv.VSpaceDimension == TargetVSpaceDimension);

            targetMv = targetMv.Compactify(true);

            if (ReferenceEquals(targetMv, null))
                return this;

            _basisBladeMaps.SetLeafValue(DomainVSpaceDimension, basisBladeId, targetMv);

            return this;
        }


        public override IGaSymMultivectorTemp MapToTemp(ulong id1)
        {
            return _basisBladeMaps.GetLeafValue(DomainVSpaceDimension, id1)?.ToTempMultivector()
                ?? GaSymMultivector.CreateZeroTemp(TargetVSpaceDimension);
        }

        public override IGaSymMultivectorTemp MapToTemp(GaSymMultivector mv)
        {
            if (mv.GaSpaceDimension != DomainGaSpaceDimension)
                throw new GaSymbolicsException("Multivector size mismatch");

            var resultMv = GaSymMultivector.CreateZeroTemp(TargetVSpaceDimension);

            var nodeStack1 = _basisBladeMaps.CreateNodesStack();
            var nodeStack2 = mv.TermsTree.CreateNodesStack();

            while (nodeStack1.Count > 0)
            {
                var node1 = nodeStack1.Pop();
                var node2 = nodeStack2.Pop();

                if (node1.IsLeafNode)
                {
                    resultMv.AddFactors(node2.Value, node1.Value);

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

            return resultMv;
        }

        public override IEnumerable<Tuple<ulong, IGaSymMultivector>> BasisBladeMaps()
        {
            return _basisBladeMaps
                .GetNodeInfo(DomainVSpaceDimension, 0)
                .GetTreeNodesInfo()
                .Where(nodeInfo => !nodeInfo.Value.IsNullOrZero())
                .Select(nodeInfo => new Tuple<ulong, IGaSymMultivector>(nodeInfo.Id, nodeInfo.Value));
        }
    }
}
