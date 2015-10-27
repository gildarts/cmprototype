using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Drawing;

namespace FISCA.PrivateControls
{
    partial class LoginForm : Form
    {
        private static LicenseInfo _license;

        public LoginForm()
        {
            InitializeComponent();
            string filename = Application.StartupPath + "\\" + "configuration.xml";
            XmlDocument doc = new XmlDocument();
            if ( File.Exists(filename) )
            {
                doc.Load(filename);
                foreach ( XmlElement item in doc.SelectNodes("Informations/LoginAccountList/LoginName") )
                {
                    cboAccount.Items.Add(item.InnerText);
                }
                if ( cboAccount.Items.Count > 0 )
                {
                    cboAccount.SelectedIndex = 0;
                    txtPassword.SelectAll();
                }
            }
        }

        public void SetPicture(Image pic)
        {
            pictureBox1.Visible = true;
            pictureBox1.Image = pic;
            pictureBox1.Size = pic.Size;
            if ( 310 - pic.Width - 20 < 0 )
                this.Width = pic.Width + 20;
            else
                this.Width = 310;
            this.Height = pic.Height + 153;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            lnkViewLicense.Visible = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void Login()
        {
            if ( cboAccount.Text.Length > 0 && txtPassword.Text.Length > 0 )
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                try
                {
                    FISCA.Authentication.DSAServices.SetLicense(_license.LincenseStream);
                    FISCA.Authentication.DSAServices.Login(cboAccount.Text, txtPassword.Text);

                    this.Enabled = false;

                    if ( checkBoxX1.Checked )
                    {
                        string filename = Application.StartupPath + "\\" + "configuration.xml";
                        XmlDocument doc = new XmlDocument();
                        if ( File.Exists(filename) )
                            doc.Load(filename);
                        XmlElement inf = (XmlElement)doc.SelectSingleNode("Informations");
                        if ( inf == null )
                        {
                            inf = doc.CreateElement("Informations");
                            doc.AppendChild(inf);
                        }
                        XmlElement ele = (XmlElement)doc.SelectSingleNode("Informations/LoginAccountList");
                        if ( ele == null )
                        {
                            ele = doc.CreateElement("LoginAccountList");
                            inf.AppendChild(ele);
                        }
                        XmlElement acc = null;
                        foreach ( XmlElement item in ele.SelectNodes("LoginName") )
                        {
                            if ( item.InnerText == cboAccount.Text )
                            {
                                acc = item;
                            }
                        }
                        if ( acc == null )
                        {
                            acc = doc.CreateElement("LoginName");
                            acc.InnerText = cboAccount.Text;
                            ele.AppendChild(acc);
                        }
                        ele.PrependChild(acc);
                        doc.Save(filename);
                    }
                    this.Close();
                }
                catch ( Exception ex )
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message, Application.ProductName);
                    this.Enabled = true;
                }
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("請輸入帳號密碼");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkSysAdmin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetLicense sl = new SetLicense();

            DialogResult dr = sl.ShowDialog();
            sl.Close();

            if ( dr == DialogResult.OK )
            {
                LoadLicense(new LicenseInfo());
                lnkViewLicense.Enabled = true;
            }
        }

        private void SetCaption(string msg)
        {
            Text = string.Format("使用者登入 ({0})", msg);
        }

        private void LoadLicense(LicenseInfo license)
        {
            try
            {
                _license = license;
                license.DecryptLicense();
                SetCaption(string.Format("已授權登入 {0}", license.AccessPoint));

                txtPassword.Enabled = true;
                cboAccount.Enabled = true;
                btnLogin.Enabled = true;
                checkBoxX1.Enabled = true;
            }
            catch ( Exception ex )
            {
                _license = null;
                SetCaption(ex.Message);
                txtPassword.Enabled = false;
                cboAccount.Enabled = false;
                btnLogin.Enabled = false;
                checkBoxX1.Enabled = false;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if ( !string.IsNullOrEmpty(cboAccount.Text) )
            {
                cboAccount.Select(0, 0);
                txtPassword.Focus();
                txtPassword.Select();
            }
            else
            {
                cboAccount.Focus();
            }

            LicenseInfo lic = new LicenseInfo();

            if ( lic.LicenseExists() )
            {
                LoadLicense(lic);
                lnkViewLicense.Enabled = true;
            }
            else
            {
                SetCaption("未安裝授權檔");
                txtPassword.Enabled = false;
                cboAccount.Enabled = false;
                btnLogin.Enabled = false;
                checkBoxX1.Enabled = false;
            }
        }

        private void lnkViewLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseInfoForm form = new LicenseInfoForm(_license);
            form.ShowDialog();
        }
    }
}
