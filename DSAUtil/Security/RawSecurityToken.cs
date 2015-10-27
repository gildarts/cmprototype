using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.DSAUtil
{
    /// <summary>
    /// 代表自已手動建立的安全代符。
    /// </summary>
    public class RawSecurityToken : XmlBaseObject, ISecurityToken
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityToken"></param>
        public RawSecurityToken(XmlElement securityToken)
        {
            BaseNode = securityToken;
        }

        #region ISecurityToken 成員

        /// <summary>
        /// 取得安全代符的內容。
        /// </summary>
        /// <returns></returns>
        public XmlElement GetTokenContent()
        {
            return BaseNode;
        }

        /// <summary>
        /// 安全代符的類型。
        /// </summary>
        public string TokenType
        {
            get { return BaseNode.SelectSingleNode("@Type").InnerText; }
        }

        #endregion
    }
}
