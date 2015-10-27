using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// 代表 Xml 處理錯誤。
    /// </summary>
    public class XmlProcessException : Exception
    {
        public XmlProcessException(string message)
            : base(message)
        {
        }

        public XmlProcessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        private string _raw_string;
        public string RawString
        {
            get { return _raw_string; }
            set { _raw_string = value; }
        }

    }
}
