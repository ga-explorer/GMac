using System;
using System.Linq;
using CodeComposerLib.SyntaxTree;
using GeometricAlgebraStructuresLib.Frames;
using GMac.GMacAPI.CodeBlock;
using GMac.GMacAPI.CodeGen;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.ASTConstants;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Parametric;
using TextComposerLib.Text.Structured;

namespace GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass
{
    internal sealed class MiscMethodsFileGenerator : CodeLibraryCodeFileGenerator
    {
        internal MiscMethodsFileGenerator(CodeLibraryComposer libGen)
            : base(libGen)
        {
        }

        //private GMacInfoMacro AddEuclideanDualGMacMacro()
        //{
        //    var codeText =
        //        Templates["edual_macro"].GenerateUsing(CurrentFrameName);

        //    var gmacMacro =
        //        _tempSymbolsCompiler.CompileMacro(
        //            codeText,
        //            _currentFrame.AssociatedFrame.ChildScope
        //            );

        //    return new GMacInfoMacro(gmacMacro);
        //}

        private void GenerateEuclideanDualFunction(int inGrade, AstMacro macroInfo)
        {
            var macroGenerator = LibraryComposer.CreateMacroCodeGenerator(macroInfo);

            var outGrade = CurrentFrame.VSpaceDimension - inGrade;

            macroGenerator.ActionSetMacroParametersBindings =
                macroBinding =>
                {
                    macroBinding.BindMultivectorPartToVariables("result", outGrade);
                    macroBinding.BindMultivectorPartToVariables("mv", inGrade);
                };

            macroGenerator.ActionSetTargetVariablesNames =
                targetNaming =>
                {
                    BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, "result", outGrade, "c");
                    BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, "mv", inGrade, "scalars");
                    BladesLibraryGenerator.SetTargetTempVariablesNames(targetNaming);
                };

            //Generate code from macro binding
            var computationsText = macroGenerator.Generate();

            TextComposer.Append(
                Templates["edual"],
                "double", GMacLanguage.ScalarTypeName,
                "grade", inGrade,
                "num", CurrentFrame.KvSpaceDimension(inGrade),
                "computations", computationsText
                );
        }

        private void GenerateMiscFunctions(int kvSpaceDim)
        {
            //This code can be replaced using ListTextBuilderCollection and ParametricTextBuilderCollection
            //objects. See GenerateMainMiscFunctions() in this file for an example
            var addCasesTemplate = Templates["add_case"];
            var subtCasesTemplate = Templates["subt_case"];
            var timesCasesTemplate = Templates["times_case"];

            var addCasesText = new ListTextComposer("," + Environment.NewLine);
            var subtCasesText = new ListTextComposer("," + Environment.NewLine);
            var timesCasesText = new ListTextComposer("," + Environment.NewLine);

            for (var index = 0; index < kvSpaceDim; index++)
            {
                addCasesText.Add(addCasesTemplate, "index", index);
                subtCasesText.Add(subtCasesTemplate, "index", index);
                timesCasesText.Add(timesCasesTemplate, "index", index);
            }


            var miscFuncsTemplate = Templates["misc"];

            TextComposer.Append(miscFuncsTemplate,
                "double", GMacLanguage.ScalarTypeName,
                "num", kvSpaceDim,
                "addcases", addCasesText,
                "subtcases", subtCasesText,
                "timescases", timesCasesText
                );
        }


        private void TestSelfDpGradeFunctionComputationCondition(SteSyntaxElementsList textBuilder, GMacComputationCodeInfo compInfo)
        {
            if (compInfo.RhsExpressionCode.IsZero)
            {
                compInfo.EnableCodeGeneration = false;
                //return;
            }

            ////Prevent generation of processing code if output grade equals 0 because this is the default grade
            ////returned by the function
            //var valueAccess = ((TlOutputVariable)compInfo.ComputedVariable).AssociatedValueAccess;

            //var id = ((ValueAccessStepByKey<int>)valueAccess.LastAccessStep).AccessKey;

            //var grade = GaUtils.ID_To_Grade(id);

            //if (grade == 0)
            //    compInfo.EnableCodeGeneration = false;
        }

        private static void AddSelfDpGradeFunctionComputationCondition(SteSyntaxElementsList textBuilder, GMacComputationCodeInfo compInfo)
        {
            if (compInfo.ComputedVariable.IsOutput == false || compInfo.EnableCodeGeneration == false)
                return;

            var basisBlade = 
                ((GMacCbOutputVariable) compInfo.ComputedVariable).ValueAccess.GetBasisBlade();

            var grade = basisBlade.Grade;

            textBuilder.AddFixedCode("if (c <= -Epsilon || c >= Epsilon) return " + grade + ";");
            textBuilder.AddEmptyLines(2);
        }

        private void GenerateSelfDpGradeFunction(int inGrade, AstMacro selfEgpMacroInfo)
        {
            var macroGenerator = LibraryComposer.CreateMacroCodeGenerator(selfEgpMacroInfo);

            var outGradesList =
                CurrentFrame
                .GradesOfEGp(inGrade, inGrade)
                .Where(grade => grade > 0)
                .OrderByDescending(g => g);

            macroGenerator.ActionSetMacroParametersBindings =
                macroBinding =>
                {
                    foreach (var outGrade in outGradesList)
                        macroBinding.BindMultivectorPartToVariables("result", outGrade);

                    macroBinding.BindMultivectorPartToVariables("mv", inGrade);
                };

            macroGenerator.ActionSetTargetVariablesNames =
                targetNaming =>
                {
                    foreach (var outGrade in outGradesList)
                        targetNaming.SetMultivectorParameters("result", outGrade, id => "c");

                    BladesLibraryGenerator.SetBasisBladeToArrayNaming(targetNaming, "mv", inGrade, "scalars");

                    BladesLibraryGenerator.SetTargetTempVariablesNames(targetNaming);
                };

            macroGenerator.ActionBeforeGenerateSingleComputation =
                TestSelfDpGradeFunctionComputationCondition;

            macroGenerator.ActionAfterGenerateSingleComputation =
                AddSelfDpGradeFunctionComputationCondition;

            macroGenerator.MacroBinding.FixOutputComputationsOrder = true;

            //Generate code from macro binding
            var computationsText = macroGenerator.Generate();

            TextComposer.Append(
                Templates["self_dp_grade"],
                "grade", inGrade,
                "double", GMacLanguage.ScalarTypeName,
                "computations", computationsText
            );
        }

        private void GenerateMainSelfDpGradeFunction()
        {
            if (CurrentFrame.VSpaceDimension <= 3)
            {
                TextComposer.Append("public int SelfDPGrade() { return 0; }");

                return;
            }

            var selfDpGradeCasesText = new ListTextComposer(Environment.NewLine);

            for (var grade = 2; grade < CurrentFrame.VSpaceDimension - 1; grade++)
                selfDpGradeCasesText.Add(
                    Templates["main_self_dp_grade_case"].GenerateUsing(grade)
                );

            TextComposer.Append(
                Templates["main_self_dp_grade"].GenerateText(
                    "frame", CurrentFrameName,
                    "main_self_dp_grade_cases", selfDpGradeCasesText.ToString()
                )
            );
        }

        private void GenerateMainMiscFunctions()
        {
            var miscCasesTemplates =
                Templates.SubCollection(
                    "main_add_case",
                    "main_subt_case",
                    "main_times_case",
                    "main_divide_case",
                    "main_inverse_case",
                    "main_edual_case"
                    );

            var miscCasesText = new ListComposerCollection(
                    "main_add_case",
                    "main_subt_case",
                    "main_times_case",
                    "main_divide_case",
                    "main_inverse_case",
                    "main_edual_case"
                    );

            miscCasesText.SetSeparator(Environment.NewLine);

            foreach (var grade in CurrentFrame.Grades())
            {
                miscCasesText.AddTextItems(miscCasesTemplates,
                    "frame", CurrentFrameName,
                    "grade", grade,
                    "num", CurrentFrame.KvSpaceDimension(grade),
                    "sign", grade.GradeHasNegativeReverse() ? "-" : "",
                    "invgrade", CurrentFrame.VSpaceDimension - grade
                    );
            }

            var mainFuncsTemplate = Templates["misc_main"];

            mainFuncsTemplate.SetParametersValues(miscCasesText);

            TextComposer.Append(
                mainFuncsTemplate,
                "frame", CurrentFrameName,
                "double", GMacLanguage.ScalarTypeName,
                "norm2_opname", DefaultMacro.MetricUnary.NormSquared,
                "emag2_opname", DefaultMacro.EuclideanUnary.MagnitudeSquared
                );

            GenerateMainSelfDpGradeFunction();
        }

        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            var kvSpaceDimList =
                Enumerable
                .Range(0, CurrentFrame.VSpaceDimension)
                .Select(grade => CurrentFrame.KvSpaceDimension(grade))
                .Distinct();

            foreach (var kvSpaceDim in kvSpaceDimList)
                GenerateMiscFunctions(kvSpaceDim);

            var selfEgpMacroInfo = CurrentFrame.Macro(DefaultMacro.EuclideanUnary.SelfGeometricProduct);

            var edualMacroInfo = CurrentFrame.Macro(DefaultMacro.EuclideanUnary.Dual);

            foreach (var inGrade in CurrentFrame.Grades())
                GenerateEuclideanDualFunction(inGrade, edualMacroInfo);

            for (var inGrade = 2; inGrade < CurrentFrame.VSpaceDimension - 1; inGrade++)
                GenerateSelfDpGradeFunction(inGrade, selfEgpMacroInfo);

            GenerateMainMiscFunctions();

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
