using TextComposerLib.Diagrams.POVRay.SDL.Objects;

namespace TextComposerLib.Diagrams.POVRay.SDL.Modifiers
{
    public sealed class SdlBoundClipModifier : ISdlObjectModifier
    {
        public ISdlObject BoundingObject { get; private set; }

        public ISdlObject ClippingObject { get; private set; }


        internal SdlBoundClipModifier(ISdlObject boundingObject, ISdlObject clippingObject)
        {
            BoundingObject = boundingObject;
            ClippingObject = clippingObject;
        }
    }
}
