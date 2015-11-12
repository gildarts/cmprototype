using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Aspose.Cells;
using FISCA.DSAClient;

namespace Manager
{
    internal class ServerManagePanel_ImportApplication
    {
        private DataGridView Grid { get; set; }

        private Server Server { get; set; }

        public ServerManagePanel_ImportApplication(DataGridView grid, Server server)
        {
            Grid = grid;
            Server = server;
        }

        public void Import()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*.xlsx|*.xlsx";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Workbook book = new Workbook(dialog.FileName);
                    Worksheet sheet = book.Worksheets[0];

                    Dictionary<string, int> column = new Dictionary<string, int>();
                    for (byte i = 0; i <= sheet.Cells.MaxDataColumn; i++)
                        column.Add(sheet.Cells[0, i].StringValue, i);

                    List<DSAppInfo> import_list = new List<DSAppInfo>();
                    HashSet<string> importApps = new HashSet<string>();
                    for (int i = 1; i <= sheet.Cells.MaxDataRow; i++)
                    {
                        DSAppInfo appinfo = new DSAppInfo();
                        appinfo.Name = sheet.Cells[i, column["name"]].StringValue;
                        appinfo.TrySetEnabled(sheet.Cells[i, column["enabled"]].StringValue);

                        foreach (string each in column.Keys)
                        {
                            //name、enabled 不能加入參數之中。
                            if (each.ToUpper() == "name".ToUpper() || each.ToUpper() == "enabled".ToUpper())
                                continue;

                            appinfo.Arguments.Add(each, sheet.Cells[i, column[each]].StringValue);
                        }

                        //appinfo.DBUrl = sheet.Cells[i, column["db_url"]].StringValue;
                        //appinfo.DMLUser = sheet.Cells[i, column["db_user"]].StringValue;
                        //appinfo.DMLPassword = sheet.Cells[i, column["db_pwd"]].StringValue;
                        //appinfo.DDLUser = sheet.Cells[i, column["db_udt_user"]].StringValue;
                        //appinfo.DDLPassword = sheet.Cells[i, column["db_udt_pwd"]].StringValue;
                        //appinfo.SchoolCode = sheet.Cells[i, column["school_code"]].StringValue;
                        //appinfo.Comment = sheet.Cells[i, column["app_comment"]].StringValue;

                        import_list.Add(appinfo);

                        if (importApps.Contains(appinfo.Name))
                        {
                            MessageBox.Show(string.Format("資料中包含了重複的名稱({0})。", appinfo.Name));
                            return;
                        }

                        importApps.Add(appinfo.Name);
                    }

                    Server.ReloadApplications();
                    HashSet<string> curApps = new HashSet<string>(
                        from app in Server.Applications select app.Name);

                    HashSet<string> newApps = new HashSet<string>(importApps);
                    newApps.ExceptWith(curApps);

                    //建立不存在的 Application。
                    int createCount = 0;
                    foreach (string name in newApps)
                    {
                        createCount++;

                        MainForm.SetBarMessage(string.Format("建立新 Application：{0}/{1}", createCount, newApps.Count));

                        Server.Manager.CloneApplication(name);
                    }

                    //Parallel.ForEach(newApps, name =>
                    //{
                    //    Server.Manager.CloneApplication(name);
                    //});

                    //先將 Application 的 Enabled 屬性設定好。
                    int setAttCount = 0;
                    foreach (DSAppInfo app in import_list)
                    {
                        setAttCount++;

                        MainForm.SetBarMessage(string.Format("設定 Application Enabled 屬性：{0}/{1}", setAttCount, import_list.Count));
                        Server.Manager.SetApplicationEnable(app.Name, app.Enabled);
                    }

                    //Parallel.ForEach(import_list, app =>
                    //{
                    //    Server.Manager.SetApplicationEnable(app.Name, app.Enabled);
                    //});

                    int setParamsCount = 0;

                    XmlHelper req = new XmlHelper();
                    foreach (DSAppInfo app in import_list)
                    {
                        setParamsCount++;

                        XmlHelper appreq = new XmlHelper(req.AddElement("Application"));
                        appreq.SetAttribute(".", "Name", app.Name);

                        foreach (KeyValuePair<string, string> p in app.Arguments)
                            appreq.AddElement(".", "Param", p.Value).SetAttribute("Name", p.Key);

                        //appreq.AddElement(".", "Param", app.DBUrl).SetAttribute("Name", "db_url");
                        //appreq.AddElement(".", "Param", app.DMLUser).SetAttribute("Name", "db_user");
                        //appreq.AddElement(".", "Param", app.DMLPassword).SetAttribute("Name", "db_pwd");
                        //appreq.AddElement(".", "Param", app.DDLUser).SetAttribute("Name", "db_udt_user");
                        //appreq.AddElement(".", "Param", app.DDLPassword).SetAttribute("Name", "db_udt_pwd");
                        //appreq.AddElement(".", "Param", app.SchoolCode).SetAttribute("Name", "school_code");
                        //appreq.AddElement(".", "Param", app.Comment).SetAttribute("Name", "app_comment");

                        if ((setParamsCount % 10) == 0)
                        {
                            MainForm.SetBarMessage(string.Format("設定 Application Params ：{0}/{1}", setParamsCount, import_list.Count));
                            Server.Manager.SetApplicationsArgument(req.GetElement("."));
                            req = new XmlHelper();
                        }
                    }

                    if ((setParamsCount % 10) != 0)
                    {
                        MainForm.SetBarMessage(string.Format("設定 Application Params ：{0}/{1}", setParamsCount, import_list.Count));
                        Server.Manager.SetApplicationsArgument(req.GetElement("."));
                        req = new XmlHelper();
                    }


                    MainForm.SetBarMessage("Reload Server...");
                    XmlElement result = Server.Manager.ReloadServer();

                    XmlNodeList nodes = result.SelectNodes("Result[@Status!='0']");
                    if (nodes.Count > 0)
                        MessageBox.Show(XmlHelper.Format(result.OuterXml));

                    MainForm.SetBarMessage("Download new configuration...");
                    Server.ReloadApplications();
                    Server.ReloadConfiguration();
                    ServerManagePanel.Instance.SetServerObject(Server);

                    MessageBox.Show("匯入完成!");

                    MainForm.SetBarMessage("Complete!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private class DSAppInfo
        {
            public DSAppInfo()
            {
                Arguments = new Dictionary<string, string>();
            }

            public string Name { get; set; }

            public Dictionary<string, string> Arguments { get; set; }

            public string DBUrl
            {
                get
                {
                    return Arguments["db_url"];
                }
            }

            //public string DMLUser { get; set; }

            //public string DMLPassword { get; set; }

            //public string DDLUser { get; set; }

            //public string DDLPassword { get; set; }

            //public string SchoolCode { get; set; }

            //public string Comment { get; set; }

            public bool Enabled { get; set; }

            public bool TrySetEnabled(string val)
            {
                bool result = false;

                if (bool.TryParse(val, out result))
                {
                    Enabled = result;
                    return true;
                }
                else
                {
                    Enabled = false;
                    return false;
                }
            }

            public override string ToString()
            {
                return string.Format("{0} ({1})", Name, DBUrl);
            }
        }
    }
}
