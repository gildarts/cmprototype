using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    public class DSAFaultException : Exception
    {
        private string _source;
        private string _code;
        private string _detail;

        public DSAFaultException(Fault fault)
            : base(fault.Message, fault.GetInnerFaultException)
        {
            _source = fault.Source;
            _code = fault.Code;
            _detail = fault.Detail;
        }

        public DSAFaultException(string source, string code, string message)
            : base(message)
        {
            _source = source;
            _code = code;
        }

        public new string Source
        {
            get { return _source; }
        }

        public string Code
        {
            get { return _code; }
        }

        public string Detail
        {
            get { return _detail; }
        }
    }
}
