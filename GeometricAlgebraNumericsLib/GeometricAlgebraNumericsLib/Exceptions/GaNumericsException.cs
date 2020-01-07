using System;

namespace GeometricAlgebraNumericsLib.Exceptions
{
    public class GaNumericsException : Exception
    {
        public GaNumericsException(string message)
            : base(message)
        {
        }

        public GaNumericsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
