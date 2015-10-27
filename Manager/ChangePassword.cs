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
    public partial class ChangePassword : Office2007Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("確認密碼失敗。");
                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            OldPassword = txtOldPassword.Text;
            NewPassword = txtNewPassword.Text;
            
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 原密碼。
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密碼。
        /// </summary>
        public string NewPassword { get; set; }
    }
}
