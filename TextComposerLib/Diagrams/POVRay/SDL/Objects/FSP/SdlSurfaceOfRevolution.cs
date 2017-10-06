using System.Collections.Generic;
using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Objects.FSP
{
    public class SdlSurfaceOfRevolution : SdlPolynomialObject, ISdlFspObject
    {
        public List<ISdlVectorValue> Points { get; private set; }

        public bool Open { get; set; }


        public SdlSurfaceOfRevolution()
        {
            Points = new List<ISdlVectorValue>();
        }
    }
}
