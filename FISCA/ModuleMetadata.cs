using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;

namespace FISCA
{
    /// <summary>
    /// 代表模組的自我描述資訊。
    /// </summary>
    public class ModuleMetadata
    {
        internal ModuleMetadata(ModuleEntryPoint point)
        {
            EntryPoint = point;
            InstallationFolder = new System.IO.FileInfo(MainMethod.ReflectedType.Assembly.Location).DirectoryName;
        }

        private ModuleEntryPoint EntryPoint { get; set; }

        /// <summary>
        /// 模組進入點。
        /// </summary>
        public MethodInfo MainMethod { get { return EntryPoint.MainMethod; } }

        /// <summary>
        /// 包含進入點的組件。
        /// </summary>
        public Assembly Assembly { get { return EntryPoint.Assembly; } }

        /// <summary>
        /// 取得模組的安裝路徑。
        /// </summary>
        public string InstallationFolder { get; private set; }

        /// <summary>
        /// 取得模組版本。
        /// </summary>
        public string InstallationVersion
        {
            get
            {
                if (Manifest != null)
                    return Manifest.GetAttribute("Version");
                else
                    return "";
            }
        }

        /// <summary>
        /// 模組的檔案資訊。
        /// </summary>
        public XmlElement Manifest
        {
            get
            {
                if (EntryPoint.BuildManifest == null)
                    return FISCA.DSAUtil.DSXmlHelper.LoadXml("<Nothing/>");
                else
                    return EntryPoint.BuildManifest;
            }
        }

        /// <summary>
        /// 部署的資訊。
        /// </summary>
        public XmlElement Description
        {
            get
            {
                if (EntryPoint.ModuleDescription == null)
                    return FISCA.DSAUtil.DSXmlHelper.LoadXml("<Nothing/>");
                else
                    return EntryPoint.ModuleDescription;
            }
        }
    }
}
