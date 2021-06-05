using System.Collections.Generic;

namespace DataStructuresLib.Lattices.Lattice1D
{
    /// <summary>
    /// A 1D lattice is like a list that can be indexed using any integer,
    /// positive or negative, such that it's a periodic list of values
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface ILattice1D<out TValue> : 
        IReadOnlyList<TValue>
    {
        TValue[] GetValuesArray();
    }
}