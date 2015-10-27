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
    public partial class ChangeSecureCode : Office2007Form
    {
        public ChangeSecureCode()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNewCode.Text))
                    throw new Exception("您不可以將安全代碼改成空白。");

                string originCipher = Program.GetOnlinePreference(LoginForm.WidgetKey, LoginForm.StateID);

                if (string.IsNullOrWhiteSpace(originCipher))
                    throw new Exception("您無法變更安全代碼，請重新登入。");

                string originPlain = Password.DecryptoData(originCipher, txtOldCode.Text);
                string newCipher = Password.EncryptoData(originPlain, txtNewCode.Text);
                Program.SetOnlinePreference(LoginForm.WidgetKey, LoginForm.StateID, newCipher);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                ErrorForm form = new ErrorForm();
                form.Display(ex.Message, ex);
            }
        }
    }
}
