using GeometryComposerLib.BasicMath.Tuples;

namespace GeometryComposerLib.BasicMath.Coordinates
{
    public interface ISphericalPosition3D : ITuple3D
    {
        double R { get; }

        double Theta { get; }

        double Phi { get; }

        double ThetaInDegrees { get; }

        double PhiInDegrees { get; }
    }
}