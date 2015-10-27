using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// 這是Fault的說明
    /// </summary>
    public class Fault
    {
        private XmlElement _fault;

        /// <summary>
        /// 初始化<see cref="Fault"/>的新執行個體。
        /// </summary>
        /// <param name="fault">Fault 資訊。</param>
        public Fault(XmlElement fault)
        {
            _fault = fault;
        }

        /// <summary>
        /// 失敗的來源名稱。
        /// </summary>
        public string Source
        {
            get { return _fault.SelectSingleNode("Source").InnerText; }
        }

        /// <summary>
        /// 失敗的代碼。
        /// </summary>
        public string Code
        {
            get { return _fault.SelectSingleNode("Code").InnerText; }
        }

        /// <summary>
        /// 失敗說明。
        /// </summary>
        public string Message
        {
            get { return _fault.SelectSingleNode("Message").InnerText; }
        }

        /// <summary>
        /// 失敗的詳細資訊。
        /// </summary>
        public string Detail
        {
            get { return _fault.SelectSingleNode("Detail").InnerText; }
        }

        /// <summary>
        /// 如果還有其他錯誤，則回傳，否則回傳 Null 參考。
        /// </summary>
        public Fault InnerFault
        {
            get
            {
                //辨斷是否還有其他 Xml 元素。
                if (_fault.NextSibling != null)
                {
                    //辨斷是否元素名稱是 Fault 與是否是一個「元素」。
                    if (_fault.NextSibling.LocalName == "Fault" &&
                        _fault.NextSibling.NodeType == XmlNodeType.Element)
                        return new Fault((XmlElement)_fault.NextSibling);
                    else
                        return null;
                }
                else
                    return null;
            }
        }

        internal Exception GetInnerFaultException
        {
            get
            {
                if (InnerFault != null)
                    return new DSAFaultException(InnerFault);
                else
                    return null;
            }
        }
    }
}
