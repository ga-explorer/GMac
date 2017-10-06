using System;
using System.Collections.Generic;

namespace UtilLib
{
    /// <summary>
    /// Represents a list of non-overlapping ranges of type T where each range is associated
    /// with some object of type U. An object can then be rtrieved using a single value of type
    /// T by searching the ranges for the given value and returning the associated object.
    /// If you need to use binary search, let AssumeUnsorted = false, sort the ranges from
    /// smaller to larger, and use more than 100 ranges
    /// </summary>
    /// <typeparam name="TR">Type of range objects</typeparam>
    /// <typeparam name="TU">Type of range-associated objects</typeparam>
    public sealed class RangeList<TR, TU> where TR : IComparable<TR>
    {
        private readonly List<TR> _minLimits = new List<TR>();
        private readonly List<TR> _maxLimits = new List<TR>();
        private readonly List<TU> _values = new List<TU>();
        public bool AssumeUnsorted;

        public RangeList() 
        {
            
        }

        public RangeList(bool assumeUnsorted)
        {
            AssumeUnsorted = assumeUnsorted;
        }

        public int Length { get { return _minLimits.Count; } }

        public void Add(TR min, TR max, TU value)
        {
            if (min.CompareTo(max) <= 0)
            {
                _minLimits.Add(min);
                _maxLimits.Add(max);
            }
            else
            {
                _minLimits.Add(max);
                _maxLimits.Add(min);
            }

            _values.Add(value);
        }

        private TU Retrieve_BinarySearch(TR value)
        {

            var index = _maxLimits.BinarySearch(value);

            if (index >= 0)
                return _values[index];

            index = ~index;

            if (_minLimits[index].CompareTo(value) <= 0)
                return _values[index];

            throw new IndexOutOfRangeException();
        }

        private TU Retrieve_LinearSearch(TR value)
        {
            for (var i = 0; i < _minLimits.Count; i++)
            {
                if (_minLimits[i].CompareTo(value) <= 0 && _maxLimits[i].CompareTo(value) >= 0)
                    return _values[i];
            }

            throw new IndexOutOfRangeException();
        }

        public TU this[TR value]
        {
            get
            {
                var minValue = _minLimits[0];
                var maxValue = _maxLimits[Length - 1];

                if (minValue.CompareTo(value) > 0 || maxValue.CompareTo(value) < 0)
                    throw new IndexOutOfRangeException();

                if (AssumeUnsorted == false && Length > 100)
                    return Retrieve_BinarySearch(value);
                
                return Retrieve_LinearSearch(value);
            }
        }

        private TU RetrieveRange_BinarySearch(TR value, out TR min, out TR max, out int index)
        {
            index = _maxLimits.BinarySearch(value);

            if (index >= 0)
            {
                min = _minLimits[index];
                max = _maxLimits[index];
                return _values[index];
            }

            index = ~index;

            if (_minLimits[index].CompareTo(value) > 0) 
                throw new IndexOutOfRangeException();

            min = _minLimits[index];
            max = _maxLimits[index];
            return _values[index];
        }

        private TU RetrieveRange_LinearSearch(TR value, out TR min, out TR max, out int index)
        {
            for (var i = 0; i < _minLimits.Count - 1; i++)
            {
                if (_minLimits[i].CompareTo(value) > 0 || _maxLimits[i].CompareTo(value) < 0) 
                    continue;

                min = _minLimits[i];
                max = _maxLimits[i];
                index = i;
                return _values[i];
            }

            throw new IndexOutOfRangeException();
        }

        public TU GetRangeContaining(TR value, out TR min, out TR max, out int index)
        {
            var minValue = _minLimits[0];
            var maxValue = _maxLimits[Length - 1];
            
            if (minValue.CompareTo(value) > 0 || maxValue.CompareTo(value) < 0)
                throw new IndexOutOfRangeException();

            if (AssumeUnsorted == false && Length > 100)
                return RetrieveRange_BinarySearch(value, out min, out max, out index);
            
            return RetrieveRange_LinearSearch(value, out min, out max, out index);
        }
    }
}
