using TextComposerLib.Text.Linear;

namespace GradedMultivectorsLibraryComposer.Composers.CSharp.FrameUtilsClass
{
    internal class ClassFileGenerator : CodeLibraryCodeFileGenerator 
    {
        internal ClassFileGenerator(CodeLibraryComposer libGen)
            : base(libGen)
        {
        }


        public override void Generate()
        {
            TextComposer.Append(Templates["frame_utils"],
                "frame", CurrentFrameName,
                "vspacedim", CurrentFrame.VSpaceDimension
            );

            FileComposer.FinalizeText();
        }
    }
}
