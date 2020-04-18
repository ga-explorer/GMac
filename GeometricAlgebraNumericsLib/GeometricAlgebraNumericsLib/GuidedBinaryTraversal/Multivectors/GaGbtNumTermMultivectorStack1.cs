using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors
{
    public sealed class GaGbtNumTermMultivectorStack1
        : GaGbtStack1, IGaGbtNumMultivectorStack1
    {
        public static GaGbtNumTermMultivectorStack1 Create(int capacity, GaNumTerm term)
        {
            return new GaGbtNumTermMultivectorStack1(capacity, term);
        }


        public GaNumTerm Term { get; }

        public IGaNumMultivector Multivector 
            => Term;

        public double TosValue { get; private set; }


        private GaGbtNumTermMultivectorStack1(int capacity, GaNumTerm term)
            : base(capacity, term.VSpaceDimension, 0ul)
        {
            Term = term;
        }


        public override bool TosHasChild0()
        {
            return ((1ul << (TosTreeDepth - 1)) & (ulong)Term.BasisBladeId) == 0;
        }

        public override bool TosHasChild1()
        {
            return ((1ul << (TosTreeDepth - 1)) & (ulong)Term.BasisBladeId) != 0;
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
                TosValue = Term[(int)TosId];
            }

            TosIndex--;
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