using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Objects.FSP
{
    public interface ISdlBlobComponent : ISdlFspObject
    {
        ISdlScalarValue Strength { get; set; }
    }
}