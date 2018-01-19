using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.GMacAPI.Binding;
using GMac.GMacAPI.CodeBlock;
using GMac.GMacAPI.Target;
using GMac.GMacAST;
using GMac.GMacAST.Symbols;
using GMac.GMacIDE;
using TextComposerLib.Diagrams.GraphViz.Dot;
using TextComposerLib.Logs.Progress;

namespace GMac.GMacAPI.CodeGen.BuiltIn.CSharp.Direct
{
    public sealed class SingleMacroGen : GMacCodeLibraryComposer
    {
        public GMacMacroBinding MacroBinding { get; }

        public Dictionary<string, string> TargetVariablesNamesDictionary { get; }
            = new Dictionary<string, string>();

        public DotGraph Graph { get; private set; }

        public override string Name => "Single Macro Generator";

        public override string Description => "Generates code for a single GMac macro.";


        public SingleMacroGen(GMacMacroBinding macroBinding)
            : base(macroBinding.BaseMacro.Root, GMacLanguageServer.CSharp4())
        {
            MacroBinding = macroBinding;

            MacroGenDefaults = new GMacMacroCodeComposerDefaults(this);

            SelectedSymbols.Add(macroBinding.BaseMacro);
        }


        private void GenerateTypeName(AstType typeInfo)
        {
            ActiveFileTextComposer.Append(
                typeInfo.IsValidPrimitiveType
                ? GMacLanguage.TargetTypeName(typeInfo.AssociatedPrimitiveType)
                : typeInfo.GMacTypeSignature);
        }

        private void GenerateTypeDefaultValue(AstType typeInfo)
        {
            if (typeInfo.IsValidIntegerType)
                ActiveFileTextComposer.Append("0");

            else if (typeInfo.IsValidBooleanType)
                ActiveFileTextComposer.Append("false");

            else if (typeInfo.IsValidScalarType)
                ActiveFileTextComposer.Append("0.0D");

            else
                ActiveFileTextComposer.Append("new " + typeInfo.GMacTypeSignature + "()");
        }

        private void GenerateMacroInputsCode(AstMacro macroInfo)
        {
            var flag = false;
            foreach (var paramInfo in macroInfo.InputParameters)
            {
                if (flag)
                    ActiveFileTextComposer.Append(", ");
                else
                    flag = true;

                GenerateTypeName(paramInfo.GMacType);

                ActiveFileTextComposer.Append(" ");

                ActiveFileTextComposer.Append(paramInfo.Name);
            }
        }

        private string BasisBladeIdToCode(string parentName, int id)
        {
            return
                new StringBuilder()
                .Append(parentName)
                .Append(".Coef[")
                .Append(id)
                .Append("]")
                .ToString();
        }

        private void SetTargetNaming(GMacTargetVariablesNaming targetNaming)
        {
            //Set default target variables names for macro parameters
            var valueAccessList =
                targetNaming
                .CodeBlock
                .BaseMacro
                .Parameters
                .SelectMany(paramInfo => paramInfo.DatastoreValueAccess.ExpandStructures());

            foreach (var valueAccess in valueAccessList)
            {
                if (valueAccess.IsPrimitive)
                {
                    var varName = valueAccess.ValueAccessName;

                    targetNaming.SetScalarParameter(valueAccess, varName);

                    continue;
                }

                var parentName = valueAccess.ValueAccessName;

                targetNaming.SetMultivectorParameters(
                    valueAccess,
                    id => BasisBladeIdToCode(parentName, id)
                    );
            }

            //Override default target variables names for macro parameters using the 
            //items in the TargetVariablesNamesDictionary
            foreach (var pair in TargetVariablesNamesDictionary)
                targetNaming.SetScalarParameter(pair.Key, pair.Value);

            //Set temporary target variables names
            targetNaming.SetTempVariables(v => "tmp" + v.NameIndex);
        }

        private GMacMacroCodeComposer InitMacroCodeGenerator(GMacMacroBinding macroBinding)
        {
            var macroGenerator = CreateMacroCodeGenerator(macroBinding.BaseMacro);

            macroGenerator.ActionSetMacroParametersBindings =
                macroGenBinding =>
                {
                    foreach (var paramBinding in macroBinding.Bindings)
                        if (paramBinding.IsVariable)
                            macroGenBinding.BindToVariables(
                                paramBinding.ValueAccess,
                                paramBinding.TestValueExpr
                                );
                        else
                            macroGenBinding.BindScalarToConstant(
                                paramBinding.ValueAccess, 
                                paramBinding.ConstantExpr
                                );
                };

            macroGenerator.ActionSetTargetVariablesNames = 
                SetTargetNaming;

            return macroGenerator;
        }

        private GMacCodeBlock GenerateMacroCode(GMacMacroBinding macroBinding)
        {
            GMacCodeBlock codeBlock = null;

            ActiveFileTextComposer.AppendAtNewLine("public static ");
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
            return new SingleMacroGen(MacroBinding);
        }

        public override IEnumerable<AstSymbol> GetBaseSymbolsList()
        {
            return Enumerable.Empty<AstSymbol>();
        }

        protected override bool VerifyReadyToGenerate()
        {
            var list = SelectedSymbols.ToArray();

            return list.Length == 1 && list[0].IsValidMacro;
        }

        protected override bool InitializeTemplates()
        {
            return true;
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
            CodeFilesComposer.InitalizeFile(MacroBinding.BaseMacro.Name + ".cs");

            var codeBlock = GenerateMacroCode(MacroBinding);

            CodeFilesComposer.UnselectActiveFile();


            Graph = codeBlock.ToGraphViz();

            var progressId = this.ReportStart("Generate GraphViz Code");

            Graph.GenerateDotCode();

            this.ReportFinish(progressId);
        }
    }
}
