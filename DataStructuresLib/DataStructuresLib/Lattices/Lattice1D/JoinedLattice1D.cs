using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Lattices.Lattice1D
{
    public class JoinedLattice1D<TValue> :
        ILattice1D<TValue>
    {
        private readonly List<ILattice1D<TValue>> _laticesList
            = new List<ILattice1D<TValue>>();


        public int Count 
            => _laticesList.Sum(lattice => lattice.Count);

        public TValue this[int index]
        {
            get
            {
                index = index.Mod(Count);
                foreach (var sourceLattice in _laticesList)
                {
                    if (index < sourceLattice.Count)
                        return sourceLattice[index];

                    index -= sourceLattice.Count;
                }

                //This should never happen
                throw new InvalidOperationException();
            }
        }

        
        public TValue[] GetValuesArray()
        {
            return _laticesList
                .SelectMany(lattice => lattice)
                .ToArray();
        }

        public JoinedLattice1D<TValue> AppendLattice(ILattice1D<TValue> sourceLattice)
        {
            _laticesList.Add(sourceLattice);

            return this;
        }

        public JoinedLattice1D<TValue> PrependLattice(ILattice1D<TValue> sourceLattice)
        {
            _laticesList.Insert(0, sourceLattice);

            return this;
        }

        public JoinedLattice1D<TValue> PrependLattice(ILattice1D<TValue> sourceLattice, int index)
        {
            _laticesList.Insert(index, sourceLattice);

            return this;
        }

        public JoinedLattice1D<TValue> RemoveLattice(int index)
        {
            _laticesList.RemoveAt(index);

            return this;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _laticesList
                .SelectMany(lattice => lattice)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}