using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using FISCA.Deployment;
using FISCA.DSA;

namespace Manager
{
    static class Program
    {
        public static ServerCollection Servers { get; private set; }

        /// <summary>
        /// 取得或設定指示組態資訊中的密碼是否有加密。
        /// </summary>
        public static bool EncryptPassword { get; set; }

        public static bool OnlineMode { get; set; }

        public static bool IsAdministrator { get; internal set; }

        /// <summary>
        /// 新 Greening。
        /// </summary>
        internal static Connection Connection { get; set; }

        internal static SecurityToken Passport { get; set; }

        internal const string Greening = "https://auth.ischool.com.tw/dsa/greening";
        //internal const string Greening = "http://qschool.benqedu.com.cn/greening/api";
        //internal const string Greening = "http://118.31.72.3:8080/greening/api";
        //internal const string Greening = "http://121.196.204.176:8080/greening/api";

        public static void SetBarMessage(string message)
        {
            MainForm.SetBarMessage(message);
        }

        public static string GetOnlinePreference(string key, string id)
        {
            FISCA.XHelper req = new FISCA.XHelper("<Request/>");
            req.AddElement(".", "WidgetKey", key);
            req.AddElement(".", "InstanceKey", id);

            FISCA.XHelper rsp = Connection.SendRequest("Preference.GetMyWidgetPreference", new Envelope(req)).XResponseBody();
            return rsp.GetText("WidgetPreference/Content");
        }

        public static void SetOnlinePreference(string key, string id, string content)
        {
            FISCA.XHelper req = new FISCA.XHelper("<Request/>");
            req.AddElement(".", "WidgetKey", key);
            req.AddElement(".", "InstanceKey", id);

            FISCA.XHelper rsp = Connection.SendRequest("Preference.GetMyWidgetPreference", new Envelope(req)).XResponseBody();
            if (rsp.GetElement("WidgetPreference") != null)
            {
                FISCA.XHelper addreq = new FISCA.XHelper("<Request/>");
                addreq.AddElement("WidgetPreference");
                addreq.AddElement("WidgetPreference", "WidgetKey", key);
                addreq.AddElement("WidgetPreference", "InstanceKey", id);
                addreq.AddElement("WidgetPreference", "Content", content);
                FISCA.XHelper addrsp = Connection.SendRequest("Preference.UpdateWidgetPreference", new Envelope(addreq)).XResponseBody();
            }
            else
            {
                FISCA.XHelper addreq = new FISCA.XHelper("<Request/>");
                addreq.AddElement("WidgetPreference");
                addreq.AddElement("WidgetPreference", "WidgetKey", key);
                addreq.AddElement("WidgetPreference", "InstanceKey", id);
                addreq.AddElement("WidgetPreference", "Content", content);
                FISCA.XHelper addrsp = Connection.SendRequest("Preference.AddWidgetPreference", new Envelope(addreq)).XResponseBody();
            }
        }

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string basePath = System.Windows.Forms.Application.StartupPath;

            //if (File.Exists(Path.Combine(basePath, "update_padding.xml")))
            //{
            //    FISCA.Deployment.UpdateHelper.ExecuteScript(Path.Combine(basePath, "update_padding.xml"), true);
            //    return;
            //}

            ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            Servers = new ServerCollection();
            OnlineMode = false;
            EncryptPassword = true;
            IsAdministrator = false;

            Aspose.Cells.License lic = new Aspose.Cells.License();
            lic.SetLicense(new MemoryStream(Properties.Resources.Aspose_Total));

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new MainForm());
        }

        internal static void Update()
        {
            string basePath = System.Windows.Forms.Application.StartupPath;
            string appUrl = "http://dl.dropbox.com/u/16912100/CMProtoptype/app.xml";
            string script_path = Path.Combine(basePath, "update_padding.xml");
            ManifestResolver resolver = new ManifestResolver(appUrl, VersionOption.Stable);
            resolver.VerifySignature = true;

            UpdateHelper uc = new UpdateHelper();
            uc.Resolver = resolver;
            uc.Install = new InstallDescriptor(basePath);

            resolver.ProgressMessage += new EventHandler<ProgressMessageEventArgs>(au_ProgressMessage);
            uc.DownloadStarted += new EventHandler(au_DownloadStarted);
            uc.DownloadProgressChanged += new EventHandler<DownloadProgressEventHandler>(au_DownloadProgressChanged);
            uc.DownloadCompleted += new EventHandler(au_DownloadCompleted);
            uc.ProgressMessage += new EventHandler<ProgressMessageEventArgs>(au_ProgressMessage);

            MainForm.SetBarMessage("檢查更新…");
            ThreadPool.QueueUserWorkItem(arg =>
            {
                try
                {
                    resolver.Resolve();

                    if (uc.CheckUpdate())
                    {
                        PaddingScript script = new PaddingScript();
                        script.WaitRelease(System.Reflection.Assembly.GetExecutingAssembly().Location);

                        uc.Update(script);

                        script.Delete(uc.Install.TemporalFolder);
                        script.Delete(script_path);
                        script.DeleteEmpty(basePath);
                        script.StartProcess(System.Reflection.Assembly.GetExecutingAssembly().Location);

                        script.Save(script_path);
                        MainForm.SetBarMessage("更新完成，請重新啟動程式。");
                    }
                    else
                        MainForm.SetBarMessage("已經是最新版。");
                }
                catch (Exception ex)
                {
                    MainForm.SetBarMessage(ex.Message);
                }
            });
        }

        static void au_ProgressMessage(object sender, ProgressMessageEventArgs e)
        {
            MainForm.SetBarMessage(e.Message);
        }

        static void au_DownloadCompleted(object sender, EventArgs e)
        {
            MainForm.SetBarMessage("下載完成。");
        }

        static void au_DownloadProgressChanged(object sender, DownloadProgressEventHandler e)
        {
            MainForm.Progress.Value = e.ProgressPercentage;
        }

        static void au_DownloadStarted(object sender, EventArgs e)
        {
            MainForm.SetBarMessage("開始下載新版本...");
            MainForm.Progress.Value = 0;
        }
    }
}
