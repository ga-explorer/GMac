using GMac.Engine.API.CodeGen;
using GMac.Engine.AST.Symbols;
using TextComposerLib.Text.Linear;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer.Composers.CSharp
{
    public abstract class CodeLibraryCodeFileGenerator : GMacCodeFileComposer
    {
        internal AstFrame CurrentFrame { get; }

        internal string CurrentFrameName { get; }

        internal CodeLibraryComposer BladesLibraryGenerator => (CodeLibraryComposer) LibraryComposer;


        internal CodeLibraryCodeFileGenerator(CodeLibraryComposer libGen)
            : base(libGen)
        {
            CurrentFrame = libGen.CurrentFrame;
            CurrentFrameName = libGen.CurrentFrameName;
        }


        internal GMacMacroCodeComposer CreateMacroCodeGenerator(string macroName)
        {
            return new GMacMacroCodeComposer(LibraryComposer, CurrentFrame.Macro(macroName));
        }

        internal void GenerateKVectorFileStartCode()
        {
            TextComposer.AppendLine(
                Templates["kvector_file_start"].GenerateUsing(CurrentFrameName)
                );

            TextComposer.IncreaseIndentation();
            TextComposer.IncreaseIndentation();
        }

        internal void GenerateKVectorFileFinishCode()
        {
            TextComposer
                .DecreaseIndentation()
                .AppendLineAtNewLine("}")
                .DecreaseIndentation()
                .AppendLineAtNewLine("}");
        }

        internal void GenerateOutermorphismFileStartCode()
        {
            TextComposer.AppendLine(
                Templates["om_file_start"],
                "frame", CurrentFrameName,
                "grade", CurrentFrame.VSpaceDimension
                );

            TextComposer.IncreaseIndentation();
            TextComposer.IncreaseIndentation();
        }

        internal void GenerateOutermorphismFileFinishCode()
        {
            TextComposer
                .DecreaseIndentation()
                .AppendLineAtNewLine("}")
                .DecreaseIndentation()
                .AppendLineAtNewLine("}");
        }

        internal void GenerateBeginRegion(string regionText)
        {
            TextComposer.AppendLineAtNewLine(@"#region " + regionText);
        }

        internal void GenerateEndRegion()
        {
            TextComposer.AppendLineAtNewLine(@"#endregion");
        }
    }
}
