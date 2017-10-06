using System;

namespace IronyGrammars.DSLException
{
    public class AstGeneratorException : CompilerException
    {
        public AstGeneratorException(string message)
            : base(message)
        {
        }

        public AstGeneratorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
