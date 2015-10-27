using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    public class SchemaValidateException : Exception
    {
        public SchemaValidateException(string message, Exception InnerException)
            : base(message, InnerException)
        {
        }
    }
}
