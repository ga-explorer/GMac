using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Lattices.Lattice2D;

namespace DataStructuresLib.Lattices.Lattice1D
{
    public class RowSubLattice1D<TValue> :
        ILattice1D<TValue>
    {
        public ILattice2D<TValue> SourceLattice { get; }

        public int RowIndex { get; }

        public int Count 
            => SourceLattice.ColumnsCount;

        public TValue this[int index] 
            => SourceLattice[RowIndex, index];


        public RowSubLattice1D(ILattice2D<TValue> sourceGrid, int rowIndex)
        {
            SourceLattice = sourceGrid;
            RowIndex = rowIndex;
        }


        public TValue[] GetValuesArray()
        {
            return Enumerable
                .Range(0, SourceLattice.ColumnsCount)
                .Select(i => SourceLattice[RowIndex, i])
                .ToArray();
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return Enumerable
                .Range(0, SourceLattice.ColumnsCount)
                .Select(i => SourceLattice[RowIndex, i])
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}