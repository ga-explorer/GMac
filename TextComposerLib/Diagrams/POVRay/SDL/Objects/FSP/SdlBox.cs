using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Objects.FSP
{
    public class SdlBox : SdlObject, ISdlFspObject
    {
        public ISdlVectorValue Corner1 { get; set; }

        public ISdlVectorValue Corner2 { get; set; }
    }
}
