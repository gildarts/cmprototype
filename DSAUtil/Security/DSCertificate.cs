using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using FISCA.DSAUtil;

namespace FISCA.DSAUtil
{
    /*
      <Content>
          <CertificateID>432</CertificateID> 
          <Issuer>tw.edu.tp.ca</Issuer> 
          <ValidFrom /> 
          <ValidTo /> 
          <Subject>tw.edu.tp.ca</Subject> 
          <Url>http://www.chvs.hcc.edu.tw/dsa/doorway.aspx</Url> 
          <PublicKey Type="RSA" /> 
      </Content>
      <ds:Signature xmlns:ds="http://www.w3.org/2000/09/xmldsig#" /> 
      </DSACertificate>
     */

    /// <summary>
    /// 代表DSA的憑證資訊。
    /// </summary>
    public class DSCertificate : XmlBaseObject
    {
        public DSCertificate(XmlElement certXml)
        {
            BaseNode = certXml;
        }

        public int CertificateID
        {
            get
            {
                return Convert.ToInt32(BaseNode.SelectSingleNode("Content/CertificateID").InnerText);
            }
        }

        public string Issuer
        {
            get
            {
                return BaseNode.SelectSingleNode("Content/Issuer").InnerText;
            }
        }

        public DateTime ValidFrom
        {
            get
            {
                return Convert.ToDateTime(BaseNode.SelectSingleNode("Content/ValidFrom").InnerText);
            }
        }

        public DateTime ValidTo
        {
            get
            {
                return Convert.ToDateTime(BaseNode.SelectSingleNode("Content/ValidTo").InnerText);
            }
        }

        public string Subject
        {
            get
            {
                return BaseNode.SelectSingleNode("Content/Subject").InnerText;
            }
        }

        public string Url
        {
            get
            {
                return BaseNode.SelectSingleNode("Content/Url").InnerText;
            }
        }

        public XmlElement PublicKeyXml
        {
            get
            {
                return BaseNode.SelectSingleNode("Content/PublicKey") as XmlElement;
            }
        }

        public XmlElement CertificateXml
        {
            get
            {
                return BaseNode;
            }
        }

        public XmlElement ContentXml
        {
            get
            {
                return BaseNode.SelectSingleNode("Content") as XmlElement;
            }
        }

        public XmlElement SignatureXml
        {
            get
            {
                XmlNamespaceManager nsmgr = new XmlNamespaceManager((XmlNameTable)new NameTable());
                nsmgr.AddNamespace("si", "http://www.w3.org/2000/09/xmldsig#"); //Xml Signature的Namespace。

                return BaseNode.SelectSingleNode("//si:Signature", nsmgr) as XmlElement;
            }
        }

        /// <summary>
        /// 驗證此憑證是否合法。
        /// </summary>
        /// <param name="cert">用來驗證此憑證的物件。</param>
        /// <returns>憑證合法回傳True。</returns>
        public bool IsValidate(DSCertificate cert)
        {
            //準備驗簽的Public Key。
            RSACryptoServiceProvider rsaCrypto = new RSACryptoServiceProvider();
            rsaCrypto.FromXmlString(cert.PublicKeyXml.OuterXml);

            return PkiUtilities.CheckSignature(rsaCrypto, SignatureXml, ContentXml.OuterXml);
        }
    }
}