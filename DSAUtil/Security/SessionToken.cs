using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FISCA.DSAUtil
{
    using em = ErrorMessage;

    /// <summary>
    /// �N������ SessionToken�A�����O�L�k�~�ӡC
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
        /// �V�S�w�� Doorway �n�D�@�� Session�C
        /// </summary>
        /// <param name="server">�ت� Doorway�C</param>
        /// <returns>�X�k�� SessionToken�C�p�G�����\�n�D SessionToken �|���ͬ۹諸 Exception�C</returns>
        /// <exception cref="DSAException">�n�D Session ���Ϳ��~�ɡC</exception>
        /// <param name="token">�n�D Session ��SecurityToken�C</param>
        /// <param name="point"></param>
        /// <exception cref="OperationFailException">�ǰe�u�n�D SessionID�v ���ӽФ�󥢱ѮɡC</exception>
        /// <exception cref="NotSupportedException">�o�{ DSA Server ���^�� SessionID�ɡC</exception>
        public static SessionToken RequestSession(ISecurityToken token, AccessPoint accessPoint)
        {
            SessionToken newSession = null;

            DSRequest req = new DSRequest();
            req.TargetService = SRV_Connect;
            req.SecurityToken = token.GetTokenContent();

            //�n�D�ϥ� Session �s�u�C
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

            //���\�����o DSResponse �ä��N�� DSA Application �䴩 Session �\��C
            if (!rsp.HasContent)
                throw new NotSupportedException(em.Get("SessionServiceNotSupported"));

            //�p�G�ڦW�٤��O SessionID �N�� DSA Server ���䴩 Session �\��C
            if (rsp.GetContent().RootName != "SessionID")
                throw new NotSupportedException(em.Get("SessionServiceNotSupported"));

            newSession = new SessionToken(rsp.GetContent().GetText("."), token, accessPoint);

            return newSession;
        }

        #region ISecurityToken ����

        /// <summary>
        /// ���s�n�D�s�� SessionToken�C
        /// </summary>
        /// <exception cref="OperationFailException">�n�D�s�� SessionID ���ѮɡC</exception>
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
