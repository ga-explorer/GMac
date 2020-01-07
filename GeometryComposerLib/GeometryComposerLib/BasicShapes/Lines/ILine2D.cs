using GeometryComposerLib.BasicMath;
using GeometryComposerLib.BasicShapes.Lines.Immutable;

namespace GeometryComposerLib.BasicShapes.Lines
{
    public interface ILine2D : IGeometryElement
    {
        double OriginX { get; }

        double OriginY { get; }


        double DirectionX { get; }

        double DirectionY { get; }


        Line2D ToLine();
    }
}