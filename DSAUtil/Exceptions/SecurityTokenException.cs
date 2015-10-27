using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    public class SecurityTokenException : Exception
    {
        public SecurityTokenException(string message, Exception InnerException)
            : base(message, InnerException)
        {
        }
    }
}
