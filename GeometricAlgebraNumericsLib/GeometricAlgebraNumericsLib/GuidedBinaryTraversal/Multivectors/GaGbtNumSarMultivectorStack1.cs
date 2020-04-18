using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Structures.BinaryTrees;

namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors
{
    public sealed class GaGbtNumSarMultivectorStack1 
        : GaGbtStack1, IGaGbtNumMultivectorStack1
    {
        public static GaGbtNumSarMultivectorStack1 Create(int capacity, GaNumMultivector multivector)
        {
            return new GaGbtNumSarMultivectorStack1(capacity, multivector);
        }


        private IGaBtrNode<double>[] BtrNodeArray { get; }


        public IGaNumMultivector Multivector { get; }

        public IGaBtrNode<double> TosBtrNode { get; private set; }

        public double TosValue { get; private set; }

        public IGaBtrNode<double> RootBtrNode { get; }


        private GaGbtNumSarMultivectorStack1(int capacity, GaNumMultivector multivector)
            : base(capacity, multivector.VSpaceDimension, 0ul)
        {
            BtrNodeArray = new IGaBtrNode<double>[capacity];
            Multivector = multivector;

            RootBtrNode = multivector.BtrRootNode;
        }


        public override bool TosHasChild0()
        {
            return TosBtrNode.HasChildNode0;
        }

        public override bool TosHasChild1()
        {
            return TosBtrNode.HasChildNode1;
        }


        public override void PushRootData()
        {
            TosIndex = 0;

            TreeDepthArray[TosIndex] = RootTreeDepth;
            IdArray[TosIndex] = RootId;
            BtrNodeArray[TosIndex] = RootBtrNode;
        }

        public override void PopNodeData()
        {
            TosTreeDepth = TreeDepthArray[TosIndex];
            TosId = IdArray[TosIndex];

            if (TosTreeDepth > 0)
            {
                TosBtrNode = BtrNodeArray[TosIndex];
            }
            else
            {
                TosValue = BtrNodeArray[TosIndex].Value;
            }

            TosIndex--;
        }

        public override void PushDataOfChild0()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;
            IdArray[TosIndex] = TosChildId0;
            BtrNodeArray[TosIndex] = TosBtrNode.ChildNode0;
        }

        public override void PushDataOfChild1()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;
            IdArray[TosIndex] = TosChildId1;
            BtrNodeArray[TosIndex] = TosBtrNode.ChildNode1;
        }
    }
}