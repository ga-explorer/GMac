using System;
using System.Text;

namespace DataStructuresLib.Basic
{
    /// <summary>
    /// This class represents an immutable triplet of items of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Triplet<T> : ITriplet<T>
    {
        public T Item1 { get; }

        public T Item2 { get; }

        public T Item3 { get; }


        public Triplet(T item1)
        {
            Item1 = item1;
        }

        public Triplet(T item1, T item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public Triplet(T item1, T item2, T item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public Triplet(ITriplet<T> triplet)
        {
            Item1 = triplet.Item1;
            Item2 = triplet.Item2;
            Item3 = triplet.Item3;
        }

        public Triplet(Tuple<T, T, T> tuple)
        {
            (Item1, Item2, Item3) = tuple;
        }


        public Triplet<T> GetCopy()
        {
            return new Triplet<T>(Item1, Item2, Item3);
        }

        /// <summary>
        /// Returns a new pair containing (this.Item2, nextItem)
        /// </summary>
        /// <param name="nextItem"></param>
        /// <returns></returns>
        public Triplet<T> NextTriplet(T nextItem)
        {
            return new Triplet<T>(Item2, Item3, nextItem);
        }

        /// <summary>
        /// Returns a new pair containing (previousItem, this.Item1)
        /// </summary>
        /// <param name="previousItem"></param>
        /// <returns></returns>
        public Triplet<T> PreviousTriplet(T previousItem)
        {
            return new Triplet<T>(previousItem, Item1, Item2);
        }

        public Triplet<T> RotateForward()
        {
            return new Triplet<T>(Item3, Item1, Item2);
        }

        public Triplet<T> RotateBackward()
        {
            return new Triplet<T>(Item2, Item3, Item1);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine("(")
                .Append(Item1)
                .Append(", ")
                .Append(Item2)
                .Append(", ")
                .Append(Item3)
                .AppendLine(")")
                .ToString();
        }
    }
}