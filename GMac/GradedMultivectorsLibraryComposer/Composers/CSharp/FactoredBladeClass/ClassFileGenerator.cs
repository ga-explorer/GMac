using TextComposerLib.Text.Linear;

namespace GradedMultivectorsLibraryComposer.Composers.CSharp.FactoredBladeClass
{
    internal class ClassFileGenerator : CodeLibraryCodeFileGenerator
    {
        internal ClassFileGenerator(CodeLibraryComposer libGen)
            : base(libGen)
        {
        }


        public override void Generate()
        {
            TextComposer.Append(
                Templates["factored_blade"],
                "frame", CurrentFrameName,
                "double", GMacLanguage.ScalarTypeName
                );

            FileComposer.FinalizeText();
        }
    }
}
