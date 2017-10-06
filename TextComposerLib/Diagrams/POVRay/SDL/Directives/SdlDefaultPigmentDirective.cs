using TextComposerLib.Diagrams.POVRay.SDL.Pigments;

namespace TextComposerLib.Diagrams.POVRay.SDL.Directives
{
    public sealed class SdlDefaultPigmentDirective : SdlDirective
    {
        public ISdlPigment Pigment { get; set; }
    }
}
