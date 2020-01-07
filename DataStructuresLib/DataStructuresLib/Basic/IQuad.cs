namespace DataStructuresLib.Basic
{
    public interface IQuad<out T>
    {
        T Item1 { get; }

        T Item2 { get; }

        T Item3 { get; }

        T Item4 { get; }
    }
}