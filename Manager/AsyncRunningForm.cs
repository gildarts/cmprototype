using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using DevComponents.DotNetBar;

namespace Manager
{
    public partial class AsyncRunningForm : Office2007Form
    {
        public AsyncRunningForm()
        {
            InitializeComponent();
            lblMsg.Text = string.Empty;
        }

        public Action OnloadAction { get; set; }

        private string _msg = string.Empty;
        /// <summary>
        /// 設定顯示訊息，支援背景執行緒呼叫。
        /// </summary>
        public string Message
        {
            get { return _msg; }
            set
            {
                if (InvokeRequired)
                    Invoke(new Action<string>(x =>
                    {
                        lblMsg.Text = x;
                        System.Windows.Forms.Application.DoEvents();
                    }), value);
                else
                    lblMsg.Text = value;
            }
        }

        private void AsyncRunningForm_Load(object sender, EventArgs e)
        {
            if (OnloadAction != null)
                OnloadAction();
        }
    }
}
