namespace GeometricAlgebraNumericsLib.Scalars
{
    public sealed class GaNumScalarTuple2<T> where T : IGaNumScalar
    {
        public static GaNumScalarTuple2<T> operator -(GaNumScalarTuple2<T> s)
        {
            return new GaNumScalarTuple2<T>(s.Item1, s.Item2);
        }


        public T Item1 { get; set; }

        public T Item2 { get; set; }


        public GaNumScalarTuple2(T item1, T item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}
