/*
 * Create Date：2006/01/25
 * Update Date：2007/11/01
 * Author Name：YaoMing Huang
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
    /// 代表DSA的Connection物件，可進行DSA Server的連線與傳送Request文件。
    /// </summary>
    /// <include file='Util30\LibDocument\DSConnection.xml' path='Documents/Document[@Name="DSConnection"]/*'/>
    /// <remarks>
    /// 此類別用方大致與前版相同，如果不需要使用新功能(如單一簽入、加強式驗證)，使用方式與前版完全一樣。
    /// </remarks>
    public class DSConnection
    {
        private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
        //private const string SRV_GetServerCertificate = "DS.Security.GetApplicationCertificate";
        internal const string SessionExpire = "511";
        internal const string PassportExpire = "512";

        //預設的DSNS Server Url。
        //internal const string DefaultNameServerUrl = "http://dsns.blazer.org.tw/ds/doorway.aspx";

        private string _nameServerUrl = string.Empty;
        private bool _isConnected = false;                   //是否已連線。
        private string _strTargetAccessPoint;               //此連線所連接的目的 AccessPoint。
        private string _strAuthAccessPoint = string.Empty;  //認證的 AccessPoint。
        private bool _usePassport = false;                //是否要使用 DSAPassport。
        private bool _tokenIsReady = false;              //SecurityToken 是否已準備完成。
        private bool _useSession = false;                 //Java AccessPoint 未實作 Session 功能。
        private bool _connectionInfoInitialed = false;        //是否已經初始化連線資訊。
        private bool _httpKeepAlive = false;              //Http 的 KeepAlive
        private int _timeout = 100000;                   //傳送文件的 Timeout 時間。
        private AccessPoint _targetAccessPoint;             //傳送 DSRequest 的 Doorway 主機。
        private AccessPoint _authAccessPoint;              //認證的 AccessPoint，通常會與 _targetAccessPoint 相同。
        private ISecurityToken _securityToken;            //原始的SecurityToken。

        /// <summary>
        /// 當連線狀態變更時，會產生此事件。
        /// </summary>
        public event EventHandler<StateChangeEventArgs> StateChange;

        /// <summary>
        /// 初始化 <see cref="DSConnection"/> 類別的新執行個體，以做為DSA的連線物件。
        /// </summary>
        /// <param name="doorway">主機名稱，在DSA 3.0支援三種方式
        /// 連線：「http://、dsa://、local://」，但仍支援原來的連線
        /// 方式(「#」號與「!」號</param>
        /// <param name="fullUserName">使用者名稱</param>
        /// <param name="password">密碼</param>
        ///<remarks>
        /// 初始化實體後，會試圖解析 Doorway 的實體位置，如果無法解析會產生 Exception。
        ///</remarks>
        /// <include file='Util30\LibDocument\DSConnection.xml' path='Documents/Document[@Name="DSConnection.Construct2"]/*'/>
        public DSConnection(string accessPoint, string fullUserName, string password)
        {
            InitConnectionInfo(accessPoint, fullUserName, password);
        }

        /// <summary>
        /// 使用 PassportToken 初始化實體。
        /// </summary>
        /// <param name="doorway"></param>
        /// <param name="securityToken"></param>
        public DSConnection(string accessPoint, PassportToken securityToken)
        {
            InitConnectionInfo(accessPoint, securityToken, true);
        }

        /// <summary>
        /// 初始化 DSConnection 類別的新執行個體，不指定任何參數。
        /// </summary>
        public DSConnection()
        {
        }

        /// <summary>
        /// 取得代表Doorway的位置。只有在連線後此屬性才會有效。
        /// </summary>
        public AccessPoint AccessPoint
        {
            get { return _targetAccessPoint; }
        }

        /// <summary>
        /// 取得或設定名稱伺服器的位置，是http的實體位置，不可以使用 Doorway 名稱的方式指定。
        /// </summary>
        /// <value>此屬性預設值是「string.Empty」。</value>
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
        /// 是否使用 Session 連線，可加速之後 SendRequest 速度。
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
        /// 取得或設定底層 HTTP 通訊協定中 KeepAlive 標頭的值。
        /// </summary>
        public bool HttpKeepAlive
        {
            get { return _httpKeepAlive; }
            set { _httpKeepAlive = value; }
        }

        private static bool _use_ie_proxy = false;       //是否略過 IE 的 Proxy 設定。
        /// <summary>
        /// 傳送 Request 時使用 IE 的Proxy 設定傳送，預設值為「False」。
        /// </summary>
        public static bool UseIEProxy
        {
            get { return _use_ie_proxy; }
            set { _use_ie_proxy = value; }
        }

        /// <summary>
        /// 取得或設定傳送資料時的等待時間，預設值為 100000 微秒(100秒)。
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// 指定連線資訊，並且進行連線動作。
        /// </summary>
        /// <param name="doorway">主機名稱，在DSA 3.0支援三種方式
        /// 連線：「http://、dsa://、local://」，但仍支援原來的連線
        /// 方式(「#」號與「!」號</param>
        /// <param name="fullUserName">使用者名稱，格式：UserName@DomainName。</param>
        /// <param name="password">密碼</param>
        ///<remarks>
        /// 初始化實體後，會試圖解析 Doorway 的實體位置，如果無法解析會產生 Exception。
        ///</remarks>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Connect(string accessPoint, string fullUserName, string password)
        {
            //初始化連線資訊。
            InitConnectionInfo(accessPoint, fullUserName, password);

            //執行連線動作。
            Connect();
        }

        /// <summary>
        /// 使用 SecurityToken 初始化實體。
        /// </summary>
        /// <param name="doorway">主機名稱，在DSA 3.0支援三種方式
        /// 連線：「http://、dsa://、local://」，但仍支援原來的連線
        /// 方式(「#」號與「!」號</param>
        /// <param name="securityToken">已經完成初始化完成的安全代符。</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Connect(string accessPoint, ISecurityToken securityToken)
        {
            //初始化連線資訊。
            InitConnectionInfo(accessPoint, securityToken, true);

            //執行連線動作。
            Connect();
        }

        /// <summary>
        /// 連線到主機。
        /// </summary>
        /// <remarks>如果建構式未指定相關資訊將會產生<see cref="DSAException"/>例外
        /// <para>在DSA 3.0中，如果連線失敗會產生<see cref="DSAException"/>
        /// 而不是使用代碼的方式回傳。</para>
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
        /// 切斷與 AccessPoint 的連線。
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
        /// 用來判斷是否在連線狀態。
        /// </summary>
        public bool IsConnected
        {
            get { return _isConnected; }
        }

        /// <summary>
        /// 傳送Request到主機。
        /// </summary>
        /// <param name="serviceName">服務名稱。</param>
        /// <returns>主機所回傳的資料。</returns>
        /// <remarks>此方法會自動產生空的申請文件傳送到主機。</remarks>
        public DSResponse SendRequest(string serviceName)
        {
            DSRequest req = null;
            return SendRequest(serviceName, req);
        }

        /// <summary>
        /// 傳送Request到主機。
        /// </summary>
        /// <param name="serviceName">服務名稱。</param>
        /// <param name="request">要傳送的申請文件。</param>
        /// <returns>主機所回傳的資料。</returns>
        /// <remarks>
        ///     <para>如果所要求的服務會回傳空白的Xml文件(Body中未含任何資料)，
        ///     此方法會回傳Null參考。</para>
        ///     <para>如果不需要處理回覆文件時，使用此方法呼叫服務很適合。</para>
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
        /// 傳送Request到主機。
        /// </summary>
        /// <param name="serviceName">服務名稱。</param>
        /// <param name="request">要傳送的申請文件。</param>
        /// <param name="timeout">設定Timeout時間，如果Timeout時間到了，主機未回傳資料，
        /// 即會產生Exception，預設Timeout時間為100秒(100,000微秒)。</param>
        /// <returns>主機所回傳的資料。</returns>
        public DSResponse SendRequest(string serviceName, DSRequest request, int timeout)
        {
            //必需確保是在連線狀態。
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
                dsrsp = SendRequest(dsreq, _targetAccessPoint, timeout, _httpKeepAlive);//傳送申請文件
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
        /// 非同步傳送申請文件。
        /// </summary>
        /// <param name="config">非同處理的組態資訊。</param>
        public void SendRequest(AsyncSendContext config)
        {
            //必需確保是在連線狀態。
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
        /// 取得登入的安全代符。
        /// </summary>
        public ISecurityToken SecurityToken
        {
            get { return _securityToken; }
        }

        /* Private Method */

        /// <summary>
        /// 執行連線動作
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void PerformConnect()
        {
            if (IsConnected) return;

            //解析目標 AccessPoint 的位置。
            //Debug.WriteLine("\nResolve TargetAccessPoint：" + _strTargetAccessPoint + "\n");
            if (_nameServerUrl == string.Empty)
                _targetAccessPoint = AccessPoint.Resolve(_strTargetAccessPoint);
            else
                _targetAccessPoint = AccessPoint.Resolve(_strTargetAccessPoint, _nameServerUrl);

            //準備 AuthAccessPoint，如跟 TargetAccessPoint 不相同再進行解析。
            if (_strTargetAccessPoint == _strAuthAccessPoint)
                _authAccessPoint = _targetAccessPoint;
            else
            {
                //Debug.WriteLine("\nResolve AuthAccessPoint：" + _strAuthAccessPoint + "\n");
                if (_nameServerUrl == string.Empty)
                    _authAccessPoint = AccessPoint.Resolve(_strAuthAccessPoint);
                else
                    _authAccessPoint = AccessPoint.Resolve(_strAuthAccessPoint, _nameServerUrl);
            }

            //準備 SecurityToken
            //_tokenIsReady 在 InitConnectionInfo 時決定。
            if (!_tokenIsReady)
            {
                //Debug.WriteLineIf(_usePassport, "\nCall RequestPassport");
                //Debug.WriteLineIf(_usePassport, "SecurityToken：\n" + _securityToken.GetTokenContent().OuterXml + "\n");
                if (_usePassport) //決定要使用 Passport Token 或是一般 Token。
                    _securityToken = RequestPassport(_securityToken); //向主機要求Passport
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
                    //DS.Security.Connect 服務出錯時。
                    throw new ConnectException(em.Get("ConnectServiceError",
                        new Replace("ServiceName", SessionToken.SRV_Connect)), ex);
                }

                //如果是 DSA Server 出錯，就直接將錯誤丟出。
                throw ex;
            }
            catch (Exception ex)
            {
                //未知的錯誤就直接丟出。
                throw new SendRequestException(em.Get("ConnectOccureUnknowError"), ex);
            }
        }
        #endregion

        #region Method SessionConnect
        private void SessionConnect()
        {
            //Debug.WriteLine("\nCall RequestSession");
            //Debug.WriteLine("TargetAccessPoint：" + _targetAccessPoint.Url);
            //Debug.WriteLine("SecurityToken：\n" + _securityToken.GetTokenContent().OuterXml + "\n");
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
                        throw new ConnectException(string.Format("連接主機錯誤，訊息：{0}", ex.Message), ex);
                }
                else
                    throw new ConnectException(string.Format("連接主機錯誤，訊息：{0}", ex.Message), ex);
            }
            _isConnected = true;
        }
        #endregion

        private PassportToken RequestPassport(ISecurityToken securityToken)
        {
            try
            {
                return PassportToken.RequestPassport(securityToken, _authAccessPoint); //向主機要求Passport;
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
                //沒有指定就使用原來的doorway來認證
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
            ////不可以使用像是「SessionToken」這類的 Token 來連線。
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
        /// 傳送Envelope到特定的 AccessPoint。
        /// </summary>
        /// <param name="request"></param>
        /// <param name="doorway"></param>
        /// <returns></returns>
        /// <exception cref="OperationFailException">當 DSA Application 無法正確回應時。</exception>
        /// <exception cref="DSAServerException">當 DSA Server 產生錯誤時。</exception>
        /// <exception cref="DSAServiceExecutionException">當服務內部執行錯誤時。</exception>
        internal static DSResponse SendRequest(DSRequest request, AccessPoint accessPoint, int timeout, bool httpKeepAlive)
        {
            StringBuilder errormsg = new StringBuilder();
            errormsg.AppendLine("SendRequestHTTP");
            errormsg.AppendLine("Url：" + accessPoint.Url);
            errormsg.AppendLine("Timeout：" + timeout.ToString());
            errormsg.AppendLine("KeepAlive：" + httpKeepAlive.ToString());
            //Debug.WriteLine(errormsg.ToString());

            //建立Http連線
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(accessPoint.Url);
            httpReq.KeepAlive = httpKeepAlive;
            httpReq.Timeout = timeout;
            httpReq.Method = "POST";
            httpReq.ContentType = "text/xml";
            httpReq.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");

            if (!UseIEProxy) //是否使用 IE Proxy。
                httpReq.Proxy = new WebProxy();

            //寫入Request主體
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

            //取得Response
            WebResponse httpRsp = null;

            try
            {
                //取得 Response 文件
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

                    //Debug.WriteLine("\n發生 WebException，Response 內容：\n" + msg + "\n");

                    throw new ServerFailException(em.Get("GetDSAResponseError") + "\n\n" + msg, ex);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine("\n發生未預期錯誤：\n" + ex.ToString());
                //Debug.WriteLine("Request：\n" + request.GetRawXml() + "\n");
                throw new ServerFailException(em.Get("GetDSAResponseError"), ex);
            }

            //建立 Response 物件。
            DSResponse rsp = new DSResponse();
            Stream rspStream = null;

            try
            {
                //取得 Response 文件的串流。
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
                //載入 Response 的資料到 Response 物件中
                rsp.Load(rawString); //載入文件
                rspStream.Close(); //關閉 Stream，文件上說這樣會重覆使用 HTTP Connection，效率比較好。
            }
            catch (Exception ex)
            {
                //當解析 Response Document 失敗時。
                XmlProcessException xmlexp = new XmlProcessException("解析 Response Document 錯誤。", ex);
                xmlexp.RawString = rawString;

                throw xmlexp;
            }

            //SimpleXmlValidator sv = SimpleXmlValidator.Standard;

            ////ValidateResponseDocument(rsp);

            //if (!sv.ValidateDocument(StandardXmlSchema.Response, rawString))
            //{
            //    //回傳的 DSResponse 不合法。
            //    SchemaValidateException ex = new SchemaValidateException(em.Get("ResponseDocumentInvalidate",
            //        new Replace("ValidMessage", sv.GetLastError())), null);

            //    throw ex;
            //}

            //如果狀態不是 Successful 的話，就 Throw Exception
            if (rsp.Status != DSAServerStatus.Successful)
            {
                if (rsp.Status == DSAServerStatus.ServiceExecutionError) //是 504(ServiceExecutionError) 的就產生不同的 Exception
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