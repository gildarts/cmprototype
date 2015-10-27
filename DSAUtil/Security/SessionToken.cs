using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.DSAUtil
{
    using em = ErrorMessage;

    /// <summary>
    /// 代表內部的 SessionToken，此類別無法繼承。
    /// </summary>
    internal sealed class SessionToken : XmlBaseObject, IRenewableToken
    {
        internal const string SRV_Connect = "DS.Base.Connect";

        private ISecurityToken _originToken;
        private AccessPoint _originServer;

        public SessionToken(string sessionID, ISecurityToken originToken, AccessPoint originPoint)
        {
            _originToken = originToken;
            _originServer = originPoint;

            DSXmlHelper token = new DSXmlHelper("SecurityToken");
            token.SetAttribute(".", "Type", "Session");
            token.AddElement(".", "SessionID", sessionID);

            BaseNode = token.BaseElement;
        }

        /// <summary>
        /// 向特定的 Doorway 要求一個 Session。
        /// </summary>
        /// <param name="server">目的 Doorway。</param>
        /// <returns>合法的 SessionToken。如果未成功要求 SessionToken 會產生相對的 Exception。</returns>
        /// <exception cref="DSAException">要求 Session 產生錯誤時。</exception>
        /// <param name="token">要求 Session 的SecurityToken。</param>
        /// <param name="point"></param>
        /// <exception cref="OperationFailException">傳送「要求 SessionID」 的申請文件失敗時。</exception>
        /// <exception cref="NotSupportedException">發現 DSA Server 未回傳 SessionID時。</exception>
        public static SessionToken RequestSession(ISecurityToken token, AccessPoint accessPoint)
        {
            SessionToken newSession = null;

            DSRequest req = new DSRequest();
            req.TargetService = SRV_Connect;
            req.SecurityToken = token.GetTokenContent();

            //要求使用 Session 連線。
            req.SetContent("<RequestSessionID/>");

            DSResponse rsp = null;

            try
            {
                rsp = DSConnection.SendEnvelope(req, accessPoint);
            }
            catch (Exception ex)
            {
                throw new SendRequestException(em.Get("RequestSessionIDFail"), ex);
            }

            //成功的取得 DSResponse 並不代表 DSA Application 支援 Session 功能。
            if (!rsp.HasContent)
                throw new NotSupportedException(em.Get("SessionServiceNotSupported"));

            //如果根名稱不是 SessionID 代表 DSA Server 不支援 Session 功能。
            if (rsp.GetContent().RootName != "SessionID")
                throw new NotSupportedException(em.Get("SessionServiceNotSupported"));

            newSession = new SessionToken(rsp.GetContent().GetText("."), token, accessPoint);

            return newSession;
        }

        #region ISecurityToken 成員

        /// <summary>
        /// 重新要求新的 SessionToken。
        /// </summary>
        /// <exception cref="OperationFailException">要求新的 SessionID 失敗時。</exception>
        private void RenewToken(bool renewChildToken)
        {
            try
            {
                SessionToken pToken = RequestSession(_originToken, _originServer);
                Load(pToken.BaseNode);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && renewChildToken)
                {
                    DSAServerException dsaexp = ex.InnerException as DSAServerException;
                    if (dsaexp != null)
                    {
                        if (dsaexp.ServerStatus == DSAServerStatus.PassportExpire || dsaexp.ServerStatus == DSAServerStatus.SessionExpire)
                        {
                            IRenewableToken renewable = _originToken as IRenewableToken;
                            try
                            {
                                renewable.RenewToken();
                                RenewToken(false);
                            }
                            catch (Exception ex1)
                            {
                                throw new SecurityTokenException(em.Get("RenewSessionIDFail"), ex1);
                            }
                        }
                    }
                }
                throw new SecurityTokenException(em.Get("RenewSessionIDFail"), ex);
            }
        }

        public void RenewToken()
        {
            RenewToken(true);
        }

        public XmlElement GetTokenContent()
        {
            return BaseNode;
        }

        public string TokenType
        {
            get { return "Session"; }
        }

        public bool Reuseable
        {
            get { return false; }
        }

        #endregion
    }
}
