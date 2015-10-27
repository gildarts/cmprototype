/*
 * Create Date�G2006/01/25
 * Author Name�GYaoMing Huang
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// �N��DSA��Request���A��Envelope�����������C
    /// </summary>
    /// <remarks>�����O�P�e�������j���t���A�ϥΫe�иԲӰѦҽd�ҡC</remarks>
    public class DSRequest : DSEnvelope
    {
        private const string TargetContractElementName = "TargetContract";
        private const string TargetServiceElementName = "TargetService";
        private const string SecurityTokenElementName = "SecurityToken";

        /// <summary>
        /// ��l�� <see cref="DSRequest"/> ���O���s�������C 
        /// </summary>
        public DSRequest()
            : base()
        {
            InitRequest();
        }

        /// <summary>
        /// �ϥΫ��w���r��A��l��<see cref="DSRequest"/>���O���s�������C
        /// </summary>
        /// <param name="bodyContent">Envelop����Body���e�A�����O�t���u�ڡv��Xml���e�C</param>
        public DSRequest(string bodyContent)
            : base()
        {
            InitRequest();
            XmlElement elm = DSXmlHelper.LoadXml(bodyContent);
            SetContent(elm);
        }

        /// <summary>
        /// ��XmlElement�����l��<see cref="DSRequest"/>���O���s�������C
        /// </summary>
        /// <param name="bodyContent">Envelop����Body���e�C</param>
        /// <remarks>�b���ͷs�������AbodyContent�Ѽƪ���N�P�즳���ѦҤ����A�ק�즳
        /// ����ä��|�v�T�����骺���e�C</remarks>
        public DSRequest(XmlDocument bodyContent)
            : base()
        {
            InitRequest();
            SetContent(bodyContent);
        }

        /// <summary>
        /// ��XmlElement�����l��<see cref="DSRequest"/>���O���s�������C
        /// </summary>
        /// <param name="bodyContent">Envelop����Body���e�C</param>
        /// <remarks>�b���ͷs�������AbodyContent�Ѽƪ���N�P�즳���ѦҤ����A�ק�즳
        /// ����ä��|�v�T�����骺���e�C</remarks>
        public DSRequest(XmlElement bodyContent)
            : base()
        {
            InitRequest();
            SetContent(bodyContent);
        }

        /// <summary>
        /// ��DSXmlHelper�����l��<see cref="DSRequest"/>���O���s�������C
        /// </summary>
        /// <param name="bodyContent">Envelop����Body���e�C</param>
        /// <remarks>�b���ͷs�������AbodyContent�Ѽƪ���N�P�즳���ѦҤ����A�ק�즳
        /// ����ä��|�v�T�����骺���e�C</remarks>
        public DSRequest(DSXmlHelper bodyContent)
            : base()
        {
            InitRequest();
            SetContent(bodyContent);
        }

        /// <summary>
        /// ��DSXmlCreator�����l��<see cref="DSRequest"/>���O���s�������C
        /// </summary>
        /// <param name="bodyContent">Envelop����Body���e�C</param>
        public DSRequest(DSXmlCreator bodyContent)
            : base()
        {
            InitRequest();
            SetContent(bodyContent.GetResultAsDSXmlHelper());
        }

        private void InitRequest()
        {
            envelope.AddElement(HeaderName, TargetContractElementName);
            envelope.AddElement(HeaderName, TargetServiceElementName);
            envelope.AddElement(HeaderName, SecurityTokenElementName);
            envelope.SetAttribute(HeaderName + "/" + SecurityTokenElementName, "Type", "Basic");
            envelope.AddElement(HeaderName + "/" + SecurityTokenElementName, "UserName", "");
            envelope.AddElement(HeaderName + "/" + SecurityTokenElementName, "Password", "");
        }

        public string TargetContract
        {
            get
            {
                return GetHeader(TargetContractElementName).InnerText;
            }
            set
            {
                SetHeader(TargetContractElementName, value);
            }
        }

        public string TargetService
        {
            get
            {
                return GetHeader(TargetServiceElementName).InnerText;
            }
            set
            {
                SetHeader(TargetServiceElementName, value);
            }
        }

        public XmlElement SecurityToken
        {
            get
            {
                return GetHeader(SecurityTokenElementName);
            }
            set
            {
                SetHeader(value);
            }
        }
    }
}
