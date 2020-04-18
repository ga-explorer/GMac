namespace GeometricAlgebraNumericsLib.Structures.BinaryTraversal
{
    public sealed class GaGbtBasisBladeNode : IGaGbtNode1<double>
    {
        public static GaGbtBasisBladeNode CreateRootNode(int treeDepth, ulong basisBladeId)
        {
            return new GaGbtBasisBladeNode(treeDepth, 0, basisBladeId);
        }


        public ulong BasisBladeId { get; }

        public int TreeDepth { get; }

        public ulong Id { get; }

        public ulong ChildId0
            => Id;

        public ulong ChildId1
            => Id | (1ul << (TreeDepth - 1));

        public double Value
            => TreeDepth == 0 ? 1 : 0;


        private GaGbtBasisBladeNode(int treeDepth, ulong id, ulong basisBladeId)
        {
            TreeDepth = treeDepth;
            Id = id;

            BasisBladeId = basisBladeId;
        }


        public bool HasChild0()
        {
            return ((1ul << (TreeDepth - 1)) & BasisBladeId) == 0;
        }

        public bool HasChild1()
        {
            return ((1ul << (TreeDepth - 1)) & BasisBladeId) != 0;
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
            return new GaGbtBasisBladeNode(
                TreeDepth - 1,
                BasisBladeId,
                ChildId0
            );
        }

        public IGaGbtNode1<double> GetValueChild1()
        {
            return new GaGbtBasisBladeNode(
                TreeDepth - 1,
                BasisBladeId,
                ChildId1
            );
        }
    }
}