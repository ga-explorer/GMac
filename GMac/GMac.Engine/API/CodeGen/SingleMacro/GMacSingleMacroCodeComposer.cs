using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.Engine.API.Binding;
using GMac.Engine.API.CodeBlock;
using GMac.Engine.API.Target;
using GMac.Engine.AST;
using GMac.Engine.AST.Symbols;

namespace GMac.Engine.API.CodeGen.SingleMacro
{
    public abstract class GMacSingleMacroCodeComposer : GMacCodeLibraryComposer
    {
        public GMacMacroBinding MacroBinding { get; }

        public GMacCodeBlock CodeBlock { get; protected set; }

        public override string Name 
            => "Single Macro Generator";

        public override string Description 
            => "Generates code for a single GMac macro.";

        /// <summary>
        /// Enable the generation of an Excel file to visualize and perform
        /// the computations made in the macro under the given macro binding
        /// </summary>
        public bool GenerateExcel { get; set; } = true;


        protected GMacSingleMacroCodeComposer(GMacMacroBinding macroBinding, GMacLanguageServer languageServer)
            : base(macroBinding.BaseMacro.Root, languageServer)
        {
            MacroBinding = macroBinding;

            MacroGenDefaults = new GMacMacroCodeComposerDefaults(this);

            SelectedSymbols.Add(macroBinding.BaseMacro);
        }


        protected void GenerateTypeName(AstType typeInfo)
        {
            ActiveFileTextComposer.Append(
                typeInfo.IsValidPrimitiveType
                    ? GMacLanguage.TargetTypeName(typeInfo.AssociatedPrimitiveType)
                    : typeInfo.GMacTypeSignature);
        }

        protected abstract void GenerateTypeDefaultValue(AstType typeInfo);

        protected void GenerateMacroInputsCode(AstMacro macroInfo)
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

        protected override bool VerifyReadyToGenerate()
        {
            var list = SelectedSymbols.ToArray();

            return list.Length == 1 && list[0].IsValidMacro;
        }

        protected override bool InitializeTemplates()
        {
            return true;
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


        public override IEnumerable<AstSymbol> GetBaseSymbolsList()
        {
            return Enumerable.Empty<AstSymbol>();
        }

        protected virtual string BasisBladeIdToCode(string parentName, int id)
        {
            return
                new StringBuilder()
                    .Append(parentName)
                    .Append(".Coef[")
                    .Append(id)
                    .Append("]")
                    .ToString();
        }

        protected virtual void SetTargetNaming(GMacTargetVariablesNaming targetNaming)
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
                    id => BasisBladeIdToCode(parentName, (int)id)
                );
            }

            //Override default target variables names for macro parameters using the 
            //items in the MacroBinding.TargetVariablesNames, if any exist
            foreach (var pair in MacroBinding.TargetVariablesNames)
                targetNaming.SetScalarParameter(pair.Key, pair.Value);

            //Set temporary target variables names
            targetNaming.SetTempVariables(v => "tmp" + v.NameIndex);
        }

        protected GMacMacroCodeComposer InitMacroCodeGenerator(GMacMacroBinding macroBinding)
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

        protected abstract GMacCodeBlock GenerateMacroCode(GMacMacroBinding macroBinding);

        protected override void ComposeTextFiles()
        {
            CodeFilesComposer.InitalizeFile(MacroBinding.BaseMacro.Name + "." + Language.DefaultFileExtension);

            CodeBlock = GenerateMacroCode(MacroBinding);

            CodeFilesComposer.UnselectActiveFile();
        }
    }
}