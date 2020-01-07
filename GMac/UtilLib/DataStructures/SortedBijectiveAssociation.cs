using System;
using System.Collections.Generic;

namespace UtilLib.DataStructures
{
    public sealed class SortedBijectiveAssociation<T, TU>
    {
        private readonly SortedDictionary<T, TU> _sourceToDestination;

        private readonly SortedDictionary<TU, T> _destinationToSource;


        public int Count { get { return _sourceToDestination.Count; } }

        public IEnumerable<KeyValuePair<T, TU>> SourceToDestinationEnumerable { get { return _sourceToDestination; } }

        public IEnumerable<KeyValuePair<TU, T>> DestinationToSourceEnumerable { get { return _destinationToSource; } }

        public IEnumerable<T> SourceItemsEnumerable { get { return _sourceToDestination.Keys; } }

        public IEnumerable<TU> DestinationItemsEnumerable { get { return _destinationToSource.Keys; } }


        public SortedBijectiveAssociation()
        {
            _sourceToDestination = new SortedDictionary<T, TU>();

            _destinationToSource = new SortedDictionary<TU, T>();
        }

        public SortedBijectiveAssociation(SortedBijectiveAssociation<T, TU> association)
        {
            _sourceToDestination = new SortedDictionary<T, TU>(association._sourceToDestination);

            _destinationToSource = new SortedDictionary<TU, T>(association._destinationToSource);
        }


        public void Clear()
        {
            _sourceToDestination.Clear();
            _destinationToSource.Clear();
        }

        public void AddAssociation(T srcItem, TU dstItem)
        {
            if (_sourceToDestination.ContainsKey(srcItem))
                throw new ArgumentException("A destination item with the same source item is already present");

            if (_destinationToSource.ContainsKey(dstItem))
                throw new ArgumentException("A source item with the same destination item is already present");

            _sourceToDestination.Add(srcItem, dstItem);
            _destinationToSource.Add(dstItem, srcItem);
        }

        public TU GetDestinationItem(T srcItem)
        {
            return _sourceToDestination[srcItem];
        }

        public T GetSourceItem(TU dstItem)
        {
            return _destinationToSource[dstItem];
        }

        public bool TryGetDestinationItem(T srcItem, out TU dstItem)
        {
            return _sourceToDestination.TryGetValue(srcItem, out dstItem);
        }

        public bool TryGetSourceItem(TU dstItem, out T srcItem)
        {
            return _destinationToSource.TryGetValue(dstItem, out srcItem);
        }

        public bool ContainsSourceItem(T srcItem)
        {
            return _sourceToDestination.ContainsKey(srcItem);
        }

        public bool ContainsDestinationItem(TU dstItem)
        {
            return _destinationToSource.ContainsKey(dstItem);
        }

        public bool RemoveSourceItem(T srcItem)
        {
            TU dstItem;

            var flag = _sourceToDestination.TryGetValue(srcItem, out dstItem);

            if (!flag) 
                return false;

            _sourceToDestination.Remove(srcItem);
            _destinationToSource.Remove(dstItem);

            return true;
        }

        public bool RemoveDestinationItem(TU dstItem)
        {
            T srcItem;

            var flag = _destinationToSource.TryGetValue(dstItem, out srcItem);

            if (!flag) 
                return false;

            _sourceToDestination.Remove(srcItem);
            _destinationToSource.Remove(dstItem);

            return true;
        }
    }
}
