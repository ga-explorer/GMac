using TextComposerLib.Diagrams.POVRay.SDL.Finishes;

namespace TextComposerLib.Diagrams.POVRay.SDL.Directives
{
    public sealed class SdlDefaultFinishDirective : SdlDirective
    {
        public ISdlFinish Finish { get; set; }
    }
}
