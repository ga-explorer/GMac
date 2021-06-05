using System.Collections.Generic;
using GMac.Engine.API.Binding;
using GMac.Engine.API.CodeBlock;
using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler.Semantic.AST;
using GMac.Engine.Compiler.Semantic.ASTInterpreter.LowLevel.Generator;
using GMac.Engine.Compiler.Semantic.ASTInterpreter.LowLevel.Optimizer.Evaluator;
using TextComposerLib.Logs.Progress;

namespace GMac.Engine.Compiler.Semantic.ASTInterpreter.LowLevel.Optimizer
{
    internal sealed class TcbOptimizer : TcbProcessor, IProgressReportSource
    {
        public static GMacCodeBlock Process(GMacMacro baseMacro)
        {
            var optimizer = new TcbOptimizer(baseMacro);

            optimizer.BeginProcessing();

            return optimizer.CodeBlock;
        }

        public static GMacCodeBlock Process(LlGenerator generator)
        {
            var optimizer = new TcbOptimizer(generator);

            optimizer.BeginProcessing();

            return optimizer.CodeBlock;
        }

        public static GMacCodeBlock Process(GMacMacro baseMacro, ProgressComposer progress)
        {
            var optimizer = new TcbOptimizer(baseMacro);

            optimizer.BeginProcessing();

            return optimizer.CodeBlock;
        }

        public static GMacCodeBlock Process(LlGenerator generator, ProgressComposer progress)
        {
            var optimizer = new TcbOptimizer(generator)
            {
                EnableTestEvaluation = false
            };

            optimizer.BeginProcessing();

            return optimizer.CodeBlock;
        }

        public static GMacCodeBlock Process(LlGenerator generator, Dictionary<string, GMacMacroParameterBinding> inputsWithTestValues, bool fixOutputsOrder, ProgressComposer progress)
        {
            var optimizer = new TcbOptimizer(generator)
            {
                FixOutputComputationsOrder = fixOutputsOrder, 
                EnableTestEvaluation = inputsWithTestValues.Count > 0,
                _inputsWithTestValues = inputsWithTestValues
            };

            optimizer.BeginProcessing();

            return optimizer.CodeBlock;
        }



        private Dictionary<string, GMacMacroParameterBinding> _inputsWithTestValues;

        
        /// <summary>
        /// The source low-level code generator for this optimizer
        /// </summary>
        public LlGenerator Generator { get; }

        public bool EnableTestEvaluation { get; private set; }

        public TlCodeBlockEvaluationHistory EvaluationDataHistory { get; private set; }

        /// <summary>
        /// If true, no optimization by re-ordering of output variables computation is performed
        /// </summary>
        public bool FixOutputComputationsOrder { get; set; }

        /// <summary>
        /// The ID of this class as a progress reporting source
        /// </summary>
        public string ProgressSourceId => "Code Block Optimizer";

        public ProgressComposer Progress => GMacEngineUtils.Progress;

        /// <summary>
        /// The base macro for this optimizer
        /// </summary>
        public GMacMacro BaseMacro => Generator.BaseMacro;

        /// <summary>
        /// The parent GMac DSL for this optimizer
        /// </summary>
        public GMacAst GMacRootAst => Generator.GMacRootAst;


        private TcbOptimizer(GMacMacro baseMacro)
            : base(new GMacCodeBlock(baseMacro.ToAstMacro()))
        {
            Generator = new LlGenerator(baseMacro);
        }

        private TcbOptimizer(LlGenerator generator)
            : base(new GMacCodeBlock(generator.BaseMacro.ToAstMacro()))
        {
            Generator = generator;
        }



        //private void OutputTrace(string traceItemTitle)
        //{
        //    if (ReferenceEquals(_progress, null))
        //        return;

        //    this.ReportNormal(traceItemTitle, CodeBlock.ToString());
        //}

        //private void OutputTrace(string traceItemTitle, string traceItemText)
        //{
        //    if (ReferenceEquals(_progress, null))
        //        return;

        //    this.ReportNormal(traceItemTitle, traceItemText);
        //}

        private void InitializeCodeBlock()
        {
            //Generate low-level code if not already generated and initialize target code block
            TcbInitialize.Process(CodeBlock, Generator.GenerateLowLevelItems());

            this.ReportNormal("Initialize Code Block", CodeBlock);

            if (EnableTestEvaluation)
            {
                EvaluationDataHistory = 
                    _inputsWithTestValues == null || _inputsWithTestValues.Count == 0
                    ? new TlCodeBlockEvaluationHistory(CodeBlock, -5.0D, 5.0D)
                    : new TlCodeBlockEvaluationHistory(CodeBlock, _inputsWithTestValues);

                EvaluationDataHistory.AddEvaluation("Initialize Code Block");
            }
        }

        private void ProcessSubExpressions()
        {
            //Use full reduction algorithm to produce less computations and simplest possible RHS expressions
            //but may take longer time and may require more temp variables
            if (GMacCompilerOptions.ReduceLowLevelRhsSubExpressions)
            {
                TcbReduceRhsExpressions.Process(CodeBlock);

                this.ReportNormal("Reduce RHS Sub-expressions", CodeBlock);

                if (EnableTestEvaluation)
                    EvaluationDataHistory.AddEvaluation("Reduce RHS Sub-expressions");

                return;
            }
            
            //Use partial reduction algorithm to factor out sub expressions used multiple times during 
            //computation but may produce larger RHS expressions per temp\output variable

            //Remove temp variables having duplicate RHS expressions
            TcbRemoveDuplicateTemps.Process(CodeBlock);

            this.ReportNormal("Remove Duplicate Temps", CodeBlock);

            if (EnableTestEvaluation)
                EvaluationDataHistory.AddEvaluation("Remove Duplicate Temps");

            //Factor common sub-expressions into separate low-level temp variables
            TcbFactorSubExpressions.Process(CodeBlock);

            this.ReportNormal("Factor Common Sub-expressions", CodeBlock);

            if (EnableTestEvaluation)
                EvaluationDataHistory.AddEvaluation("Factor Common Sub-expressions");
        }

        private void FinalizeCodeBlock()
        {
            //TcbDependencyUpdate.Process(CodeBlock);

            //OutputTrace("Dependency Update");

            //Re-order computations so that less expensive output variables and temps are computed first
            TcbReOrderComputations.Process(CodeBlock, FixOutputComputationsOrder);

            this.ReportNormal("Re-order Computations", CodeBlock);

            if (EnableTestEvaluation)
                EvaluationDataHistory.AddEvaluation("Re-order computations");

            //Minimize number of temporary variables needed in the final code
            TcbReUseTempVariables.Process(CodeBlock);

            this.ReportNormal("Re-use Temp Variables", CodeBlock);

            CodeBlock.UpdateParametersDictionary();

            if (EnableTestEvaluation)
            {
                EvaluationDataHistory.AddEvaluation("Re-use temp variables");

                this.ReportNormal("Evaluation History", EvaluationDataHistory);
            }
        }

        protected override void BeginProcessing()
        {
            InitializeCodeBlock();

            ProcessSubExpressions();

            FinalizeCodeBlock();
        }
    }
}
