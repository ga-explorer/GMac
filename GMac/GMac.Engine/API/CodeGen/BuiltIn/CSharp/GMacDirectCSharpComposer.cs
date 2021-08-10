using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.Engine.API.Binding;
using GMac.Engine.API.Target;
using GMac.Engine.AST;
using GMac.Engine.AST.Expressions;
using GMac.Engine.AST.Symbols;
using TextComposerLib.Files;
using TextComposerLib.Text.Parametric;

namespace GMac.Engine.API.CodeGen.BuiltIn.CSharp
{
    public sealed class GMacDirectCSharpComposer : GMacCodeLibraryComposer
    {
        private AstNamespace _currentNamespace;

        private ParametricTextComposer MultivectorTemplate => Templates["multivector"];

        public override string Name => "Direct GMacDSL Generator";

        public override string Description => "Generate multiple files holding a direct translation of GMacDSL constructs into target language constructs.";


        public GMacDirectCSharpComposer(AstRoot astInfo)
            : base(astInfo, GMacLanguageServer.CSharp4())
        {
            MacroGenDefaults = new GMacMacroCodeComposerDefaults(this);
        }


        protected override bool InitializeTemplates()
        {
            Templates.Clear();

            var codeTemplate1 = new ParametricTextComposer("<@", "@>");

            codeTemplate1.SetTemplateText(
@"public Multivector()
{
}

public Multivector(params double[] coefs)
{
    int i = 0;
    foreach (var coef in coefs.Take(<@gadim@>))
        Coef[i++] = coef;
}

public Multivector(IEnumerable<double> coefs)
{
    int i = 0;
    foreach (var coef in coefs.Take(<@gadim@>))
        Coef[i++] = coef;
}

"
            );

            Templates.Add("multivector", codeTemplate1);

            return true;
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

        private void GenerateScalarValueCode(AstValueScalar valueInfo)
        {
            var exprCode = GMacLanguage.GenerateCode(valueInfo.ScalarValue.Expression);

            ActiveFileTextComposer.Append(exprCode);
        }

        private void GenerateMultivectorValueCode(AstValueMultivector valueInfo)
        {
            ActiveFileTextComposer.Append("new ");
            ActiveFileTextComposer.Append(GetSymbolTargetName(valueInfo.FrameMultivector));
            ActiveFileTextComposer.Append("(");
            
            for (var id = 0UL; id < valueInfo.Frame.GaSpaceDimension; id++)
            {
                if (id > 0)
                    ActiveFileTextComposer.Append(", ");

                GenerateScalarValueCode(valueInfo[id]);
            }

            ActiveFileTextComposer.Append(")");
        }

        private void GenerateStructureValueCode(AstValueStructure valueInfo)
        {
            ActiveFileTextComposer.Append("new ");
            ActiveFileTextComposer.Append(valueInfo.Structure.AssociatedStructure.SymbolAccessName);
            ActiveFileTextComposer.AppendLine("()");
            ActiveFileTextComposer.AppendLine("{");
            ActiveFileTextComposer.IncreaseIndentation();

            //Generate structure members initialization code
            var firstFlag = true;
            foreach (var termInfo in valueInfo.Terms)
            {
                if (firstFlag)
                    firstFlag = false;
                else
                    ActiveFileTextComposer.AppendLine(",");

                ActiveFileTextComposer.Append(termInfo.DataMemberName);
                ActiveFileTextComposer.Append(" = ");
                GenerateValueCode(termInfo.DataMemberValue);
            }
            ActiveFileTextComposer.AppendLine();

            ActiveFileTextComposer.DecreaseIndentation();
            ActiveFileTextComposer.Append("}");
        }

        private void GenerateValueCode(AstValue valueInfo)
        {
            //if (valueInfo.IsIntegerValue)
            //    ActiveFileComposer.Append(valueInfo.IntegerValue.ToString());

            //else if (valueInfo.IsBooleanValue)
            //    ActiveFileComposer.Append(valueInfo.BooleanValue.ToString());

            if (valueInfo.IsValidScalarValue)
                GenerateScalarValueCode(valueInfo as AstValueScalar);

            else if (valueInfo.IsValidMultivectorValue)
                GenerateMultivectorValueCode(valueInfo as AstValueMultivector);

            else if (valueInfo.IsValidStructureValue)
                GenerateStructureValueCode(valueInfo as AstValueStructure);

            else
                ActiveFileTextComposer.Append("<undefined value>");
        }

        private void GenerateConstantCode(AstConstant constantInfo)
        {
            ActiveFileTextComposer.AppendAtNewLine("public static readonly ");
            GenerateTypeName(constantInfo.GMacType);
            ActiveFileTextComposer.Append(" ");
            ActiveFileTextComposer.Append(constantInfo.Name);
            ActiveFileTextComposer.Append(" = ");
            GenerateValueCode(constantInfo.Value);
            ActiveFileTextComposer.AppendLine(";");
            ActiveFileTextComposer.AppendLine();
        }

        private void GenerateFrameMultivectorCode(AstFrameMultivector mvInfo)
        {
            ActiveFileTextComposer.AppendLineAtNewLine(@"public sealed class " + mvInfo.Name);
            ActiveFileTextComposer.AppendLineAtNewLine("{");
            ActiveFileTextComposer.IncreaseIndentation();
            ActiveFileTextComposer.AppendLineAtNewLine(@"public readonly " + GMacLanguage.ScalarTypeName + "[] Coef = new " + GMacLanguage.ScalarTypeName + "[" + mvInfo.ParentFrame.GaSpaceDimension + "];");
            ActiveFileTextComposer.AppendLineAtNewLine();
            ActiveFileTextComposer.AppendLineAtNewLine();

            //Use a parametric text template instead of a direct logging approach
            //Set template parameters values
            MultivectorTemplate["gadim"] = mvInfo.ParentFrame.GaSpaceDimension.ToString();

            //Generate text from the template into the code
            ActiveFileTextComposer.AppendAtNewLine(MultivectorTemplate.GenerateText());
            ActiveFileTextComposer.AppendLine();

            ActiveFileTextComposer.DecreaseIndentation();
            ActiveFileTextComposer.AppendLineAtNewLine("}");
        }

        private void GenerateFrameCode(AstFrame frameInfo)
        {
            CodeFilesComposer.InitalizeFile(frameInfo.Name + ".cs", GenerateCodeFileStartCode);

            ActiveFileTextComposer.AppendLineAtNewLine(@"public static class " + frameInfo.Name);
            ActiveFileTextComposer.AppendLineAtNewLine("{");
            ActiveFileTextComposer.IncreaseIndentation();

            GenerateFrameMultivectorCode(frameInfo.FrameMultivector);

            ActiveFileTextComposer.AppendLineAtNewLine();

            foreach (var constantInfo in frameInfo.Constants)
                GenerateConstantCode(constantInfo);

            foreach (var macroInfo in frameInfo.Macros)
                GenerateMacroCode(macroInfo);

            ActiveFileTextComposer.DecreaseIndentation();
            ActiveFileTextComposer.AppendLineAtNewLine("}");

            CodeFilesComposer.UnselectActiveFile(GenerateCodeFileEndCode);
        }

        private void GenerateStructureCode(AstStructure structureInfo)
        {
            CodeFilesComposer.InitalizeFile(structureInfo.Name + ".cs", GenerateCodeFileStartCode);

            ActiveFileTextComposer.AppendAtNewLine(@"public sealed class ");
            ActiveFileTextComposer.AppendLine(structureInfo.Name);
            ActiveFileTextComposer.AppendLineAtNewLine("{");
            ActiveFileTextComposer.IncreaseIndentation();

            foreach (var memberInfo in structureInfo.DataMembers)
            {
                ActiveFileTextComposer.AppendAtNewLine("public ");
                GenerateTypeName(memberInfo.GMacType);
                ActiveFileTextComposer.Append(" ");
                ActiveFileTextComposer.Append(memberInfo.Name);
                ActiveFileTextComposer.AppendLine(" { get; set; }");
                ActiveFileTextComposer.AppendLine();
            }

            ActiveFileTextComposer.AppendLine();

            ActiveFileTextComposer.AppendAtNewLine("public ");
            ActiveFileTextComposer.Append(structureInfo.Name);
            ActiveFileTextComposer.AppendLine("()");
            ActiveFileTextComposer.Append("{");
            ActiveFileTextComposer.IncreaseIndentation();

            foreach (var memberInfo in structureInfo.DataMembers.Where(memberInfo => !memberInfo.GMacType.IsValidPrimitiveType))
            {
                ActiveFileTextComposer.AppendAtNewLine(memberInfo.Name);
                ActiveFileTextComposer.Append(" = ");
                GenerateTypeDefaultValue(memberInfo.GMacType);
                ActiveFileTextComposer.AppendLine(";");
            }

            ActiveFileTextComposer.DecreaseIndentation();
            ActiveFileTextComposer.Append("}");

            ActiveFileTextComposer.DecreaseIndentation();
            ActiveFileTextComposer.AppendLineAtNewLine("}");

            CodeFilesComposer.UnselectActiveFile(GenerateCodeFileEndCode);
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

        private void InitializeFullMacroBinding(GMacMacroBinding macroBinding)
        {
            macroBinding.Clear();

            var valueAccessList =
                macroBinding
                .BaseMacro
                .Parameters
                .SelectMany(paramInfo => paramInfo.DatastoreValueAccess.ExpandAll());

            foreach (var valueAccess in valueAccessList)
                macroBinding.BindToVariables(valueAccess);
        }

        private void InitializeFullTargetNaming(GMacTargetVariablesNaming targetNaming)
        {
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
                    id => BasisBladeIdToCode(parentName, (int)id)
                    );
            }

            targetNaming.SetTempVariables(v => "tmp" + v.NameIndex);
        }

        private GMacMacroCodeComposer InitMacroCodeGenerator(GMacMacroBinding macroBinding)
        {
            //MacroGenDefaults.FixOutputComputationsOrder = true;

            var macroGenerator =
                new GMacMacroCodeComposer(MacroGenDefaults, macroBinding.BaseMacro)
                {
                    ActionSetMacroParametersBindings = InitializeFullMacroBinding,
                    ActionSetTargetVariablesNames = InitializeFullTargetNaming
                };

            return macroGenerator;
        }

        private void GenerateMacroCode(AstMacro macro)
        {
            var macroBinding = GMacMacroBinding.Create(macro);

            InitializeFullMacroBinding(macroBinding);

            GenerateMacroCode(macroBinding);
        }

        private void GenerateMacroCode(GMacMacroBinding macroBinding)
        {
            //GMacCodeBlock codeBlock = null;

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

                //codeBlock = macroGenerator.CodeBlock;
            }

            ActiveFileTextComposer.AppendLineAtNewLine("return result;");

            ActiveFileTextComposer.DecreaseIndentation();
            ActiveFileTextComposer.AppendLineAtNewLine("}");
            ActiveFileTextComposer.AppendLine();

            //return codeBlock;
        }

        private void GenerateCodeFileStartCode(TextFileComposer fileComposer)
        {
            var textComposer = fileComposer.TextComposer;

            textComposer.AppendLineAtNewLine(@"using System;");
            textComposer.AppendLineAtNewLine(@"using System.Collections.Generic;");
            textComposer.AppendLineAtNewLine(@"using System.Linq;");
            textComposer.AppendLineAtNewLine(@"using System.Text;");
            textComposer.AppendLineAtNewLine();
            textComposer.AppendAtNewLine(@"namespace ");
            textComposer.Append(GetSymbolTargetName(_currentNamespace));
            textComposer.AppendLineAtNewLine("{");
            textComposer.IncreaseIndentation();
        }

        private void GenerateCodeFileEndCode(TextFileComposer fileComposer)
        {
            var textComposer = fileComposer.TextComposer;

            textComposer.DecreaseIndentation();
            textComposer.AppendLineAtNewLine("}");

            fileComposer.FinalizeText();
        }

        private void GenerateNamespaceCode(AstNamespace namespaceInfo)
        {
            _currentNamespace = namespaceInfo;

            CodeFilesComposer.DownFolder(namespaceInfo.Name);

            CodeFilesComposer.InitalizeFile(namespaceInfo.Name + "Utils.cs", GenerateCodeFileStartCode);

            ActiveFileTextComposer.AppendLineAtNewLine(@"public static class " + namespaceInfo.Name + "Utils");
            ActiveFileTextComposer.AppendLineAtNewLine("{");
            ActiveFileTextComposer.IncreaseIndentation();

            foreach (var constantInfo in namespaceInfo.ChildConstants)
                GenerateConstantCode(constantInfo);

            foreach (var macroInfo in namespaceInfo.ChildMacros)
                GenerateMacroCode(macroInfo);

            ActiveFileTextComposer.DecreaseIndentation();
            ActiveFileTextComposer.AppendLineAtNewLine("}");

            CodeFilesComposer.UnselectActiveFile(GenerateCodeFileEndCode);

            foreach (var frameInfo in namespaceInfo.ChildFrames)
                GenerateFrameCode(frameInfo);

            foreach (var structureInfo in namespaceInfo.ChildStructures)
                GenerateStructureCode(structureInfo);

            foreach (var childNamespaceInfo in namespaceInfo.ChildNamespaces)
                GenerateNamespaceCode(childNamespaceInfo);

            CodeFilesComposer.UpFolder();
        }


        protected override void InitializeSubComponents()
        {
        }

        protected override void FinalizeSubComponents()
        {

        }

        protected override string GetSymbolTargetName(AstSymbol symbol)
        {
            return symbol.AccessName;
        }

        protected override void ComposeTextFiles()
        {
            foreach (var namespaceInfo in Root.ChildNamespaces)
                GenerateNamespaceCode(namespaceInfo);
        }

        public override GMacCodeLibraryComposer CreateEmptyComposer()
        {
            return new GMacDirectCSharpComposer(Root);
        }

        public override IEnumerable<AstSymbol> GetBaseSymbolsList()
        {
            return Enumerable.Empty<AstSymbol>();
        }

        protected override bool VerifyReadyToGenerate()
        {
            return true;
        }
    }
}
