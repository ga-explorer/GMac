using GraphicsComposerLib.SVG.Elements;

namespace GraphicsComposerLib.SVG.Compositions
{
    public interface ISvgGeometryComposer
    {
        ISvgGeometryComposerIDs ElementsIDs { get; }

        ISvgGeometryComposerStyler ElementsStyler { get; }

        SvgElement Compose(bool applyStyles);
    }
}