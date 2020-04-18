using System;
using System.Linq;
using GMac.GMacAPI.Binding;
using GMac.GMacAPI.CodeGen;
using GMac.GMacAPI.Target;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacUtils;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Parametric;
using TextComposerLib.Text.Structured;

namespace GMacSamples.CodeGen.Multivectors
{
    internal sealed class DerivedMvClassCalcFileGenerator : MvLibraryMacroCodeFileGenerator
    {
        #region Templates

        private const string BilinearTemplateText = @"
public #result_type# #op_name#(#mv_class_name# mv)
{
    #code#
}
";

        #endregion

        internal static void Generate(MvLibrary libGen, MvClassData derivedClassData, MvClassData calcClassData)
        {
            var generator = new DerivedMvClassCalcFileGenerator(libGen, derivedClassData, calcClassData);

            generator.Generate();
        }


        internal MvClassData ClassData { get; }

        internal MvClassData CalcClassData { get; }


        private DerivedMvClassCalcFileGenerator(MvLibrary libGen, MvClassData classData, MvClassData calcClassData)
            : base(libGen)
        {
            ClassData = classData;
            CalcClassData = calcClassData;
        }


        protected override void InitializeGenerator(GMacMacroCodeComposer macroCodeGen)
        {
            macroCodeGen.ActionBeforeGenerateSingleComputation =
                (composer, codeInfo) => 
                    codeInfo.EnableCodeGeneration = 
                        codeInfo.ComputedVariable.IsTemp || codeInfo.ComputedVariable.IsNonZero;
        }

        protected override void SetMacroParametersBindings(GMacMacroBinding macroBinding)
        {
            macroBinding.BindToVariables("result");
            macroBinding.BindToTreePattern("mv1", ClassData.ClassBinding);
            macroBinding.BindToTreePattern("mv2", CalcClassData.ClassBinding);
        }

        protected override void SetTargetVariablesNames(GMacTargetVariablesNaming targetNaming)
        {
            //Set names of output parameter components
            var outputParam = targetNaming.BaseMacro.OutputParameterValueAccess;

            if (outputParam.GMacType.IsValidMultivectorType)
                targetNaming.SetMultivectorParameters(outputParam, b => "result." + b.GradeIndexName);
            else
                targetNaming.SetScalarParameter(outputParam, "result");

            //Set names for input parameters components
            targetNaming.SetMultivectorParameters("mv1", b => b.GradeIndexName);

            targetNaming.SetMultivectorParameters("mv2", b => "mv." + b.GradeIndexName);

            //Set names for temp variables
            MvLibraryGenerator.SetTargetTempVariablesNames(targetNaming);
        }


        private string GenerateProductMethodCode(string macroName, out string resultClassName)
        {
            SetBaseMacro(CurrentFrame.Macro(macroName));

            var computationsText = GenerateComputationsCode();

            //The result is zero
            if (MacroCodeGenerator.CodeBlock.NonZeroOutputVariables.Any() == false)
            {
                //The result is the zero multivector
                if (BaseMacro.OutputType.IsValidMultivectorType)
                {
                    resultClassName = MvLibraryGenerator.MultivectorClassesData[0].ClassName;
                    return "return Zero;";
                }

                //The result is the scalar zero
                resultClassName = GMacLanguage.ScalarTypeName;
                return "return " + GMacLanguage.ScalarZero + ";";
            }

            //The result is not zero
            string resultDeclarationText;

            if (BaseMacro.OutputType.IsValidMultivectorType)
            {
                var grades =
                    MacroCodeGenerator
                        .CodeBlock
                        .NonZeroOutputVariables
                        .Select(v => v.ValueAccess.GetBasisBlade().Grade)
                        .Distinct();

                var id = grades.Sum(grade => 1 << grade);

                resultClassName = MvLibraryGenerator.MultivectorClassesData[id].ClassName;

                resultDeclarationText = "var result = new " + resultClassName + "();";
            }
            else
            {
                resultClassName = GMacLanguage.ScalarTypeName;

                resultDeclarationText = "var result = " + GMacLanguage.ScalarZero + ";";
            }

            var composer = new LinearComposer();

            composer
                .AppendLineAtNewLine(resultDeclarationText)
                .AppendLine()
                .AppendLineAtNewLine(computationsText)
                .AppendAtNewLine("return result;");

            return composer.ToString();
        }

        private string GenerateProductMethod(string macroName)
        {
            string resultClassName;

            var codeText = GenerateProductMethodCode(macroName, out resultClassName);

            var template = new ParametricComposer("#", "#", BilinearTemplateText);

            var composer = new LinearComposer();

            composer.Append(
                template,
                "result_type", resultClassName,
                "op_name", BaseMacro.Name,
                "mv_class_name", CalcClassData.ClassName,
                "code", codeText
                );

            return composer.ToString();
        }

        private string GenerateIsEqualMethodCode()
        {
            //The two classes are zero multivector classes
            if (ClassData.ClassId == 0 && CalcClassData.ClassId == 0) return "return true;";

            var idCommon = ClassData.ClassId & CalcClassData.ClassId;

            var idDiff1 = ClassData.ClassId & ~CalcClassData.ClassId;

            var idDiff2 = ~ClassData.ClassId & CalcClassData.ClassId;

            var composer = new ListComposer(" || " + Environment.NewLine)
            {
                FinalPrefix = "return !(" + Environment.NewLine,
                FinalSuffix = Environment.NewLine + ");",
                ActiveItemPrefix = "    "
            };

            var basisBlades = ClassData.Frame.BasisBladesOfGrades(idDiff1.PatternToPositions());

            foreach (var basisBlade in basisBlades)
                composer.Add(
                    String.Format("{0} <= -Epsilon || {0} >= Epsilon", basisBlade.GradeIndexName)
                    );

            basisBlades = ClassData.Frame.BasisBladesOfGrades(idDiff2.PatternToPositions());

            foreach (var basisBlade in basisBlades)
                composer.Add(
                    String.Format("{0} <= -Epsilon || {0} >= Epsilon", "mv." + basisBlade.GradeIndexName)
                    );

            basisBlades = ClassData.Frame.BasisBladesOfGrades(idCommon.PatternToPositions());

            foreach (var basisBlade in basisBlades)
            {
                var term = "(" + basisBlade.GradeIndexName + " - mv." + basisBlade.GradeIndexName + ")";

                composer.Add(
                    String.Format("{0} <= -Epsilon || {0} >= Epsilon", term)
                    );
            }

            return composer.ToString();
        }

        private string GenerateIsEqualMethod()
        {
            var codeText = GenerateIsEqualMethodCode();

            var template = new ParametricComposer("#", "#", BilinearTemplateText);

            var composer = new LinearComposer();

            composer.Append(
                template,
                "result_type", "bool",
                "op_name", "IsEqual",
                "mv_class_name", CalcClassData.ClassName,
                "code", codeText
                );

            return composer.ToString();
        }

        private string GenerateAddMethodCode(MvClassData resultClassData)
        {
            //The two classes are zero multivector classes
            if (resultClassData.ClassId == 0) return "return Zero;";

            var idCommon = ClassData.ClassId & CalcClassData.ClassId;

            var idDiff1 = ClassData.ClassId & ~CalcClassData.ClassId;

            var idDiff2 = ~ClassData.ClassId & CalcClassData.ClassId;

            var composer = new ListComposer("," + Environment.NewLine)
            {
                FinalPrefix = "return new " + resultClassData.ClassName + "()" + Environment.NewLine + "{" + Environment.NewLine,
                FinalSuffix = Environment.NewLine + "};",
                ActiveItemPrefix = "    "
            };

            var basisBlades = ClassData.Frame.BasisBladesOfGrades(idDiff1.PatternToPositions());

            foreach (var basisBlade in basisBlades)
                composer.Add(
                    $"{basisBlade.GradeIndexName} = {basisBlade.GradeIndexName}"
                    );

            basisBlades = ClassData.Frame.BasisBladesOfGrades(idDiff2.PatternToPositions());

            foreach (var basisBlade in basisBlades)
                composer.Add(
                    $"{basisBlade.GradeIndexName} = {"mv." + basisBlade.GradeIndexName}"
                    );

            basisBlades = ClassData.Frame.BasisBladesOfGrades(idCommon.PatternToPositions());

            foreach (var basisBlade in basisBlades)
            {
                var term = basisBlade.GradeIndexName + " + mv." + basisBlade.GradeIndexName;

                composer.Add(
                    $"{basisBlade.GradeIndexName} = {term}"
                    );
            }

            return composer.ToString();
        }

        private string GenerateAddMethod()
        {
            var resultId = ClassData.ClassId | CalcClassData.ClassId;

            var resultClassData = MvLibraryGenerator.MultivectorClassesData[resultId];

            var codeText = GenerateAddMethodCode(resultClassData);

            var template = new ParametricComposer("#", "#", BilinearTemplateText);

            var composer = new LinearComposer();

            composer.Append(
                template,
                "result_type", resultClassData.ClassName,
                "op_name", "Add",
                "mv_class_name", CalcClassData.ClassName,
                "code", codeText
                );

            return composer.ToString();
        }

        private string GenerateSubtractMethodCode(MvClassData resultClassData)
        {
            //The two classes are zero multivector classes
            if (resultClassData.ClassId == 0) return "return Zero;";

            var idCommon = ClassData.ClassId & CalcClassData.ClassId;

            var idDiff1 = ClassData.ClassId & ~CalcClassData.ClassId;

            var idDiff2 = ~ClassData.ClassId & CalcClassData.ClassId;

            var composer = new ListComposer("," + Environment.NewLine)
            {
                FinalPrefix = "return new " + resultClassData.ClassName + "()" + Environment.NewLine + "{" + Environment.NewLine,
                FinalSuffix = Environment.NewLine + "};",
                ActiveItemPrefix = "    "
            };

            var basisBlades = ClassData.Frame.BasisBladesOfGrades(idDiff1.PatternToPositions());

            foreach (var basisBlade in basisBlades)
                composer.Add(
                    $"{basisBlade.GradeIndexName} = {basisBlade.GradeIndexName}"
                    );

            basisBlades = ClassData.Frame.BasisBladesOfGrades(idDiff2.PatternToPositions());

            foreach (var basisBlade in basisBlades)
                composer.Add(
                    $"{basisBlade.GradeIndexName} = {"-mv." + basisBlade.GradeIndexName}"
                    );

            basisBlades = ClassData.Frame.BasisBladesOfGrades(idCommon.PatternToPositions());

            foreach (var basisBlade in basisBlades)
            {
                var term = basisBlade.GradeIndexName + " - mv." + basisBlade.GradeIndexName;

                composer.Add(
                    $"{basisBlade.GradeIndexName} = {term}"
                    );
            }

            return composer.ToString();
        }

        private string GenerateSubtractMethod()
        {
            var resultId = ClassData.ClassId | CalcClassData.ClassId;

            var resultClassData = MvLibraryGenerator.MultivectorClassesData[resultId];

            var codeText = GenerateSubtractMethodCode(resultClassData);

            var template = new ParametricComposer("#", "#", BilinearTemplateText);

            var composer = new LinearComposer();

            composer.Append(
                template,
                "result_type", resultClassData.ClassName,
                "op_name", "Subtract",
                "mv_class_name", CalcClassData.ClassName,
                "code", codeText
                );

            return composer.ToString();
        }

        private string GenerateClassCode()
        {
            var macroNames = new[]
            {
                DefaultMacro.EuclideanBinary.OuterProduct,
                DefaultMacro.MetricBinary.GeometricProduct,
                DefaultMacro.MetricBinary.LeftContractionProduct,
                DefaultMacro.MetricBinary.RightContractionProduct,
                DefaultMacro.MetricBinary.ScalarProduct
            };

            var composer = new LinearComposer();

            if (MvLibraryGenerator.MacroGenDefaults.AllowGenerateMacroCode)
            {
                foreach (var macroName in macroNames)
                    composer.AppendAtNewLine(
                        GenerateProductMethod(macroName)
                        );
            }

            composer.AppendAtNewLine(
                GenerateAddMethod()
                );

            composer.AppendAtNewLine(
                GenerateSubtractMethod()
                );

            composer.AppendAtNewLine(
                GenerateIsEqualMethod()
                );

            return composer.ToString();
        }


        public override void Generate()
        {
            var classCodeText =
                MvLibraryGenerator.MacroGenDefaults.AllowGenerateMacroCode
                ? GenerateClassCode() 
                : "";

            TextComposer.Append(
                Templates["mv_class_file"],
                "frame", CurrentFrameName,
                "mv_class_name", ClassData.ClassName,
                "mv_class_code", classCodeText
                );

            FileComposer.FinalizeText();
        }
    }
}
