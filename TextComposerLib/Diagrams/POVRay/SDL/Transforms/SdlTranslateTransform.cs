using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Transforms
{
    public sealed class SdlTranslateTransform : SdlTransform
    {
        public ISdlVectorValue Direction { get; private set; }


        internal SdlTranslateTransform(ISdlVectorValue direction)
        {
            Direction = direction;
        }
    }
}
