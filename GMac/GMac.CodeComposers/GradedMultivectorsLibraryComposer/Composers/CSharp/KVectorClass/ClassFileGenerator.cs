using GMac.Engine.Compiler.Semantic.ASTConstants;
using TextComposerLib.Text.Linear;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass
{
    /// <summary>
    /// Generate the main code file for the blade class
    /// </summary>
    internal sealed class ClassFileGenerator : CodeLibraryCodeFileGenerator
    {
        internal ClassFileGenerator(CodeLibraryComposer libGen)
            : base(libGen)
        {
        }


        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            TextComposer.Append(
                Templates["kvector"],
                "frame", CurrentFrameName,
                "double", GMacLanguage.ScalarTypeName,
                "norm2_opname", DefaultMacro.MetricUnary.NormSquared
                );

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
