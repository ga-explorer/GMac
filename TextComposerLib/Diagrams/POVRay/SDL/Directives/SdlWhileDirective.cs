using System.Collections.Generic;
using TextComposerLib.Diagrams.POVRay.SDL.Values;

namespace TextComposerLib.Diagrams.POVRay.SDL.Directives
{
    public sealed class SdlWhileDirective : SdlDirective
    {
        public ISdlBooleanValue LoopCondition { get; set; }

        public List<ISdlStatement> Statements { get; private set; }


        public SdlWhileDirective()
        {
            Statements = new List<ISdlStatement>();
        }
    }
}
