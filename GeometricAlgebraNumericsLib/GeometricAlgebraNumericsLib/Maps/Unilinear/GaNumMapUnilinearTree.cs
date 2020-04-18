using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraNumericsLib.Exceptions;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees.NodeInfo;

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
        

        internal GaBtrInternalNode<IGaNumMultivector> BasisBladesMapTree { get; }


        public IEnumerable<IGaNumMultivector> Multivectors
            => BasisBladesMapTree.GetTreeLeafValues();

        public override int DomainVSpaceDimension { get; }

        public override int TargetVSpaceDimension { get; }

        public override IGaNumMultivector this[int id1]
        {
            get
            {
                BasisBladesMapTree.TryGetLeafValue(
                    DomainVSpaceDimension,
                    (ulong)id1, 
                    out var mv
                );

                return mv 
                       ?? GaNumTerm.CreateZero(TargetGaSpaceDimension);
            }
        }

        public override GaNumSarMultivector this[GaNumSarMultivector mv]
        {
            get
            {
                if (mv.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var tempMv = new GaNumSarMultivectorFactory(TargetVSpaceDimension);

                var nodeStack1 = BasisBladesMapTree.CreateNodesStack();
                var nodeStack2 = mv.BtrRootNode.CreateNodesStack();

                while (nodeStack1.Count > 0)
                {
                    var node1 = nodeStack1.Pop();
                    var node2 = nodeStack2.Pop();

                    if (node1.IsLeafNode)
                    {
                        tempMv.AddTerms(node2.Value, node1.Value);

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

                return tempMv.GetSarMultivector();
            }
        }

        public override GaNumDgrMultivector this[GaNumDgrMultivector mv]
        {
            get
            {
                if (mv.GaSpaceDimension != DomainGaSpaceDimension)
                    throw new GaNumericsException("Multivector size mismatch");

                var tempMv = new GaNumDgrMultivectorFactory(TargetVSpaceDimension);

                var nodeStack1 = BasisBladesMapTree.CreateNodesStack();
                var nodeStack2 = GaBinaryTreeGradedNodeInfo1<double>.CreateStack(mv.VSpaceDimension, (ulong)mv.GetStoredGradesBitPattern());

                while (nodeStack1.Count > 0)
                {
                    var node1 = nodeStack1.Pop();
                    var node2 = nodeStack2.Pop();

                    if (node1.IsLeafNode)
                    {
                        tempMv.AddTerms(
                            mv[(int)node2.Id], 
                            node1.Value
                        );

                        continue;
                    }

                    if (node1.HasChildNode0 && node2.HasChildNode0)
                    {
                        nodeStack1.Push(node1.ChildNode0);
                        nodeStack2.Push(node2.GetChildNodeInfo0());
                    }

                    if (node1.HasChildNode1 && node2.HasChildNode1)
                    {
                        nodeStack1.Push(node1.ChildNode1);
                        nodeStack2.Push(node2.GetChildNodeInfo1());
                    }
                }

                return tempMv.GetDgrMultivector();
            }
        }


        private GaNumMapUnilinearTree(int targetVSpaceDim)
        {
            DomainVSpaceDimension = targetVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;

            BasisBladesMapTree = new GaBtrInternalNode<IGaNumMultivector>();
        }

        private GaNumMapUnilinearTree(int domainVSpaceDim, int targetVSpaceDim)
        {
            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;

            BasisBladesMapTree = new GaBtrInternalNode<IGaNumMultivector>();
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

            BasisBladesMapTree.SetLeafValue(
                DomainVSpaceDimension, 
                (ulong)basisBladeId, 
                targetMv
            );

            return this;
        }

        
        public override IEnumerable<Tuple<int, IGaNumMultivector>> BasisBladeMaps()
        {
            return BasisBladesMapTree
                .GetNodeInfo(DomainVSpaceDimension, 0)
                .GetTreeLeafValuePairs()
                .Where(pair => !pair.Value.IsNullOrEmpty())
                .Select(pair => new Tuple<int, IGaNumMultivector>((int)pair.Key, pair.Value));
        }
    }
}
