using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.Deployment
{
    /// <summary>
    /// 
    /// </summary>
    public class BuildManifest
    {
        private static BuildManifest _null = new BuildManifest();
        /// <summary>
        /// 代表 Null 的 ModuleManifest。
        /// </summary>
        public static BuildManifest Null
        {
            get { return _null; }
        }

        /// <summary>
        /// 
        /// </summary>
        public BuildManifest()
        {
            Files = new FileCollection();
        }

        /// <summary>
        /// Manifest 中的所有檔案資訊，此屬性是唯讀，自行增加資料，將使此類別運作不正常。
        /// </summary>
        public FileCollection Files { get; private set; }

        /// <summary>
        /// 取得 Manifest 的版本。
        /// </summary>
        public string Version { get { return XmlTools.GetText(RawXml, "@Version"); } }

        /// <summary>
        /// 
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
            RawXml.OwnerDocument.Save(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return RawXml.OuterXml;
        }

        /// <summary>
        /// 
        /// </summary>
        public XmlElement RawXml { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static BuildManifest Load(string url)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(url);

            BuildManifest manifest = Load(doc.DocumentElement);
            manifest.Url = url;

            return manifest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BuildManifest Load(XmlElement data)
        {
            BuildManifest manifest = new BuildManifest();
            manifest.RawXml = data.CloneNode(true) as XmlElement;

            foreach (XmlElement each in data.SelectNodes("Files/File"))
                manifest.Files.Add(new File(each));

            return manifest;
        }
    }
}
