using GraphicsComposerLib.SVG.Values;

namespace GraphicsComposerLib.SVG.Paths.Segments
{
    public interface ISvgPathSegment
    {
        string SegmentText(SvgValueLengthUnit unit);
    }
}