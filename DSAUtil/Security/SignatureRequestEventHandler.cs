using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// 代表數位簽章的事件處理程序。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void SignatureRequestEventHandler(object sender, SignatureRequestEventArgs args);

    /// <summary>
    /// 代表數位簽章的要求。
    /// </summary>
    /// <param name="rawData">要簽章的原始資料。</param>
    /// <returns>數位簽章資料。</returns>
    public delegate byte[] SignatureRequest(byte[] rawData);

}
