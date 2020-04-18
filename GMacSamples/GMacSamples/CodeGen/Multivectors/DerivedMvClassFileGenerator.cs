using System;
using GMac.GMacCompiler.Semantic.ASTConstants;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Parametric;
using TextComposerLib.Text.Structured;

namespace GMacSamples.CodeGen.Multivectors
{
    internal sealed class DerivedMvClassFileGenerator : MvLibraryCodeFileGenerator
    {
        #region Templates

        private static readonly ParametricComposer DeclareCoefsTemplate = 
            new ParametricComposer("#", "#", @"public #double# #coef# { get; set; }");

        private static readonly ParametricComposer AbstractCoefsTemplate = 
            new ParametricComposer("#", "#", @"public override #double# #coef# { get { return #value#; } }");

        private static readonly ParametricComposer AbstractMethodCaseTemplate = 
            new ParametricComposer("#", "#", @"case #id#: return #op_name#((#class_name#)mv);");

        private static readonly ParametricComposer MainCodeTemplate = new ParametricComposer("#", "#",
@"
public #mv_class_name#()
{
    ClassId = #mv_class_id#;
}


public override int ActiveClassId
{
    get
    {
        #activeclassid_code#
    }
}

public override #double# Norm2
{
    get
    {
        #norm2_code#
    }
}

public override bool IsZero
{
    get
    {
        #iszero_code#
    }
}

public override bool IsKVector
{
    get
    {
        #iskvector_code#
    }
}

public override bool IsBlade
{
    get
    {
        #isblade_code#
    }
}

public override bool IsScalar
{
    get
    {
        #isscalar_code#
    }
}

public override bool IsVector
{
    get
    {
        #isvector_code#
    }
}

public override bool IsPseudoVector
{
    get
    {
        #ispseudovector_code#
    }
}

public override bool IsPseudoScalar
{
    get
    {
        #ispseudoscalar_code#
    }
}

public override bool IsTerm
{
    get
    {
        #isterm_code#
    }
}

public override bool IsEven
{
    get
    {
        #iseven_code#
    }
}

public override bool IsOdd
{
    get
    {
        #isodd_code#
    }
}

public override bool IsEqual(#frame#Multivector mv)
{
    switch (mv.ClassId)
    {
        #isequal_cases#
        default: throw new InvalidOperationException();
    }
}

public override #frame#Multivector Simplify()
{
    #simplify_code#
}

public override #frame#Multivector OP(#frame#Multivector mv)
{
    switch (mv.ClassId)
    {
        #op_cases#
        default: throw new InvalidOperationException();
    }
}

public override #frame#Multivector GP(#frame#Multivector mv)
{
    switch (mv.ClassId)
    {
        #gp_cases#
        default: throw new InvalidOperationException();
    }
}

public override #frame#Multivector LCP(#frame#Multivector mv)
{
    switch (mv.ClassId)
    {
        #lcp_cases#
        default: throw new InvalidOperationException();
    }
}

public override #frame#Multivector RCP(#frame#Multivector mv)
{
    switch (mv.ClassId)
    {
        #rcp_cases#
        default: throw new InvalidOperationException();
    }
}

public override #double# SP(#frame#Multivector mv)
{
    switch (mv.ClassId)
    {
        #sp_cases#
        default: throw new InvalidOperationException();
    }
}

public override #frame#Multivector Add(#frame#Multivector mv)
{
    switch (mv.ClassId)
    {
        #add_cases#
        default: throw new InvalidOperationException();
    }
}

public override #frame#Multivector Subtract(#frame#Multivector mv)
{
    switch (mv.ClassId)
    {
        #subtract_cases#
        default: throw new InvalidOperationException();
    }
}
");

        #endregion

        internal static void Generate(MvLibrary libGen, MvClassData classData)
        {
            var generator = new DerivedMvClassFileGenerator(libGen, classData);

            generator.Generate();
        }

        internal MvClassData ClassData { get; }


        private DerivedMvClassFileGenerator(MvLibrary libGen, MvClassData classData)
            : base(libGen)
        {
            ClassData = classData;
        }


        private string GenerateDeclareCoefsText()
        {
            var s = new ListComposer(Environment.NewLine);

            DeclareCoefsTemplate["double"] = GMacLanguage.ScalarTypeName;

            foreach (var basisBlade in ClassData.ClassBasisBlades)
                s.Add(DeclareCoefsTemplate, "coef", basisBlade.GradeIndexName);

            return s.Add().ToString();
        }

        private string GenerateAbstractCoefsText()
        {
            var s = new ListComposer(Environment.NewLine);

            AbstractCoefsTemplate["double"] = GMacLanguage.ScalarTypeName;

            foreach (var basisBlade in ClassData.Frame.BasisBlades())
            {
                var id = basisBlade.BasisBladeId;

                var value = ClassData.ClassBinding.ContainsVariable(id) ? basisBlade.GradeIndexName : "0.0D";

                s.Add(AbstractCoefsTemplate, "coef", ("Coef" + id), "value", value);
            }
                
            return s.Add().ToString();
        }

        private string GenerateAbstractMethodCases(string opName)
        {
            var composer = new ListComposer(Environment.NewLine);

            AbstractMethodCaseTemplate["op_name"] = opName;

            foreach (var classData in MvLibraryGenerator.MultivectorClassesData.Values)
                composer.Add(
                    AbstractMethodCaseTemplate, 
                    "id", classData.ClassId, 
                    "class_name", classData.ClassName
                    );

            return composer.ToString();
        }

        private string GenerateIsZeroCode()
        {
            if (ClassData.ClassId == 0) return "return true;";

            var composer = new ListComposer(" || " + Environment.NewLine)
            {
                FinalPrefix = "return !(" + Environment.NewLine,
                FinalSuffix = Environment.NewLine + ");",
                ActiveItemPrefix = "    "
            };

            foreach (var basisBlade in ClassData.ClassBasisBlades)
            {
                composer.Add(
                    String.Format("{0} <= -Epsilon || {0} >= Epsilon", basisBlade.GradeIndexName)
                    );
            }

            return composer.ToString();
        }

        private string GenerateNorm2Code()
        {
            if (ClassData.ClassId == 0) 
                return "return " + GMacLanguage.ScalarZero + ";";

            //var macroGen = new GMacMacroCodeGenerator(LibraryComposer, CurrentFrame.Macro(DefaultMacro.MetricUnary.NormSquared));
            var macroGen = CreateMacroCodeGenerator(DefaultMacro.MetricUnary.NormSquared);

            macroGen.ActionSetMacroParametersBindings =
                macroBinding =>
                {
                    macroBinding.BindToVariables(macroBinding.BaseMacro.OutputParameterValueAccess);

                    macroBinding.BindMultivectorPartToVariables("mv", ClassData.ClassBasisBladeIds);
                };

            macroGen.ActionSetTargetVariablesNames =
                targetNaming =>
                {
                    //This line is actually not needed because we  will override the macro output assignment code
                    //using the ActionBeforeGenerateSingleComputation member delegate
                    targetNaming.SetScalarParameter(targetNaming.BaseMacro.OutputParameterValueAccess, "result");

                    targetNaming.SetMultivectorParameters("mv", b => b.GradeIndexName);

                    //targetNaming.SetTempVariables(index => "tempVar" + index.ToString("X4") + "");
                    MvLibraryGenerator.SetTargetTempVariablesNames(targetNaming);
                };

            macroGen.ActionBeforeGenerateSingleComputation =
                (composer, codeInfo) =>
                {
                    if (!codeInfo.ComputedVariable.IsOutput) return;

                    composer
                        .AddEmptyLine()
                        .Add(SyntaxFactory.ReturnValue(codeInfo.RhsExpressionCode))
                        .AddEmptyLine();

                    codeInfo.EnableCodeGeneration = false;
                };

            return macroGen.Generate();
        }

        private string GenerateClassCode()
        {
            var composer = new LinearComposer();

            composer.AppendLineAtNewLine(GenerateDeclareCoefsText());

            composer.AppendLineAtNewLine(GenerateAbstractCoefsText());

            composer.AppendAtNewLine(
                MainCodeTemplate, 
                "mv_class_name", ClassData.ClassName, 
                "mv_class_id", ClassData.ClassId,
                "frame", CurrentFrameName,
                "double", GMacLanguage.ScalarTypeName, 
                "iszero_code", GenerateIsZeroCode(),
                "norm2_code", GenerateNorm2Code(),
                "isequal_cases", GenerateAbstractMethodCases("IsEqual"),
                "op_cases", GenerateAbstractMethodCases("OP"),
                "gp_cases", GenerateAbstractMethodCases("GP"),
                "lcp_cases", GenerateAbstractMethodCases("LCP"),
                "rcp_cases", GenerateAbstractMethodCases("RCP"),
                "sp_cases", GenerateAbstractMethodCases("SP"),
                "add_cases", GenerateAbstractMethodCases("Add"),
                "subtract_cases", GenerateAbstractMethodCases("Subtract")
                );

            return composer.ToString();
        }

        public override void Generate()
        {
            var classCodeText = GenerateClassCode();

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
