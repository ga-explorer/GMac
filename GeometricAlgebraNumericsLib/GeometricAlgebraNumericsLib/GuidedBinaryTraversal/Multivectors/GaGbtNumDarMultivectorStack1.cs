using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors
{
    public sealed class GaGbtNumDarMultivectorStack1
        : GaGbtStack1, IGaGbtNumMultivectorStack1
    {
        public static GaGbtNumDarMultivectorStack1 Create(int capacity, IGaNumMultivector multivector)
        {
            return new GaGbtNumDarMultivectorStack1(capacity, multivector);
        }


        public IGaNumMultivector Multivector { get; }

        public double TosValue { get; private set; }


        private GaGbtNumDarMultivectorStack1(int capacity, IGaNumMultivector multivector)
            : base(capacity, multivector.VSpaceDimension, 0ul)
        {
            Multivector = multivector;
        }


        public override void PushRootData()
        {
            TosIndex = 0;

            TreeDepthArray[TosIndex] = RootTreeDepth;
            IdArray[TosIndex] = RootId;
        }

        public override void PopNodeData()
        {
            TosTreeDepth = TreeDepthArray[TosIndex];
            TosId = IdArray[TosIndex];

            if (TosTreeDepth == 0)
            {
                TosValue = Multivector[(int)TosId];
            }

            TosIndex--;
        }


        public override bool TosHasChild0()
        {
            return TosTreeDepth > 0;
        }

        public override bool TosHasChild1()
        {
            return TosTreeDepth > 0;
        }

        public override void PushDataOfChild0()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;
            IdArray[TosIndex] = TosChildId0;
        }

        public override void PushDataOfChild1()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;
            IdArray[TosIndex] = TosChildId1;
        }
    }
}