using System;
using System.Linq;
using System.Text;
using CodeComposerLib.SyntaxTree;
using GMac.Engine.API.Binding;
using GMac.Engine.API.CodeBlock;
using GMac.Engine.API.Target;
using GMac.Engine.AST;
using GMac.Engine.AST.Symbols;
using TextComposerLib.Loggers.Progress;

namespace GMac.Engine.API.CodeGen
{
    /// <summary>
    /// This abstract class can be used to implement a sub-process of macro-based computational code generation 
    /// using the main code library generator composnents and a macro binding component
    /// </summary>
    public class GMacMacroCodeComposer : GMacCodeStringComposer
    {
        private static void GenerateDeclareTempsCode(GMacMacroCodeComposer macroCodeGen)
        {
            var tempVarNames =
                macroCodeGen.CodeBlock
                .TempVariables
                .Select(item => item.TargetVariableName)
                .Distinct();

            //Add temp variables declaration code
            foreach (var tempVarName in tempVarNames)
                macroCodeGen.SyntaxList.Add(
                    macroCodeGen
                        .GMacLanguage
                        .SyntaxFactory
                        .DeclareLocalVariable(macroCodeGen.GMacLanguage.ScalarTypeName, tempVarName)
                    );

            macroCodeGen.SyntaxList.AddEmptyLine();
        }

        public static void DefaultGenerateCommentsBeforeComputations(GMacMacroCodeComposer macroCodeGen)
        {
            macroCodeGen.SyntaxList.Add(
                macroCodeGen.SyntaxFactory.Comment(
                    "Begin GMac Macro Code Generation, " + DateTime.Now.ToString("O")
                    )
                );

            macroCodeGen.SyntaxList.Add(
                macroCodeGen.SyntaxFactory.Comment("Macro: " + macroCodeGen.CodeBlock.BaseMacro.AccessName)
                );

            macroCodeGen.SyntaxList.Add(
                macroCodeGen.SyntaxFactory.Comment(
                    macroCodeGen
                    .CodeBlock
                    .GetStatisticsReport()
                    .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                    )
                );

            IGMacCbParameterVariable paramVar;

            var commentTextLines =
                macroCodeGen
                .MacroBinding
                .Bindings
                .Select(
                    p =>
                    {
                        var s = new StringBuilder();

                        s.Append("   ").Append(p.ValueAccessName);

                        if (p.IsConstant) 
                            return s.Append(" = constant: '").Append(p.ConstantValue).Append("'").ToString();

                        if (!macroCodeGen.CodeBlock.TryGetParameterVariable(p.ValueAccess, out paramVar))
                            return s.Append(" = constant: '0', the parameter binding was not found in the code block!!").ToString();

                        return (string.IsNullOrEmpty(paramVar.TargetVariableName))
                            ? s.ToString()
                            : s.Append(" = variable: ").Append(paramVar.TargetVariableName).ToString();
                    })
                .ToArray();

            macroCodeGen.SyntaxList.Add(
                macroCodeGen.SyntaxFactory.Comment()
                );

            macroCodeGen.SyntaxList.Add(
                macroCodeGen.SyntaxFactory.Comment("Macro Binding Data: ")
                );

            macroCodeGen.SyntaxList.Add(macroCodeGen.SyntaxFactory.Comment(commentTextLines));

            macroCodeGen.SyntaxList.AddEmptyLine();
        }

        public static void DefaultGenerateCommentsAfterComputations(GMacMacroCodeComposer macroCodeGen)
        {
            macroCodeGen.SyntaxList.Add(
                macroCodeGen.SyntaxFactory.Comment(
                    "Finish GMac Macro Code Generation, " + DateTime.Now.ToString("O")
                    )
                );
        }

        public static bool DefaultActionBeforeGenerateComputations(GMacMacroCodeComposer macroCodeGen)
        {
            DefaultGenerateCommentsBeforeComputations(macroCodeGen);

            GenerateDeclareTempsCode(macroCodeGen);

            return true;
        }

        public static void DefaultActionAfterGenerateComputations(GMacMacroCodeComposer macroCodeGen)
        {
            DefaultGenerateCommentsAfterComputations(macroCodeGen);
        }

        public static void DefaultActionSetMacroParametersBindings(GMacMacroBinding macroBinding)
        {
            var lowLevelParams =
                macroBinding
                .BaseMacro
                .Parameters
                .SelectMany(p => p.DatastoreValueAccess.ExpandAll());

            foreach (var macroParam in lowLevelParams)
                macroBinding.BindToVariables(macroParam);
        }

        public static void DefaultActionSetTargetVariablesNames(GMacTargetVariablesNaming targetNaming)
        {
            targetNaming.SetInputVariables(v => v.ValueAccessName);

            targetNaming.SetOutputVariables(v => v.ValueAccessName);

            //targetNaming.SetTempVariables(v => v.LowLevelName);
            targetNaming.SetTempVariables(v => "tmp" + v.NameIndex);
        }


        /// <summary>
        /// The expression converter object used in this class
        /// </summary>
        protected GMacMathematicaExpressionConverter ExpressionConverter => LibraryComposer.GMacLanguage.ExpressionConverter;


        public override string ProgressSourceId => "Macro Code Generator";

        /// <summary>
        /// The defaults used in initializing this macro code generator when created or when
        /// the base macro is set to a new macro
        /// </summary>
        public GMacMacroCodeComposerDefaults UsedDefaults { get; }

        /// <summary>
        /// The text composer object where all generated macro code is written
        /// </summary>
        public SteSyntaxElementsList SyntaxList { get; }

        /// <summary>
        /// The macro binding object to set primitive values assigned to macro parameters. 
        /// Values may be constants for input parameters or variables input or output parameters
        /// </summary>
        public GMacMacroBinding MacroBinding { get; private set; }

        /// <summary>
        /// The base macro used for code generation
        /// </summary>
        public AstMacro BaseMacro 
            => MacroBinding?.BaseMacro;

        /// <summary>
        /// The code block created from the macro binding holding abstract symbolic computations to be
        /// used for final code generation
        /// </summary>
        public GMacCodeBlock CodeBlock { get; private set; }

        /// <summary>
        /// An object that can be used to assign target variable names to low-level code block variables to be
        /// used in final code generation
        /// </summary>
        public GMacTargetVariablesNaming TargetVariablesNaming { get; private set; }

        /// <summary>
        /// If false, no code is actually generated from this macro code generator
        /// The default is true
        /// </summary>
        public bool AllowGenerateMacroCode { get; set; }

        /// <summary>
        /// If false, the comments before each computational line are not generated
        /// </summary>
        public bool AllowGenerateComputationComments { get; set; }


        /// <summary>
        /// This is executed before code generation to set the abstract macro parameters bindings to 
        /// constants and variables as desired.
        /// </summary>
        public Action<GMacMacroBinding> ActionSetMacroParametersBindings { get; set; }

        /// <summary>
        /// This is executed before code generation to assign target variable names to low-level code block
        /// variables. The target variable names will be the ones used in final code generation
        /// </summary>
        public Action<GMacTargetVariablesNaming> ActionSetTargetVariablesNames { get; set; }

        /// <summary>
        /// This is executed before generating computation code. It can be used to add comments, declare temp 
        /// variables in the target code or any other similar purpose.
        /// </summary>
        public Func<GMacMacroCodeComposer, bool> ActionBeforeGenerateComputations { get; set; }

        /// <summary>
        /// This is executed after generating computation code. It can be used to add comments, destruct temp
        /// variables in the target code or or any other similar purpose.
        /// </summary>
        public Action<GMacMacroCodeComposer> ActionAfterGenerateComputations { get; set; }

        /// <summary>
        /// This is executed each time before a computation code is generated. It can be used to inject code
        /// in the final generated code or to prevent code generation of this line by returning false
        /// </summary>
        public Action<SteSyntaxElementsList, GMacComputationCodeInfo> ActionBeforeGenerateSingleComputation { get; set; }

        /// <summary>
        /// This is executed each time after a computation code is generated. It can be used to inject code
        /// in the final generated code
        /// </summary>
        public Action<SteSyntaxElementsList, GMacComputationCodeInfo> ActionAfterGenerateSingleComputation { get; set; }


        /// <summary>
        /// True if the macro binding is not null and ready for code block generation
        /// </summary>
        public bool IsMacroBindingReady 
            => (MacroBinding != null && MacroBinding.IsMacroBindingReady);

        /// <summary>
        /// True if the code block is generated and ready
        /// </summary>
        public bool IsCodeBlockReady 
            => CodeBlock != null;


        public GMacMacroCodeComposer(GMacCodeLibraryComposer libGen, AstMacro baseMacro = null)
            : base(libGen)
        {
            SyntaxList = new SteSyntaxElementsList();

            MacroBinding = baseMacro.IsNullOrInvalid() ? null : GMacMacroBinding.Create(baseMacro);

            UsedDefaults = libGen.MacroGenDefaults.Duplicate();

            SetDefaults();
        }

        public GMacMacroCodeComposer(GMacMacroCodeComposerDefaults codeGenDefaults, AstMacro baseMacro = null)
            : base(codeGenDefaults.LibraryComposer)
        {
            SyntaxList = new SteSyntaxElementsList();

            MacroBinding = baseMacro.IsNullOrInvalid() ? null : GMacMacroBinding.Create(baseMacro);

            UsedDefaults = codeGenDefaults.Duplicate();

            SetDefaults();
        }


        private void SetDefaults()
        {
            if (UsedDefaults != null)
            {
                if (MacroBinding != null)
                {
                    MacroBinding.FixOutputComputationsOrder = UsedDefaults.FixOutputComputationsOrder;
                }

                AllowGenerateMacroCode = UsedDefaults.AllowGenerateMacroCode;

                ActionBeforeGenerateComputations = UsedDefaults.ActionBeforeGenerateComputations;

                ActionAfterGenerateComputations = UsedDefaults.ActionAfterGenerateComputations;

                return;
            }

            AllowGenerateMacroCode = true;
        }

        /// <summary>
        /// Used to replace the base macro by another one. This sets the defaults of the generator and clears
        /// the internal code composer
        /// </summary>
        /// <param name="baseMacro"></param>
        public void SetBaseMacro(AstMacro baseMacro)
        {
            SyntaxList.Clear();

            CodeBlock = null;

            TargetVariablesNaming = null;

            MacroBinding = GMacMacroBinding.Create(baseMacro);

            SetDefaults();
        }

        /// <summary>
        /// Generate the code for a single computation
        /// </summary>
        /// <param name="codeInfo"></param>
        protected virtual void GenerateSingleComputationCode(GMacComputationCodeInfo codeInfo)
        {
            //Generate comment to show symbolic form for this computation
            if (AllowGenerateComputationComments)
                SyntaxList.Add(
                    GMacLanguage
                        .SyntaxFactory
                        .Comment(codeInfo.ComputedVariable.ToString())
                );

            //Generate assignment statement for this computation
            SyntaxList.Add(
                GMacLanguage
                    .SyntaxFactory
                    .AssignToLocalVariable(codeInfo.TargetVariableName, codeInfo.RhsExpressionCode)
            );

            //Add an empty line
            if (AllowGenerateComputationComments || codeInfo.ComputedVariable.IsOutput)
                SyntaxList.Add(GMacLanguage.SyntaxFactory.EmptyLine());
        }

        /// <summary>
        /// Generate the low-level assignments code from the low-level optimized macro code
        /// </summary>
        private void GenerateProcessingCode()
        {
            ExpressionConverter.ActiveCodeBlock = CodeBlock;

            //Iterate over optimized low-level computations
            foreach (var computedVar in CodeBlock.ComputedVariables)
            {
                //Convert the rhs text expression tree into target language code
                var rhsExprCode = 
                    ExpressionConverter.Convert(computedVar.RhsExpr);

                //Create the codeInfo object
                var codeInfo = new GMacComputationCodeInfo()
                {
                    ComputedVariable = computedVar,
                    RhsExpressionCode = rhsExprCode,
                    GMacLanguage = GMacLanguage,
                    EnableCodeGeneration = true
                };

                //Generate the assignment target code based on the codeInfo object
                //Execute this action before generating computation code
                if (ReferenceEquals(ActionBeforeGenerateSingleComputation, null) == false)
                    ActionBeforeGenerateSingleComputation(SyntaxList, codeInfo);

                //If the action prevented generation of code don't generating computation code
                if (codeInfo.EnableCodeGeneration)
                    GenerateSingleComputationCode(codeInfo);

                //Execute this action after generating computation code
                if (ReferenceEquals(ActionAfterGenerateSingleComputation, null) == false)
                    ActionAfterGenerateSingleComputation(SyntaxList, codeInfo);
            }

            //SyntaxList.AddEmptyLine();
        }

        /// <summary>
        /// Generate optimized macro code in the target language given a list of macro parameters bindings
        /// </summary>
        /// <returns></returns>
        public override string Generate()
        {
            //Initialize components of macro code generator
            SyntaxList.Clear();

            MacroBinding.Clear();

            CodeBlock = null;

            TargetVariablesNaming = null;

            if (AllowGenerateMacroCode == false) return string.Empty;

            LibraryComposer.CheckProgressRequestStop();

            var progressId = this.ReportStart(
                "Generating Macro Code For: " + BaseMacro.AccessName
                );

            try
            {
                //Bind macro parameters to variables and constants as needed
                if (ActionSetMacroParametersBindings == null)
                    DefaultActionSetMacroParametersBindings(MacroBinding);
                else
                    ActionSetMacroParametersBindings(MacroBinding);

                if (IsMacroBindingReady == false)
                {
                    this.ReportWarning("Macro Binding Not Ready", MacroBinding.ToString());
                    this.ReportFinish(progressId);

                    return string.Empty;
                }

                this.ReportNormal("Macro Binding Ready", MacroBinding.ToString());

                //Create the optimizd code block holding abstract computations based on the macro parameters 
                //bindings. This step is typically the most time consuming because of many symbolic computations
                CodeBlock = MacroBinding.CreateOptimizedCodeBlock();


                //Assign target variable names to low-level code block variables. The code block is generated
                //automatically in the call to the OptimizedCodeBlock member
                TargetVariablesNaming = new GMacTargetVariablesNaming(GMacLanguage, CodeBlock);

                if (ActionSetTargetVariablesNames == null)
                    DefaultActionSetTargetVariablesNames(TargetVariablesNaming);
                else
                    ActionSetTargetVariablesNames(TargetVariablesNaming);


                //Generate code before computations for comments, temp declarations, and the like
                var result =
                    ActionBeforeGenerateComputations == null
                        ? DefaultActionBeforeGenerateComputations(this)
                        : ActionBeforeGenerateComputations(this);


                //Generate computations code if allowed by last action result
                if (result) GenerateProcessingCode();


                //Generate code after computations for comments, temp destruction, and the like
                if (ActionAfterGenerateComputations == null)
                    DefaultActionAfterGenerateComputations(this);
                else
                    ActionAfterGenerateComputations(this);
            }
            catch (Exception e)
            {
                this.ReportError(e);
            }

            //Clean everything up and return final generated code
            ExpressionConverter.ActiveCodeBlock = null;

            //Un-parse the SyntaxList into the final code
            var codeText = CodeGenerator.GenerateCode(SyntaxList);

            this.ReportFinish(progressId, codeText);

            return codeText;
        }
    }
}
