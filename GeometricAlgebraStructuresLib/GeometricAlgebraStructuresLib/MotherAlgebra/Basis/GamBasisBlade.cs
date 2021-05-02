namespace GeometricAlgebraStructuresLib.MotherAlgebra.Basis
{
    public abstract class GamBasisBlade
    {
        public static GamBasisBlade ScalarBasisBlade { get; } = new GamBasisBlade000();

        public static GamBasisBlade Create(ulong positiveId, ulong negativeId, ulong zeroId)
        {
            if (positiveId == 0ul)
            {
                if (negativeId == 0ul)
                    return (zeroId == 0ul) 
                        ? ScalarBasisBlade 
                        : new GamBasisBlade001(zeroId);

                return (zeroId == 0ul) 
                    ? (GamBasisBlade)new GamBasisBlade010(negativeId)
                    : new GamBasisBlade011(negativeId, zeroId);
            }

            if (negativeId == 0ul)
                return (zeroId == 0ul) 
                    ? (GamBasisBlade)new GamBasisBlade100(positiveId) 
                    : new GamBasisBlade101(positiveId, zeroId);

            return (zeroId == 0ul) 
                ? (GamBasisBlade)new GamBasisBlade110(positiveId, negativeId)
                : new GamBasisBlade111(positiveId, negativeId, zeroId);
        }


        public abstract ulong PositiveId { get; }

        public abstract ulong NegativeId { get; }

        public abstract ulong ZeroId { get; }


        public override string ToString()
        {
            return base.ToString();
        }
    }
}