namespace GMac.GMacMath.Numeric.Maps
{
    public abstract class GaNumMap : IGaNumMap
    {
        public abstract int TargetVSpaceDimension { get; }

        public int TargetGaSpaceDimension
            => TargetVSpaceDimension.ToGaSpaceDimension();
    }
}
