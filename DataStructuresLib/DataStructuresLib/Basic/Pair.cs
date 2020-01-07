using System;
using System.Text;

namespace DataStructuresLib.Basic
{
    /// <summary>
    /// This class represents an immutable pair of items of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Pair<T> : IPair<T>
    {
        public T Item1 { get; }

        public T Item2 { get; }


        public Pair(T firstItem)
        {
            Item1 = firstItem;
            Item2 = firstItem;
        }

        public Pair(T firstItem, T secondItem)
        {
            Item1 = firstItem;
            Item2 = secondItem;
        }

        public Pair(IPair<T> pair)
        {
            Item1 = pair.Item1;
            Item2 = pair.Item2;
        }

        public Pair(Tuple<T, T> tuple)
        {
            (Item1, Item2) = tuple;
        }


        public Pair<T> GetCopy()
        {
            return new Pair<T>(Item1, Item2);
        }

        /// <summary>
        /// Returns a new pair containing (this.Item2, nextItem)
        /// </summary>
        /// <param name="nextItem"></param>
        /// <returns></returns>
        public Pair<T> NextPair(T nextItem)
        {
            return new Pair<T>(Item2, nextItem);
        }

        /// <summary>
        /// Returns a new pair containing (previousItem, this.Item1)
        /// </summary>
        /// <param name="previousItem"></param>
        /// <returns></returns>
        public Pair<T> PreviousPair(T previousItem)
        {
            return new Pair<T>(previousItem, Item1);
        }

        /// <summary>
        /// Returns a new pair containing (this.Item2, this.Item1)
        /// </summary>
        /// <returns></returns>
        public Pair<T> SwapItems()
        {
            return new Pair<T>(Item2, Item1);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine("(")
                .Append(Item1)
                .Append(", ")
                .Append(Item2)
                .AppendLine(")")
                .ToString();
        }
    }
}
