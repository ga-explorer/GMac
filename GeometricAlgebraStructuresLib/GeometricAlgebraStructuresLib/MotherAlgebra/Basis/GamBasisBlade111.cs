namespace GeometricAlgebraStructuresLib.MotherAlgebra.Basis
{
    internal sealed class GamBasisBlade111 : GamBasisBlade
    {
        public override ulong PositiveId { get; }

        public override ulong NegativeId { get; }

        public override ulong ZeroId { get; }


        internal GamBasisBlade111(ulong positiveId, ulong negativeId, ulong zeroId)
        {
            PositiveId = positiveId;
            NegativeId = negativeId;
            ZeroId = zeroId;
        }
    }
}