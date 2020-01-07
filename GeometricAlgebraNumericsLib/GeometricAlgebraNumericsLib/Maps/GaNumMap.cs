using GeometricAlgebraNumericsLib.Frames;

namespace GeometricAlgebraNumericsLib.Maps
{
    public abstract class GaNumMap : IGaNumMap
    {
        public abstract int TargetVSpaceDimension { get; }

        public int TargetGaSpaceDimension
            => TargetVSpaceDimension.ToGaSpaceDimension();
    }
}
