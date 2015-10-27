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
        /// ��Xml��ƶi���ñ���C
        /// </summary>
        /// <param name="publicKey">ñ�����_�C</param>
        /// <param name="xmlData">�nñ������ơC</param>
        /// <returns>ñ�����������(���t�줺�e)�C</returns>
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
        /// ��ñ���O�_���T�C
        /// </summary>
        /// <param name="publicKey">���Ҫ����}���_�C</param>
        /// <param name="xmlData">�n���Ҫ���ơC</param>
        /// <returns>���T�^��True�C</returns>
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
        /// �p���J��ƪ� SHA1 ����ȡC
        /// </summary>
        /// <param name="str">��ơC</param>
        /// <returns>����ȡC</returns>
        public static string HashString(string str)
        {
            SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();

            byte[] source = Encoding.UTF8.GetBytes(str);
            byte[] result = hasher.ComputeHash(source);

            return Convert.ToBase64String(result);
        }
    }
}
