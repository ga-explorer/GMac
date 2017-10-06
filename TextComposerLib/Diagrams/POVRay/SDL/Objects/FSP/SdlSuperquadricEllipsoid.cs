using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Objects.FSP
{
    public class SdlSuperquadricEllipsoid : SdlObject, ISdlFspObject
    {
        public ISdlScalarValue EastWestExponent { get; set; }

        public ISdlScalarValue NorthSouthExponent { get; set; }
    }
}
