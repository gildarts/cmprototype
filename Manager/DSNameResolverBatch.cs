using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Threading;
using FISCA.DSA;

namespace Manager
{
    public partial class DSNameResolverBatch : Office2007Form
    {
        private List<Application> Applications { get; set; }

        private Connection Connection { get; set; }

        internal DSNameResolverBatch(List<Application> apps)
        {
            InitializeComponent();

            Applications = apps;
        }

        private void DSNameResolverBatch_Load(object sender, EventArgs e)
        {
            try
            {
                FISCA.DSA.Connection conn = new FISCA.DSA.Connection();
                conn.EnableSession = false;
                conn.Connect("http://dsns1.ischool.com.tw/dsns/dsns", "", "anonymous", "");

                Connection = conn;

                dgvTestList.Rows.Clear();
                foreach (Application each in Applications)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvTestList, "準備中", each.Name, "ta." + each.Name, "sa." + each.Name, each.Comment);
                    row.Tag = each;

                    AsyncRunner<ResolverState, object> runner = new AsyncRunner<ResolverState, object>();
                    ResolverState state = new ResolverState();
                    state.Row = row;
                    state.SchoolAccessName = each.Name;
                    state.TeacherAccessName = "ta." + each.Name;
                    state.StudentAccessName = "sa." + each.Name;

                    runner.Arguments = state;
                    runner.Task = x =>
                    {
                        string msg;

                        if (!Resolver(x.Arguments.SchoolAccessName, out msg))
                            x.Arguments.SchoolAccessMessage = msg;

                        //if (!Resolver(x.Arguments.TeacherAccessName, out msg))
                        //    x.Arguments.TeacherAccessMessage = msg;

                        //if (!Resolver(x.Arguments.StudentAccessName, out msg))
                        //    x.Arguments.StudentAccessMessage = msg;
                    };
                    runner.Complete = x =>
                    {
                        if (InvokeRequired)
                        {
                            Invoke(new Action<ResolverState>(xx =>
                            {
                                ShowResolveMessage(xx);
                            }), x.Arguments);
                        }
                        else
                            ShowResolveMessage(x.Arguments);
                    };
                    row.Tag = runner;

                    dgvTestList.Rows.Add(row);
                }
                dgvTestList.AutoResizeColumns();

                foreach (DataGridViewRow each in dgvTestList.Rows)
                {
                    each.Cells[0].Value = "開始測試";
                    (each.Tag as AsyncRunner<ResolverState, object>).Run();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowResolveMessage(ResolverState state)
        {
            DataGridViewRow row = state.Row;
            row.Cells[1].ErrorText = state.SchoolAccessMessage;
            row.Cells[2].ErrorText = state.TeacherAccessMessage;
            row.Cells[3].ErrorText = state.StudentAccessMessage;

            row.Cells[0].Value = state.HasError ? "錯誤" : "正常";
        }

        private bool Resolver(string name, out string msg)
        {
            try
            {
                FISCA.XHelper req = new FISCA.XHelper();
                //req.SetText(".", name);
                req.SetInnerXml(".", name);

                FISCA.XHelper rsp = Connection.SendRequest("DS.NameService.GetDoorwayURL", new Envelope(req)).XResponseBody();

                string url = rsp.GetText(".");

                if (!string.IsNullOrWhiteSpace(url))
                {
                    msg = url;

                    try
                    {
                        Connection conn = new Connection();
                        conn.Connect(url, "", "anonymous", "");
                    }
                    catch (Exception ex)
                    {
                        if (ex is FISCA.DSA.DSAServerException)
                        {
                            FISCA.DSA.DSAServerException srvex = ex as FISCA.DSA.DSAServerException;
                            if (srvex.Status == "502")
                                return true;
                        }

                        msg = ex.Message + "\n位置：" + url;
                        return false;
                    }

                    return true;
                }
                else
                {
                    msg = "此名稱無任何對應!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        class ResolverState
        {
            public DataGridViewRow Row { get; set; }

            public string SchoolAccessName { get; set; }

            public string TeacherAccessName { get; set; }

            public string StudentAccessName { get; set; }

            public string SchoolAccessMessage { get; set; }

            public string TeacherAccessMessage { get; set; }

            public string StudentAccessMessage { get; set; }

            public bool HasError
            {
                get
                {
                    return !(string.IsNullOrWhiteSpace(SchoolAccessMessage) &&
                        string.IsNullOrWhiteSpace(TeacherAccessMessage) &&
                        string.IsNullOrWhiteSpace(StudentAccessMessage));
                }
            }
        }
    }
}
