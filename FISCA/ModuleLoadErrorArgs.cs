using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA
{
    /// <summary>
    /// 模組載入錯誤相關參數。
    /// </summary>
    public class ModuleLoadErrorArgs : EventArgs
    {
        internal ModuleLoadErrorArgs(ModuleMetadata module, Exception error)
        {
            Module = module;
            Error = error;
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
        /// 錯誤訊息。
        /// </summary>
        public Exception Error { get; private set; }
    }
}
