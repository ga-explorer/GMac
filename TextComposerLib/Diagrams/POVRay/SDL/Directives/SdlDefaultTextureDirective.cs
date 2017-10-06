using TextComposerLib.Diagrams.POVRay.SDL.Textures;

namespace TextComposerLib.Diagrams.POVRay.SDL.Directives
{
    public sealed class SdlDefaultTextureDirective : SdlDirective
    {
        public ISdlTexture Texture { get; set; }
    }
}
