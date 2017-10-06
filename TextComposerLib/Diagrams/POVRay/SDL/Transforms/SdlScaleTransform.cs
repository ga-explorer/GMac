using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Transforms
{
    public sealed class SdlScaleTransform : SdlTransform
    {
        public ISdlVectorValue FactorVector { get; private set; }


        internal SdlScaleTransform(ISdlVectorValue factorVector)
        {
            FactorVector = factorVector;
        }
    }
}
