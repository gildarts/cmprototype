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
    using em = ErrorMessage;

    /// <summary>
    /// �N��DSA��Response���A��Envelope�����������C
    /// </summary>
    public class DSResponse : DSEnvelope
    {
        private const string DSFaultName = "DSFault";
        private const string DSFaultPath = HeaderName + "/DSFault";
        private const string FaultName = "Fault";
        private const string FaultPath = HeaderName + "/" + DSFaultName + "/Fault";
        private const string StatusCodePath = "Status/Code";
        private const string StatusMessagePath = "Status/Message";

        /// <summary>
        /// ��l��<see cref="DSRequest"/>���O���s�������C
        /// </summary>
        public DSResponse()
            : base()
        {
            InitResponse();
        }

        /// <summary>
        /// �ϥΫ��w���r��A��l��<see cref="DSRequest"/>���O���s�������C
        /// </summary>
        /// <param name="bodyContent">Envelop����Body���e�A�����O�t���u�ڡv��Xml���e�C</param>
        public DSResponse(string bodyContent)
            : base()
        {
            InitResponse();
            XmlElement elm = DSXmlHelper.LoadXml(bodyContent);
            SetContent(elm);
        }

        /// <summary>
        /// �ϥΫ��w���r���l��<see cref="DSResponse"/>���O���s�������C
        /// </summary>
        /// <param name="bodyContent">Envelop����Body���e�C</param>
        public DSResponse(XmlDocument bodyContent)
            : base()
        {
            InitResponse();
            SetContent(bodyContent);
        }

        /// <summary>
        /// ��XmlElement�����l��<see cref="DSResponse"/>���O���s�������C
        /// </summary>
        /// <param name="bodyContent">Envelop����Body���e�C</param>
        public DSResponse(XmlElement bodyContent)
            : base()
        {
            InitResponse();
            SetContent(bodyContent);
        }

        /// <summary>
        /// ��DSXmlHelper�����l��<see cref="DSResponse"/>���O���s�������C
        /// </summary>
        /// <param name="bodyContent">Envelop����Body���e�C</param>
        public DSResponse(DSXmlHelper bodyContent)
            : base()
        {
            InitResponse();
            SetContent(bodyContent);
        }

        /// <summary>
        /// ��DSXmlCreator�����l��<see cref="DSResponse"/>���O���s�������C
        /// </summary>
        /// <param name="bodyContent">Envelop����Body���e�C</param>
        public DSResponse(DSXmlCreator bodyContent)
            : base()
        {
            InitResponse();
            SetContent(bodyContent.GetResultAsDSXmlHelper());
        }

        private void InitResponse()
        {
            envelope.AddElement(HeaderName, "Status");
            envelope.AddElement(HeaderName + "/Status", "Code", "0");
            envelope.AddElement(HeaderName + "/Status", "Message");
            envelope.AddElement(HeaderName, DSFaultName);
        }

        /// <summary>
        /// �s�W�@��Fault���󤤡C
        /// </summary>
        /// <param name="source">���~�ӷ��C</param>
        /// <param name="code">�N�X�C</param>
        /// <param name="message">�T���C</param>
        /// <param name="detail">�ԲӸ�T�C</param>
        public void AddFault(string source, string code, string message, string detail)
        {
            DSXmlHelper helper;
            if (envelope.PathExist(DSFaultPath))
                helper = new DSXmlHelper(envelope.GetElement(DSFaultPath));
            else
                helper = new DSXmlHelper(envelope.AddElement(HeaderName, DSFaultName));

            helper.AddElement(FaultName);
            helper.AddElement(FaultName, "Source", source);
            helper.AddElement(FaultName, "Code", code);
            helper.AddElement(FaultName, "Message", message);
            helper.AddElement(FaultName, "Detail", detail);
        }

        /// <summary>
        /// �M���Ҧ�Fault��ơC
        /// </summary>
        public void ClearFault()
        {
            envelope.GetElement(DSFaultPath).RemoveAll();
        }

        /// <summary>
        /// ���o DSResponse ���� Fault ��T�C
        /// </summary>
        /// <returns></returns>
        public Fault GetFault()
        {
            if (envelope.PathExist(FaultPath))
                return new Fault(envelope.GetElement(FaultPath));
            else
                return null;
        }

        /// <summary>
        /// �B�z�ӽФ���^�Ǫ����A�C
        /// </summary>
        public DSAServerStatus Status
        {
            get
            {
                string status = GetHeader("Status/Code").InnerText;

                if (status == string.Empty)
                    return DSAServerStatus.Unknow;
                else
                {
                    try
                    {
                        return (DSAServerStatus)Enum.Parse(typeof(DSAServerStatus), status);
                    }
                    catch (ArgumentException ex)
                    {
                        throw new Exception(em.Get("ConvertDSAServerStatusError"), ex);
                    }
                }
            }
            set
            {
                //�ud�v�O�N Status �o�� Enum �ন�Ʀr��ܡC
                SetHeader("Status/Code", value.ToString("d"));
            }
        }

        public string StatusMessage
        {
            get
            {
                return GetHeader(StatusMessagePath).InnerText;
            }
            set
            {
                SetHeader(StatusMessagePath, value);
            }
        }
    }
}
