using GMac.GMacAPI.CodeBlock;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Optimizer
{
    /// <summary>
    /// Target Code Block Processor base class
    /// </summary> 
    internal abstract class TcbProcessor
    {
        public GMacCodeBlock CodeBlock { get; private set; }


        protected TcbProcessor(GMacCodeBlock codeBlock)
        {
            CodeBlock = codeBlock;
        }


        protected abstract void BeginProcessing();
    }
}
