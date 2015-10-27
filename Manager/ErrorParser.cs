using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FISCA.DSA;

namespace Manager
{
    static class ErrorParser
    {
        public static bool TryGetSqlException(Exception ex, out string sqlMsg)
        {
            Regex x = new Regex(".*SQLException:(.*)");
            sqlMsg = string.Empty;

            string errmsg = string.Empty;
            if (ex is DSAServerException)
            {
                errmsg = (ex as DSAServerException).Response;
            }
            else
                return false;

            Match m = x.Match(errmsg);
            if (m.Success)
            {
                sqlMsg = m.Groups[1].Value.Trim();
                return true;
            }
            else
                return false;
        }
    }
}
