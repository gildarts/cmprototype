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
            dialog.Filter = "*.xls|*.xls";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Workbook book = new Workbook();
                    book.Open(dialog.FileName);
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
                        appinfo.DBUrl = sheet.Cells[i, column["db_url"]].StringValue;
                        appinfo.DMLUser = sheet.Cells[i, column["db_user"]].StringValue;
                        appinfo.DMLPassword = sheet.Cells[i, column["db_pwd"]].StringValue;
                        appinfo.DDLUser = sheet.Cells[i, column["db_udt_user"]].StringValue;
                        appinfo.DDLPassword = sheet.Cells[i, column["db_udt_pwd"]].StringValue;
                        appinfo.Comment = sheet.Cells[i, column["app_comment"]].StringValue;
                        appinfo.TrySetEnabled(sheet.Cells[i, column["enabled"]].StringValue);

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
                    foreach (string name in newApps)
                        Server.Manager.CloneApplication(name);

                    //Parallel.ForEach(newApps, name =>
                    //{
                    //    Server.Manager.CloneApplication(name);
                    //});

                    //先將 Application 的 Enabled 屬性設定好。
                    foreach (DSAppInfo app in import_list)
                        Server.Manager.SetApplicationEnable(app.Name, app.Enabled);

                    //Parallel.ForEach(import_list, app =>
                    //{
                    //    Server.Manager.SetApplicationEnable(app.Name, app.Enabled);
                    //});

                    XmlHelper req = new XmlHelper();
                    foreach (DSAppInfo app in import_list)
                    {
                        XmlHelper appreq = new XmlHelper(req.AddElement("Application"));
                        appreq.SetAttribute(".", "Name", app.Name);
                        appreq.AddElement(".", "Param", app.DBUrl).SetAttribute("Name", "db_url");
                        appreq.AddElement(".", "Param", app.DMLUser).SetAttribute("Name", "db_user");
                        appreq.AddElement(".", "Param", app.DMLPassword).SetAttribute("Name", "db_pwd");
                        appreq.AddElement(".", "Param", app.DDLUser).SetAttribute("Name", "db_udt_user");
                        appreq.AddElement(".", "Param", app.DDLPassword).SetAttribute("Name", "db_udt_pwd");
                        appreq.AddElement(".", "Param", app.Comment).SetAttribute("Name", "app_comment");
                    }
                    Server.Manager.SetApplicationsArgument(req.GetElement("."));

                    XmlElement result = Server.Manager.ReloadServer();

                    XmlNodeList nodes = result.SelectNodes("Result[@Status!='0']");
                    if (nodes.Count > 0)
                        MessageBox.Show(XmlHelper.Format(result.OuterXml));

                    Server.ReloadApplications();
                    Server.ReloadConfiguration();
                    ServerManagePanel.Instance.SetServerObject(Server);

                    MessageBox.Show("匯入完成!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private class DSAppInfo
        {
            public string Name { get; set; }

            public string DBUrl { get; set; }

            public string DMLUser { get; set; }

            public string DMLPassword { get; set; }

            public string DDLUser { get; set; }

            public string DDLPassword { get; set; }

            public string Comment { get; set; }

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
