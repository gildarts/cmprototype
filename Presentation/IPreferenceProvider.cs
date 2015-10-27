using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.Presentation
{
    /// <summary>
    /// 使用者設定資料的處理器
    /// </summary>
    public interface IPreferenceProvider
    {
        /// <summary>
        /// 取得或設定，特定名稱的設定值
        /// </summary>
        /// <param name="Key">設定值名稱</param>
        XmlElement this[string Key] { get; set; } 
    }
}
