using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Xml;
using FISCA.DSA;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Manager
{
    internal partial class LogViewer : Office2007Form
    {
        private Server Server { get; set; }

        private LogQuery Log { get; set; }

        private string ManualQuery
        {
            get { return txtSql.Text; }
            set { txtSql.Text = value; }
        }

        public LogViewer(Server srv)
        {
            InitializeComponent();
            Server = srv;
            Log = new LogQuery(Server.Manager, srv.Configuration.Log.Target, srv.SuperUser);

            txtStartTime.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd HH:mm:ss");
            txtEndTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            List<DataItem> items = new List<DataItem>();
            items.Add(new DataItem("group_time", "區間"));
            items.Add(new DataItem("pcount", "處理量"));
            items.Add(new DataItem("avg_rsp_time", "平均反應時間"));
            items.Add(new DataItem("max_rsp_time", "最慢反應時間"));
            items.Add(new DataItem("min_rsp_time", "最快反應時間"));
            items.Add(new DataItem("total_rsp_time", "負載量"));
            cboOrderField.DisplayMember = "DisplayText";
            cboOrderField.ValueMember = "Value";
            foreach (DataItem each in items)
                cboOrderField.Items.Add(each);

            cboGroupRange.SelectedIndex = cboGroupRange.FindString("Day");
            cboOrderField.SelectedIndex = cboOrderField.FindString("負載量");
            cboOrder.SelectedIndex = cboOrder.FindString("由大到小");
            txtLimit.Text = "20";

            foreach (Application each in Server.Applications)
                txtSchool.AutoCompleteCustomSource.Add(each.Name);

            LogDetailTabControl.SelectedTab = TabRequest;

            Stream sqllang = Assembly.GetExecutingAssembly().GetManifestResourceStream("Manager.SyntaxLanguage.ActiproSoftware.SQL.xml");
            txtSql.Document.LoadLanguageFromXml(sqllang, 0);
            sqllang.Seek(0, SeekOrigin.Begin);
            txtGroupSql.Document.LoadLanguageFromXml(sqllang, 0);
            sqllang.Close();
        }

        private void btnRefresLogCount_Click(object sender, EventArgs e)
        {
            lblLogCount.Text = string.Format("日誌總筆數：{0}", Log.GetLogTotalCount().ToString("n0"));
            lblLogSize.Text = string.Format("日誌資料容量：{0}", Log.GetLogDatabaseSize().ToString("n0"));
        }

        private void btnQueryRspTime_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime start, end;

                if (!DateTime.TryParse(txtStartTime.Text, out start))
                {
                    MessageBox.Show("開始時間不正確。");
                    return;
                }

                if (!DateTime.TryParse(txtEndTime.Text, out end))
                {
                    MessageBox.Show("結束時間不正確。");
                    return;
                }

                int limit;
                if (!int.TryParse(txtLimit.Text, out limit))
                    limit = 20;

                string execSql;
                XmlElement rsp = Log.CalcResponseTime(
                    (LogQuery.GroupRange)Enum.Parse(typeof(LogQuery.GroupRange), cboGroupRange.SelectedItem.ToString(), true),
                    txtSchool.Text, txtContract.Text, txtService.Text,
                    start, end,
                    (LogQuery.ResponseTimeOrderField)Enum.Parse(typeof(LogQuery.ResponseTimeOrderField), cboOrderField.SelectedItem.ToString(), true),
                    (cboOrder.SelectedItem.ToString() == "由大到小") ? SortOrder.Descending : SortOrder.Ascending,
                    limit, out execSql);

                txtGroupSql.Text = execSql;
                dgvRspTime.Rows.Clear();
                foreach (XmlElement each in rsp.SelectNodes("Record"))
                {
                    FISCA.XHelper hlp = new FISCA.XHelper(each);
                    string range = hlp.GetText("group_time");
                    string count = hlp.GetText("pcount");
                    decimal avg_rsp_time = Math.Round(hlp.TryGetDecimal("avg_rsp_time", -1), 0);
                    decimal max_rsp_time = Math.Round(hlp.TryGetDecimal("max_rsp_time", -1), 0);
                    decimal min_rsp_time = Math.Round(hlp.TryGetDecimal("min_rsp_time", -1), 0);
                    decimal total_rsp_time = Math.Round(hlp.TryGetDecimal("total_rsp_time", -1), 0);

                    DataGridViewRow row = new DataGridViewRow();
                    row.Tag = each;
                    row.CreateCells(dgvRspTime, range, count, avg_rsp_time.ToString("n0"), max_rsp_time.ToString("n0"), min_rsp_time.ToString("n0"), total_rsp_time.ToString("n0"));
                    dgvRspTime.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                ErrorForm err = new ErrorForm();
                err.Display("查詢時爆掉了!", ex);
            }
        }

        class DataItem
        {
            public DataItem(string value, string displayText)
            {
                Value = value;
                DisplayText = displayText;
            }

            public string Value { get; private set; }

            public string DisplayText { get; private set; }

            public override string ToString()
            {
                return Value;
            }
        }

        private void dgvRspTime_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgvRspTime.Rows[e.RowIndex];
                XmlElement xml = row.Tag as XmlElement;

                string range = cboGroupRange.SelectedItem.ToString();
                string dt = xml.SelectSingleNode("group_time").InnerText;

                List<string> conds = new List<string>();

                if (!string.IsNullOrWhiteSpace(txtSchool.Text))
                    conds.Add(string.Format("dsa_application_name = '{0}'", txtSchool.Text));

                if (!string.IsNullOrWhiteSpace(txtContract.Text))
                    conds.Add(string.Format("dsa_contract_name = '{0}'", txtContract.Text));

                if (!string.IsNullOrWhiteSpace(txtService.Text))
                    conds.Add(string.Format("dsa_service_name = '{0}'", txtService.Text));

                string strCond = string.Empty;
                if (conds.Count > 0)
                    strCond = "and " + string.Join(" and ", conds.ToArray());

                ManualQuery = string.Format(SQL.QueryRspTimeDetail, range, dt, strCond);
                InjectLimit();

                tabControl1.SelectedTab = ListQuery;
                ExecuteManualQuery();
            }
            catch { MessageBox.Show("Bomb...."); }
        }

        private void txtSql_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                ExecuteManualQuery();
        }

        private void ExecuteManualQuery()
        {
            try
            {
                AsyncRunner runner = new AsyncRunner();
                runner.Message = "執行 SQL 中…";
                runner.Run(
                    x =>
                    {
                        XmlElement data = Log.ExecuteManualQuery(FilterQuery());

                        StringReader reader = new StringReader(data.OuterXml);
                        x.Result = reader;
                    },
                    x =>
                    {
                        if (x.IsTaskError)
                        {
                            ErrorForm err = new ErrorForm();
                            err.Display(x.TaskError.Message, x.TaskError);
                        }
                        else
                        {
                            StringReader reader = x.Result as StringReader;
                            DataSet ds = new DataSet();
                            ds.ReadXml(reader, XmlReadMode.Auto);

                            if (ds.Tables.Count <= 0)
                                dgvManualQData.DataSource = null;
                            else
                            {
                                dgvManualQData.AutoGenerateColumns = true;
                                dgvManualQData.DataSource = ds.Tables[0];
                                dgvManualQData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
                            }
                        }
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string FilterQuery()
        {
            List<string> denies = new List<string>();
            denies.Add(@"insert\s+into\s.*values");
            denies.Add(@"update\s.*set\s");

            foreach (string eachi in denies)
            {
                Regex rx = new Regex(eachi, RegexOptions.IgnoreCase);
                Match m = rx.Match(ManualQuery);
                if (m.Success)
                    throw new Exception("您使用的可能不安裝的指令，無法執行!");
            }

            return ManualQuery;
        }

        private void InjectLimit()
        {
            ManualQuery = ManualQuery + "\nlimit " + txtLimit.Text;
        }

        private void btnQueryDetail_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtLogID.Text;
                string identity = txtIdentity.Text;
                string logdatetime = txtLogDateTime.Text;

                AsyncRunner runner = new AsyncRunner();
                runner.Message = "讀取日誌中...";
                runner.MessageOwner = this;

                runner.Run(
                    x =>
                    {
                        x.Result = Log.GetLogDetail(id, identity, logdatetime);
                    },
                    x =>
                    {
                        dgvDetailList.Rows.Clear();
                        if (x.IsTaskError)
                            MessageBox.Show("Task Bomb...");
                        else
                        {
                            XmlElement logs = x.Result as XmlElement;
                            foreach (XmlElement each in logs.SelectNodes("Record"))
                            {
                                FISCA.XHelper hlp = new FISCA.XHelper(each);
                                string school = hlp.GetText("dsa_application_name");
                                string contract = hlp.GetText("dsa_contract_name");
                                string service = hlp.GetText("dsa_service_name");
                                string pTime = hlp.GetText("process_time");

                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(dgvDetailList, school, contract, service, pTime);
                                row.Tag = each;
                                dgvDetailList.Rows.Add(row);
                            }
                            dgvDetailList.AutoResizeColumns();

                            if (dgvDetailList.Rows.Count > 0)
                            {
                                dgvDetailList.Rows[0].Selected = true;
                                ShowLogRowDetail(dgvDetailList.Rows[0]);
                            }
                        }
                    });
            }
            catch { MessageBox.Show("Bomb..."); }
        }

        private void dgvDetailList_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgvDetailList.Rows[e.RowIndex];
                ShowLogRowDetail(row);
            }
            catch { MessageBox.Show("Bomb..."); }
        }

        private void ShowLogRowDetail(DataGridViewRow row)
        {
            XmlElement log = row.Tag as XmlElement;
            FISCA.XHelper hlp = new FISCA.XHelper(log);

            string compressed = hlp.GetText("data_compressed");

            bLogID.Text = hlp.GetText("id");
            bIdentity.Text = hlp.GetText("identity");
            bException.Text = hlp.GetText("occure_exception") == "t" ? "是" : "否";
            bDateTime.Text = hlp.GetText("log_time");
            request.Text = MaskSecret(compressed == "t" ? Decompress(hlp.GetText("dsrequest")) : hlp.GetText("dsrequest"));
            response.Text = compressed == "t" ? Decompress(hlp.GetText("dsresponse")) : hlp.GetText("dsresponse");
            processlog.Text = Decompress(hlp.GetText("process_log"));
            clientinfo.Text = hlp.GetText("client_info");
            serverinfo.Text = hlp.GetText("server_info");
            exception.Text = hlp.GetText("exception");

            xmlsyntax.FormatDocument(request.Document);
            xmlsyntax.FormatDocument(response.Document);
            xmlsyntax.FormatDocument(processlog.Document);
            xmlsyntax.FormatDocument(clientinfo.Document);
            xmlsyntax.FormatDocument(serverinfo.Document);
        }

        private static string MaskSecret(string data)
        {
            string xml = data;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlNode password = doc.DocumentElement.SelectSingleNode("Header/SecurityToken/Password");
                if (password != null)
                    password.InnerText = "這是秘密XD!";

                XmlNode appkey = doc.DocumentElement.SelectSingleNode("Header/SecurityToken/ApplicationKey");
                if (appkey != null)
                    appkey.RemoveAll();

                return doc.DocumentElement.OuterXml;
            }
            catch
            {
                return xml;
            }
        }

        private static string Decompress(string data)
        {
            if (data.StartsWith("<")) return data;

            try
            {
                byte[] compressed = Convert.FromBase64String(data);
                GZipStream gzip = new GZipStream(new MemoryStream(compressed), CompressionMode.Decompress);
                StreamReader reader = new StreamReader(gzip, Encoding.UTF8);

                string result = reader.ReadToEnd();
                reader.Close();
                gzip.Close();

                return result;
            }
            catch
            {
                return data;
            }
        }

        private void btnTextView_Click(object sender, EventArgs e)
        {
            if (dgvManualQData.SelectedCells.Count > 0)
            {
                TextViewer.View(dgvManualQData.SelectedCells[0].Value.ToString());
            }
        }

        private void btnXmlView_Click(object sender, EventArgs e)
        {
            if (dgvManualQData.SelectedCells.Count > 0)
            {
                XmlViewer.View(dgvManualQData.SelectedCells[0].Value.ToString());
            }
        }

        private void btnBase64XmlView_Click(object sender, EventArgs e)
        {
            if (dgvManualQData.SelectedCells.Count > 0)
            {
                try
                {
                    string compress = dgvManualQData.SelectedCells[0].Value.ToString();

                    byte[] compressBinary = Convert.FromBase64String(compress);

                    GZipStream de = new GZipStream(new MemoryStream(compressBinary), CompressionMode.Decompress);

                    StreamReader reader = new StreamReader(de, Encoding.UTF8);

                    string uncompress = reader.ReadToEnd();
                    reader.Close();

                    XmlViewer.View(uncompress);
                }
                catch (Exception ex)
                {
                    ErrorForm.Show(ex.Message, ex);
                }
            }
        }
    }
}
