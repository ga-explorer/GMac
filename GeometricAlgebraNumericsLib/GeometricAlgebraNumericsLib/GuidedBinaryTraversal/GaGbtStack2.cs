using System.Diagnostics;

namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal
{
    public abstract class GaGbtStack2 
        : GaGbtStack, IGaGbtStack2
    {
        public IGaGbtStack1 Stack1 { get; }

        public IGaGbtStack1 Stack2 { get; }


        public ulong TosId1
            => Stack1.TosId;

        public ulong TosId2
            => Stack2.TosId;


        public ulong TosChildId10
            => Stack1.TosChildId0;

        public ulong TosChildId11
            => Stack1.TosChildId1;

        public ulong TosChildId20
            => Stack2.TosChildId0;

        public ulong TosChildId21
            => Stack2.TosChildId1;

        public ulong RootId1 
            => Stack1.RootId;

        public ulong RootId2 
            => Stack2.RootId;


        protected GaGbtStack2(IGaGbtStack1 stack1, IGaGbtStack1 stack2)
            : base(stack1.Capacity, stack1.RootTreeDepth)
        {
            Debug.Assert(
                stack1.Capacity == stack2.Capacity &&
                stack1.RootTreeDepth == stack2.RootTreeDepth
            );

            Stack1 = stack1;
            Stack2 = stack2;
        }


        public override void PushRootData()
        {
            TosIndex = 0;

            TreeDepthArray[TosIndex] = RootTreeDepth;

            Stack1.PushRootData();
            Stack2.PushRootData();
        }

        public override void PopNodeData()
        {
            Stack1.PopNodeData();
            Stack2.PopNodeData();

            TosTreeDepth = TreeDepthArray[TosIndex];

            //Console.Out.WriteLine($"depth:{TosTreeDepth}, id1: {TosId1}, id2: {TosId2}");

            TosIndex--;
        }
        

        public bool TosHasChild10()
        {
            return Stack1.TosHasChild0();
        }

        public bool TosHasChild11()
        {
            return Stack1.TosHasChild1();
        }

        public bool TosHasChild20()
        {
            return Stack2.TosHasChild0();
        }

        public bool TosHasChild21()
        {
            return Stack2.TosHasChild1();
        }


        public void PushDataOfChild00()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;

            Stack1.PushDataOfChild0();
            Stack2.PushDataOfChild0();
        }

        public void PushDataOfChild10()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;

            Stack1.PushDataOfChild1();
            Stack2.PushDataOfChild0();
        }

        public void PushDataOfChild01()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;

            Stack1.PushDataOfChild0();
            Stack2.PushDataOfChild1();
        }

        public void PushDataOfChild11()
        {
            TosIndex++;

            TreeDepthArray[TosIndex] = TosTreeDepth - 1;

            Stack1.PushDataOfChild1();
            Stack2.PushDataOfChild1();
        }
    }
}