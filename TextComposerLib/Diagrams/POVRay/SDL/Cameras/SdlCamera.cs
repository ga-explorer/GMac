using System.Collections.Generic;
using TextComposerLib.Diagrams.POVRay.SDL.Transforms;

namespace TextComposerLib.Diagrams.POVRay.SDL.Cameras
{
    public abstract class SdlCamera : ISdlCamera
    {
        public List<ISdlTransform> Transforms { get; }



        protected SdlCamera()
        {
            Transforms = new List<ISdlTransform>();
        }
    }
}
