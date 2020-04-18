namespace GeometricAlgebraNumericsLib.Structures.BinaryTraversal
{
    public sealed class GaGbtBasisVectorNode : IGaGbtNode1<double>
    {
        public static GaGbtBasisVectorNode CreateRootNode(int treeDepth, int basisVectorIndex)
        {
            return new GaGbtBasisVectorNode(treeDepth, 0, basisVectorIndex);
        }


        public int BasisVectorIndex { get; }

        public int TreeDepth { get; }

        public ulong Id { get; }

        public ulong ChildId0
            => Id;

        public ulong ChildId1
            => Id | (1ul << (TreeDepth - 1));

        public double Value
            => TreeDepth == 0 ? 1 : 0;


        private GaGbtBasisVectorNode(int treeDepth, ulong id, int basisVectorIndex)
        {
            TreeDepth = treeDepth;
            Id = id;

            BasisVectorIndex = basisVectorIndex;
        }


        public bool HasChild0()
        {
            return TreeDepth - 1 != BasisVectorIndex;
        }

        public bool HasChild1()
        {
            return TreeDepth - 1 == BasisVectorIndex;
        }

        public IGaGbtNode1 GetChild0()
        {
            return GetValueChild0();
        }

        public IGaGbtNode1 GetChild1()
        {
            return GetValueChild1();
        }

        public IGaGbtNode1<double> GetValueChild0()
        {
            return new GaGbtBasisVectorNode(
                TreeDepth - 1,
                ChildId0,
                BasisVectorIndex
            );
        }

        public IGaGbtNode1<double> GetValueChild1()
        {
            return new GaGbtBasisVectorNode(
                TreeDepth - 1,
                ChildId1,
                BasisVectorIndex
            );
        }
    }
}