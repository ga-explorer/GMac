using GMac.Engine.API.CodeGen;
using GMac.Engine.AST.Symbols;
using TextComposerLib.Text.Linear;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer.Composers.CSharp
{
    public abstract class CodeLibraryMacroCodeFileGenerator 
        : GMacMacroCodeFileComposer
    {
        internal AstFrame CurrentFrame { get; }

        internal string CurrentFrameName { get; }

        internal CodeLibraryComposer BladesLibraryGenerator => (CodeLibraryComposer)LibraryComposer;


        internal CodeLibraryMacroCodeFileGenerator(CodeLibraryComposer libGen)
            : base(libGen)
        {
            CurrentFrame = libGen.CurrentFrame;
            CurrentFrameName = libGen.CurrentFrameName;
        }

        internal CodeLibraryMacroCodeFileGenerator(CodeLibraryComposer libGen, string baseMacroName)
            : base(libGen, libGen.CurrentFrame.Macro(baseMacroName))
        {
            CurrentFrame = libGen.CurrentFrame;
            CurrentFrameName = libGen.CurrentFrameName;
        }

        internal CodeLibraryMacroCodeFileGenerator(CodeLibraryComposer libGen, AstMacro baseMacro)
            : base(libGen, baseMacro)
        {
            CurrentFrame = libGen.CurrentFrame;
            CurrentFrameName = libGen.CurrentFrameName;
        }


        internal void GenerateBladeFileStartCode()
        {
            TextComposer.AppendLine(
                Templates["kvector_file_start"].GenerateUsing(CurrentFrameName)
                );

            TextComposer.IncreaseIndentation();
            TextComposer.IncreaseIndentation();
        }

        internal void GenerateBladeFileFinishCode()
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
