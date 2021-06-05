using System;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Lattices.Lattice2D
{
    public class ComputedValuesLattice2D<TValue> :
        ILattice2D<TValue>
    {
        public Func<int, int, TValue> ComputingFunc { get; }

        public int RowsCount { get; }

        public int ColumnsCount { get; }

        public TValue this[int rowIndex, int columnIndex] 
            => ComputingFunc(
                rowIndex.Mod(RowsCount),
                columnIndex.Mod(ColumnsCount)
            );


        public ComputedValuesLattice2D(int rowsCount, int columnsCount, Func<int, int, TValue> computingFunc)
        {
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            ComputingFunc = computingFunc;
        }


        public TValue[,] GetValuesArray()
        {
            var valuesArray = new TValue[RowsCount, ColumnsCount];

            for (var rowIndex = 0; rowIndex < RowsCount; rowIndex++)
            for (var colIndex = 0; colIndex < ColumnsCount; colIndex++)
                valuesArray[rowIndex, colIndex] = ComputingFunc(rowIndex, colIndex);

            return valuesArray;
        }
    }
}