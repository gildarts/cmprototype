using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Deployment
{
    /// <summary>
    /// 代表一組檔案集合。
    /// </summary>
    public class FileCollection : List<File>
    {
        /// <summary>
        /// 
        /// </summary>
        public int TotalSize
        {
            get
            {
                int size = 0;

                foreach (File each in this)
                    size += each.Size;

                return size;
            }
        }
    }
}
