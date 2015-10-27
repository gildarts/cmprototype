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
    /// <summary>
    /// 代表DSA的Request文件，由Envelope概念的延伸。
    /// </summary>
    /// <remarks>此類別與前版有較大的差異，使用前請詳細參考範例。</remarks>
    public class DSRequest : DSEnvelope
    {
        private const string TargetContractElementName = "TargetContract";
        private const string TargetServiceElementName = "TargetService";
        private const string SecurityTokenElementName = "SecurityToken";

        /// <summary>
        /// 初始化 <see cref="DSRequest"/> 類別的新執行個體。 
        /// </summary>
        public DSRequest()
            : base()
        {
            InitRequest();
        }

        /// <summary>
        /// 使用指定的字串，初始化<see cref="DSRequest"/>類別的新執行個體。
        /// </summary>
        /// <param name="bodyContent">Envelop中的Body內容，必須是含有「根」的Xml內容。</param>
        public DSRequest(string bodyContent)
            : base()
        {
            InitRequest();
            XmlElement elm = DSXmlHelper.LoadXml(bodyContent);
            SetContent(elm);
        }

        /// <summary>
        /// 依XmlElement物件初始化<see cref="DSRequest"/>類別的新執行個體。
        /// </summary>
        /// <param name="bodyContent">Envelop中的Body內容。</param>
        /// <remarks>在產生新執行體後，bodyContent參數物件將與原有的參考分離，修改原有
        /// 物件並不會影響此行體的內容。</remarks>
        public DSRequest(XmlDocument bodyContent)
            : base()
        {
            InitRequest();
            SetContent(bodyContent);
        }

        /// <summary>
        /// 依XmlElement物件初始化<see cref="DSRequest"/>類別的新執行個體。
        /// </summary>
        /// <param name="bodyContent">Envelop中的Body內容。</param>
        /// <remarks>在產生新執行體後，bodyContent參數物件將與原有的參考分離，修改原有
        /// 物件並不會影響此行體的內容。</remarks>
        public DSRequest(XmlElement bodyContent)
            : base()
        {
            InitRequest();
            SetContent(bodyContent);
        }

        /// <summary>
        /// 依DSXmlHelper物件初始化<see cref="DSRequest"/>類別的新執行個體。
        /// </summary>
        /// <param name="bodyContent">Envelop中的Body內容。</param>
        /// <remarks>在產生新執行體後，bodyContent參數物件將與原有的參考分離，修改原有
        /// 物件並不會影響此行體的內容。</remarks>
        public DSRequest(DSXmlHelper bodyContent)
            : base()
        {
            InitRequest();
            SetContent(bodyContent);
        }

        /// <summary>
        /// 依DSXmlCreator物件初始化<see cref="DSRequest"/>類別的新執行個體。
        /// </summary>
        /// <param name="bodyContent">Envelop中的Body內容。</param>
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
