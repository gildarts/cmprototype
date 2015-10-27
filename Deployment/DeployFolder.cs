using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Deployment
{
    /// <summary>
    /// 
    /// </summary>
    public enum FolderType
    {
        /// <summary>
        /// 直接將檔案 Deploy 到此目錄。
        /// </summary>
        TargetFolder,
        /// <summary>
        /// 此目錄只是基礎目錄，還要加上模組資訊，才是要 Deploy 的目錄。
        /// </summary>
        BaseFolder
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeployFolder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="type"></param>
        public DeployFolder(string location, FolderType type)
        {
            Location = location;
            Type = type;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FolderType Type { get; set; }
    }
}
