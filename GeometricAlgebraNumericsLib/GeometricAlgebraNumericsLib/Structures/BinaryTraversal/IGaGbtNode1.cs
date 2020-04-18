namespace GeometricAlgebraNumericsLib.Structures.BinaryTraversal
{
    public interface IGaGbtNode1 : IGaGbtNode
    {
        ulong Id { get; }

        ulong ChildId0 { get; }

        ulong ChildId1 { get; }

        bool HasChild0();

        bool HasChild1();

        IGaGbtNode1 GetChild0();

        IGaGbtNode1 GetChild1();
    }

    public interface IGaGbtNode1<out T> : IGaGbtNode1
    {
        T Value { get; }

        IGaGbtNode1<T> GetValueChild0();

        IGaGbtNode1<T> GetValueChild1();
    }
}