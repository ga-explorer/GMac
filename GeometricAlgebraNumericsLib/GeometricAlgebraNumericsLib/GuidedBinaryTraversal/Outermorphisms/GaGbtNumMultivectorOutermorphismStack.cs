using System;
using System.Collections.Generic;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.VectorKVectorOp;

namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Outermorphisms
{
    public sealed class GaGbtNumMultivectorOutermorphismStack 
        : GaGbtStack1
    {
        public static GaGbtNumMultivectorOutermorphismStack Create(IReadOnlyList<IGaNumVector> basisVectorsMappingsList, IGaNumMultivector mv)
        {
            return new GaGbtNumMultivectorOutermorphismStack(
                basisVectorsMappingsList,
                mv.CreateGbtStack(mv.VSpaceDimension + 1)
            );
        }


        private GaNumDarKVector[] KVectorArray { get; }

        private IGaGbtNumMultivectorStack1 MultivectorStack { get; }


        public IReadOnlyList<IGaNumVector> BasisVectorsMappingsList { get; }

        public IGaNumMultivector Multivector 
            => MultivectorStack.Multivector;

        public GaNumDarKVector TosKVector { get; private set; }

        public double TosValue 
            => MultivectorStack.TosValue;

        public GaNumDarKVector RootKVector { get; }


        private GaGbtNumMultivectorOutermorphismStack(IReadOnlyList<IGaNumVector> basisVectorsMappingsList, IGaGbtNumMultivectorStack1 multivectorStack)
            : base(multivectorStack.Capacity, multivectorStack.RootTreeDepth, multivectorStack.RootId)
        {
            KVectorArray = new GaNumDarKVector[Capacity];

            BasisVectorsMappingsList = basisVectorsMappingsList;
            MultivectorStack = multivectorStack;

            RootKVector = GaNumDarKVector.CreateScalar(Multivector.VSpaceDimension, 1);
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
            return MultivectorStack.TosHasChild0();
        }

        public override bool TosHasChild1()
        {
            return MultivectorStack.TosHasChild1();
        }


        public override void PushRootData()
        {
            TosIndex = 0;

            TreeDepthArray[TosIndex] = RootTreeDepth;
            IdArray[TosIndex] = RootId;
            KVectorArray[TosIndex] = RootKVector;
            
            MultivectorStack.PushRootData();
        }

        public override void PopNodeData()
        {
            MultivectorStack.PopNodeData();

            TosTreeDepth = TreeDepthArray[TosIndex];
            TosId = IdArray[TosIndex];
            TosKVector = KVectorArray[TosIndex];

            TosIndex--;
        }

        public override void PushDataOfChild0()
        {
            MultivectorStack.PushDataOfChild0();

            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;
            IdArray[TosIndex] = TosChildId0;
            KVectorArray[TosIndex] = GetTosChildKVector0();
        }

        public override void PushDataOfChild1()
        {
            MultivectorStack.PushDataOfChild1();

            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;
            IdArray[TosIndex] = TosChildId1;
            KVectorArray[TosIndex] = GetTosChildKVector1();
        }

        public IEnumerable<Tuple<double, GaNumDarKVector>> TraverseForScaledKVectors()
        {
            GaNumVectorKVectorOpUtils.SetActiveVSpaceDimension(Multivector.VSpaceDimension);

            PushRootData();

            //var maxStackSizeCounter = 0;

            while (!IsEmpty)
            {
                //maxStackSizeCounter = Math.Max(maxStackSizeCounter, nodesStack.Count);

                PopNodeData();

                if (TosIsLeaf)
                {
                    if (TosValue != 0)
                        yield return new Tuple<double, GaNumDarKVector>(TosValue, TosKVector);

                    continue;
                }

                if (TosHasChild1())
                    PushDataOfChild1();

                if (TosHasChild0())
                    PushDataOfChild0();

                //var stackSize = opStack.SizeInBytes();
                //if (sizeCounter < stackSize) sizeCounter = stackSize;
            }

            //Console.WriteLine("Max Stack Size: " + sizeCounter.ToString("###,###,###,###,###,##0"));
            //Console.WriteLine(@"Max Stack Size: " + maxStackSizeCounter.ToString("###,###,###,###,###,##0"));        }
        }

        public IEnumerable<Tuple<ulong, GaNumDarKVector>> TraverseForIdKVectors()
        {
            GaNumVectorKVectorOpUtils.SetActiveVSpaceDimension(Multivector.VSpaceDimension);

            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return new Tuple<ulong, GaNumDarKVector>(TosId, TosKVector);

                    continue;
                }

                if (TosHasChild1())
                    PushDataOfChild1();

                if (TosHasChild0())
                    PushDataOfChild0();
            }
        }
    }
}