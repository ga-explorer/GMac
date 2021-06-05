using System;
using GeometricAlgebraStructuresLib.Frames;
using GMac.Engine.API.Binding;
using GMac.Engine.API.CodeGen;
using GMac.Engine.API.Target;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass
{
    internal sealed class SelfEgpMethodsFileGenerator 
        : CodeLibraryMacroCodeFileGenerator
    {
        private int _inGrade;

        private int _outGrade;

        internal string OperatorName { get; }


        internal SelfEgpMethodsFileGenerator(CodeLibraryComposer libGen, string opName)
            : base(libGen, opName)
        {
            OperatorName = opName;
        }


        protected override void InitializeGenerator(GMacMacroCodeComposer macroCodeGen)
        {
            
        }

        protected override void SetMacroParametersBindings(GMacMacroBinding macroBinding)
        {
            var outputValueAccess = macroBinding.BaseMacro.OutputParameterValueAccess;

            if (outputValueAccess.GMacType.IsValidMultivectorType)
                macroBinding.BindMultivectorPartToVariables(outputValueAccess, _outGrade);
            else
                macroBinding.BindToVariables(outputValueAccess);

            macroBinding.BindMultivectorPartToVariables("mv", _inGrade);
        }

        protected override void SetTargetVariablesNames(GMacTargetVariablesNaming targetNaming)
        {
            var outputParam = targetNaming.BaseMacro.OutputParameterValueAccess;

            if (outputParam.GMacType.IsValidMultivectorType)
                BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, outputParam, _outGrade, "c");
            else
                targetNaming.SetScalarParameter(outputParam, "c[0]");

            BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, "mv", _inGrade, "scalars");

            BladesLibraryGenerator.SetTargetTempVariablesNames(targetNaming);
        }

        private void GenerateMethod(string funcName, int inputGrade, int outputGrade)
        {
            _inGrade = inputGrade;
            _outGrade = outputGrade;

            var computationsText = GenerateComputationsCode();

            var kvSpaceDim = CurrentFrame.KvSpaceDimension(_outGrade);

            TextComposer.AppendAtNewLine(
                Templates["self_bilinearproduct"],
                "name", funcName,
                "num", kvSpaceDim,
                "double", GMacLanguage.ScalarTypeName,
                "computations", computationsText
            );
        }

        private void GenerateMethods(int inputGrade)
        {
            var gpCaseText = new ListTextComposer("," + Environment.NewLine);

            var gradesList = CurrentFrame.GradesOfEGp(inputGrade, inputGrade);

            foreach (var outputGrade in gradesList)
            {
                var funcName = 
                    BladesLibraryGenerator
                    .GetBinaryFunctionName(OperatorName, inputGrade, inputGrade, outputGrade);

                GenerateMethod(
                    funcName,
                    inputGrade,
                    outputGrade
                    );

                gpCaseText.Add(Templates["selfgp_case"],
                    "frame", CurrentFrameName,
                    "grade", outputGrade,
                    "name", funcName
                );
            }

            TextComposer.AppendAtNewLine(
                Templates["selfgp"],
                "frame", CurrentFrameName,
                "name", BladesLibraryGenerator.GetBinaryFunctionName(OperatorName, inputGrade, inputGrade),
                "double", GMacLanguage.ScalarTypeName,
                "selfgp_case", gpCaseText
            );
        }

        private void GenerateMainMethod()
        {
            var casesTemplate = Templates["selfgp_main_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade in CurrentFrame.Grades())
            {
                casesText.Add(
                    casesTemplate,
                    "name", BladesLibraryGenerator.GetBinaryFunctionName(OperatorName, grade, grade),
                    "grade", grade,
                    "frame", CurrentFrameName
                );
            }

            TextComposer.AppendAtNewLine(
                Templates["selfgp_main"],
                "name", OperatorName,
                "frame", CurrentFrameName,
                "cases", casesText
            );
        }

        public override void Generate()
        {
            GenerateBladeFileStartCode();

            foreach (var grade in CurrentFrame.Grades())
                GenerateMethods(grade);

            GenerateMainMethod();

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
