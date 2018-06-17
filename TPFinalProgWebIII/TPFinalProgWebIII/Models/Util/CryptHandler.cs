using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

/*Visto en: stackoverflow.com/questions/9031537/really-simple-encryption-with-c-sharp-and-symmetricalgorithm
 * Para la cookie. El enunciado dice simplemente encriptar la cookie, no habla de algún 
 * algoritmo en particular */

namespace TPFinalProgWebIII.Models.Util {
    public static class CookieHandler {
        private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        public static string Crypt(string valor)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = System.Text.Encoding.Unicode.GetBytes(valor);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string Decrypt(string valor)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(valor);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return System.Text.Encoding.Unicode.GetString(outputBuffer);
        }
    }
}