using System.Security.Cryptography;
using System.Text;

namespace UtilLib.Strings
{
    public static class StringUtils
    {
        private static readonly SHA256Managed HashString = new SHA256Managed();


        public static string GetHashSha256(string text)
        {
            return GetHashSha256(text, Encoding.ASCII);
        }

        public static string GetHashSha256(string text, Encoding textEncoding)
        {
            var bytes = textEncoding.GetBytes(text);

            return GetHashSha256(bytes);
        }

        public static string GetHashSha256(byte[] bytes)
        {
            var hash = HashString.ComputeHash(bytes);

            var hashString = new StringBuilder(2 * hash.Length);

            foreach (var x in hash)
                hashString.AppendFormat("{0:X2}", x);

            return hashString.ToString();
        }
    }
}
