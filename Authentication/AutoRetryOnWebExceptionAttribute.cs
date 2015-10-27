using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.Authentication
{
    /// <summary>
    /// 標上此屬性的方法或類別中的方法在CallService時若發生WebException則將自動重試
    /// </summary>
    public class AutoRetryOnWebExceptionAttribute : Attribute
    {
    }
}
