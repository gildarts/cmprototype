using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA
{
    /// <summary>
    /// 代表應用程式進入點，在系統啟動過程中，會是第一個被執行的方法。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ApplicationMainAttribute : MainMethodAttribute
    {
    }
}
