namespace GeometricAlgebraStructuresLib.MotherAlgebra.Basis
{
    internal sealed class GamBasisBlade100 : GamBasisBlade
    {
        public override ulong PositiveId { get; }

        public override ulong NegativeId => 0ul;

        public override ulong ZeroId => 0ul;


        internal GamBasisBlade100(ulong positiveId)
        {
            PositiveId = positiveId;
        }
    }
}