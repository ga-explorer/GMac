using System.Collections.Generic;
using TextComposerLib.Diagrams.POVRay.SDL.Modifiers;

namespace TextComposerLib.Diagrams.POVRay.SDL.Objects
{
    public abstract class SdlObject : ISdlObject
    {
        public List<ISdlObjectModifier> Modifiers { get; private set; }


        protected SdlObject()
        {
            Modifiers = new List<ISdlObjectModifier>();
        }
    }
}
