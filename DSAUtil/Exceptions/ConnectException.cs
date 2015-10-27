using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    public class ConnectException:Exception
    {
        public ConnectException(string message, Exception InnerException)
            : base(message, InnerException)
        {
        }
    }
}
