namespace GeometricAlgebraStructuresLib.MotherAlgebra.Basis
{
    internal sealed class GamBasisBlade110 : GamBasisBlade
    {
        public override ulong PositiveId { get; }

        public override ulong NegativeId { get; }

        public override ulong ZeroId => 0ul;


        internal GamBasisBlade110(ulong positiveId, ulong negativeId)
        {
            PositiveId = positiveId;
            NegativeId = negativeId;
        }
    }
}