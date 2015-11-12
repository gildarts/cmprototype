using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Aspose.Cells;
using FISCA;
using FISCA.Authentication;
using FISCA.DSA;
using Manager.Interfaces;

namespace Manager
{
    public partial class ServerManagePanel : UserControl
    {
        private static ServerManagePanel panel = null;

        private static Color MarkForecolor = Color.Red;

        public static ServerManagePanel Instance
        {
            get
            {
                if (panel == null)
                {
                    panel = new ServerManagePanel();
                    panel.Dock = DockStyle.Fill;
                }
                return panel;
            }
        }

        public ServerManagePanel()
        {
            InitializeComponent();

            PrepareTask = null;
        }

        private Server CurrentServer { get; set; }

        private bool HasSelectedItem { get { return dgvSchoolManageList.SelectedRows.Count > 0; } }

        private DataGridViewRow SelectedRow
        {
            get
            {
                if (HasSelectedItem)
                {
                    DataGridViewRow row = dgvSchoolManageList.SelectedRows[0];
                    return row;
                }
                else
                    return null;
            }
        }

        private Application SelectedApp
        {
            get
            {
                if (HasSelectedItem)
                {
                    DataGridViewRow row = dgvSchoolManageList.SelectedRows[0];
                    Application app = row.Tag as Application;
                    return app;
                }
                else
                    return null;
            }
        }

        internal void SetServerObject(Server srv)
        {
            //btnViewLog.Visible = Program.IsAdministrator;
            btnDBWindow.Visible = Program.IsAdministrator;

            if (CurrentServer != null)
            {
                if (CurrentServer.Configuration != null)
                    CurrentServer.Configuration.Changed -= new EventHandler(Configuration_ContentRefreshed);

                CurrentServer.ApplicationAdded -= new EventHandler<AppChangeEventArgs>(CurrentServer_ApplicationAdded);
                CurrentServer.ApplicationRemoved -= new EventHandler<AppChangeEventArgs>(CurrentServer_ApplicationRemoved);
            }

            CurrentServer = srv;
            CurrentServer.Configuration.Changed += new EventHandler(Configuration_ContentRefreshed);
            CurrentServer.ApplicationAdded += new EventHandler<AppChangeEventArgs>(CurrentServer_ApplicationAdded);
            CurrentServer.ApplicationRemoved += new EventHandler<AppChangeEventArgs>(CurrentServer_ApplicationRemoved);

            InitialConfigurationPanel();
        }

        private void RemoveApplicationRow(AppChangeEventArgs e)
        {
            foreach (DataGridViewRow each in dgvSchoolManageList.Rows)
            {
                if (object.ReferenceEquals(each.Tag, e.TargetApp))
                {
                    dgvSchoolManageList.Rows.Remove(each);
                    e.TargetApp.NameChanged -= new EventHandler(each_NameChanged);
                    e.TargetApp.ConfigChanged -= new EventHandler(each_ConfigChanged);
                }
            }
        }

        private void CurrentServer_ApplicationAdded(object sender, AppChangeEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new Action(() =>
                {
                    CreateSchoolManagerRow(e.TargetApp);
                }));
            else
                CreateSchoolManagerRow(e.TargetApp);
        }

        private void CurrentServer_ApplicationRemoved(object sender, AppChangeEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new Action(() => RemoveApplicationRow(e)));
            else
                RemoveApplicationRow(e);
        }

        private void Configuration_ContentRefreshed(object sender, EventArgs e)
        {
            if (InvokeRequired)
                Invoke(new Action(() => InitialConfigurationPanel()));
            else
                InitialConfigurationPanel();
        }

        private XmlResultAsyncRunner PrepareTask { get; set; }
        private bool ReadingTaskPadding { get; set; }
        /// <summary>
        /// 學校清單。
        /// </summary>
        private XmlElement Applications { get; set; }

        private void InitialConfigurationPanel()
        {
            txtAccessPointUrl.Text = CurrentServer.AccessPointUrl;
            txtCoreVersion.Text = CurrentServer.CoreVersion;
            txtServiceVersion.Text = CurrentServer.ServiceVersion;
            txtUserName.Text = CurrentServer.LoginUserName;
            txtServiceLastUpdate.Text = DTParser.TryToStandard(CurrentServer.ServiceLastUpdate);
            txtCoreLastUpate.Text = DTParser.TryToStandard(CurrentServer.CoreLastUpdate);
            txtLogEnabled.Text = CurrentServer.Configuration.Log.LogEnabled ? "是" : "否";
            txtLogProcess.Text = CurrentServer.Configuration.Log.LogProcess ? "是" : "否";
            txtLogUDS.Text = CurrentServer.Configuration.Log.LogUDS ? "是" : "否";
            txtLogOpt.Text = CurrentServer.Configuration.Log.Option.ToString();
            txtCompress.Text = CurrentServer.Configuration.Log.CompressData ? "是" : "否";
            txtLogDB.Text = CurrentServer.Configuration.Log.Target;
            txtDBMSVersion.Text = CurrentServer.DBMSVersionString;
            synMemo.Text = CurrentServer.RegistryData.Memo;
            btnSetTemplate.Text = "樣版資料庫：" + (string.IsNullOrWhiteSpace(CurrentServer.TemplateDatabase) ? "<未指定>" : CurrentServer.TemplateDatabase);
            txtTrafficFile.Text = string.Empty;
            dgvTraffics.Rows.Clear();
            txtSendSize.Text = "";
            txtReceiveSize.Text = "";
            txtClientWaitTime.Text = "";
            txtServerProcessTime.Text = "";
            txtDifferenceTime.Text = "";

            Version ver;
            if (Version.TryParse(CurrentServer.CoreVersion, out ver))
            {
                //btnChangeLogSetting.Enabled = (ver >= new Version(4, 1, 6, 0));
                btnSaveConfig.Enabled = (ver >= new Version(4, 1, 6, 0));
            }
            else
                btnChangeLogSetting.Enabled = false;

            txtSDUpdateUrl.Text = CurrentServer.Configuration.ServiceDefinitionUpdateUrl.Url;
            txtSDUserName.Text = CurrentServer.Configuration.ServiceDefinitionUpdateUrl.UserName;
            txtSDPassword.Text = CurrentServer.Configuration.ServiceDefinitionUpdateUrl.Password;

            txtSIUpdateUrl.Text = CurrentServer.Configuration.ComponentUpdateUrl.Url;
            txtSIUserName.Text = CurrentServer.Configuration.ComponentUpdateUrl.UserName;
            txtSIPassword.Text = CurrentServer.Configuration.ComponentUpdateUrl.Password;

            dgvLoadbalance.Rows.Clear();
            foreach (string each in CurrentServer.Configuration.LoadBalances)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgvLoadbalance, each);
                dgvLoadbalance.Rows.Add(row);
            }

            //dgvSchools.Rows.Clear();
            //foreach (Application each in CurrentServer.Applications)
            //{
            //    if (each.IsShared) continue;

            //    CreateSchoolListRow(each);
            //}

            foreach (DataGridViewRow each in dgvSchoolManageList.Rows)
            {
                Application app = each.Tag as Application;
                app.NameChanged -= new EventHandler(each_NameChanged);
                app.ConfigChanged -= new EventHandler(each_ConfigChanged);
            }

            dgvSchoolManageList.Rows.Clear();
            foreach (Application each in CurrentServer.Applications)
            {
                if (each.IsShared) continue;

                CreateSchoolManagerRow(each);

                each.NameChanged += new EventHandler(each_NameChanged);
                each.ConfigChanged += new EventHandler(each_ConfigChanged);
            }

            #region 需要非同步執行的動作
            if (PrepareTask == null)
            {
                PrepareTask = new XmlResultAsyncRunner();
                PrepareTask.Task = GetRunningTask();
                PrepareTask.Complete = GetRunningCompleteTask();
            }

            if (PrepareTask.IsRunning)
                ReadingTaskPadding = true;
            else
                PrepareTask.Run();
            #endregion
        }

        private void each_ConfigChanged(object sender, EventArgs e)
        {
            DataGridViewRow found = null;
            Application find = sender as Application;
            foreach (DataGridViewRow each in dgvSchoolManageList.Rows)
            {
                if (find == each.Tag)
                {
                    found = each;
                    break;
                }
            }

            if (found != null)
            {
                found.Cells[1].Value = find.DatabaseName;
                found.Cells[2].Value = find.Enabled;
                found.Cells[3].Value = find.Comment;
            }
        }

        private void each_NameChanged(object sender, EventArgs e)
        {
            DataGridViewRow found = null;
            Application find = sender as Application;
            foreach (DataGridViewRow each in dgvSchoolManageList.Rows)
            {
                if (find == each.Tag)
                {
                    found = each;
                    break;
                }
            }

            if (found != null)
                found.Cells[0].Value = find.Name;
        }

        private void CreateSchoolManagerRow(Application app)
        {
            DataGridViewRow approw = new DataGridViewRow();
            approw.CreateCells(dgvSchoolManageList, app.Name, app.DatabaseName, app.Enabled, app.Comment);

            Color color = Color.Black;
            //if (!app.Enabled)
            //    color = Color.Red;

            foreach (DataGridViewCell cell in approw.Cells)
                cell.Style.ForeColor = color;

            if (MainForm.MarkList.ContainsKey(app.Name))
            {
                foreach (DataGridViewCell cell in approw.Cells)
                {
                    cell.Style.ForeColor = MarkForecolor;
                    cell.Style.SelectionForeColor = MarkForecolor;
                }
            }

            approw.Tag = app;

            app.NameChanged += new EventHandler(each_NameChanged);
            app.ConfigChanged += new EventHandler(each_ConfigChanged);

            dgvSchoolManageList.Rows.Add(approw);
        }

        private Action<AsyncRunner<object, XmlElement>> GetRunningTask()
        {
            return arg =>
            {
                //Run Task
            };
        }

        private Action<AsyncRunner<object, XmlElement>> GetRunningCompleteTask()
        {
            return arg =>
            {
                if (ReadingTaskPadding)
                {
                    ReadingTaskPadding = false;
                    PrepareTask.Run();
                }
                else
                {
                    //Complete
                }
            };
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword chpwd = new ChangePassword();

            while (chpwd.ShowDialog() == DialogResult.OK)
            {
                if (CurrentServer.ChangeConnectionPassword(chpwd.OldPassword, chpwd.NewPassword))
                    break;

                MessageBox.Show("變更密碼失敗，請確認原密碼是否正確。");
            }
        }

        private void synMemo_TextChanged(object sender, EventArgs e)
        {
            CurrentServer.RegistryData.Memo = synMemo.Text;
        }

        #region 儲存 Log
        private void btnChangeLogSetting_Click(object sender, EventArgs e)
        {
            ChangeLogSetting log = new ChangeLogSetting(CurrentServer.Configuration);

            if (log.ShowDialog() == DialogResult.OK)
            {
                XmlResultAsyncRunner runner = new XmlResultAsyncRunner();
                runner.Message = "更新 DSA Server 組態中...";
                runner.MessageOwner = this;
                runner.Run(
                    arg =>
                    {
                        XmlElement result = CurrentServer.Manager.UpdateServerConfiguration(CurrentServer.Configuration.ToXml());
                        CurrentServer.Manager.ReloadServer();
                        CurrentServer.ReloadConfiguration();
                        arg.Result = result;
                    },
                    arg =>
                    {
                        if (arg.IsTaskError)
                        {
                            ErrorForm ef = new ErrorForm();
                            ef.Display("更新 DSA Server 組態錯誤。", arg.TaskError);
                        }
                        else if (arg.Result.SelectNodes("Result[@Status!='0']").Count > 0)
                        {
                            ErrorForm ef = new ErrorForm();
                            ef.Display("更新 DSA Server 組態錯誤，請檢查詳細資料。", FISCA.XHelper.Format(arg.Result.OuterXml));
                        }
                    });
            }
        }
        #endregion

        #region 儲存線上更新、負載平衡資訊。
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("您確定要儲存設定？", "Prototype", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                UrlData sd = new UrlData(txtSDUserName.Text, txtSDPassword.Text, txtSDUpdateUrl.Text);
                UrlData si = new UrlData(txtSIUserName.Text, txtSIPassword.Text, txtSIUpdateUrl.Text);

                HashSet<string> balances = new HashSet<string>();
                foreach (DataGridViewRow each in dgvLoadbalance.Rows)
                {
                    if (each.IsNewRow) continue;
                    balances.Add(each.Cells[0].Value + "");
                }

                XmlResultAsyncRunner runner = new XmlResultAsyncRunner();
                runner.Message = "更新 DSA Server 組態中...";
                runner.MessageOwner = this;
                runner.Run(
                    arg =>
                    {
                        CurrentServer.Configuration.ServiceDefinitionUpdateUrl = sd;
                        CurrentServer.Configuration.ComponentUpdateUrl = si;
                        CurrentServer.Configuration.LoadBalances = balances;

                        XmlElement result = CurrentServer.Manager.UpdateServerConfiguration(CurrentServer.Configuration.ToXml());
                        CurrentServer.Manager.ReloadServer();
                        CurrentServer.ReloadConfiguration();
                        arg.Result = result;
                    },
                    arg =>
                    {
                        if (arg.IsTaskError)
                        {
                            ErrorForm ef = new ErrorForm();
                            ef.Display("更新 DSA Server 組態錯誤。", arg.TaskError);
                        }
                        else if (arg.Result.SelectNodes("Result[@Status!='0']").Count > 0)
                        {
                            ErrorForm ef = new ErrorForm();
                            ef.Display("更新 DSA Server 組態錯誤，請檢查詳細資料。", FISCA.XHelper.Format(arg.Result.OuterXml));
                        }
                    });
            }
        }
        #endregion

        private void labelX18_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void txtFilterPattern_TextChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow each in dgvSchoolManageList.Rows)
                    each.Visible = true;

                if (string.IsNullOrWhiteSpace(txtFilterPattern.Text)) return;

                Regex rx = new Regex(txtFilterPattern.Text);

                foreach (DataGridViewRow eachRow in dgvSchoolManageList.Rows)
                {
                    bool match = false;
                    foreach (DataGridViewCell eachCell in eachRow.Cells)
                    {
                        string source = eachCell.Value + "";

                        if (rx.Match(source).Success)
                        {
                            match = true;
                            break;
                        }
                    }
                    eachRow.Visible = match;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("本功能支援 Regular Expression 必須使用正確的語法。\n\n" + ex.Message);
            }
        }

        /// <summary>
        /// 新增學校。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddApp_Click(object sender, EventArgs e)
        {
            InputBox input = new InputBox("新 Application 名稱");
            input.Confirming += delegate(object o, CancelEventArgs arg)
            {
                arg.Cancel = true;

                if (string.IsNullOrWhiteSpace(input.InputString))
                {
                    MessageBox.Show("識別必須輸入資料。");
                    return;
                }

                if (CurrentServer.ContainsApplication(input.InputString))
                {
                    MessageBox.Show("識別名稱重覆。");
                    return;
                }

                if (input.InputString.Contains(":"))
                {
                    string dbName = input.InputString.Split(':')[1];
                    if (string.IsNullOrWhiteSpace(CurrentServer.TemplateDatabase))
                    {
                        MessageBox.Show("未指定樣版資料庫，無法建立新資料庫。");
                        return;
                    }

                    if (CurrentServer.Manager.DatabaseExists(dbName))
                    {
                        MessageBox.Show("您指定的新資料庫名稱已存在。");
                        return;
                    }
                }

                arg.Cancel = false;
            };

            if (input.ShowDialog() == DialogResult.OK)
            {
                string newString = input.InputString;
                AsyncRunner runner = new AsyncRunner();
                runner.Message = "建立新 Application 中...";
                runner.MessageOwner = this;

                runner.Run(x =>
                {
                    if (newString.Contains(":"))
                    {
                        string appName = newString.Split(':')[0];
                        string dbName = newString.Split(':')[1];
                        CurrentServer.Manager.TerminalDBConnection(CurrentServer.TemplateDatabase);
                        CurrentServer.Manager.CreateNewDatabase(dbName, CurrentServer.TemplateDatabase);
                        CurrentServer.AddApplication(appName.ToLower());
                        Application app = CurrentServer.GetSharedApplication();
                        Application.Argument arg = app.GetArgument();
                        //arg.SetDatabaseFullName(dbName); //這部份有調整。
                        arg.Name = appName;
                        CurrentServer.SetApplicationArgument(arg);
                    }
                    else
                        CurrentServer.AddApplication(input.InputString.ToLower());

                    SyncServerStatus();
                }, x =>
                {
                    if (x.IsTaskError)
                    {
                        string sqlmsg;
                        string msg;

                        if (ErrorParser.TryGetSqlException(x.TaskError, out sqlmsg))
                            msg = ("建立資料庫錯誤，您可能沒有權限建立資料庫\n\n" + sqlmsg);
                        else
                            msg = "建立新 Application 發生錯誤！";

                        ErrorForm err = new ErrorForm();
                        err.Display(msg, x.TaskError);

                    }
                });
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSchoolManageList.SelectedRows.Count < 0)
                return;

            List<Application> apps = new List<Application>();
            foreach (DataGridViewRow row in dgvSchoolManageList.SelectedRows)
                apps.Add(row.Tag as Application);

            if (MessageBox.Show(string.Format("您確定刪除選擇的 Application?\n一但刪除之後將無法復原\n\n註：資料庫請手動刪除。"), "prototype", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int count = 0;
                foreach (Application app in apps)
                {
                    count++;
                    MainForm.SetBarMessage(string.Format("移除：{0}/{1}", count, apps.Count));
                    CurrentServer.RemoveApplication(app.Name);
                }

                SyncServerStatus();
            }
        }

        private void txtFilterPattern_ButtonCustomClick(object sender, EventArgs e)
        {
            txtFilterPattern.Text = string.Empty;
        }

        private void Context_PopupOpen(object sender, DevComponents.DotNetBar.PopupOpenEventArgs e)
        {
            if (dgvSchoolManageList.SelectedRows.Count <= 0)
            {
                e.Cancel = true;
                return;
            }

            // Context Menu 開啟時。
            mcChangeArgument.Enabled = (dgvSchoolManageList.SelectedRows.Count == 1);
            mcChangeEnabledStatus.Visible = (dgvSchoolManageList.SelectedRows.Count > 0);
            mcDelete.Enabled = (dgvSchoolManageList.SelectedRows.Count > 0);
            mcRename.Enabled = (dgvSchoolManageList.SelectedRows.Count == 1);
            mcResetDBPermission.Enabled = (dgvSchoolManageList.SelectedRows.Count > 0);
            mcTestDSNS.Enabled = (dgvSchoolManageList.SelectedRows.Count > 0);

            mcResetSecureCode.Visible = false;
            mc1RunSingleSql.Visible = false;
            btnDebugDB.Visible = false;
            btnRiskFeature.Visible = false;
            if (Control.ModifierKeys == Keys.Shift)
            {
                btnRiskFeature.Visible = Program.IsAdministrator;
                mcResetSecureCode.Visible = (dgvSchoolManageList.SelectedRows.Count == 1) && Program.IsAdministrator;
                mc1RunSingleSql.Visible = (dgvSchoolManageList.SelectedRows.Count > 0) && Program.IsAdministrator;
                btnDebugDB.Visible = (dgvSchoolManageList.SelectedRows.Count == 1) && Program.IsAdministrator;
            }

            DataGridViewRow row = dgvSchoolManageList.SelectedRows[0];
            Application app = row.Tag as Application;

            //if (app.Enabled)
            //    mcChangeEnabledStatus.Text = "變更為「停用」";
            //else
            //    mcChangeEnabledStatus.Text = "變更為「啟用」";
        }

        private void btnChangeArgument_Click(object sender, EventArgs e)
        {
            if (dgvSchoolManageList.SelectedRows.Count > 0)
            {
                Application app = dgvSchoolManageList.SelectedRows[0].Tag as Application;
                if (new SchoolArgumentForm(app).ShowDialog() == DialogResult.OK)
                    SyncServerStatus();
            }
        }

        private void btnChangeEnableStatus_Click(object sender, EventArgs e)
        {
            if (HasSelectedItem)
            {
                string msg = string.Format("您確定要啟用/停用選擇項目的狀態？", SelectedApp.Name);

                if (MessageBox.Show(msg, "Prototype", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    List<Application> apps = new List<Application>();
                    foreach (DataGridViewRow row in dgvSchoolManageList.SelectedRows)
                        apps.Add(row.Tag as Application);

                    AsyncRunner runner = new AsyncRunner();
                    runner.Message = "準備中...";
                    runner.MessageOwner = this;
                    runner.Arguments = apps;
                    runner.Run((x) =>
                    {
                        List<Application> applist = x.Arguments as List<Application>;
                        foreach (Application app in applist)
                        {
                            app.Owner.SetApplicationEnable(app.Name, !app.Enabled);
                            x.Message = app.Name;
                        }
                    }, (x) =>
                    {
                        SyncServerStatus();
                    });
                }
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (dgvSchoolManageList.SelectedRows.Count > 0)
            {
                Application app = dgvSchoolManageList.SelectedRows[0].Tag as Application;

                InputBox input = new InputBox("重新命名", app.Name);
                input.Confirming += delegate(object o, CancelEventArgs arg)
                {
                    arg.Cancel = true;

                    if (string.Equals(input.InputString, app.Name)) return;

                    if (app.Owner.ContainsApplication(input.InputString))
                    {
                        MessageBox.Show("名稱重覆。");
                        return;
                    }

                    arg.Cancel = false;
                };

                if (input.ShowDialog() == DialogResult.OK)
                {
                    app.Owner.RenameApplication(app.Name, input.InputString.ToLower());
                    SyncServerStatus();
                }
            }
        }

        private void SyncServerStatus()
        {
            if (InvokeRequired)
                Invoke(new Action(() => SyncServerStatusInternal()));
            else
                SyncServerStatusInternal();
        }

        private void SyncServerStatusInternal()
        {
            AsyncRunner<object, XmlElement> runner = new AsyncRunner<object, XmlElement>();
            runner.Message = "同步主機資料中...";
            runner.MessageOwner = this;
            runner.Run(
                arg =>
                {
                    runner.Result = CurrentServer.Manager.ReloadServer();
                },
                arg =>
                {
                    if (runner.Result.SelectNodes("Result[@Status!='0']").Count > 0)
                    {
                        MessageBox.Show("同步主機資料錯誤：\n\n" + runner.Result.OuterXml);
                    }
                });
        }

        private void TestDBConnection()
        {
            if (dgvSchoolManageList.SelectedRows.Count > 0)
            {
                Dictionary<string, XmlElement> testResults = new Dictionary<string, XmlElement>();
                List<Application> prepareApps = new List<Application>();

                foreach (DataGridViewRow row in dgvSchoolManageList.SelectedRows)
                    prepareApps.Add(row.Tag as Application);

                AsyncRunner runner = new AsyncRunner();
                runner.Message = "測試連線中...";
                runner.MessageOwner = this;
                runner.Run(
                    x =>
                    {
                        Parallel.ForEach(prepareApps, app =>
                        {
                            XmlElement result = CurrentServer.Manager.TestConnection(app.Name).SelectSingleNode("Application") as XmlElement;
                            testResults.Add(app.Name, result);
                        });
                    },
                    x =>
                    {
                        if (x.IsTaskError)
                        {
                            ErrorForm err = new ErrorForm();
                            err.Display("測試連線發生錯誤!", x.TaskError);
                            return;
                        }
                        else
                        {
                            bool allSuccess = true;
                            foreach (DataGridViewRow row in dgvSchoolManageList.SelectedRows)
                            {
                                Application app = row.Tag as Application;

                                if (!app.Enabled) continue;

                                XmlElement result = testResults[app.Name];

                                bool success = result.SelectSingleNode("Property[@Name='Status']").InnerText == "0";

                                if (!success)
                                {
                                    row.Cells["chDBName"].Tag = result.OuterXml;
                                    row.Cells["chDBName"].ErrorText = "測試發現錯誤，請 DoubleClick 檢視詳細資料。";
                                }
                                else
                                {
                                    row.Cells["chDBName"].Tag = null;
                                    row.Cells["chDBName"].ErrorText = string.Empty;
                                }

                                allSuccess &= (success);
                            }

                            if (allSuccess)
                                MessageBox.Show("測試完成，沒有發現任何錯誤。");
                            else
                                MessageBox.Show("測試資料庫連線錯誤，請檢查詳細資訊!");

                            //bool appComplete = testresult.SelectSingleNode("Property[@Name='Status']").InnerText == "0";
                            //bool adminComplete = testresult.SelectSingleNode("Contract[@Name='admin']/Property[@Name='Status']").InnerText == "0";
                        }
                    });
            }
        }

        private void btnSetupDefaultSchool_Click(object sender, EventArgs e)
        {
            Application shared = CurrentServer.GetSharedApplication();

            if (shared == null) return;

            if (new SchoolArgumentForm(shared).ShowDialog() == DialogResult.OK)
                SyncServerStatus();
        }

        private void btnSetTemplate_Click(object sender, EventArgs e)
        {
            InputBox input = new InputBox("設定新 Application 樣版資料庫", CurrentServer.TemplateDatabase);
            input.Confirming += delegate(object o, CancelEventArgs arg)
            {
                if (!CurrentServer.Manager.DatabaseExists(input.InputString))
                {
                    MessageBox.Show("您輸入的資料庫並不存在，請確認資料庫名稱。");
                    arg.Cancel = true;
                }
            };

            if (input.ShowDialog() == DialogResult.OK)
            {
                CurrentServer.TemplateDatabase = input.InputString;
                btnSetTemplate.Text = "樣版資料庫：" + input.InputString;
            }
        }

        private void btnResetDBPermission_Click(object sender, EventArgs e)
        {
            if (HasSelectedItem)
            {
                List<string> dbList = new List<string>();
                foreach (DataGridViewRow row in dgvSchoolManageList.SelectedRows)
                    dbList.Add((row.Tag as Application).DatabaseFullName);

                new DBPermissionReset(CurrentServer, SelectedApp.DatabaseFullName, dbList).ShowDialog();
            }
        }

        private void txtTrafficFile_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void txtTrafficFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] obj = e.Data.GetData("FileNameW") as string[];

            if (obj.Length > 0)
            {
                txtTrafficFile.Text = obj[0];
            }
        }

        #region Traffic Diagnostic
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(txtTrafficFile.Text))
            //{
            //    MessageBox.Show("請指定正確的檔案名稱。");
            //    return;
            //}

            //try
            //{
            //    FISCA.XHelper traffics = new FISCA.XHelper(FISCA.XHelper.ParseAsDOM(new FileInfo(txtTrafficFile.Text)));
            //    dgvTraffics.Rows.Clear();
            //    foreach (XmlElement each in traffics.GetElements("Traffic"))
            //    {
            //        if (each.SelectSingleNode("ExceptionOccur") != null)
            //        {
            //            string t1 = each.GetAttribute("Timestamp");
            //            string s1 = each.GetAttribute("Service");
            //            DataGridViewRow r1 = new DataGridViewRow();
            //            r1.CreateCells(dgvTraffics, t1, s1, "Error", "Error", "Error", "Error", "Error");
            //            r1.Tag = each;
            //            dgvTraffics.Rows.Add(r1);
            //            continue;
            //        }

            //        TrafficData traffic = new TrafficData(each);

            //        FISCA.XHelper hlpeach = new FISCA.XHelper(each);
            //        string timestamp = traffic.Timestamp.ToString("yyyy/MM/dd HH:mm:ss");
            //        string service = traffic.Service;
            //        string reqSize = FormatToNumber(traffic.RequestSize);
            //        string rspSize = FormatToNumber(traffic.ResponseSize);
            //        string clientwait = FormatToNumber(traffic.ClientWaitTime);

            //        DataGridViewRow row = new DataGridViewRow();
            //        row.CreateCells(dgvTraffics, timestamp, service, reqSize, rspSize, clientwait, "", "");
            //        row.Tag = traffic;
            //        dgvTraffics.Rows.Add(row);
            //    }

            //    Dictionary<string, DataGridViewRow> detailRequiredRows = new Dictionary<string, DataGridViewRow>();
            //    foreach (DataGridViewRow eachrow in dgvTraffics.Rows)
            //    {
            //        //代表有問題的 Row。
            //        if (eachrow.Tag is XmlElement) continue;

            //        TrafficData td = eachrow.Tag as TrafficData;

            //        detailRequiredRows.Add(td.LogGuid, eachrow);
            //    }

            //    AsyncRunner runner = new AsyncRunner();
            //    runner.Run(
            //        x =>
            //        {
            //            List<string> logids = new List<string>(detailRequiredRows.Keys);
            //            XmlElement log = CurrentServer.Manager.GetLogs(logids);

            //            foreach (XmlElement eachLog in log.SelectNodes("Log"))
            //            {
            //                string logId = eachLog.GetAttribute("LogID");

            //                if (detailRequiredRows.ContainsKey(logId))
            //                {
            //                    TrafficData td = detailRequiredRows[logId].Tag as TrafficData;
            //                    td.RawSystemLog = eachLog;
            //                    td.ServerProcessTime = long.Parse(eachLog.SelectSingleNode("Property[@Name='ProcessTime']").InnerText);
            //                }
            //            }
            //        },
            //        x =>
            //        {
            //            if (x.IsTaskError)
            //            {
            //                ErrorForm err = new ErrorForm();
            //                err.Display(x.TaskError.Message, x.TaskError);
            //            }
            //            else
            //            {
            //                long intSend = 0, intReceive = 0, intClient = 0, intServer = 0, intDifference = 0;

            //                foreach (DataGridViewRow eachRow in detailRequiredRows.Values)
            //                {
            //                    TrafficData td = eachRow.Tag as TrafficData;
            //                    eachRow.Cells["chServerProcessTime"].Value = FormatToNumber(td.ServerProcessTime);
            //                    if (td.ServerProcessTime <= 0)
            //                        eachRow.Cells["chServerProcessTime"].ErrorText = "有問題!!!";

            //                    eachRow.Cells["chDifferenceTime"].Value = FormatToNumber(td.TimeDiffence);

            //                    intSend += td.RequestSize;
            //                    intReceive += td.ResponseSize;
            //                    intClient += td.ClientWaitTime;
            //                    intServer += td.ServerProcessTime;
            //                    intDifference += td.TimeDiffence;
            //                }
            //                txtSendSize.Text = FormatToNumber(intSend);
            //                txtReceiveSize.Text = FormatToNumber(intReceive);
            //                txtClientWaitTime.Text = FormatToNumber(intClient);
            //                txtServerProcessTime.Text = FormatToNumber(intServer);
            //                txtDifferenceTime.Text = FormatToNumber(intDifference);
            //            }
            //        });
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private static string FormatToNumber(long number)
        {
            return long.Parse(number.ToString()).ToString("n0");
        }

        internal class TrafficData
        {
            public TrafficData(XmlElement trafficData)
            {
                try
                {
                    FISCA.XHelper hlpeach = new FISCA.XHelper(trafficData);
                    RawTraffic = trafficData;
                    Timestamp = DateTime.Parse(hlpeach.GetText("@Timestamp"));
                    Service = hlpeach.GetText("@Service");
                    LogGuid = hlpeach.GetText("Guid");
                    RequestSize = long.Parse(hlpeach.GetText("RequestSize"));
                    ResponseSize = long.Parse(hlpeach.GetText("ResponseSize"));
                    ClientWaitTime = long.Parse(hlpeach.GetText("SpenTime"));
                }
                catch (Exception)
                {
                }
            }

            public DateTime Timestamp { get; set; }

            public string Service { get; set; }

            public long RequestSize { get; set; }

            public long ResponseSize { get; set; }

            public long ClientWaitTime { get; set; }

            public long ServerProcessTime { get; set; }

            public long TimeDiffence { get { return ClientWaitTime - ServerProcessTime; } }

            public string LogGuid { get; set; }

            public XmlElement RawTraffic { get; private set; }

            public XmlElement RawSystemLog { get; set; }
        }

        private void dgvTraffics_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTraffics.Rows[e.RowIndex];
                TrafficData td = row.Tag as TrafficData;

                if (td != null)
                    new TrafficDetail(td, CurrentServer).ShowDialog();
                else
                    new TrafficDetail(row.Tag as XmlElement, CurrentServer).ShowDialog();
            }
        }

        private void dgvTraffics_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            List<int> sorts = new List<int>(new int[] { chSendSize.Index, 
                chReceiveSize.Index, 
                chClientWait.Index, 
                chServerProcessTime.Index,
                chDifferenceTime.Index });

            if (sorts.Contains(e.Column.Index))
            {
                TrafficData X, Y;
                long x = 0, y = 0;

                X = dgvTraffics.Rows[e.RowIndex1].Tag as TrafficData;
                Y = dgvTraffics.Rows[e.RowIndex2].Tag as TrafficData;

                if (X == null)
                    X = new TrafficData(FISCA.XHelper.ParseAsDOM("<Root/>"));
                if (Y == null)
                    Y = new TrafficData(FISCA.XHelper.ParseAsDOM("<Root/>"));

                if (e.Column.Index == chSendSize.Index)
                {
                    x = X.RequestSize;
                    y = Y.RequestSize;
                }
                else if (e.Column.Index == chReceiveSize.Index)
                {
                    x = X.ResponseSize;
                    y = Y.ResponseSize;
                }
                else if (e.Column.Index == chClientWait.Index)
                {
                    x = X.ClientWaitTime;
                    y = Y.ClientWaitTime;
                }
                else if (e.Column.Index == chServerProcessTime.Index)
                {
                    x = X.ServerProcessTime;
                    y = Y.ServerProcessTime;
                }
                else if (e.Column.Index == chDifferenceTime.Index)
                {
                    x = X.TimeDiffence;
                    y = Y.TimeDiffence;
                }

                e.SortResult = x.CompareTo(y);
                e.Handled = true;
            }
        }
        #endregion

        private void btnResetSecureCode_Click(object sender, EventArgs e)
        {
            if (HasSelectedItem)
            {
                InputBox input = new InputBox("重設學校安全代碼", Guid.NewGuid().ToString());
                input.Confirming += delegate(object s, CancelEventArgs arg)
                {
                    string msg = string.Format("您確定要重新設定「{0}」的授權安全碼？一但重新指定後，所有的「授權檔」將全部失效，必須要重新產生。", SelectedApp.Name);

                    arg.Cancel = MessageBox.Show(msg, "Prototype", MessageBoxButtons.YesNo) == DialogResult.No;
                };
                if (input.ShowDialog() == DialogResult.OK)
                {
                    CurrentServer.Manager.ResetLicenseCode(SelectedApp.DatabaseName, input.InputString);
                }
            }
            else
                MessageBox.Show("請選擇 Application.");
        }

        private void btnListDatabase_Click(object sender, EventArgs e)
        {
            new DatabaseListForm(CurrentServer).Show();
        }

        private void btnViewLog_Click(object sender, EventArgs e)
        {
            //new LogViewer(CurrentServer).ShowDialog();
        }

        private void btnTestDSNS_Click(object sender, EventArgs e)
        {
            List<Application> apps = new List<Application>();
            foreach (DataGridViewRow each in dgvSchoolManageList.SelectedRows)
            {
                apps.Add(each.Tag as Application);
            }

            new DSNameResolverBatch(apps).ShowDialog();
        }

        private void btnSetDSNS_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrorForm err = new ErrorForm();
                err.Display("Bomb!!!", ex);
            }
        }

        private void btnRunSingleSql_Click(object sender, EventArgs e)
        {
            //update modules set module_url=replace(module_url,'beta.smartschool.com.tw','dsa1.ischool.com.tw')

            InputLinesBox input = new InputLinesBox("SQL Command");
            if (input.ShowDialog() != DialogResult.OK)
                return;

            ServerManager manager = CurrentServer.Manager;
            string cmd = input.InputString;
            List<string> faillist = new List<string>();

            List<Application> apps = new List<Application>();
            foreach (DataGridViewRow each in dgvSchoolManageList.SelectedRows)
                apps.Add(each.Tag as Application);

            Parallel.ForEach<Application>(apps, each =>
            {
                try
                {
                    manager.ExecuteUpdateCommand(cmd, each.DatabaseName);
                }
                catch (Exception ex)
                {
                    faillist.Add(each.Name + "\n" + ErrorReport.Generate(ex) + "\n\n\n");
                }
            });

            if (faillist.Count > 0)
            {
                try
                {
                    string path = Path.Combine(System.Windows.Forms.Application.StartupPath, "db_upgrade_errors.xml");
                    File.WriteAllText(path, string.Join("\n", faillist.ToArray()));
                    string msg = string.Format("有部份學校升級失敗，錯誤資訊已寫入到「" + path + "」");
                    MessageBox.Show(msg);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("升級完成。");
        }

        private void btnDebugDB_Click(object sender, EventArgs e)
        {
            new DBDebugger(new DatabaseManager(CurrentServer.Manager, SelectedApp.DatabaseName)).ShowDialog();
        }

        private void btnDBWindow_Click(object sender, EventArgs e)
        {
            new DBDebugger(new DatabaseManager(CurrentServer.Manager, string.Empty)).ShowDialog();
        }

        private void dgvSchoolManageList_SelectionChanged(object sender, EventArgs e)
        {
            lblSelectionCount.Text = dgvSchoolManageList.SelectedRows.Count.ToString();
        }

        //沒用到了。
        private void btnExportMark_Click(object sender, EventArgs e)
        {
            Workbook book = new Workbook();
            book.Worksheets.Clear();
            Worksheet sheet = book.Worksheets[book.Worksheets.Add()];

            sheet.Cells[0, 0].PutValue("accesspoint");
            sheet.Cells[0, 1].PutValue("memo");

            int rowIndex = 1;
            foreach (DataGridViewRow row in dgvSchoolManageList.Rows)
            {
                if (row.Cells[0].Style.ForeColor == MarkForecolor)
                {
                    Application app = (row.Tag as Application);

                    if (!app.Enabled) continue;

                    sheet.Cells[rowIndex, 0].PutValue(app.Name);
                    sheet.Cells[rowIndex, 1].PutValue(app.Comment);
                    rowIndex++;
                }
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel 檔案(*.xls)|*.xls";
            dialog.FileName = "匯出標記.xls";

            if (dialog.ShowDialog() == DialogResult.OK)
                book.Save(dialog.FileName);
        }

        private void btnSpecUpgrade_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = ".Net Assembly(*.dll)|*.dll";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Assembly asm = Assembly.LoadFrom(dialog.FileName);

                    List<IAppUpgrader> items = new List<IAppUpgrader>();
                    foreach (Type type in asm.GetExportedTypes())
                    {
                        if (IsTypeMatch(type))
                        {
                            IAppUpgrader upgrader = Activator.CreateInstance(type) as IAppUpgrader;
                            items.Add(upgrader);
                        }
                    }

                    UpgraderSelector selector = new UpgraderSelector(items.ToArray());
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        string msg = string.Format("您確定要執行「{0}」升級？", selector.Selected.Description);

                        if (MessageBox.Show(msg, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            DoUpgrade(selector.Selected);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 執行升級。
        /// </summary>
        /// <param name="upgrader"></param>
        private void DoUpgrade(IAppUpgrader upgrader)
        {
            List<Application> apps = new List<Application>();
            foreach (DataGridViewRow each in dgvSchoolManageList.SelectedRows)
                apps.Add(each.Tag as Application);

            string accountName = Guid.NewGuid().ToString();
            string insertAccount = string.Format("insert into _login(login_name,password,sys_admin) " +
                                                                                    "values('{0}','cRDtpNCeBiql5KOQsKVyrA0sAiA=','1')", accountName);

            string deleteAccount = string.Format("delete from _login where login_name='{0}'", accountName);

            Dictionary<string, Exception> upgrade_list = new Dictionary<string, Exception>();
            foreach (Application app in apps)
            {
                ServerManager sm = app.Owner.Manager;
                string baseUrl = app.Owner.AccessPointUrl.Replace("manager", string.Empty);

                try
                {
                    Dictionary<string, string> args = new Dictionary<string, string>();

                    sm.ExecuteUpdateCommand(insertAccount, app.DatabaseName, app.DDLUserName, app.DDLPassword);

                    DSAServices.Connect(baseUrl + app.Name, accountName, "1234");

                    if (MainForm.MarkList.ContainsKey(app.Name))
                        args = MainForm.MarkList[app.Name];

                    MainForm.SetBarMessage("正在升級：" + app.Name);

                    //DSAServices.DefaultDataSource

                    ExtraSecurityToken clone = new ExtraSecurityToken(DSAServices.DefaultDataSource.SecurityToken);
                    CloneSecurityToken token = new CloneSecurityToken(clone.TokenType, clone.XmlString);
                    Connection targetDSA = new Connection();
                    targetDSA.Connect(DSAServices.AccessPoint, "admin", token);

                    upgrader.DoUpgrade(targetDSA, new SqlCommand(sm, app.DatabaseName), args);
                    upgrade_list.Add(app.Name, null);
                }
                catch (Exception ex)
                {
                    upgrade_list.Add(app.Name, ex);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    try
                    {
                        sm.ExecuteUpdateCommand(deleteAccount, app.DatabaseName, app.DDLUserName, app.DDLPassword);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(string.Format("執行下列指令錯誤：{0}", deleteAccount));
                    }
                }
            }

            new UpgradeResultForm(upgrade_list).ShowDialog();
            MainForm.SetBarMessage("升級完成。");
        }

        private bool IsTypeMatch(Type type)
        {
            foreach (Type each in type.GetInterfaces())
                if (each == typeof(IAppUpgrader))
                    return true;

            return false;
        }

        private class SqlCommand : ISqlCommand
        {
            private ServerManager Manager { get; set; }

            private string DBName { get; set; }

            public SqlCommand(ServerManager manager, string dbName)
            {
                Manager = manager;
                DBName = dbName;
            }

            #region ISqlCommand 成員

            public XElement ExecuteQuery(string cmd)
            {
                return XElement.Parse(Manager.ExecuteQueryCommand(cmd, DBName).OuterXml);
            }

            public void ExecuteUpdate(List<string> cmds)
            {
                Manager.ExecuteUpdateCommands(cmds, DBName);
            }

            #endregion
        }

        private void btnRawEdit_Click(object sender, EventArgs e)
        {
            XmlEditor editor = new XmlEditor(CurrentServer.Configuration.GetRawXmlData().OuterXml);
            editor.Confirming += delegate(object sender1, CancelEventArgs e1)
            {
                try
                {
                    XElement.Parse(editor.XmlData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    e1.Cancel = true;
                }
            };

            if (editor.ShowDialog() == DialogResult.OK)
            {
                XmlElement result = CurrentServer.Manager.UpdateServerConfiguration(XHelper.ParseAsDOM(editor.XmlData));
                MessageBox.Show(string.Format("儲存結果：\n\n{0}", XHelper.Format(result.OuterXml)));
                CurrentServer.ReloadConfiguration();
                SetServerObject(CurrentServer);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            new ServerManagePanel_ImportApplication(dgvSchoolManageList, CurrentServer).Import();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvSchoolManageList.SelectedRows.Count > 0)
                new ServerManagePanel_ExportApplication(dgvSchoolManageList).ExportSelected();
        }

        private void dgvSchoolManageList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dgvSchoolManageList.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Tag != null)
            {
                XmlViewer.View(cell.Tag + "");
            }
        }

        private void btnTestConn_Click_1(object sender, EventArgs e)
        {
            TestDBConnection();
        }
    }
}
