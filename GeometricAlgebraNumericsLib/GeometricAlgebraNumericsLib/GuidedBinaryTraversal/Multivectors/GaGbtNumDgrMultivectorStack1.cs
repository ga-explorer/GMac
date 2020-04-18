using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors
{
    public sealed class GaGbtNumDgrMultivectorStack1
        : GaGbtStack1, IGaGbtNumMultivectorStack1
    {
        public static GaGbtNumDgrMultivectorStack1 Create(int capacity, IGaNumMultivector multivector)
        {
            return new GaGbtNumDgrMultivectorStack1(capacity, multivector);
        }


        private ulong[] ActiveGradesBitMask0Array { get; }

        private ulong[] ActiveGradesBitMask1Array { get; }


        public IGaNumMultivector Multivector { get; }

        public double TosValue { get; private set; }


        public ulong ActiveGradesBitPattern { get; }

        public ulong TosActiveGradesBitMask0 { get; private set; }

        public ulong TosActiveGradesBitMask1 { get; private set; }

        public ulong TosChildActiveGradesBitPattern0
            => ActiveGradesBitPattern &
               (TosActiveGradesBitMask0 >> 1) &
               TosActiveGradesBitMask1;

        public ulong TosChildActiveGradesBitPattern1
            => ActiveGradesBitPattern &
               TosActiveGradesBitMask0 &
               (TosActiveGradesBitMask1 << 1);

        public ulong RootActiveGradesBitMask0 { get; }

        public ulong RootActiveGradesBitMask1 { get; }


        private GaGbtNumDgrMultivectorStack1(int capacity, IGaNumMultivector multivector)
            : base(capacity, multivector.VSpaceDimension, 0ul)
        {
            Multivector = multivector;
            ActiveGradesBitPattern = (ulong)multivector.GetStoredGradesBitPattern();

            ActiveGradesBitMask0Array = new ulong[capacity];
            ActiveGradesBitMask1Array = new ulong[capacity];

            RootActiveGradesBitMask0 =
                RootActiveGradesBitMask1 =
                    (1ul << (multivector.VSpaceDimension + 2)) - 1;
        }


        public override bool TosHasChild0()
        {
            return TosChildActiveGradesBitPattern0 != 0;
        }

        public override bool TosHasChild1()
        {
            return TosChildActiveGradesBitPattern1 != 0;
        }


        public override void PushRootData()
        {
            TosIndex = 0;

            TreeDepthArray[TosIndex] = RootTreeDepth;
            IdArray[TosIndex] = RootId;
            ActiveGradesBitMask0Array[TosIndex] = RootActiveGradesBitMask0;
            ActiveGradesBitMask1Array[TosIndex] = RootActiveGradesBitMask1;
        }

        public override void PopNodeData()
        {
            TosTreeDepth = TreeDepthArray[TosIndex];
            TosId = IdArray[TosIndex];

            if (TosTreeDepth > 0)
            {
                TosActiveGradesBitMask0 = ActiveGradesBitMask0Array[TosIndex];
                TosActiveGradesBitMask1 = ActiveGradesBitMask1Array[TosIndex];
            }
            else
            {
                TosValue = Multivector[(int)TosId];
            }

            TosIndex--;
        }

        public override void PushDataOfChild0()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;
            IdArray[TosIndex] = TosChildId0;
            ActiveGradesBitMask0Array[TosIndex] = TosActiveGradesBitMask0 >> 1;
            ActiveGradesBitMask1Array[TosIndex] = TosActiveGradesBitMask1;
        }

        public override void PushDataOfChild1()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;
            IdArray[TosIndex] = TosChildId1;
            ActiveGradesBitMask0Array[TosIndex] = TosActiveGradesBitMask0;
            ActiveGradesBitMask1Array[TosIndex] = TosActiveGradesBitMask1 << 1;
        }
    }
}