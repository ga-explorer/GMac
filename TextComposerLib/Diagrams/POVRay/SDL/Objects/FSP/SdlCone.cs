using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Objects.FSP
{
    public class SdlCone : SdlObject, ISdlFspObject
    {
        public ISdlVectorValue BasePoint { get; set; }

        public ISdlVectorValue CapPoint { get; set; }

        public ISdlScalarValue BaseRadius { get; set; }

        public ISdlScalarValue CapRadius { get; set; }

        public bool Open { get; set; }

    }
}
