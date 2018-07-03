using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Helpers.MD5
{
    /// <summary>
    /// Klasa zawierająca metodę do generowanie MD5 hash
    /// </summary>
    public static class MD5Generator
    {
        /// <summary>
        /// Metoda generująca MD5 hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CalculateMD5Hash(params string[] input)
        {
            // byte array representation of that string
            byte[] _encodedString = new UTF8Encoding().GetBytes(string.Join("", input));

            // need MD5 to calculate the hash
            byte[] _hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(_encodedString);

            // string representation (similar to UNIX format)
            return BitConverter.ToString(_hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();
        }
    }
}
