using GeometricAlgebraStructuresLib.Frames;
using GMac.Engine.API.Binding;
using GMac.Engine.API.CodeGen;
using GMac.Engine.API.Target;
using TextComposerLib.Text.Linear;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass
{
    internal sealed class BilinearProductMethodFileGenerator : CodeLibraryMacroCodeFileGenerator
    {
        internal string MethodName { get; }

        internal int InputGrade1 { get; }

        internal int InputGrade2 { get; }

        internal int OutputGrade { get; }


        internal BilinearProductMethodFileGenerator(CodeLibraryComposer libGen, string baseMacroName, string funcName, int inGrade1, int inGrade2, int outGrade)
            : base(libGen, baseMacroName)
        {
            MethodName = funcName;
            InputGrade1 = inGrade1;
            InputGrade2 = inGrade2;
            OutputGrade = outGrade;
        }


        protected override void InitializeGenerator(GMacMacroCodeComposer macroCodeGen)
        {

        }

        protected override void SetMacroParametersBindings(GMacMacroBinding macroBinding)
        {
            var outputParam = macroBinding.BaseMacro.OutputParameterValueAccess;

            if (outputParam.GMacType.IsValidMultivectorType)
                macroBinding.BindMultivectorPartToVariables(outputParam, OutputGrade);
            else
                macroBinding.BindToVariables(outputParam);

            macroBinding.BindMultivectorPartToVariables("mv1", InputGrade1);
            macroBinding.BindMultivectorPartToVariables("mv2", InputGrade2);
        }

        protected override void SetTargetVariablesNames(GMacTargetVariablesNaming targetNaming)
        {
            var outputParam = targetNaming.BaseMacro.OutputParameterValueAccess;

            if (outputParam.GMacType.IsValidMultivectorType)
                BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, outputParam, OutputGrade, "c");
            else
                targetNaming.SetScalarParameter(outputParam, "c[0]");

            BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, "mv1", InputGrade1, "scalars1");
            BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, "mv2", InputGrade2, "scalars2");

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
