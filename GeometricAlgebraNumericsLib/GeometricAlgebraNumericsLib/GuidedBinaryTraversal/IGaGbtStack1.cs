namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal
{
    public interface IGaGbtStack1 : IGaGbtStack
    {
        ulong TosId { get; }

        ulong TosChildId0 { get; }

        ulong TosChildId1 { get; }


        ulong RootId { get; }


        bool TosHasChild0();

        bool TosHasChild1();


        void PushDataOfChild0();

        void PushDataOfChild1();
    }

    public interface IGaGbtStack1<out T> : IGaGbtStack1
    {
        T TosValue { get; }
    }
}