using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Structures.BinaryTraversal
{
    public sealed class GaGbtFarMultivectorNode : IGaGbtNode1<double>
    {
        public static GaGbtFarMultivectorNode CreateRootNode(IGaNumMultivector multivector)
        {
            return new GaGbtFarMultivectorNode(
                multivector.VSpaceDimension,
                0,
                multivector
            );
        }
        
        
        public int TreeDepth { get; }

        public ulong Id { get; }

        public ulong ChildId0
            => Id;

        public ulong ChildId1
            => Id | (1ul << (TreeDepth - 1));

        public double Value 
            => TreeDepth == 0 ? Multivector[(int)Id] : 0;

        public IGaNumMultivector Multivector { get; }


        private GaGbtFarMultivectorNode(int treeDepth, ulong id, IGaNumMultivector multivector)
        {
            TreeDepth = treeDepth;

            Id = id;

            Multivector = multivector;
        }


        public bool HasChild0()
        {
            return true;
        }

        public bool HasChild1()
        {
            return true;
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
            return new GaGbtFarMultivectorNode(
                TreeDepth - 1,
                ChildId0,
                Multivector
            );
        }

        public IGaGbtNode1<double> GetValueChild1()
        {
            return new GaGbtFarMultivectorNode(
                TreeDepth - 1,
                ChildId1,
                Multivector
            );
        }
    }
}