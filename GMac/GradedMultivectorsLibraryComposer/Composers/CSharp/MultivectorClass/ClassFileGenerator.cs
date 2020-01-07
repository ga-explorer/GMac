using TextComposerLib.Text.Linear;

namespace GradedMultivectorsLibraryComposer.Composers.CSharp.MultivectorClass
{
    internal class ClassFileGenerator : CodeLibraryCodeFileGenerator 
    {
        internal ClassFileGenerator(CodeLibraryComposer libGen)
            : base(libGen)
        {
        }


        public override void Generate()
        {
            TextComposer.Append(Templates["multivector"],
                "frame", CurrentFrameName,
                "double", GMacLanguage.ScalarTypeName,
                "grades_count", CurrentFrame.VSpaceDimension + 1
                );

            FileComposer.FinalizeText();
        }
    }
}
