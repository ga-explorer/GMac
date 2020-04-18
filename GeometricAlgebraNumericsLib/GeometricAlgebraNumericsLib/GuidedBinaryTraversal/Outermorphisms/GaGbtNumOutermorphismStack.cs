using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.VectorKVectorOp;

namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Outermorphisms
{
    public sealed class GaGbtNumOutermorphismStack
        : GaGbtStack1
    {
        public static GaGbtNumOutermorphismStack Create(IReadOnlyList<GaNumVector> basisVectorsMappingsList)
        {
            var domainVSpaceDim = basisVectorsMappingsList.Count;
            var targetVSpaceDim = basisVectorsMappingsList[0].VSpaceDimension;
            var capacity = domainVSpaceDim + 1;

            return new GaGbtNumOutermorphismStack(
                capacity,
                domainVSpaceDim,
                targetVSpaceDim,
                basisVectorsMappingsList
            );
        }


        private GaNumDarKVector[] KVectorArray { get; }


        public int DomainVSpaceDimension { get; }

        public int TargetVSpaceDimension { get; }

        public int DomainGaSpaceDimension 
            => DomainVSpaceDimension.ToGaSpaceDimension();

        public int TargetGaSpaceDimension 
            => TargetVSpaceDimension.ToGaSpaceDimension();

        public IReadOnlyList<GaNumVector> BasisVectorsMappingsList { get; }

        public GaNumDarKVector TosKVector { get; private set; }

        public GaNumDarKVector RootKVector { get; }


        private GaGbtNumOutermorphismStack(int capacity, int domainVSpaceDim, int targetVSpaceDim, IReadOnlyList<GaNumVector> basisVectorsMappingsList)
            : base(capacity, domainVSpaceDim, 0ul)
        {
            KVectorArray = new GaNumDarKVector[Capacity];

            DomainVSpaceDimension = domainVSpaceDim;
            TargetVSpaceDimension = targetVSpaceDim;
            BasisVectorsMappingsList = basisVectorsMappingsList;

            RootKVector = GaNumDarKVector.CreateScalar(targetVSpaceDim, 1);
        }


        public GaNumDarKVector GetTosChildKVector0()
        {
            return TosKVector;
        }

        public GaNumDarKVector GetTosChildKVector1()
        {
            var basisVector = BasisVectorsMappingsList[TosTreeDepth - 1];

            return TosKVector.Grade == 0
                ? basisVector.ToDarKVector()
                : basisVector.Op(TosKVector);
        }


        public override bool TosHasChild0()
        {
            return true;
        }

        public override bool TosHasChild1()
        {
            return true;
        }


        public override void PushRootData()
        {
            TosIndex = 0;

            TreeDepthArray[TosIndex] = RootTreeDepth;
            IdArray[TosIndex] = RootId;
            KVectorArray[TosIndex] = RootKVector;
        }

        public override void PopNodeData()
        {
            TosTreeDepth = TreeDepthArray[TosIndex];
            TosId = IdArray[TosIndex];
            TosKVector = KVectorArray[TosIndex];

            TosIndex--;
        }

        public override void PushDataOfChild0()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;
            IdArray[TosIndex] = TosChildId0;
            KVectorArray[TosIndex] = GetTosChildKVector0();
        }

        public override void PushDataOfChild1()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;
            IdArray[TosIndex] = TosChildId1;
            KVectorArray[TosIndex] = GetTosChildKVector1();
        }

        public IEnumerable<Tuple<ulong, GaNumDarKVector>> Traverse()
        {
            GaNumVectorKVectorOpUtils.SetActiveVSpaceDimension(TargetVSpaceDimension);

            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return new Tuple<ulong, GaNumDarKVector>(TosId, TosKVector);

                    continue;
                }

                PushDataOfChild1();

                PushDataOfChild0();
            }
        }
    }
}