using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA
{
    /// <summary>
    /// 設定相依的模組進入點名稱，被相依的進入點會先被執行。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class DependencyAttribute : Attribute
    {
        /// <summary>
        /// 指定要參考的模組進入點名稱(不分大小寫)，被參考的進入點，一定會先被乎叫。
        /// </summary>
        /// <param name="mainName">進入點名稱。</param>
        public DependencyAttribute(string mainName)
        {
            MainName = mainName;
        }

        /// <summary>
        /// 相依的模組進入點名稱。
        /// </summary>
        public string MainName { get; private set; }
    }
}
