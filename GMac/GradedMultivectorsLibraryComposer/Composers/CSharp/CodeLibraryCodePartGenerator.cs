using GMac.GMacAPI.CodeGen;
using GMac.GMacAST.Symbols;

namespace GradedMultivectorsLibraryComposer.Composers.CSharp
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
