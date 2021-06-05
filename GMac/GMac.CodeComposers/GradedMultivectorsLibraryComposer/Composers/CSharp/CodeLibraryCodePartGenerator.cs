using GMac.Engine.API.CodeGen;
using GMac.Engine.AST.Symbols;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer.Composers.CSharp
{
    internal abstract class CodeLibraryCodePartGenerator : GMacCodePartComposer
    {
        internal AstFrame Frame { get; private set; }

        internal string FrameTargetName { get; private set; }

        internal CodeLibraryComposer BladesLibraryGenerator => (CodeLibraryComposer) LibraryComposer;


        internal CodeLibraryCodePartGenerator(CodeLibraryComposer libGen)
            : base(libGen)
        {
            Frame = libGen.CurrentFrame;
            FrameTargetName = libGen.CurrentFrameName;
        }
    }
}
