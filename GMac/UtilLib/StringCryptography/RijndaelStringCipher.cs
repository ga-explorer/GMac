using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UtilLib.StringCryptography
{
    /// <summary>
    /// This class uses the Rijndael algorithm for encrypting\decrypting strings using static methods
    /// </summary>
    public static class RijndaelStringCipher
    {
        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string InitVector = "tu89geji340t89u2";

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int Keysize = 256;

        public static string Encrypt(string plainText, string passPhrase)
        {
            var initVectorBytes = Encoding.UTF8.GetBytes(InitVector);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var password = new Rfc2898DeriveBytes(passPhrase, new byte[] { 0 });// new PasswordDeriveBytes(passPhrase, null);
            var keyBytes = password.GetBytes(Keysize / 8);
            var symmetricKey = new RijndaelManaged {Mode = CipherMode.CBC};
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            var initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
            var cipherTextBytes = Convert.FromBase64String(cipherText);
            var password = new Rfc2898DeriveBytes(passPhrase, new byte[] {0});// new PasswordDeriveBytes(passPhrase, null);
            var keyBytes = password.GetBytes(Keysize / 8);
            var symmetricKey = new RijndaelManaged {Mode = CipherMode.CBC};
            var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainTextBytes = new byte[cipherTextBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
