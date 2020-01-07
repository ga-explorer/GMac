using GeometryComposerLib.BasicMath;
using GeometryComposerLib.BasicShapes.Lines.Immutable;

namespace GeometryComposerLib.BasicShapes.Lines
{
    public interface ILine3D : IGeometryElement
    {
        double OriginX { get; }

        double OriginY { get; }

        double OriginZ { get; }


        double DirectionX { get; }

        double DirectionY { get; }

        double DirectionZ { get; }


        Line3D ToLine();
    }
}