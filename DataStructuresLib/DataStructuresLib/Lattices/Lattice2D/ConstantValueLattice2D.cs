namespace DataStructuresLib.Lattices.Lattice2D
{
    public class ConstantValueLattice2D<TValue> :
        ILattice2D<TValue>
    {
        public TValue Value { get; }

        public int RowsCount { get; }

        public int ColumnsCount { get; }

        public TValue this[int rowIndex, int columnIndex] 
            => Value;


        public ConstantValueLattice2D(int rowsCount, int columnsCount, TValue value)
        {
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            Value = value;
        }


        public TValue[,] GetValuesArray()
        {
            var valuesArray = new TValue[RowsCount, ColumnsCount];

            for (var rowIndex = 0; rowIndex < RowsCount; rowIndex++)
            for (var colIndex = 0; colIndex < ColumnsCount; colIndex++)
                valuesArray[rowIndex, colIndex] = Value;

            return valuesArray;
        }
    }
}