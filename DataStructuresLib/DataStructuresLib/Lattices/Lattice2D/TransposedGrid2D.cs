namespace DataStructuresLib.Lattices.Lattice2D
{
    public class TransposedLattice2D<TValue> :
        ILattice2D<TValue>
    {
        public ILattice2D<TValue> SourceLattice { get; }

        public int RowsCount 
            => SourceLattice.ColumnsCount;

        public int ColumnsCount 
            => SourceLattice.RowsCount;

        public TValue this[int rowIndex, int columnIndex] 
            => SourceLattice[columnIndex, rowIndex];


        public TransposedLattice2D(ILattice2D<TValue> sourceLattice)
        {
            SourceLattice = sourceLattice;
        }


        public TValue[,] GetValuesArray()
        {
            var valuesArray = new TValue[RowsCount, ColumnsCount];

            for (var colIndex = 0; colIndex < ColumnsCount; colIndex++)
            for (var rowIndex = 0; rowIndex < RowsCount; rowIndex++)
                valuesArray[rowIndex, colIndex] = SourceLattice[colIndex, rowIndex];

            return valuesArray;
        }
    }
}