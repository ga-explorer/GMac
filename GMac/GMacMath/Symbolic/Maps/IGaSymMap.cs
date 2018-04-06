using SymbolicInterface.Mathematica;

namespace GMac.GMacMath.Symbolic.Maps
{
    public interface IGaSymMap : ISymbolicObject
    {
        int TargetVSpaceDimension { get; }

        int TargetGaSpaceDimension { get; }
    }
}