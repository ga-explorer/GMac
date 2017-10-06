using IronyGrammars.DSLException;

namespace GMac.GMacCompiler.Symbolic
{
    public class GMacSymbolicException : DslException
    {
        public GMacSymbolicException(string message)
            : base(message)
        {
        }
    }
}
