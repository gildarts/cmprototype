using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using FISCA.DSA;
using System.Threading;

namespace Manager
{
    public partial class RegistryForm : Office2007Form
    {
        internal static DialogResult Confirm(ref ServerRegistryData data)
        {
            RegistryForm from = new RegistryForm(data);

            if (from.ShowDialog() == DialogResult.OK)
            {
                data = from.RegistryData;
                return DialogResult.OK;
            }
            else
                return DialogResult.Cancel;
        }

        private bool AccessPointConfirmed { get; set; }

        public RegistryForm()
        {
            InitializeComponent();
            AccessPointConfirmed = false;
        }

        internal RegistryForm(ServerRegistryData data)
        {
            InitializeComponent();
            AccessPointConfirmed = true;
            txtAccessPoint.Text = data.AccessPointUrl;
            txtUserName.Text = data.UserName;
            txtPassword.Text = data.Password;
            txtMemo.Text = data.Memo;
            txtAccessPoint.Enabled = false;
        }

        internal ServerRegistryData RegistryData { get; private set; }

        private void RegistryForm_Load(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += delegate(object s1, DoWorkEventArgs e1)
            {
                string[] names = DSNameResolver.ExportAllName();
                e1.Result = names;
            };
            bw.RunWorkerCompleted += delegate(object s1, RunWorkerCompletedEventArgs e1)
            {
                txtAccessPoint.AutoCompleteCustomSource.AddRange(e1.Result as string[]);
            };
            bw.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtAccessPoint_TextChanged(object sender, EventArgs e)
        {
            if (AccessPointConfirmed) return;

            btnRegistry.Enabled = true;
            ErrorNotify.SetError(txtAccessPoint, string.Empty);

            string pattern = txtAccessPoint.Text.Trim().ToLower();
            foreach (string each in Program.Servers.Keys)
            {
                if (each == pattern)
                {
                    ErrorNotify.SetError(txtAccessPoint, "此 DSA Server 已註冊。");
                    btnRegistry.Enabled = false;
                }
            }
        }

        private void btnRegistry_Click(object sender, EventArgs e)
        {
            ServerRegistryData apreg = new ServerRegistryData();
            apreg.AccessPointUrl = txtAccessPoint.Text.Trim();
            apreg.UserName = txtUserName.Text;
            apreg.Password = txtPassword.Text;
            apreg.Memo = txtMemo.Text;

            AsyncRunner<ServerRegistryData, string> runner = new AsyncRunner<ServerRegistryData, string>();
            runner.Message = "測試連線中...";
            runner.MessageOwner = this;
            runner.Arguments = apreg;
            runner.Run(
                arg =>
                {
                    Connection conn = new Connection();
                    conn.Connect(arg.Arguments.AccessPointUrl, "", arg.Arguments.UserName, arg.Arguments.Password);

                    try
                    {
                        conn.SendRequest("Server.GetServerInfo", new Envelope());
                    }
                    catch (DSAServerException ex)
                    {
                        if (ex.Status == "501")
                            throw new ArgumentException("您必須連線到 DSA Server 的管理存取點。");
                        else
                            throw;
                    }

                    arg.Result = "測試連線成功。";
                },
                arg =>
                {
                    if (arg.IsTaskError)
                    {
                        string sqlmsg = string.Empty;
                        string msg = "測試連線錯誤：\n" + arg.TaskError.Message;

                        if (ErrorParser.TryGetSqlException(arg.TaskError, out sqlmsg))
                            msg = "測試連線錯誤\n" + sqlmsg;

                        ErrorForm ef = new ErrorForm();
                        ef.Display(msg, arg.TaskError);
                    }
                    else
                    {
                        RegistryData = apreg;
                        DialogResult = DialogResult.OK;
                    }
                });
        }
    }
}
