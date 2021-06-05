using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Lattices.Lattice2D
{
    public class JoinedRowsLattice2D<TValue> :
        ILattice2D<TValue>
    {
        private readonly List<ILattice2D<TValue>> _laticesList
            = new List<ILattice2D<TValue>>();


        public int RowsCount
            => _laticesList.Sum(lattice => lattice.RowsCount);

        public int ColumnsCount { get; }

        public TValue this[int rowIndex, int columnIndex]
        {
            get
            {
                rowIndex = rowIndex.Mod(RowsCount);
                foreach (var sourceLattice in _laticesList)
                {
                    if (rowIndex < sourceLattice.RowsCount)
                        return sourceLattice[rowIndex, columnIndex];

                    rowIndex -= sourceLattice.RowsCount;
                }

                //This should never happen
                throw new InvalidOperationException();
            }
        }


        public JoinedRowsLattice2D(int columnsCount)
        {
            Debug.Assert(columnsCount > 0);

            ColumnsCount = columnsCount;
        }


        public TValue[,] GetValuesArray()
        {
            var valuesArray = new TValue[RowsCount, ColumnsCount];

            var rowIndex = 0;
            foreach (var sourceLattice in _laticesList)
            {
                for (var sourceRowIndex = 0; sourceRowIndex < sourceLattice.ColumnsCount; sourceRowIndex++)
                {
                    for (var colIndex = 0; colIndex < ColumnsCount; colIndex++)
                        valuesArray[rowIndex, colIndex] = sourceLattice[sourceRowIndex, colIndex];

                    rowIndex++;
                }
            }

            return valuesArray;
        }

        public JoinedRowsLattice2D<TValue> AppendLattice(ILattice2D<TValue> sourceLattice)
        {
            _laticesList.Add(sourceLattice);

            return this;
        }

        public JoinedRowsLattice2D<TValue> PrependLattice(ILattice2D<TValue> sourceLattice)
        {
            _laticesList.Insert(0, sourceLattice);

            return this;
        }

        public JoinedRowsLattice2D<TValue> PrependLattice(ILattice2D<TValue> sourceLattice, int index)
        {
            _laticesList.Insert(index, sourceLattice);

            return this;
        }

        public JoinedRowsLattice2D<TValue> RemoveLattice(int index)
        {
            _laticesList.RemoveAt(index);

            return this;
        }
    }
}