using System;
using System.Collections.Generic;
using System.Linq;
using GMac.GMacAPI.Target;
using GMac.GMacAST;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.ASTConstants;
using TextComposerLib.Code.Languages.GMacDSL;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GMac.GMacAPI.CodeGen.BuiltIn.GMac.GMacFrame
{
    public sealed partial class FrameLibrary : GMacCodeLibraryComposer
    {
        public static string Generate(AstFrame frame)
        {
            var codeGenerator = new FrameLibrary(frame.Root);

            codeGenerator.SelectedSymbols.Add(frame);

            codeGenerator.Generate(
                () =>
                {
                    codeGenerator.CodeFilesComposer.InitalizeFile(frame.AccessName + ".gmac");

                    codeGenerator.GenerateFrameCode(frame);

                    codeGenerator.CodeFilesComposer.UnselectActiveFile();
                }
                );

            return codeGenerator.CodeFilesComposer.FirstFileFinalText;
        }


        private static string ComposeMacroInputs(params string[] inputsInfo)
        {
            if (inputsInfo.Length == 0)
                return "";

            var inputsText = new ListComposer(", ");

            for (var i = 0; i < inputsInfo.Length; i += 2)
                inputsText.Add(inputsInfo[i].Trim() + " : " + inputsInfo[i + 1].Trim());

            return inputsText.ToString();
        }


        public override string Name => "GMacDSL Frame Default Objects Generator";

        public override string Description => "Generate GMacDSL code for default objects for a given frame";

        public GMacDslSyntaxFactory GMacDslSyntaxFactory => GMacLanguage.SyntaxFactory as GMacDslSyntaxFactory;


        public FrameLibrary(AstRoot ast)
            : base(ast, GMacLanguageServer.GMacDsl())
        {
            MacroGenDefaults = new GMacMacroCodeComposerDefaults(this);
        }


        protected override bool InitializeTemplates()
        {
            Templates.Parse(GMacCodeTemplates);

            return true;
        }

        //TODO: You can replace these by calls to GMacDslSyntaxFactory methods
        private void GenerateBinaryScalarMacro(AstFrame frameInfo, string macroName, string exprText)
        {
            ActiveFileTextComposer.Append(
                Templates["binary_macro"], 
                "frame", frameInfo.Name,
                "name", macroName,
                "output_type", TypeNames.Scalar,
                "expr", exprText
                );
        }

        private void GenerateBinaryMacro(AstFrame frameInfo, string macroName, string exprText)
        {
            ActiveFileTextComposer.Append(
                Templates["binary_macro"],
                "frame", frameInfo.Name,
                "name", macroName,
                "output_type", frameInfo.FrameMultivector.Name,
                "expr", exprText
                );
        }

        private void GenerateBinaryScalarMacro(AstFrame frameInfo, string macroName, GMacOpInfo opInfo)
        {
            GenerateBinaryScalarMacro(frameInfo, macroName, "mv1 " + opInfo.OpSymbol + " mv2");
        }

        private void GenerateBinaryMacro(AstFrame frameInfo, string macroName, GMacOpInfo opInfo)
        {
            GenerateBinaryMacro(frameInfo, macroName, "mv1 " + opInfo.OpSymbol + " mv2");
        }

        private void GenerateUnaryScalarMacro(AstFrame frameInfo, string macroName, string exprText)
        {
            ActiveFileTextComposer.Append(
                Templates["unary_macro"],
                "frame", frameInfo.Name,
                "name", macroName,
                "output_type", TypeNames.Scalar,
                "expr", exprText
                );
        }

        private void GenerateUnaryMacro(AstFrame frameInfo, string macroName, string exprText)
        {
            ActiveFileTextComposer.Append(
                Templates["unary_macro"],
                "frame", frameInfo.Name,
                "name", macroName,
                "output_type", frameInfo.FrameMultivector.Name,
                "expr", exprText
                );
        }

        private void GenerateUnaryScalarMacro(AstFrame frameInfo, string macroName, GMacOpInfo opInfo)
        {
            GenerateUnaryScalarMacro(frameInfo, macroName, opInfo.OpName + "(mv)");
        }

        private void GenerateUnaryMacro(AstFrame frameInfo, string macroName, GMacOpInfo opInfo)
        {
            GenerateUnaryMacro(frameInfo, macroName, opInfo.OpName + "(mv)");
        }

        private void GenerateMacro(AstFrame frameInfo, string macroName, string inputsText, string resultExprText)
        {
            ActiveFileTextComposer.Append(
                Templates["general_macro"],
                "frame", frameInfo.Name,
                "name", macroName,
                "inputs", inputsText,
                "output_type", frameInfo.FrameMultivector.Name,
                "expr", resultExprText
                );
        }

        private void GenerateMacro(AstFrame frameInfo, string macroName, string inputsText, object commandsText, string resultExprText)
        {
            ActiveFileTextComposer.Append(
                Templates["macro"],
                "frame", frameInfo.Name,
                "name", macroName,
                "inputs", inputsText,
                "output_type", frameInfo.FrameMultivector.Name,
                "commands", commandsText,
                "expr", resultExprText
                );
        }

        private void GenerateMacro(AstFrame frameInfo, string macroName, string inputsText, string outputType, object commandsText, string resultExprText)
        {
            ActiveFileTextComposer.Append(
                Templates["macro"],
                "frame", frameInfo.Name,
                "name", macroName,
                "inputs", inputsText,
                "output_type", outputType,
                "commands", commandsText,
                "expr", resultExprText
                );
        }


        private void GenerateStructures(AstFrame frameInfo)
        {
            var membersText = new ListComposer("," + Environment.NewLine);

            for (var id = 0; id <= frameInfo.MaxBasisBladeId; id++)
                membersText.Add(
                    Templates["structure_member"],
                    "name", "ImageE" + id,
                    "type", frameInfo.FrameMultivector.Name
                    );

            ActiveFileTextComposer.Append(
                Templates["structure"], 
                "frame", frameInfo.Name,
                "name", DefaultStructure.LinearTransform,
                "members", membersText
                );

            membersText.Clear();

            for (var index = 1; index <= frameInfo.VSpaceDimension; index++)
                membersText.Add(
                    Templates["structure_member"],
                    "name", "ImageV" + index,
                    "type", frameInfo.FrameMultivector.Name
                    );

            ActiveFileTextComposer.Append(
                Templates["structure"],
                "frame", frameInfo.Name,
                "name", DefaultStructure.Outermorphism,
                "members", membersText
                );
        }

        private void GenerateConstants(AstFrame frameInfo)
        {
            for (var id = 0; id <= frameInfo.MaxBasisBladeId; id++)
                ActiveFileTextComposer.Append(
                    Templates["constant"],
                    "frame", frameInfo.Name,
                    "name", "E" + id,
                    "value", "Multivector(#E" + id + "# = 1)"
                    );
        }

        private void GenerateApplyVersorMacro(AstFrame frameInfo, string macroName, string exprText)
        {
            GenerateMacro(
                frameInfo,
                macroName,
                ComposeMacroInputs(
                    "v", frameInfo.FrameMultivector.Name,
                    "mv", frameInfo.FrameMultivector.Name
                    ),
                frameInfo.FrameMultivector.Name,
                "",
                exprText
                );
        }

        private void GenerateFrameCode(AstFrame frameInfo)
        {
            ActiveFileTextComposer.AppendLine("namespace " + GetSymbolTargetName(frameInfo.ParentNamespace));
            ActiveFileTextComposer.AppendLine();

            GenerateStructures(frameInfo);

            GenerateConstants(frameInfo);

            GenerateMetricUnaryMacros(frameInfo);

            GenerateEuclideanUnaryMacros(frameInfo);
            
            GenerateMetricBinaryMacros(frameInfo);

            GenerateEuclideanBinaryMacros(frameInfo);

            GenerateLinearTransformMacros(frameInfo);

            GenerateOutermorphismMacros(frameInfo);

            GenerateMetricVersorMacros(frameInfo);

            GenerateEuclideanVersorMacros(frameInfo);
        }


        public override GMacCodeLibraryComposer CreateEmptyGenerator()
        {
            return new FrameLibrary(Root);
        }

        public override IEnumerable<AstSymbol> GetBaseSymbolsList()
        {
            return Root.Frames;
        }

        protected override bool VerifyReadyToGenerate()
        {
            return SelectedSymbols.All(s => s.IsValidFrame);
        }

        protected override void InitializeOtherComponents()
        {

        }

        protected override void FinalizeOtherComponents()
        {

        }

        protected override string GetSymbolTargetName(AstSymbol symbol)
        {
            return symbol.AccessName;
        }

        protected override void ComposeTextFiles()
        {
            var framesList = SelectedSymbols.Cast<AstFrame>();

            CodeFilesComposer.DownFolder("GMacDSL");

            foreach (var frame in framesList)
            {
                CodeFilesComposer.InitalizeFile("frame_" + GetSymbolTargetName(frame) + ".gmac");

                GenerateFrameCode(frame);

                CodeFilesComposer.UnselectActiveFile();
            }

            CodeFilesComposer.UpFolder();
        }
    }
}
