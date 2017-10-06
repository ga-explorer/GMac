using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UtilLib.StringCryptography
{
    /// <summary>
    /// This class uses a Triple DES algorithm for encrypting\decrypting strings using instance methods as follows:
    /// For string encryption:
    /// string message = "A very secret message here.";
    /// DataEncryptor keys = new TripleDESStringCipher();
    /// string encr = keys.EncryptString(message);
    /// 
    /// For string decryption:
    /// string actual = keys.DecryptString(encr);
    /// </summary>
    public sealed class TripleDesStringCipher
    {
        #region Factory

        public TripleDesStringCipher()
        {
            Algorithm = new TripleDESCryptoServiceProvider {Padding = PaddingMode.PKCS7};
        }

        public TripleDesStringCipher(TripleDESCryptoServiceProvider keys)
        {
            Algorithm = keys;
        }

        public TripleDesStringCipher(byte[] key, byte[] iv)
        {
            Algorithm = new TripleDESCryptoServiceProvider
            {
                Padding = PaddingMode.PKCS7, 
                Key = key, 
                IV = iv
            };
        }

        #endregion

        #region Properties

        public TripleDESCryptoServiceProvider Algorithm { get; set; }

        public byte[] Key
        {
            get { return Algorithm.Key; }
            set { Algorithm.Key = value; }
        }

        public byte[] Iv
        {
            get { return Algorithm.IV; }
            set { Algorithm.IV = value; }
        }

        #endregion

        #region Crypto

        public byte[] Encrypt(byte[] data) 
        { 
            return Encrypt(data, data.Length); 
        }

        public byte[] Encrypt(byte[] data, int length)
        {
            try
            {
                // Create a MemoryStream.
                var ms = new MemoryStream();

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                var cs = new CryptoStream(ms,
                    Algorithm.CreateEncryptor(Algorithm.Key, Algorithm.IV),
                    CryptoStreamMode.Write);

                // Write the byte array to the crypto stream and flush it.
                cs.Write(data, 0, length);
                cs.FlushFinalBlock();

                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                var ret = ms.ToArray();

                // Close the streams.
                cs.Close();
                ms.Close();

                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("A cryptographic error occured: {0}", ex.Message);
            }
            return null;
        }

        public string EncryptString(string text)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(text)));
        }

        public byte[] Decrypt(byte[] data) 
        { 
            return Decrypt(data, data.Length); 
        }

        public byte[] Decrypt(byte[] data, int length)
        {
            try
            {
                // Create a new MemoryStream using the passed 
                // array of encrypted data.
                var ms = new MemoryStream(data);

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                var cs = new CryptoStream(ms,
                    Algorithm.CreateDecryptor(Algorithm.Key, Algorithm.IV),
                    CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                var result = new byte[length];

                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                cs.Read(result, 0, result.Length);

                return result;
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("A cryptographic error occured: {0}", ex.Message);
            }

            return null;
        }

        public string DecryptString(string data)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(data))).TrimEnd('\0');
        }

        #endregion
    }
}
