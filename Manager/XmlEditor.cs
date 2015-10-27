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
    public partial class XmlEditor : Office2007Form
    {
        public XmlEditor(string xml)
        {
            InitializeComponent();
            txtInput.Text = xml;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            CancelEventArgs arg = new CancelEventArgs(false);

            if (Confirming != null)
                Confirming(this, arg);

            if (arg.Cancel)
                DialogResult = System.Windows.Forms.DialogResult.None;
            else
                DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 當使用者按下「確定」鈕時發生。
        /// </summary>
        public event CancelEventHandler Confirming;

        /// <summary>
        /// 使用者輸入的字串。
        /// </summary>
        public string XmlData { get { return txtInput.Text; } }

    }
}
