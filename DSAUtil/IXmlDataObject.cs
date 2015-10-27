using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.DSAUtil
{
    public interface IXmlDataObject
    {
        void Initialize(XmlNode baseXml);
    }
}
