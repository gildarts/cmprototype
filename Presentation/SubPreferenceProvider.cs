using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.Presentation
{
    class SubPreferenceProvider:IPreferenceProvider
    {
        private string _ChildName;
        private IPreferenceProvider _Parent;
        public SubPreferenceProvider(string child, IPreferenceProvider parent)
        {
            _ChildName = child;
            _Parent = parent;
        }
        #region IPreferenceProvider 成員

        public System.Xml.XmlElement this[string Key]
        {
            get
            {
                XmlElement PreferenceElement = null;
                if ( _Parent != null )
                    PreferenceElement = _Parent[_ChildName + "___" + Key];
                if ( PreferenceElement == null )
                {
                    PreferenceElement = new XmlDocument().CreateElement(_ChildName + "___" + Key);
                }
                return PreferenceElement;
            }
            set
            {
                if ( _Parent != null )
                    _Parent[_ChildName + "___" + Key] = value;
            }
        }

        #endregion
    }
}
