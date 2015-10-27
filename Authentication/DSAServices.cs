using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Cryptography;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;
using FISCA.DSAUtil;
using System.Reflection;

namespace FISCA.Authentication
{
    /// <summary>
    /// 處理用戶端與Server端的DSA呼叫
    /// </summary>
    public static class DSAServices
    {
        private static int _RunningRequest = 0;
        private static ulong _TransferBytes = 0;
        private static ulong _TransferTimes = 0;
        private static bool _TransferSuccess = true;
        private static string _WebExceptionMessage = "";
        //private static ManualResetEvent _LockConnection = new ManualResetEvent(true);
        private static Dictionary<DSConnection, ManualResetEvent> _LockConnection = new Dictionary<DSConnection, ManualResetEvent>();
        private static bool _is_sys_admin;
        private static bool _IsDeadAlready = false;

        private static Image _NormalImage = null;
        private static Image _LoadingImage = Properties.Resources.寬箭頭;
        private static Image _ErrorImage = Properties.Resources.紅箭頭;
        private static Image _ShadowImage = Properties.Resources.寬箭頭陰影;
        private static Bitmap _LoadingDrawImage = null;
        private static bool _AutoDisplayLoadingMessageOnMotherForm = false;
        private static FISCA.DSAUtil.DSConnection _DSConnection = new FISCA.DSAUtil.DSConnection();
        /// <summary>
        /// 在傳輸資料時於MotherForm上顯示動畫
        /// </summary>
        public static void AutoDisplayLoadingMessageOnMotherForm()
        {
            if (_AutoDisplayLoadingMessageOnMotherForm) return;
            _AutoDisplayLoadingMessageOnMotherForm = true;
            _LoadingDrawImage = new Bitmap(30, 30);
            var _MinimalLoadingTime = 0;
            var g = Graphics.FromImage(_LoadingDrawImage);
            g.TranslateTransform(14.2F, 14.5F);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            var gshadow = Graphics.FromImage(_LoadingDrawImage);
            gshadow.TranslateTransform(15.7F, 15.5F);

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 75;
            timer.Tick += delegate
            {
                if (FISCA.Presentation.MotherForm.StartMenu.Image != _LoadingDrawImage)
                {
                    _NormalImage = FISCA.Presentation.MotherForm.StartMenu.Image;
                }
                if (_RunningRequest == 0 && _MinimalLoadingTime == 0)
                    timer.Stop();
                if (_RunningRequest <= 0 && _MinimalLoadingTime == 0 && _TransferSuccess)
                {
                    FISCA.Presentation.MotherForm.StartMenu.Image = _NormalImage;
                }
                else
                {
                    float _Speed = 35;
                    if (_TransferTimes > 0)
                        _Speed = (((float)_TransferBytes / 1024F / ((float)_TransferTimes / 1000))) / 25F * 15;
                    if (_Speed > 35)
                        _Speed = 35;
                    if (_MinimalLoadingTime > 0) _MinimalLoadingTime = _MinimalLoadingTime - 1;
                    if (_TransferSuccess)
                    {

                        gshadow.Clear(Color.Transparent);
                        g.Clear(Color.Transparent);
                        gshadow.RotateTransform(_Speed);
                        gshadow.DrawImage(_ShadowImage, -15.0F, -15.0F, 30.0F, 30.0F);
                        gshadow.RotateTransform(180);
                        gshadow.DrawImage(_ShadowImage, -15.0F, -15.0F, 30.0F, 30.0F);

                        g.RotateTransform(_Speed);
                        g.DrawImage(_LoadingImage, -15.0F, -15.0F, 30.0F, 30.0F);
                        g.RotateTransform(180);
                        g.DrawImage(_LoadingImage, -15.0F, -15.0F, 30.0F, 30.0F);
                        FISCA.Presentation.MotherForm.StartMenu.Image = _LoadingDrawImage;
                    }
                    else
                    {
                        gshadow.Clear(Color.Transparent);
                        g.Clear(Color.Transparent);

                        gshadow.RotateTransform(_Speed / 2.0F);
                        gshadow.DrawImage(_ShadowImage, -15.0F, -15.0F, 30.0F, 30.0F);
                        gshadow.RotateTransform(180);
                        gshadow.DrawImage(_ShadowImage, -15.0F, -15.0F, 30.0F, 30.0F);

                        g.RotateTransform(_Speed / 2.0F);
                        g.DrawImage(_ErrorImage, -15.0F, -15.0F, 30.0F, 30.0F);
                        g.RotateTransform(180);
                        g.DrawImage(_ErrorImage, -15.0F, -15.0F, 30.0F, 30.0F);
                        FISCA.Presentation.MotherForm.StartMenu.Image = _LoadingImage;
                        FISCA.Presentation.MotherForm.SetStatusBarMessage("網路連線異常" + (_WebExceptionMessage == "" ? "" : ("(" + _WebExceptionMessage + ")")) + "，請檢查您的網路連線狀態。");
                    }
                }
            };
            System.Windows.Forms.Application.Idle += delegate
            {
                if (_RunningRequest > 0 && !timer.Enabled)
                {
                    _MinimalLoadingTime = 20;//每次轉動都至少轉1.5秒
                    timer.Start();
                }
            };
        }
        /// <summary>
        /// 取得登入系統的<see cref="FISCA.DSAUtil.DSConnection"/> 
        /// </summary>
        public static DSConnection DefaultConnection
        {
            get
            {
                if (IsLogined) return _DSConnection;
                else return null;
            }
        }
        /// <summary>
        /// 授權登入系統
        /// </summary>
        /// <param name="fileName">授權檔路徑</param>
        /// <returns>授權是否成功</returns>
        public static void SetLicense(string fileName)
        {
            SetLicense(new FileStream(fileName, FileMode.Open));
        }
        /// <summary>
        /// 設定授權登入系統
        /// </summary>
        /// <param name="stream">授權檔串流</param>
        public static void SetLicense(Stream stream)
        {
            //ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            //{
            //    //只要是「CN=intellischool Root Authority」發的憑證都信任。
            //    return (certificate.Issuer == "CN=intellischool Root Authority");
            //};
            _DSConnection = new DSConnection();

            byte[] CryptoKey = Encoding.UTF8.GetBytes("IntelliSchool SmartSchool Cryptography Key");
            byte[] cipher = new byte[stream.Length];
            stream.Read(cipher, 0, Convert.ToInt32(stream.Length));
            stream.Close();

            byte[] plain = ProtectedData.Unprotect(cipher, CryptoKey, DataProtectionScope.LocalMachine);
            string xmlString = Encoding.UTF8.GetString(plain);

            DSXmlHelper hlplicense = new DSXmlHelper(DSXmlHelper.LoadXml(xmlString));
            DSXmlHelper apptoken = new DSXmlHelper("SecurityToken");
            apptoken.SetAttribute(".", "Type", "Application");
            apptoken.AddElement(".", hlplicense.GetElement("ApplicationKey"));

            var accessPoint = hlplicense.GetText("AccessPoint");
            var applicationToken = new ApplicationToken(apptoken.BaseElement);
            _DSConnection.Connect(accessPoint, applicationToken);
            //Framework.BugReporter.SetRunTimeMessage("DsnsName", accessPoint);
        }
        /// <summary>
        /// 登入使用者
        /// </summary>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        public static void Login(string account, string password)
        {
            DSXmlHelper request = new DSXmlHelper("Request");
            request.AddElement("Condition");
            request.AddElement("Condition", "UserName", account.ToUpper());
            request.AddElement("Condition", "Password", ComputePasswordHash(password));

            DSXmlHelper response = _DSConnection.SendRequest("SmartSchool.Personal.CheckUserPassword", request);

            if (response.GetElements("User").Length <= 0)
                throw new Exception("使用者帳號或密碼錯誤。");

            XmlElement user = response.GetElement("User");
            if (user.GetAttribute("IsSysAdmin") == "1")
                _is_sys_admin = true;
            else
                _is_sys_admin = false;

            //Framework.BugReporter.SetRunTimeMessage("LoginUser", account);
            IsLogined = true;
            UserAccount = account;
        }
        /// <summary>
        /// 登入使用者
        /// </summary>
        /// <param name="license">授權檔串流</param>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        public static void Login(Stream license, string account, string password)
        {
            SetLicense(license);
            Login(account, password);
        }
        /// <summary>
        /// 登入使用者
        /// </summary>
        /// <param name="licensePath">授權檔路徑</param>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        public static void Login(string licensePath, string account, string password)
        {
            SetLicense(licensePath);
            Login(account, password);
        }
        /// <summary>
        /// 取得登入帳號是否為系統管理員帳號
        /// </summary>
        public static bool IsSysAdmin
        {
            get { return _is_sys_admin; }
        }
        /// <summary>
        /// 使用者已經成功登入系統
        /// </summary>
        public static bool IsLogined { get; internal set; }
        /// <summary>
        /// 呼叫DSAService
        /// </summary>
        /// <param name="service">ServiceName</param>
        /// <param name="req">DSRequest</param>
        /// <returns>DSResponse</returns>
        public static DSResponse CallService(string service, DSRequest req)
        {
            if (!IsLogined) throw new Exception("尚未登入系統。");
            return CallService(_DSConnection, service, req);
        }

        private static object obj = new object();
        private static int t1_sum = 0;
        private static XmlWriter w = null;

        /// <summary>
        /// 呼叫DSAService
        /// </summary>
        /// <param name="connection">連線主機</param>
        /// <param name="service">ServiceName</param>
        /// <param name="req">DSRequest</param>
        /// <returns>DSResponse</returns>
        public static DSResponse CallService(this DSConnection connection, string service, DSRequest req)
        {
            List<Exception> exs;

            if (!File.Exists(Path.Combine(Application.StartupPath, "追蹤流量")))
            {
                t1_sum = 0;
                w = null;

                return CallServiceInternal(connection, service, req, out exs);
            }
            else
            {
                lock (obj)
                {
                    DSResponse rsp = null;
                    if (w == null)
                    {
                        FileStream fs = new FileStream(Path.Combine(Application.StartupPath, "追蹤流量紀錄.xml"), FileMode.Create, FileAccess.Write);
                        w = XmlTextWriter.Create(fs) as XmlWriter;

                        w.WriteStartElement("Traffics");
                        Application.ApplicationExit += delegate
                        {
                            w.WriteEndElement();
                            w.Close();
                        };
                    }

                    w.WriteStartElement("Traffic");
                    {
                        w.WriteAttributeString("Timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        w.WriteAttributeString("Service", service);

                        w.WriteAttributeString("ThreadCount", Process.GetCurrentProcess().Threads.Count.ToString());

                        w.WriteStartElement("CallStack");
                        {
                            StackTrace st = new StackTrace();
                            foreach (StackFrame each in st.GetFrames())
                            {
                                MethodBase method = each.GetMethod();
                                if (method.Module.Name.StartsWith("System.")) continue;
                                if (method.Module.Name.StartsWith("Microsoft.")) continue;
                                if (method.Module.Name.StartsWith("DevComponent.")) continue;
                                if (method.Module.Name.StartsWith("mscorlib.")) continue;

                                w.WriteStartElement("Frame");
                                {
                                    w.WriteAttributeString("Assembly", method.Module.Name);
                                    w.WriteAttributeString("Type", method.ReflectedType.FullName);
                                    w.WriteAttributeString("Member", method.Name);
                                }
                                w.WriteEndElement();
                            }
                        }
                        w.WriteEndElement();

                        int t1 = Environment.TickCount;

                        w.WriteElementString("RequestSize", req.GetRawBinary().Length.ToString());

                        try
                        {
                            rsp = CallServiceInternal(connection, service, req, out exs);
                        }
                        catch (Exception ex)
                        {
                            ExceptionReport exr = new ExceptionReport();

                            w.WriteStartElement("ExceptionOccur");
                            w.WriteRaw(exr.Transform(ex));
                            w.WriteEndElement();

                            w.WriteEndElement(); //end the Traffic element.

                            throw;
                        }

                        w.WriteElementString("ResponseSize", rsp.GetRawBinary().Length.ToString());
                        w.WriteElementString("SpenTime", (Environment.TickCount - t1).ToString());
                        w.WriteElementString("RetryCount", exs.Count.ToString());

                        t1_sum += Environment.TickCount - t1;

                        w.WriteElementString("SpenTimeTotal", t1_sum.ToString());
                    }
                    w.WriteEndElement();
                    w.Flush();

                    return rsp;
                }
            }
        }

        private static DSResponse CallServiceInternal(DSConnection connection, string service, DSRequest req, out List<Exception> exs)
        {
            exs = new List<Exception>();

            _RunningRequest++;
            lock (_LockConnection)
            {
                if (!_LockConnection.ContainsKey(connection))
                    _LockConnection.Add(connection, new ManualResetEvent(true));
            }
            var failedAutoRetry = false;
            var failedPass = false;
            var stackTrace = new List<string>();
            foreach (StackFrame frame in (new StackTrace()).GetFrames())//呼叫堆疊
            {
                Type type = frame.GetMethod().ReflectedType;
                foreach (object var in frame.GetMethod().GetCustomAttributes(true))//呼叫的函數是否為AutoRetryOnWebException
                {
                    if (var is AutoRetryOnWebExceptionAttribute)
                    {
                        failedAutoRetry = true;
                        break;
                    }
                    if (var is LeaveOnErrorAttribute)
                    {
                        failedPass = true;
                        break;
                    }
                }
                if (failedPass == false && failedAutoRetry == false)
                {
                    foreach (object var in type.GetCustomAttributes(true))//呼叫的類別是否為AutoRetryOnWebException
                    {
                        if (var is AutoRetryOnWebExceptionAttribute)
                        {
                            failedAutoRetry = true;
                            break;
                        }
                        if (var is LeaveOnErrorAttribute)
                        {
                            failedPass = true;
                            break;
                        }
                    }
                }
            }

            DSResponse resp = null;
            while (true)//呼叫成功立刻break，發生網路錯誤且自動重試時則continue
            {
                try
                {
                    _LockConnection[connection].WaitOne();
                    DateTime d1 = DateTime.Now;
                    resp = connection.SendRequest(service, req, 120000);  //120秒才  Timeout
                    long binaryLength = resp.GetRawBinary().LongLength + (req == null ? 0 : req.GetRawBinary().LongLength);//計算傳輸資料量
                    int milliseconds = ((TimeSpan)(DateTime.Now - d1)).Milliseconds;//計算花費時間
                    _TransferBytes = _TransferBytes + (ulong)resp.GetRawBinary().Length;
                    _TransferTimes = _TransferTimes + (ulong)milliseconds;
                    _TransferSuccess = true;
                }
                catch (Exception e)
                {
                    exs.Add(e);

                    if (failedPass)
                        return resp;
                    var isWebException = false;
                    var currentException = e;
                    while (currentException != null)
                    {
                        if (currentException is WebException)
                        {
                            isWebException = true;
                            _WebExceptionMessage = currentException.Message;
                            break;
                        }
                        currentException = currentException.InnerException;
                    }
                    if (isWebException)
                    {
                        //回報網路
                        //Framework.BugReporter.ReportException(e, false);
                        if (failedAutoRetry)
                        {
                            _TransferSuccess = false;
                            continue;
                        }
                        else
                        {
                            _LockConnection[connection].Reset();
                            if (_AutoDisplayLoadingMessageOnMotherForm)
                            {
                                lock (_LockConnection)
                                {
                                    if (_IsDeadAlready)
                                    {
                                        _LockConnection[connection].WaitOne();
                                    }
                                    else
                                    {
                                        _IsDeadAlready = true;
                                        if (MessageBox.Show("網路連線異常" + (_WebExceptionMessage == "" ? "" : ("(" + _WebExceptionMessage + ")")) + "，系統必須關閉。\n您可能正在新增或修改資料，\n系統因網路環境錯誤無法判斷您正進行的作業是否成功儲存，\n繼續使用將無法保證資料的正確，\n\n建議您在重新登入系統後檢查資料的變更是否完成。\n\n是否重新登入系統?", "網路連線異常", MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                                           == DialogResult.Yes)
                                        {
                                            Application.Restart();
                                            _LockConnection[connection].Set();
                                        }
                                        else
                                        {
                                            Application.Exit();
                                            _LockConnection[connection].Set();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                _RunningRequest--;
                                _LockConnection[connection].Set();
                                throw;
                            }
                        }
                    }
                    else
                    {
                        _RunningRequest--;
                        _LockConnection[connection].Set();
                        throw;
                    }
                }
                break;
            }
            _RunningRequest--;

            return resp;
        }

        //private static object Obj = new object();

        /// <summary>
        /// 登入帳號
        /// </summary>
        public static string UserAccount { get; private set; }
        /// <summary>
        /// 登入的主機名稱。
        /// </summary>
        public static string AccessPoint { get { return _DSConnection.AccessPoint.Name; } }
        /// <summary>
        /// 將密碼從明碼轉換為暗碼
        /// </summary>
        /// <param name="password">明碼</param>
        /// <returns>轉碼後的暗碼</returns>
        public static string ComputePasswordHash(string password)
        {
            SHA1Managed sha1 = new SHA1Managed();
            Encoding utf8 = Encoding.UTF8;
            byte[] hashResult = sha1.ComputeHash(utf8.GetBytes(password));
            return Convert.ToBase64String(hashResult);
        }
    }
    internal class ApplicationToken : ISecurityToken
    {
        public ApplicationToken(XmlElement token)
        {
            _token_content = token;
        }

        private XmlElement _token_content;

        #region ISecurityToken 成員

        public System.Xml.XmlElement GetTokenContent()
        {
            return _token_content;
        }

        public string TokenType
        {
            get { return "Application"; }
        }

        public bool Reuseable
        {
            get { return false; }
        }

        #endregion
    }

    #region Exception Report
    internal class ExceptionReport
    {
        private Dictionary<Type, IExtraProcesser> _collect_types;
        private List<Type> _subclasses = new List<Type>();

        public ExceptionReport()
        {
            _collect_types = new Dictionary<Type, IExtraProcesser>();
            AddType(typeof(Exception), true);
            AddType(typeof(HttpWebRequest), true);
            AddType(typeof(HttpWebResponse), true);
        }

        public void AddType(Type type)
        {
            AddType(type, false);
        }

        public void AddType(Type type, bool includeSubclass)
        {
            if (_collect_types.ContainsKey(type)) return;

            _collect_types.Add(type, null);

            if (includeSubclass)
                _subclasses.Add(type);
        }

        public void AddType(Type type, IExtraProcesser processer)
        {
            if (_collect_types.ContainsKey(type)) return;

            _collect_types.Add(type, processer);
        }

        public void AddType(Type type, IExtraProcesser processer, bool includeSubclass)
        {
            if (_collect_types.ContainsKey(type)) return;

            _collect_types.Add(type, processer);

            if (includeSubclass)
                _subclasses.Add(type);
        }

        public string Transform(Exception ex)
        {
            XmlTextWriter writer = new XmlTextWriter(new MemoryStream(), Encoding.UTF8);
            writer.Formatting = Formatting.Indented;

            //writer.WriteStartElement(ex.GetType().Name);
            writer.WriteStartElement("Exception");
            {
                Transform(writer, ex);
            }
            writer.WriteEndElement();

            writer.Flush();
            writer.BaseStream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(writer.BaseStream, Encoding.UTF8);

            string result = reader.ReadToEnd();
            reader.Close();

            return result;
        }

        private void Transform(XmlWriter writer, object ex)
        {
            Type ext = ex.GetType();
            foreach (PropertyInfo each in ext.GetProperties())
            {
                if (!each.CanRead)
                    continue;

                writer.WriteStartElement(each.Name);
                writer.WriteAttributeString("PropertyType", each.PropertyType.Name);
                {
                    try
                    {
                        object obj = each.GetValue(ex, null);

                        if (obj != null)
                        {
                            writer.WriteAttributeString("InstanceType", obj.GetType().Name);

                            if (_collect_types.ContainsKey(obj.GetType()) || Subclasses(obj.GetType()))
                                Transform(writer, obj);
                            else
                                writer.WriteString(obj.ToString());
                        }
                        else
                            writer.WriteComment("此屬性值為「Null」。");
                    }
                    catch (Exception e)
                    {
                        writer.WriteComment(string.Format("讀取屬性值錯誤，訊息：\n{0}", e.Message));
                    }
                }
                writer.WriteEndElement();
            }

            writer.WriteComment("自定錯誤訊息處理流程所產生的資料。");
            writer.WriteStartElement("ExtraProcessSection");
            {
                if (_collect_types.ContainsKey(ex.GetType()))
                {
                    IExtraProcesser processer = _collect_types[ex.GetType()];

                    if (processer != null)
                    {
                        ExtraInformation[] infos = processer.Process(ex);

                        if (infos != null)
                        {
                            foreach (ExtraInformation each in infos)
                            {
                                writer.WriteStartElement(each.Name);
                                writer.WriteString(each.Data);
                                writer.WriteEndElement();
                            }
                        }
                    }
                }
            }
            writer.WriteEndElement();
        }

        private bool Subclasses(Type type)
        {
            foreach (Type each in _subclasses)
            {
                if (type.IsSubclassOf(each))
                    return true;
            }

            return false;
        }
    }

    internal class ExtraInformation
    {
        public ExtraInformation(string name, string data)
        {
            Name = name;
            Data = data;
        }

        public string Name { get; private set; }

        public string Data { get; private set; }
    }

    internal interface IExtraProcesser
    {
        ExtraInformation[] Process(object instance);
    }
    #endregion
}
