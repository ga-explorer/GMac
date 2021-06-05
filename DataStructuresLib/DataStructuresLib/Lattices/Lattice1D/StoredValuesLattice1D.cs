using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Lattices.Lattice1D
{
    public class StoredValuesLattice1D<TValue> :
        ILattice1D<TValue>
    {
        private readonly TValue[] _valuesArray;

        public int Count 
            => _valuesArray.Length;

        public TValue this[int index] 
            => _valuesArray[index.Mod(Count)];


        public StoredValuesLattice1D(TValue[] valuesArray)
        {
            Debug.Assert(valuesArray.Length > 0);

            _valuesArray = valuesArray;
        }


        public TValue[] GetValuesArray()
        {
            return _valuesArray;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return (IEnumerator<TValue>)_valuesArray.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}