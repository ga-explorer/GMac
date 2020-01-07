namespace DataStructuresLib.Basic
{
    public interface ITriplet<out T>
    {
        T Item1 { get; }

        T Item2 { get; }

        T Item3 { get; }
    }
}