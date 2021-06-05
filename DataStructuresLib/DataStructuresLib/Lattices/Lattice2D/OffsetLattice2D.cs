namespace DataStructuresLib.Lattices.Lattice2D
{
    public class OffsetLattice2D<TValue> :
        ILattice2D<TValue>
    {
        public ILattice2D<TValue> SourceLattice { get; }

        public int RowsCount 
            => SourceLattice.RowsCount;

        public int ColumnsCount 
            => SourceLattice.ColumnsCount;

        public int SourceRowOffset { get; }

        public int SourceColumnOffset { get; }

        public TValue this[int rowIndex, int columnIndex] 
            => SourceLattice[
                SourceRowOffset + rowIndex, 
                SourceColumnOffset + columnIndex
            ];


        public OffsetLattice2D(ILattice2D<TValue> sourceLattice, int sourceRowOffset, int sourceColumnOffset)
        {
            SourceLattice = sourceLattice;
            SourceRowOffset = sourceRowOffset;
            SourceColumnOffset = sourceColumnOffset;
        }


        public TValue[,] GetValuesArray()
        {
            var valuesArray = new TValue[RowsCount, ColumnsCount];

            for (var colIndex = 0; colIndex < ColumnsCount; colIndex++)
            {
                var sourceColIndex = 
                    (SourceColumnOffset + colIndex) % SourceLattice.ColumnsCount;

                for (var rowIndex = 0; rowIndex < RowsCount; rowIndex++)
                {
                    var sourceRowIndex = 
                        (SourceRowOffset + rowIndex) % SourceLattice.RowsCount;

                    valuesArray[rowIndex, colIndex] = SourceLattice[sourceRowIndex, sourceColIndex];
                }
            }

            return valuesArray;
        }
    }
}