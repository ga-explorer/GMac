using TextComposerLib.Files;
using TextComposerLib.Text.Linear;

namespace GMac.GMacAPI.CodeGen
{
    /// <summary>
    /// This abstract class can be used to implement a sub-process of code file generation using the main
    /// code library generator composnents
    /// </summary>
    public abstract class GMacCodeFileComposer : GMacCodePartComposer
    {
        public TextFileComposer FileComposer { get; }

        public LinearComposer TextComposer => FileComposer.TextComposer;


        protected GMacCodeFileComposer(GMacCodeLibraryComposer libGen)
            : base(libGen)
        {
            FileComposer = LibraryComposer.ActiveFileComposer;
        }


        public abstract void Generate();
    }
}
