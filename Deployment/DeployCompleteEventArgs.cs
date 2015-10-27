using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Deployment
{
    /// <summary>
    /// 
    /// </summary>
    public class DeployCompleteEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public DeployCompleteEventArgs()
        {
            Files = new FileCollection();
        }

        /// <summary>
        /// 取得更新是否成功。
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 取得更新的錯誤訊息，如果沒有錯誤則此屬性為 Null。
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// 取得更新的檔案資訊。
        /// </summary>
        public FileCollection Files { get; set; }

        /// <summary>
        /// 模組的下載路徑。
        /// </summary>
        public string ModuleUrl { get; set; }

        /// <summary>
        /// 模組安裝路徑。
        /// </summary>
        public string InstallFolder { get; set; }

        /// <summary>
        /// 模組的 Manifest。
        /// </summary>
        public BuildManifest Manifest { get; set; }

        /// <summary>
        /// 模組的 Deploy.xml 資訊。
        /// </summary>
        public ModuleDescription Description { get; set; }
    }
}
