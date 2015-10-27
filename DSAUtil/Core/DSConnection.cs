/*
 * Create Date�G2006/01/25
 * Update Date�G2007/11/01
 * Author Name�GYaoMing Huang
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.Security;
using System.Threading;

// IntelliSchool.DSA30.Util
namespace FISCA.DSAUtil
{
    using em = ErrorMessage;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.IO.Compression;

    /// <summary>
    /// �N��DSA��Connection����A�i�i��DSA Server���s�u�P�ǰeRequest���C
    /// </summary>
    /// <include file='Util30\LibDocument\DSConnection.xml' path='Documents/Document[@Name="DSConnection"]/*'/>
    /// <remarks>
    /// �����O�Τ�j�P�P�e���ۦP�A�p�G���ݭn�ϥηs�\��(�p��@ñ�J�B�[�j������)�A�ϥΤ覡�P�e�������@�ˡC
    /// </remarks>
    public class DSConnection
    {
        private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
        //private const string SRV_GetServerCertificate = "DS.Security.GetApplicationCertificate";
        internal const string SessionExpire = "511";
        internal const string PassportExpire = "512";

        //�w�]��DSNS Server Url�C
        //internal const string DefaultNameServerUrl = "http://dsns.blazer.org.tw/ds/doorway.aspx";

        private string _nameServerUrl = string.Empty;
        private bool _isConnected = false;                   //�O�_�w�s�u�C
        private string _strTargetAccessPoint;               //���s�u�ҳs�����ت� AccessPoint�C
        private string _strAuthAccessPoint = string.Empty;  //�{�Ҫ� AccessPoint�C
        private bool _usePassport = false;                //�O�_�n�ϥ� DSAPassport�C
        private bool _tokenIsReady = false;              //SecurityToken �O�_�w�ǳƧ����C
        private bool _useSession = false;                 //Java AccessPoint ����@ Session �\��C
        private bool _connectionInfoInitialed = false;        //�O�_�w�g��l�Ƴs�u��T�C
        private bool _httpKeepAlive = false;              //Http �� KeepAlive
        private int _timeout = 100000;                   //�ǰe��� Timeout �ɶ��C
        private AccessPoint _targetAccessPoint;             //�ǰe DSRequest �� Doorway �D���C
        private AccessPoint _authAccessPoint;              //�{�Ҫ� AccessPoint�A�q�`�|�P _targetAccessPoint �ۦP�C
        private ISecurityToken _securityToken;            //��l��SecurityToken�C

        /// <summary>
        /// ��s�u���A�ܧ�ɡA�|���ͦ��ƥ�C
        /// </summary>
        public event EventHandler<StateChangeEventArgs> StateChange;

        /// <summary>
        /// ��l�� <see cref="DSConnection"/> ���O���s�������A�H����DSA���s�u����C
        /// </summary>
        /// <param name="doorway">�D���W�١A�bDSA 3.0�䴩�T�ؤ覡
        /// �s�u�G�uhttp://�Bdsa://�Blocal://�v�A�����䴩��Ӫ��s�u
        /// �覡(�u#�v���P�u!�v��</param>
        /// <param name="fullUserName">�ϥΪ̦W��</param>
        /// <param name="password">�K�X</param>
        ///<remarks>
        /// ��l�ƹ����A�|�չϸѪR Doorway �������m�A�p�G�L�k�ѪR�|���� Exception�C
        ///</remarks>
        /// <include file='Util30\LibDocument\DSConnection.xml' path='Documents/Document[@Name="DSConnection.Construct2"]/*'/>
        public DSConnection(string accessPoint, string fullUserName, string password)
        {
            InitConnectionInfo(accessPoint, fullUserName, password);
        }

        /// <summary>
        /// �ϥ� PassportToken ��l�ƹ���C
        /// </summary>
        /// <param name="doorway"></param>
        /// <param name="securityToken"></param>
        public DSConnection(string accessPoint, PassportToken securityToken)
        {
            InitConnectionInfo(accessPoint, securityToken, true);
        }

        /// <summary>
        /// ��l�� DSConnection ���O���s�������A�����w����ѼơC
        /// </summary>
        public DSConnection()
        {
        }

        /// <summary>
        /// ���o�N��Doorway����m�C�u���b�s�u�ᦹ�ݩʤ~�|���ġC
        /// </summary>
        public AccessPoint AccessPoint
        {
            get { return _targetAccessPoint; }
        }

        /// <summary>
        /// ���o�γ]�w�W�٦��A������m�A�Ohttp�������m�A���i�H�ϥ� Doorway �W�٪��覡���w�C
        /// </summary>
        /// <value>���ݩʹw�]�ȬO�ustring.Empty�v�C</value>
        public string NameServerUrl
        {
            get { return _nameServerUrl; }
            set
            {
                if (IsConnected)
                    throw new InvalidOperationException(em.Get("ConnectedError"));

                _nameServerUrl = value;
            }
        }

        /// <summary>
        /// �O�_�ϥ� Session �s�u�A�i�[�t���� SendRequest �t�סC
        /// </summary>
        public bool UseSession
        {
            get { return _useSession; }
            set
            {
                if (IsConnected)
                    throw new InvalidOperationException(em.Get("ConnectedError"));

                _useSession = value;
            }
        }

        /// <summary>
        /// ���o�γ]�w���h HTTP �q�T��w�� KeepAlive ���Y���ȡC
        /// </summary>
        public bool HttpKeepAlive
        {
            get { return _httpKeepAlive; }
            set { _httpKeepAlive = value; }
        }

        private static bool _use_ie_proxy = false;       //�O�_���L IE �� Proxy �]�w�C
        /// <summary>
        /// �ǰe Request �ɨϥ� IE ��Proxy �]�w�ǰe�A�w�]�Ȭ��uFalse�v�C
        /// </summary>
        public static bool UseIEProxy
        {
            get { return _use_ie_proxy; }
            set { _use_ie_proxy = value; }
        }

        /// <summary>
        /// ���o�γ]�w�ǰe��Ʈɪ����ݮɶ��A�w�]�Ȭ� 100000 �L��(100��)�C
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// ���w�s�u��T�A�åB�i��s�u�ʧ@�C
        /// </summary>
        /// <param name="doorway">�D���W�١A�bDSA 3.0�䴩�T�ؤ覡
        /// �s�u�G�uhttp://�Bdsa://�Blocal://�v�A�����䴩��Ӫ��s�u
        /// �覡(�u#�v���P�u!�v��</param>
        /// <param name="fullUserName">�ϥΪ̦W�١A�榡�GUserName@DomainName�C</param>
        /// <param name="password">�K�X</param>
        ///<remarks>
        /// ��l�ƹ����A�|�չϸѪR Doorway �������m�A�p�G�L�k�ѪR�|���� Exception�C
        ///</remarks>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Connect(string accessPoint, string fullUserName, string password)
        {
            //��l�Ƴs�u��T�C
            InitConnectionInfo(accessPoint, fullUserName, password);

            //����s�u�ʧ@�C
            Connect();
        }

        /// <summary>
        /// �ϥ� SecurityToken ��l�ƹ���C
        /// </summary>
        /// <param name="doorway">�D���W�١A�bDSA 3.0�䴩�T�ؤ覡
        /// �s�u�G�uhttp://�Bdsa://�Blocal://�v�A�����䴩��Ӫ��s�u
        /// �覡(�u#�v���P�u!�v��</param>
        /// <param name="securityToken">�w�g������l�Ƨ������w���N�šC</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Connect(string accessPoint, ISecurityToken securityToken)
        {
            //��l�Ƴs�u��T�C
            InitConnectionInfo(accessPoint, securityToken, true);

            //����s�u�ʧ@�C
            Connect();
        }

        /// <summary>
        /// �s�u��D���C
        /// </summary>
        /// <remarks>�p�G�غc�������w������T�N�|����<see cref="DSAException"/>�ҥ~
        /// <para>�bDSA 3.0���A�p�G�s�u���ѷ|����<see cref="DSAException"/>
        /// �Ӥ��O�ϥΥN�X���覡�^�ǡC</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Connect()
        {
            if (!_connectionInfoInitialed)
                throw new InvalidOperationException(em.Get("ConnectionInfoNotInitialize"));

            PerformConnect();

            if (StateChange != null)
                StateChange(this, new StateChangeEventArgs(DSConnectionState.Connected));
        }

        /// <summary>
        /// ���_�P AccessPoint ���s�u�C
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Disconnect()
        {
            _isConnected = false;
            _securityToken = null;

            if (StateChange != null)
                StateChange(this, new StateChangeEventArgs(DSConnectionState.Disconnected));
        }

        /// <summary>
        /// �ΨӧP�_�O�_�b�s�u���A�C
        /// </summary>
        public bool IsConnected
        {
            get { return _isConnected; }
        }

        /// <summary>
        /// �ǰeRequest��D���C
        /// </summary>
        /// <param name="serviceName">�A�ȦW�١C</param>
        /// <returns>�D���Ҧ^�Ǫ���ơC</returns>
        /// <remarks>����k�|�۰ʲ��ͪŪ��ӽФ��ǰe��D���C</remarks>
        public DSResponse SendRequest(string serviceName)
        {
            DSRequest req = null;
            return SendRequest(serviceName, req);
        }

        /// <summary>
        /// �ǰeRequest��D���C
        /// </summary>
        /// <param name="serviceName">�A�ȦW�١C</param>
        /// <param name="request">�n�ǰe���ӽФ��C</param>
        /// <returns>�D���Ҧ^�Ǫ���ơC</returns>
        /// <remarks>
        ///     <para>�p�G�ҭn�D���A�ȷ|�^�Ǫťժ�Xml���(Body�����t������)�A
        ///     ����k�|�^��Null�ѦҡC</para>
        ///     <para>�p�G���ݭn�B�z�^�Ф��ɡA�ϥΦ���k�I�s�A�ȫܾA�X�C</para>
        /// </remarks>
        public DSXmlHelper SendRequest(string serviceName, DSXmlHelper request)
        {
            DSResponse rsp;

            if (request == null)
                rsp = SendRequest(serviceName);
            else
                rsp = SendRequest(serviceName, new DSRequest(request));

            if (rsp.HasContent)
                return rsp.GetContent();
            else
                return null;
        }

        public DSResponse SendRequest(string serviceName, DSRequest request)
        {
            return SendRequest(serviceName, request, _timeout);
        }

        /// <summary>
        /// �ǰeRequest��D���C
        /// </summary>
        /// <param name="serviceName">�A�ȦW�١C</param>
        /// <param name="request">�n�ǰe���ӽФ��C</param>
        /// <param name="timeout">�]�wTimeout�ɶ��A�p�GTimeout�ɶ���F�A�D�����^�Ǹ�ơA
        /// �Y�|����Exception�A�w�]Timeout�ɶ���100��(100,000�L��)�C</param>
        /// <returns>�D���Ҧ^�Ǫ���ơC</returns>
        public DSResponse SendRequest(string serviceName, DSRequest request, int timeout)
        {
            //���ݽT�O�O�b�s�u���A�C
            if (!IsConnected)
                throw new InvalidOperationException(em.Get("MustConnected"));

            DSRequest dsreq;

            if (request == null)
                dsreq = new DSRequest();
            else
                dsreq = request;

            dsreq.TargetService = serviceName;
            dsreq.SecurityToken = _securityToken.GetTokenContent();

            DSResponse dsrsp;

            try
            {
                dsrsp = SendRequest(dsreq, _targetAccessPoint, timeout, _httpKeepAlive);//�ǰe�ӽФ��
            }
            catch (DSAFaultException ex)
            {
                throw ex;
            }
            catch (DSAServerException ex)
            {
                if (ex.ServerStatus == DSAServerStatus.PassportExpire || ex.ServerStatus == DSAServerStatus.SessionExpire)
                {
                    if (_securityToken is IRenewableToken)
                    {
                        IRenewableToken renew = _securityToken as IRenewableToken;
                        try
                        {
                            renew.RenewToken();
                        }
                        catch (SecurityTokenException ex1)
                        {
                            throw ex1;
                        }
                        catch (Exception iex)
                        {
                            throw new SecurityTokenException(em.Get("RenewSecurityTokenFail"), iex);
                        }

                        dsrsp = SendRequest(serviceName, request, timeout);
                    }
                    else
                        throw new NotSupportedException(em.Get("ObjectStatusError"));
                }
                else
                    throw ex;
            }

            return dsrsp;
        }

        /// <summary>
        /// �D�P�B�ǰe�ӽФ��C
        /// </summary>
        /// <param name="config">�D�P�B�z���պA��T�C</param>
        public void SendRequest(AsyncSendContext config)
        {
            //���ݽT�O�O�b�s�u���A�C
            if (!IsConnected)
                throw new InvalidOperationException(em.Get("MustConnected"));

            if (config.Request == null)
                config.Request = new DSRequest();

            config.TargetAccessPoint = _targetAccessPoint;
            config.SecurityToken = _securityToken;

            AsyncPostRequest sender = new AsyncPostRequest(config);
            sender.StartAsync();
        }

        /// <summary>
        /// ���o�n�J���w���N�šC
        /// </summary>
        public ISecurityToken SecurityToken
        {
            get { return _securityToken; }
        }

        /* Private Method */

        /// <summary>
        /// ����s�u�ʧ@
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void PerformConnect()
        {
            if (IsConnected) return;

            //�ѪR�ؼ� AccessPoint ����m�C
            //Debug.WriteLine("\nResolve TargetAccessPoint�G" + _strTargetAccessPoint + "\n");
            if (_nameServerUrl == string.Empty)
                _targetAccessPoint = AccessPoint.Resolve(_strTargetAccessPoint);
            else
                _targetAccessPoint = AccessPoint.Resolve(_strTargetAccessPoint, _nameServerUrl);

            //�ǳ� AuthAccessPoint�A�p�� TargetAccessPoint ���ۦP�A�i��ѪR�C
            if (_strTargetAccessPoint == _strAuthAccessPoint)
                _authAccessPoint = _targetAccessPoint;
            else
            {
                //Debug.WriteLine("\nResolve AuthAccessPoint�G" + _strAuthAccessPoint + "\n");
                if (_nameServerUrl == string.Empty)
                    _authAccessPoint = AccessPoint.Resolve(_strAuthAccessPoint);
                else
                    _authAccessPoint = AccessPoint.Resolve(_strAuthAccessPoint, _nameServerUrl);
            }

            //�ǳ� SecurityToken
            //_tokenIsReady �b InitConnectionInfo �ɨM�w�C
            if (!_tokenIsReady)
            {
                //Debug.WriteLineIf(_usePassport, "\nCall RequestPassport");
                //Debug.WriteLineIf(_usePassport, "SecurityToken�G\n" + _securityToken.GetTokenContent().OuterXml + "\n");
                if (_usePassport) //�M�w�n�ϥ� Passport Token �άO�@�� Token�C
                    _securityToken = RequestPassport(_securityToken); //�V�D���n�DPassport
            }

            if (UseSession)
                SessionConnect();
            else
                UnSessionConnect();
        }

        #region Method UnSessionConnect
        private void UnSessionConnect()
        {
            DSRequest req = new DSRequest();
            req.TargetService = SessionToken.SRV_Connect;
            req.SecurityToken = _securityToken.GetTokenContent();

            try
            {
                SendEnvelope(req, _targetAccessPoint);
                _isConnected = true;
            }
            catch (ServerFailException ex)
            {
                throw ex;
            }
            catch (DSAServerException ex)
            {
                if (ex.ServerStatus == DSAServerStatus.ServiceExecutionError)
                {
                    //DS.Security.Connect �A�ȥX���ɡC
                    throw new ConnectException(em.Get("ConnectServiceError",
                        new Replace("ServiceName", SessionToken.SRV_Connect)), ex);
                }

                //�p�G�O DSA Server �X���A�N�����N���~��X�C
                throw ex;
            }
            catch (Exception ex)
            {
                //���������~�N������X�C
                throw new SendRequestException(em.Get("ConnectOccureUnknowError"), ex);
            }
        }
        #endregion

        #region Method SessionConnect
        private void SessionConnect()
        {
            //Debug.WriteLine("\nCall RequestSession");
            //Debug.WriteLine("TargetAccessPoint�G" + _targetAccessPoint.Url);
            //Debug.WriteLine("SecurityToken�G\n" + _securityToken.GetTokenContent().OuterXml + "\n");
            try
            {
                _securityToken = SessionToken.RequestSession(_securityToken, _targetAccessPoint);
            }
            catch (SendRequestException ex)
            {
                if (ex.InnerException is DSAServerException)
                {
                    DSAServerException serverex = ex.InnerException as DSAServerException;
                    if (serverex.ServerStatus == DSAServerStatus.CredentialInvalid)
                        throw new ConnectException(em.Get("CredentialInvalid"), ex);
                    else
                        throw new ConnectException(string.Format("�s���D�����~�A�T���G{0}", ex.Message), ex);
                }
                else
                    throw new ConnectException(string.Format("�s���D�����~�A�T���G{0}", ex.Message), ex);
            }
            _isConnected = true;
        }
        #endregion

        private PassportToken RequestPassport(ISecurityToken securityToken)
        {
            try
            {
                return PassportToken.RequestPassport(securityToken, _authAccessPoint); //�V�D���n�DPassport;
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException(em.Get("RequestPassportError",
                    new Replace("AuthAccessPoint", _authAccessPoint.Name)), ex);
            }
        }

        private void ParseUserName(string fullUserName, ref string userName, ref string authDoorway)
        {
            string[] info = fullUserName.Split('#');

            if (info.Length > 1)
            {
                userName = info[0];
                _usePassport = true;
                authDoorway = info[1];

                if (authDoorway.ToUpper() == "LOCALHOST" || authDoorway.ToUpper() == "LOCAL")
                    authDoorway = _strTargetAccessPoint;
            }
            else
            {
                userName = fullUserName;
                _usePassport = false;
                //�S�����w�N�ϥέ�Ӫ�doorway�ӻ{��
                authDoorway = _strTargetAccessPoint;
            }
        }

        private void InitConnectionInfo(string doorway, string fullUserName, string password)
        {
            string userName = string.Empty;
            ParseUserName(fullUserName, ref userName, ref _strAuthAccessPoint);

            InitConnectionInfo(doorway, new BasicToken(userName, password), false);
        }

        private void InitConnectionInfo(string accessPoint, ISecurityToken securityToken, bool tokenIsReady)
        {
            ////���i�H�ϥι��O�uSessionToken�v�o���� Token �ӳs�u�C
            //if (!securityToken.Reuseable)
            //    throw new ArgumentException(em.Get("SecurityTokenNotSupported"), "securityToken");

            if (string.IsNullOrEmpty(_strAuthAccessPoint))
                _strAuthAccessPoint = accessPoint;

            _strTargetAccessPoint = accessPoint;
            _securityToken = securityToken;
            _tokenIsReady = tokenIsReady;

            _connectionInfoInitialed = true;
        }

        /* Internal Method */

        internal static DSResponse SendEnvelope(DSRequest request, AccessPoint accessPoint)
        {
            return SendRequest(request, accessPoint, 100000, false);
        }

        /// <summary>
        /// �ǰeEnvelope��S�w�� AccessPoint�C
        /// </summary>
        /// <param name="request"></param>
        /// <param name="doorway"></param>
        /// <returns></returns>
        /// <exception cref="OperationFailException">�� DSA Application �L�k���T�^���ɡC</exception>
        /// <exception cref="DSAServerException">�� DSA Server ���Ϳ��~�ɡC</exception>
        /// <exception cref="DSAServiceExecutionException">��A�Ȥ���������~�ɡC</exception>
        internal static DSResponse SendRequest(DSRequest request, AccessPoint accessPoint, int timeout, bool httpKeepAlive)
        {
            StringBuilder errormsg = new StringBuilder();
            errormsg.AppendLine("SendRequestHTTP");
            errormsg.AppendLine("Url�G" + accessPoint.Url);
            errormsg.AppendLine("Timeout�G" + timeout.ToString());
            errormsg.AppendLine("KeepAlive�G" + httpKeepAlive.ToString());
            //Debug.WriteLine(errormsg.ToString());

            //�إ�Http�s�u
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(accessPoint.Url);
            httpReq.KeepAlive = httpKeepAlive;
            httpReq.Timeout = timeout;
            httpReq.Method = "POST";
            httpReq.ContentType = "text/xml";
            httpReq.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");

            if (!UseIEProxy) //�O�_�ϥ� IE Proxy�C
                httpReq.Proxy = new WebProxy();

            //�g�JRequest�D��
            try
            {
                Stream reqstream = httpReq.GetRequestStream();
                reqstream.WriteTimeout = timeout;

                StreamWriter reqWriter = new StreamWriter(reqstream, Encoding.UTF8);

                reqWriter.Write(request.GetRawXml());
                reqWriter.Close();
            }
            catch (Exception ex)
            {
                throw;
            }

            //���oResponse
            WebResponse httpRsp = null;

            try
            {
                //���o Response ���
                httpRsp = httpReq.GetResponse();
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                    throw new ServerFailException(em.Get("GetDSAResponseError"), ex);
                else
                {
                    StreamReader reader = new StreamReader(ex.Response.GetResponseStream());
                    string msg = reader.ReadToEnd();
                    reader.Close();

                    //Debug.WriteLine("\n�o�� WebException�AResponse ���e�G\n" + msg + "\n");

                    throw new ServerFailException(em.Get("GetDSAResponseError") + "\n\n" + msg, ex);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine("\n�o�ͥ��w�����~�G\n" + ex.ToString());
                //Debug.WriteLine("Request�G\n" + request.GetRawXml() + "\n");
                throw new ServerFailException(em.Get("GetDSAResponseError"), ex);
            }

            //�إ� Response ����C
            DSResponse rsp = new DSResponse();
            Stream rspStream = null;

            try
            {
                //���o Response ��󪺦�y�C
                if ((httpRsp as HttpWebResponse).GetResponseHeader("Content-Encoding") == "gzip")
                    rspStream = new GZipStream(httpRsp.GetResponseStream(), CompressionMode.Decompress);
                else
                    rspStream = httpRsp.GetResponseStream();
            }
            catch (InvalidOperationException ex)
            {
                throw new ServerFailException(em.Get("GetHTTPResponseStreamError"), ex);
            }

            StreamReader sr = new StreamReader(rspStream, Encoding.UTF8);
            string rawString = sr.ReadToEnd();
            sr.Close();
            try
            {
                //���J Response ����ƨ� Response ����
                rsp.Load(rawString); //���J���
                rspStream.Close(); //���� Stream�A���W���o�˷|���Шϥ� HTTP Connection�A�Ĳv����n�C
            }
            catch (Exception ex)
            {
                //��ѪR Response Document ���ѮɡC
                XmlProcessException xmlexp = new XmlProcessException("�ѪR Response Document ���~�C", ex);
                xmlexp.RawString = rawString;

                throw xmlexp;
            }

            //SimpleXmlValidator sv = SimpleXmlValidator.Standard;

            ////ValidateResponseDocument(rsp);

            //if (!sv.ValidateDocument(StandardXmlSchema.Response, rawString))
            //{
            //    //�^�Ǫ� DSResponse ���X�k�C
            //    SchemaValidateException ex = new SchemaValidateException(em.Get("ResponseDocumentInvalidate",
            //        new Replace("ValidMessage", sv.GetLastError())), null);

            //    throw ex;
            //}

            //�p�G���A���O Successful ���ܡA�N Throw Exception
            if (rsp.Status != DSAServerStatus.Successful)
            {
                if (rsp.Status == DSAServerStatus.ServiceExecutionError) //�O 504(ServiceExecutionError) ���N���ͤ��P�� Exception
                {
                    DSAFaultException fault = new DSAFaultException(rsp.GetFault());
                    throw new DSAServerException(rsp.Status, rsp.StatusMessage, rawString, fault);
                }
                else
                    throw new DSAServerException(rsp.Status, rsp.StatusMessage, rawString);
            }

            return rsp;
        }

        //private static void ValidateResponseDocument(DSResponse rsp)
        //{
        //}
    }
}