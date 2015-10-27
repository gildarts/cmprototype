using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Authentication
{
    /// <summary>
    /// 標上此屬性的方法或類別中的方法在CallService時若發生Exception則不會理會錯誤
    /// </summary>
    public class LeaveOnErrorAttribute:Attribute
    {
    }
}
