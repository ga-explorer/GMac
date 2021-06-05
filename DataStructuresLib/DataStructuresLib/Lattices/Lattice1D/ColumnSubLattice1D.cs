using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.Lattices.Lattice2D;

namespace DataStructuresLib.Lattices.Lattice1D
{
    public class ColumnSubLattice1D<TValue> :
        ILattice1D<TValue>
    {
        public ILattice2D<TValue> SourceLattice { get; }

        public int ColumnIndex { get; }

        public int Count 
            => SourceLattice.RowsCount;

        public TValue this[int index] 
            => SourceLattice[index, ColumnIndex];


        public ColumnSubLattice1D(ILattice2D<TValue> sourceGrid, int columnIndex)
        {
            SourceLattice = sourceGrid;
            ColumnIndex = columnIndex;
        }


        public TValue[] GetValuesArray()
        {
            return Enumerable
                .Range(0, SourceLattice.RowsCount)
                .Select(i => SourceLattice[i, ColumnIndex])
                .ToArray();
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return Enumerable
                .Range(0, SourceLattice.RowsCount)
                .Select(i => SourceLattice[i, ColumnIndex])
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}