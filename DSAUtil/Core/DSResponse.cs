/*
 * Create Date：2006/01/25
 * Author Name：YaoMing Huang
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.DSAUtil
{
    using em = ErrorMessage;

    /// <summary>
    /// 代表DSA的Response文件，由Envelope概念的延伸。
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
        /// 初始化<see cref="DSRequest"/>類別的新執行個體。
        /// </summary>
        public DSResponse()
            : base()
        {
            InitResponse();
        }

        /// <summary>
        /// 使用指定的字串，初始化<see cref="DSRequest"/>類別的新執行個體。
        /// </summary>
        /// <param name="bodyContent">Envelop中的Body內容，必須是含有「根」的Xml內容。</param>
        public DSResponse(string bodyContent)
            : base()
        {
            InitResponse();
            XmlElement elm = DSXmlHelper.LoadXml(bodyContent);
            SetContent(elm);
        }

        /// <summary>
        /// 使用指定的字串初始化<see cref="DSResponse"/>類別的新執行個體。
        /// </summary>
        /// <param name="bodyContent">Envelop中的Body內容。</param>
        public DSResponse(XmlDocument bodyContent)
            : base()
        {
            InitResponse();
            SetContent(bodyContent);
        }

        /// <summary>
        /// 依XmlElement物件初始化<see cref="DSResponse"/>類別的新執行個體。
        /// </summary>
        /// <param name="bodyContent">Envelop中的Body內容。</param>
        public DSResponse(XmlElement bodyContent)
            : base()
        {
            InitResponse();
            SetContent(bodyContent);
        }

        /// <summary>
        /// 依DSXmlHelper物件初始化<see cref="DSResponse"/>類別的新執行個體。
        /// </summary>
        /// <param name="bodyContent">Envelop中的Body內容。</param>
        public DSResponse(DSXmlHelper bodyContent)
            : base()
        {
            InitResponse();
            SetContent(bodyContent);
        }

        /// <summary>
        /// 依DSXmlCreator物件初始化<see cref="DSResponse"/>類別的新執行個體。
        /// </summary>
        /// <param name="bodyContent">Envelop中的Body內容。</param>
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
        /// 新增一個Fault到文件中。
        /// </summary>
        /// <param name="source">錯誤來源。</param>
        /// <param name="code">代碼。</param>
        /// <param name="message">訊息。</param>
        /// <param name="detail">詳細資訊。</param>
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
        /// 清除所有Fault資料。
        /// </summary>
        public void ClearFault()
        {
            envelope.GetElement(DSFaultPath).RemoveAll();
        }

        /// <summary>
        /// 取得 DSResponse 中的 Fault 資訊。
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
        /// 處理申請文件後回傳的狀態。
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
                //「d」是將 Status 這個 Enum 轉成數字顯示。
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
