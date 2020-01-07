using GeometryComposerLib.BasicMath.Tuples;

namespace GeometryComposerLib.BasicMath.Coordinates
{
    public interface IPolarPosition2D : ITuple2D
    {
        double R { get; }

        double Theta { get; }

        double ThetaInDegrees { get; }
    }
}