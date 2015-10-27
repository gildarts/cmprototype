using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// �N�� DSA Server �����~�C
    /// </summary>
    public class DSAServerException : Exception
    {
        private DSAServerStatus _status;
        private string _detailMessage;

        public DSAServerException(DSAServerStatus status, string message, string detailMessage, Exception innerException)
            : base(message, innerException)
        {
            _status = status;
            _detailMessage = detailMessage;
        }

        public DSAServerException(DSAServerStatus status, string message, string detailMessage)
            : base(message)
        {
            _status = status;
            _detailMessage = detailMessage;
        }

        public DSAServerException(DSAServerStatus status, string message, Exception innerException)
            : base(message, innerException)
        {
            _status = status;
            _detailMessage = "";
        }

        public DSAServerException(DSAServerStatus status, string message)
            : base(message)
        {
            _status = status;
            _detailMessage = "";
        }

        /// <summary>
        /// ���ε{�������~���A�C
        /// </summary>
        public DSAServerStatus ServerStatus
        {
            get { return _status; }
        }

        public string DetailMessage
        {
            get { return _detailMessage; }
        }
    }
}
