using System;
using System.Text;

namespace DataStructuresLib.Basic
{
    /// <summary>
    /// This class represents an immutable pair of items of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Quad<T> : IQuad<T>
    {
        public T Item1 { get; }

        public T Item2 { get; }

        public T Item3 { get; }

        public T Item4 { get; }


        public Quad(T item1, T item2, T item3, T item4)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }

        public Quad(IQuad<T> quad)
        {
            Item1 = quad.Item1;
            Item2 = quad.Item2;
            Item3 = quad.Item3;
            Item4 = quad.Item4;
        }

        public Quad(Tuple<T, T, T, T> tuple)
        {
            (Item1, Item2, Item3, Item4) = tuple;
        }


        public Quad<T> GetCopy()
        {
            return new Quad<T>(this);
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
                .Append(", ")
                .Append(Item4)
                .AppendLine(")")
                .ToString();
        }
    }
}