using System.Collections.Generic;
using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Objects.FSP
{
    public class SdlBlob : SdlPolynomialObject, ISdlFspObject
    {
        public List<ISdlBlobComponent> Components { get; private set; }

        public ISdlScalarValue Threshold { get; set; }


        public SdlBlob()
        {
            Components = new List<ISdlBlobComponent>();
        }
    }
}
