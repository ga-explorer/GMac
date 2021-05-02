namespace GeometricAlgebraStructuresLib.MotherAlgebra.Basis
{
    internal sealed class GamBasisBlade001 : GamBasisBlade
    {
        public override ulong PositiveId => 0ul;

        public override ulong NegativeId => 0ul;

        public override ulong ZeroId { get; }


        internal GamBasisBlade001(ulong zeroId)
        {
            ZeroId = zeroId;
        }
    }
}