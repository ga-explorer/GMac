namespace GeometricAlgebraNumericsLib.Frames
{
    public interface IGaFrame
    {
        int VSpaceDimension { get; }

        int GaSpaceDimension { get; }

        int MaxBasisBladeId { get; }

        int GradesCount { get; }
    }
}
