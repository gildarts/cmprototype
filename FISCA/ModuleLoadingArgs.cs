using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA
{
    /// <summary>
    /// 代表模組載入驗證事件參數。
    /// </summary>
    public class ModuleLoadingArgs : EventArgs
    {
        internal ModuleLoadingArgs(string installationFolder, ModuleMetadata module)
        {
            Module = module;
            Cancel = false;
            InstallationFolder = installationFolder;
        }

        /// <summary>
        /// 模組顯示名稱。
        /// </summary>
        public string DisplayName
        {
            get
            {
                if (Module.Description == null)
                    return Module.Assembly.FullName;
                else
                    return Module.Description.GetAttribute("DisplayName");
            }
        }

        /// <summary>
        /// 模組資訊。
        /// </summary>
        public ModuleMetadata Module { get; private set; }

        /// <summary>
        /// 模組安裝路徑。
        /// </summary>
        public string InstallationFolder { get; private set; }

        /// <summary>
        /// 取得或設定是否取消載入模組。
        /// </summary>
        public bool Cancel { get; set; }
    }
}
