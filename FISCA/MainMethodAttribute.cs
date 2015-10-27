using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA
{
    /// <summary>
    /// 代表模組進入點。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MainMethodAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public MainMethodAttribute()
        {
            Name = string.Empty;
        }

        /// <summary>
        /// 標示一個模組的進入點。
        /// </summary>
        /// <param name="name">為此進入點命名(不分大小寫)，讓其他模組可以參考。</param>
        public MainMethodAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 進入點名稱。
        /// </summary>
        public string Name { get; private set; }
    }
}
