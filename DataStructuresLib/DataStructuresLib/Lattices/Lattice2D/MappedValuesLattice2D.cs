using System;

namespace DataStructuresLib.Lattices.Lattice2D
{
    public class MappedValuesLattice2D<TValue> :
        ILattice2D<TValue>
    {
        public ILattice2D<TValue> SourceLattice { get; }

        public Func<TValue, TValue> ValueMapping { get; }

        public int RowsCount 
            => SourceLattice.RowsCount;

        public int ColumnsCount 
            => SourceLattice.ColumnsCount;

        public TValue this[int rowIndex, int columnIndex] 
            => ValueMapping(SourceLattice[columnIndex, rowIndex]);


        public MappedValuesLattice2D(ILattice2D<TValue> sourceGrid, Func<TValue, TValue> valueMapping)
        {
            SourceLattice = sourceGrid;
            ValueMapping = valueMapping;
        }


        public TValue[,] GetValuesArray()
        {
            var valuesArray = new TValue[RowsCount, ColumnsCount];

            for (var colIndex = 0; colIndex < ColumnsCount; colIndex++)
            for (var rowIndex = 0; rowIndex < RowsCount; rowIndex++)
                valuesArray[rowIndex, colIndex] = ValueMapping(SourceLattice[rowIndex, colIndex]);

            return valuesArray;
        }
    }

    public class MappedValuesLattice2D<TValue1, TValue2> :
        ILattice2D<TValue2>
    {
        public ILattice2D<TValue1> SourceLattice { get; }

        public Func<TValue1, TValue2> ValueMapping { get; }

        public int RowsCount 
            => SourceLattice.RowsCount;

        public int ColumnsCount 
            => SourceLattice.ColumnsCount;

        public TValue2 this[int rowIndex, int columnIndex] 
            => ValueMapping(SourceLattice[columnIndex, rowIndex]);


        public MappedValuesLattice2D(ILattice2D<TValue1> sourceGrid, Func<TValue1, TValue2> valueMapping)
        {
            SourceLattice = sourceGrid;
            ValueMapping = valueMapping;
        }


        public TValue2[,] GetValuesArray()
        {
            var valuesArray = new TValue2[RowsCount, ColumnsCount];

            for (var colIndex = 0; colIndex < ColumnsCount; colIndex++)
            for (var rowIndex = 0; rowIndex < RowsCount; rowIndex++)
                valuesArray[rowIndex, colIndex] = ValueMapping(SourceLattice[rowIndex, colIndex]);

            return valuesArray;
        }
    }
}