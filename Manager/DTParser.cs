using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manager
{
    class DTParser
    {
        public static string TryToStandard(string input)
        {
            DateTime dtout;

            if (DateTime.TryParse(input, out dtout))
                return dtout.ToString("yyyy/MM/dd HH:mm:ss");
            else
                return input;
        }
    }
}
