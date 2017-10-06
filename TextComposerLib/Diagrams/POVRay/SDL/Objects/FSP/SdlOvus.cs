using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Objects.FSP
{
    public class SdlOvus : SdlObject, ISdlFspObject
    {
        public ISdlScalarValue BottomRadius { get; set; }

        public ISdlScalarValue TopRadius { get; set; }
    }
}
