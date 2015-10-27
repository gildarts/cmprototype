using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using IO = System.IO;
using System.Net;

namespace FISCA.Deployment
{
    /// <summary>
    /// 
    /// </summary>
    public class ModuleDescription
    {
        private static ModuleDescription _null = new ModuleDescription();
        /// <summary>
        /// 代表空的 DeployDescription。
        /// </summary>
        public static ModuleDescription Null
        {
            get { return _null; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ModuleDescription()
        {
            Builds = new List<ModuleBuild>();
        }

        /// <summary>
        /// 取得設定部署到 Local 時的目錄名稱。
        /// </summary>
        public string DeployFolder { get { return XmlTools.GetText(RawXml, "@DeployFolder"); } }

        /// <summary>
        /// 取得模組的顯示名稱。
        /// </summary>
        public string DisplayName { get { return XmlTools.GetText(RawXml, "@DisplayName"); } }

        /// <summary>
        /// 取得模組的描述。
        /// </summary>
        public string Description { get { return XmlTools.GetText(RawXml, "Description"); } }

        /// <summary>
        /// 取得顯示圖示的網址。
        /// </summary>
        public string IconUrl { get { return XmlTools.GetText(RawXml, "IconUrl"); } }

        /// <summary>
        /// 取得模組詳細說明的位置。
        /// </summary>
        public string DetailInformationUrl { get { return XmlTools.GetText(RawXml, "DetailInformationUrl"); } }

        private List<ModuleBuild> Builds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ModuleBuild GetBuild(string name)
        {
            string requireBuild = name.ToLower();

            ModuleBuild foundBuild = ModuleBuild.Null;
            foreach (ModuleBuild each in Builds)
            {
                if (requireBuild == each.Name.ToLower())
                {
                    foundBuild = each;
                    break;
                }
            }

            if (foundBuild == ModuleBuild.Null && (requireBuild == Consts.ReleaseBuild || requireBuild == Consts.TestBuild))
            {//預設會有 Release、Test 這兩個 Build。
                foundBuild = new ModuleBuild();
                foundBuild.Name = name;
            }

            return foundBuild;
        }

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
        public XmlElement RawXml { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return RawXml.OuterXml;
        }

        #region Static Methods

        /// <summary>
        /// 從 Url 載入部署設定檔。
        /// </summary>
        /// <param name="url">模組位置。</param>
        /// <returns>如果沒有寫 module.xml 會回傳 DeployDescription.Null。</returns>
        public static ModuleDescription Load(ModuleUrl url)
        {
            return ModuleDescription.Load(url.Url);
        }

        /// <summary>
        /// 從 Url 直接載入部署設定檔。
        /// </summary>
        /// <param name="moduleUrl">設定檔的 Url。</param>
        public static ModuleDescription Load(string moduleUrl)
        {
            XmlDocument doc = new XmlDocument();
            ModuleUrl url = new ModuleUrl(moduleUrl);
            Console.WriteLine(string.Format("下載 module.xml {0}", url.GetDeployDescriptionUrl()));
            doc.Load(url.GetDeployDescriptionUrl());

            ModuleDescription deploy = Load(doc.DocumentElement);

            return deploy;
        }

        /// <summary>
        /// 載入部署設定檔。
        /// </summary>
        /// <param name="data">設定檔資料。</param>
        private static ModuleDescription Load(XmlElement data)
        {
            ModuleDescription deploydesc = new ModuleDescription();
            deploydesc.RawXml = data.CloneNode(true) as XmlElement;

            //設定 ModuleBuild 物件集合屬性。
            foreach (XmlElement each in deploydesc.RawXml.SelectNodes("Builds/Build"))
                deploydesc.Builds.Add(new ModuleBuild(each));

            return deploydesc;
        }
        #endregion
    }
}
