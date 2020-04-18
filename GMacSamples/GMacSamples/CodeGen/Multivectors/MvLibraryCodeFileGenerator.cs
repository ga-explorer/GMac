using GMac.GMacAPI.CodeGen;
using GMac.GMacAST.Symbols;

namespace GMacSamples.CodeGen.Multivectors
{
    internal abstract class MvLibraryCodeFileGenerator : GMacCodeFileComposer
    {
        internal AstFrame CurrentFrame { get; }

        internal string CurrentFrameName { get; private set; }

        internal MvLibrary MvLibraryGenerator => (MvLibrary) LibraryComposer;


        internal MvLibraryCodeFileGenerator(MvLibrary libGen)
            : base(libGen)
        {
            CurrentFrame = libGen.CurrentFrame;
            CurrentFrameName = libGen.CurrentFrameName;
        }


        internal GMacMacroCodeComposer CreateMacroCodeGenerator(string macroName)
        {
            return new GMacMacroCodeComposer(LibraryComposer, CurrentFrame.Macro(macroName));
        }
    }
}
