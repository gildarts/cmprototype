using System;
using System.Collections.Generic;
using System.Text;
using FISCA.DSA;
using System.Xml;
using FISCA;

namespace Manager
{
    class Server
    {
        public ServerRegistryData RegistryData { get; set; }

        internal Connection Connection { get; set; }

        public Server(ServerRegistryData registryData)
        {
            IsConnected = false;
            Manager = null;
            Configuration = null;
            RegistryData = registryData;
            AccessPointUrl = RegistryData.AccessPointUrl;
            LoginUserName = registryData.UserName;
        }

        /// <summary>
        /// 建立連線。
        /// </summary>
        public void Connect()
        {
            Connection conn = ConnectInternal();
            Connection = conn;

            //這行要在前面...
            Manager = new ServerManager(this);

            LoadConfiguration();
            LoadApplications();
            GetVersionsInfo();

            IsConnected = true;
        }

        private void GetVersionsInfo()
        {
            FISCA.XHelper req = new FISCA.XHelper();
            req.AddElement(".", "ApplicationName", Application.SharedName);
            Envelope rsp = Connection.SendRequest("Server.GetApplicationInfo", new Envelope(req));
            XmlElement ver = XHelper.ParseAsDOM(rsp.Headers["Version"]);
            ServiceVersion = rsp.XResponseBody().GetText("DeployVersion");
            if (ver != null)
                CoreVersion = ver.InnerText;
            else
                CoreVersion = "見到鬼";

            DatabaseManager dm = new DatabaseManager(Manager, string.Empty);
            DBMSVersionString = dm.GetDBMSVersionString();
        }

        /// <summary>
        /// 更改目前已連線的使用者密碼。
        /// </summary>
        /// <param name="password"></param>
        public bool ChangeConnectionPassword(string oldPassword, string password)
        {
            try
            {
                Connection ds = new Connection();
                ds.EnableSecureTunnel = true;
                ds.Connect(RegistryData.AccessPointUrl, "", RegistryData.UserName, oldPassword);

                FISCA.XHelper req = new FISCA.XHelper();
                req.AddElement(".", "UserName", RegistryData.UserName);
                req.AddElement(".", "Password", password);
                ds.SendRequest("Account.ChangeManagerPassword", new Envelope(req)).XResponseBody();

                RegistryData.Password = password;
                Connection = ConnectInternal();
                Manager.Connection = Connection;

                Manager.ReloadServer();
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 重新載入 Server Configuration。
        /// </summary>
        public void ReloadConfiguration()
        {
            if (!IsConnected)
                throw new InvalidOperationException("只有在連線狀態才能呼叫此方法。");

            LoadConfiguration();
        }

        public void ReloadApplications()
        {
            if (!IsConnected)
                throw new InvalidOperationException("只有在連線狀態才能呼叫此方法。");

            LoadApplications();
        }

        public string AccessPointUrl { get; set; }

        public string LoginUserName { get; set; }

        /// <summary>
        /// 取得是否處於已連線狀態。
        /// </summary>
        public bool IsConnected { get; set; }

        public ServerConfiguration Configuration { get; private set; }

        public ServerManager Manager { get; private set; }

        public string ServiceLastUpdate { get; private set; }

        public string CoreLastUpdate { get; private set; }

        public string CoreVersion { get; private set; }

        public string ServiceVersion { get; private set; }

        public string DBMSVersionString { get; private set; }

        /// <summary>
        /// 資料庫的超級使用者。
        /// </summary>
        public AccountData SuperUser { get { return RegistryData.SuperUser; } }

        /// <summary>
        /// 樣版資料庫名稱。
        /// </summary>
        public string TemplateDatabase { get; set; }

        public Application GetSharedApplication()
        {
            foreach (Application each in Applications)
            {
                if (each.IsShared)
                    return each;
            }
            return null;
        }

        private Dictionary<string, Application> _apps = new Dictionary<string, Application>();
        public IEnumerable<Application> Applications { get { return _apps.Values; } }

        public void AddApplication(string name)
        {
            string target = name.ToLower();

            Manager.CloneApplication(name);
            XmlElement appconf = Manager.ListApplication(name);

            Application app = new Application(this);
            app.LoadDefinition(appconf.SelectSingleNode("Application") as XmlElement);
            _apps.Add(app.Name, app);

            if (ApplicationAdded != null)
                ApplicationAdded(this, new AppChangeEventArgs(app));
        }

        public void RemoveApplication(string name)
        {
            string target = name.ToLower();
            Application toremove = _apps[name];

            Manager.RemoveApplication(name);
            _apps.Remove(name);

            if (ApplicationRemoved != null)
                ApplicationRemoved(this, new AppChangeEventArgs(toremove));
        }

        public bool ContainsApplication(string name)
        {
            return _apps.ContainsKey(name);
        }

        public void SetApplicationArgument(Application.Argument arg)
        {
            if (!ContainsApplication(arg.Name))
                throw new ArgumentException("指定的 Application 不存在。");

            Application app = _apps[arg.Name];

            Manager.SetApplicationArgument(arg.ToXml());
            XmlElement rsp = Manager.ListApplication(arg.Name);
            app.LoadDefinition(rsp.SelectSingleNode("Application") as XmlElement);
            app.RaiseConfigChanged();
        }

        public void RenameApplication(string oldName, string newName)
        {
            if (!ContainsApplication(oldName))
                throw new ArgumentException("指定的 Application 不存在。");

            Application app = _apps[oldName];

            Manager.RenameApplication(oldName, newName);
            XmlElement rsp = Manager.ListApplication(newName);
            app.LoadDefinition(rsp.SelectSingleNode("Application") as XmlElement);
            _apps.Remove(oldName);
            _apps.Add(newName, app);
            app.RaiseNameChanged();
        }

        public void SetApplicationEnable(string name, bool enable)
        {
            if (!ContainsApplication(name))
                throw new ArgumentException("指定的 Application 不存在。");

            Application app = _apps[name];

            Manager.SetApplicationEnable(name, enable);
            XmlElement rsp = Manager.ListApplication(name);
            app.LoadDefinition(rsp.SelectSingleNode("Application") as XmlElement);
            app.RaiseConfigChanged();
        }

        public event EventHandler<AppChangeEventArgs> ApplicationAdded;

        public event EventHandler<AppChangeEventArgs> ApplicationRemoved;

        private Connection ConnectInternal()
        {
            Connection conn = new Connection();
            conn.EnableSecureTunnel = true;
            conn.Connect(RegistryData.AccessPointUrl, "", RegistryData.UserName, RegistryData.Password);
            return conn;
        }

        private void LoadConfiguration()
        {
            FISCA.XHelper rsp = new FISCA.XHelper(Manager.GetServerInfo());
            if (Configuration == null)
                Configuration = new ServerConfiguration();
            Configuration.FromXml(rsp.GetElement("Property[@Name='ServerXml']/ApplicationServer"));

            ServiceLastUpdate = rsp.GetText("UpdateInfo");
            CoreLastUpdate = rsp.GetText("SVN/@Date");
        }

        private void LoadApplications()
        {
            FISCA.XHelper rsp = new FISCA.XHelper(Manager.ListApplication());
            _apps = new Dictionary<string, Application>();

            foreach (XmlElement each in rsp.GetElements("Application"))
            {
                Application app = new Application(this);
                app.LoadDefinition(each);
                _apps.Add(app.Name, app);
            }
        }
    }
}
