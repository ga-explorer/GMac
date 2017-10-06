using System.Collections.Generic;
using TextComposerLib.Diagrams.POVRay.SDL.Transforms;

namespace TextComposerLib.Diagrams.POVRay.SDL
{
    public interface ISdlTransformable : ISdlElement
    {
        List<ISdlTransform> Transforms { get; }
    }
}