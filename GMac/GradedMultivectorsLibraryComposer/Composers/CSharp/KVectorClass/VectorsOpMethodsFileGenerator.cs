using System;
using GeometricAlgebraNumericsLib.Frames;
using GMac.GMacAPI.Binding;
using GMac.GMacAPI.CodeGen;
using GMac.GMacAPI.Target;
using GMac.GMacAST.Symbols;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass
{
    /// <summary>
    /// This class generates a single macro into a code file using several related bindings and target variable
    /// namings.
    /// </summary>
    internal sealed class VectorsOpMethodsFileGenerator : CodeLibraryMacroCodeFileGenerator
    {
        private int _outGrade;


        internal VectorsOpMethodsFileGenerator(CodeLibraryComposer libGen, AstMacro macroInfo)
            : base(libGen, macroInfo)
        {
        }


        protected override void InitializeGenerator(GMacMacroCodeComposer macroCodeGen)
        {
            
        }

        protected override void SetMacroParametersBindings(GMacMacroBinding macroBinding)
        {
            macroBinding.BindMultivectorPartToVariables("result", _outGrade);

            for (var gradeIdx = 0; gradeIdx < CurrentFrame.VSpaceDimension; gradeIdx++)
                if (gradeIdx < _outGrade)
                    macroBinding.BindToVariables("v" + gradeIdx + ".@G1@");
                else
                    macroBinding.BindScalarToConstant("v" + gradeIdx + ".#E0#", 1);
        }

        protected override void SetTargetVariablesNames(GMacTargetVariablesNaming targetNaming)
        {
            BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, "result", _outGrade, "scalars");

            for (var gradeIdx = 0; gradeIdx < _outGrade; gradeIdx++)
            {
                var vectorName = "vectors[" + gradeIdx + "].C";

                targetNaming.SetMultivectorParameters(
                    "v" + gradeIdx + ".@G1@",
                    id => vectorName + (CurrentFrame.BasisBladeIndex(id) + 1)
                    );
            }

            BladesLibraryGenerator.SetTargetTempVariablesNames(targetNaming);
        }

        private void GenerateVectorsOpFunction()
        {
            //Each time this protected method is called the internal GMacMacroCodeGenerator is initialized,
            //the bindings and target names are set, and the macro code is generated automatically.
            var computationsText = GenerateComputationsCode();

            TextComposer.Append(
                Templates["op_vectors"],
                "frame", CurrentFrameName,
                "double", GMacLanguage.ScalarTypeName,
                "grade", _outGrade,
                "num", CurrentFrame.KvSpaceDimension(_outGrade),
                "computations", computationsText
                );
        }

        public override void Generate()
        {
            GenerateBladeFileStartCode();

            var casesText = new ListTextComposer(Environment.NewLine);

            for (var grade = 2; grade <= CurrentFrame.VSpaceDimension; grade++)
            {
                _outGrade = grade;

                GenerateVectorsOpFunction();

                casesText.Add(
                    Templates["op_vectors_main_case"].GenerateUsing(grade)
                    );
            }

            TextComposer.Append(
                Templates["op_vectors_main"],
                "frame", CurrentFrameName,
                "op_vectors_main_case", casesText
                );

            GenerateBladeFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
