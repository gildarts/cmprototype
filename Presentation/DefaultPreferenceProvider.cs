using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.Presentation
{
    class DefaultPreferenceProvider:IPreferenceProvider
    {
        #region IPreferenceProvider 成員

        public System.Xml.XmlElement this[string Key]
        {
            get
            {
                return new XmlDocument().CreateElement(Key);
            }
            set
            {
                
            }
        }

        #endregion
    }
}
