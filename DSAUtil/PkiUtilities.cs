using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace FISCA.DSAUtil
{
    public static class PkiUtilities
    {
        /// <summary>
        /// 對Xml資料進行數簽章。
        /// </summary>
        /// <param name="publicKey">簽章金鑰。</param>
        /// <param name="xmlData">要簽章的資料。</param>
        /// <returns>簽章完成的資料(不含原內容)。</returns>
        public static string ComputeSignature(RSACryptoServiceProvider privateKey, string xmlData)
        {
            XmlDocument signData = new XmlDocument();
            signData.PreserveWhitespace = true;
            signData.LoadXml(xmlData);

            SignedXml sx = new SignedXml(signData);
            sx.AddReference(new Reference(""));
            sx.SigningKey = privateKey;

            sx.ComputeSignature();

            return sx.GetXml().OuterXml;
        }

        /// <summary>
        /// 驗簽章是否正確。
        /// </summary>
        /// <param name="publicKey">驗證的公開金鑰。</param>
        /// <param name="xmlData">要驗證的資料。</param>
        /// <returns>正確回傳True。</returns>
        public static bool CheckSignature(RSACryptoServiceProvider publicKey, XmlElement signature, string xmlData)
        {
            XmlDocument signData = new XmlDocument();
            signData.PreserveWhitespace = true;
            signData.LoadXml(xmlData);

            SignedXml sx = new SignedXml(signData);
            sx.AddReference(new Reference(""));

            sx.LoadXml(signature);

            return sx.CheckSignature(publicKey);
        }

        /// <summary>
        /// 計算輸入資料的 SHA1 雜湊值。
        /// </summary>
        /// <param name="str">資料。</param>
        /// <returns>雜湊值。</returns>
        public static string HashString(string str)
        {
            SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();

            byte[] source = Encoding.UTF8.GetBytes(str);
            byte[] result = hasher.ComputeHash(source);

            return Convert.ToBase64String(result);
        }
    }
}
