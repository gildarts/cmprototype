using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using FISCA.DSA;

namespace Manager
{
    public partial class NameServiceForm : Office2007Form
    {
        public NameServiceForm()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                FISCA.DSA.Connection cn1 = new FISCA.DSA.Connection();
                cn1.EnableSession = false;
                cn1.Connect("http://dsns1.ischool.com.tw/dsns/dsns", "", "anonymous", "");

                Connection conn = cn1;

                FISCA.XHelper req = new FISCA.XHelper();
                //req.SetText(".", txtAccessPoint.Text);
                req.SetInnerXml(".", txtAccessPoint.Text);

                FISCA.XHelper rsp = conn.SendRequest("DS.NameService.GetDoorwayURL", new Envelope(req)).XResponseBody();

                string url = rsp.GetText(".");

                if (!string.IsNullOrWhiteSpace(url))
                    txtPhysicalUrl.Text = url;
                else
                    txtPhysicalUrl.Text = "<此名稱無任何對應>";
            }
            catch (Exception ex)
            {
                ErrorForm form = new ErrorForm();
                form.Display(ex.Message, ex);
            }
        }

        private void NameServiceForm_Load(object sender, EventArgs e)
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

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                Connection conn = new Connection();
                conn.Connect(txtPhysicalUrl.Text, "", "", "");
                MessageBox.Show("測試連線成功。");
            }
            catch (DSAServerException ex)
            {
                if (ex.Status == "502")
                    MessageBox.Show("測試連線成功。");
                else
                {
                    ErrorForm form = new ErrorForm();
                    form.Display(ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                ErrorForm form = new ErrorForm();
                form.Display(ex.Message, ex);
            }
        }
    }
}