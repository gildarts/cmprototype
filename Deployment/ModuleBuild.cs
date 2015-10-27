using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.Deployment
{
    /// <summary>
    /// 
    /// </summary>
    public class ModuleBuild
    {
        private static ModuleBuild _null = new ModuleBuild();
        /// <summary>
        /// 代表 Null 的 ModuleBuild。
        /// </summary>
        public static ModuleBuild Null
        {
            get { return _null; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ModuleBuild()
        {
            Urls = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public ModuleBuild(XmlElement data)
            : this()
        {
            Name = XmlTools.GetText(data, "@Name");

            foreach (XmlNode each in data.SelectNodes("Url"))
                Urls.Add(each.InnerText);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Urls { get; private set; }
    }
}
