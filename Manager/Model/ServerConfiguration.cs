using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSA;
using System.Windows.Forms;

namespace Manager
{
    enum LogOpportunity
    {
        Always,
        Never,
        OccuredException
    }

    /// <summary>
    /// 代表 DSA Server 的組態資訊。
    /// </summary>
    class ServerConfiguration
    {
        private XmlElement BaseXml { get; set; }

        public ServerConfiguration()
        {
            BaseXml = null;
        }

        /// <summary>
        /// 取得內部的原生 Xml 資料。
        /// </summary>
        /// <returns></returns>
        public XmlElement GetRawXmlData()
        {
            return BaseXml.CloneNode(true) as XmlElement;
        }

        /// <summary>
        /// 當呼叫 FromXml 時發生，但第一次呼叫不會發生此事件。
        /// </summary>
        public event EventHandler Changed;

        public void FromXml(XmlElement config)
        {
            BaseXml = config;
            Log = new LogConfiguration(BaseXml.SelectSingleNode("Property[@Name='DSALog']/Log") as XmlElement);

            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        public XmlElement ToXml()
        {
            Log.UpdateValues();
            return BaseXml.CloneNode(true) as XmlElement;
        }

        /// <summary>
        /// 取得或設定 Service 線上更新位置。
        /// </summary>
        public UrlData ServiceDefinitionUpdateUrl
        {
            get
            {
                FISCA.XHelper parser = new FISCA.XHelper(BaseXml.SelectSingleNode("Property[@Name='UpdateCenter']/UpdateCenter/AppDeploy") as XmlElement);
                
                string userName = parser.GetText("@UserName");
                string password = parser.GetText("@Password");
                string url = parser.GetText(".");

                return new UrlData(userName, password, url);
            }
            set
            {
                FISCA.XHelper parser = new FISCA.XHelper(BaseXml.SelectSingleNode("Property[@Name='UpdateCenter']/UpdateCenter/AppDeploy") as XmlElement);

                parser.SetAttribute(".", "UserName", value.UserName);
                parser.SetAttribute(".", "Password", value.Password);
                parser.SetInnerXml(".", value.Url);
            }
        }

        /// <summary>
        /// 取得或設定 Components 線上更新位置。
        /// </summary>
        public UrlData ComponentUpdateUrl
        {
            get
            {
                FISCA.XHelper parser = new FISCA.XHelper(BaseXml.SelectSingleNode("Property[@Name='UpdateCenter']/UpdateCenter/Components") as XmlElement);

                string userName = parser.GetText("@UserName");
                string password = parser.GetText("@Password");
                string url = parser.GetText(".");

                return new UrlData(userName, password, url);
            }
            set
            {
                FISCA.XHelper parser = new FISCA.XHelper(BaseXml.SelectSingleNode("Property[@Name='UpdateCenter']/UpdateCenter/Components") as XmlElement);

                parser.SetAttribute(".", "UserName", value.UserName);
                parser.SetAttribute(".", "Password", value.Password);
                parser.SetInnerXml(".", value.Url);
            }
        }

        /// <summary>
        /// 取得或設定負載平衡的 DSAServer Url 位置。
        /// 例：http://localhost:8080/is4/manager。
        /// 設定時請指定整個屬性，不要直接呼叫 Add 方法。
        /// </summary>
        public HashSet<string> LoadBalances
        {
            get
            {
                FISCA.XHelper parser = new FISCA.XHelper(BaseXml.SelectSingleNode("Property[@Name='LoadBalanceSetup']/LoadBalanceSetup") as XmlElement);

                HashSet<string> hs = new HashSet<string>();
                foreach (XmlElement each in parser.GetElements("ServerManagerURL"))
                    hs.Add(each.InnerText);

                return hs;
            }
            set
            {
                FISCA.XHelper parser = new FISCA.XHelper(BaseXml.SelectSingleNode("Property[@Name='LoadBalanceSetup']/LoadBalanceSetup") as XmlElement);
                parser.Data.RemoveAll();

                foreach (string each in value)
                    parser.AddElement(".", "ServerManagerURL", each);
            }
        }

        public LogConfiguration Log { get; private set; }
    }
}
