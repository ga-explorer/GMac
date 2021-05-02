namespace GeometricAlgebraStructuresLib.MotherAlgebra.Basis
{
    internal sealed class GamBasisBlade011 : GamBasisBlade
    {
        public override ulong PositiveId => 0ul;

        public override ulong NegativeId { get; }

        public override ulong ZeroId { get; }


        internal GamBasisBlade011(ulong negativeId, ulong zeroId)
        {
            NegativeId = negativeId;
            ZeroId = zeroId;
        }
    }
}