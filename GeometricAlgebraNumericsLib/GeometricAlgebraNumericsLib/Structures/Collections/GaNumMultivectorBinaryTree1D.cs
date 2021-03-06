﻿using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;
using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraNumericsLib.Structures.Collections
{
    public sealed class GaNumMultivectorBinaryTree1D
    {
        internal GaBtrInternalNode<IGaNumMultivector> RootNode { get; }

        public int DomainVSpaceDimension { get; }

        public ulong DomainGaSpaceDimension
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public int TargetVSpaceDimension { get; }

        public ulong TargetGaSpaceDimension
            => TargetVSpaceDimension.ToGaSpaceDimension();

        public IGaNumMultivector this[int id1]
        {
            get
            {
                RootNode.TryGetLeafValue(
                    DomainVSpaceDimension, 
                    (ulong)id1, 
                    out var mv
                );

                return mv;
            }
            set =>
                RootNode.SetLeafValue(
                    DomainVSpaceDimension,
                    (ulong)id1,
                    value ?? GaNumSarMultivector.CreateZero(TargetVSpaceDimension)
                );
        }

        public double this[ulong id1, ulong id2] 
            => RootNode.TryGetLeafValue(
                   DomainVSpaceDimension, 
                   id1, 
                   out var mv
               ) && !ReferenceEquals(mv, null) ? mv[id2] : 0.0d;


        public GaNumMultivectorBinaryTree1D(int domainVSpaceDimension, int targetVSpaceDimension)
        {
            RootNode = new GaBtrInternalNode<IGaNumMultivector>();

            DomainVSpaceDimension = domainVSpaceDimension;
            TargetVSpaceDimension = targetVSpaceDimension;
        }
    }
}
