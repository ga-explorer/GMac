using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Objects.FSP
{
    public class SdlTorus : SdlPolynomialObject, ISdlFspObject
    {
        public ISdlScalarValue MajorRadius { get; set; }

        public ISdlScalarValue MinorRadius { get; set; }
    }
}
