using GeometricAlgebraStructuresLib.Frames;
using GMac.GMacAPI.Binding;
using GMac.GMacAPI.CodeGen;
using GMac.GMacAPI.Target;
using TextComposerLib.Text.Linear;

namespace GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass
{
    internal sealed class ApplyVersorMethodFileGenerator : CodeLibraryMacroCodeFileGenerator
    {
        internal string MethodName { get; }

        internal int InputGrade1 { get; }

        internal int InputGrade2 { get; }

        internal int OutputGrade { get; }


        internal ApplyVersorMethodFileGenerator(CodeLibraryComposer libGen, string baseMacroName, string methodName, int inGrade1, int inGrade2, int outGrade)
            : base(libGen, baseMacroName)
        {
            MethodName = methodName;
            InputGrade1 = inGrade1;
            InputGrade2 = inGrade2;
            OutputGrade = outGrade;
        }


        protected override void InitializeGenerator(GMacMacroCodeComposer macroCodeGen)
        {

        }

        protected override void SetMacroParametersBindings(GMacMacroBinding macroBinding)
        {
            macroBinding.BindMultivectorPartToVariables("result", OutputGrade);

            macroBinding.BindMultivectorPartToVariables("v", InputGrade1);
            macroBinding.BindMultivectorPartToVariables("mv", InputGrade2);
        }

        protected override void SetTargetVariablesNames(GMacTargetVariablesNaming targetNaming)
        {
            BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, "result", OutputGrade, "c");
            
            BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, "v", InputGrade1, "scalars1");
            BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, "mv", InputGrade2, "scalars2");

            BladesLibraryGenerator.SetTargetTempVariablesNames(targetNaming);
        }

        public override void Generate()
        {
            GenerateBladeFileStartCode();

            var computationsText = GenerateComputationsCode();

            var kvSpaceDim = CurrentFrame.KvSpaceDimension(OutputGrade);

            TextComposer.AppendAtNewLine(
                Templates["bilinearproduct"],
                "name", MethodName,
                "num", kvSpaceDim,
                "double", GMacLanguage.ScalarTypeName,
                "computations", computationsText
            );

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
