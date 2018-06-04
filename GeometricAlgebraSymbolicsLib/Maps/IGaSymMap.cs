using GeometricAlgebraSymbolicsLib.Cas.Mathematica;

namespace GeometricAlgebraSymbolicsLib.Maps
{
    public interface IGaSymMap : ISymbolicObject
    {
        int TargetVSpaceDimension { get; }

        int TargetGaSpaceDimension { get; }
    }
}