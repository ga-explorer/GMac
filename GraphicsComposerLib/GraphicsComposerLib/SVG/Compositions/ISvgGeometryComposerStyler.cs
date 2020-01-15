using GraphicsComposerLib.SVG.Elements;

namespace GraphicsComposerLib.SVG.Compositions
{
    public interface ISvgGeometryComposerStyler
    {
        SvgElement ComposedElement { get; }

        ISvgGeometryComposerIDs ComposedElementsIDs { get; }

        SvgElement ApplyStyles();
    }
}