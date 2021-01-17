using GeometricAlgebraStructuresLib.Storage;

namespace GeometricAlgebraStructuresLib.GuidedBinaryTraversal.Multivectors
{
    public sealed class GaGbtMvsSparseGradedStack1<T>
        : GaGbtStack1, IGaGbtMultivectorStorageStack1<T>
    {
        public static GaGbtMvsSparseGradedStack1<T> Create(int capacity, IGaMultivectorStorage<T> multivectorStorage)
        {
            return new GaGbtMvsSparseGradedStack1<T>(capacity, multivectorStorage);
        }


        private ulong[] ActiveGradesBitMask0Array { get; }

        private ulong[] ActiveGradesBitMask1Array { get; }


        public IGaMultivectorStorage<T> MultivectorStorage { get; }

        public T TosValue { get; private set; }


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


        private GaGbtMvsSparseGradedStack1(int capacity, IGaMultivectorStorage<T> multivectorStorage)
            : base(capacity, multivectorStorage.VSpaceDimension, 0ul)
        {
            MultivectorStorage = multivectorStorage;
            ActiveGradesBitPattern = (ulong)multivectorStorage.GetStoredGradesBitPattern();

            ActiveGradesBitMask0Array = new ulong[capacity];
            ActiveGradesBitMask1Array = new ulong[capacity];

            RootActiveGradesBitMask0 = 
                RootActiveGradesBitMask1 = 
                    (1ul << (multivectorStorage.VSpaceDimension + 2)) - 1;
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
                TosValue = MultivectorStorage.GetTermScalar((int)TosId);
            }

            TosIndex--;
        }

        public override bool TosHasChild(int childIndex)
        {
            if ((childIndex & 1) == 0)
                return TosChildActiveGradesBitPattern0 != 0 && (
                    TosTreeDepth > 1 || MultivectorStorage.ContainsStoredTerm((int)TosChildId0)
                );

            return TosChildActiveGradesBitPattern1 != 0 && (
                TosTreeDepth > 1 || MultivectorStorage.ContainsStoredTerm((int)TosChildId1)
            );
        }

        public override void PushDataOfChild(int childIndex)
        {
            TosIndex++;
            TreeDepthArray[TosIndex] = TosTreeDepth - 1;

            if ((childIndex & 1) == 0)
            {
                IdArray[TosIndex] = TosChildId0;
                ActiveGradesBitMask0Array[TosIndex] = TosActiveGradesBitMask0 >> 1;
                ActiveGradesBitMask1Array[TosIndex] = TosActiveGradesBitMask1;
            }
            else
            {
                IdArray[TosIndex] = TosChildId1;
                ActiveGradesBitMask0Array[TosIndex] = TosActiveGradesBitMask0;
                ActiveGradesBitMask1Array[TosIndex] = TosActiveGradesBitMask1 << 1;
            }
        }
    }
}