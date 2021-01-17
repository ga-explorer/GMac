using GeometricAlgebraStructuresLib.Storage;

namespace GeometricAlgebraStructuresLib.GuidedBinaryTraversal.Multivectors
{
    public sealed class GaGbtMvsDenseArrayStack1<T>
        : GaGbtStack1, IGaGbtMultivectorStorageStack1<T>
    {
        public static GaGbtMvsDenseArrayStack1<T> Create(int capacity, IGaMultivectorStorage<T> multivectorStorage)
        {
            return new GaGbtMvsDenseArrayStack1<T>(capacity, multivectorStorage);
        }


        public IGaMultivectorStorage<T> MultivectorStorage { get; }

        public T TosValue { get; private set; }


        private GaGbtMvsDenseArrayStack1(int capacity, IGaMultivectorStorage<T> multivectorStorage)
            : base(capacity, multivectorStorage.VSpaceDimension, 0ul)
        {
            MultivectorStorage = multivectorStorage;
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
                TosValue = MultivectorStorage.GetTermScalar((int)TosId);
            }

            TosIndex--;
        }

        public override bool TosHasChild(int childIndex)
        {
            return true;
        }

        public override void PushDataOfChild(int childIndex)
        {
            TosIndex++;
            TreeDepthArray[TosIndex] = TosTreeDepth - 1;

            if ((childIndex & 1) == 0)
                IdArray[TosIndex] = TosChildId0;
            else
                IdArray[TosIndex] = TosChildId1;
        }
    }
}