using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// �N�� Xml �B�z���~�C
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
