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
    - <DSAPassport Version="1.0">
        - <Content>
              <PassportID>456</PassportID> 
              <Issuer>tw.edu.tp</Issuer> 
              <IssueInstant>cctt-mm-ddThh:mm:ss</IssueInstant> 
              <ValidTo>cctt-mm-ddThh:mm:ss</ValidTo> 
              <Subject>billyu</Subject> 
              <AuthMethod>Basic|Enhanced</AuthMethod> 
              <RequesterIP>203.72.205.11</RequesterIP> 
            - <Roles>
                  <Role>Teacher</Role> 
                  <Role>Administrator</Role> 
              </Roles>
            - <Attributes>
                  <Attribute1>Value1</Attribute1> 
                  <Attribute2>Value2</Attribute2> 
              </Attributes>
          </Content>
        <ds:Signature xmlns:ds="http://www.w3.org/2000/09/xmldsig#" /> 
      </DSAPassport>
     */

    /// <summary>
    /// �N��DSA�q���Ҫ�Xml��T�A�o�����O�����~�ӡC
    /// </summary>
    public sealed class DSPassport : XmlBaseObject
    {
        public DSPassport(XmlElement passportXml)
        {
            BaseNode = passportXml;
        }

        public string Version
        {
            get
            {
                return BaseNode.SelectSingleNode("@Version").InnerText;
            }
        }

        public int PassportID
        {
            get
            {
                return Convert.ToInt32(BaseNode.SelectSingleNode("Content/PassportID").InnerText);
            }
        }

        public string Issuer
        {
            get
            {
                return BaseNode.SelectSingleNode("Content/Issuer").InnerText;
            }
        }

        public DateTime IssueInstant
        {
            get
            {
                return Convert.ToDateTime(BaseNode.SelectSingleNode("Content/IssueInstant").InnerText);
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

        public string AuthMethod
        {
            get
            {
                return BaseNode.SelectSingleNode("Content/AuthMethod").InnerText;
            }
        }

        public string RequesterIP
        {
            get
            {
                return BaseNode.SelectSingleNode("Content/RequesterIP").InnerText;
            }
        }

        public XmlElement Roles
        {
            get
            {
                return BaseNode.SelectSingleNode("Content/Roles") as XmlElement;
            }
        }

        public XmlElement Attributes
        {
            get
            {
                return BaseNode.SelectSingleNode("Content/Attributes") as XmlElement;
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
                nsmgr.AddNamespace("si", "http://www.w3.org/2000/09/xmldsig#"); //Xml Signature��Namespace�C

                return BaseNode.SelectSingleNode("//si:Signature", nsmgr) as XmlElement;
            }
        }

        /// <summary>
        /// ���Ҧ�Passport�O�_�X�k�C
        /// </summary>
        /// <param name="cert">�Ψ����Ҧ�Passport������C</param>
        /// <returns>���ҦX�k�^��True�C</returns>
        public bool IsValidate(DSCertificate cert)
        {
            //�ǳ���ñ��Public Key�C
            RSACryptoServiceProvider rsaCrypto = new RSACryptoServiceProvider();
            rsaCrypto.FromXmlString(cert.PublicKeyXml.OuterXml);

            return PkiUtilities.CheckSignature(rsaCrypto, SignatureXml, ContentXml.OuterXml);
        }
    }
}
