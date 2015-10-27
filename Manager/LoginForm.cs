using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;
using FISCA.DSA;

namespace Manager
{
    public partial class LoginForm : Office2007Form
    {
        public const string WidgetKey = "DSA4-Manager-Tool";
        public const string StateID = "b0c75bbd-a07e-4464-86af-4c97bbb9ebed";

        public static string SecureCode { private get; set; }

        public static void SaveState()
        {
            AsyncRunner runner = new AsyncRunner();
            runner.Message = "儲存狀態中...";
            runner.Run(
                x =>
                {
                    string plain = MainForm.GetManagerTreeState().OuterXml;
                    string cipher = Password.EncryptoData(plain, SecureCode);
                    Program.SetOnlinePreference(WidgetKey, StateID, cipher);
                },
                x =>
                {
                    x.Message = "儲存完成！";
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(300);
                });
        }

        private static void LoadState(string xmldata)
        {
            try
            {
                string plain = Password.DecryptoData(xmldata, SecureCode);
                XmlElement state = FISCA.XHelper.ParseAsDOM(plain);
                MainForm.CreateManagerTree(state);
            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {
                throw new ArgumentException("解密資料失敗，請確認您的安全代碼是否正確。", ex);
            }
        }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(LoginData))
                {
                    string cipher = File.ReadAllText(LoginData);
                    string plain = Password.UnProtected(cipher);
                    FISCA.XHelper data = new FISCA.XHelper(FISCA.XHelper.ParseAsDOM(plain));
                    txtUserName.Text = data.GetText("UserName");
                    txtPassword.Text = data.GetText("Password");
                    txtSecretCode.Text = data.GetText("SecureCode");
                    chkSaveLocal.Checked = true;
                }
            }
            catch { }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            bool originMode = Program.OnlineMode;
            bool originEncrypt = Program.EncryptPassword;
            try
            {
                Program.OnlineMode = true; //設定為線上模式。
                Program.EncryptPassword = false; //不用預設加密機制，因為會另外整個資料完整加密。

                DoLoginAction();

                if (chkSaveLocal.Checked)
                    SaveLoginData();
                else
                {
                    try { File.Delete(LoginData); }
                    catch { }
                }

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                Program.OnlineMode = originMode;
                Program.EncryptPassword = originEncrypt;

                ErrorForm err = new ErrorForm();
                err.Display(ex.Message, ex);
            }
        }

        private readonly string LoginData = Path.Combine(System.Windows.Forms.Application.StartupPath, "login.bin");
        private void SaveLoginData()
        {
            try
            {
                FISCA.XHelper data = new FISCA.XHelper("<Login/>");
                data.AddElement(".", "UserName", txtUserName.Text);
                data.AddElement(".", "Password", txtPassword.Text);
                data.AddElement(".", "SecureCode", txtSecretCode.Text);
                File.WriteAllText(LoginData, Password.Protected(data.XmlString));
            }
            catch { }
        }

        private void DoLoginAction()
        {
            Connection conn = new Connection();
            conn.Connect(Program.Greening, "", txtUserName.Text, txtPassword.Text);

            Program.Connection = conn;
            Program.IsAdministrator = txtUserName.Text.IndexOf("@ischool.com.tw") >= 0;

            string xmldata = Program.GetOnlinePreference(WidgetKey, StateID);

            if (string.IsNullOrWhiteSpace(xmldata))
            {
                //第一次登入要決定 Secure Code。
                InputBox input = new InputBox("請設定您的安全代碼");
                input.Confirming += delegate(object o, CancelEventArgs arg)
                {
                    if (string.IsNullOrEmpty(input.InputString))
                    {
                        string msg = "您未輸入任何資料，這代表您的安全代碼要與「目前」密碼相同，您確定嗎？";
                        if (MessageBox.Show(msg, "Prototype", MessageBoxButtons.OKCancel) ==
                            DialogResult.Cancel)
                            arg.Cancel = true;
                    }
                };
                if (input.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (string.IsNullOrWhiteSpace(input.InputString))
                        SecureCode = txtPassword.Text;
                    else
                        SecureCode = input.InputString;

                    MainForm.ResetManagerTree();
                }
                else
                    throw new Exception("未決定安全代碼，無法登入系統。");
            }
            else
            {
                //依原則決定 Secure Code。
                if (string.IsNullOrWhiteSpace(txtSecretCode.Text))
                    SecureCode = txtPassword.Text;
                else
                    SecureCode = txtSecretCode.Text;

                LoadState(xmldata);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void greening_DoubleClick(object sender, EventArgs e)
        {
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            chkSaveLocal.Checked = false;
        }
    }
}
