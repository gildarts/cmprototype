using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Manager
{
    public partial class SetSuperUserForm : Office2007Form
    {
        private Server Server { get; set; }

        internal SetSuperUserForm(Server srv)
        {
            InitializeComponent();
            Server = srv;

            chkDefault.Checked = (srv.SuperUser == AccountData.Default);
            if (srv.SuperUser != AccountData.Default)
            {
                txtUserName.Text = srv.SuperUser.UserName;
                txtPassword.Text = srv.SuperUser.Password;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (chkDefault.Checked)
                Result = AccountData.Default;
            else
            {
                Result = new AccountData(txtUserName.Text, txtPassword.Text);
                try
                {
                    Exception ex = Server.Manager.TestAccount(Result);
                    if (ex != null) throw ex;
                }
                catch (Exception ex)
                {
                    string msg = "測試連線失敗。";
                    string sqlmsg = string.Empty;
                    if (ErrorParser.TryGetSqlException(ex, out sqlmsg))
                        msg = "測試連線失敗\n" + sqlmsg;

                    ErrorForm err = new ErrorForm();
                    err.Display(msg, ex);

                    DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        internal AccountData Result { get; private set; }

        private void UserNamePassword_TextChanged(object sender, EventArgs e)
        {
            chkAssign.Checked = true;
        }

        private void CheckControl_CheckedChanged(object sender, EventArgs e)
        {
            txtUserName.Enabled = chkAssign.Checked;
            txtPassword.Enabled = chkAssign.Checked;
        }
    }
}
