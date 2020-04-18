namespace GeometricAlgebraNumericsLib.Products
{
    public sealed class GaNumProductTerm<T>
    {
        public int IdsXor { get; }

        public int IdsAnd { get; }

        public T Value { get; }


        internal GaNumProductTerm(int idsXor)
        {
            IdsXor = idsXor;
            IdsAnd = 0;
            Value = default;
        }

        internal GaNumProductTerm(int idsXor, int idsAnd)
        {
            IdsXor = idsXor;
            IdsAnd = idsAnd;
            Value = default;
        }

        internal GaNumProductTerm(int idsXor, int idsAnd, T value)
        {
            IdsXor = idsXor;
            IdsAnd = idsAnd;
            Value = value;
        }
    }
}