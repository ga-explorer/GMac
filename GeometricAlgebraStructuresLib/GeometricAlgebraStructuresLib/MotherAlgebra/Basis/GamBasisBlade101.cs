namespace GeometricAlgebraStructuresLib.MotherAlgebra.Basis
{
    internal sealed class GamBasisBlade101 : GamBasisBlade
    {
        public override ulong PositiveId { get; }

        public override ulong NegativeId => 0ul;

        public override ulong ZeroId { get; }


        internal GamBasisBlade101(ulong positiveId, ulong zeroId)
        {
            PositiveId = positiveId;
            ZeroId = zeroId;
        }
    }
}