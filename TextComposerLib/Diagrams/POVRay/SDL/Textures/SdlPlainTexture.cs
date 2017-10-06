using System.Collections.Generic;
using TextComposerLib.Diagrams.POVRay.SDL.Finishes;
using TextComposerLib.Diagrams.POVRay.SDL.Pigments;
using TextComposerLib.Diagrams.POVRay.SDL.Transforms;

namespace TextComposerLib.Diagrams.POVRay.SDL.Textures
{
    public class SdlPlainTexture : SdlTexture
    {
        public string TextureIdentifier { get; set; }

        public string PigmentIdentifier { get; set; }

        public string FinishIdentifier { get; set; }

        public ISdlPigment Pigment { get; set; }

        public ISdlFinish Finish { get; set; }

        public List<ISdlTransform> Transforms { get; private set; }


        public SdlPlainTexture()
        {
            Transforms = new List<ISdlTransform>();
        }
    }
}
