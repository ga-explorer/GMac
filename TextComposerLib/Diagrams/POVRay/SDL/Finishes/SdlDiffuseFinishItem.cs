using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Finishes
{
    public sealed class SdlDiffuseFinishItem : ISdlFinishItem
    {
        public bool Albedo { get; set; }

        public ISdlScalarValue Amount { get; set; }
    }
}
