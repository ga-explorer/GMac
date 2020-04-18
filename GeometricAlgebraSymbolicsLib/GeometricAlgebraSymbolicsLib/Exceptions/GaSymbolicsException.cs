using System;

namespace GeometricAlgebraSymbolicsLib.Exceptions
{
    public class GaSymbolicsException : Exception
    {
        public GaSymbolicsException(string message)
            : base(message)
        {
        }

        public GaSymbolicsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
