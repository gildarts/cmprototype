using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Manager
{
    static class Password
    {
        public static string Protected(string password)
        {
            if (string.IsNullOrEmpty(password)) return string.Empty;

            byte[] plain = Encoding.UTF8.GetBytes(password);

            string key = "ischool cloud";
            byte[] cipher = ProtectedData.Protect(plain, Encoding.UTF8.GetBytes(key), DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(cipher);
        }

        public static string UnProtected(string base64String)
        {
            if (string.IsNullOrEmpty(base64String)) return string.Empty;

            string key = "ischool cloud";
            byte[] cipher = Convert.FromBase64String(base64String);
            byte[] plain = ProtectedData.Unprotect(cipher, Encoding.UTF8.GetBytes(key), DataProtectionScope.CurrentUser);

            return Encoding.UTF8.GetString(plain);
        }

        public static string EncryptoData(string plain, string key)
        {
            AesKey ak = new AesKey(key);
            AesManaged aes = new AesManaged();
            aes.IV = ak.IV;
            aes.Key = ak.Key;
            ICryptoTransform transform = aes.CreateEncryptor();

            byte[] plainbinary = Encoding.UTF8.GetBytes(plain);
            byte[] cipher = transform.TransformFinalBlock(plainbinary, 0, plainbinary.Length);
            aes.Clear();

            return Convert.ToBase64String(cipher);
        }

        public static string DecryptoData(string cipher, string key)
        {
            AesKey ak = new AesKey(key);
            AesManaged aes = new AesManaged();
            aes.IV = ak.IV;
            aes.Key = ak.Key;
            ICryptoTransform transform = aes.CreateDecryptor();

            byte[] cipherbinary = Convert.FromBase64String(cipher);
            byte[] plain = transform.TransformFinalBlock(cipherbinary, 0, cipherbinary.Length);
            aes.Clear();

            return Encoding.UTF8.GetString(plain);
        }
    }
}
