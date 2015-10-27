using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Linq;
using Aspose.Cells;
using DevComponents.DotNetBar;
using FISCA;
using FISCA.DSA;

namespace Manager.NameService
{
    public partial class MainManager : Office2007Form
    {
        private Connection Connection { get; set; }

        private List<AccessPointName> DataSource { get; set; }

        private List<AccessPointName> FilteredDataSource { get; set; }

        public MainManager()
        {
            InitializeComponent();
            dgvDSNSList.AutoGenerateColumns = false;
        }

        private void MainManager_Load(object sender, EventArgs e)
        {
        }

        private void AssignToDataGridView(List<AccessPointName> aps)
        {
            dgvDSNSList.DataSource = new BindingList<AccessPointName>(aps);
        }

        private List<AccessPointName> GetAccessPointNames()
        {
            List<AccessPointName> aps = new List<AccessPointName>();
            XElement xrsp = GetAllRecordFromServer();

            foreach (XElement each in xrsp.Elements("DSNS"))
                aps.Add(new AccessPointName(each));

            aps.Sort((x, y) =>
            {
                return x.Memo.CompareTo(y.Memo);
            });

            return aps;
        }

        private XElement GetAllRecordFromServer()
        {
            Envelope rsp = Connection.SendRequest("DS.NameService.Export", new Envelope());
            XElement xrsp = XElement.Parse(rsp.BodyContent.XmlString);
            return xrsp;
        }

        private static PassportToken GetPassportToken()
        {
            Envelope rsp = Program.Connection.SendRequest("DS.Base.GetPassportToken", new Envelope());
            PassportToken token = new PassportToken(rsp.BodyContent);
            return token;
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            InitData();

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "*.xml|*.xml";
            dialog.FileName = "dsns.xml";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                XElement data = GetAllRecordFromServer();
                data.Save(dialog.FileName);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            InitData();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*.xml|*.xml";
            dialog.FileName = "dsns.xml";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    XElement data = XElement.Load(dialog.FileName);
                    SendToServer(data);
                    MessageBox.Show("復原資料完成。");
                    ReloadAllData();
                    DoFind(txtFilter.Text);
                }
                catch (Exception ex)
                {
                    ErrorForm.Show("復原資料錯誤!", ex);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            InitData();

            XElement req = AccessPointNameToRequest(DataSource);

            SendToServer(req);
            ReloadAllData();
            DoFind(txtFilter.Text);
        }

        private XElement AccessPointNameToRequest(List<AccessPointName> aps)
        {
            XElement req = new XElement("Request");

            foreach (AccessPointName name in aps)
            {
                req.Add(name.ToXElement());
            }
            return req;
        }

        private void SendToServer(XElement data)
        {
            Envelope req = new Envelope(new XStringHolder(data));
            Connection.SendRequest("DS.NameService.Import", req);
        }

        private void ReloadAllData()
        {
            AsyncRunner runner = new AsyncRunner();
            runner.Message = "重新下載資料中...";
            runner.MessageOwner = this;
            runner.Run(arg =>
            {
                DataSource = GetAccessPointNames();
            }, arg =>
            {
                FilteredDataSource = new List<AccessPointName>();
                AssignToDataGridView(DataSource);
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DoFind(txtFilter.Text);
        }

        private void DoFind(string pattern)
        {
            List<AccessPointName> filtered = new List<AccessPointName>();
            foreach (AccessPointName ap in DataSource)
            {
                if (ap.Name.IndexOf(pattern) >= 0)
                {
                    filtered.Add(ap);
                    continue;
                }

                if (ap.Url.IndexOf(pattern) >= 0)
                {
                    filtered.Add(ap);
                    continue;
                }

                if (ap.SecuredUrl.IndexOf(pattern) >= 0)
                {
                    filtered.Add(ap);
                    continue;
                }
                if (ap.Catalog.IndexOf(pattern) >= 0)
                {
                    filtered.Add(ap);
                    continue;
                }

                if (ap.Title.IndexOf(pattern) >= 0)
                {
                    filtered.Add(ap);
                    continue;
                }

                if (ap.Memo.IndexOf(pattern) >= 0)
                {
                    filtered.Add(ap);
                    continue;
                }
            }
            AssignToDataGridView(filtered);
            FilteredDataSource = filtered;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            txtDSNSHost.Enabled = true;
            InitData();
        }

        private void InitData()
        {
            if (!txtDSNSHost.Enabled) return;

            string dsns = txtDSNSHost.Text;
            AsyncRunner runner = new AsyncRunner();
            runner.Message = "下載資料中…";
            runner.MessageOwner = this;
            runner.Run(arg =>
            {
                if (Program.Connection == null)
                    throw new Exception("必需要連線才能使用。");

                Connection = new Connection();
                Connection.Connect(dsns, "setup", GetPassportToken());

                arg.Result = GetAccessPointNames();
            },
            arg =>
            {
                if (arg.IsTaskError)
                {
                    ErrorForm.Show("下載資料錯誤!", arg.TaskError);
                }
                else
                {
                    AssignToDataGridView(arg.Result as List<AccessPointName>);
                    DataSource = arg.Result as List<AccessPointName>;
                    txtDSNSHost.Enabled = false;
                }
            });
        }

        private void dgvDSNSList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDSNSList.BeginEdit(true);
        }

        private void btnBackupExcel_Click(object sender, EventArgs e)
        {
            InitData();

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "*.xls|*.xls";
            dialog.FileName = "dsns.xls";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<AccessPointName> aps = GetAccessPointNames();

                SaveAsExcel(aps, dialog.FileName);
            }
        }

        private void SaveAsExcel(List<AccessPointName> aps, string fileName)
        {
            try
            {
                Workbook book = new Workbook();
                book.Worksheets.Clear();
                Worksheet sheet = book.Worksheets[book.Worksheets.Add()];

                int columnIndex = 0;
                int rowIndex = 0;

                sheet.Cells[0, columnIndex++].PutValue("Name");
                sheet.Cells[0, columnIndex++].PutValue("URL");
                sheet.Cells[0, columnIndex++].PutValue("SecuredURL");
                sheet.Cells[0, columnIndex++].PutValue("Title");
                sheet.Cells[0, columnIndex++].PutValue("Memo");
                sheet.Cells[0, columnIndex++].PutValue("Catalog");
                sheet.Cells[0, columnIndex++].PutValue("Active");
                sheet.Cells[0, columnIndex++].PutValue("IsPublic");

                rowIndex++;
                foreach (AccessPointName ap in aps)
                {
                    columnIndex = 0;
                    sheet.Cells[rowIndex, columnIndex++].PutValue(ap.Name);
                    sheet.Cells[rowIndex, columnIndex++].PutValue(ap.Url);
                    sheet.Cells[rowIndex, columnIndex++].PutValue(ap.SecuredUrl);
                    sheet.Cells[rowIndex, columnIndex++].PutValue(ap.Title);
                    sheet.Cells[rowIndex, columnIndex++].PutValue(ap.Memo);
                    sheet.Cells[rowIndex, columnIndex++].PutValue(ap.Catalog);
                    sheet.Cells[rowIndex, columnIndex++].PutValue(ap.Active);
                    sheet.Cells[rowIndex, columnIndex++].PutValue(ap.IsPublic);

                    rowIndex++;
                }

                book.Save(fileName);
                Process.Start(fileName);
            }
            catch (Exception ex)
            {
                ErrorForm.Show("儲存失敗！", ex);
            }
        }

        private void btnRestoreExcel_Click(object sender, EventArgs e)
        {
            InitData();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*.xls|*.xls";
            dialog.FileName = "dsns.xls";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    SendToServer(FromExcel(dialog.FileName));
                    MessageBox.Show("復原資料完成。");
                    ReloadAllData();
                    DoFind(txtFilter.Text);
                }
                catch (Exception ex)
                {
                    ErrorForm.Show("復原資料錯誤!", ex);
                }
            }
        }

        private XElement FromExcel(string fileName)
        {
            try
            {
                List<AccessPointName> aps = new List<AccessPointName>();
                Dictionary<string, int> column_map = new Dictionary<string, int>();

                Workbook book = new Workbook();
                book.Open(fileName);
                Worksheet sheet = book.Worksheets[0];
                ReadColumnName(column_map, sheet);

                for (int i = 1; i <= sheet.Cells.MaxDataRow; i++)
                {
                    AccessPointName ap = new AccessPointName();

                    ap.Name = sheet.Cells[i, column_map["Name"]].StringValue;
                    ap.Url = sheet.Cells[i, column_map["URL"]].StringValue;
                    ap.SecuredUrl = sheet.Cells[i, column_map["SecuredURL"]].StringValue;
                    ap.Title = sheet.Cells[i, column_map["Title"]].StringValue;
                    ap.Memo = sheet.Cells[i, column_map["Memo"]].StringValue;
                    ap.Catalog = sheet.Cells[i, column_map["Catalog"]].StringValue;
                    ap.SetActive(sheet.Cells[i, column_map["Active"]].StringValue);
                    ap.SetIsPublic(sheet.Cells[i, column_map["IsPublic"]].StringValue);

                    aps.Add(ap);
                }

                return AccessPointNameToRequest(aps);
            }
            catch (Exception ex)
            {
                ErrorForm.Show("復原資料錯誤！", ex);
            }

            return null;
        }

        private static void ReadColumnName(Dictionary<string, int> column_map, Worksheet sheet)
        {
            for (int i = 0; i <= sheet.Cells.MaxDataColumn; i++)
            {
                string column_name = sheet.Cells[0, i].StringValue;

                if (string.IsNullOrWhiteSpace(column_name))
                    continue;

                column_map.Add(column_name, i);
            }
        }
    }
}
