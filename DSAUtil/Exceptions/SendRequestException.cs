using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    public class SendRequestException : Exception
    {
        public SendRequestException(string message, Exception InnerException)
            : base(message, InnerException)
        {
        }
    }
}
