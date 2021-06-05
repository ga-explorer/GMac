namespace DataStructuresLib.Lattices.Lattice2D
{
    /// <summary>
    /// A 2D lattice is like an array that can be indexed using any two integers,
    /// positive or negative, such that it's a periodic 2D block of values
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface ILattice2D<out TValue>
    {
        int RowsCount { get; }

        int ColumnsCount { get; }

        TValue this[int rowIndex, int columnIndex] { get; }

        TValue[,] GetValuesArray();
    }
}