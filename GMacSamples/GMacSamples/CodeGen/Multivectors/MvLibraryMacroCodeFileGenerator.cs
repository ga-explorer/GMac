using GMac.GMacAPI.CodeGen;
using GMac.GMacAST.Symbols;

namespace GMacSamples.CodeGen.Multivectors
{
    internal abstract class MvLibraryMacroCodeFileGenerator : GMacMacroCodeFileComposer
    {
        internal AstFrame CurrentFrame { get; private set; }

        internal string CurrentFrameName { get; private set; }

        internal MvLibrary MvLibraryGenerator => (MvLibrary) LibraryComposer;


        internal MvLibraryMacroCodeFileGenerator(MvLibrary libGen)
            : base(libGen)
        {
            CurrentFrame = libGen.CurrentFrame;
            CurrentFrameName = libGen.CurrentFrameName;
        }

        internal MvLibraryMacroCodeFileGenerator(MvLibrary libGen, string baseMacroName)
            : base(libGen, libGen.CurrentFrame.Macro(baseMacroName))
        {
            CurrentFrame = libGen.CurrentFrame;
            CurrentFrameName = libGen.CurrentFrameName;
        }

        internal MvLibraryMacroCodeFileGenerator(MvLibrary libGen, AstMacro baseMacro)
            : base(libGen, baseMacro)
        {
            CurrentFrame = libGen.CurrentFrame;
            CurrentFrameName = libGen.CurrentFrameName;
        }
    }
}
