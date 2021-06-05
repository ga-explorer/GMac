using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Lattices.Lattice2D
{
    public class JoinedColumnsLattice2D<TValue> :
        ILattice2D<TValue>
    {
        private readonly List<ILattice2D<TValue>> _laticesList
            = new List<ILattice2D<TValue>>();


        public int RowsCount { get; }

        public int ColumnsCount 
            => _laticesList.Sum(lattice => lattice.ColumnsCount);

        public TValue this[int rowIndex, int columnIndex]
        {
            get
            {
                columnIndex = columnIndex.Mod(ColumnsCount);
                foreach (var sourceLattice in _laticesList)
                {
                    if (columnIndex < sourceLattice.ColumnsCount)
                        return sourceLattice[rowIndex, columnIndex];

                    columnIndex -= sourceLattice.ColumnsCount;
                }

                //This should never happen
                throw new InvalidOperationException();
            }
        }


        public JoinedColumnsLattice2D(int rowsCount)
        {
            Debug.Assert(rowsCount > 0);

            RowsCount = rowsCount;
        }


        public TValue[,] GetValuesArray()
        {
            var valuesArray = new TValue[RowsCount, ColumnsCount];

            var colIndex = 0;
            foreach (var sourceLattice in _laticesList)
            {
                for (var sourceColIndex = 0; sourceColIndex < sourceLattice.ColumnsCount; sourceColIndex++)
                {
                    for (var rowIndex = 0; rowIndex < RowsCount; rowIndex++)
                        valuesArray[rowIndex, colIndex] = sourceLattice[rowIndex, sourceColIndex];

                    colIndex++;
                }
            }

            return valuesArray;
        }

        public JoinedColumnsLattice2D<TValue> AppendLattice(ILattice2D<TValue> sourceLattice)
        {
            _laticesList.Add(sourceLattice);

            return this;
        }

        public JoinedColumnsLattice2D<TValue> PrependLattice(ILattice2D<TValue> sourceLattice)
        {
            _laticesList.Insert(0, sourceLattice);

            return this;
        }

        public JoinedColumnsLattice2D<TValue> PrependLattice(ILattice2D<TValue> sourceLattice, int index)
        {
            _laticesList.Insert(index, sourceLattice);

            return this;
        }

        public JoinedColumnsLattice2D<TValue> RemoveLattice(int index)
        {
            _laticesList.RemoveAt(index);

            return this;
        }
    }
}