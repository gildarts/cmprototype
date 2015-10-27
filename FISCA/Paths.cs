using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FISCA
{
    static class Paths
    {
        /// <summary>
        /// 可執行檔的完整路徑。
        /// </summary>
        public static string Executable
        {
            get { return new FileInfo(Application.ExecutablePath).DirectoryName; }
        }

        /// <summary>
        /// 存放 Kernel 檔案的完整路徑。
        /// </summary>
        public static string Kernel
        {
            get { return Path.Combine(Executable, "kernel"); }
        }

        /// <summary>
        /// 存放模組的完整路徑。
        /// </summary>
        public static string Module
        {
            get { return Path.Combine(Executable, "modules"); }
        }

        /// <summary>
        /// 取得暫存檔案名稱的完整路徑，除了檔案外的路徑由系統決定。
        /// </summary>
        /// <param name="fileName">檔案名稱。</param>
        /// <returns></returns>
        public static string GetTemporalFile(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
