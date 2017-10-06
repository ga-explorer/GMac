using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Pigments
{
    public sealed class SdlSolidColorPigment : SdlPigment
    {
        public ISdlColorValue Color { get; set; }


        internal SdlSolidColorPigment(ISdlColorValue color)
        {
            Color = color;
        }
    }
}
