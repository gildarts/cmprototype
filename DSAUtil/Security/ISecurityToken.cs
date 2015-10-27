using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Text;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// 代表DSA的安全代符(SecurityToken)。
    /// </summary>
    public interface ISecurityToken
    {
        /// <summary>
        /// 取得SecurityToken的內容，必需確保取出的Token是合法的。
        /// </summary>
        XmlElement GetTokenContent();

        /// <summary>
        /// 代表Token的類型，例如：Basic、Enhanced、DSAPassport等。
        /// </summary>
        string TokenType { get;}

        ///// <summary>
        ///// 指示此 SecurityToken 是否可以跨 DSA Application 重覆使用，
        ///// 例如：PassportToken，不可重覆使用如：SessionToken。
        ///// </summary>
        //bool Reuseable { get;}

    }//end ISecurityToken
}