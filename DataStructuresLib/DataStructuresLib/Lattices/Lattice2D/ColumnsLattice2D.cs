using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib.Lattices.Lattice1D;

namespace DataStructuresLib.Lattices.Lattice2D
{
    public class ColumnsLattice2D<TValue> : 
        ILattice2D<TValue>, 
        IReadOnlyList<ILattice1D<TValue>>
    {
        private readonly List<ILattice1D<TValue>> _columnsList
            = new List<ILattice1D<TValue>>();


        public int RowsCount { get; }

        public int ColumnsCount 
            => _columnsList.Count;

        public int Count 
            => _columnsList.Count;

        public ILattice1D<TValue> this[int index] 
            => _columnsList[index];

        public TValue this[int rowIndex, int columnIndex]
        {
            get
            {
                var column = _columnsList[columnIndex];

                return column[rowIndex];
            }
        }


        public ColumnsLattice2D(int rowsCount)
        {
            Debug.Assert(rowsCount > 0);

            RowsCount = rowsCount;
        }


        public ColumnsLattice2D<TValue> AppendColumn(ILattice1D<TValue> column)
        {
            Debug.Assert(column.Count == RowsCount);

            _columnsList.Add(column);

            return this;
        }

        public ColumnsLattice2D<TValue> PrependColumn(ILattice1D<TValue> column)
        {
            Debug.Assert(column.Count == RowsCount);

            _columnsList.Insert(0, column);

            return this;
        }

        public ColumnsLattice2D<TValue> InsertColumn(ILattice1D<TValue> column, int index)
        {
            Debug.Assert(column.Count == RowsCount);

            _columnsList.Insert(index, column);

            return this;
        }
        
        public ColumnsLattice2D<TValue> RemoveColumn(int index)
        {
            _columnsList.RemoveAt(index);

            return this;
        }

        public TValue[,] GetValuesArray()
        {
            var valuesArray = new TValue[RowsCount, ColumnsCount];

            for (var colIndex = 0; colIndex < ColumnsCount; colIndex++)
            {
                var column = _columnsList[colIndex];

                for (var rowIndex = 0; rowIndex < RowsCount; rowIndex++)
                    valuesArray[rowIndex, colIndex] = column[rowIndex];
            }

            return valuesArray;
        }

        public IEnumerator<ILattice1D<TValue>> GetEnumerator()
        {
            return _columnsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}