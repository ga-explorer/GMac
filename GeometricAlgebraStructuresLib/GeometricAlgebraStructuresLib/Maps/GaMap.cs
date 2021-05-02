using GeometricAlgebraStructuresLib.Frames;

namespace GeometricAlgebraStructuresLib.Maps
{
    public abstract class GaMap : IGaMap
    {
        public abstract int TargetVSpaceDimension { get; }

        public ulong TargetGaSpaceDimension
            => TargetVSpaceDimension.ToGaSpaceDimension();
    }
}
