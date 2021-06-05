using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Lattices.Lattice1D
{
    public class ConstantValueLattice1D<TValue> :
        ILattice1D<TValue> 
    {
        public TValue Value { get; }

        public int Count { get; }

        public TValue this[int index] 
            => Value;


        public ConstantValueLattice1D(int count, TValue value)
        {
            Count = count;
            Value = value;
        }


        public TValue[] GetValuesArray()
        {
            return Enumerable.Repeat(Value, Count).ToArray();
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return Enumerable.Repeat(Value, Count).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}