using System.Linq;
using System.Text;

namespace UtilLib
{
    public static class StringToLiteralEncoder
    {
        private static readonly char[] HexDigitLower = "0123456789abcdef".ToCharArray();
        private static readonly char[] LiteralEncodeEscapeChars;

        static StringToLiteralEncoder()
        {
            // Per http://msdn.microsoft.com/en-us/library/h21280bw.aspx
            var escapes = new[] { "\aa", "\bb", "\ff", "\nn", "\rr", "\tt", "\vv", "\"\"", "\\\\", "??", "\00" };

            LiteralEncodeEscapeChars = new char[escapes.Max(e => e[0]) + 1];

            foreach (var escape in escapes)
                LiteralEncodeEscapeChars[escape[0]] = escape[1];
        }

        /// <summary>
        /// Convert the string to the equivalent C# string literal, enclosing the string in double quotes and inserting
        /// escape sequences as necessary.
        /// </summary>
        /// <param name="s">The string to be converted to a C# string literal.</param>
        /// <returns><paramref name="s"/> represented as a C# string literal.</returns>
        public static string Encode(string s)
        {
            if (null == s) return "null";

            var sb = new StringBuilder(s.Length + 2).Append('"');

            foreach (var c in s)
            {
                if (c < LiteralEncodeEscapeChars.Length && '\0' != LiteralEncodeEscapeChars[c])
                    sb.Append('\\').Append(LiteralEncodeEscapeChars[c]);

                else if ('~' >= c && c >= ' ')
                    sb.Append(c);

                else
                    sb.Append(@"\x")
                        .Append(HexDigitLower[c >> 12 & 0x0F])
                        .Append(HexDigitLower[c >> 8 & 0x0F])
                        .Append(HexDigitLower[c >> 4 & 0x0F])
                        .Append(HexDigitLower[c & 0x0F]);
            }

            return sb.Append('"').ToString();
        }
    }
}
