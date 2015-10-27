using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DevComponents.DotNetBar.Rendering;
using System.Xml;
using DevComponents.DotNetBar;
using System.IO;
using FISCA.DSAUtil;

namespace FISCA
{
    /// <summary>
    /// 啟動系統
    /// </summary>
    public static class ApplicationStarter
    {
        /// <summary>
        /// 啟動系統
        /// </summary>
        public static void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.CurrentCulture = new System.Globalization.CultureInfo("zh-TW", false);
            Application.Run(new AppContex(new AppInfo()));
        }
        /// <summary>
        /// 啟動系統
        /// </summary>
        /// <param name="appInfo">系統初始設定</param>
        public static void Run(AppInfo appInfo)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.CurrentCulture = new System.Globalization.CultureInfo("zh-TW", false);
            Application.Run(new AppContex(appInfo));
        }
        /// <summary>
        /// 使用者登入成功，開始載入擴充模組之前
        /// </summary>
        public static event EventHandler SystemStartUp;

        private class AppContex : ApplicationContext
        {
            private AssemblyCenter Resolver;
            private AppInfo _Info = null;
            private Form _LoginForm = null;
            private List<string> InstalledModules = new List<string>(); //已安裝的模組 Url 清單。
            private PrivateControls.InitializationManager _Manager = new FISCA.PrivateControls.InitializationManager();

            public AppContex(AppInfo info)
            {
                #region 讀取預設樣式資訊
                string filename = Application.StartupPath + "\\" + "configuration.xml";
                bool hasLoginInfo = System.IO.File.Exists(filename);
                if (hasLoginInfo)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filename);
                    XmlElement ele = (XmlElement)doc.SelectSingleNode("Informations/Style");
                    if (ele != null)
                    {
                        switch (ele.GetAttribute("RibbonColor"))
                        {
                            case "Black":
                                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(eOffice2007ColorScheme.Black);
                                break;
                            case "Blue":
                                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(eOffice2007ColorScheme.Blue);
                                break;
                            case "Silver":
                                RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(eOffice2007ColorScheme.Silver);
                                break;
                        }
                    }
                }
                ((Office2007Renderer)GlobalManager.Renderer).ColorTableChanged += delegate
                {
                    #region 寫入預設樣式資訊
                    if (hasLoginInfo)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(filename);
                        XmlElement inf = (XmlElement)doc.SelectSingleNode("Informations");
                        XmlElement ele = (XmlElement)doc.SelectSingleNode("Informations/Style");
                        if (inf != null && ele == null)
                        {
                            ele = doc.CreateElement("Style");
                            inf.AppendChild(ele);
                        }
                        eOffice2007ColorScheme colorScheme = ((Office2007Renderer)GlobalManager.Renderer).ColorTable.InitialColorScheme;
                        switch (colorScheme)
                        {
                            case eOffice2007ColorScheme.Black:
                                ele.SetAttribute("RibbonColor", "Black");
                                break;
                            case eOffice2007ColorScheme.Blue:
                                ele.SetAttribute("RibbonColor", "Blue");
                                break;
                            case eOffice2007ColorScheme.Silver:
                                ele.SetAttribute("RibbonColor", "Silver");
                                break;
                        }
                        try
                        {
                            doc.Save(filename);
                        }
                        catch { }
                    }
                    #endregion
                };
                #endregion
                Resolver = new AssemblyCenter(info.DevelopMode); //自定組件解析器。
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolve);
                _Info = info;
                if (info.CustomizationLoginForm == null)
                {
                    _LoginForm = new FISCA.PrivateControls.LoginForm();
                    if (info.LoginFormLogo != null)
                        ((FISCA.PrivateControls.LoginForm)_LoginForm).SetPicture(info.LoginFormLogo);
                }
                else
                    _LoginForm = info.CustomizationLoginForm;
                Application.Idle += new EventHandler(Application_Idle);

            }

            private int _RunStep = 0;
            private bool _NextStep = true;
            private bool _ActiveLoginForm = false;
            private void Application_Idle(object sender, EventArgs e)
            {
                if (_ActiveLoginForm)
                {
                    if (_LoginForm.ActiveMdiChild == null)
                    {
                        _LoginForm.Activate();
                    }
                    else
                    {
                        Form f = _LoginForm.ActiveMdiChild;
                        while (f.ActiveMdiChild == null)
                        {
                            f = f.ActiveMdiChild;
                        }
                        f.Activate();
                    }
                    _ActiveLoginForm = false;
                }
                if (_NextStep)
                {
                    _NextStep = false;
                    switch (_RunStep)
                    {
                        case 0:
                            #region 顯示啟動畫面
                            if (_Info.CheckFont)
                            {
                                FISCA.Presentation.FontsChecker.Check(true);
                            }
                            _Manager.SetMessage("檢查核心程式更新...");
                            _Manager.Icon = _Info.Icon;
                            _Manager.ShowInTaskbar = _Info.Icon != null;
                            _Manager.FormClosing += delegate
                            {
                                if (_RunStep != 6)
                                    ExitThread();
                            };
                            this.MainForm = _Manager;
                            _Manager.Show();
                            _RunStep = 1;
                            _NextStep = true;
                            System.Windows.Forms.Application.DoEvents();
                            Application.RaiseIdle(new EventArgs());
                            #endregion
                            break;
                        case 1:
                            #region 檢查核心程式更新...
                            if (!string.IsNullOrEmpty(_Info.KernelUrl))
                            {
                                try
                                {
                                    FISCA.Deployment.ModuleUrl url = new FISCA.Deployment.ModuleUrl(_Info.KernelUrl);

                                    FISCA.Deployment.DeployFolder folder = new FISCA.Deployment.DeployFolder(Application.StartupPath,
                                        FISCA.Deployment.FolderType.TargetFolder);

                                    FISCA.Deployment.ModuleDeployment mod = new FISCA.Deployment.ModuleDeployment(url, folder,
                                        FISCA.Deployment.Consts.ReleaseBuild);

                                    //如果需要更新核心。
                                    if (mod.IsUpdateRequired)
                                    {
                                        FISCA.PrivateControls.KernelDeployProgress kdp = new FISCA.PrivateControls.KernelDeployProgress(mod);
                                        kdp.ShowDialog();
                                    }

                                }
                                catch { }
                            }
                            _RunStep = 2;
                            _NextStep = true;
                            System.Windows.Forms.Application.DoEvents();
                            Application.RaiseIdle(new EventArgs());
                            #endregion
                            break;
                        case 2:
                            #region 使用者登入
                            _LoginForm.FormClosing += delegate
                            {
                                if (!FISCA.Authentication.DSAServices.IsLogined)
                                {
                                    ExitThread();
                                }
                                else
                                {
                                    _Manager.SetMessage("啟動核心系統...");
                                    _RunStep = 3;
                                    _NextStep = true;
                                }
                            };
                            _LoginForm.Icon = _Info.Icon;
                            _Manager.Activated += delegate
                            {
                                if (_RunStep == 2 && !_LoginForm.ContainsFocus)
                                    _ActiveLoginForm = true;
                            };
                            _Manager.SetMessage("使用者登入");
                            _LoginForm.Show();
                            System.Windows.Forms.Application.DoEvents();
                            Application.RaiseIdle(new EventArgs());
                            #endregion
                            break;
                        case 3:
                            #region 啟動核心系統
                            if (SystemStartUp != null)
                                SystemStartUp(this, new EventArgs());
                            _RunStep = 4;
                            _NextStep = true;
                            System.Windows.Forms.Application.DoEvents();
                            Application.RaiseIdle(new EventArgs());
                            #endregion
                            break;
                        case 4:
                            #region 檢查及更新擴充模組
                            _Manager.SetMessage("檢查及更新擴充模組...");
                            UpdateModules();
                            _RunStep = 5;
                            _NextStep = true;
                            System.Windows.Forms.Application.DoEvents();
                            Application.RaiseIdle(new EventArgs());
                            #endregion
                            break;
                        case 5:
                            #region 載入擴充模組
                            _Manager.SetMessage("載入擴充模組...");
                            //在更新之後建立 Assembly 快取。
                            Resolver.CacheAssembly();
                            //啟動所有其他模組
                            ModuleLoader loader = new ModuleLoader(Resolver);
                            loader.ModuleLoading += delegate(object sender2, ModuleLoadingArgs e2)
                            {
                                _Manager.SetAssembly(e2.DisplayName);
                                System.Windows.Forms.Application.DoEvents();
                                Application.RaiseIdle(new EventArgs());
                            };
                            loader.Load();
                            _RunStep = 6;
                            _NextStep = true;
                            System.Windows.Forms.Application.DoEvents();
                            Application.RaiseIdle(new EventArgs());
                            #endregion
                            break;
                        case 6:
                            #region 啟動MotherForm
                            this.MainForm = FISCA.Presentation.MotherForm.Form;
                            FISCA.Presentation.MotherForm.Form.Icon = _Info.Icon;
                            _Manager.Close();
                            Application.Idle -= new EventHandler(Application_Idle);//反向註冊事件
                            if (_Info.DisplayLoadingMessage)
                                Authentication.DSAServices.AutoDisplayLoadingMessageOnMotherForm();
                            FISCA.Presentation.MotherForm.Form.Show();
                            #endregion
                            break;
                    }
                }
            }

            private System.Reflection.Assembly AssemblyResolve(object sender, ResolveEventArgs args)
            {
                if (Resolver == null) return null;
                return Resolver.Get(args.Name);
            }
            private static DSXmlHelper GetModule(params string[] id)
            {
                DSXmlHelper request = new DSXmlHelper("Request");
                request.AddElement("Condition");

                foreach (string each in id)
                    request.AddElement("Condition", "ID", each);

                DSResponse response = FISCA.Authentication.DSAServices.CallService("SmartSchool.Module.GetModule", new DSRequest(request));

                return response.GetContent();
            }
            private void UpdateModules()
            {
                if (!_Info.FreezeExtension)
                {
                    if (!Directory.Exists(Paths.Module))
                        Directory.CreateDirectory(Paths.Module);

                    //先把該刪的模組刪一刪。
                    foreach (string each in Directory.GetDirectories(Paths.Module))
                    {
                        DirectoryInfo folder = new DirectoryInfo(each);
                        FileInfo[] files = folder.GetFiles("delete.required");
                        if (files.Length > 0)
                        {
                            string update_temporal = Path.Combine(Paths.Executable, "_update_temporal");

                            if (!Directory.Exists(update_temporal)) Directory.CreateDirectory(update_temporal);

                            try { folder.Delete(true); }
                            catch
                            {
                                try { folder.MoveTo(Path.Combine(update_temporal, Path.GetRandomFileName())); }
                                catch { }
                            }
                        }
                    }

                    //下載模組清單。
                    DSXmlHelper response = GetModule();
                    foreach (XmlElement each in response.GetElements("Module"))
                    {
                        string moduleUrl = each.SelectSingleNode("ModuleUrl").InnerText;
                        InstalledModules.Add(moduleUrl);
                    }

                    //檢查模組更新。
                    if (InstalledModules.Count > 0)
                    {
                        FISCA.PrivateControls.ModuleDeployProgress mdp = new FISCA.PrivateControls.ModuleDeployProgress(InstalledModules);
                        mdp.ShowDialog();
                    }
                }
            }
        }
    }
}
