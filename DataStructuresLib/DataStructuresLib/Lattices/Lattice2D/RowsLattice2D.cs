using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib.Lattices.Lattice1D;

namespace DataStructuresLib.Lattices.Lattice2D
{
    public class RowsLattice2D<TValue> : 
        ILattice2D<TValue>, 
        IReadOnlyList<ILattice1D<TValue>>
    {
        private readonly List<ILattice1D<TValue>> _rowsList
            = new List<ILattice1D<TValue>>();


        public int RowsCount 
            => _rowsList.Count;

        public int ColumnsCount { get; }

        public int Count 
            => _rowsList.Count;

        public ILattice1D<TValue> this[int index] 
            => _rowsList[index];

        public TValue this[int rowIndex, int columnIndex]
        {
            get
            {
                var column = _rowsList[rowIndex];

                return column[columnIndex];
            }
        }


        public RowsLattice2D(int columnsCount)
        {
            Debug.Assert(columnsCount > 0);

            ColumnsCount = columnsCount;
        }


        public RowsLattice2D<TValue> AppendRow(ILattice1D<TValue> row)
        {
            Debug.Assert(row.Count == ColumnsCount);

            _rowsList.Add(row);

            return this;
        }

        public RowsLattice2D<TValue> PrependRow(ILattice1D<TValue> row)
        {
            Debug.Assert(row.Count == ColumnsCount);

            _rowsList.Insert(0, row);

            return this;
        }

        public RowsLattice2D<TValue> InsertRow(ILattice1D<TValue> row, int index)
        {
            Debug.Assert(row.Count == ColumnsCount);

            _rowsList.Insert(index, row);

            return this;
        }
        
        public RowsLattice2D<TValue> RemoveRow(int index)
        {
            _rowsList.RemoveAt(index);

            return this;
        }

        public TValue[,] GetValuesArray()
        {
            var valuesArray = new TValue[RowsCount, ColumnsCount];
            
            for (var rowIndex = 0; rowIndex < RowsCount; rowIndex++)
            {
                var row = _rowsList[rowIndex];

                for (var colIndex = 0; colIndex < ColumnsCount; colIndex++)
                    valuesArray[rowIndex, colIndex] = row[colIndex];
            }

            return valuesArray;
        }

        public IEnumerator<ILattice1D<TValue>> GetEnumerator()
        {
            return _rowsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}