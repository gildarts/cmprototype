using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.Deployment
{
    internal static class XmlTools
    {
        public static string GetText(XmlNode node, string xpath)
        {
            XmlNode result = node.SelectSingleNode(xpath);

            if (result != null)
                return result.InnerText;
            else
                return string.Empty;
        }

        public static int GetInteger(XmlNode node, string xpath)
        {
            XmlNode result = node.SelectSingleNode(xpath);

            if (result != null)
                return result.InnerText == string.Empty ? 0 : int.Parse(result.InnerText);
            else
                return 0;
        }
    }
}
