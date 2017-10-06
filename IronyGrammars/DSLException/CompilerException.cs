﻿using System;

namespace IronyGrammars.DSLException
{
    public class CompilerException : DslException
    {
        public CompilerException(string message)
            : base(message)
        {
        }

        public CompilerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}
