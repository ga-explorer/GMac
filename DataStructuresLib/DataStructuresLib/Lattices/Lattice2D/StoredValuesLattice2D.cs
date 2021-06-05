using System.Diagnostics;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Lattices.Lattice2D
{
    public class StoredValuesLattice2D<TValue> :
        ILattice2D<TValue>
    {
        private readonly TValue[,] _valuesArray;

        public int RowsCount 
            => _valuesArray.GetLength(0);

        public int ColumnsCount 
            => _valuesArray.GetLength(1);

        public TValue this[int rowIndex, int columnIndex]
        {
            get => _valuesArray[
                rowIndex.Mod(RowsCount),
                columnIndex.Mod(ColumnsCount)
            ];
            set => _valuesArray[
                rowIndex.Mod(RowsCount),
                columnIndex.Mod(ColumnsCount)
            ] = value;
        }

        public StoredValuesLattice2D(TValue[,] valuesArray)
        {
            Debug.Assert(valuesArray.Length > 0);

            _valuesArray = valuesArray;
        }


        public TValue[,] GetValuesArray()
        {
            return _valuesArray;
        }
    }
}