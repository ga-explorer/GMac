namespace GeometricAlgebraStructuresLib.MotherAlgebra.Basis
{
    internal sealed class GamBasisBlade010 : GamBasisBlade
    {
        public override ulong PositiveId => 0ul;

        public override ulong NegativeId { get; }

        public override ulong ZeroId => 0ul;


        internal GamBasisBlade010(ulong negativeId)
        {
            NegativeId = negativeId;
        }
    }
}