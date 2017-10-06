using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Pigments
{
    public abstract class SdlPigment : ISdlPigment
    {
        public string PigmentIdentifier { get; set; }

        public ISdlColorValue QuickColor { get; set; }
    }
}
