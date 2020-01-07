using GMac.GMacAPI.Binding;
using GMac.GMacAPI.CodeBlock;
using GMac.GMacAPI.Target;
using GMac.GMacAST;

namespace GMac.GMacAPI.CodeGen.SingleMacro
{
    public sealed class GMacSingleMacroCppComposer : GMacSingleMacroCodeComposer
    {
        public GMacSingleMacroCppComposer(GMacMacroBinding macroBinding)
            : base(macroBinding, GMacLanguageServer.Cpp11())
        {
        }


        protected override void GenerateTypeDefaultValue(AstType typeInfo)
        {
            if (typeInfo.IsValidIntegerType)
                ActiveFileTextComposer.Append("0");

            else if (typeInfo.IsValidBooleanType)
                ActiveFileTextComposer.Append("false");

            else if (typeInfo.IsValidScalarType)
                ActiveFileTextComposer.Append("0.0");

            else
                ActiveFileTextComposer.Append("new " + typeInfo.GMacTypeSignature + "()");
        }

        protected override GMacCodeBlock GenerateMacroCode(GMacMacroBinding macroBinding)
        {
            GMacCodeBlock codeBlock = null;

            ActiveFileTextComposer.AppendAtNewLine("public ");
            GenerateTypeName(macroBinding.BaseMacro.OutputType);
            ActiveFileTextComposer.Append(" ");
            ActiveFileTextComposer.Append(macroBinding.BaseMacro.Name);
            ActiveFileTextComposer.Append("(");

            GenerateMacroInputsCode(macroBinding.BaseMacro);

            ActiveFileTextComposer.AppendLine(")");

            ActiveFileTextComposer.AppendLineAtNewLine("{");
            ActiveFileTextComposer.IncreaseIndentation();

            if (macroBinding.BaseMacro.OutputType.IsValidScalarType)
            {
                ActiveFileTextComposer.AppendAtNewLine(GMacLanguage.ScalarTypeName);
                ActiveFileTextComposer.AppendLine(" result;");
            }
            else
            {
                ActiveFileTextComposer.AppendAtNewLine("var result = ");

                GenerateTypeDefaultValue(macroBinding.BaseMacro.OutputType);

                ActiveFileTextComposer.AppendLine(";");
            }

            ActiveFileTextComposer.AppendLine();

            if (MacroGenDefaults.AllowGenerateMacroCode)
            {
                var macroGenerator = InitMacroCodeGenerator(macroBinding);

                ActiveFileTextComposer.AppendLineAtNewLine(
                    macroGenerator.Generate()
                    );

                codeBlock = macroGenerator.CodeBlock;
            }

            ActiveFileTextComposer.AppendLineAtNewLine("return result;");

            ActiveFileTextComposer.DecreaseIndentation();
            ActiveFileTextComposer.AppendLineAtNewLine("}");
            ActiveFileTextComposer.AppendLine();

            return codeBlock;
        }


        public override GMacCodeLibraryComposer CreateEmptyGenerator()
        {
            return new GMacSingleMacroCppComposer(MacroBinding);
        }
    }
}
