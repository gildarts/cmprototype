using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using DevComponents.DotNetBar;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using DevComponents.AdvTree;
using System.Threading;
using Aspose.Cells;
using FISCA.DSA;
using FISCA.Deployment;

namespace Manager
{
    public partial class MainForm : Office2007Form
    {
        static MainForm()
        {
            MarkList = new Dictionary<string, Dictionary<string, string>>();
        }

        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            ReadTitle();
        }

        private static MainForm Instance { get; set; }

        /// <summary>
        /// 畫面進度條。
        /// </summary>
        public static ProgressBarItem Progress { get { return Instance.progress; } }

        public static void SetBarMessage(string message)
        {
            try
            {
                if (Instance.InvokeRequired)
                    Instance.Invoke(new Action<string>(x => Instance.liMessage.Text = x), message);
                else
                    Instance.liMessage.Text = message;
            }
            catch { }
        }

        public static void CreateManagerTree(XmlElement configuration)
        {
            XmlElement config = configuration;
            if (Control.ModifierKeys.HasFlag(Keys.Shift))
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "*.state|*.state";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(dialog.FileName);
                    config = doc.DocumentElement;
                }
            }

            ResetManagerTree();
            Instance.CreateServerTree(config, Instance.DSATree.Nodes);
        }

        public static XmlElement GetManagerTreeState()
        {
            Instance.ReadTitle();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<Configuration/>");
            Instance.SaveServerTree(doc.DocumentElement, Instance.DSATree.Nodes);
            return doc.DocumentElement;
        }

        public static void ResetManagerTree()
        {
            Instance.ReadTitle();
            Instance.DSATree.Nodes.Clear();
            Instance.SetContent(null);
            Program.Servers.Clear();
        }

        internal static bool TryConnect(AdvNode<Server> node)
        {
            return Instance.TryConnectInternal(node);
        }

        internal static void ViewServer(Server srv)
        {
            ServerManagePanel.Instance.SetServerObject(srv);
            Instance.SetContent(ServerManagePanel.Instance);
        }

        internal static void SelectNode(Node node)
        {
            Instance.DSATree.SelectedNode = node;
        }

        private void ReadTitle()
        {
            string version = "0.0.0.0";
            try
            {
                ReleaseManifest rm = new ReleaseManifest();
                rm.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ReleaseManifest.StandardFileName));
                version = rm.Version.ToString();
            }
            catch { }

            Text = string.Format("DSA Server Manager [{0}] [{1}]", Program.OnlineMode ? "線上" : "離線", version);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string basePath = System.Windows.Forms.Application.StartupPath;
            //if (!File.Exists(Path.Combine(basePath, "停止更新")))
            //    Program.Update();

            ResetManagerTree();

            string configPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "configuration.xml");
            if (!File.Exists(configPath)) return;

            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);

            CreateManagerTree(doc.DocumentElement);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Program.OnlineMode)
                LoginForm.SaveState();
            else
            {
                SaveStateToLocal();
            }
        }

        private static void SaveStateToLocal()
        {
            //本機不在儲存任何 Server 資料。
            //XmlElement data = GetManagerTreeState();
            //data.OwnerDocument.Save(Path.Combine(System.Windows.Forms.Application.StartupPath, "configuration.xml"));
        }

        private void SaveServerTree(XmlElement settings, NodeCollection nodes)
        {
            FISCA.XHelper helper = new FISCA.XHelper(settings);

            foreach (Node each in nodes)
            {
                if (each is AdvNode<ServerGroup>)
                {
                    AdvNode<ServerGroup> obj = each as AdvNode<ServerGroup>;
                    FISCA.XHelper hgroup = new FISCA.XHelper(helper.AddElement("ServerGroup"));
                    hgroup.SetAttribute(".", "Title", each.Text);
                    hgroup.AddElement(".", "Memo", obj.BehindObject.Memo);

                    if (each.IsSelected)
                        hgroup.SetAttribute(".", "Selected", "True");

                    SaveServerTree(hgroup.Data, each.Nodes);
                }
                else if (each is AdvNode<Server>)
                {
                    AdvNode<Server> obj = each as AdvNode<Server>;
                    FISCA.XHelper hserver = new FISCA.XHelper(obj.BehindObject.RegistryData.ToXml());

                    if (each.IsSelected)
                        hserver.SetAttribute(".", "Selected", "True");

                    hserver.SetAttribute(".", "TemplateDatabase", obj.BehindObject.TemplateDatabase);

                    helper.AddElement(".", hserver.Data);
                }
            }
        }

        private void CreateServerTree(XmlElement settings, NodeCollection nodes)
        {
            foreach (XmlElement each in settings.SelectNodes("Server | ServerGroup"))
            {
                FISCA.XHelper helper = new FISCA.XHelper(each);

                if (string.Equals(each.LocalName, "ServerGroup", StringComparison.CurrentCultureIgnoreCase))
                {
                    AdvNode<ServerGroup> group = CreateServerGroupNode(helper.GetText("@Title"),
                        helper.GetText("Memo"),
                        Images.DSAGroup);
                    nodes.Add(group);
                    CreateServerTree(each, group.Nodes);

                    bool selected = helper.TryGetBoolean("@Selected", false);
                    if (selected)
                        DSATree.SelectedNode = group;
                }
                else if (string.Equals(each.LocalName, "Server", StringComparison.CurrentCultureIgnoreCase))
                {
                    ServerRegistryData reg = new ServerRegistryData();
                    reg.FromXml(each);
                    Server srv = new Server(reg);
                    Program.Servers.Add(srv);
                    AdvNode<Server> server = CreateServerNode(srv, Images.Disable_Server);
                    nodes.Add(server);

                    srv.TemplateDatabase = helper.GetText("@TemplateDatabase");

                    bool selected = helper.TryGetBoolean("@Selected", false);
                    if (selected)
                        DSATree.SelectedNode = server;
                }
            }
        }

        private void mnuRegistry_Click(object sender, EventArgs e)
        {
            RegistryForm rf = new RegistryForm();

            if (rf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Program.Servers.Contains(rf.RegistryData.AccessPointUrl))
                {
                    MessageBox.Show("已經註冊相同的 DSA Server。");
                    return;
                }

                Server srv = new Server(rf.RegistryData);
                AsyncRunner<Server, object> runner = new AsyncRunner<Server, object>();
                runner.Arguments = srv;
                runner.Message = "載入 Server 資訊中...";
                runner.MessageOwner = this;
                runner.Run(arg => arg.Arguments.Connect(),
                    arg =>
                    {
                        if (arg.IsTaskError)
                        {
                            ErrorForm ef = new ErrorForm();
                            ef.Display(arg.TaskError.Message, arg.TaskError);
                        }
                        else
                        {
                            Program.Servers.Add(srv);

                            AdvNode<Server> an = CreateServerNode(srv, Images.Normal_Server);
                            DSATree.Nodes.Add(an);
                        }
                    });
            }
        }

        private void DSATree_SelectionChanged(object sender, EventArgs e)
        {
            if (DSATree.SelectedNode != null)
            {
                Node node = DSATree.SelectedNode;

                if (node is AdvNode<Server>)
                {
                    AdvNode<Server> n = node as AdvNode<Server>;

                    TryConnectInternal(n);

                    if (n.BehindObject.IsConnected)
                    {
                        ServerManagePanel.Instance.SetServerObject(n.BehindObject);
                        SetContent(ServerManagePanel.Instance);
                    }
                    else
                        SetContent(null);
                }
                else if (node is AdvNode<ServerGroup>)
                {
                    AdvNode<ServerGroup> n = node as AdvNode<ServerGroup>;

                    ServerGroupPanel.Instance.SetGroupObject(n);
                    SetContent(ServerGroupPanel.Instance);
                }
                else
                    SetContent(null);
            }
        }

        private bool TryConnectInternal(AdvNode<Server> n)
        {
            Server srv = n.BehindObject;

            if (!srv.IsConnected)
            {
                DialogResult dr = MessageBox.Show("是否載入 Server 資訊？", "DSA Manager", MessageBoxButtons.YesNo);

                if (dr == System.Windows.Forms.DialogResult.No)
                    return false;

                AsyncRunner runner = new AsyncRunner();
                runner.Message = "載入 Server 資訊中...";
                runner.MessageOwner = this;
                runner.Run(arg =>
                {
                    srv.Connect();
                },
                    arg =>
                    {
                        if (arg.IsTaskError)
                        {
                            ErrorForm ef = new ErrorForm();
                            ef.Display(arg.TaskError.Message, arg.TaskError);
                        }
                    });
            }

            if (srv.IsConnected)
                n.Image = Images.Normal_Server;

            return srv.IsConnected;
        }

        private void btnRefreshServer_Click(object sender, EventArgs e)
        {
            if (DSATree.SelectedNode != null)
            {
                if (DSATree.SelectedNode is AdvNode<Server>)
                {
                    AdvNode<Server> n = DSATree.SelectedNode as AdvNode<Server>;
                    Server srv = n.BehindObject;

                    AsyncRunner runner = new AsyncRunner();
                    runner.Message = "Reload Server Configuration...";
                    runner.MessageOwner = this;
                    runner.Run(
                        x =>
                        {
                            srv.Connect();
                            srv.Manager.ReloadServer();
                        },
                        x =>
                        {
                            if (x.IsTaskError)
                            {
                                ErrorForm err = new ErrorForm();
                                err.Display(x.TaskError.Message, x.TaskError);
                            }
                        });

                    if (!runner.IsTaskError)
                    {
                        ServerManagePanel.Instance.SetServerObject(srv);
                        SetContent(ServerManagePanel.Instance);
                    }
                }
            }
        }

        private void SetContent(UserControl control)
        {
            if (control == null)
            {
                splitContainer1.Panel2.Controls.Clear();
                return;
            }

            UserControl previousControl = null;
            if (splitContainer1.Panel2.Controls.Count > 0)
                previousControl = splitContainer1.Panel2.Controls[0] as UserControl;

            if (control == previousControl) return;

            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(control);
        }

        private void mnuUpdate_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("這個動作會更新已註冊所有 DSA Server 的 Service，您確定要立即執行？", "Prototype", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                bool enforce = false;

                XmlResultAsyncRunner runner = new XmlResultAsyncRunner();
                runner.Message = "更新 DSA Server Services 中...";
                runner.MessageOwner = this;
                runner.Run(
                    arg =>
                    {
                        FISCA.XHelper rsp = new FISCA.XHelper();

                        System.Threading.Tasks.Parallel.ForEach<Server>(Program.Servers, each =>
                        {
                            try
                            {
                                XmlElement result = each.Manager.UpdateServices(enforce);
                                lock (rsp)
                                {
                                    rsp.AddElement("ServerResult");
                                    rsp.SetAttribute("ServerResult", "ManagerAccessPoint", each.AccessPointUrl);
                                    rsp.AddElement("ServerResult", result);
                                }
                            }
                            catch (Exception ex)
                            {
                                lock (rsp)
                                {
                                    rsp.AddElement(".", "ServerResult", ex.Message);
                                    rsp.SetAttribute("ServerResult", "ManagerAccessPoint", each.AccessPointUrl);
                                    rsp.AddElement("ServerResult", "Exception", ex.Message);
                                }
                            }
                        });

                        arg.Result = rsp.Data;
                    },
                    arg =>
                    {
                        if (arg.IsTaskError)
                        {
                            ErrorForm ef = new ErrorForm();
                            ef.Display("更新 DSA Server Service 錯誤。", arg.TaskError);
                        }

                        MessageBox.Show(FISCA.XHelper.Format(arg.Result.OuterXml));
                    });
            }
        }

        private void btnRemoveServer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定要移除選擇的項目？", "Prototype", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                return;

            if (DSATree.SelectedNode != null)
            {
                if (DSATree.SelectedNode is AdvNode<Server>)
                {
                    AdvNode<Server> n = DSATree.SelectedNode as AdvNode<Server>;
                    Server srv = n.BehindObject;

                    Program.Servers.Remove(srv);

                    if (n.Parent == null)
                        DSATree.Nodes.Remove(n);
                    else
                        n.Parent.Nodes.Remove(n);

                    SetContent(null);
                }
                else
                {
                    AdvNode<ServerGroup> n = DSATree.SelectedNode as AdvNode<ServerGroup>;
                    if (n.Parent == null)
                        DSATree.Nodes.Remove(DSATree.SelectedNode);
                    else
                        n.Parent.Nodes.Remove(n);
                }
            }
        }

        private void mnuAddServerGroup_Click(object sender, EventArgs e)
        {
            InputBox input = new InputBox("請輸入名稱");

            input.Confirming += delegate(object sender1, CancelEventArgs arg)
            {
                //MessageBox.Show("Confirm....");
            };

            if (input.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AdvNode<ServerGroup> node = CreateServerGroupNode(input.InputString, string.Empty, Images.DSAGroup);
                DSATree.Nodes.Add(node);
            }
        }

        private static AdvNode<ServerGroup> CreateServerGroupNode(string title, string memo, Image image)
        {
            ServerGroup obj = new ServerGroup();
            obj.Memo = memo;

            AdvNode<ServerGroup> node = new AdvNode<ServerGroup>(obj);
            node.Text = title;
            node.Image = image;
            return node;
        }

        private static AdvNode<Server> CreateServerNode(Server srv, Image image)
        {
            bool setSuperUser = (srv.SuperUser != AccountData.Default);

            AdvNode<Server> an = new AdvNode<Server>(srv);
            an.Image = image;
            an.Text = GetServerTitle(srv);
            return an;
        }

        private void DSATree_NodeDragFeedback(object sender, DevComponents.AdvTree.TreeDragFeedbackEventArgs e)
        {
            if (e.ParentNode != null)
            {
                if (!(e.ParentNode is AdvNode<ServerGroup>))
                    e.AllowDrop = false;
            }

            Console.WriteLine(e.DragNode.ToString());

            if (e.ParentNode != null)
                Console.WriteLine(e.ParentNode.ToString());
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            AdvNode<ServerGroup> n = DSATree.SelectedNode as AdvNode<ServerGroup>;

            InputBox input = new InputBox("輸入新名稱", n.Text);

            if (input.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                n.Text = input.InputString;
        }

        private void TreeContextMenu_PopupShowing(object sender, EventArgs e)
        {

        }

        private void TreeContextMenu_PopupOpen(object sender, PopupOpenEventArgs e)
        {
            bool isServer = DSATree.SelectedNode is AdvNode<Server>;
            bool isGroup = DSATree.SelectedNode is AdvNode<ServerGroup>;

            e.Cancel = !(isServer || isGroup);

            btnRefreshServer.Visible = isServer;
            btnRename.Visible = isGroup;
            btnUpdateService.Visible = isServer | isGroup;
            btnChangeRegistry.Visible = isServer;
            btnSetSuperUser.Visible = isServer;
            btnRemoveServer.Visible = isServer | isGroup;

            if (isServer)
            {
                Server srv = (DSATree.SelectedNode as AdvNode<Server>).BehindObject;
                btnSetSuperUser.Enabled = srv.IsConnected;
            }
        }

        private void btnUpdateService_Click(object sender, EventArgs e)
        {
            if (DSATree.SelectedNode != null)
            {
                DialogResult confirm = MessageBox.Show("您確定要立即更新 Services？", "Prototype", MessageBoxButtons.YesNo);
                if (confirm != DialogResult.Yes)
                    return;

                if (DSATree.SelectedNode is AdvNode<Server>)
                {
                    AdvNode<Server> n = DSATree.SelectedNode as AdvNode<Server>;
                    Server srv = n.BehindObject;

                    try
                    {
                        UpdateSingleServerService(srv);
                        ServerManagePanel.Instance.SetServerObject(srv);
                    }
                    catch (Exception ex)
                    {
                        ErrorForm.Show(ex.Message, ex);
                    }
                }
                else if (DSATree.SelectedNode is AdvNode<ServerGroup>)
                {
                    AdvNode<ServerGroup> group = DSATree.SelectedNode as AdvNode<ServerGroup>;
                    MultiTaskingRunner runner = new MultiTaskingRunner();
                    PrepareBatchServiceUpdate(group, runner);
                    runner.ExecuteTasks();
                    ServerGroupPanel.Instance.SetGroupObject(group);
                }
            }
        }

        private void PrepareBatchServiceUpdate(AdvNode<ServerGroup> advNode, MultiTaskingRunner runner)
        {
            foreach (Node each in advNode.Nodes)
            {
                if (each is AdvNode<Server>)
                {
                    Server srv = (each as AdvNode<Server>).BehindObject;
                    CancellationTokenSource source = new CancellationTokenSource();
                    string name = string.Format("({0}){1}", srv.RegistryData.Memo, srv.RegistryData.AccessPointUrl);
                    runner.AddTask(name, state => UpdateSingleServerService(state as Server), srv, source);
                }
                else if (each is AdvNode<ServerGroup>)
                    PrepareBatchServiceUpdate(each as AdvNode<ServerGroup>, runner);
            }
        }

        private void UpdateSingleServerService(Server srv)
        {
            if (!srv.IsConnected) throw new InvalidOperationException("未在連線狀態，無法更新。");

            bool enforce = true;

            XmlElement result = srv.Manager.UpdateServices(enforce);
            srv.Manager.ReloadServer();
            srv.Connect(); //重新連線已更新相關資料。

            if (result.SelectNodes("Result[@Status!='0']").Count > 0)
                throw new FISCA.DSA.DSAServerException("504", "更新 DSA Server Service 錯誤，請檢查詳細資料。", "", result.OuterXml, null);
        }

        #region 更改註冊資訊
        private void btnChangeRegistry_Click(object sender, EventArgs e)
        {
            if (DSATree.SelectedNode != null)
            {
                if (DSATree.SelectedNode is AdvNode<Server>)
                {
                    AdvNode<Server> n = DSATree.SelectedNode as AdvNode<Server>;
                    Server srv = n.BehindObject;
                    ServerRegistryData regdata = srv.RegistryData;
                    string oldPassword = regdata.Password;

                    if (RegistryForm.Confirm(ref regdata) == System.Windows.Forms.DialogResult.OK)
                    {
                        srv.RegistryData = regdata;
                        srv.Connect();
                        n.Image = Images.Normal_Server;
                        ServerManagePanel.Instance.SetServerObject(srv);
                        SetContent(ServerManagePanel.Instance);
                    }
                }
            }
        }
        #endregion

        private void txtSetSuperUser_Click(object sender, EventArgs e)
        {
            if (DSATree.SelectedNode != null)
            {
                if (DSATree.SelectedNode is AdvNode<Server>)
                {
                    AdvNode<Server> node = (DSATree.SelectedNode as AdvNode<Server>);
                    Server srv = node.BehindObject;
                    SetSuperUserForm form = new SetSuperUserForm(srv);
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        srv.RegistryData.SuperUser = form.Result;
                        node.Text = GetServerTitle(srv);
                    }
                }
            }
        }

        private static string GetServerTitle(Server srv)
        {
            bool isSuperUser = srv.SuperUser != AccountData.Default;
            return string.Format("{0}{1}", isSuperUser ? "(超級權限)" : "", srv.AccessPointUrl);
        }

        private void btnConnectToCenter_Click(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            lf.ShowDialog();
        }

        private void btnNameService_Click(object sender, EventArgs e)
        {
            new NameServiceForm().ShowDialog();
        }

        private void btnChangeSecureCode_Click(object sender, EventArgs e)
        {
            new ChangeSecureCode().ShowDialog();
        }

        private void btnClearSetting_Click(object sender, EventArgs e)
        {
            string msg = "您確定要清除所有設定？";

            if (MessageBox.Show(msg, "Prototype", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                Program.SetOnlinePreference(LoginForm.WidgetKey, LoginForm.StateID, string.Empty);
                MainForm.ResetManagerTree();
                Program.OnlineMode = false;
                Program.EncryptPassword = true;
                MessageBox.Show("清除完成，請重新登入。");
            }
        }

        private void btnSecurity_PopupOpen(object sender, PopupOpenEventArgs e)
        {
            btnChangeSecureCode.Enabled = Program.OnlineMode;
            btnClearSetting.Enabled = Program.OnlineMode;
        }

        /// <summary>
        /// 存放所有需要特殊標記的 DSNS 名稱清單。
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> MarkList { get; private set; }

        private void btnImportMarkList_Click(object sender, EventArgs e)
        {
            try
            {
                Aspose.Cells.License lic = new Aspose.Cells.License();
                lic.SetLicense(new MemoryStream(Properties.Resources.Aspose_Total));

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Excel 檔案(*.xls)|*.xls";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Workbook book = new Workbook();
                    book.Open(dialog.FileName);

                    Worksheet sheet = book.Worksheets[0];

                    LoadSheetData(sheet);
                    new MarkListForm().ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadSheetData(Worksheet sheet)
        {
            MarkList = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

            //建立欄位清單。
            Dictionary<string, int> columnMap = new Dictionary<string, int>();
            for (int i = 0; i <= sheet.Cells.MaxDataColumn; i++)
                columnMap.Add(sheet.Cells[0, i].StringValue, i);

            //讀取 Excel 資料。
            for (int row = 1; row <= sheet.Cells.MaxDataRow; row++)
            {
                string ap = sheet.Cells[row, columnMap["accesspoint"]].StringValue;

                Dictionary<string, string> columnValues = new Dictionary<string, string>();

                foreach (string column in columnMap.Keys)
                {
                    string value = sheet.Cells[row, columnMap[column]].StringValue;
                    columnValues.Add(column, value);
                }

                MarkList.Add(ap, columnValues);
            }
        }

        private void btnNameServiceMan_Click(object sender, EventArgs e)
        {
            new NameService.MainManager().Show();
        }

        private void mnuChangePassword_Click(object sender, EventArgs e)
        {
            if (DSATree.SelectedNode != null)
            {
                if (DSATree.SelectedNode is AdvNode<Server>)
                {
                    AdvNode<Server> n = DSATree.SelectedNode as AdvNode<Server>;
                    Server srv = n.BehindObject;

                    ChangePassword chpwd = new ChangePassword();

                    while (chpwd.ShowDialog() == DialogResult.OK)
                    {
                        if (srv.ChangeConnectionPassword(chpwd.OldPassword, chpwd.NewPassword))
                        {
                            if (Program.OnlineMode)
                            {
                                LoginForm.SaveState();
                                srv.ReloadConfiguration();
                            }

                            break;
                        }

                        MessageBox.Show("變更密碼失敗，請確認原密碼是否正確。");
                    }
                }
            }
        }

        private void btnSaveState_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "*.state|*.state";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                XmlDocument doc = MainForm.GetManagerTreeState().OwnerDocument;
                doc.Save(dialog.FileName);
            }
        }
    }
}