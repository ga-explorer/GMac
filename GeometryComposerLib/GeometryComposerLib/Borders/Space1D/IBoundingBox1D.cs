using GeometryComposerLib.Borders.Space1D.Immutable;
using GeometryComposerLib.Borders.Space1D.Mutable;

namespace GeometryComposerLib.Borders.Space1D
{
    public interface IBoundingBox1D
    {
        double MinValue { get; }

        double MaxValue { get; }

        BoundingBox1D GetBoundingBox();

        MutableBoundingBox1D GetMutableBoundingBox();
    }
}