using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Objects.FSP
{
    public class SdlSphere : SdlObject, ISdlFspObject
    {
        public ISdlVectorValue Center { get; set; }

        public ISdlScalarValue Radius { get; set; }
    }
}
