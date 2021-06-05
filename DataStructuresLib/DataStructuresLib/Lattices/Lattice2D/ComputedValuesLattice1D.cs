using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using DataStructuresLib.Lattices.Lattice1D;

namespace DataStructuresLib.Lattices.Lattice2D
{
    public class ComputedValuesLattice1D<TValue> :
        ILattice1D<TValue>
    {
        public Func<int, TValue> ComputingFunc { get; }

        public int Count { get; }

        public TValue this[int index] 
            => ComputingFunc(index.Mod(Count));
        

        public ComputedValuesLattice1D(int count, Func<int, TValue> computingFunc)
        {
            Count = count;
            ComputingFunc = computingFunc;
        }


        public TValue[] GetValuesArray()
        {
            return Enumerable
                .Range(0, Count)
                .Select(ComputingFunc)
                .ToArray();
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return Enumerable
                .Range(0, Count)
                .Select(ComputingFunc)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}